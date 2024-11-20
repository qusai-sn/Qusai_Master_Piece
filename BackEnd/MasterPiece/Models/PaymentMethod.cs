using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public int UserId { get; set; }

    public string? CardholderName { get; set; }

    public string? CardNumber { get; set; }

    public string? ExpirationDate { get; set; }

    public string? Cvv { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
