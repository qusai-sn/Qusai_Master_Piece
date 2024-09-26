using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MasterPiece.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Location { get; set; }

    public int? TotalSeats { get; set; }

    public int? AvailableSeats { get; set; }

    public decimal? TicketPrice { get; set; }

    public int? CategoryId { get; set; }

    public int? TypeId { get; set; }

    public int? OrganizerId { get; set; }

    public string? BannerUrl { get; set; }

    public virtual EventCategory? Category { get; set; }

    public virtual User? Organizer { get; set; }

    [JsonIgnore]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual EventType? Type { get; set; }
}
