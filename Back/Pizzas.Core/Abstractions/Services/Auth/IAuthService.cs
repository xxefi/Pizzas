using Pizzas.Core.Dtos.Auth;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto loginDto);
    Task<string> RegisterAsync(CreateUserDto createUserDto);
    Task<UserDto> ConfirmOtpAsync(string sessionId, string otp);
    Task<AccessInfoDto> RefreshTokenAsync();
    Task<bool> LogoutAsync();
}