using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
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
        {
            //Checks if phonenumber already exists in database. If it doesn't already exist, it continues creating customer.
            bool PhoneAlreadyExists = await _customerRepository.PhoneAlreadyExistsAsync(DTO.PhoneNumber);
            if (PhoneAlreadyExists)
            {
                throw new Exception("Telefonnummer findes allerede!");
            }
            //Creates Address for private customer
            Address address = new Address(DTO.City, DTO.PostalCode, DTO.StreetName, DTO.HouseNumber);
            //Creates Private Customer
            PrivateCustomer privateCustomer = new PrivateCustomer(DTO.LastName, DTO.Gender, DTO.Birthday, DTO.FirstName, address, DTO.PhoneNumber, DTO.Email);
            _customerRepository.CreateNewAsync(privateCustomer);
        }


    }


}