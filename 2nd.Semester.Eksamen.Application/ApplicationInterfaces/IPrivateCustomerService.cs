using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IPrivateCustomerService
    {
        public Task<int> CreatePrivateCustomerAsync(PrivateCustomerDTO privateCustomerDTO);
        public Task<PrivateCustomer?> GetByIDAsync(int id);
        public Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId);
        public Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        public Task AddOrderAsync(Order order);
        public Task UpdateOrderAsync(Order order);
        public Task<Booking?> GetNextPendingBookingAsync(int customerId);
        public Task UpdateBookingAsync(Booking booking);
        public Task UpdateDiscountAsync(Discount discount);
        public Task<PrivateCustomer?> GetCustomerByIdAsync(int customerId);

    }
}
