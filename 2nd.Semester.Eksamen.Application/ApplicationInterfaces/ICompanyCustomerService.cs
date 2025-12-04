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
        Task<int> CreateCompanyCustomerAsync(CompanyCustomerDTO dto);
        Task<CompanyCustomer?> GetByIDAsync(int id);
        Task<CompanyCustomer?> GetCustomerByIdAsync(int id);

        Task UpdateAsync(CompanyCustomer customer);
        Task DeleteAsync(CompanyCustomer customer);

        Task<Order?> GetOrderByBookingIdAsync(int bookingId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }

}
