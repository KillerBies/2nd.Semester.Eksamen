using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces
{
    public interface ICustomerRepository
    {
        public Task<Customer?> GetByIDAsync(int id);
        public Task<Customer?> GetByPhoneNumberAsync(string PhoneNumber);
        public Task<List<Customer?>> GetAllAsync();
        public Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter);
        public Task<Customer?> GetByGuidAsync(Guid guid);
        public Task UpdateCustomerAsync(Customer Customer);
        public Task DeleteCustomerAsync(Customer Customer);
        public Task DeleteByIdDbAsync(int id);
        public Task<Customer?> GetByPhoneAsync(string phoneNumber);
        public Task CreateNewAsync(Customer customer);
        public Task UpdateAsync(Customer customer);
        public Task DeleteAsync(Customer customer);

        public Task<Booking?> GetBookingWithTreatmentsAndProductsAsync(int bookingId);
        public Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        public Task UpdateOrderAsync(Order order);
        public Task<Booking?> GetNextPendingBookingAsync(int customerId);
        public Task UpdateBookingAsync(Booking booking);
        public Task UpdateDiscountAsync(Discount discount);
        public Task AddOrderAsync(Order order);
        public Task SetBookingStatusAsync(int bookingId, BookingStatus status);
    }
}
