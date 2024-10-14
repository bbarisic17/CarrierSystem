using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;
using King.Carrier.TicketsInfrastructure.Integrations.Consul;
using King.Carrier.TicketsInfrastructure.Integrations.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using King.Carrier.TicketsInfrastructure.Integrations.Caching.HybridCache;

namespace King.Carrier.TicketsInfrastructure.Integrations;

public static class DependencyInjection
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSerilogIntegration(configuration)
            .AddRabbitMQIntegration()
            .AddConsul(configuration)
            .AddHybridCache(configuration);

        return services;
    }
}
