using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using ProbneKolokwium2.Models_DTOS;

namespace ProbneKolokwium2.Data;

public class BoatContext : DbContext
{
    protected BoatContext()
    {
    }

    public BoatContext(DbContextOptions options) : base(options)
    {
    }
    public virtual DbSet<BoatStandard> BoatStandards { get; set; }
    public virtual DbSet<ClientCategory> ClientCategories { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Reservation>  Reservations{ get; set; }
    public virtual DbSet<Sailboat> Sailboats { get; set; }
    public virtual DbSet<SailboatReservation> SailboatReservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoatStandard>().HasData(
            new BoatStandard() { IdBoatStandard = 1, level = 1, Name = "podstawowy" },
            new BoatStandard() { IdBoatStandard = 2, level = 2, Name = "tani" },
            new BoatStandard() { IdBoatStandard = 3, level = 3, Name = "średni" },
            new BoatStandard() { IdBoatStandard = 4, level = 4, Name = "na bogato" }
        );
        modelBuilder.Entity<ClientCategory>().HasData(
          new ClientCategory(){IdClientCategory = 1,Name = "plebs",DiscountPerc = 0},
          new ClientCategory(){IdClientCategory = 2,Name = "magnat",DiscountPerc = 40},
          new ClientCategory(){IdClientCategory = 3,Name = "średni",DiscountPerc = 5}
        );
        modelBuilder.Entity<Client>().HasData(
            new Client(){IdClient = 1,Name = "Klient",LastName = "Klientowski",Birthday = new DateTime(2024,1,1),Pesel = "0000000001",email ="aaa@aaa.com",IdClientCategory = 1},
            new Client(){IdClient = 2,Name = "Jan",LastName = "Kowalski",Birthday = new DateTime(2004,1,1),Pesel = "0000000002",email ="bbb@aaa.com",IdClientCategory = 2}

            );
        modelBuilder.Entity<Sailboat>().HasData(
            new Sailboat(){IdBoatStandard = 1,Capacity = 1000,Description = "no łódka no",IdSailboat = 1,Name = "ŁODZ",Price = 100.0},
            new Sailboat(){IdBoatStandard = 4,Capacity = 500,Description = "no zaglowka no",IdSailboat = 2,Name = "zaglowka",Price = 1000.0},
            new Sailboat(){IdBoatStandard = 3,Capacity = 100,Description = "no tankowiec no",IdSailboat = 3,Name = "tankowiec 1000",Price = 500.0}
        );
        modelBuilder.Entity<Reservation>().HasData(
            new Reservation(){IdReservation = 1,IdClient = 1,DateFrom = new DateTime(2024,6,13),DateTo = new DateTime(2024,6,23),IdBoatStandard = 3,Capacity = 50,NumOfBoats = 1,Fullfilled = false},
            new Reservation(){IdReservation = 2,IdClient = 2,DateFrom = new DateTime(2024,6,16),DateTo = new DateTime(2024,7,16),IdBoatStandard = 1,Capacity = 520,NumOfBoats = 2,Fullfilled = false}
        );
        modelBuilder.Entity<SailboatReservation>().HasData(
            new SailboatReservation() { IdSailboat = 2, IdReservation = 1 },
            new SailboatReservation() { IdSailboat = 1, IdReservation = 2 },
            new SailboatReservation() { IdSailboat = 3, IdReservation = 2 }
            );
        modelBuilder.Entity<SailboatReservation>()
            .HasOne(sr => sr.IdSailboatNavigation)
            .WithMany(s => s.Sailboat_SailboatReservations)
            .HasForeignKey(sr => sr.IdSailboat)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<SailboatReservation>()
            .HasOne(sr => sr.IdReservationNavigation)
            .WithMany(r => r.Reservation_SailboatReservation)
            .HasForeignKey(sr => sr.IdReservation)
            .OnDelete(DeleteBehavior.NoAction);
        base.OnModelCreating(modelBuilder);
    }
}