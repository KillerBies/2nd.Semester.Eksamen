using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
namespace _2nd.Semester.Eksamen.Application;

public class LoyaltyService
{
    private readonly ILoyaltyDiscountRepository _loyaltyDiscountRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IBookingRepository _bookingRepository;

    public LoyaltyService(ILoyaltyDiscountRepository loyaltyDiscountRepository, ICustomerRepository customerRepository, IBookingRepository bookingRepository)
    {
        _loyaltyDiscountRepository = loyaltyDiscountRepository;
        _customerRepository = customerRepository;
        _bookingRepository = bookingRepository;
    }


}