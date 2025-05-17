using Pizzas.Core.Dtos.Create;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IOtpService
{
    Task<string> GenerateAndSaveOtpAsync(string sessionId);
    Task<bool> VerifyOtpAsync(string sessionId, string otp);
    Task SavePendingUserAsync(string sessionId, CreateUserDto userDto);
    Task<CreateUserDto?> GetPendingUserAsync(string sessionId);
    Task ClearOtpAndPendingUserAsync(string sessionId);
}