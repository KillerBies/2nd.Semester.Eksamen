using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface ICustomerUpdateService
    {
        public Task UpdateCustomer(CustomerDTO customerDTO);
    }
}
