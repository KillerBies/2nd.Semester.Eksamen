using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repo;

        public DiscountService(IDiscountRepository repo)
        {
            _repo = repo;
        }

        // Fetch all discounts from the database
        public async Task<List<Discount>> GetAllDiscountsAsync()
        {
            return await _repo.GetAllAsync();
        }

        // Fetch the loyalty discount based on number of visits
        public async Task<LoyaltyDiscount?> GetLoyaltyDiscountForVisitsAsync(int numberOfVisits)
        {
            // Pull all loyalty discounts
            var loyaltyDiscounts = await _repo.GetLoyaltyDiscountsAsync();

            // Only consider discounts the customer actually qualifies for
            // i.e., MinimumVisits <= numberOfVisits
            return loyaltyDiscounts
                .Where(ld => numberOfVisits >= ld.MinimumVisits)
                .OrderByDescending(ld => ld.MinimumVisits)
                .FirstOrDefault();

        }

        // Get the products by ID from the DB
        public async Task<List<Product>> GetProductsByIdsAsync(List<int> ids)
        {
            return await _repo.GetByIdsAsync(ids);
        }

        public async Task<Campaign?> GetCampaignByDiscountIdAsync(int discountId)
        {
            return await _repo.GetCampaignByDiscountIdAsync(discountId);
        }


    }

}
