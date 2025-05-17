using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Auth;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using static BCrypt.Net.BCrypt;

namespace Pizzas.Application.Services.Main;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly IOtpService _iotpService;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<UpdateUserDto> _updateUserValidator;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository,
        IRoleService roleService,
        IOtpService iotpService,
        IValidator<CreateUserDto> createUserValidator, IValidator<UpdateUserDto> updateUserValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _roleService = roleService;
        _iotpService = iotpService;
        _createUserValidator = createUserValidator;
        _updateUserValidator = updateUserValidator;
        _unitOfWork = unitOfWork;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    
    public async Task<UserDto> GetCurrentUserAsync()
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateProfileAsync(UpdateUserDto updateDto)
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        _mapper.Map(updateDto, user);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _userRepository.UpdateAsync(new[] { user });
            
            return _mapper.Map<UserDto>(user);
        });
    }
    
    public async Task<UserOrdersStatsDto> GetOrdersStatsAsync()
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        
        var stats = new UserOrdersStatsDto
        {
            TotalOrders = user.Orders.Count,
            TotalSpent = user.Orders.Sum(o => o.TotalAmount)
        };

        return stats;
    }

    public async Task<string> GetUserPasswordHashAsync(string userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user.Password;
    }

    public async Task UpdateUserPasswordAsync(string userId, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.Password = newPassword;
        await _userRepository.UpdateAsync(new[]{user});
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = (await _userRepository.FindAsync(u => u.Email == email))
            .FirstOrDefault();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserCredentialsDto?> GetUserCredentialsByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return new UserCredentialsDto
        {
            Id = user.Id,
            Password = user.Password,
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var role = await _roleService.GetRoleByNameAsync("Customer");
        await _createUserValidator.ValidateAndThrowAsync(createUserDto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var user = _mapper.Map<UserEntity>(createUserDto);
            user.Email = createUserDto.Email.ToLowerInvariant();
            user.Username = createUserDto.Username.ToLowerInvariant();
            user.RoleId = role.Id;
            user.Password = HashPassword(createUserDto.Password);
            user.Verified = false;
            
            await _userRepository.AddAsync(user);
            
            return _mapper.Map<UserDto>(user);
        });
    }

    public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        
        await _updateUserValidator.ValidateAndThrowAsync(updateUserDto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(updateUserDto, existingUser);
            await _userRepository.UpdateAsync(new[] { existingUser });
            
            return _mapper.Map<UserDto>(existingUser);
        });
    }

    public async Task<UserDto> ConfirmUserEmailAsync(string otp)
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        user.Verified = true;
        await _userRepository.UpdateAsync([user]);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }
}