using _2nd.Semester.Eksamen.Domain.DomainInterfaces.PersonInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Exceptions;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices.PersonService
{
    public class CustomerFactoryDomainService : ICustomerFactoryDomainService
    {
        private readonly ICustomerRepository _repo;
        private readonly ICustomerQueryRepository _queryRepo;
        public CustomerFactoryDomainService(ICustomerQueryRepository customerQueryRepository, ICustomerRepository customerRepository) 
        {
            _repo = customerRepository;
            _queryRepo = customerQueryRepository;
        }
        async Task ICustomerFactoryDomainService.CreateCustomerAsync(Customer customer)
        {
            if ((await _queryRepo.GetByPhoneAsync(customer.PhoneNumber) != null))
                throw new DomainException("En kunde har allerede dette telefon nummer");
            await _repo.CreateNewAsync(customer);
        }
    }
}
