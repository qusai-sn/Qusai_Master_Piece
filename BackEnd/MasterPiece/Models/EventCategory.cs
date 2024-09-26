using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MasterPiece.Models;

public partial class EventCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
