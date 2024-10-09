using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace King.Carrier.TicketsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthcheckController : ControllerBase
{
    private readonly ILogger<HealthcheckController> _logger;
    private readonly IHttpContextAccessor _context;

    public HealthcheckController(ILogger<HealthcheckController> logger, IHttpContextAccessor context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("healthcheck")]
    public IActionResult Healthcheck()
    {
        var message = $"{_context!.HttpContext!.Request.Host} is healthy";
        _logger.LogInformation(message);
        return Ok(message);
    }
}
