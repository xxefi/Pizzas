using Pizzas.Core.Dtos.Auth;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto loginDto);
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<UserDto> ConfirmOtpAsync(string sessionId, int otp);
    Task<AccessInfoDto> RefreshTokenAsync();
    Task<bool> LogoutAsync();
}