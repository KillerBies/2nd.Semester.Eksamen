using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IOrderService
    {
        Task<(decimal originalTotal, Discount? appliedDiscount, Discount? loyaltyDiscount, decimal finalTotal)>
            CalculateBestDiscountsAsync(int customerId, List<int> productIds);



        Task<List<Product>> GetProductsByIdsAsync(List<int> productIds);

        Task<PrivateCustomer?> GetCustomerByIdAsync(int customerId);
    }
}
