using Microsoft.AspNetCore.Http;

namespace Pizzas.Common.Extentions;

public static class CookieWorkExtensions
{
    private const string ACCESS_TOKEN_COOKIE_NAME = "atk";
    private const string REFRESH_TOKEN_COOKIE_NAME = "rtk";
    public static string? GetAccessToken(this HttpContext context)
        => context.Request.Cookies[ACCESS_TOKEN_COOKIE_NAME];
    public static string? GetRefreshToken(this HttpContext context)
        => context.Request.Cookies[REFRESH_TOKEN_COOKIE_NAME];

    public static void SetAuthCookies(this HttpContext context, string accessToken, string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };
        context.Response.Cookies.Append(ACCESS_TOKEN_COOKIE_NAME, accessToken, cookieOptions);
        context.Response.Cookies.Append(REFRESH_TOKEN_COOKIE_NAME, refreshToken, cookieOptions);
    }

    public static void DeleteAuthCookies(this HttpContext context)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };
        context.Response.Cookies.Delete(ACCESS_TOKEN_COOKIE_NAME, cookieOptions);
        context.Response.Cookies.Delete(REFRESH_TOKEN_COOKIE_NAME, cookieOptions);
    }
}