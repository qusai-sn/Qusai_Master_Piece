using Microsoft.AspNetCore.Mvc;
using MasterPiece.DTO;
using MasterPiece.Services;

namespace MasterPiece.Controllers.Profile
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventProfileController : ControllerBase
    {
        private readonly IEventsProfile _eventsProfile;

        public EventProfileController(IEventsProfile eventsProfile)
        {
            _eventsProfile = eventsProfile;
        }

        [HttpGet("{userId}/active")]
        public async Task<ActionResult<IEnumerable<EventProfileDto>>> GetActiveEvents(int userId)
        {
            try
            {
                var events = await _eventsProfile.GetActiveEventsAsync(userId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving active events");
            }
        }

        [HttpGet("{userId}/ended")]
        public async Task<ActionResult<IEnumerable<EventProfileDto>>> GetEndedEvents(int userId)
        {
            try
            {
                var events = await _eventsProfile.GetEndedEventsForUserAsync(userId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving ended events");
            }
        }

        [HttpGet("{userId}/latest")]
        public async Task<ActionResult<IEnumerable<EventProfileDto>>> GetLatestEvents(int userId)
        {
            try
            {
                var events = await _eventsProfile.GetLatestEventsAsync(userId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving latest events");
            }
        }
    }
}