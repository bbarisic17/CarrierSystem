using King.Carrier.AccountingApplication.Integrations.Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace King.Carrier.AccountingInfrastructure.Integrations.Consul;

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
