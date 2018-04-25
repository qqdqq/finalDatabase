using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ThemeParkDatabase.Models
{
    public partial class ThemeParkDatabaseContext : DbContext
    {
        public  DbSet<Attraction> Attraction { get; set; }
        public  DbSet<AttractionType> AttractionType { get; set; }
        public  DbSet<AttractionVisit> AttractionVisit { get; set; }
        public  DbSet<DailyParkReport> DailyParkReport { get; set; }
        public  DbSet<DeletionRequest> DeletionRequest { get; set; }
        public  DbSet<Department> Department { get; set; }
        public  DbSet<Employee> Employee { get; set; }
        public  DbSet<Location> Location { get; set; }
        public  DbSet<MaintenanceRequest> MaintenanceRequest { get; set; }
        public  DbSet<ParkInfomation> ParkInfomation { get; set; }
        public  DbSet<Ticket> Ticket { get; set; }
        public  DbSet<TicketType> TicketType { get; set; }
        public  DbSet<Vendor> Vendor { get; set; }
        public  DbSet<VendorSalesReport> VendorSalesReport { get; set; }
        public  DbSet<VendorType> VendorType { get; set; }
        public  DbSet<Visitor> Visitor { get; set; }
        public  DbSet<MaintenanceAudit> MaintenanceAudit { get; set; }

        public ThemeParkDatabaseContext(DbContextOptions<ThemeParkDatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attraction>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AttractionType)
                    .WithMany(p => p.Attraction)
                    .HasForeignKey(d => d.AttractionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attraction_AttractionType");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Attraction)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attraction_Location");
            });

            modelBuilder.Entity<AttractionType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AttractionVisit>(entity =>
            {
                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.AttractionVisit)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AttractionVisit_Attraction");
            });

            modelBuilder.Entity<DailyParkReport>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<DeletionRequest>(entity =>
            {
                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TimeRequested).HasColumnType("datetime");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleInitial).HasColumnType("nchar(1)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department1");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.Property(e => e.CurrentStatus).IsRequired();

                entity.Property(e => e.DateRequested).HasColumnType("datetime");

                entity.Property(e => e.DateResolved).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.MaintenanceRequest)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaintenanceRequest_Attraction");
            });

            modelBuilder.Entity<ParkInfomation>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasColumnType("nchar(10)");

                entity.Property(e => e.State).HasMaxLength(20);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.RedeemedDate).HasColumnType("date");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TicketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_TicketType");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.VisitorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Ticket_Visitor");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Vendor)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vendor_Location");

                entity.HasOne(d => d.VendorType)
                    .WithMany(p => p.Vendor)
                    .HasForeignKey(d => d.VendorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vendor_VendorType");
            });

            modelBuilder.Entity<VendorSalesReport>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorSalesReport)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_VendorSalesReport_Vendor");
            });

            modelBuilder.Entity<VendorType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleInitial).HasMaxLength(1);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
            });
        }
    }
}
