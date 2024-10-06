using MediatR;
using FluentValidation;
using King.Carrier.TicketsApplication.Persistence;
using King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;
using King.Carrier.TicketsDomain.Entities.Tickets;

namespace King.Carrier.TicketsApplication.Tickets.PayIn.Backoffice.Commands;

public class PayInTicketBackofficeCommand : IRequest<TicketBackoffice>
{
    public string StartLocation { get; set; } = default!;
    public string EndLocation { get; set; } = default!;
    public decimal Price { get; set; }
}

public class PayInTicketBackofficeCommandValidator : AbstractValidator<PayInTicketBackofficeCommand>
{
    public PayInTicketBackofficeCommandValidator()
    {
        RuleFor(e => e.StartLocation).NotEmpty();
        RuleFor(e => e.EndLocation).NotEmpty();
        RuleFor(e => e.Price).NotEmpty();
    }
}

public class PayInTicketBackofficeCommandHandler : IRequestHandler<PayInTicketBackofficeCommand, TicketBackoffice>
{
    private readonly IApplicationDbContext _context;
    private readonly ITicketsPublisher _ticketsPublisher;

    public PayInTicketBackofficeCommandHandler(IApplicationDbContext context, ITicketsPublisher ticketsPublisher)
    {
        _context = context;
        _ticketsPublisher=ticketsPublisher;
    }

    public async Task<TicketBackoffice> Handle(PayInTicketBackofficeCommand command, CancellationToken cancellationToken)
    {
        //provjeriti banku za uplatu
        //pozvati fiskalizaciju

        var ticket = new Ticket
        {
            CreatedTimestamp = DateTime.Now,
            EndLocation = command.EndLocation,
            StartLocation = command.StartLocation,
            SerialNumber = Guid.NewGuid().ToString(),
            Price = command.Price,
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        await PublishTicketMessageOnRabbitMQ(ticket);

        var ticketBackoffice = new TicketBackoffice
        {
            StartLocation = ticket.StartLocation,
            EndLocation = ticket.EndLocation,
            PaidPrice = ticket.Price,
            SerialNumber = ticket.SerialNumber
        };

        return ticketBackoffice;
    }

    private async Task<bool> PublishTicketMessageOnRabbitMQ(Ticket ticket)
    {
        return await _ticketsPublisher.SendMessage(new Integrations.TicketsApi.RabbitMq.TicketMessage
        {
            Price = ticket.Price,
            TicketNumber = ticket.SerialNumber
        });
    }
}