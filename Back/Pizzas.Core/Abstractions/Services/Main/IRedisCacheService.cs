using StackExchange.Redis;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IRedisCacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan expiration);
    Task<T?> GetAsync<T>(string key);
    Task KeyDeleteAsync(string key);
    Task<bool> KeyExistsAsync(string key);
    Task PublishAsync(string channel, string message);
    Task SubscribeAsync(string channel, Action<string> handler);
    ISubscriber GetSubscriber();
}