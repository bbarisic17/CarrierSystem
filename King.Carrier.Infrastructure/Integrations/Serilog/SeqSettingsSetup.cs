using King.Carrier.TicketsApplication.Integrations.Seq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace King.Carrier.TicketsInfrastructure.Integrations.Serilog;

public class SeqSettingsSetup : IConfigureOptions<SeqSettings>
{
    private readonly IConfiguration _configuration;

    public SeqSettingsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(SeqSettings options)
    {
        _configuration
            .GetSection(SeqSettings.SectionName)
            .Bind(options);
    }
}
