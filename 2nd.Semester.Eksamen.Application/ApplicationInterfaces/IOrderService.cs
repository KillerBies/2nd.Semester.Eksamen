using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IOrderService
    {

        Task<(decimal originalTotal,
              Discount? appliedDiscount,
              Discount? loyaltyDiscount,
              decimal finalTotal,
              List<ProductDiscountInfoDTO> itemDiscounts)>
            CalculateBestDiscountsPerItemAsync(int customerId, List<Product> products);

        Task<Order> CreateOrUpdateOrderForBookingAsync(int bookingId);

    }
}
