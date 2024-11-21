namespace MasterPiece.DTO
{
    public class EventOverviewDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
   
        public string Location { get; set; }
        public string LocationUrl { get; set; }
        public int? EventTypeId { get; set; }
        public int? TotalSeats { get; set; }
        public int? EventCategoryId { get; set; }
        public int OrganizerId { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? WhatToExpect { get; set; }
        public string? Highlights { get; set; }

        public IFormFile ThumbnailFile { get; set; }
        public IFormFile BannerFile { get; set; }
    }

    public class EventSpeakerDTO
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Bio { get; set; }
 
    }

    public class EventSessionDTO
    {
        public int EventId { get; set; }
        public string SessionTitle { get; set; }
        public string SessionDescription { get; set; }
        public string SessionTime { get; set; }
        public string SessionType { get; set; }  // Morning, Afternoon, Evening
    }

    public class EventTicketDTO
    {
        public int EventId { get; set; }
        public string TicketType { get; set; }  // Free or Paid
        public decimal? Price { get; set; }
        public decimal? DonationLimit { get; set; }
        public int? TotalSeats { get; set; }
    }

    public class CreateEventResponseDTO
    {
        public bool Success { get; set; }
        public int? EventId { get; set; }
        public string Message { get; set; }
    }


}
