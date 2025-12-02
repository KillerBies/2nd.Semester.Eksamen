using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services
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




        public async Task<List<LoyaltyDiscount>> GetAllLoyaltyDiscountsAsync()
        {
            return (await _repo.GetLoyaltyDiscountsAsync()).ToList();
        }


        // Get the products by ID from the DB
        public async Task<List<Product>> GetProductsByIdsAsync(List<int> ids)
        {
            return await _repo.GetByIdsAsync(ids);
        }

    }

}
