using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd
{
    public class ReadEmployeeUserCardsCommand
    {
        // Inject your repository/DbContext if needed (via constructor)
        private readonly IEmployeeRepository _repo;

        public ReadEmployeeUserCardsCommand(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<EmployeeUserCardDTO>> GetAllUserCardsAsync()
        {
            var employees = await _repo.GetAllAsync(); // LISTE af Employee

            return employees.Select(e => new EmployeeUserCardDTO
            {
                Id = e.Id,
                Name = e.Name,
                Type = e.Type,
                PhoneNumber = e.PhoneNumber
            }).ToList();
        }
    }
}