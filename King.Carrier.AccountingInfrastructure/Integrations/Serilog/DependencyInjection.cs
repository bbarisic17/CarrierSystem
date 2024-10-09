using King.Carrier.AccountingApplication.Integrations.Seq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace King.Carrier.AccountingInfrastructure.Integrations.Serilog;

public static class DependencyInjection
{
    public static IServiceCollection AddSerilogIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<SeqSettingsSetup>();
        var serviceProvider = services.BuildServiceProvider();
        var seqSettings = serviceProvider.GetRequiredService<IOptions<SeqSettings>>().Value;

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(seqSettings.Url)
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(dispose: true);
        });

        return services;
    }
}
