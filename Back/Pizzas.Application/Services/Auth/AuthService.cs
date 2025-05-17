using System.Security.Claims;
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
using static BCrypt.Net.BCrypt;
using static Pizzas.Core.Constants.ValidationConstants;
using static System.Text.RegularExpressions.Regex;


namespace Pizzas.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IUserRepository _userRepository;
    private readonly IBlackListedService _blackListedService;
    private readonly IUserActiveSessionsService _userActiveSessionsService;

    public AuthService(IUserService userService,
        IEmailService emailService,
        IOtpService otpService,
        ITokenService tokenService, IHttpContextAccessor httpContextAccessor,
        IValidator<CreateUserDto> createUserValidator,
        IUserRepository userRepository,
        IBlackListedService blackListedService, IUserActiveSessionsService userActiveSessionsService)
    {
        _userService = userService;
        _emailService = emailService;
        _otpService = otpService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _createUserValidator = createUserValidator;
        _userRepository = userRepository;
        _blackListedService = blackListedService;
        _userActiveSessionsService = userActiveSessionsService;
    }
    
    public HttpContext httpContext => _httpContextAccessor.HttpContext;
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        switch (loginDto)
        {
            case null:
            case { Email: null or "", Password: null or "" }:
                throw new PizzasException(ExceptionType.InvalidRequest, "EmailAndPasswordCannotBeEmpty");
        }

        switch (loginDto.Email)
        {
            case var email when !IsMatch(email, EmailRegex):
                throw new PizzasException(ExceptionType.InvalidRequest, "InvalidEmailFormat");
        }
        
        var user = await _userService.GetUserByEmailAsync(loginDto.Email)
            ?? throw new PizzasException(ExceptionType.InvalidRequest, "UserDoesNotExist");
        
        var userCredentials = await _userService.GetUserCredentialsByIdAsync(user.Id);
        
        switch (Verify(loginDto.Password, userCredentials.Password))
        {
            case false:
                throw new PizzasException(ExceptionType.InvalidRequest, "InvalidEmailOrPassword");
        }
        
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        var deviceInfo = httpContext.Request.Headers["User-Agent"].ToString();
        
        var createUserActiveSessionDto = new CreateUserActiveSessionDto
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshTokenExpiryTime,
            DeviceInfo = deviceInfo
        };
        
        await _userActiveSessionsService.AddUserActiveSessionAsync(createUserActiveSessionDto);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };
        
        httpContext.Response.Cookies.Append("atk", accessToken, cookieOptions);
        httpContext.Response.Cookies.Append("rtk", refreshToken, cookieOptions);


        return true;

    }

    public async Task<string> RegisterAsync(CreateUserDto userDto)
    {
        await _createUserValidator.ValidateAndThrowAsync(userDto);

        var existingUser = (await _userRepository.FindAsync(
                u => u.Email == userDto.Email || u.Username == userDto.Username))
            .FirstOrDefault();
        
        if (existingUser is not null)
            throw new PizzasException(ExceptionType.Conflict, "UserAlreadyExists");
            
        var sessionId = Guid.NewGuid().ToString();

        await _otpService.SavePendingUserAsync(sessionId, userDto);
        var otp = await _otpService.GenerateAndSaveOtpAsync(sessionId);

        await _emailService.SendOtpAsync(userDto.Email, otp);

        return sessionId; 
    }

    public async Task<UserDto> ConfirmOtpAsync(string sessionId, string otp)
    {
        if (string.IsNullOrWhiteSpace(sessionId) || string.IsNullOrWhiteSpace(otp))
            throw new PizzasException(ExceptionType.InvalidRequest, "SessionIdAndOtpRequired");
        
        var isOtpValid = await _otpService.VerifyOtpAsync(sessionId, otp);
        if (!isOtpValid)
            throw new PizzasException(ExceptionType.InvalidCredentials, "InvalidOrExpiredOtp");
        
        var pendingUser = await _otpService.GetPendingUserAsync(sessionId)
            ?? throw new PizzasException(ExceptionType.InvalidRequest, "PendingUserNotFound");
        
        var user = await _userService.CreateUserAsync(pendingUser);
        await _otpService.ClearOtpAndPendingUserAsync(sessionId);
        
        return user;
    }

    public async Task<AccessInfoDto> RefreshTokenAsync()
    {
        var userId = GetUserId();
        var accessToken = httpContext.Request.Cookies["atk"];
        var refreshToken = httpContext.Request.Cookies["rtk"];
        
        switch (accessToken, refreshToken)
        {
            case (null or "", _) or (_, null or ""):
                throw new PizzasException(ExceptionType.InvalidRefreshToken, "MissingTokens");
            
            case (_, _) when await _blackListedService.IsBlackListedAsync(new BlackListedDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }):
                throw new PizzasException(ExceptionType.InvalidRefreshToken, "TokenIsBlacklisted");
        }
        
        var user = await _userService.GetUserByIdAsync(userId);
        var userActiveSessions = await _userActiveSessionsService.GetUserActiveSessionAsync(userId);
        
        switch (userActiveSessions.Any())
        {
            case false:
                throw new PizzasException(ExceptionType.InvalidRequest, "NoActiveSessionsFound");
        }
        
        var userActiveSession = userActiveSessions.FirstOrDefault(session => session.RefreshToken == refreshToken);

        switch (userActiveSession)
        {
            case null:
                throw new PizzasException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");
            case { RefreshTokenExpiryTime: var expiryTime } when expiryTime < DateTime.UtcNow:
                throw new PizzasException(ExceptionType.InvalidRefreshToken, "RefreshTokenExpired");
        }
        
        var newAccessToken = _tokenService.GenerateAccessToken(user!);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        var deviceInfo = httpContext.Request.Headers.UserAgent.ToString();
        
        var updateUserActiveSessionDto = new UpdateUserActiveSessionDto
        {
            UserId = user.Id,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiryDate = refreshTokenExpiryTime,
            DeviceInfo = deviceInfo
        };
        var newBlackList = new CreateBlackListedDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
        };
        await _blackListedService.AddToBlackListAsync(newBlackList);
        await _userActiveSessionsService.UpdateUserActiveSessionAsync(userActiveSession.Id, updateUserActiveSessionDto);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };
        
        httpContext.Response.Cookies.Append("atk", newAccessToken, cookieOptions);
        httpContext.Response.Cookies.Append("rtk", newRefreshToken, cookieOptions);
        
        return new AccessInfoDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
        };

    }

    public async Task<bool> LogoutAsync()
    {
        var userId = GetUserId();
        var accessToken = httpContext.Request.Cookies["atk"];
        var refreshToken = httpContext.Request.Cookies["rtk"];
        var userActiveSessions =
            await _userActiveSessionsService.GetUserActiveSessionAsync(userId);
        var userActiveSession = userActiveSessions
            .FirstOrDefault(session => session.RefreshToken == refreshToken);
        var deviceInfo = httpContext.Request.Headers.UserAgent.ToString();

        switch (accessToken, refreshToken, userActiveSessions.Any(), userActiveSession)
        {
            case (null or "", _, _, _)
                or (_, null or "", _, _):
                throw new PizzasException(ExceptionType.InvalidRefreshToken, "MissingTokens");

            case (_, _, false, _):
                throw new PizzasException(ExceptionType.InvalidRefreshToken, "NoActiveSessionsFound");
            
            case (_, _, _, null):
                throw new PizzasException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");

            case (_, _, _, { RefreshToken: var token }):
                if (token != refreshToken)
                    throw new PizzasException(ExceptionType.InvalidRefreshToken, "InvalidRefreshToken");
                break;
        }
        
        var newBlackList = new CreateBlackListedDto
        {
            IpAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
            DeviceInfo = deviceInfo,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        await _blackListedService.AddToBlackListAsync(newBlackList);
        await _userActiveSessionsService.DeleteUserActiveSessionAsync(userActiveSession.Id);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };

        httpContext.Response.Cookies.Delete("atk", cookieOptions);
        httpContext.Response.Cookies.Delete("rtk", cookieOptions);

        return true;
    }
}