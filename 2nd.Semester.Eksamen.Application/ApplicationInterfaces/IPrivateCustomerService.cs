using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IPrivateCustomerService : ICustomerService
    {
        public Task<int> CreatePrivateCustomerAsync(PrivateCustomerDTO privateCustomerDTO);
        Task DeleteAsync(PrivateCustomer customer);
        Task<PrivateCustomer?> GetByIDAsync(int customerId);
        Task UpdateAsync(PrivateCustomer customer);
    }
}
