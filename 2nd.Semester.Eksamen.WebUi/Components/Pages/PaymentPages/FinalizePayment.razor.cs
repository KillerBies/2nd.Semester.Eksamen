using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
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

                // Fetch all products from the booking (treatments + attached products)
                products = GetProductsFromBooking(booking);

                if (!products.Any())
                {
                    errorMessage = "No treatments or products found for this booking.";
                    return;
                }

                // Get discounts per item
                (originalTotal, bestDiscount, loyaltyDiscount, finalTotal, itemDiscounts) =
                    await OrderService.CalculateBestDiscountsPerItemAsync(customer.Id, products);

                // SLAY: Compare max of treatment/product discount instead of DiscountAmount
                decimal loyaltyValue = loyaltyDiscount != null
                    ? Math.Max(loyaltyDiscount.TreatmentDiscount, loyaltyDiscount.ProductDiscount)
                    : 0;
                decimal bestValue = bestDiscount != null
                    ? Math.Max(bestDiscount.TreatmentDiscount, bestDiscount.ProductDiscount)
                    : 0;

                appliedDiscount = (loyaltyValue > bestValue) ? loyaltyDiscount : bestDiscount;
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

                // Use flattened product IDs for order lines
                var allItems = FlattenBookingItems(booking);

                foreach (var (productId, quantity) in allItems)
                {
                    var orderLine = new OrderLine
                    {
                        OrderID = order.Id,
                        ProductId = productId, // ONLY use the ID
                        NumberOfProducts = quantity
                    };

                    await OrderLineService.AddOrderLineAsync(orderLine);
                }

                // Add visit and update loyalty discount
                customer.AddVisit();

                if (loyaltyDiscount != null)
                {
                    loyaltyDiscount.NumberOfUses++;
                    await CustomerService.UpdateDiscountAsync(loyaltyDiscount);
                }

                await CustomerService.UpdateAsync(customer);

                // Mark booking as completed
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

        private List<(int productId, int quantity)> FlattenBookingItems(Booking booking)
        {
            var allItems = new List<(int productId, int quantity)>();

            if (booking.Treatments != null)
            {
                foreach (var tb in booking.Treatments)
                {
                    if (tb.Treatment != null)
                        allItems.Add((tb.Treatment.Id, 1));

                    if (tb.TreatmentBookingProducts != null)
                    {
                        foreach (var tbp in tb.TreatmentBookingProducts)
                        {
                            if (tbp.Product != null)
                                allItems.Add((tbp.Product.Id, tbp.NumberOfProducts));
                        }
                    }
                }
            }

            return allItems;
        }

        private List<Product> GetProductsFromBooking(Booking booking)
        {
            var products = new List<Product>();

            if (booking.Treatments != null)
            {
                foreach (var tb in booking.Treatments)
                {
                    if (tb.Treatment != null)
                        products.Add(tb.Treatment);

                    if (tb.TreatmentBookingProducts != null)
                    {
                        foreach (var tbp in tb.TreatmentBookingProducts)
                        {
                            if (tbp.Product != null)
                                products.Add(tbp.Product);
                        }
                    }
                }
            }

            return products.Distinct().ToList();
        }
    }
}
