using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface ICustomerService
    {
        public Task<Customer?> GetCustomerByIdAsync(int customerId);
        public Task<Customer?> GetByIDAsync(int id);
        public Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId);
        public Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        public Task AddOrderAsync(Order order);
        public Task UpdateOrderAsync(Order order);
        public Task UpdateAsync(Customer customer);
        public Task DeleteAsync(Customer customer);
        public Task DeleteByIdAsync(int id);
        public Task<Booking?> GetNextPendingBookingAsync(int customerId);
        public Task UpdateBookingAsync(Booking booking);
        public Task UpdateDiscountAsync(Discount discount);
        public Task<List<CustomerDTO?>> GetAllCustomersAsDTO();
    }
}
