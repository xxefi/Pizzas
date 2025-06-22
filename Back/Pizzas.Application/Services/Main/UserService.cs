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
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Auth;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using static BCrypt.Net.BCrypt;

namespace Pizzas.Application.Services.Main;

public class UserService(
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository,
    IRoleService roleService,
    IValidator<CreateUserDto> createUserValidator,
    IValidator<UpdateUserDto> updateUserValidator,
    IUnitOfWork unitOfWork)
    : IUserService
{
    private HttpContext context => httpContextAccessor.HttpContext!;

    private async Task<UserDto> CreateUserInternalAsync(CreateUserDto dto)
    {
        var existingUser = (await userRepository.FindAsync(
                u => u.Email == dto.Email || u.Username == dto.Username))
            .FirstOrDefault();

        if (existingUser is not null)
            throw new PizzasException(ExceptionType.Conflict, "UserAlreadyExists");

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            var user = mapper.Map<UserEntity>(dto);
            user.Email = dto.Email.ToLowerInvariant();
            user.Username = dto.Username.ToLowerInvariant();
            user.RoleId = dto.RoleId;
            user.Password = HashPassword(dto.Password);
            user.Verified = false;

            await userRepository.AddAsync(user);
            return mapper.Map<UserDto>(user);
        });
    }
    
    public async Task<PublicUserDto> GetCurrentUserAsync()
    {
        var userId = context.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);
        return mapper.Map<PublicUserDto>(user);
    }

    public async Task<UserDto> UpdateProfileAsync(UpdateUserDto updateDto)
    {
        var userId = context.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        mapper.Map(updateDto, user);

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await userRepository.UpdateAsync(new[] { user });
            
            return mapper.Map<UserDto>(user);
        });
    }
    
    public async Task<UserOrdersStatsDto> GetOrdersStatsAsync()
    {
        var userId = context.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);
        
        var stats = new UserOrdersStatsDto
        {
            TotalOrders = user.Orders.Count,
            TotalSpent = user.Orders.Sum(o => o.TotalAmount)
        };

        return stats;
    }

    public async Task<string> GetUserPasswordHashAsync(string userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        return user.Password;
    }

    public async Task UpdateUserPasswordAsync(string userId, string newPassword)
    {
        var user = await userRepository.GetByIdAsync(userId);
        user.Password = newPassword;
        await userRepository.UpdateAsync([user]);
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = (await userRepository.FindAsync(u => u.Email == email))
            .FirstOrDefault()
            .EnsureFound("UserNotFound");
        return mapper.Map<UserDto>(user);
    }

    public async Task<UserCredentialsDto?> GetUserCredentialsByIdAsync(string id)
    {
        var user = await userRepository.GetByIdAsync(id);

        return new UserCredentialsDto
        {
            Id = user.Id,
            Password = user.Password,
        };
    }

    public async Task<UserDto> RegisterUserAsync(RegisterDto registerDto)
    {
        var role = await roleService.GetRoleByNameAsync("Customer");

        var createUserDto = mapper.Map<CreateUserDto>(registerDto);
        createUserDto.RoleId = role.Id;

        await createUserValidator.ValidateAndThrowAsync(createUserDto);

        return await CreateUserInternalAsync(createUserDto);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        await createUserValidator.ValidateAndThrowAsync(createUserDto);

        return await CreateUserInternalAsync(createUserDto);
    }

    public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
    {
        var existingUser = await userRepository.GetByIdAsync(id);
        
        await updateUserValidator.ValidateAndThrowAsync(updateUserDto);
        
        return await unitOfWork.StartTransactionAsync(async () =>
        {
            mapper.Map(updateUserDto, existingUser);
            await userRepository.UpdateAsync(new[] { existingUser });
            
            return mapper.Map<UserDto>(existingUser);
        });
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await userRepository.DeleteAsync(userId);

            return true;
        });
    }

    public async Task<PaginatedResponse<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize)
    {
        var totalUsers = await userRepository.GetAllAsync();

        if (pageNumber <= 0 || pageSize <= 0)
            throw new PizzasException(ExceptionType.BadRequest, "PaginationError");

        int totalItems = totalUsers.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var pagedUsers = totalUsers
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var userDtos = mapper.Map<IEnumerable<UserDto>>(pagedUsers);
        
        return new PaginatedResponse<UserDto>
        {
            Data = userDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<UserDto> ConfirmUserEmailAsync(string otp)
    {
        var userId = context.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        user.Verified = true;
        await userRepository.UpdateAsync([user]);

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return mapper.Map<UserDto>(user);
    }
}