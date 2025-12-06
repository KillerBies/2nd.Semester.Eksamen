using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces
{
    public interface ICompanyCustomerRepository
    {
        //Repository for Customer. 
        public Task<CompanyCustomer?> GetByIDAsync(int id);
        public Task<CompanyCustomer?> GetByPhoneAsync(string phoneNumber);
        public Task<IEnumerable<Customer?>> GetAllAsync();
        public Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(CompanyCustomer Customer);
        public Task<bool> PhoneAlreadyExistsAsync(string PhoneNumber);
        public Task UpdateAsync(CompanyCustomer Customer);
        public Task DeleteAsync(CompanyCustomer Customer);
        public Task<Booking?> GetBookingWithTreatmentsAndTreatmentAsync(int bookingId);
        public Task UpdateOrderAsync(Order order);
        public Task<Booking?> GetNextPendingBookingAsync(int customerId);
        public Task UpdateBookingAsync(Booking booking);
        public Task UpdateDiscountAsync(Discount discount);
        public Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        public Task AddOrderAsync(Order order);
        Task<CompanyCustomer?> GetCustomerByIdAsync(int id);

    }

}
