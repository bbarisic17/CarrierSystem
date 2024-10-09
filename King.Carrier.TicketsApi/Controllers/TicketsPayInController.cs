using King.Carrier.TicketsApplication.Tickets.PayIn.Backoffice.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace King.Carrier.TicketsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsPayInController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketsPayInController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("backofficePayIn")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PayInTicketBackoffice(PayInTicketBackofficeCommand payInTicketBackofficeCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(payInTicketBackofficeCommand, cancellationToken);

            return Ok(result);
        }

        [HttpPost("mobilePayIn")]
        public async Task<IActionResult> PayInTicketMobileApp()
        {
            return Ok();
        }
    }
}
