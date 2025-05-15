using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(string permissionName);
    Task<IEnumerable<PermissionDto>> GetUserPermissionsAsync();
    Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(string roleId);
    Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissions);
    Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissions);
    Task<bool> RoleHasPermissionAsync(string roleId, string permissionName);
    Task<IEnumerable<RoleDto>> GetRolesWithPermissionAsync(string permissionName);
    Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto);
    Task<PermissionDto> UpdatePermissionAsync(string id, UpdatePermissionDto dto);
    Task<bool> DeletePermissionAsync(string id);
}