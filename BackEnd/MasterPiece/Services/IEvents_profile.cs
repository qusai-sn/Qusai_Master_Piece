using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
    public interface IEventsProfile
    {
        Task<IEnumerable<EventProfileDto>> GetActiveEventsAsync(int userId, int limit = 6);
        Task<IEnumerable<EventProfileDto>> GetEndedEventsForUserAsync(int userId, int limit = 6);
        Task<IEnumerable<EventProfileDto>> GetLatestEventsAsync(int userId, int limit = 6);
        Task<DashboardStatsDto> GetUserDashboardStats(int userId);
    }

    public class EventsProfileService : IEventsProfile
    {
        private readonly MasterPieceContext _context;

        public EventsProfileService(MasterPieceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventProfileDto>> GetActiveEventsAsync(int userId, int limit = 6)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            return await _context.Tickets
                .Where(t => t.UserId == userId &&
                           ((t.Event.EventDate == today && t.Event.StartTime >= currentTime) ||
                            t.Event.EventDate > today))
                .OrderBy(t => t.Event.EventDate)
                .ThenBy(t => t.Event.StartTime)
                .Take(limit)
                .Select(t => new EventProfileDto
                {
                    EventId = t.Event.EventId,
                    Title = t.Event.Title,
                    ThumbnailUrl = t.Event.ThumbnailUrl,
                    Category = t.Event.Category.CategoryName,
                    Type = t.Event.Type.TypeName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EventProfileDto>> GetEndedEventsForUserAsync(int userId, int limit = 6)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            return await _context.Tickets
                .Where(t => t.UserId == userId &&
                           t.Event.EventDate < today)
                .OrderByDescending(t => t.Event.EventDate)
                .Take(limit)
                .Select(t => new EventProfileDto
                {
                    EventId = t.Event.EventId,
                    Title = t.Event.Title,
                    ThumbnailUrl = t.Event.ThumbnailUrl,
                    Category = t.Event.Category.CategoryName,
                    Type = t.Event.Type.TypeName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EventProfileDto>> GetLatestEventsAsync(int userId, int limit = 6)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            // Get user's interests based on past tickets
            var userCategories = await _context.Tickets
                .Where(t => t.UserId == userId)
                .Select(t => t.Event.CategoryId)
                .Distinct()
                .ToListAsync();

            // Get latest events prioritizing user's interests
            return await _context.Events
                .Where(e => e.EventDate >= today)
                .OrderByDescending(e => userCategories.Contains(e.CategoryId ?? 0)) // Prioritize user's interests
                .ThenBy(e => e.EventDate)
                .ThenBy(e => e.StartTime)
                .Take(limit)
                .Select(e => new EventProfileDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    ThumbnailUrl = e.ThumbnailUrl,
                    Category = e.Category.CategoryName,
                    Type = e.Type.TypeName
                })
                .ToListAsync();
        }


        public async Task<DashboardStatsDto> GetUserDashboardStats(int userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            // Get events in waiting (upcoming events user has tickets for)
            var eventsInWaiting = await _context.Tickets
                .Where(t => t.UserId == userId && t.Event.EventDate > today)
                .Select(t => t.EventId)
                .Distinct()
                .CountAsync();

            // Get active events (events happening today that user has tickets for)
            var activeEvents = await _context.Tickets
                .Where(t => t.UserId == userId && t.Event.EventDate == today)
                .Select(t => t.EventId)
                .Distinct()
                .CountAsync();

            // Get attended events (past events user has tickets for)
            var attendedEvents = await _context.Tickets
                .Where(t => t.UserId == userId && t.Event.EventDate < today)
                .Select(t => t.EventId)
                .Distinct()
                .CountAsync();

            // Get latest events
            var latestEvents = await _context.Tickets
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Event.EventDate)
                .Select(t => new EventProfileDto
                {
                    EventId = t.Event.EventId,
                    Title = t.Event.Title,
                    ThumbnailUrl = t.Event.ThumbnailUrl,
                    Category = t.Event.Category.CategoryName,
                    Type = t.Event.Type.TypeName
                })
                .Take(3)
                .ToListAsync();

            return new DashboardStatsDto
            {
                EventsInWaiting = eventsInWaiting,
                ActiveEvents = activeEvents,
                AttendedEvents = attendedEvents,
                LatestEvents = latestEvents
            };
        }

    }




}