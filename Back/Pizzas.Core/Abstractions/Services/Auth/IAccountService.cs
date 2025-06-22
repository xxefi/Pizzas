using Pizzas.Core.Dtos.Auth;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IAccountService
{
    Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}