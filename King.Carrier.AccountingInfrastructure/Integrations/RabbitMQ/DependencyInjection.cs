using King.Carrier.AccountingInfrastructure.Integrations.RabbitMQ.TicketsApi;
using Microsoft.Extensions.DependencyInjection;

namespace King.Carrier.AccountingInfrastructure.Integrations.RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMQIntegration(this IServiceCollection services)
    {
        services.ConfigureOptions<RabbitMQSettingsSetup>();
        services.AddHostedService<TicketsApiConsumer>();

        return services;
    }
}
