using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? EventId { get; set; }

    public int? UserId { get; set; }

    public string? TransactionId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User? User { get; set; }
}
