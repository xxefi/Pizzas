using System.Security.Claims;

namespace Pizzas.Core.Constants;

public static class ClaimKeys
{
    public const string UserId = ClaimTypes.NameIdentifier;
    public const string Role = ClaimTypes.Role;
    public const string RoleId = "roleId";
}