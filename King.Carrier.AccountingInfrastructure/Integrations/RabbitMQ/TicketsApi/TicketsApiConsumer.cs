using King.Carrier.AccountingApplication.Integrations.RabbitMQ;
using King.Carrier.AccountingApplication.Persistence;
using King.Carrier.AccountingDomain.Entities.Receipts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace King.Carrier.AccountingInfrastructure.Integrations.RabbitMQ.TicketsApi;

public class TicketsApiConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<TicketsApiConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<RabbitMQSettings> _rabbitMQSettings;

    public TicketsApiConsumer(IServiceProvider serviceProvider, ILogger<TicketsApiConsumer> logger, IOptions<RabbitMQSettings> rabbitMQSettings)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _rabbitMQSettings = rabbitMQSettings;

        var factory = new ConnectionFactory() { HostName = _rabbitMQSettings.Value.HostName, Port = _rabbitMQSettings.Value.Port };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _rabbitMQSettings.Value.TicketsQueueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        _channel.ExchangeDeclare(exchange: _rabbitMQSettings.Value.CarrierExchangeName,
                                 type: ExchangeType.Direct,
                                 durable: true);

        _channel.QueueBind(queue: _rabbitMQSettings.Value.TicketsQueueName,
                          exchange: _rabbitMQSettings.Value.CarrierExchangeName,
                          routingKey: "ticket");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var ticketMessage = JsonSerializer.Deserialize<TicketMessage>(message);
            var receiptId = await ProcessMessage(ticketMessage, stoppingToken);
            _logger.LogInformation(message);
        };


        _channel.BasicConsume(queue: _rabbitMQSettings.Value.TicketsQueueName,
                              autoAck: true,
                              consumer: consumer);
    }

    private async Task<int> ProcessMessage(TicketMessage ticketMessage, CancellationToken cancellationToken)
    {
        var receipt = new Receipt
        {
            CreatedTimestamp = DateTime.Now,
            Price = ticketMessage.Price,
            SerialNumber = ticketMessage.TicketNumber
        };

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            await dbContext.Receipts.AddAsync(receipt, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Received ticket with id {receipt.Id}");
            return receipt.Id;
        }
    }
}
