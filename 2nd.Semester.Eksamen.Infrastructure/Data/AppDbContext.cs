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
        public DbSet<OrderLine> OrderLines { get; set; }


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
        public DbSet<ScheduleDay> ScheduleDays { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }

        //Snapshots
        public DbSet<OrderSnapshot> OrderSnapshots { get; set; }
        public DbSet<AddressSnapshot> AddressSnapshots { get; set; }
        public DbSet<AppliedDiscountSnapshot> AppliedDiscountSnapshots { get; set; }
        public DbSet<BookingSnapshot> BookingsSnapshots { get; set; }
        public DbSet<CustomerSnapshot> CustomerSnapshots { get; set; }
        public DbSet<PrivateCustomerSnapshot> PrivateCustomerSnapshots { get; set; }
        public DbSet<CompanyCustomerSnapshot> CompanyCustomerSnapshots { get; set; }
        public DbSet<OrderLineSnapshot> OrderLinesSnapshots { get; set; }
        public DbSet<ProductSnapshot> ProductSnapshots { get; set; }
        public DbSet<TreatmentSnapshot> TreatmentSnapshots { get; set; }










        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().UseTphMappingStrategy();
            modelBuilder.Entity<Product>().UseTphMappingStrategy();
            modelBuilder.Entity<Discount>().UseTptMappingStrategy();

            modelBuilder.Entity<Campaign>().ToTable("Campaigns");
            modelBuilder.Entity<LoyaltyDiscount>().ToTable("LoyaltyDiscounts");
            modelBuilder.Entity<Discount>().ToTable("Discount");

            //Booking model
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Treatments)
                .WithOne(tb => tb.Booking)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Booking>()
                .Property(b => b.Start)
                .HasPrecision(0);
            modelBuilder.Entity<Booking>()
                .Property(b => b.End)
                .HasPrecision(0);
            modelBuilder.Entity<Booking>()
                .HasOne<Customer>(b => b.Customer)
                .WithMany(c => c.BookingHistory)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Employee>()
                .HasMany<ScheduleDay>(b => b.Schedule)
                .WithOne(s => s.Employee)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ScheduleDay>()
                .HasMany<TimeRange>(b => b.TimeRanges)
                .WithOne(s => s.ScheduleDay)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TimeRange>()
                .Property(b => b.End)
                .HasPrecision(0);
            modelBuilder.Entity<TimeRange>()
                .Property(b => b.Start)
                .HasPrecision(0);




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
                      .WithOne(b => b.Order)
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
            // TreatmentBooking TreatmentBookingProducts
            modelBuilder.Entity<TreatmentBookingProduct>()
                .HasOne(tbp => tbp.TreatmentBooking)
                .WithMany(tb => tb.TreatmentBookingProducts)
                .HasForeignKey(tbp => tbp.TreatmentBookingID)
                .OnDelete(DeleteBehavior.NoAction);

            // TreatmentBookingProduct Product
            modelBuilder.Entity<TreatmentBookingProduct>()
                .HasOne(tbp => tbp.Product)
                .WithMany()
                .HasForeignKey(tbp => tbp.ProductId)
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
            modelBuilder.Entity<Customer>()
                .Property(e => e.PointBalance)
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


            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountedPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Discount>()
                .Property(d => d.ProductDiscount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Discount>()
                .Property(d => d.TreatmentDiscount)
                .HasPrecision(18, 2);

            //SNAPSHOTS
            modelBuilder.Entity<CustomerSnapshot>().UseTphMappingStrategy();
            modelBuilder.Entity<ProductSnapshot>().UseTptMappingStrategy();
            
            modelBuilder.Entity<OrderSnapshot>()
            .HasMany(o => o.OrderLinesSnapshot)
            .WithOne(ol => ol.OrderSnapshot)
            .HasForeignKey(ol => ol.OrderSnapshotId)
            .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderSnapshot>()
                .Property(o => o.DateOfPayment)
                .HasPrecision(18, 2);
            modelBuilder.Entity<OrderSnapshot>()
                .Property(o => o.CustomDiscount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<OrderSnapshot>()
                .Property(o => o.TotalAfterDiscount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<OrderSnapshot>()
                .HasOne(o => o.BookingSnapshot)
                .WithOne(b => b.OrderSnapshot)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderSnapshot>()
                .HasOne(o => o.AppliedDiscountSnapshot)
                .WithOne(ad => ad.OrderSnapshot)
                .HasForeignKey<OrderSnapshot>(o => o.AppliedSnapshotId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AppliedDiscountSnapshot>()
                .Property(apd=>apd.TreatmentDiscount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<AppliedDiscountSnapshot>()
                .Property(apd => apd.ProductDiscount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<BookingSnapshot>()
                .HasMany(b => b.TreatmentSnapshot)
                .WithOne(t => t.BookingSnapshot)
                
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookingSnapshot>()
                .HasOne(b => b.CustomerSnapshot)
                .WithOne(c => c.BookingSnapshot)
                
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CustomerSnapshot>()
                .HasOne(c => c.AddressSnapshot)
                .WithOne(a => a.CustomerSnapshot)
                
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductSnapshot>()
                .HasMany(p => p.OrderLines)
                .WithOne(o => o.ProductSnapshot)
                .HasForeignKey(o => o.ProductSnapshotId);
            modelBuilder.Entity<ProductSnapshot>()
                .Property(ps => ps.PricePerUnit)
                .HasPrecision(18, 2);
            modelBuilder.Entity<ProductSnapshot>()
                .Property(ps => ps.DiscountedPrice)
                .HasPrecision(18, 2);
            







            base.OnModelCreating(modelBuilder);
        }
    }
}