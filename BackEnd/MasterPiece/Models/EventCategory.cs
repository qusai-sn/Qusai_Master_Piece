using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class EventCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
