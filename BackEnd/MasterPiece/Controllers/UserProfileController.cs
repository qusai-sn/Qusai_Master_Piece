using MasterPiece.DTO;
using MasterPiece.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly IUserService _userService;

    public UserProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserProfileDto>> GetUserProfile(int userId)
    {
        var profile = await _userService.GetUserProfileAsync(userId);
        if (profile == null)
            return NotFound();

        return Ok(profile);
    }



    [HttpGet("{userId}/attended-events")]
    public async Task<ActionResult<IEnumerable<EventListDto>>> GetAttendedEvents(int userId)
    {
        var events = await _userService.GetAttendedEventsAsync(userId);
        return Ok(events);
    }
}