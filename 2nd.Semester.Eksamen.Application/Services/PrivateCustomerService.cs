using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
namespace _2nd.Semester.Eksamen.Application.Services
{
    public class PrivateCustomerService : IPrivateCustomerService
    { 
      
       private readonly IPrivateCustomerRepository _customerRepository;
       public PrivateCustomerService(IPrivateCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreatePrivateCustomerAsync(PrivateCustomerDTO DTO)
        {   //Creates Address for private customer
           Address address = new Address(DTO.City, DTO.PostalCode, DTO.StreetName, DTO.HouseNumber);
            //Creates Private Customer
             PrivateCustomer privateCustomer = new PrivateCustomer(DTO.LastName, DTO.Gender, DTO.Birthday, DTO.FirstName, address, DTO.PhoneNumber, DTO.Email);
             _customerRepository.CreateNewAsync(privateCustomer); 
        }

        
        }


}