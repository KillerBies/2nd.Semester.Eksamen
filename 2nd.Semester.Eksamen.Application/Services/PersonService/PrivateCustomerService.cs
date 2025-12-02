using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class PrivateCustomerService : IPrivateCustomerService
    {

        private readonly IPrivateCustomerRepository _customerRepository;
        public PrivateCustomerService(IPrivateCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> CreatePrivateCustomerAsync(PrivateCustomerDTO DTO)
        {
            //Creates Address for private customer
            Address address = new Address(DTO.City, DTO.PostalCode, DTO.StreetName, DTO.HouseNumber);
            //Creates Private Customer
            PrivateCustomer privateCustomer = new PrivateCustomer(DTO.LastName, DTO.Gender, DTO.Birthday, DTO.Name, address, DTO.PhoneNumber, DTO.Email, DTO.Notes, DTO.SaveAsCustomer);
            await _customerRepository.CreateNewAsync(privateCustomer);
            return (await _customerRepository.GetByPhoneAsync(DTO.PhoneNumber)).Id;
        }


    }


}