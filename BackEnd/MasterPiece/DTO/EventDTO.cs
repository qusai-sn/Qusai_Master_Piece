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

}
