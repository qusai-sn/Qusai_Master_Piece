using MasterPiece.DTO;
using MasterPiece.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CreateEventController : ControllerBase
{


    private readonly ICreateEventService _createEventService;

    public CreateEventController(ICreateEventService createEventService)
    {
        _createEventService = createEventService;
    }

    private async Task<string> UploadImageAsync(IFormFile file, string subfolder)
    {
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Media", "eventBanners", subfolder);
        Directory.CreateDirectory(uploadFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"{Request.Scheme}://{Request.Host}/Media/eventBanners/{subfolder}/{fileName}"; 
    }



    [HttpPost("overview")]
    public async Task<ActionResult<CreateEventResponseDTO>> CreateEventOverview([FromForm] EventOverviewDTO overviewDTO)
    {

        var thumbnailUrl = await UploadImageAsync(overviewDTO.ThumbnailFile, "thumbnails");
        var bannerUrl = await UploadImageAsync(overviewDTO.BannerFile, "banners");

        overviewDTO.ThumbnailUrl = thumbnailUrl;
        overviewDTO.BannerUrl = bannerUrl;

        var result = await _createEventService.CreateEventOverview(overviewDTO);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }



    [HttpPost("speaker")]
    public async Task<ActionResult<CreateEventResponseDTO>> AddEventSpeaker([FromForm] EventSpeakerDTO speakerDTO)
    {
 
 
        var result = await _createEventService.AddEventSpeaker(speakerDTO);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }



    [HttpPost("session")]
    public async Task<ActionResult<CreateEventResponseDTO>> AddEventSession([FromBody] EventSessionDTO sessionDTO)
    {
        var result = await _createEventService.AddEventSession(sessionDTO);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }




    [HttpPost("tickets")]
    public async Task<ActionResult<CreateEventResponseDTO>> AddEventTickets([FromBody] EventTicketDTO ticketDTO)
    {
        var result = await _createEventService.AddEventTickets(ticketDTO);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }




    [HttpPost("finalize/{eventId}")]
    public async Task<ActionResult<CreateEventResponseDTO>> FinalizeEvent(int eventId)
    {
        var result = await _createEventService.FinalizeEvent(eventId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }




    [HttpGet("status/{eventId}")]
    public async Task<ActionResult<bool>> IsEventComplete(int eventId)
    {
        var isComplete = await _createEventService.IsEventComplete(eventId);
        return Ok(isComplete);
    }




}