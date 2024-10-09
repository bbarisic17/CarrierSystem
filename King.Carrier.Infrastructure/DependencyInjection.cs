using King.Carrier.TicketsInfrastructure.Integrations;
using King.Carrier.TicketsInfrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace King.Carrier.TicketsInfrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIntegrations(configuration)
            .AddPersistence(configuration);

        return services;
    }
}
