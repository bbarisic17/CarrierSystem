using King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;
using King.Carrier.TicketsApplication.Integrations.TicketsApi.RabbitMq;
using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace King.Carrier.TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor context)
        {
            _logger = logger;
            _context=context;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("healthcheck")]
        public IActionResult Healthcheck()
        {
            var msg = $"{_context.HttpContext.Request.Host} is healthy";
            _logger.LogInformation(msg);
            return Ok(msg);
        }

        //[HttpPost("publishTicket")]
        //public async Task<IActionResult> PublishTicket(int price, string ticketNumber)
        //{
        //    var ticketMessage = new TicketMessage
        //    {
        //        Price = price,
        //        TicketNumber = ticketNumber
        //    };

        //    _ticketsPublisher.SendMessage(ticketMessage);
        //    return Ok();
        //}
    }
}
