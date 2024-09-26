using MasterPiece.Services;
using MasterPiece.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MasterPiece.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        private readonly IEventService _eventService;


        // Inject the IEventService through the constructor
        public EventDetailsController(IEventService eventService)
        {
            _eventService = eventService;
        }
        


        // GET: api/EventDetails
        [HttpGet("GetAllEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();

            return Ok(events);  
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Event ID is not valid.");
            }

            var eventDetails = await _eventService.GetEventByIdAsync(id);

            if (eventDetails == null)
            {
                return NotFound("Event not found.");
            }

            return Ok(eventDetails);
        }
    }
}
