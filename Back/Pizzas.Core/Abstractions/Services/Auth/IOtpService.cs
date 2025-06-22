using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IOtpService
{
    Task<string> GenerateAndSaveOtpAsync(string sessionId);
    Task<bool> VerifyOtpAsync(string sessionId, int otp);
    Task SavePendingUserAsync(string sessionId, RegisterDto userDto);
    Task<RegisterDto?> GetPendingUserAsync(string sessionId);
    Task ClearOtpAndPendingUserAsync(string sessionId);
}