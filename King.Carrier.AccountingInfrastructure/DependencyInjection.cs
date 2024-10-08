using King.Carrier.AccountingInfrastructure.Integrations;
using King.Carrier.AccountingInfrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace King.Carrier.AccountingInfrastructure;

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
