using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Speaker
{
    public int SpeakerId { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public string? Bio { get; set; }

    public string? FacebookUrl { get; set; }

    public string? TwitterUrl { get; set; }

    public string? WhatsAppUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? ProfileImageUrl { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
