using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? EventId { get; set; }

    public int? UserId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal? Price { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User? User { get; set; }
}
