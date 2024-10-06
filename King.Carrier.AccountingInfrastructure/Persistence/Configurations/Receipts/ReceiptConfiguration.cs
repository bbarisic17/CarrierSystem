using King.Carrier.AccountingDomain.Entities.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace King.Carrier.AccountingInfrastructure.Persistence.Configurations.Receipts;

public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> entity)
    {
        entity.HasKey(e => e.Id)
             .HasName("Receipt_PK");

        entity.Property(e => e.SerialNumber)
             .HasMaxLength(150)
             .IsRequired();

        entity.Property(e => e.CreatedTimestamp)
            .IsRequired();
    }
}
