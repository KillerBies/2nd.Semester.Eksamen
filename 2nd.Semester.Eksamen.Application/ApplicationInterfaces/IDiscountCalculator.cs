using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IDiscountCalculator
    {
        Task<(decimal originalTotal,
               Discount? appliedDiscount,
               Discount? loyaltyDiscount,
               decimal finalTotal,
               List<ProductDiscountInfoDTO> itemDiscounts)>
               CalculateAsync(int customerId, List<Product> products);
    }
}
