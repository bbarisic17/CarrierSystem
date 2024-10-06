using King.Carrier.TicketsDomain.Entities.Tickets;
using Microsoft.EntityFrameworkCore;

namespace King.Carrier.TicketsApplication.Persistence;

public interface IApplicationDbContext : IDisposable
{
    DbSet<Ticket> Tickets { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
