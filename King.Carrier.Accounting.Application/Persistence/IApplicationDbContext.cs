using King.Carrier.AccountingDomain.Entities.Receipts;
using Microsoft.EntityFrameworkCore;

namespace King.Carrier.AccountingApplication.Persistence;

public interface IApplicationDbContext : IDisposable
{
    DbSet<Receipt> Receipts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
