using King.Carrier.TicketsApplication.Integrations.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;

internal class RabbitMQSettingsSetup : IConfigureOptions<RabbitMQSettings>
{
    private readonly IConfiguration _configuration;

    public RabbitMQSettingsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(RabbitMQSettings options)
    {
        _configuration
            .GetSection(RabbitMQSettings.SectionName)
            .Bind(options);
    }
}
