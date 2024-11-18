using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task<string> GetEventHighLights(int eventId);  
        Task<IEnumerable<EventSession>> GetEventSchedule(int eventId);
        Task<IEnumerable<Speaker>> GetEventSpeakers(int eventId);
        Task<IEnumerable<EventSummaryDto>> GetEventSummariesAsync();



    }

    public class EventService : IEventService
    {
        private readonly MasterPieceContext _context;

        public EventService(MasterPieceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Category)    // Include Category
                .Include(e => e.Type)        // Include Type
                .Include(e => e.Organizer)   // Include Organizer
                .Include(e => e.Tickets)     // Include Tickets
                .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            if (eventId <= 0)
            {
                return null;
            }

            return await _context.Events
                .Include(e => e.Category)    // Include Category
                .Include(e => e.Type)        // Include Type
                .Include(e => e.Organizer)   // Include Organizer
                .Include(e => e.Tickets)     // Include Tickets
                .FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<string> GetEventHighLights(int eventId)
        {
            if (eventId <= 0)
            {
                return null;
            }

            var eventEntity = await _context.Events
                                    .Where(e => e.EventId == eventId)
                                    .Select(e => e.Highlights)
                                    .FirstOrDefaultAsync();

            return eventEntity;
        }


        public async Task<IEnumerable<EventSession>> GetEventSchedule(int eventId)
        {
            if (eventId <= 0)
            {
                return null;
            }

            var eventSessions = await _context.EventSessions
                .Where(es => es.EventId == eventId)
                .ToListAsync();

            return eventSessions;
        }

        public async Task<IEnumerable<Speaker>> GetEventSpeakers(int eventId)
        {
            if (eventId <= 0)
            {
                return null;
            }

            var speakers = await _context.Speakers
                .Where(s => s.Events.Any(e => e.EventId == eventId)) // Join Speakers with the Event
                .ToListAsync();

            return speakers;
        }

        public async Task<IEnumerable<EventSummaryDto>> GetEventSummariesAsync()
        {
            return await _context.Events
                .Select(e => new EventSummaryDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    ThumbnailUrl = e.ThumbnailUrl,          // Assuming this property exists
                    Category = e.Category.CategoryName,     // Adjust according to your model
                    Price = e.TicketPrice                   // Adjust if you have a specific price logic
                })
                .ToListAsync();
        }


    }

}
