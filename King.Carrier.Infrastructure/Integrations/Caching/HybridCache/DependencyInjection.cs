using King.Carrier.TicketsApplication.Integrations.Caching.HybridCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace King.Carrier.TicketsInfrastructure.Integrations.Caching.HybridCache;

public static class DependencyInjection
{
    public static IServiceCollection AddHybridCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration!.GetConnectionString("Redis")!));

        services.AddScoped<IHybridCache, HybridCache>();

        return services;
    }
}
