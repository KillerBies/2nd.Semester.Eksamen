using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Application.Adapters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly Domain_to_DTO _domain_To_DTO;
        public CustomerService(ICustomerRepository customerRepository, Domain_to_DTO domain_To_DTO)
        {
            _customerRepository = customerRepository;
            _domain_To_DTO = domain_To_DTO;
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

        public async Task DeleteByIdAsync(int id)
        { 
            await _customerRepository.DeleteByIdDbAsync(id);
        }
        // --- BOOKINGS / ORDERS ---
        public Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId)
            => _customerRepository.GetBookingWithTreatmentsAndProductsAsync(bookingId);

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

        public async Task <List<CustomerDTO?>>  GetAllCustomersAsDTO()
        {
            List<Customer?> customers = await _customerRepository.GetAllAsync();

            if (customers == null) return new List<CustomerDTO>();

            List<CustomerDTO> dtoList = new();
            foreach (Customer customer in customers)
            {
                if (customer is PrivateCustomer p)
                {
                    var privateCustomerDTO = _domain_To_DTO.PrivateCustomerToDTO(p);
                    dtoList.Add(privateCustomerDTO);
                }
                else if (customer is CompanyCustomer c)
                {
                    var companyCustomerDTO = _domain_To_DTO.BusinessCustomerToDTO(c);
                    dtoList.Add(companyCustomerDTO);
                }
            }
            return dtoList;


        }


    }

}
