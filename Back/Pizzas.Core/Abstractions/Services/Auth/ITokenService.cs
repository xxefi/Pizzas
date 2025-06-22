using Pizzas.Core.Dtos.Read;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface ITokenService
{
    string GenerateAccessToken(UserDto user);
    string GenerateRefreshToken();
    string GenerateRandomPassword(int length = 12);
}