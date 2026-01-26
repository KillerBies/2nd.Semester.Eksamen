using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces
{
    public interface IEmployeeRepository
    {
        //CUD
        public Task CreateNewEmployeeAsync(Employee employee);
        public Task UpdateEmployeeAsync(Employee Employee);
        public Task DeleteEmployeeAsync(Employee Employee);
    }
}
