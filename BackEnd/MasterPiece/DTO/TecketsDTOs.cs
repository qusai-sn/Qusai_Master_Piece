namespace MasterPiece.DTO
{
    public class TicketDto1
    {
        public int TicketId { get; set; }
        public string? EventTitle { get; set; }
        public string? Category { get; set; }
        public DateOnly? EventDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public decimal? Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
    }
}