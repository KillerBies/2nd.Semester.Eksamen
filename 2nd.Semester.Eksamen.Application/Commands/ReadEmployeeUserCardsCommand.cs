using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public class ReadEmployeeUserCardsCommand
    {
        // Inject your repository/DbContext if needed (via constructor)
        private readonly IEmployeeRepository _repo;

        public ReadEmployeeUserCardsCommand(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EmployeeUserCardModel>> ExecuteAsync()
        {
            // Just call the repository method that returns the DTOs
            var userCards = await _repo.GetAllUserCards();
            return userCards;
        }
    }
}