using Microsoft.AspNetCore.Mvc;
using MasterPiece.Services;
using MasterPiece.DTO;

namespace MasterPiece.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketService;

        public TicketsController(ITicketsService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<SimpleTicketDto>>> GetUserTickets(int userId)
        {
            try
            {
                var tickets = await _ticketService.GetUserTicketsAsync(userId);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving tickets");
            }
        }
    }
}