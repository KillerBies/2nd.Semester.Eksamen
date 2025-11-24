using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        //Order data
        public DbSet<Order> Orders { get; set; }

        //Booking Data
        public DbSet<Booking> Bookings { get; set; }

        //Treatment data
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TreatmentBooking> BookedTreatments { get; set; }

        //Product data
        public DbSet<Product> Products { get; set; }

        //Person data
        public DbSet<PrivateCustomer> PrivateCustomers { get; set; }
        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Adresses { get; set; }

        //Discount data
        public DbSet<PunchCard> PunchCards { get; set; }
        public DbSet<LoyaltyDiscount> LoyaltyDiscounts { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().UseTptMappingStrategy();
            modelBuilder.Entity<Customer>().UseTptMappingStrategy();

            modelBuilder.Entity<PrivateCustomer>().ToTable("PrivateCustomers");
            modelBuilder.Entity<CompanyCustomer>().ToTable("CompanyCustomers");
            modelBuilder.Entity<Campaign>().ToTable("Campaigns");
            modelBuilder.Entity<LoyaltyDiscount>().ToTable("LoyaltyDiscounts");

            //Booking model
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Treatments)
                .WithOne(tb => tb.Booking)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne<Customer>(b => b.Customer)
                .WithMany(c => c.BookingHistory)
                .OnDelete(DeleteBehavior.NoAction);

            //Campaign 
            modelBuilder.Entity<Campaign>()
                .HasMany(T => T.ProductsInCampaign);

            //Order
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(p => p.Order)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Booking);

            //Treatment booking
            modelBuilder.Entity<TreatmentBooking>()
                .HasOne(tb => tb.Treatment);
            modelBuilder.Entity<TreatmentBooking>()
                .HasMany(tb => tb.ProductsUsed);
            modelBuilder.Entity<TreatmentBooking>()
                .HasOne(tb => tb.Employee)
                .WithMany(e => e.Appointments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.ExperienceLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .HasMany(b => b.TreatmentHistory);

            modelBuilder.Entity<PrivateCustomer>()
                .HasMany(b => b.BookingHistory);
            modelBuilder.Entity<CompanyCustomer>()
                .HasMany(b => b.BookingHistory);

            modelBuilder.Entity<Employee>()
                .Property(e => e.BasePriceMultiplier)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(o => o.DiscountedTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasPrecision(18, 2);


            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);


            modelBuilder.Entity<Discount>()
                .Property(d => d.DiscountAmount)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}