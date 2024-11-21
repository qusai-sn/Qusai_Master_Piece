using MasterPiece.DTO;
using MasterPiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterPiece.Controllers.Organizer
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public OrganizerProfileController(IUserService userService)
        {
            _userService = userService;
        }


        // Get organizer's profile
        [HttpGet("{userId}")]
        public async Task<ActionResult<MyProfileDto>> GetOrganizerProfile(int userId)
        {
            var profile = await _userService.GetMyProfileAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }




        // Update organizer's profile
        [HttpPut("update")]
        public async Task<ActionResult<UserProfileResponseDto>> UpdateOrganizerProfile(UpdateProfileDto updateProfile)
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
}
