using MasterPiece.Services;
using MasterPiece.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using MasterPiece.DTO;

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




        // GET: api/EventDetails/GetAllEvents
        [HttpGet("GetAllEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }




        // GET: api/EventDetails/{id}
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




        // GET: api/EventDetails/{id}/highlights
        [HttpGet("{id}/highlights")]
        public async Task<IActionResult> GetEventHighlights(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Event ID is not valid.");
            }

            var highlights = await _eventService.GetEventHighLights(id);
            if (highlights == null)
            {
                return NotFound("Event highlights not found.");
            }

            return Ok(highlights);
        }




        // GET: api/EventDetails/{id}/schedule
        [HttpGet("{id}/schedule")]
        public async Task<IActionResult> GetEventSchedule(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Event ID is not valid.");
            }

            var schedule = await _eventService.GetEventSchedule(id);
            if (schedule == null || !schedule.Any())
            {
                return NotFound("Event schedule not found.");
            }

            return Ok(schedule);
        }




        // GET: api/EventDetails/{id}/speakers
        [HttpGet("{id}/speakers")]
        public async Task<IActionResult> GetEventSpeakers(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Event ID is not valid.");
            }

            var speakers = await _eventService.GetEventSpeakers(id);
            if (speakers == null || !speakers.Any())
            {
                return NotFound("No speakers found for this event.");
            }

            return Ok(speakers);
        }




        [HttpGet("summaries")]
        public async Task<ActionResult<IEnumerable<EventSummaryDto>>> GetEventSummaries()
        {
            var eventSummaries = await _eventService.GetEventSummariesAsync();
            return Ok(eventSummaries);
        }



    }
}
