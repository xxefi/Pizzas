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

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IValidator<CreateRoleDto> _createRoleValidator;
    private readonly IValidator<UpdateRoleDto> _updateRoleValidator;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IMapper mapper, IHttpContextAccessor httpContextAccessor, 
        IRoleRepository roleRepository, IUserRepository userRepository, 
        IPermissionRepository permissionRepository, IValidator<CreateRoleDto> createRoleValidator,
        IValidator<UpdateRoleDto> updateRoleValidator, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
        _createRoleValidator = createRoleValidator;
        _updateRoleValidator = updateRoleValidator;
        _unitOfWork = unitOfWork;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<RoleDto?> GetRoleByNameAsync(string name)
    {
        var role = (await _roleRepository.FindAsync(r => r.Name == name))
            .FirstOrDefault()
            .EnsureFound("RoleNotFound");
        
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> GetCurrentUserRoleAsync()
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        return _mapper.Map<RoleDto>(user.Role);
    }

    public async Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(string roleId)
    {
        var permissions = await _permissionRepository
            .FindAsync(p => p.Roles.Any(r => r.Id == roleId))
            ?? throw new PizzasException(ExceptionType.NotFound, "RoleNotFound");
            
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<bool> UserHasRoleAsync(string roleName)
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        return user.Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase);
    }

    public async Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleId)
    {
        var users = await _userRepository.FindAsync(u => u.RoleId == roleId);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<bool> RoleHasPermissionAsync(string roleId, string permissionName)
    {
        var permissions = await _permissionRepository
            .FindAsync(p => p.Roles.Any(r => r.Id == roleId) && p.Name == permissionName);

        return permissions.Any();
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto roleDto)
    {
        await _createRoleValidator.ValidateAndThrowAsync(roleDto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var role = _mapper.Map<RoleEntity>(roleDto);
            await _roleRepository.AddAsync(role);

            return _mapper.Map<RoleDto>(role);
        });
    }

    public async Task<RoleDto> UpdateAsync(string id, UpdateRoleDto roleDto)
    {
        var existingRole = await _roleRepository.GetByIdAsync(id);
        
        await _updateRoleValidator.ValidateAndThrowAsync(roleDto);
        
        _mapper.Map(roleDto, existingRole);
        await _roleRepository.UpdateAsync(new[] { existingRole });
        await _unitOfWork.SaveChangesAsync();
       
        return _mapper.Map<RoleDto>(existingRole);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _roleRepository.DeleteAsync(id);

            return true;
        });
    }
}