using Microsoft.EntityFrameworkCore;
using RentCar_API.Models;

namespace RentCar_API.Data
{
    public class RentCarDbContext : DbContext
    {
        public RentCarDbContext(DbContextOptions<RentCarDbContext> options) : base(options)
        {
            
        }

        public required DbSet<LtPayment> Payments {get; set;}
        public required DbSet<MsCar> Cars {get; set;}
        public required DbSet<MsCarImages> CarImages {get; set;}
        public required DbSet<MsCustomer> Customers {get; set;}
        public required DbSet<MsEmployee> Employees {get; set;}
        public required DbSet<TrMaintenance> Maintenances {get; set;}
        public required DbSet<TrRental> Rentals {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MsCarImages>()
                .HasOne(img => img.Car)
                .WithMany(car => car.CarImages)
                .HasForeignKey(img => img.Car_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LtPayment>()
                .HasOne(payment => payment.Rental)
                .WithOne(rental => rental.Payment)
                .HasForeignKey<LtPayment>(payment => payment.Rental_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrRental>()
                .HasOne(rent => rent.Customer)
                .WithMany(cust => cust.Rentals)
                .HasForeignKey(rent => rent.Customer_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrRental>()
                .HasOne(rent => rent.Car)
                .WithMany(car => car.Rentals)
                .HasForeignKey(rent => rent.Car_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrMaintenance>()
                .HasOne(main => main.Car)
                .WithMany(car => car.Maintenances)
                .HasForeignKey(main => main.Car_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrMaintenance>()
                .HasOne(main => main.Employee)
                .WithMany(em => em.Maintenances)
                .HasForeignKey(main => main.Employee_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MsCar>()
                .Property(c => c.Car_id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<MsCarImages>()
                .Property(c => c.Image_car_id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<LtPayment>()
                .Property(c => c.Payment_id)
                .HasDefaultValueSql("NEWID()");
            
            modelBuilder.Entity<MsCustomer>()
                .Property(c => c.Customer_id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<MsEmployee>()
                .Property(c => c.Employee_id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<TrMaintenance>()
                .Property(c => c.Maintenance_id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<TrRental>()
                .Property(c => c.Rental_id)
                .HasDefaultValueSql("NEWID()");
            
        }

    }
}