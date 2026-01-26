using _2nd.Semester.Eksamen.Domain.DomainInterfaces.PersonInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Exceptions;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices.PersonService
{
    public class EmployeeFactoryDomainService : IEmployeeFactoryDomainService
    {
        private readonly IEmployeeQueryRepository _queryRepo;
        private readonly IEmployeeRepository _repo;
        public EmployeeFactoryDomainService(IEmployeeRepository employeeRepository, IEmployeeQueryRepository employeeQueryRepository) 
        {
            _queryRepo = employeeQueryRepository;
            _repo = employeeRepository;
        }
        async Task IEmployeeFactoryDomainService.CreateEmployeeAsync(Employee employee)
        {
            if((await _queryRepo.GetByPhoneNumberAsync(employee.PhoneNumber)) != null)
                throw new DomainException("En medarbejder med dette telefon nummer eksistere allerede");
            await _repo.CreateNewAsync(employee);
        }
    }
}
