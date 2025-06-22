using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Antiforgery;

namespace Pizzas.Presentation.Middlewares;

public class CsrfMiddleware
{
    private readonly RequestDelegate _next;

    public CsrfMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
        var tokens = antiforgery.GetAndStoreTokens(context);

        context.Response.Cookies.Append("csrf", tokens.RequestToken!,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

        if (HttpMethods.IsPost(context.Request.Method))
        {
            if (!context.Request.Cookies.TryGetValue("csrf", out var csrfToken) || string.IsNullOrEmpty(csrfToken))
            {
                context.Response.StatusCode = StatusCodes.Status418ImATeapot;
                context.Response.ContentType = "application/json; charset=utf-8";
                var errorResponse = new 
                    { message = "csrf token is missing. Can you resend the request?" };

                var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                await context.Response.WriteAsync(jsonResponse);
                return;
            }
        }

        await _next(context);
    }
}