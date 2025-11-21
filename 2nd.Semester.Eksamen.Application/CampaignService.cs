using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;

namespace _2nd.Semester.Eksamen.Application;

public class CampaignService
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IBookingRepository _bookingRepository;

    public CampaignService(ICampaignRepository campaignRepository, ICustomerRepository customerRepository, IBookingRepository bookingRepository)
    {
        _campaignRepository = campaignRepository;
        _customerRepository = customerRepository;
        _bookingRepository = bookingRepository;
    }
}