using King.Carrier.AccountingDomain.Entities.Receipts;
using Microsoft.EntityFrameworkCore;

namespace King.Carrier.AccountingInfrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Receipt> Receipts { get; set; }
}
