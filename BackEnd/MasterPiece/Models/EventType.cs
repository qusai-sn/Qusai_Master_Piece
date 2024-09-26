using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MasterPiece.Models;

public partial class EventType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
