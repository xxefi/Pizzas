using System.Text.RegularExpressions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Auth;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Core.Constants.ValidationConstants;
using static BCrypt.Net.BCrypt;
using static NanoidDotNet.Nanoid;

namespace Pizzas.Application.Services.Auth;

public class AuthService(
    IMapper mapper,
    IUserService userService,
    IEmailService emailService,
    IOtpService otpService,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<CreateUserDto> createUserValidator,
    IUserRepository userRepository,
    IBlackListedService blackListedService,
    IUserActiveSessionsService userActiveSessionsService)
    : IAuthService
{
    private const int RefreshTokenLifespanDays = 7;
    private HttpContext httpContext => httpContextAccessor.HttpContext;
    private HttpRequest httpRequest => httpContext.Request;

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        if (loginDto is null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            throw new PizzasException(ExceptionType.InvalidRequest, "EmailAndPasswordCannotBeEmpty");

        if (!Regex.IsMatch(loginDto.Email, EmailRegex))
            throw new PizzasException(ExceptionType.InvalidRequest, "InvalidEmailFormat");

        var user = await userService.GetUserByEmailAsync(loginDto.Email)
                   ?? throw new PizzasException(ExceptionType.InvalidRequest, "UserDoesNotExist");

        var credentials = await userService.GetUserCredentialsByIdAsync(user.Id);

        if (!Verify(loginDto.Password, credentials.Password))
            throw new PizzasException(ExceptionType.InvalidRequest, "InvalidEmailOrPassword");

        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(RefreshTokenLifespanDays);

        await userActiveSessionsService.AddUserActiveSessionAsync(new CreateUserActiveSessionDto
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshExpiry,
            DeviceInfo = httpRequest.GetDeviceInfo()
        });

        httpContext.SetAuthCookies(accessToken, refreshToken);

        return true;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        await DefaultValidatorExtensions.ValidateAndThrowAsync(createUserValidator, mapper.Map<CreateUserDto>(registerDto));

        var existingUser = (await userRepository.FindAsync(u => u.Email == registerDto.Email))
            .FirstOrDefault();

        if (existingUser is not null)
            throw new PizzasException(ExceptionType.Conflict, "UserAlreadyExists");

        var sessionId = Generate(size: 64);

        await otpService.SavePendingUserAsync(sessionId, registerDto);
        var otp = await otpService.GenerateAndSaveOtpAsync(sessionId);

        await emailService.SendOtpAsync(registerDto.Email, otp);

        return sessionId;
    }

    public async Task<UserDto> ConfirmOtpAsync(string sessionId, int otp)
    {
        if (string.IsNullOrWhiteSpace(sessionId) || int.IsNegative(otp))
            throw new PizzasException(ExceptionType.InvalidRequest, "CredentialsRequired");

        var isOtpValid = await otpService.VerifyOtpAsync(sessionId, otp);
        if (!isOtpValid)
            throw new PizzasException(ExceptionType.InvalidCredentials, "InvalidOrExpiredOtp");

        var pendingUser = await otpService.GetPendingUserAsync(sessionId)
            ?? throw new PizzasException(ExceptionType.InvalidRequest, "PendingUserNotFound");

        var user = await userService.RegisterUserAsync(pendingUser);
        await otpService.ClearOtpAndPendingUserAsync(sessionId);

        return user;
    }

    public async Task<AccessInfoDto> RefreshTokenAsync()
    {
        var userId = httpContext.GetUserId();

        var atk = httpContext.GetAccessToken();
        var rtk = httpContext.GetRefreshToken();

        if (string.IsNullOrWhiteSpace(atk) || string.IsNullOrWhiteSpace(rtk))
            throw new PizzasException(ExceptionType.InvalidRefreshToken, "MissingTokens");

        var isBlackListed = await blackListedService.IsBlackListedAsync(new BlackListedDto
        {
            AccessToken = atk,
            RefreshToken = rtk
        });

        if (isBlackListed)
            throw new PizzasException(ExceptionType.InvalidRefreshToken, "TokenIsBlacklisted");

        var user = await userService.GetUserByIdAsync(userId);
        var sessions = await userActiveSessionsService.GetUserActiveSessionAsync(userId);
        if (!sessions.Any())
            throw new PizzasException(ExceptionType.InvalidRequest, "NoActiveSessionsFound");

        var session = sessions.FirstOrDefault(x => x.RefreshToken == rtk)
            ?? throw new PizzasException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");

        if (session.RefreshTokenExpiryTime < DateTime.UtcNow)
            throw new PizzasException(ExceptionType.InvalidRefreshToken, "RefreshTokenExpired");

        var newAtk = tokenService.GenerateAccessToken(user);
        var newRtk = tokenService.GenerateRefreshToken();
        var newExpiry = DateTime.UtcNow.AddDays(RefreshTokenLifespanDays);

        await blackListedService.AddToBlackListAsync(new CreateBlackListedDto
        {
            AccessToken = atk,
            RefreshToken = rtk,
            IpAddress = httpContext.GetIpAddress()
        });

        await userActiveSessionsService.UpdateUserActiveSessionAsync(session.Id, new UpdateUserActiveSessionDto
        {
            UserId = user.Id,
            AccessToken = newAtk,
            RefreshToken = newRtk,
            RefreshTokenExpiryDate = newExpiry,
            DeviceInfo = httpRequest.GetDeviceInfo()
        });

        httpContext.SetAuthCookies(newAtk, newRtk);

        return new AccessInfoDto
        {
            AccessToken = newAtk,
            RefreshToken = newRtk
        };
    }

    public async Task<bool> LogoutAsync()
    {
        var userId = httpContext.GetUserId();

        var atk = httpContext.GetAccessToken();
        var rtk = httpContext.GetRefreshToken();

        if (string.IsNullOrWhiteSpace(atk) || string.IsNullOrWhiteSpace(rtk))
            throw new PizzasException(ExceptionType.InvalidRefreshToken, "MissingTokens");

        var sessions = await userActiveSessionsService.GetUserActiveSessionAsync(userId);
        if (!sessions.Any())
            throw new PizzasException(ExceptionType.InvalidRefreshToken, "NoActiveSessionsFound");

        var session = sessions.FirstOrDefault(x => x.RefreshToken == rtk)
                      ?? throw new PizzasException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");

        await blackListedService.AddToBlackListAsync(new CreateBlackListedDto
        {
            IpAddress = httpContext.GetIpAddress(),
            DeviceInfo = httpRequest.GetDeviceInfo(),
            AccessToken = atk,
            RefreshToken = rtk
        });

        await userActiveSessionsService.DeleteUserActiveSessionAsync(session.Id);

        httpContext.DeleteAuthCookies();

        return true;
    }
}
