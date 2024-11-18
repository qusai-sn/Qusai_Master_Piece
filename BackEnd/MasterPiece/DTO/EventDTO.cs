namespace MasterPiece.DTO
{
    public class EventSummaryDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
    public class EventListDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public decimal TicketPrice { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public string Location { get; set; }
        public string CategoryName { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class CartItemDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; } // list of tickets
        public IEnumerable<EventListDto> AttendedEvents { get; set; } // list of attendded events 
    }

    public class MyProfileDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Biography { get; set; }

    }



    public class TicketDto
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateOnly EventDate { get; set; }
        public string QrCode { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class PaymentRequestDto
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class UpdateProfileDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Biography { get; set; }
    }

    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UserProfileResponseDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Biography { get; set; }
        public bool IsOrganizer { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class SimpleTicketDto
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }

    public class EventProfileDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
    
    }
    public class DashboardStatsDto
    {
        public int EventsInWaiting { get; set; }
        public int ActiveEvents { get; set; }
        public int AttendedEvents { get; set; }
        public List<EventProfileDto> LatestEvents { get; set; }
    }


}
