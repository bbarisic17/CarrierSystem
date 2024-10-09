using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;

namespace King.Carrier.TicketsInfrastructure.Integrations.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(2),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(1)
            });

        services.AddFusionCacheMemoryBackplane();
        services.AddFusionCacheStackExchangeRedisBackplane(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });
        return services;
    }
}
