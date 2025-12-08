using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices;

public class CampaignService
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IPrivateCustomerRepository _privateCustomerRepository;
    private readonly ICompanyCustomerRepository _companyCustomerRepository;
    private readonly IBookingRepository _bookingRepository;

    public CampaignService(ICampaignRepository campaignRepository, IPrivateCustomerRepository privateCustomerRepository, ICompanyCustomerRepository companyCustomerRepository, IBookingRepository bookingRepository)
    {
        _campaignRepository = campaignRepository;
        _privateCustomerRepository = privateCustomerRepository;
        _companyCustomerRepository = companyCustomerRepository;
        _bookingRepository = bookingRepository;
    }
}