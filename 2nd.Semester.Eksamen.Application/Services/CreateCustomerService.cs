using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.Interfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Application.Services
{
    public class CreateCustomerService : ICreateCustomerService
    {
        
       private readonly ICustomerRepository _customerRepository;
       public CreateCustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreatePrivateCustomerAsync(PrivateCustomerDTO DTO)
        {   //Creates Address for private customer
            Address address = new Address(DTO.City, DTO.PostalCode, DTO.StreetName, DTO.HouseNumber);
            //Creates Private Customer
            PrivateCustomer privateCustomer = new PrivateCustomer(DTO.Name, address, DTO.PhoneNumber, DTO.Email, DTO.Gender, DTO.Birthday);
            await _customerRepository.CreateNewCustomerAsync(privateCustomer); 

        }

        public async Task CreateCompanyCustomerAsync(CompanyCustomerDTO DTO)
        {
            Address address = new Address(DTO.City, DTO.PostalCode, DTO.StreetName, DTO.HouseNumber);
            //Creates Company Customer
            CompanyCustomer companyCustomer = new CompanyCustomer(DTO.Name, address, DTO.PhoneNumber, DTO.Email, DTO.CVRNumber);
            await _customerRepository.CreateNewCustomerAsync(companyCustomer);

        }
    }


}