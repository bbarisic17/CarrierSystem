using King.Carrier.TicketsDomain.Entities.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace King.Carrier.TicketsInfrastructure.Persistence.Configurations.Tickets;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> entity)
    {
        entity.HasKey(e => e.Id)
             .HasName("Ticket_PK");

        entity.Property(e => e.SerialNumber)
             .HasMaxLength(150)
             .IsRequired();

        entity.Property(e => e.CreatedTimestamp)
            .IsRequired();

        entity.Property(e => e.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(e => e.StartLocation)
            .HasMaxLength(150)
            .IsRequired();

        entity.Property(e => e.EndLocation)
            .HasMaxLength(150)
            .IsRequired();
    }
}
