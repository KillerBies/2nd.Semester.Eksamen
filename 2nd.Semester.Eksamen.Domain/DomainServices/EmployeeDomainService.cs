using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices
{
    public class EmployeeDomainService
    {
        private readonly IEmployeeRepository _repo;
        public EmployeeDomainService(IEmployeeRepository repo)
        {
            _repo = repo;
        }
    }
}
