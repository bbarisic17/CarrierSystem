namespace King.Carrier.TicketsApplication.Tickets.PayIn.Backoffice.Commands;

public class TicketBackoffice
{
    public string StartLocation { get; set; } = default!;
    public string EndLocation { get; set; } = default!;
    public decimal PaidPrice { get; set; }
    public string SerialNumber { get; set; } = default!;
}
