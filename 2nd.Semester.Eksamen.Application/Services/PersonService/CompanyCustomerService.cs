using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;

public class CompanyCustomerService : ICustomerService
{
    private readonly ICompanyCustomerRepository _customerRepository;

    public CompanyCustomerService(ICompanyCustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer?> GetByIDAsync(int id) => await _customerRepository.GetByIDAsync(id);
    public async Task<Customer?> GetCustomerByIdAsync(int customerId) => await _customerRepository.GetByIDAsync(customerId);

    public async Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId)
        => await _customerRepository.GetBookingWithTreatmentsAndTreatmentAsync(bookingId);

    //public async Task<Order?> GetOrderByBookingIdAsync(int bookingId)
    //    => await _customerRepository.GetOrderByBookingIdAsync(bookingId);

    //public async Task AddOrderAsync(Order order) => await _customerRepository.AddOrderAsync(order);
    public async Task UpdateOrderAsync(Order order) => await _customerRepository.UpdateOrderAsync(order);

    public async Task UpdateAsync(Customer customer) => await _customerRepository.UpdateAsync((CompanyCustomer)customer);
    public async Task DeleteAsync(Customer customer) => await _customerRepository.DeleteAsync((CompanyCustomer)customer);

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

    public Task<Order?> GetOrderByBookingIdAsync(int bookingId)
    {
        throw new NotImplementedException();
    }
}
