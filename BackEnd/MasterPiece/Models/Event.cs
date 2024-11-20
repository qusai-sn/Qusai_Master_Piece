using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? EventDate { get; set; }

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

    public string? LocationUrl { get; set; }

    public string? Status { get; set; }

    public int? PlanId { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? WhatToExpect { get; set; }

    public string? Highlights { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual EventCategory? Category { get; set; }

    public virtual ICollection<EventSession> EventSessions { get; set; } = new List<EventSession>();

    public virtual User? Organizer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Plan? Plan { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual EventType? Type { get; set; }

    public virtual ICollection<Speaker> Speakers { get; set; } = new List<Speaker>();
}
