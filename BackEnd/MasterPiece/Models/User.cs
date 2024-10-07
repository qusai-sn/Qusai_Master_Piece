using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? Salt { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Username { get; set; }

    public string? Biography { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
