using Newtonsoft.Json;
using Pizzas.Core.Abstractions.Services.Main;
using StackExchange.Redis;

namespace Pizzas.Application.Services.Main;

public class RedisCacheService : IRedisCacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ISubscriber _subscriber;
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _subscriber = _redis.GetSubscriber();
        _db = _redis.GetDatabase();
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var json = JsonConvert.SerializeObject(value);
        await _db.StringSetAsync(key, json, expiration);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var cachedData = await _db.StringGetAsync(key);
        return cachedData.HasValue ? JsonConvert.DeserializeObject<T>(cachedData) : default;
    }

    public async Task KeyDeleteAsync(string key)
        => await _db.KeyDeleteAsync(key);

    public async Task<bool> KeyExistsAsync(string key)
        => await _db.KeyExistsAsync(key);

    public async Task PublishAsync(string channel, string message)
        => await _subscriber.PublishAsync(channel, message);

    public async Task SubscribeAsync(string channel, Action<string> handler)
    {
        await _subscriber.SubscribeAsync(channel, (redisChannel, redisMessage) =>
        {
            handler(redisMessage);
        });
    }

    public ISubscriber GetSubscriber()
        => _subscriber;
}