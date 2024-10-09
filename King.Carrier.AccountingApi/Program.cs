using Serilog;
using King.Carrier.AccountingInfrastructure.Persistence;
using King.Carrier.AccountingApplication;
using King.Carrier.AccountingInfrastructure.Integrations.RabbitMQ.TicketsApi;
using King.Carrier.AccountingInfrastructure;

namespace King.Carrier.AccountingApi
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

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            Log.Logger.Information("AccountingApi is starting...");

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
