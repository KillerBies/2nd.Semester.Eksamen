using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.CustomerRepositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CustomerRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }

        // ================= CREATE =================
        public async Task CreateNewCustomerAsync(Customer customer) => await CreateNewAsync(customer);

        public async Task CreateNewAsync(Customer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        // ================= READ =================
        public async Task<Customer?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers
                .Include(c => c.BookingHistory)
                    .ThenInclude(b => b.Treatments)
                        .ThenInclude(tb => tb.Treatment)
                .Include(c => c.PunchCards)
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber) => await GetByPhoneAsync(phoneNumber);

        public async Task<Customer?> GetByPhoneAsync(string phoneNumber)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.Include(c => c.Address)
                                           .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<Customer?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.Include(c => c.Address).ToListAsync();
        }

        public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        {
            throw new NotImplementedException(); // You can implement filtering later
        }

        // ================= UPDATE =================
        public async Task UpdateCustomerAsync(Customer customer) => await UpdateAsync(customer);

        public async Task UpdateAsync(Customer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDiscountAsync(Discount discount)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        // ================= DELETE =================
        public async Task DeleteCustomerAsync(Customer customer) => await DeleteAsync(customer);

        public async Task DeleteAsync(Customer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        // ================= ORDERS =================
        // In PrivateCustomerRepository
        //public async Task AddOrderAsync(Order order, int bookingId)
        //{
        //    // Load the booking (with customer and treatments) from the DB
        //    var _context = await _factory.CreateDbContextAsync();
        //    var booking = await _context.Bookings
        //        .Include(b => b.Customer)
        //        .Include(b => b.Treatments)
        //        .ThenInclude(bt => bt.Treatment)
        //        .FirstOrDefaultAsync(b => b.Id == bookingId)
        //        ?? throw new Exception("Booking not found");

        //    order.Booking = booking;

        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();
        //}



        public async Task<Order?> GetOrderByBookingIdAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Orders.FirstOrDefaultAsync(o => o.BookingId == bookingId);
        }

        // ================= BOOKINGS =================
        public async Task<Booking?> GetBookingWithTreatmentsAndTreatmentAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<Booking?> GetNextPendingBookingAsync(int customerId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment)
                .Include(b => b.Customer)
                .Where(b => b.Customer.Id == customerId && b.Status == BookingStatus.Pending)
                .OrderBy(b => b.Start)
                .FirstOrDefaultAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            var _context = await _factory.CreateDbContextAsync();
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
