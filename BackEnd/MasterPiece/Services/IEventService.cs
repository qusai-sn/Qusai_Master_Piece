using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<EventDetailsDTO> GetEventByIdAsync(int eventId);
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

        public async Task<EventDetailsDTO> GetEventByIdAsync(int eventId)
        {
            if (eventId <= 0)
            {
                return null;
            }

            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Type)
                .Include(e => e.Organizer)
                .Include(e => e.Tickets)
                .Where(e => e.EventId == eventId)
                .Select(e => new EventDetailsDTO
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    EventDate = e.EventDate,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Location = e.Location,
                    TotalSeats = e.TotalSeats,
                    AvailableSeats = e.AvailableSeats,
                    TicketPrice = e.TicketPrice,
                    BannerUrl = e.BannerUrl,
                    LocationUrl = e.LocationUrl,
                    ThumbnailUrl = e.ThumbnailUrl,
                    WhatToExpect = e.WhatToExpect,
                    Highlights = e.Highlights,
                    CategoryName = e.Category.CategoryName,
                    TypeName = e.Type.TypeName,
                    Organizer = new OrganizerInfoDTO
                    {
                        OrganizerId = e.Organizer.UserId,
                        Name = $"{e.Organizer.FirstName} {e.Organizer.LastName}",
            
                    },
                    TicketsSummary = new TicketsSummaryDTO
                    {
                        TotalTickets = e.TotalSeats ?? 0,
             
              
                    }
                })
                .FirstOrDefaultAsync();
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
                    Price = e.TicketPrice ?? 0m                   // Adjust if you have a specific price logic
                })
                .ToListAsync();
        }


    }

}
