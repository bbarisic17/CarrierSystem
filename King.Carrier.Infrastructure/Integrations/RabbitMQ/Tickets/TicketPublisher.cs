using King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;
using King.Carrier.TicketsApplication.Integrations.TicketsApi.RabbitMq;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ.Tickets;

public class TicketPublisher : ITicketsPublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitMqSetupService _rabbitMqSetupService;

    public TicketPublisher(RabbitMqSetupService rabbitMqSetupService)
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "tickets_queue",
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        _channel.ExchangeDeclare(exchange: "carrier_exchange",
                                 type: ExchangeType.Direct, // Change the type as needed (e.g., Fanout, Topic)
                                 durable: true);

        _channel.QueueBind(queue: "tickets_queue",
                          exchange: "carrier_exchange",
                          routingKey: "ticket");

        _rabbitMqSetupService=rabbitMqSetupService;
    }

    public async Task<bool> SendMessage(TicketMessage message)
    {
        var messageSerialized = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageSerialized);

        //_channel.BasicPublish(exchange: "carrier_exchange",
        //                      routingKey: "ticket",
        //                      basicProperties: null,
        //                      body: body);

        _rabbitMqSetupService.GetChannel().BasicPublish(exchange: "carrier_exchange",
                              routingKey: "ticket",
                              basicProperties: null,
                              body: body);

        return true;
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
