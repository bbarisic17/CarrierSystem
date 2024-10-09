using Consul;
using King.Carrier.TicketsApplication.Integrations.Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace King.Carrier.TicketsInfrastructure.Integrations.Consul;

public static class DependencyInjection
{
    public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<ConsulSettingsSetup>();
        services.AddHostedService<ConsulRegistrationHostedService>();
        var serviceProvider = services.BuildServiceProvider();
        var consulSettings = serviceProvider.GetRequiredService<IOptions<ConsulSettings>>().Value;

        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            consulConfig.Address = new Uri(consulSettings.Url);
        }));

        return services;
    }
}
