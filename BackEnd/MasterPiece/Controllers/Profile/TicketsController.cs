using Microsoft.AspNetCore.Mvc;
using MasterPiece.Services;
using MasterPiece.DTO;

namespace MasterPiece.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Tickets2Controller : ControllerBase
    {
        private readonly ITicketsService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        public Tickets2Controller(ITicketsService ticketService, ILogger<TicketsController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto1>>> GetAllTickets()
        {
            try
            {
                var tickets = await _ticketService.GetUserTicketsAsync();
                return Ok(tickets ?? Enumerable.Empty<TicketDto1>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllTickets: {ex.Message}", ex);
                return StatusCode(500, new { message = "Error retrieving tickets", error = ex.Message });
            }
        }
    }
}