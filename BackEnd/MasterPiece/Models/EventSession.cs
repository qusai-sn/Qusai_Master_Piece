using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class EventSession
{
    public int SessionId { get; set; }

    public int? EventId { get; set; }

    public string? SessionTime { get; set; }

    public string? SessionTitle { get; set; }

    public string? SessionDescription { get; set; }

    public string? SessionType { get; set; }

    public virtual Event? Event { get; set; }
}
