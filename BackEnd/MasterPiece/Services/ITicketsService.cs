using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterPiece.Services
{
    // Define the interface
    public interface ITicketsService
    {
        Task<IEnumerable<SimpleTicketDto>> GetUserTicketsAsync(int userId);
    }

    // Implement the interface in the TicketService class
    public class TicketService : ITicketsService
    {
        private readonly MasterPieceContext _context;

        public TicketService(MasterPieceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SimpleTicketDto>> GetUserTicketsAsync(int userId)
        {
            return await _context.Tickets
                .Where(t => t.UserId == userId)
                .Include(t => t.Event)
                .Select(t => new SimpleTicketDto
                {
                    EventId = t.EventId ?? 0,
                    EventTitle = t.Event.Title,
                    PurchaseDate = t.PurchaseDate ?? DateTime.Now,
                    Price = t.Price ?? 0
                })
                .OrderByDescending(t => t.PurchaseDate)
                .ToListAsync();
        }
    }
}
