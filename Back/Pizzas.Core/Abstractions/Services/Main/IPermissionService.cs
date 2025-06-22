using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IPermissionService
{
    Task<PermissionDto> CreateAsync(CreatePermissionDto dto);
    Task<PermissionDto> UpdateAsync(string id, UpdatePermissionDto dto);
    Task<bool> DeleteAsync(string id);
    Task<PermissionDto> GetByIdAsync(string id);
    Task<IEnumerable<PermissionDto>> GetAllAsync();
    Task<PermissionDto> GetByNameAsync(string name);
    Task<IEnumerable<PermissionDto>> GetByRoleIdAsync(string roleId);
    Task EnsurePermissionAsync(string permissionName);
}