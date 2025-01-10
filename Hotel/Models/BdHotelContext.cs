using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Models;

public partial class BdHotelContext : DbContext
{
    public BdHotelContext()
    {
    }

    public BdHotelContext(DbContextOptions<BdHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chambre> Chambres { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Sejour> Sejours { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceReservation> ServiceReservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=BD_Hotel; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chambre>(entity =>
        {
            entity.HasKey(e => e.IdChambre).HasName("PK__Chambre__FCF7257D0CCC5852");

            entity.ToTable("Chambre");

            entity.Property(e => e.IdChambre).HasColumnName("idChambre");
            entity.Property(e => e.Capacite).HasColumnName("capacite");
            entity.Property(e => e.Disponibilite)
                .HasMaxLength(50)
                .HasDefaultValue("Disponible")
                .HasColumnName("disponibilite");
            entity.Property(e => e.EtatMaintenance)
                .HasMaxLength(50)
                .HasDefaultValue("Prete")
                .HasColumnName("etatMaintenance");
            entity.Property(e => e.Tarif)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tarif");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__Client__A6A610D4BFE1548D");

            entity.ToTable("Client");

            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.PointsFidel)
                .HasDefaultValue(0)
                .HasColumnName("pointsFidel");
            entity.Property(e => e.Preference)
                .HasMaxLength(255)
                .HasColumnName("preference");
            entity.Property(e => e.Prenom)
                .HasMaxLength(100)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasMaxLength(15)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmp).HasName("PK__Employee__3F174B8B7AF8CC7D");

            entity.ToTable("Employee");

            entity.Property(e => e.IdEmp).HasColumnName("idEmp");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Poste)
                .HasMaxLength(50)
                .HasColumnName("poste");
            entity.Property(e => e.Prenom)
                .HasMaxLength(100)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasMaxLength(15)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdRes).HasName("PK__Reservat__3C8791D50AB18863");

            entity.ToTable("Reservation");

            entity.Property(e => e.IdRes).HasColumnName("idRes");
            entity.Property(e => e.DateDebut).HasColumnName("dateDebut");
            entity.Property(e => e.DateFin).HasColumnName("dateFin");
            entity.Property(e => e.Etat)
                .HasMaxLength(50)
                .HasDefaultValue("Confirmée")
                .HasColumnName("etat");
            entity.Property(e => e.IdChambre).HasColumnName("idChambre");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.Reduction)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reduction");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdChambreNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdChambre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__idCha__4316F928");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__idCli__4222D4EF");
        });

        modelBuilder.Entity<Sejour>(entity =>
        {
            entity.HasKey(e => e.IdSejour).HasName("PK__Sejour__17B6D00AB828315A");

            entity.ToTable("Sejour");

            entity.Property(e => e.IdSejour).HasColumnName("idSejour");
            entity.Property(e => e.DateDebut).HasColumnName("dateDebut");
            entity.Property(e => e.DateFin).HasColumnName("dateFin");
            entity.Property(e => e.IdChambre).HasColumnName("idChambre");
            entity.Property(e => e.IdClient).HasColumnName("idClient");

            entity.HasOne(d => d.IdChambreNavigation).WithMany(p => p.Sejours)
                .HasForeignKey(d => d.IdChambre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sejour__idChambr__45F365D3");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Sejours)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sejour__idClient__46E78A0C");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdServ).HasName("PK__Service__C5AE900A56D775AD");

            entity.ToTable("Service");

            entity.Property(e => e.IdServ).HasColumnName("idServ");
            entity.Property(e => e.Capacite).HasColumnName("capacite");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Disponibilite)
                .HasMaxLength(50)
                .HasDefaultValue("Disponible")
                .HasColumnName("disponibilite");
            entity.Property(e => e.DureeSession)
                .HasDefaultValue(2)
                .HasColumnName("dureeSession");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Tarif)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tarif");
        });

        modelBuilder.Entity<ServiceReservation>(entity =>
        {
            entity.HasKey(e => e.IdServRes).HasName("PK__ServiceR__0FF9F55F06255254");

            entity.ToTable("ServiceReservation");

            entity.Property(e => e.IdServRes).HasColumnName("idServRes");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Heure).HasColumnName("heure");
            entity.Property(e => e.IdRes).HasColumnName("idRes");
            entity.Property(e => e.IdServ).HasColumnName("idServ");
            entity.Property(e => e.Participants).HasColumnName("participants");
            entity.Property(e => e.Sessions)
                .HasDefaultValue(1)
                .HasColumnName("sessions");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdResNavigation).WithMany(p => p.ServiceReservations)
                .HasForeignKey(d => d.IdRes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceRe__idRes__4E88ABD4");

            entity.HasOne(d => d.IdServNavigation).WithMany(p => p.ServiceReservations)
                .HasForeignKey(d => d.IdServ)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceRe__idSer__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
