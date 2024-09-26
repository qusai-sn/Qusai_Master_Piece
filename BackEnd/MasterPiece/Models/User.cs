using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MasterPiece.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Username { get; set; }

    public string? Biography { get; set; }

    [JsonIgnore]
    public string? PasswordHash { get; set; }

    [JsonIgnore]
    public string? Salt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [JsonIgnore]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
