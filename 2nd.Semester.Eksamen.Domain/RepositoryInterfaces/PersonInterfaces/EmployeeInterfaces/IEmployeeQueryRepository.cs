using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces
{
    public interface IEmployeeQueryRepository
    {
        public Task<Employee?> GetEmployeeByPhoneNumberAsync(string phone);
        public Task<Employee?> GetEmployeeByIDAsync(Guid id);
        public Task<IEnumerable<Employee>?> GetAllEmployeesAsync();
        public Task<List<string>> GetAllDistinctEmployeeSpecialtiesAsync();
    }
}
