namespace MasterPiece.DTO
{

    public class EventDetailsDTO
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly? EventDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public string Location { get; set; }
        public int? TotalSeats { get; set; }
        public int? AvailableSeats { get; set; }
        public decimal? TicketPrice { get; set; }
        public string BannerUrl { get; set; }
        public string LocationUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string WhatToExpect { get; set; }
        public string Highlights { get; set; }

        // Category and Type information
        public string CategoryName { get; set; }
        public string TypeName { get; set; }

        // Basic organizer information
        public OrganizerInfoDTO Organizer { get; set; }

        // Tickets summary
        public TicketsSummaryDTO TicketsSummary { get; set; }
    }

    public class OrganizerInfoDTO
    {
        public int OrganizerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class TicketsSummaryDTO
    {
        public int TotalTickets { get; set; }
        public int SoldTickets { get; set; }
        public decimal TotalRevenue { get; set; }
    }


    public class TicketCreateDTO
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Qrcode { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

}
