using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            // Booking.Customer
            modelBuilder.Entity<Booking>()
                .Property(b => b.Customer)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions { WriteIndented = false }),
                    v => System.Text.Json.JsonSerializer.Deserialize<PersonSnapshot>(v, new System.Text.Json.JsonSerializerOptions())
                );

            // TreatmentBooking.ProductsUsed
            modelBuilder.Entity<TreatmentBooking>()
                .Property(t => t.ProductsUsed)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions { WriteIndented = false }),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<ProductSnapshot>>(v, new System.Text.Json.JsonSerializerOptions())
                )
                .Metadata.SetValueComparer(
                    new ValueComparer<List<ProductSnapshot>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );

            // TreatmentBooking.Employee
            modelBuilder.Entity<TreatmentBooking>()
                .Property(t => t.Employee)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions { WriteIndented = false }),
                    v => System.Text.Json.JsonSerializer.Deserialize<PersonSnapshot>(v, new System.Text.Json.JsonSerializerOptions())
                );

            // Employee.Appointments
            modelBuilder.Entity<Employee>()
                .Property(e => e.Appointments)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions { WriteIndented = false }),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<Appointment>>(v, new System.Text.Json.JsonSerializerOptions())
                )
                .Metadata.SetValueComparer(
                    new ValueComparer<List<Appointment>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );

            // Order.Products
            modelBuilder.Entity<Order>()
                .Property(o => o.Products)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<ProductSnapshot>>(v, new System.Text.Json.JsonSerializerOptions())
                )
                .Metadata.SetValueComparer(
                    new ValueComparer<List<ProductSnapshot>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );

            modelBuilder.Entity<Customer>()
                .Property(c => c.PointBalance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .Property(e => e.BasePriceMultiplier)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(o => o.DiscountedTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TreatmentSnapshot>()
                .Property(t => t.BasePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductSnapshot>()
                .Property(p => p.PricePerUnit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Discount>()
                .Property(d => d.DiscountAmount)
                .HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }
    }
}
