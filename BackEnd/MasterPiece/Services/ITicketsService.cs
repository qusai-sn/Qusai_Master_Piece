using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto1>> GetAllTicketsAsync();
    }

    public class TicketService : ITicketService
    {
        private readonly MasterPieceContext _context;

        public TicketService(MasterPieceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TicketDto1>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .Include(t => t.Event)
                .ThenInclude(e => e.Category)
                .Select(t => new TicketDto1
                {
                    TicketId = t.TicketId,
                    EventTitle = t.Event != null ? t.Event.Title : null,
                    Category = t.Event != null && t.Event.Category != null ? t.Event.Category.CategoryName : null,
                    EventDate = t.Event != null ? t.Event.EventDate : null,
                    StartTime = t.Event != null ? t.Event.StartTime : null,
                    Price = t.Price,
                    PurchaseDate = t.PurchaseDate != null ? t.PurchaseDate.Value : DateTime.Now,
                    Location = t.Event != null ? t.Event.Location : null,
                    Status = t.Event != null && t.Event.EventDate != null
                        ? DetermineTicketStatus(t.Event.EventDate.Value)
                        : "Unknown"
                })
                .OrderByDescending(t => t.EventDate)
                .ToListAsync();
        }
        private string DetermineTicketStatus(DateOnly eventDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return eventDate < today ? "Expired" :
                   eventDate == today ? "Active" : "Upcoming";
        }
    }
}










