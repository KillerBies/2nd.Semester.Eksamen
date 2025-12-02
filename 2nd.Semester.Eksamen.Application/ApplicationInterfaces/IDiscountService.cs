using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IDiscountService
    {
        Task<List<Discount>> GetAllDiscountsAsync();
        Task<List<LoyaltyDiscount>> GetAllLoyaltyDiscountsAsync();
        Task<LoyaltyDiscount?> GetLoyaltyDiscountForVisitsAsync(int numberOfVisits);

    }
}
