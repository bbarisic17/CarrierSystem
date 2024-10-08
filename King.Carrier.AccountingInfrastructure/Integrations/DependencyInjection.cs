using King.Carrier.AccountingInfrastructure.Integrations.Consul;
using King.Carrier.AccountingInfrastructure.Integrations.RabbitMQ;
using King.Carrier.AccountingInfrastructure.Integrations.Redis;
using King.Carrier.AccountingInfrastructure.Integrations.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace King.Carrier.AccountingInfrastructure.Integrations;

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
