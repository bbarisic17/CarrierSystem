namespace King.Carrier.AccountingInfrastructure.Integrations.RabbitMQ.TicketsApi;

public class TicketMessage
{
    public decimal Price { get; set; }
    public string TicketNumber { get; set; } = default!;
}
