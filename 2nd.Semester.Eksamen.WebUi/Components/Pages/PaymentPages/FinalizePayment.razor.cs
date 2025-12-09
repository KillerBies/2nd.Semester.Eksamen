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

        private Customer? customer;
        private List<Product> products = new();
        private decimal originalTotal;
        private Discount? appliedDiscount;
        private Discount? bestDiscount;
        private Discount? loyaltyDiscount;
        private decimal finalTotal;

        // For Razor table
        private List<ProductDiscountInfoDTO> itemDiscounts = new();

        [Inject] private IOrderService _orderService { get; set; } = default!;
        [Inject] private ICustomerService _customerService { get; set; } = default!;
        [Inject] private IDiscountCalculator _discountCalculator { get; set; } = default!;

        [Inject] private IInvoiceService _invoiceService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            errorMessage = null;

            try
            {
                customer = await _customerService.GetByIDAsync(id);
                if (customer == null) throw new Exception("Customer not found");

                var booking = await _customerService.GetNextPendingBookingAsync(customer.Id);
                if (booking == null)
                {
                    errorMessage = "No pending booking found for this customer.";
                    return;
                }

                // Flatten booking items, formatting it so it gives a list of product and quantity
                var allItems = FlattenBookingItems(booking);
                products = allItems.Select(x => x.product).Distinct().ToList();

                if (!products.Any())
                {
                    errorMessage = "No treatments or products found for this booking.";
                    return;
                }

                // Get totals & discounts
                (originalTotal, bestDiscount, loyaltyDiscount, finalTotal, itemDiscounts) =
                    await _discountCalculator.CalculateAsync(customer.Id, products);


                // compare best discount using actual discount amounts
                decimal bestDiscountAmount = bestDiscount != null
                    ? itemDiscounts.Where(i => i.DiscountName == bestDiscount.Name).Sum(i => i.DiscountAmount)
                    : 0;

                decimal loyaltyDiscountAmount = loyaltyDiscount != null
                    ? itemDiscounts.Where(i => i.IsLoyalty).Sum(i => i.DiscountAmount)
                    : 0;

                appliedDiscount = (loyaltyDiscountAmount > bestDiscountAmount)
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
                var booking = await _customerService.GetNextPendingBookingAsync(customer.Id);
                if (booking == null)
                {
                    errorMessage = "No pending booking found.";
                    return;
                }

                // Let OrderService handle creating/updating order & order lines
                var order = await _orderService.CreateOrUpdateOrderForBookingAsync(booking.Id);

                customer.AddVisit();

                if (appliedDiscount != null)
                {
                    appliedDiscount.NumberOfUses++;
                    await _customerService.UpdateDiscountAsync(appliedDiscount);
                }


                await _customerService.UpdateAsync(customer);

                booking.TryChangeStatus(BookingStatus.Completed);
                await _customerService.UpdateBookingAsync(booking);

                paymentSuccess = true;
                Console.WriteLine($"Order #{order.Id} created with {products.Count} unique products.");

                //ONLY A TEST TO SEE IF CREATING SNAPSHOT WORKS!!!!!!!!
                await _invoiceService.CreateSnapshotInDBAsync(order);

                if (!customer.SaveAsCustomer)
                { //TODO: Fix error around cascading delete of customer + booking
                    //await CustomerService.DeleteAsync(customer);
                }

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

            if (booking.Treatments != null)
            {
                foreach (var tb in booking.Treatments)
                {
                    // Apply employee multiplier only to the treatment itself
                    if (tb.Treatment != null && tb.Employee != null)
                    {

                        // Clone the treatment so we don't overwrite DB values accidentally
                        var treatmentWithMultiplier = new Treatment
                        {
                            Id = tb.Treatment.Id,
                            Name = tb.Treatment.Name,
                            Price = tb.Treatment.Price * tb.Employee.BasePriceMultiplier,
                            DiscountedPrice = tb.Treatment.DiscountedPrice * tb.Employee.BasePriceMultiplier
                        };

                        allItems.Add((treatmentWithMultiplier, 1));
                    }

                    // Add all additional products without any multiplier
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

            return allItems;
        }

    }
}
