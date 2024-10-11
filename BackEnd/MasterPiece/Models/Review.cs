using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? EventId { get; set; }

    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User? User { get; set; }
}
