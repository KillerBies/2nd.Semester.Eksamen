using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<TreatmentBookingProduct> TreatmentBookingProducts { get; set; }

        //Product data
        public DbSet<Product> Products { get; set; }

        //Person data
        public DbSet<PrivateCustomer> PrivateCustomers { get; set; }
        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Adresses { get; set; }

        //Discount data
        public DbSet<PunchCard> PunchCards { get; set; }
        public DbSet<LoyaltyDiscount> LoyaltyDiscounts { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        //Schedule data
        public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }
        public DbSet<ScheduleDay> ScheduleDays { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().UseTphMappingStrategy();
            modelBuilder.Entity<Product>().UseTptMappingStrategy();

            modelBuilder.Entity<Treatment>().ToTable("Treatments");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Campaign>().ToTable("Campaigns");
            modelBuilder.Entity<LoyaltyDiscount>().ToTable("LoyaltyDiscounts");
            modelBuilder.Entity<Discount>().ToTable("Discount");

            //Booking model
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Treatments)
                .WithOne(tb => tb.Booking)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Employee>()
                .HasOne<EmployeeSchedule>(b => b.Schedule)
                .WithOne(s => s.Employee)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<EmployeeSchedule>()
                .HasMany(es => es.Days)
                .WithOne(sd => sd.Schedule)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScheduleDay>()
                .HasMany<TimeRange>(sd => sd.TimeRanges)
                .WithOne(tr => tr.ScheduleDay)
                .OnDelete(DeleteBehavior.Cascade);






            modelBuilder.Entity<Booking>()
                .HasOne<Customer>(b => b.Customer)
                .WithMany(c => c.BookingHistory)
                .OnDelete(DeleteBehavior.NoAction);

            //Campaign 
            modelBuilder.Entity<Campaign>()
                .HasMany(T => T.ProductsInCampaign);

            //Order
            modelBuilder.Entity<Order>(entity =>
            {
                // An order has many products (OrderLines)
                entity.HasMany(o => o.Products)
                      .WithOne(p => p.Order)
                      .HasForeignKey(p => p.OrderID)
                      .OnDelete(DeleteBehavior.Cascade);

                // An order belongs to one booking
                entity.HasOne(o => o.Booking)
                      .WithOne()
                      .HasForeignKey<Order>(o => o.BookingId)
                      .OnDelete(DeleteBehavior.NoAction);

                // Configure precision for totals
                entity.Property(o => o.Total).HasPrecision(18, 2);
                entity.Property(o => o.DiscountedTotal).HasPrecision(18, 2);
            });


            //Treatment booking
            modelBuilder.Entity<TreatmentBooking>()
                .HasOne(tb => tb.Employee)
                .WithMany(e => e.Appointments)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TreatmentBooking>()
                .HasMany(tb => tb.ProductsUsed)
                .WithOne(up => up.TreatmentBooking)
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
                .HasOne(e => e.Address)
                .WithOne()
                .HasForeignKey<Employee>(e => e.AddressId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Customer>()
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