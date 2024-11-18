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


    // Get profile 

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserProfileDto>> GetUserProfile(int userId)
    {
        var profile = await _userService.GetUserProfileAsync(userId);
        if (profile == null)
            return NotFound();

        return Ok(profile);
    }


    // Get My profile 

    [HttpGet("MyProfile/{userId}")]
    public async Task<ActionResult<MyProfileDto>> GetMyProfile(int userId)
    {
        var profile = await _userService.GetMyProfileAsync(userId);
        if (profile == null)
            return NotFound();

        return Ok(profile);
    }



    // get Attended events

    [HttpGet("{userId}/attended-events")]
    public async Task<ActionResult<IEnumerable<EventListDto>>> GetAttendedEvents(int userId)
    {
        var events = await _userService.GetAttendedEventsAsync(userId);
        return Ok(events);
    }


    // Update user Profile

    [HttpPut("update")]
    public async Task<ActionResult<UserProfileResponseDto>> UpdateProfile(UpdateProfileDto updateProfile)
    {
        try
        {
            var updatedProfile = await _userService.UpdateProfileAsync(updateProfile);
            if (updatedProfile == null)
                return NotFound();

            return Ok(updatedProfile);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}