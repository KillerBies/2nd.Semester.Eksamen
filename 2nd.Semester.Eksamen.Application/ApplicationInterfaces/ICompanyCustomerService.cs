using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface ICompanyCustomerService : ICustomerService
    {
        public Task<int> CreateCompanyCustomerAsync(CompanyCustomerDTO dto);
        public Task<CompanyCustomer?> GetByIDAsync(int id);
        public Task<CompanyCustomer?> GetCustomerByIdAsync(int id);

        public Task UpdateAsync(CompanyCustomer customer);
        public Task DeleteAsync(CompanyCustomer customer);

        public Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        public Task AddOrderAsync(Order order);
        public Task UpdateOrderAsync(Order order);
    }

}
