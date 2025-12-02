using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;

namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class CompanyCustomerService : ICompanyCustomerService
    {
        private readonly ICompanyCustomerRepository _customerRepository;
        public CompanyCustomerService(ICompanyCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<int> CreateCompanyCustomerAsync(CompanyCustomerDTO DTO)
        {
            Address address = new Address(DTO.City, DTO.PostalCode, DTO.StreetName, DTO.HouseNumber);
            //Creates Company Customer
            CompanyCustomer companyCustomer = new CompanyCustomer(DTO.Name, DTO.CVRNumber, address, DTO.PhoneNumber, DTO.Email, DTO.Notes, DTO.SaveAsCustomer);
            await _customerRepository.CreateNewAsync(companyCustomer);
            return (await _customerRepository.GetByPhoneAsync(DTO.PhoneNumber)).Id;
        }

        public Task DeleteAsync(CompanyCustomer customer)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyCustomer?> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CompanyCustomer customer)
        {
            throw new NotImplementedException();
        }
    }
}
