using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Auth;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IUserService
{
    Task<PublicUserDto> GetCurrentUserAsync();
    Task<UserDto> UpdateProfileAsync(UpdateUserDto updateDto);
    Task<UserOrdersStatsDto> GetOrdersStatsAsync();
    Task<string> GetUserPasswordHashAsync(string userId);
    Task UpdateUserPasswordAsync(string userId, string newPassword);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserCredentialsDto?> GetUserCredentialsByIdAsync(string id);
    Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
    Task<bool> DeleteUserAsync(string userId);
    Task<PaginatedResponse<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize); 
    Task<UserDto> ConfirmUserEmailAsync(string otp);
    Task<UserDto?> GetUserByIdAsync(string id);
}