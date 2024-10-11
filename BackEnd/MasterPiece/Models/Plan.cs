using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Plan
{
    public int PlanId { get; set; }

    public string? PlanName { get; set; }

    public int? DurationInDays { get; set; }

    public decimal? CommissionAmount { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
