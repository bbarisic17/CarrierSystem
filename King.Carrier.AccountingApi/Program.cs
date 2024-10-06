
using Consul;
using King.Carrier.AccountingInfrastructure.Consul;
using King.Carrier.AccountingInfrastructure.Integrations.TicketsApi.RabbitMq;
using Serilog;
using Serilog.Sinks.Seq;
using ZiggyCreatures.Caching.Fusion;
using King.Carrier.AccountingInfrastructure.Persistence;

namespace King.Carrier.AccountingApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration) // Read from appsettings.json
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://seq:5341")// Add any other sinks here
            );

            builder.Services.AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(2),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(1)
            });

            builder.Services.AddFusionCacheMemoryBackplane();
            builder.Services.AddFusionCacheStackExchangeRedisBackplane(options =>
            {
                options.Configuration = "redis:6379";  // Redis connection string
            });

            //napravit da se ovo automatski registrira
            //koristiti konfiguracije
            builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri("http://consul:8500");
            }));

            builder.Services.AddHostedService<ConsulRegistrationHostedService>();
            builder.Services.AddHostedService<TicketsApiConsumer>();

            builder.Services.AddPersistence(builder.Configuration);

            Log.Logger.Information("AccountingApi is starting...");

            var app = builder.Build();
            await app.MigrateDatabase();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
