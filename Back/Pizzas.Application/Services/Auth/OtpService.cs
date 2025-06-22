using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.Application.Services.Auth;

public class OtpService : IOtpService
{
    private readonly IRedisCacheService _redisCache;

    private readonly TimeSpan _otpLifetime = TimeSpan.FromMinutes(5);
    private readonly TimeSpan _otpResendInterval = TimeSpan.FromSeconds(60);

    public OtpService(IRedisCacheService redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<string> GenerateAndSaveOtpAsync(string sessionId)
    {
        var otpTimestampKey = $"otpTimestamp:{sessionId}";
        
        var lastSentTicksStr = await _redisCache.GetAsync<string?>(otpTimestampKey);
        if (!string.IsNullOrEmpty(lastSentTicksStr) && long.TryParse(lastSentTicksStr, out var lastSentTicks))
        {
            var lastSentTime = DateTimeOffset.FromUnixTimeMilliseconds(lastSentTicks);
            if (DateTimeOffset.UtcNow - lastSentTime < _otpResendInterval)
                throw new PizzasException(ExceptionType.OperationFailed, "PleaseWaitBeforeRequesting");
        }
        
        var otp = new Random().Next(000001, 999999).ToString();
        await _redisCache.SetAsync($"otp:{sessionId}", otp, _otpLifetime);
        
        var nowMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        await _redisCache.SetAsync(otpTimestampKey, nowMs, _otpLifetime);
        return otp;
    }

    public async Task<bool> VerifyOtpAsync(string sessionId, int otp)
    {
        var storedOtp = await _redisCache.GetAsync<int>($"otp:{sessionId}");
        return storedOtp == otp;
    }
    public async Task SavePendingUserAsync(string sessionId, RegisterDto userDto)
        => await _redisCache.SetAsync($"pendingUser:{sessionId}", userDto, TimeSpan.FromMinutes(5));

    public async Task<RegisterDto?> GetPendingUserAsync(string sessionId)
        => await _redisCache.GetAsync<RegisterDto>($"pendingUser:{sessionId}");

    public async Task ClearOtpAndPendingUserAsync(string sessionId)
    {
        await _redisCache.KeyDeleteAsync($"otp:{sessionId}");
        await _redisCache.KeyDeleteAsync($"pendingUser:{sessionId}");
    }
}