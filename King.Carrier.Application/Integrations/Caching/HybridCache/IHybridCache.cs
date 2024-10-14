namespace King.Carrier.TicketsApplication.Integrations.Caching.HybridCache;

public interface IHybridCache
{
    Task SetAsync<T>(string key, T value, TimeSpan expiration);
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}
