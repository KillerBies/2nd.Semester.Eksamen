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

        public async Task<int> CreatePrivateCustomerAsync(PrivateCustomerDTO dto)
        {
            Address address = new Address(
                dto.City,
                dto.PostalCode,
                dto.StreetName,
                dto.HouseNumber
            );

            PrivateCustomer privateCustomer = new PrivateCustomer(
                dto.LastName,
                dto.Gender,
                dto.Birthday,
                dto.Name,
                address,
                dto.PhoneNumber,
                dto.Email,
                dto.Notes,
                dto.SaveAsCustomer
            );

            await _customerRepository.CreateNewAsync(privateCustomer);

            var inserted = await _customerRepository.GetByPhoneAsync(dto.PhoneNumber);
            return inserted.Id;
        }

        public async Task<PrivateCustomer?> GetByIDAsync(int customerId)
        {
            return await _customerRepository.GetByIDAsync(customerId);
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            // For now only private exists — later this will check both repos
            return await _customerRepository.GetByIDAsync(customerId);
        }

        public async Task DeleteAsync(PrivateCustomer customer)
        {
            await _customerRepository.DeleteAsync(customer);
        }

        public async Task UpdateAsync(PrivateCustomer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }
    }



}