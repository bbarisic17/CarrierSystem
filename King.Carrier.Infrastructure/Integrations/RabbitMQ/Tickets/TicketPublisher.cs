using King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;
using King.Carrier.TicketsApplication.Integrations.TicketsApi.RabbitMq;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ.Tickets;

public class TicketPublisher : ITicketsPublisher
{
    private readonly RabbitMqSetupService _rabbitMqSetupService;

    public TicketPublisher(RabbitMqSetupService rabbitMqSetupService)
    {
        _rabbitMqSetupService = rabbitMqSetupService;
    }

    public async Task<bool> SendMessage(TicketMessage message)
    {
        var messageSerialized = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageSerialized);

        _rabbitMqSetupService.GetChannel().BasicPublish(exchange: "carrier_exchange",
                              routingKey: "ticket",
                              basicProperties: null,
                              body: body);

        return true;
    }
}
