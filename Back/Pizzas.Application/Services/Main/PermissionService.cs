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
    private readonly IMapper _mapper;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IValidator<CreatePermissionDto> _createPermissionValidator;
    private readonly IValidator<UpdatePermissionDto> _updatePermissionValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private HttpContext context => _httpContextAccessor.HttpContext!;

    public PermissionService(
        IMapper mapper,
        IPermissionRepository permissionRepository,
        IValidator<CreatePermissionDto> createPermissionValidator,
        IValidator<UpdatePermissionDto> updatePermissionValidator,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _permissionRepository = permissionRepository;
        _createPermissionValidator = createPermissionValidator;
        _updatePermissionValidator = updatePermissionValidator;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task EnsurePermissionAsync(string permissionName)
    {
        var roleId = context.GetRoleId();
        var permissions = await _permissionRepository.FindAsync(p =>
            p.Roles.Any(r => r.Id == roleId));

        _ = permissions.Any(p => p.Name == permissionName)
            ? true
            : throw new PizzasException(ExceptionType.UnauthorizedAccess, $"permission_denied: {permissionName}");
    }

    public async Task<PermissionDto> CreateAsync(CreatePermissionDto dto)
    {
        await EnsurePermissionAsync("permission_create");
        await _createPermissionValidator.ValidateAndThrowAsync(dto);

        if (await _permissionRepository.AnyAsync(p => p.Name == dto.Name))
            throw new PizzasException(ExceptionType.Conflict, "PermissionAlreadyExists");
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var permissionEntity = _mapper.Map<PermissionEntity>(dto);
            await _permissionRepository.AddAsync(permissionEntity);
            return _mapper.Map<PermissionDto>(permissionEntity);
        });
    }

    public async Task<PermissionDto> UpdateAsync(string id, UpdatePermissionDto dto)
    {
        await EnsurePermissionAsync("permission_update");

        var existingPermission = await _permissionRepository.GetByIdAsync(id);

        await _updatePermissionValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingPermission);
            await _permissionRepository.UpdateAsync([existingPermission]);
            return _mapper.Map<PermissionDto>(existingPermission);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await EnsurePermissionAsync("permission_delete");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _permissionRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<PermissionDto> GetByIdAsync(string id)
    {
        await EnsurePermissionAsync("permission_view");

        var permission = await _permissionRepository.GetByIdAsync(id);

        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task<IEnumerable<PermissionDto>> GetAllAsync()
    {
        await EnsurePermissionAsync("permission_view");

        var permissions = await _permissionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto> GetByNameAsync(string name)
    {
        await EnsurePermissionAsync("permission_view");

        var permission = (await _permissionRepository.FindAsync(p => p.Name == name))
            .FirstOrDefault()
            .EnsureFound("PermissionNotFound");

        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task<IEnumerable<PermissionDto>> GetByRoleIdAsync(string roleId)
    {
        await EnsurePermissionAsync("permission_view");

        var permissions = await _permissionRepository.FindAsync(p => p.Roles.Any(r => r.Id == roleId));
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }
}