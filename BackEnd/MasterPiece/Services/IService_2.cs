using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;


namespace MasterPiece.Services
{




    // Interface for : get user profile , Get attended events
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task<IEnumerable<EventListDto>> GetAttendedEventsAsync(int userId);
    }



    // Interface for payment process
    public interface IPaymentService
    {
        Task<IEnumerable<TicketDto>> ProcessPaymentAsync(PaymentRequestDto paymentRequest);
    }



    public class UserService : IUserService
    {
        private readonly MasterPieceContext _context;

        public UserService(MasterPieceContext context)
        {
            _context = context;
        }



        // get full user data by : user id 
        public async Task<UserProfileDto> GetUserProfileAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Tickets)
                    .ThenInclude(t => t.Event)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return null;

            return new UserProfileDto
            {

                // user profile data 

                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

                //ticket list data

                Tickets = user.Tickets.Select(t => new TicketDto
                {
                    TicketId = t.TicketId,
                    EventId = t.EventId ?? 0,
                    EventTitle = t.Event?.Title,
                    EventDate = t.Event?.EventDate ?? default,
                    QrCode = t.Qrcode,
                    Price = t.Price ?? 0,
                    PurchaseDate = t.PurchaseDate ?? DateTime.Now
                }),

                // attended event data 

                AttendedEvents = user.Tickets
                    .Where(t => t.Event?.EventDate < DateOnly.FromDateTime(DateTime.Now))
                    .Select(t => new EventListDto
                    {
                        EventId = t.Event.EventId,
                        Title = t.Event.Title,
                        Description = t.Event.Description,
                        ThumbnailUrl = t.Event.ThumbnailUrl,
                        EventDate = t.Event.EventDate ?? default,
                        Location = t.Event.Location
                    })
            };
        }



        // get attended event data by : user id
        public async Task<IEnumerable<EventListDto>> GetAttendedEventsAsync(int userId)
        {
            return await _context.Tickets
                .Where(t => t.UserId == userId && t.Event.EventDate < DateOnly.FromDateTime(DateTime.Now))
                .Select(t => new EventListDto
                {
                    EventId = t.Event.EventId,
                    Title = t.Event.Title,
                    Description = t.Event.Description,
                    ThumbnailUrl = t.Event.ThumbnailUrl,
                    EventDate = t.Event.EventDate ?? default,
                    Location = t.Event.Location,
                    CategoryName = t.Event.Category.CategoryName
                })
                .ToListAsync();
        }
    }



    public class PaymentService : IPaymentService
    {
        private readonly MasterPieceContext _context;

        public PaymentService(MasterPieceContext context)
        {
            _context = context;
        }

        // ignore this
        public async Task<IEnumerable<TicketDto>> ProcessPaymentAsync(PaymentRequestDto paymentRequest)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tickets = new List<Ticket>();

                foreach (var item in paymentRequest.CartItems)
                {
                    var eventEntity = await _context.Events.FindAsync(item.EventId);
                    if (eventEntity == null || eventEntity.AvailableSeats < item.Quantity)
                        throw new Exception($"Not enough seats available for event {item.EventId}");

                    for (int i = 0; i < item.Quantity; i++)
                    {
                        var ticket = new Ticket
                        {
                            EventId = item.EventId,
                            UserId = paymentRequest.UserId,
                            Price = item.Price,
                            PurchaseDate = DateTime.Now,
                            Qrcode = $"QR-{Guid.NewGuid():N}"
                        };
                        tickets.Add(ticket);
                        eventEntity.AvailableSeats--;
                    }
                }

                var payment = new Payment
                {
                    UserId = paymentRequest.UserId,
                    PaymentAmount = paymentRequest.TotalAmount,
                    PaymentDate = DateTime.Now,
                    TransactionId = Guid.NewGuid().ToString()
                };

                _context.Tickets.AddRange(tickets);
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return tickets.Select(t => new TicketDto
                {
                    TicketId = t.TicketId,
                    EventId = t.EventId ?? 0,
                    QrCode = t.Qrcode,
                    Price = t.Price ?? 0,
                    PurchaseDate = t.PurchaseDate ?? DateTime.Now
                });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}