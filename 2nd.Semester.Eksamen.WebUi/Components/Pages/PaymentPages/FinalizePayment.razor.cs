using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using Microsoft.AspNetCore.Components;

namespace _2nd.Semester.Eksamen.Pages.PaymentPages
{
    public partial class FinalizePayment : ComponentBase
    {
        [Parameter] public int id { get; set; }

        private bool isLoading = true;
        private string? errorMessage;
        private bool paymentSuccess = false;

        private PrivateCustomer? customer;
        private List<Product> products = new();
        private List<ProductDiscountInfo> itemDiscounts = new();

        private decimal originalTotal;
        private Discount? appliedDiscount;
        private Discount? loyaltyDiscount;
        private decimal finalTotal;

        [Inject] private IOrderService OrderService { get; set; } = default!;
        [Inject] public ICustomerService CustomerService { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            errorMessage = null;

            try
            {
                customer = await CustomerService.GetByIDAsync(id) as PrivateCustomer;
                if (customer == null)
                    throw new Exception("Customer not found");

                var booking = await CustomerService.GetNextPendingBookingAsync(customer.Id);
                if (booking == null)
                {
                    errorMessage = "No pending bookings found for this customer.";
                    return;
                }

                // Extract treatment products from the booking
                products = booking.Treatments
                    .Where(tb => tb.Treatment != null)
                    .Select(tb => tb.Treatment)
                    .Cast<Product>()
                    .ToList();

                // 💥 USE THE NEW ENGINE!
                var result = await OrderService
                    .CalculateBestDiscountsPerItemAsync(customer.Id, products);

                originalTotal = result.originalTotal;
                loyaltyDiscount = result.loyaltyDiscount;
                appliedDiscount = result.appliedDiscount;
                finalTotal = result.finalTotal;
                itemDiscounts = result.itemDiscounts;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                isLoading = false;
            }
        }


        private async Task FinalizePaymentAsync()
        {
            if (customer == null) return;

            isLoading = true;
            errorMessage = null;
            paymentSuccess = false;

            try
            {
                var booking = await CustomerService.GetNextPendingBookingAsync(customer.Id);
                if (booking == null)
                {
                    errorMessage = "No pending booking found.";
                    return;
                }

                // Create or update order
                var order = await OrderService.CreateOrUpdateOrderForBookingAsync(booking.Id);

                // Update customer visit count
                customer.AddVisit();
                await CustomerService.UpdateAsync(customer);

                paymentSuccess = true;

                booking.Status = BookingStatus.Completed;
                await CustomerService.UpdateBookingAsync(booking);
            }
            catch (Exception ex)
            {
                errorMessage = $"Error creating order: {ex.Message}";
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
