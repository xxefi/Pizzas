namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IEmailService
{
    Task SendOtpAsync(string email, string otpCode);
}