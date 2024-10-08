using King.Carrier.AccountingApplication.Integrations.Seq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace King.Carrier.AccountingInfrastructure.Integrations.Serilog
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSerilogIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<SeqSettingsSetup>();
            var serviceProvider = services.BuildServiceProvider();
            var seqSettings = serviceProvider.GetRequiredService<IOptions<SeqSettings>>().Value;

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration) // Read from appsettings.json
                .Enrich.FromLogContext()
                .WriteTo.Console() // Write logs to console
                .WriteTo.Seq(seqSettings.Url) // Use Seq URL from settings
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders(); // Clear default providers
                loggingBuilder.AddSerilog(dispose: true); // Use Serilog
            });

            return services;
        }
    }
}
