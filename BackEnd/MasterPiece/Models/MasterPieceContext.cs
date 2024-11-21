using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Models;

public partial class MasterPieceContext : DbContext
{
    public MasterPieceContext()
    {
    }

    public MasterPieceContext(DbContextOptions<MasterPieceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCategory> EventCategories { get; set; }

    public virtual DbSet<EventSession> EventSessions { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Speaker> Speakers { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-QJHUPSA;Database=MasterPiece;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__9DE44554B0A9244F");

            entity.Property(e => e.AnnouncementId).HasColumnName("AnnouncementID");
            entity.Property(e => e.DateSent)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");

            entity.HasOne(d => d.Event).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Announcem__Event__1332DBDC");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK__Announcem__Organ__14270015");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Email);  // Assuming Email is the primary key
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Message).HasMaxLength(500);
        });


        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C870423944D4");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.BannerUrl)
                .HasMaxLength(255)
                .HasColumnName("BannerURL");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.LocationUrl)
                .HasMaxLength(255)
                .HasColumnName("Location_URL");
            entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(255)
                .HasColumnName("ThumbnailURL");
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Category).WithMany(p => p.Events)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Events__Category__787EE5A0");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK__Events__Organize__797309D9");

            entity.HasOne(d => d.Plan).WithMany(p => p.Events)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__Events__PlanID__7B5B524B");

            entity.HasOne(d => d.Type).WithMany(p => p.Events)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Events__TypeID__7A672E12");

            entity.HasMany(d => d.Speakers).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventSpeaker",
                    r => r.HasOne<Speaker>().WithMany()
                        .HasForeignKey("SpeakerId")
                        .HasConstraintName("FK__EventSpea__Speak__19DFD96B"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK__EventSpea__Event__18EBB532"),
                    j =>
                    {
                        j.HasKey("EventId", "SpeakerId").HasName("PK__EventSpe__FEDABD0329EBDC89");
                        j.ToTable("EventSpeakers");
                        j.IndexerProperty<int>("EventId").HasColumnName("EventID");
                        j.IndexerProperty<int>("SpeakerId").HasColumnName("SpeakerID");
                    });
        });

        modelBuilder.Entity<EventCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__EventCat__19093A2B9D7A5AC3");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<EventSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__EventSes__C9F49270FEE8D5B9");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.SessionTime).HasMaxLength(50);
            entity.Property(e => e.SessionTitle).HasMaxLength(255);
            entity.Property(e => e.SessionType).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.EventSessions)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EventSess__Event__0F624AF8");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__EventTyp__516F0395BAD35E79");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.TypeName).HasMaxLength(255);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58E6EB7B76");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .HasColumnName("TransactionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Payments)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Payments__EventI__05D8E0BE");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payments__UserID__06CD04F7");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1D37132C7B8");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.CardNumber).HasMaxLength(255);
            entity.Property(e => e.CardholderName).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Cvv)
                .HasMaxLength(50)
                .HasColumnName("CVV");
            entity.Property(e => e.ExpirationDate).HasMaxLength(50);

            //entity.HasOne(d => d.User).WithMany(p => p.PaymentMethods)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__PaymentMe__UserI__2B0A656D");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__755C22D7804F9AFF");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.CommissionAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PlanName).HasMaxLength(255);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEF3D2B030");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Reviews__EventID__0B91BA14");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__0C85DE4D");
        });

        modelBuilder.Entity<Speaker>(entity =>
        {
            entity.HasKey(e => e.SpeakerId).HasName("PK__Speakers__79E75739A5D25CCC");

            entity.Property(e => e.SpeakerId).HasColumnName("SpeakerID");
            entity.Property(e => e.FacebookUrl)
                .HasMaxLength(255)
                .HasColumnName("FacebookURL");
            entity.Property(e => e.InstagramUrl)
                .HasMaxLength(255)
                .HasColumnName("InstagramURL");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ProfileImageURL");
            entity.Property(e => e.Role).HasMaxLength(255);
            entity.Property(e => e.TwitterUrl)
                .HasMaxLength(255)
                .HasColumnName("TwitterURL");
            entity.Property(e => e.WhatsAppUrl)
                .HasMaxLength(255)
                .HasColumnName("WhatsAppURL");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC62758D1A488");

            entity.HasIndex(e => e.Qrcode, "UQ__Tickets__5B869AD91326EE4D").IsUnique();

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.DonationLimi).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Qrcode)
                .HasMaxLength(255)
                .HasColumnName("QRCode");
            entity.Property(e => e.TicketType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Tickets__EventID__01142BA1");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Tickets__UserID__02084FDA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC85879993");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D867F98B").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.IsOrganizer).HasDefaultValue(false);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Salt).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
