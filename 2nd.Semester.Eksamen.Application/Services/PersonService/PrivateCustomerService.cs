using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;

public class PrivateCustomerService : IPrivateCustomerService, ICustomerService
{
    private readonly IPrivateCustomerRepository _customerRepository;

    public PrivateCustomerService(IPrivateCustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public Task<PrivateCustomer?> GetByIDAsync(int customerId)
    => _customerRepository.GetByIDAsync(customerId);


    public Task<PrivateCustomer?> GetCustomerByIdAsync(int customerId)
    => _customerRepository.GetByIDAsync(customerId);


    public Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId)
    => _customerRepository.GetBookingWithTreatmentsAndTreatmentAsync(bookingId);


    public Task<Order?> GetOrderByBookingIdAsync(int bookingId)
    => _customerRepository.GetOrderByBookingIdAsync(bookingId);


    public Task AddOrderAsync(Order order)
    => _customerRepository.AddOrderAsync(order);


    public Task UpdateOrderAsync(Order order)
    => _customerRepository.UpdateOrderAsync(order);


    public Task<Booking?> GetNextPendingBookingAsync(int customerId)
    => _customerRepository.GetNextPendingBookingAsync(customerId);


    public Task UpdateBookingAsync(Booking booking)
    => _customerRepository.UpdateBookingAsync(booking);


    public Task UpdateDiscountAsync(Discount discount)
    => _customerRepository.UpdateDiscountAsync(discount);

    //public async Task AddOrderAsync(Order order, Booking booking) => await _customerRepository.AddOrderAsync(order);

    public async Task UpdateAsync(Customer customer) => await _customerRepository.UpdateAsync((PrivateCustomer)customer);
    public async Task DeleteAsync(Customer customer) => await _customerRepository.DeleteAsync((PrivateCustomer)customer);

    // Additional private-specific method
    public async Task<int> CreatePrivateCustomerAsync(PrivateCustomerDTO dto)
    {
        var privateCustomer = new PrivateCustomer(dto.LastName, dto.Gender, dto.Birthday, dto.Name,
            new Address(dto.City, dto.PostalCode, dto.StreetName, dto.HouseNumber),
            dto.PhoneNumber, dto.Email, dto.Notes, dto.SaveAsCustomer);

        await _customerRepository.CreateNewAsync(privateCustomer);
        var inserted = await _customerRepository.GetByPhoneAsync(dto.PhoneNumber);
        return inserted.Id;
    }
}
