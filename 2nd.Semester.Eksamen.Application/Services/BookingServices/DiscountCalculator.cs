using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using AppDiscountResult = _2nd.Semester.Eksamen.Application.Helpers.DiscountResult;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly IDiscountService _discountService;
        private readonly ICustomerService _customerService;

        public DiscountCalculator(IDiscountService discountService, ICustomerService customerService)
        {
            _discountService = discountService;
            _customerService = customerService;
        }

        public async Task<(decimal originalTotal,
                           Discount? appliedDiscount,
                           Discount? loyaltyDiscount,
                           decimal finalTotal,
                           List<ProductDiscountInfoDTO> itemDiscounts)>
            CalculateAsync(int customerId, List<Product> products)
        {
            if (products == null || !products.Any())
                throw new Exception("No products provided for discount calculation");

            var itemDiscounts = new List<ProductDiscountInfoDTO>();
            var originalTotal = products.Sum(p => p.Price);

            var allDiscounts = await _discountService.GetAllDiscountsAsync();
            var customer = await _customerService.GetCustomerByIdAsync(customerId)
                           ?? throw new Exception("Customer not found");

            // -------- 1️ Determine loyalty discount --------
            Discount? loyaltyDiscount = null;
            var loyaltyEntity = await _discountService.GetLoyaltyDiscountForVisitsAsync(customer.NumberOfVisists);
            if (loyaltyEntity != null)
            {
                loyaltyDiscount = new Discount
                {
                    Id = loyaltyEntity.Id,
                    Name = loyaltyEntity.Name,
                    TreatmentDiscount = loyaltyEntity.TreatmentDiscount,
                    ProductDiscount = loyaltyEntity.ProductDiscount,
                    IsLoyalty = true,
                    AppliesToProduct = loyaltyEntity.AppliesToProduct,
                    AppliesToTreatment = loyaltyEntity.AppliesToTreatment
                };
            }

            // -------- 2️ Build list of valid campaign discounts --------
            var validCampaignDiscounts = new List<Discount>();
            foreach (var discount in allDiscounts.Where(d => !d.IsLoyalty))
            {
                var campaign = await _discountService.GetCampaignByDiscountIdAsync(discount.Id);
                if (campaign != null && campaign.CheckTime())
                {
                    validCampaignDiscounts.Add(discount);
                }
            }

            // -------- 3️ Parallel calculation to find best discount --------
            var discountsToCheck = validCampaignDiscounts
                                   .Concat(loyaltyDiscount != null ? new[] { loyaltyDiscount } : Array.Empty<Discount>())
                                   .ToList();

            var discountResult = new AppDiscountResult();


            await Parallel.ForEachAsync(discountsToCheck, async (discount, ct) =>
            {
                decimal totalSavings = 0;

                foreach (var product in products)
                {
                    bool isTreatment = product is Treatment;
                    bool applies = (isTreatment && discount.AppliesToTreatment) || (!isTreatment && discount.AppliesToProduct);
                    if (!applies) continue;

                    totalSavings += product.Price * discount.GetDiscountAmountFor(product);
                }

                discountResult.TryUpdate(totalSavings, discount);
            });

            var bestDiscount = discountResult.Discount;
            decimal finalTotal = 0;

            // -------- 4️ Apply best discount to each product and build DTO --------
            foreach (var product in products)
            {
                bool isTreatment = product is Treatment;
                decimal finalPrice = product.Price;
                decimal discountAmount = 0;
                string discountName = string.Empty;
                bool isLoyaltyApplied = false;

                if (bestDiscount != null)
                {
                    bool applies = (isTreatment && bestDiscount.AppliesToTreatment) || (!isTreatment && bestDiscount.AppliesToProduct);
                    if (applies)
                    {
                        discountAmount = bestDiscount.GetDiscountAmountFor(product);
                        finalPrice = product.Price * (1 - discountAmount);
                        discountName = bestDiscount.Name ?? "";
                        isLoyaltyApplied = bestDiscount.IsLoyalty;
                    }
                }

                finalTotal += finalPrice;

                itemDiscounts.Add(new ProductDiscountInfoDTO
                {
                    ProductId = product.Id,
                    ProductName = product.Name ?? "",
                    OriginalPrice = product.Price,
                    FinalPrice = finalPrice,
                    DiscountAmount = discountAmount,
                    DiscountName = discountName,
                    IsLoyalty = isLoyaltyApplied
                });
            }

            return (originalTotal, bestDiscount, loyaltyDiscount, finalTotal, itemDiscounts);
        }
    }
}
