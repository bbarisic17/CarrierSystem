using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;
using King.Carrier.TicketsInfrastructure.Integrations.Consul;
using King.Carrier.TicketsInfrastructure.Integrations.Redis;
using King.Carrier.TicketsInfrastructure.Integrations.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace King.Carrier.TicketsInfrastructure.Integrations;

public static class DependencyInjection
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSerilogIntegration(configuration)
            .AddRabbitMQIntegration()
            .AddRedis(configuration)
            .AddConsul(configuration);

        return services;
    }
}
