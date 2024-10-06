using King.Carrier.TicketsInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace King.Carrier.TicketsApi;

public static class MigrationExecutor
{
    public static async Task<IHost> MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            throw;
        }

        return host;
    }
}
