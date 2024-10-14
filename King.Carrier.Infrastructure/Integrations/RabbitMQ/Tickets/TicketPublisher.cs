using King.Carrier.TicketsApplication.Integrations.Caching.HybridCache;
using King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;
using King.Carrier.TicketsApplication.Integrations.TicketsApi.RabbitMq;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ.Tickets;

public class TicketPublisher : ITicketsPublisher
{
    private readonly RabbitMqSetupService _rabbitMqSetupService;
    private readonly IHybridCache _hybridCache;

    public TicketPublisher(RabbitMqSetupService rabbitMqSetupService, IHybridCache hybridCache)
    {
        _rabbitMqSetupService = rabbitMqSetupService;
        _hybridCache = hybridCache;
    }

    public async Task<bool> SendMessage(TicketMessage message)
    {
        var messageSerialized = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageSerialized);

        await _hybridCache.SetAsync(message.TicketNumber, "Ticket", TimeSpan.FromMinutes(15));

        _rabbitMqSetupService.GetChannel().BasicPublish(exchange: "carrier_exchange",
                              routingKey: "ticket",
                              basicProperties: null,
                              body: body);

        return true;
    }
}
