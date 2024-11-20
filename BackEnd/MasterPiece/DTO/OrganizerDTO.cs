namespace MasterPiece.DTO
{

    public class OrganizerDashboardDTO
    {
        public int MyEventsCount { get; set; }
        public int ActiveEventsCount { get; set; }
        public int EndedEventsCount { get; set; }
        public int TotalAttendees { get; set; }
        public int TotalTickets { get; set; }
        public decimal TotalEarnings { get; set; }
        public List<EventSummaryDto> LatestEvents { get; set; }
        public List<OrganizerTicketSaleDTO> LatestTicketSales { get; set; }
    }

    public class OrganizerTicketSaleDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public decimal Earnings { get; set; }
        public DateTime Date { get; set; }
    }

    // DTO/TicketSalesDTO.cs
    public class TicketSalesDTO
    {
        public string OrderId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
     }

    // DTO/OrganizerEventDTO.cs
    public class OrganizerEventDTO
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
    }


    // DTO/PaymentMethodDTO.cs
    public class PaymentMethodDTO
    {
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public bool IsDefault { get; set; }
    }

}
