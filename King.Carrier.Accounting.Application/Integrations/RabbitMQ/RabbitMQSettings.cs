namespace King.Carrier.AccountingApplication.Integrations.RabbitMQ;

public class RabbitMQSettings
{
    public const string SectionName = "RabbitMQSettings";
    public string HostName { get; set; } = default!;
    public int Port {  get; set; }
    public string TicketsQueueName { get; set; } = default!;
    public string CarrierExchangeName {  get; set; } = default!;
}
