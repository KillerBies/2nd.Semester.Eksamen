using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // Midlertidlig liste til test af skabelse af employee
        private readonly List<Employee> _employees = new();

        public async Task AddAsync(Employee employee)
        {
            _employees.Add(employee);
        }
    }
}
