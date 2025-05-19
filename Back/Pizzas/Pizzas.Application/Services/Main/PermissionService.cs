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

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IValidator<CreatePermissionDto> _createPermissionValidator;
    private readonly IValidator<UpdatePermissionDto> _updatePermissionValidator;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IValidator<CreatePermissionDto> createPermissionValidator,
        IValidator<UpdatePermissionDto> updatePermissionValidator,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _createPermissionValidator = createPermissionValidator;
        _updatePermissionValidator = updatePermissionValidator;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    private async Task<ICollection<PermissionEntity>> GetPermissionsForRoleAsync(string roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        return role.Permissions;
    }

    public async Task<bool> HasPermissionAsync(string permissionName)
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        var role = user.Role;

        return role.Permissions.Any(p => p.Name == permissionName);
    }

    public async Task<IEnumerable<PermissionDto>> GetUserPermissionsAsync()
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        var role = user.Role;

        return _mapper.Map<IEnumerable<PermissionDto>>(role.Permissions);
    }

    public async Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(string roleId)
    {
        var permissions = await GetPermissionsForRoleAsync(roleId);
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissions)
    {
        var userPermissionNames = new HashSet<string>(
            (await GetUserPermissionsAsync()).Select(p => p.Name)
        );

        return permissions.All(p => userPermissionNames.Contains(p));
    }

    public async Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissions)
    {
        var userPermissionNames = new HashSet<string>(
            (await GetUserPermissionsAsync()).Select(p => p.Name)
        );

        return permissions.Any(p => userPermissionNames.Contains(p));
    }

    public async Task<bool> RoleHasPermissionAsync(string roleId, string permissionName)
    {
        var permissions = await GetPermissionsForRoleAsync(roleId);
        return permissions.Any(p => p.Name == permissionName);
    }

    public async Task<IEnumerable<RoleDto>> GetRolesWithPermissionAsync(string permissionName)
    {
        var roles = await _roleRepository.FindAsync(r => r.Permissions.Any(p => p.Name == permissionName));
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto)
    {
        await _createPermissionValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var permissionEntity = _mapper.Map<PermissionEntity>(dto);

            await _permissionRepository.AddAsync(permissionEntity);

            return _mapper.Map<PermissionDto>(permissionEntity);
        });
    }

    public async Task<PermissionDto> UpdatePermissionAsync(string id, UpdatePermissionDto dto)
    {
        var existingPermission = await _permissionRepository.GetByIdAsync(id);
        await _updatePermissionValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingPermission);
            await _permissionRepository.UpdateAsync(new[] { existingPermission });

            return _mapper.Map<PermissionDto>(existingPermission);
        });
    }

    public async Task<bool> DeletePermissionAsync(string id)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _permissionRepository.DeleteAsync(id);
            return true;
        });
    }
}