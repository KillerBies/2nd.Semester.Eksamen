using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerByIdAsync(int customerId);
    }
}
