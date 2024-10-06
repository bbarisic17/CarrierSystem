using King.Carrier.TicketsApplication.Persistence;
using King.Carrier.TicketsDomain.Entities.Tickets;
using Microsoft.EntityFrameworkCore;

namespace King.Carrier.TicketsInfrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Ticket> Tickets { get; set; }
}
