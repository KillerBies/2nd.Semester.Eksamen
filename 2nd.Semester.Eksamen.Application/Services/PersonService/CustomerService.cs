using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // --- CREATION (kept DTO helpers like before) ---
        public async Task<int> CreatePrivateCustomerAsync(PrivateCustomerDTO dto)
        {
            var address = new Address(dto.City, dto.PostalCode, dto.StreetName, dto.HouseNumber);

            var customer = new PrivateCustomer(
                dto.LastName,
                dto.Gender,
                dto.Birthday,
                dto.Name,
                address,
                dto.PhoneNumber,
                dto.Email,
                dto.Notes,
                dto.SaveAsCustomer
            );

            await _customerRepository.CreateNewAsync(customer);
            return (await _customerRepository.GetByPhoneAsync(dto.PhoneNumber))!.Id;
        }

        public async Task<int> CreateCompanyCustomerAsync(CompanyCustomerDTO dto)
        {
            var address = new Address(dto.City, dto.PostalCode, dto.StreetName, dto.HouseNumber);

            var customer = new CompanyCustomer(
                dto.Name,
                dto.CVRNumber,
                address,
                dto.PhoneNumber,
                dto.Email,
                dto.Notes,
                dto.SaveAsCustomer
            );

            await _customerRepository.CreateNewAsync(customer);
            return (await _customerRepository.GetByPhoneAsync(dto.PhoneNumber))!.Id;
        }

        // --- BASIC CUSTOMER ACCESSORS ---
        public Task<Customer?> GetCustomerByIdAsync(int customerId)
            => _customerRepository.GetByIDAsync(customerId);

        public Task<Customer?> GetByIDAsync(int id)
            => _customerRepository.GetByIDAsync(id);

        public Task<Customer?> GetByPhoneAsync(string phoneNumber)
            => _customerRepository.GetByPhoneAsync(phoneNumber);

        // --- UPDATE / DELETE ---
        public Task UpdateAsync(Customer customer)
            => _customerRepository.UpdateAsync(customer);

        public Task DeleteAsync(Customer customer)
            => _customerRepository.DeleteAsync(customer);

        // --- BOOKINGS / ORDERS ---
        public Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId)
            => _customerRepository.GetBookingWithTreatmentsAndTreatmentAsync(bookingId);

        public Task<Order?> GetOrderByBookingIdAsync(int bookingId)
            => _customerRepository.GetOrderByBookingIdAsync(bookingId);

        public Task AddOrderAsync(Order order)
            => _customerRepository.AddOrderAsync(order);

        public Task UpdateOrderAsync(Order order)
            => _customerRepository.UpdateOrderAsync(order);

        public Task<Booking?> GetNextPendingBookingAsync(int customerId)
            => _customerRepository.GetNextPendingBookingAsync(customerId);

        public Task UpdateBookingAsync(Booking booking)
            => _customerRepository.UpdateBookingAsync(booking);

        public Task UpdateDiscountAsync(Discount discount)
            => _customerRepository.UpdateDiscountAsync(discount);
    }
    //private readonly IPrivateCustomerRepository _privateRepo;
    //private readonly ICompanyCustomerRepository _companyRepo;

    //public CustomerService(IPrivateCustomerRepository privateRepo, ICompanyCustomerRepository companyRepo)
    //{
    //    _privateRepo = privateRepo;
    //    _companyRepo = companyRepo;
    //}

    //// CUSTOMER GETTERS
    //public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    //{
    //    var privateCustomer = await _privateRepo.GetByIDAsync(customerId);
    //    if (privateCustomer != null) return privateCustomer;

    //    var companyCustomer = await _companyRepo.GetByIDAsync(customerId);
    //    return companyCustomer;
    //}

    //public Task<Customer?> GetByIDAsync(int id) => GetCustomerByIdAsync(id);

    //public async Task<Customer?> GetByPhoneAsync(string phoneNumber)
    //{
    //    var privateCustomer = await _privateRepo.GetByPhoneAsync(phoneNumber);
    //    if (privateCustomer != null) return privateCustomer;

    //    var companyCustomer = await _companyRepo.GetByPhoneAsync(phoneNumber);
    //    return companyCustomer;
    //}

    //// CREATE
    //public async Task<int> CreatePrivateCustomerAsync(PrivateCustomer customer)
    //{
    //    await _privateRepo.CreateNewAsync(customer);
    //    return (await _privateRepo.GetByPhoneAsync(customer.PhoneNumber))!.Id;
    //}

    //public async Task<int> CreateCompanyCustomerAsync(CompanyCustomer customer)
    //{
    //    await _companyRepo.CreateNewAsync(customer);
    //    return (await _companyRepo.GetByPhoneAsync(customer.PhoneNumber))!.Id;
    //}

    //// UPDATE & DELETE
    //public async Task UpdateAsync(Customer customer)
    //{
    //    switch (customer)
    //    {
    //        case PrivateCustomer pc: await _privateRepo.UpdateAsync(pc); break;
    //        case CompanyCustomer cc: await _companyRepo.UpdateAsync(cc); break;
    //        default: throw new Exception("Unknown customer type");
    //    }
    //}

    //public async Task DeleteAsync(Customer customer)
    //{
    //    switch (customer)
    //    {
    //        case PrivateCustomer pc: await _privateRepo.DeleteAsync(pc); break;
    //        case CompanyCustomer cc: await _companyRepo.DeleteAsync(cc); break;
    //        default: throw new Exception("Unknown customer type");
    //    }
    //}

    //// BOOKINGS
    //public async Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId)
    //{
    //    var booking = await _privateRepo.GetBookingWithTreatmentsAndTreatmentAsync(bookingId);
    //    if (booking != null) return booking;

    //    return await _companyRepo.GetBookingWithTreatmentsAndTreatmentAsync(bookingId);
    //}

    //public async Task<Booking?> GetNextPendingBookingAsync(int customerId)
    //{
    //    var booking = await _privateRepo.GetNextPendingBookingAsync(customerId);
    //    if (booking != null) return booking;

    //    return await _companyRepo.GetNextPendingBookingAsync(customerId);
    //}

    //public async Task UpdateBookingAsync(Booking booking)
    //{
    //    if (booking.Customer is PrivateCustomer) await _privateRepo.UpdateBookingAsync(booking);
    //    else if (booking.Customer is CompanyCustomer) await _companyRepo.UpdateBookingAsync(booking);
    //    else throw new Exception("Unknown customer type on booking");
    //}

    //// ORDERS
    //public async Task<Order?> GetOrderByBookingIdAsync(int bookingId)
    //{
    //    var order = await _privateRepo.GetOrderByBookingIdAsync(bookingId);
    //    if (order != null) return order;

    //    return await _companyRepo.GetOrderByBookingIdAsync(bookingId);
    //}

    //public async Task AddOrderAsync(Order order, Booking booking)
    //{
    //    // Use the booking we already have
    //    var customer = booking.Customer ?? throw new Exception("Booking must have a customer.");

    //    // Link the booking to the order
    //    order.Booking = booking;

    //    if (customer is PrivateCustomer)
    //        await _privateRepo.AddOrderAsync(order);
    //    else if (customer is CompanyCustomer)
    //        await _companyRepo.AddOrderAsync(order);
    //    else
    //        throw new Exception("Unknown customer type on order");
    //}




    //public async Task UpdateOrderAsync(Order order)
    //{
    //    var customer = order.Booking.Customer;

    //    if (customer is PrivateCustomer)
    //        await _privateRepo.UpdateOrderAsync(order);
    //    else if (customer is CompanyCustomer)
    //        await _companyRepo.UpdateOrderAsync(order);
    //    else
    //        throw new Exception("Unknown customer type on order");
    //}


    //// DISCOUNTS
    //public async Task UpdateDiscountAsync(Discount discount)
    //{
    //    // Both repos can update discounts, but usually shared globally
    //    await _privateRepo.UpdateDiscountAsync(discount);
    //    await _companyRepo.UpdateDiscountAsync(discount);
    //}
}
