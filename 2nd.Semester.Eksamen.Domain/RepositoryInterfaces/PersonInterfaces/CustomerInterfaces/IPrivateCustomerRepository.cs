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
    public interface IPrivateCustomerRepository
    {
        //Repository for Customer. 
        public Task<PrivateCustomer?> GetByIDAsync(int id);
        public Task <PrivateCustomer?> GetByPhoneAsync(string phoneNumber);
        //public Task<IEnumerable<Customer?>> GetAllAsync();
        //public Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(PrivateCustomer Customer);
        public Task<PrivateCustomer?> GetByGuidAsync(Guid guid);
        public Task<bool> PhoneAlreadyExistsAsync(string PhoneNumber);
        //public Task UpdateAsync(PrivateCustomer Customer);
        public Task DeleteAsync(PrivateCustomer Customer);
        public Task UpdateAsync(PrivateCustomer Customer);
        public Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId);
        public Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        public Task AddOrderAsync(Order order);
        public Task UpdateOrderAsync(Order order);
        public Task<Booking?> GetBookingWithTreatmentsAndTreatmentAsync(int bookingId);
        public Task<IEnumerable<Booking?>> GetBookingsForCustomerAsync(int customerId);
        public Task UpdateBookingAsync(Booking booking);
        public Task <List<Booking?>> GetBookingsByCustomerIdAsync(int customerId);
        public Task<Booking?> GetNextPendingBookingAsync(int customerId);
        public Task UpdateDiscountAsync(Discount discount);

    }
}
