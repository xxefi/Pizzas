using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using static Pizzas.Core.Constants.ClaimKeys;

namespace Pizzas.Common.Extentions;

public static class HttpContextExtensions
{
    private static string GetClaimValue(this HttpContext httpContext, string claimType)
        =>  string.IsNullOrWhiteSpace(httpContext.User.FindFirst(claimType)?.Value)
            ? throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized")
            : httpContext.User.FindFirst(claimType)!.Value;
    public static string GetUserId(this HttpContext httpContext)
        => httpContext.GetClaimValue(UserId);
    public static string GetUserRole(this HttpContext httpContext)
        => httpContext.GetClaimValue(Role);
    public static string GetRoleId(this HttpContext httpContext)
        => httpContext.GetClaimValue(RoleId);
}