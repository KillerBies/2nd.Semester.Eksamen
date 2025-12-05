using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
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
        private decimal originalTotal;
        private Discount? appliedDiscount;
        private Discount? bestDiscount;
        private Discount? loyaltyDiscount;
        private decimal finalTotal;

        // For Razor table
        private List<ProductDiscountInfo> itemDiscounts = new();

        [Inject] private IOrderService OrderService { get; set; } = default!;
        [Inject] public IOrderLineService OrderLineService { get; set; } = default!;
        [Inject] public ICustomerService CustomerService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            errorMessage = null;

            try
            {
                customer = await CustomerService.GetByIDAsync(id) as PrivateCustomer;
                if (customer == null) throw new Exception("Customer not found");

                var booking = await CustomerService.GetNextPendingBookingAsync(customer.Id);
                if (booking == null)
                {
                    errorMessage = "No pending booking found for this customer.";
                    return;
                }

                // Use helper to get all products/treatments
                var allItems = FlattenBookingItems(booking);
                products = allItems.Select(x => x.product).Distinct().ToList();

                if (!products.Any())
                {
                    errorMessage = "No treatments or products found for this booking.";
                    return;
                }

                (originalTotal, bestDiscount, loyaltyDiscount, finalTotal, itemDiscounts) =
                    await OrderService.CalculateBestDiscountsPerItemAsync(customer.Id, products);

                appliedDiscount = (loyaltyDiscount != null &&
                                   loyaltyDiscount.DiscountAmount > (bestDiscount?.DiscountAmount ?? 0))
                                   ? loyaltyDiscount
                                   : bestDiscount;
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

                var order = await OrderService.CreateOrUpdateOrderForBookingAsync(booking.Id);

                var allItems = FlattenBookingItems(booking);

                foreach (var (product, quantity) in allItems)
                {
                    // Use IDs only to avoid EF trying to insert Order or Product again
                    var orderLine = new OrderLine
                    {
                        OrderID = order.Id,
                        ProductId = product.Id,
                        NumberOfProducts = quantity
                    };

                    await OrderLineService.AddOrderLineAsync(orderLine);
                }

                customer.AddVisit();

                if (loyaltyDiscount != null)
                {
                    loyaltyDiscount.NumberOfUses++;
                    await CustomerService.UpdateDiscountAsync(loyaltyDiscount);
                }

                await CustomerService.UpdateAsync(customer);

                booking.Status = BookingStatus.Completed;
                await CustomerService.UpdateBookingAsync(booking);

                paymentSuccess = true;
                Console.WriteLine($"Order #{order.Id} created with {allItems.Count} order lines.");
            }
            catch (Exception ex)
            {
                errorMessage = $"Error during payment: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
            finally
            {
                isLoading = false;
            }
        }


        private List<(Product product, int quantity)> FlattenBookingItems(Booking booking)
        {
            var allItems = new List<(Product product, int quantity)>();

            // Treatments + their attached products
            if (booking.Treatments != null)
            {
                foreach (var tb in booking.Treatments)
                {
                    if (tb.Treatment != null)
                        allItems.Add((tb.Treatment, 1)); // currently sets all items to be 1 in quantity

                    if (tb.TreatmentBookingProducts != null)
                    {
                        foreach (var tbp in tb.TreatmentBookingProducts)
                        {
                            if (tbp.Product != null)
                                allItems.Add((tbp.Product, tbp.NumberOfProducts));
                        }
                    }
                }
            }

            // Optional: direct booking products
            if (booking.TreatmentBookingProducts != null)
            {
                foreach (var bp in booking.TreatmentBookingProducts)
                {
                    if (bp.Product != null)
                        allItems.Add((bp.Product, bp.NumberOfProducts));
                }
            }

            return allItems;
        }



    }
}
