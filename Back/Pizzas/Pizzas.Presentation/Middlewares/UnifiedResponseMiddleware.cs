using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Services.Main;

namespace Pizzas.Presentation.Middlewares;

public class UnifiedResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILocalizationService _localizationService;

    public UnifiedResponseMiddleware(RequestDelegate next, ILocalizationService localizationService)
    {
        _next = next;
        _localizationService = localizationService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);

            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();
            context.Response.Body = originalBodyStream;

            var wrapped = WrapSuccess(context, responseText);
            await context.Response.WriteAsync(wrapped);
        }
        catch (PizzasException ex)
        {
            context.Response.Body = originalBodyStream;
            await HandlePizzasException(context, ex);
        }
        catch (Exception ex)
        {
            context.Response.Body = originalBodyStream;
            await HandleUnhandledException(context, ex);
        }
    }

    private string WrapSuccess(HttpContext context, string original)
    {
        var data = JsonSerializer.Deserialize<object>(original ?? "{}");
        var meta = BuildMeta(context, context.Response.StatusCode);

        var response = new
        {
            response = data,
            status = "ok",
            meta
        };

        return JsonSerializer.Serialize(response, JsonOpts());
    }

    private async Task HandlePizzasException(HttpContext context, PizzasException ex)
    {
        var lang = GetLanguageFromRequest(context);
        var code = GetStatusCodeForExceptionType(ex.ExceptionType);
        var meta = BuildMeta(context, code);

        var errorResponse = new
        {
            status = "fail",
            code,
            message = _localizationService.GetLocalizedString(ex.Message, lang),
            ex = ex.ExceptionType.ToString(),
            meta
        };

        context.Response.StatusCode = code;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, JsonOpts()));
    }

    private async Task HandleUnhandledException(HttpContext context, Exception ex)
    {
        var meta = BuildMeta(context, 500);

        var errorResponse = new
        {
            status = "fail",
            code = 500,
            message = ex.Message,
            ex = "InternalServerError",
            meta
        };

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, JsonOpts()));
    }

    private object BuildMeta(HttpContext context, int statusCode)
    {
        return new
        {
            status = statusCode,
            timestamp = DateTime.UtcNow.ToString("o"),
            unix = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            execMs = DateTime.UtcNow.Millisecond,
            req = Guid.NewGuid(),
            time = new
            {
                utc = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                local = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            },
            server = new
            {
                host = Environment.MachineName,
                os = Environment.OSVersion.VersionString
            },
            client = new
            {
                ip = context.Connection.RemoteIpAddress?.ToString(),
                ua = context.Request.Headers["User-Agent"].ToString()
            }
        };
    }

    private JsonSerializerOptions JsonOpts() => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private static int GetStatusCodeForExceptionType(ExceptionType exceptionType)
    {
        return exceptionType switch
        {
            ExceptionType.InvalidToken => (int)HttpStatusCode.BadRequest,
            ExceptionType.InvalidRefreshToken => (int)HttpStatusCode.BadRequest,
            ExceptionType.InvalidCredentials => (int)HttpStatusCode.BadRequest,
            ExceptionType.UserNotFound => (int)HttpStatusCode.NotFound,
            ExceptionType.NullCredentials => (int)HttpStatusCode.BadRequest,
            ExceptionType.InvalidRequest => (int)HttpStatusCode.BadRequest,
            ExceptionType.PasswordMismatch => (int)HttpStatusCode.BadRequest,
            ExceptionType.EmailAlreadyConfirmed => (int)HttpStatusCode.BadRequest,
            ExceptionType.EmailNotConfirmed => (int)HttpStatusCode.BadRequest,
            ExceptionType.EmailAlreadyExists => (int)HttpStatusCode.BadRequest,
            ExceptionType.CredentialsAlreadyExists => (int)HttpStatusCode.BadRequest,
            ExceptionType.NotFound => (int)HttpStatusCode.NotFound,
            ExceptionType.UnauthorizedAccess => (int)HttpStatusCode.Unauthorized,
            ExceptionType.Forbidden => (int)HttpStatusCode.Forbidden,
            ExceptionType.BadRequest => (int)HttpStatusCode.BadRequest,
            ExceptionType.Conflict => (int)HttpStatusCode.Conflict,
            ExceptionType.InternalServerError => (int)HttpStatusCode.InternalServerError,
            ExceptionType.ServiceUnavailable => (int)HttpStatusCode.ServiceUnavailable,
            ExceptionType.OperationFailed => (int)HttpStatusCode.BadRequest,
            ExceptionType.Validation => (int)HttpStatusCode.UnprocessableContent,
            ExceptionType.DatabaseError => (int)HttpStatusCode.InternalServerError,
            ExceptionType.Critical => (int)HttpStatusCode.InternalServerError,
            _ => (int)HttpStatusCode.InternalServerError,
        };
    }

    private static string GetLanguageFromRequest(HttpContext context)
    {
        var queryLang = context.Request.Query["lang"].ToString().ToLower();
        var headerLang = context.Request.Headers["Accept-Language"].ToString().Split(',')[0].ToLower();

        return (queryLang is "ru" or "az" or "en") ? queryLang
             : (headerLang is "ru" or "az" or "en") ? headerLang
             : "en";
    }
}