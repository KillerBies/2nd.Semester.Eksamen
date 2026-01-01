using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
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

        public async Task CreateNewAsync(Customer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                //Checks if phonenumber already exists in database. If it doesn't already exist, it continues creating customer.
                if (await _context.Customers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber)) throw new Exception("Telefonnummer findes allerede!");
                customer.Guid = Guid.NewGuid();
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
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
        public async Task<Customer?> GetByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers
                .Include(c => c.BookingHistory)
                    .ThenInclude(b => b.Treatments)
                        .ThenInclude(tb => tb.Treatment)
                .Include(c => c.PunchCards)
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Guid == guid);
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber) => await GetByPhoneAsync(phoneNumber);

        public async Task<Customer?> GetByPhoneAsync(string phoneNumber)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.Include(c => c.Address)
                                           .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<List<Customer?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.Include(c => c.Address).Include(c=>c.BookingHistory).ToListAsync();
        }

        public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        {
            throw new NotImplementedException(); // You can implement filtering later
        }

        // ================= UPDATE =================
        public async Task UpdateCustomerAsync(Customer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var adressToUpdate = await _context.Adresses.FindAsync(Customer.AddressId);
                adressToUpdate.UpdateStreetName(Customer.Address.StreetName);
                adressToUpdate.UpdatePostalCode(Customer.Address.PostalCode);
                adressToUpdate.UpdateHouseNumber(Customer.Address.HouseNumber);
                adressToUpdate.UpdateCity(Customer.Address.City);
                if (Customer is PrivateCustomer pc)
                {
                    PrivateCustomer customerToUpDate = (PrivateCustomer) await _context.Customers.FindAsync(Customer.Id);
                    customerToUpDate.TrySetPhoneNumber(pc.PhoneNumber);
                    customerToUpDate.TrySetLastName(pc.Name, pc.LastName);
                    customerToUpDate.SetBirthDate(pc.BirthDate,(DateTime.Today.Year - pc.BirthDate.Year));
                    customerToUpDate.Email = pc.Email;
                    customerToUpDate.Gender = pc.Gender;
                    customerToUpDate.Notes = pc.Notes;
                    customerToUpDate.SaveAsCustomer = pc.SaveAsCustomer;
                }
                else if(Customer is CompanyCustomer cc)
                {
                    CompanyCustomer customerToUpDate = (CompanyCustomer) await _context.Customers.FindAsync(Customer.Id);
                    customerToUpDate.TrySetPhoneNumber(cc.PhoneNumber);
                    customerToUpDate.Email = cc.Email;
                    customerToUpDate.Notes = cc.Notes;
                    customerToUpDate.TrySetCVRNumber(cc.CVRNumber);
                    customerToUpDate.SaveAsCustomer = cc.SaveAsCustomer;
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));

            var _context = await _factory.CreateDbContextAsync();

            // Attach the entity to the new context and mark Status as modified
            _context.Bookings.Attach(booking);
            _context.Entry(booking).Property(b => b.Status).IsModified = true;

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
        public async Task DeleteCustomerAsync(Customer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                Customer trackedcustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == Customer.Id);
                _context.Customers.Remove(trackedcustomer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteByIdDbAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }




        }
        public async Task DeleteAsync(Customer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        public async Task<Order?> GetOrderByBookingIdAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Orders.Include(o => o.Booking).FirstOrDefaultAsync(o => o.BookingId == bookingId);
        }

        // ================= BOOKINGS =================
        public async Task<Booking?> GetBookingWithTreatmentsAndProductsAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();

            var booking = await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment)
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.TreatmentBookingProducts)
                        .ThenInclude(tbp => tbp.Product)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            return booking;
        }



        public async Task<Booking?> GetNextPendingBookingAsync(int customerId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment)
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.TreatmentBookingProducts)
                        .ThenInclude(tbp => tbp.Product)
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
        public async Task SetBookingStatusAsync(int bookingId, BookingStatus status)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return;
            booking.Status = status;
            await _context.SaveChangesAsync();
        }

    }
}
