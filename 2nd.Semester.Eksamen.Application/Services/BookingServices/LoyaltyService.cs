using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
namespace _2nd.Semester.Eksamen.Application.Services.BookingServices;

public class LoyaltyService
{
    private readonly ILoyaltyDiscountRepository _loyaltyDiscountRepository;
    private readonly IPrivateCustomerRepository _privateCustomerRepository;
    private readonly ICompanyCustomerRepository _companyCustomerRepository;
    private readonly IBookingRepository _bookingRepository;

    public LoyaltyService(ILoyaltyDiscountRepository loyaltyDiscountRepository, IPrivateCustomerRepository privateCustomerRepository, ICompanyCustomerRepository companyCustomerRepository, IBookingRepository bookingRepository)
    {
        _loyaltyDiscountRepository = loyaltyDiscountRepository;
        _privateCustomerRepository = privateCustomerRepository;
        _companyCustomerRepository = companyCustomerRepository;
        _bookingRepository = bookingRepository;
    }



}