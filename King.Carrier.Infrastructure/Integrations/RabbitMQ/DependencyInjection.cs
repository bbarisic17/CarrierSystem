using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMQIntegration(this IServiceCollection services)
    {
        services.ConfigureOptions<RabbitMQSettingsSetup>();

        return services;
    }
}
