using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices.PersonService
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
