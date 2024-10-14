using King.Carrier.TicketsApplication.Integrations.Caching.HybridCache;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace King.Carrier.TicketsInfrastructure.Integrations.Caching.HybridCache;

public class HybridCache : IHybridCache
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDatabase _redisDb;

    public HybridCache(IMemoryCache memoryCache, IConnectionMultiplexer redis)
    {
        _memoryCache = memoryCache;
        _redisDb = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        if (_memoryCache.TryGetValue(key, out T value))
        {
            return value;
        }

        var redisValue = await _redisDb.StringGetAsync(key);
        if (redisValue.IsNull)
        {
            return default;
        }

        value = System.Text.Json.JsonSerializer.Deserialize<T>(redisValue!)!;
        _memoryCache.Set(key, value);

        return value;
    }

    public async Task RemoveAsync(string key)
    {
        _memoryCache.Remove(key);
        await _redisDb.KeyDeleteAsync(key);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        _memoryCache.Set(key, value, expiration);

        await _redisDb.StringSetAsync(key, System.Text.Json.JsonSerializer.Serialize(value), expiration);
    }
}
