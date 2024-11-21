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

        Task<Ticket> CreateTicketAsync(TicketCreateDTO ticketDto);


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



        public async Task<Ticket> CreateTicketAsync(TicketCreateDTO ticketDto)
        {
            var ticket = new Ticket
            {
                EventId = ticketDto.EventId,
                UserId = ticketDto.UserId,
                Qrcode = ticketDto.Qrcode,
                Price = ticketDto.Price,
                PurchaseDate = ticketDto.PurchaseDate
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }











    }
}










