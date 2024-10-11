using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class EventType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
