using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace King.Carrier.AccountingInfrastructure.Integrations.TicketsApi.RabbitMq;

public class TicketsApiConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<TicketsApiConsumer> _logger;

    public TicketsApiConsumer()
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
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation(message);
        };

        _channel.BasicConsume(queue: "tickets_queue",
                              autoAck: true,
                              consumer: consumer);

        return Task.CompletedTask;
    }
}
