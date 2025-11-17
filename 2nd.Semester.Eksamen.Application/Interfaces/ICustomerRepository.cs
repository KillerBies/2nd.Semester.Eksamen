using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Application.Interfaces
{
    public interface ICustomerRepository
    {
         Task CreateNewCustomer(Customer customer);

    }
}
