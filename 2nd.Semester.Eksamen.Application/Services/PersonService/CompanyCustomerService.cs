using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;

public class CompanyCustomerService : ICustomerService, ICompanyCustomerService
{
    private readonly ICompanyCustomerRepository _customerRepository;

    public CompanyCustomerService(ICompanyCustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }


    // ---- ICUSTOMERSERVICE IMPLEMENTATION ----

    async Task<Customer?> ICustomerService.GetByIDAsync(int id)
        => await _customerRepository.GetByIDAsync(id);

    async Task<Customer?> ICustomerService.GetCustomerByIdAsync(int id)
        => await _customerRepository.GetCustomerByIdAsync(id);

    async Task ICustomerService.UpdateAsync(Customer customer)
    {
        if (customer is not CompanyCustomer companyCustomer)
            throw new InvalidOperationException("Customer must be a CompanyCustomer");

        await _customerRepository.UpdateAsync(companyCustomer);
    }

    async Task ICustomerService.DeleteAsync(Customer customer)
    {
        if (customer is not CompanyCustomer companyCustomer)
            throw new InvalidOperationException("Customer must be a CompanyCustomer");

        await _customerRepository.DeleteAsync(companyCustomer);
    }

    // ---- COMPANY-SPECIFIC METHODS ----
    public Task<CompanyCustomer?> GetByIDAsync(int id)
        => _customerRepository.GetByIDAsync(id);

    public Task<CompanyCustomer?> GetCustomerByIdAsync(int id)
        => _customerRepository.GetCustomerByIdAsync(id);

    public Task UpdateAsync(CompanyCustomer customer)
        => _customerRepository.UpdateAsync(customer);

    public Task DeleteAsync(CompanyCustomer customer)
        => _customerRepository.DeleteAsync(customer);

    // ---- ORDER METHODS ----
    public async Task<Order?> GetOrderByBookingIdAsync(int bookingId)
        => await _customerRepository.GetOrderByBookingIdAsync(bookingId);
    public Task AddOrderAsync(Order order)
        => _customerRepository.AddOrderAsync(order);

    public Task UpdateOrderAsync(Order order)
        => _customerRepository.UpdateOrderAsync(order);


    // Additional company-specific method
    public async Task<int> CreateCompanyCustomerAsync(CompanyCustomerDTO dto)
    {
        var companyCustomer = new CompanyCustomer(dto.Name, dto.CVRNumber,
            new Address(dto.City, dto.PostalCode, dto.StreetName, dto.HouseNumber),
            dto.PhoneNumber, dto.Email, dto.Notes, dto.SaveAsCustomer);

        await _customerRepository.CreateNewAsync(companyCustomer);
        var inserted = await _customerRepository.GetByPhoneAsync(dto.PhoneNumber);
        return inserted.Id;
    }
    public async Task<Booking?> GetNextPendingBookingAsync(int customerId)
    {
        return await _customerRepository.GetNextPendingBookingAsync(customerId);
    }

    public async Task UpdateBookingAsync(Booking booking)
    {
        await _customerRepository.UpdateBookingAsync(booking);
    }

    public async Task UpdateDiscountAsync(Discount discount)
    {
        await _customerRepository.UpdateDiscountAsync(discount);
    }
    public async Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId)
        => await _customerRepository.GetBookingWithTreatmentsAndTreatmentAsync(bookingId);

    public async Task<List<CustomerDTO>> GetAllCustomersAsDTO()
    {
        throw new NotImplementedException();
    }
    public async Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<CustomerDTO> GetCustomerDTOById(int id)
    {
        throw new NotImplementedException();
    }
}
