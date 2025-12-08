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
        //Read
        public Task<Employee?> GetByIDAsync(int id);
        public Task<IEnumerable<Employee?>> GetAllAsync();
        public Task<IEnumerable<Employee?>> GetByFilterAsync(Filter filter);
        public Task<IEnumerable<Employee?>> GetByTreatmentSpecialtiesAsync(List<string> specialties);
        public Task<List<string>> GetAllSpecialtiesAsync();

        //CUD
        public Task CreateNewAsync(Employee employee);
        public Task UpdateAsync(Employee Employee);
        public Task DeleteAsync(Employee Employee);
    }
}
