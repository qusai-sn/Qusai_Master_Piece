using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Announcement
{
    public int AnnouncementId { get; set; }

    public int? EventId { get; set; }

    public int? OrganizerId { get; set; }

    public string? Message { get; set; }

    public DateTime? DateSent { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User? Organizer { get; set; }
}
