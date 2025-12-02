using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;

namespace _2nd.Semester.Eksamen.Pages.PaymentPages
{
    public partial class FinalizePayment : ComponentBase
    {
        [Parameter] public int CustomerId { get; set; }

        private bool isLoading = true;
        private PrivateCustomer? customer;
        private List<Product> products = new();
        private decimal originalTotal;
        private Discount? appliedDiscount;
        private Discount? bestDiscount;
        private Discount? loyaltyDiscount;
        private decimal finalTotal;

        [Inject] private IOrderService OrderService { get; set; } = default!;
        [Inject] private IPrivateCustomerService CustomerService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            // For testing, hardcode some product IDs if needed
            int CustomerId = 1;
            var productIds = new List<int> { 1 };

            var result = await OrderService.CalculateBestDiscountsAsync(CustomerId, productIds);
            (originalTotal, bestDiscount, loyaltyDiscount, finalTotal) = result;

            appliedDiscount = (loyaltyDiscount != null &&
                   loyaltyDiscount.DiscountAmount > (bestDiscount?.DiscountAmount ?? 0))
                   ? loyaltyDiscount
                   : bestDiscount;


            if (customer is PrivateCustomer pc)
            {
                pc.AddVisit();
                await CustomerService.UpdateAsync(pc);
            }
            //else if (customer is CompanyCustomer cc) // Implement Company Customer later
            //{
                
            //}


            // Load products
            products = await OrderService.GetProductsByIdsAsync(productIds);

            isLoading = false;

        }

        private async Task FinalizePaymentAsync()
        {
            if (customer == null) return;

            if (!customer.SaveAsCustomer)
            {
                await CustomerService.DeleteAsync(customer);
            }
            else
            {
                customer.AddVisit();
                await CustomerService.UpdateAsync(customer);
            }

            // TODO: Create order in DB using finalTotal, bestDiscount, loyaltyDiscount
        }
    }
}
