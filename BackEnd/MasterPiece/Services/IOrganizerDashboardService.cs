
// Services/IOrganizerDashboardService.cs
using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
    public interface IOrganizerDashboardService
    {
        Task<OrganizerDashboardDTO> GetDashboardAsync(int organizerId);
        Task<bool> IsOrganizerAsync(int userId);
    }
}

// Services/OrganizerDashboardService.cs
namespace MasterPiece.Services
{
    public class OrganizerDashboardService : IOrganizerDashboardService
    {
        private readonly MasterPieceContext _context;

        public OrganizerDashboardService(MasterPieceContext context)
        {
            _context = context;
        }

        public async Task<bool> IsOrganizerAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.IsOrganizer ?? false;
        }

        public async Task<OrganizerDashboardDTO> GetDashboardAsync(int organizerId)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            var events = await _context.Events
                .Where(e => e.OrganizerId == organizerId)
                .Include(e => e.Tickets)
                .Include(e => e.Category)
                .Include(e => e.Type)
                .ToListAsync();

            var activeEvents = events.Where(e => e.EventDate >= currentDate).ToList();
            var endedEvents = events.Where(e => e.EventDate < currentDate).ToList();

            var dashboard = new OrganizerDashboardDTO
            {
                MyEventsCount = events.Count,
                ActiveEventsCount = activeEvents.Count,
                EndedEventsCount = endedEvents.Count,
                TotalAttendees = events.Sum(e => e.Tickets.Count),
                TotalTickets = events.Sum(e => e.TotalSeats ?? 0),
                TotalEarnings = events.Sum(e => e.Tickets.Sum(t => t.Price ?? 0)),

                LatestEvents = events
                    .OrderByDescending(e => e.EventDate)
                    .Take(3)
                    .Select(e => new EventSummaryDto
                    {
                        EventId = e.EventId,
                        Title = e.Title,
                        Description = e.Description,
                        ThumbnailUrl = e.ThumbnailUrl,
                        Category = e.Category?.CategoryName,
                        Price = e.TicketPrice ?? 0m
                    })
                    .ToList(),

                LatestTicketSales = await _context.Tickets
                    .Where(t => t.Event.OrganizerId == organizerId)
                    .OrderByDescending(t => t.PurchaseDate)
                    .Take(4)
                    .Select(t => new OrganizerTicketSaleDTO
                    {
                        EventId = t.EventId ?? 0,
                        EventName = t.Event.Title,
                        Earnings = t.Price ?? 0,
                        Date = t.PurchaseDate ?? DateTime.Now
                    })
                    .ToListAsync()
            };

            return dashboard;
        }
    }
}