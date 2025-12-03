using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.CustomerRepositories
{
    public class CompanyCustomerRepository : ICompanyCustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CompanyCustomerRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<CompanyCustomer?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.ToListAsync();
        }
        public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }

        public async Task<CompanyCustomer?> GetByPhoneAsync(string phoneNumber)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task CreateNewAsync(CompanyCustomer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                //Checks if phonenumber already exists in database. If it doesn't already exist, it continues creating customer.
                if (await _context.CompanyCustomers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber)) throw new Exception("Telefonnummer findes allerede!");
                await _context.CompanyCustomers.AddAsync(customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<bool> PhoneAlreadyExistsAsync(string phone)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.AnyAsync(c => c.PhoneNumber == phone);
        }
        public async Task UpdateAsync(CompanyCustomer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.CompanyCustomers.Update(Customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(CompanyCustomer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.CompanyCustomers.Remove(Customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        // ORDERS
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

        //public async Task<Order?> GetOrderByBookingIdAsync(int bookingId)
        //{
        //    var context = await _factory.CreateDbContextAsync();

        //    return await context.Orders
        //        .Include(o => o.Booking)
        //        .ThenInclude(b => b.Customer)
        //        .FirstOrDefaultAsync(o => o.BookingId == bookingId);
        //}


        // BOOKINGS
        public async Task<Booking?> GetBookingWithTreatmentsAndTreatmentAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        // UPDATE ORDERS
        public async Task UpdateOrderAsync(Order order)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetNextPendingBookingAsync(int customerId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment)
                .Where(b => b.Customer.Id == customerId && b.Status == BookingStatus.Pending)
                .OrderBy(b => b.Start)
                .FirstOrDefaultAsync();
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

    }
}
