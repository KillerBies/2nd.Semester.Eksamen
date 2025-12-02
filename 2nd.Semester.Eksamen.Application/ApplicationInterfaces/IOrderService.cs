using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IOrderService
    {
        Task<(decimal originalTotal, Discount? appliedDiscount, Discount? loyaltyDiscount, decimal finalTotal)>
            CalculateBestDiscountsAsync(int customerId, List<int> productIds);

        Task<List<Product>> GetProductsByIdsAsync(List<int> productIds);

    }
}
