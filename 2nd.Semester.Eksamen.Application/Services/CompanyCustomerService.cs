using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Application.Services
{
    public class CompanyCustomerService : ICompanyCustomerService
    {
        private readonly ICompanyCustomerRepository _customerRepository;
        public CompanyCustomerService(ICompanyCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        





        public async Task CreateCompanyCustomerAsync(CompanyCustomerDTO DTO)
        {
            //Checks if phonenumber already exists in database. If it doesn't already exist, it continues creating customer.
            bool PhoneAlreadyExists = await _customerRepository.PhoneAlreadyExistsAsync(DTO.PhoneNumber);
            if (PhoneAlreadyExists)
            {
                throw new Exception("Telefonnummer findes allerede!");
            }
            Address address = new Address(DTO.City, DTO.PostalCode, DTO.StreetName, DTO.HouseNumber);
            //Creates Company Customer
            CompanyCustomer companyCustomer = new CompanyCustomer(DTO.Name, DTO.CVRNumber, address, DTO.PhoneNumber, DTO.Email);
            await _customerRepository.CreateNewAsync(companyCustomer);

        }
    }
}
