using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly IPrivateCustomerService _privateCustomerService;

        public OrderService(
            IProductService productService,
            IDiscountService discountService,
            IPrivateCustomerService customerService)
        {
            _productService = productService;
            _discountService = discountService;
            _privateCustomerService = customerService;
        }


        public async Task<(decimal originalTotal, Discount? appliedDiscount, Discount? loyaltyDiscount, decimal finalTotal)>
            CalculateBestDiscountsAsync(int customerId, List<int> productIds)
        {
            // 1️. Get products + total
            var products = await _productService.GetProductsByIdsAsync(productIds);
            decimal originalTotal = products.Sum(p => p.Price);

            // 2️. Get regular discounts (non-loyalty)
            var allDiscounts = await _discountService.GetAllDiscountsAsync();
            var regularDiscounts = allDiscounts.Where(d => !d.IsLoyalty).ToList();
            var bestRegular = regularDiscounts.OrderByDescending(d => d.DiscountAmount).FirstOrDefault();

            // 3️. Get customer
            var customer = await _privateCustomerService.GetCustomerByIdAsync(customerId)
                           ?? throw new Exception("Customer not found");

            // 4️. Get eligible loyalty discount
            var loyaltyEntity = await _discountService.GetLoyaltyDiscountForVisitsAsync(customer.NumberOfVisists);
            Discount? loyaltyDiscount = null;

            if (loyaltyEntity != null)
            {
                loyaltyDiscount = new Discount
                {
                    Id = loyaltyEntity.Id,
                    Name = loyaltyEntity.DiscountType,
                    DiscountAmount = loyaltyEntity.DiscountAmount,
                    IsLoyalty = true
                };
            }

            // 5️. Pick the one to apply, loyalty only if higher %
            Discount? appliedDiscount = loyaltyDiscount != null && loyaltyDiscount.DiscountAmount > (bestRegular?.DiscountAmount ?? 0)
                ? loyaltyDiscount
                : bestRegular;

            // 6️. Calculate final total
            decimal finalTotal = originalTotal;
            if (appliedDiscount != null)
                finalTotal -= originalTotal * appliedDiscount.DiscountAmount;

            return (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal);
        }




        public Task<List<Product>> GetProductsByIdsAsync(List<int> productIds)
        {
            return _productService.GetProductsByIdsAsync(productIds);
        }

        public Task<List<Discount>> GetAllDiscountsAsync()
        {
            return _discountService.GetAllDiscountsAsync();
        }

        public async Task<PrivateCustomer?> GetCustomerByIdAsync(int customerId)
        {
            PrivateCustomer? privateCustomer = await _privateCustomerService.GetByIDAsync(customerId);
            return privateCustomer;
        }

    }


}
