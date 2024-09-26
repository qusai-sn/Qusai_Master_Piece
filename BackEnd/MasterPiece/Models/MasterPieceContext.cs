﻿using System;
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

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCategory> EventCategories { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-AD5CUR3;Database=MasterPiece;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C870C5C34B6E");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.BannerUrl)
                .HasMaxLength(255)
                .HasColumnName("BannerURL");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Category).WithMany(p => p.Events)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Events__Category__5070F446");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK__Events__Organize__4F7CD00D");

            entity.HasOne(d => d.Type).WithMany(p => p.Events)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Events__TypeID__5165187F");
        });

        modelBuilder.Entity<EventCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__EventCat__19093A2B660DE3B0");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__EventTyp__516F039568A5AC0D");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.TypeName).HasMaxLength(255);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC6277DCC7CBE");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Tickets__EventID__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Tickets__UserID__5535A963");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC52036696");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
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
