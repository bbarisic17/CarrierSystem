using King.Carrier.TicketsApplication;
using King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;
using King.Carrier.TicketsInfrastructure;
using King.Carrier.TicketsInfrastructure.Integrations.Consul;
using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;
using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ.Tickets;

namespace King.Carrier.TicketsApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHostedService<ConsulRegistrationHostedService>();
            builder.Services.AddScoped<ITicketsPublisher, TicketPublisher>();
            builder.Services.AddScoped<RabbitMqSetupService>();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();
            await app.MigrateDatabase();

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
