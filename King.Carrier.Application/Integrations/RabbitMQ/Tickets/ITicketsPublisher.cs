using King.Carrier.TicketsApplication.Integrations.TicketsApi.RabbitMq;

namespace King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;

public interface ITicketsPublisher
{
    Task<bool> SendMessage(TicketMessage ticketMessage);
}
