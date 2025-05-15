using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Pizzas.Core.Abstractions.Services.Auth;

namespace Pizzas.Presentation.Middlewares;

public class TokenRefreshMiddleware
{
    private readonly RequestDelegate _next;
    
    public TokenRefreshMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(HttpContext context, IAuthService authService)
    {
        var accessToken = context.Request.Cookies["atk"];
        if (string.IsNullOrEmpty(accessToken) || !IsTokenExpired(accessToken))
        {
            await _next(context);
            return;
        }

        var refreshToken = context.Request.Cookies["rtk"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            context.Response.StatusCode = 401; 
            return;
        }

        try
        {
            var newTokens = await authService.RefreshTokenAsync();
            context.Response.Cookies.Append("atk", newTokens.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            context.Response.Cookies.Append("rtk", newTokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var error = new
            {
                status = 401,
                message = ex.Message
            };

            var json = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(json);
            return;
        }


        await _next(context);
    }
    private static bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
        return jwtToken?.ValidTo < DateTime.UtcNow && jwtToken?.ValidTo != null;
    }
}
