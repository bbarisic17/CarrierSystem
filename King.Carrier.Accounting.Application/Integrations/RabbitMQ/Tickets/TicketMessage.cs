namespace King.Carrier.AccountingApplication.Integrations.RabbitMQ.Tickets;

public class TicketMessage
{
    public decimal Price { get; set; }
    public string TicketNumber { get; set; } = default!;
}
