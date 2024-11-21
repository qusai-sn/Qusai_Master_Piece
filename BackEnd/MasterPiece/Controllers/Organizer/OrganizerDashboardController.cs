// Controllers/OrganizerDashboardController.cs
using MasterPiece.DTO;
using MasterPiece.Models;
using MasterPiece.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizerDashboardController : ControllerBase
    {
        private readonly IOrganizerDashboardService _organizerDashboardService;
        private readonly MasterPieceContext _context;  
        private readonly IEventService _eventService;

        public OrganizerDashboardController(IOrganizerDashboardService organizerDashboardService , MasterPieceContext context, IEventService eventService)
        {
            _organizerDashboardService = organizerDashboardService;
            _context = context;
            _eventService = eventService;
        }



        [HttpGet("{userId}")]
        public async Task<IActionResult> GetDashboard(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            var isOrganizer = await _organizerDashboardService.IsOrganizerAsync(userId);
            if (!isOrganizer)
            {
                return Unauthorized("User is not an organizer");
            }

            var dashboard = await _organizerDashboardService.GetDashboardAsync(userId);
            return Ok(dashboard);
        }




        [HttpGet("recent/{organizerId}")]
        public async Task<ActionResult<IEnumerable<TicketSalesDTO>>> GetRecentSales(int organizerId)
        {
            var sales = await _context.Tickets
                .Where(t => t.Event.OrganizerId == organizerId)
                .OrderByDescending(t => t.PurchaseDate)
                .Take(8)
                .Select(t => new TicketSalesDTO
                {
                    OrderId = $"#{t.TicketId}",
                    EventName = t.Event.Title,
                    Date = t.PurchaseDate ?? DateTime.Now,
                    Price = t.Price ?? 0,
              
                })
                .ToListAsync();

            return Ok(sales);
        }





        [HttpGet("all/{organizerId}")]
        public async Task<ActionResult<IEnumerable<TicketSalesDTO>>> GetAllSales(int organizerId)
        {
            var sales = await _context.Tickets
                .Where(t => t.Event.OrganizerId == organizerId)
                .OrderByDescending(t => t.PurchaseDate)
                .Select(t => new TicketSalesDTO
                {
                    OrderId = $"#{t.TicketId}",
                    EventName = t.Event.Title,
                    Date = t.PurchaseDate ?? DateTime.Now,
                    Price = t.Price ?? 0,
                    
                })
                .ToListAsync();

            return Ok(sales);
        }



        [HttpGet("/table/{organizerId}")]
        public async Task<ActionResult<IEnumerable<OrganizerEventDTO>>> GetOrganizerEvents(int organizerId)
        {
            var events = await _eventService.GetAllEventsAsync();

            var organizerEvents = events
                .Where(e => e.OrganizerId == organizerId)
                .OrderByDescending(e => e.EventDate)
                .Select(e => new OrganizerEventDTO
                {
                    EventId = $"#{e.EventId}",
                    EventName = e.Title,
                    EventDate = e.EventDate?.ToDateTime(e.StartTime ?? TimeOnly.MinValue) ?? DateTime.MinValue,
                    Price = e.TicketPrice ?? 0m
                })
                .ToList();

            return Ok(organizerEvents);
        }


    }
}
//  scaffold "Server=DESKTOP-GIEQN5M;Database=MasterPiece;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c MasterPieceContext  :

// Scaffold-DbContext "Server=DESKTOP-GIEQN5M;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context MyDbContext
