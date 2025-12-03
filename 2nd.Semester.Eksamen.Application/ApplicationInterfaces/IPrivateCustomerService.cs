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
        //Task DeleteAsync(PrivateCustomer customer);
        //Task<PrivateCustomer?> GetByIDAsync(int customerId);
        //Task UpdateAsync(PrivateCustomer customer);
        //Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId);
        //Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        //Task AddOrderAsync(Order order);
        //Task UpdateOrderAsync(Order order);
        //Task<Booking?> GetNextPendingBookingAsync(int id);
        //Task UpdateBookingAsync(Booking booking);
        //Task UpdateDiscountAsync(Discount appliedDiscount);
        Task<PrivateCustomer?> GetByIDAsync(int id);
        Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId);
        Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<Booking?> GetNextPendingBookingAsync(int customerId);
        Task UpdateBookingAsync(Booking booking);
        Task UpdateDiscountAsync(Discount discount);
        Task<PrivateCustomer?> GetCustomerByIdAsync(int customerId);

    }
}
