using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        //Repository for Employees. 
        public Task<Employee?> GetByIDAsync(int id);
        public Task<IEnumerable<Employee?>> GetAllAsync();
        public Task<IEnumerable<Employee>> GetAllUserCardsAsync();
        public Task<IEnumerable<Employee?>> GetByFilterAsync(Filter filter);
        public Task<IEnumerable<Employee?>> GetBySpecialtyAsync(string Category);
        public Task CreateNewAsync(Employee Employee); // Create employee
        public Task UpdateAsync(Employee Employee);
        public Task DeleteAsync(Employee Employee);
        public Task SaveChangesAsync();
    }
}
