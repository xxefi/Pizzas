using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Auth;
using static BCrypt.Net.BCrypt;

namespace Pizzas.Application.Services.Auth;

public class AccountService : IAccountService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(IUserService userService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    
    public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var userId = GetUserId();
        var currentPassword = await _userService.GetUserPasswordHashAsync(userId);

        if (!Verify(changePasswordDto.CurrentPassword, currentPassword))
            throw new PizzasException(ExceptionType.InvalidRequest, "OldPasswordIncorrect");
        
        var newPassword = HashPassword(changePasswordDto.NewPassword);
        await _userService.UpdateUserPasswordAsync(userId, newPassword); 

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userService.GetUserByEmailAsync(resetPasswordDto.Email);

        var newPassword = _tokenService.GenerateRandomPassword();
        var hashedPassword = HashPassword(newPassword);
        
        //var updateUserDto = new UpdateUserDto { Password = hashedPassword };
       
        //await _userService.UpdateUserAsync(user.Id, updateUserDto);

        return true;
    }
    
}