using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;



namespace MasterPiece.Services
{
    public interface ICreateEventService
    {
        Task<CreateEventResponseDTO> CreateEventOverview(EventOverviewDTO overviewDTO);
        Task<CreateEventResponseDTO> AddEventSpeaker(EventSpeakerDTO speakerDTO);
        Task<CreateEventResponseDTO> AddEventSession(EventSessionDTO sessionDTO);
        Task<CreateEventResponseDTO> AddEventTickets(EventTicketDTO ticketDTO);
        Task<CreateEventResponseDTO> FinalizeEvent(int eventId);
        Task<bool> IsEventComplete(int eventId);
    }


    public class CreateEventService : ICreateEventService
    {


        private readonly MasterPieceContext _context;



        public CreateEventService(MasterPieceContext context)
        {
            _context = context;
        }




        public async Task<CreateEventResponseDTO> CreateEventOverview(EventOverviewDTO dto)
        {
            try
            {
                if (dto.StartTime == null || dto.EndTime == null)
                {
                    throw new ArgumentException("StartTime and EndTime must be provided and valid.");
                }


                var newEvent = new Event
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    Highlights = dto.Highlights,
                    WhatToExpect = dto.WhatToExpect,
                    EventDate = DateOnly.FromDateTime(dto.EventDate),
                    StartTime = TimeOnly.FromTimeSpan(dto.StartTime),
                    EndTime = TimeOnly.FromTimeSpan(dto.EndTime),
                    Location = dto.Location,
                    LocationUrl = dto.LocationUrl,
                    TypeId = dto.EventTypeId,
                    CategoryId = dto.EventCategoryId,
                    OrganizerId = dto.OrganizerId,
                    ThumbnailUrl = dto.ThumbnailUrl,
                    BannerUrl = dto.BannerUrl,
                    Status = "Draft",
                    AvailableSeats = dto.TotalSeats,
                    TotalSeats = dto.TotalSeats
                };

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return new CreateEventResponseDTO
                {
                    Success = true,
                    EventId = newEvent.EventId,
                    Message = "Event overview created successfully"
                };
            }
            catch (Exception ex)
            {
                return new CreateEventResponseDTO
                {
                    Success = false,
                    Message = $"Failed to create event overview: {ex.Message}"
                };
            }
        }










        public async Task<CreateEventResponseDTO> AddEventSpeaker(EventSpeakerDTO dto)
        {

            try
            {
                var speaker = new Speaker
                {
                    Name = dto.Name,
                    Role = dto.Role,
                    Bio = dto.Bio
                };



                _context.Speakers.Add(speaker);
                await _context.SaveChangesAsync();


                // Add to junction table
                var eventSpeaker = new Dictionary<string, object>
                {
                    ["EventId"] = dto.EventId,
                    ["SpeakerId"] = speaker.SpeakerId
                };


                await _context.Database.ExecuteSqlRawAsync(
                    "INSERT INTO EventSpeakers (EventId, SpeakerId) VALUES ({0}, {1})",
                    dto.EventId, speaker.SpeakerId);

                return new CreateEventResponseDTO
                {
                    Success = true,
                    EventId = dto.EventId,
                    Message = "Speaker added successfully"
                };
            }
            catch (Exception ex)
            {
                return new CreateEventResponseDTO
                {
                    Success = false,
                    Message = "Failed to add speaker"
                };
            }
        }




        public async Task<CreateEventResponseDTO> AddEventSession(EventSessionDTO dto)
        {
            try
            {


                var session = new EventSession
                {
                    EventId = dto.EventId,
                    SessionTitle = dto.SessionTitle,
                    SessionDescription = dto.SessionDescription,
                    SessionTime = dto.SessionTime,
                    SessionType = dto.SessionType
                };



                _context.EventSessions.Add(session);
                await _context.SaveChangesAsync();



                return new CreateEventResponseDTO
                {
                    Success = true,
                    EventId = dto.EventId,
                    Message = "Session added successfully"
                };
            }
            catch (Exception ex)
            {
                return new CreateEventResponseDTO
                {
                    Success = false,
                    Message = $"Failed to add session : {ex.Message}"
                };
            }
        }




        public async Task<CreateEventResponseDTO> AddEventTickets(EventTicketDTO dto)
        {

            try
            {
                var @event = await _context.Events.FindAsync(dto.EventId);
                if (@event == null) throw new Exception("Event not found");


                @event.TicketPrice = dto.Price ?? 0;
                @event.TotalSeats = dto.TotalSeats ?? 0;
                @event.AvailableSeats = dto.TotalSeats ?? 0;


                await _context.SaveChangesAsync();


                return new CreateEventResponseDTO
                {
                    Success = true,
                    EventId = dto.EventId,
                    Message = "Ticket information added successfully"
                };
            }

            catch (Exception ex)
            {
                return new CreateEventResponseDTO
                {
                    Success = false,
                    Message = "Failed to add ticket information"
                };
            }
        }



        public async Task<CreateEventResponseDTO> FinalizeEvent(int eventId)
        {

            try
            {
                var @event = await _context.Events
                    .Include(e => e.Speakers)
                    .Include(e => e.EventSessions)
                    .FirstOrDefaultAsync(e => e.EventId == eventId);

                if (@event == null)
                    throw new Exception("Event not found");

                if (!await IsEventComplete(eventId))
                    throw new Exception("Event is incomplete");

                @event.Status = "Pending";  // Change status from Draft to Pending
                await _context.SaveChangesAsync();

                return new CreateEventResponseDTO
                {
                    Success = true,
                    EventId = eventId,
                    Message = "Event finalized successfully"
                };
            }
            catch (Exception ex)
            {
                return new CreateEventResponseDTO
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<bool> IsEventComplete(int eventId)
        {
            var @event = await _context.Events
                .Include(e => e.Speakers)
                .Include(e => e.EventSessions)
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (@event == null) return false;

            // Check if all required components are present
            return !string.IsNullOrEmpty(@event.Title) &&
                   !string.IsNullOrEmpty(@event.Description) &&
                   @event.Speakers.Any() &&
                   @event.EventSessions.Any() &&
                   (@event.TicketPrice > 0 || @event.TotalSeats > 0);
        }
    }





}
