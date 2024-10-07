using MasterPiece.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
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





    }

}
