namespace King.Carrier.TicketsDomain.Entities.Tickets;

public class Ticket
{
    public int Id { get; set; }
    public string StartLocation { get; set; } = default!;
    public string EndLocation { get; set; } = default!;
    public string SerialNumber { get; set; } = default!;
    public DateTime CreatedTimestamp { get; set; }
    public decimal Price { get; set; }
}
