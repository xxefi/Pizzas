using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Services.Main;

public class RoleService(
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    IPermissionRepository permissionRepository,
    IPermissionService permissionService,
    IValidator<CreateRoleDto> createRoleValidator,
    IValidator<UpdateRoleDto> updateRoleValidator,
    IUnitOfWork unitOfWork)
    : IRoleService
{
    private HttpContext context => httpContextAccessor.HttpContext;

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        await permissionService.EnsurePermissionAsync("role_view");
        var roles = await roleRepository.GetAllAsync();
        return mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<RoleDto> GetRoleByNameAsync(string name)
    {
        //await permissionService.EnsurePermissionAsync("role_view");
        var role = (await roleRepository.FindAsync(r => r.Name == name))
            .FirstOrDefault()
            .EnsureFound("RoleNotFound");

        return mapper.Map<RoleDto>(role);
    }
    public async Task<RoleDto> GetCurrentUserRoleAsync()
    {
        var userId = context.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        return mapper.Map<RoleDto>(user.Role);
    }

    public async Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(string roleId)
    {
        await permissionService.EnsurePermissionAsync("role_permission_view");
        var permissions = await permissionRepository
            .FindAsync(p => p.Roles.Any(r => r.Id == roleId));

        return mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }


    public async Task<bool> UserHasRoleAsync(string roleName)
    {
        await permissionService.EnsurePermissionAsync("role_check");
        var userId = context.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        return user.Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase);
    }


    public async Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleId)
    {
        await permissionService.EnsurePermissionAsync("role_user_list");
        var users = await userRepository.FindAsync(u => u.RoleId == roleId);
        return mapper.Map<IEnumerable<UserDto>>(users);
    }


    public async Task<bool> RoleHasPermissionAsync(string roleId, string permissionName)
    {
        await permissionService.EnsurePermissionAsync("role_permission_check");
        return await permissionRepository.AnyAsync(p =>
            p.Roles.Any(r => r.Id == roleId)
            && p.Name == permissionName);
    }



    public async Task<RoleDto> AddPermissionToRoleAsync(string roleId, List<string> permissionNames)
    {
        await permissionService.EnsurePermissionAsync("role_permission_add");
        var role = await roleRepository.GetByIdAsync(roleId);
        var permissions = await permissionRepository
            .FindAsync(p => permissionNames.Contains(p.Name));
        
        if (!permissions.Any())
            throw new PizzasException(ExceptionType.NotFound, "PermissionsNotFound");
        
        foreach (var permission in permissions)
            if (!role.Permissions.Any(p => p.Id == permission.Id))
                role.Permissions.Add(permission);
        
        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await roleRepository.UpdateAsync([role]);
            return mapper.Map<RoleDto>(role);
        });
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto roleDto)
    {
        await permissionService.EnsurePermissionAsync("role_create");
        await createRoleValidator.ValidateAndThrowAsync(roleDto);

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            var role = mapper.Map<RoleEntity>(roleDto);
            await roleRepository.AddAsync(role);
            return mapper.Map<RoleDto>(role);
        });
    }


    public async Task<RoleDto> UpdateAsync(string id, UpdateRoleDto roleDto)
    {
        await permissionService.EnsurePermissionAsync("role_update");
        var existingRole = await roleRepository.GetByIdAsync(id);

        await updateRoleValidator.ValidateAndThrowAsync(roleDto);
        mapper.Map(roleDto, existingRole);

        await roleRepository.UpdateAsync(new[] { existingRole });
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<RoleDto>(existingRole);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await permissionService.EnsurePermissionAsync("role_delete");
        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await roleRepository.DeleteAsync(id);
            return true;
        });
    }
}