// TicketsController.cs
using Microsoft.AspNetCore.Mvc;
using MasterPiece.Services;
using MasterPiece.DTO;

namespace MasterPiece.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketService;

        public TicketsController(ITicketsService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDTO ticketDto)
        {
            try
            {
                var ticket = await _ticketService.CreateTicketAsync(ticketDto);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}