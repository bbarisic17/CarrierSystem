using King.Carrier.TicketsApplication.Integrations.Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace King.Carrier.TicketsInfrastructure.Integrations.Consul;

public class ConsulSettingsSetup : IConfigureOptions<ConsulSettings>
{
    private readonly IConfiguration _configuration;

    public ConsulSettingsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(ConsulSettings options)
    {
        _configuration
            .GetSection(ConsulSettings.SectionName)
            .Bind(options);
    }
}
