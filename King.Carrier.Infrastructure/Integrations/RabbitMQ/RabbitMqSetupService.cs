using RabbitMQ.Client;

namespace King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;

public class RabbitMqSetupService
{
    private readonly IModel _channel;

    public RabbitMqSetupService()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.QueueDeclare(queue: "tickets_queue",
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        _channel.ExchangeDeclare(exchange: "carrier_exchange",
                                 type: ExchangeType.Direct,
                                 durable: true);

        _channel.QueueBind(queue: "tickets_queue",
                           exchange: "carrier_exchange",
                           routingKey: "ticket");
    }

    public IModel GetChannel()
    {
        return _channel;
    }
}
