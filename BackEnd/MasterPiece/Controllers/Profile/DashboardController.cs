using MasterPiece.DTO;
using MasterPiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterPiece.Controllers.Profile
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IEventsProfile _eventsProfile;

        public DashboardController(IEventsProfile eventsProfile)
        {
            _eventsProfile = eventsProfile;
        }

        [HttpGet("stats/{userId}")]
        public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats(int userId)
        {
            try
            {
                var stats = await _eventsProfile.GetUserDashboardStats(userId);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving dashboard statistics");
            }
        }
    }
}
