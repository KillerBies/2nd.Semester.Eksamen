using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class CustomerUpdateService : ICustomerUpdateService
    {
        public ICustomerRepository _customerRepository;
        public DTO_to_Domain _toDomainAdapter;
        public CustomerUpdateService(ICustomerRepository customerRepository, DTO_to_Domain dTO_To_Domain)
        {
            _customerRepository = customerRepository;
            _toDomainAdapter = dTO_To_Domain;
        }
        public async Task UpdateCustomer(CustomerDTO customer)
        {
            var Customer = await _toDomainAdapter.DTOCustomerToDomain(customer.id);
            Customer.Address.UpdateStreetName(customer.StreetName);
            Customer.Address.UpdatePostalCode(customer.PostalCode);
            Customer.Address.UpdateHouseNumber(customer.HouseNumber);
            Customer.Address.UpdateCity(customer.City);
            if (Customer is PrivateCustomer pc)
            {
                PrivateCustomerDTO updatedCustomer = (PrivateCustomerDTO)customer;
                pc.TrySetPhoneNumber(updatedCustomer.PhoneNumber);
                pc.TrySetLastName(updatedCustomer.Name, updatedCustomer.LastName);
                pc.SetBirthDate(updatedCustomer.Birthday, (DateTime.Today.Year - updatedCustomer.Birthday.Year));
                pc.Email = updatedCustomer.Email;
                pc.Gender = updatedCustomer.Gender;
                pc.Notes = updatedCustomer.Notes;
                await _customerRepository.UpdateCustomerAsync(pc);
            }
            else if (Customer is CompanyCustomer cc)
            {
                CompanyCustomerDTO updatedCustomer = (CompanyCustomerDTO)customer;
                cc.TrySetPhoneNumber(updatedCustomer.PhoneNumber);
                cc.Email = updatedCustomer.Email;
                cc.Notes = updatedCustomer.Notes;
                cc.TrySetCVRNumber(updatedCustomer.CVRNumber);
                await _customerRepository.UpdateCustomerAsync(cc);
            }
        }
    }
}
