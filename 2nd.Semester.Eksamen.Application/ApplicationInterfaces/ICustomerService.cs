using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<Customer?> GetByIDAsync(int id);
        Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId);
        Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        Task<Booking?> GetNextPendingBookingAsync(int customerId);
        Task UpdateBookingAsync(Booking booking);
        Task UpdateDiscountAsync(Discount discount);
    }
}
