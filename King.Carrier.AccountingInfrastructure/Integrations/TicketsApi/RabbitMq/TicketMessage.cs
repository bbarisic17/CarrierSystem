namespace King.Carrier.AccountingInfrastructure.Integrations.TicketsApi.RabbitMq;

public class TicketMessage
{
    public decimal Price { get; set; }
    public string TicketNumber { get; set; } = default!;
}
