using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
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
        private decimal originalTotal;
        private Discount? appliedDiscount;
        private Discount? bestDiscount;
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
                // 1️⃣ Get customer
                customer = await CustomerService.GetByIDAsync(id) as PrivateCustomer;
                if (customer == null) throw new Exception("Customer not found");

                // 2️⃣ Get next pending booking
                var booking = await CustomerService.GetNextPendingBookingAsync(customer.Id);
                if (booking == null)
                {
                    errorMessage = "No pending bookings found for this customer.";
                    return;
                }

                // 3️⃣ Convert booked treatments into products
                products = booking.Treatments
                    .Where(tb => tb.Treatment != null)
                    .Select(tb => new Product
                    {
                        Id = tb.Treatment.Id,
                        Name = tb.Treatment.Name,
                        Price = tb.Treatment.Price
                    })
                    .ToList();


                // 4️⃣ Calculate discounts
                (originalTotal, bestDiscount, loyaltyDiscount, finalTotal) =
                    await OrderService.CalculateBestDiscountsAsync(customer.Id, products);

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
                // 1️⃣ Get the next pending booking for this customer
                var booking = await CustomerService.GetNextPendingBookingAsync(customer.Id);
                if (booking == null)
                {
                    errorMessage = "No pending booking found.";
                    return;
                }

                // 2️⃣ Create or update the order for the booking
                var order = await OrderService.CreateOrUpdateOrderForBookingAsync(booking.Id);




                // 3 Update customer visits
                customer.AddVisit();
                if (appliedDiscount != null)
                {
                    appliedDiscount.NumberOfUses++;
                    await CustomerService.UpdateDiscountAsync(appliedDiscount);
                }
                await CustomerService.UpdateAsync(customer);

                paymentSuccess = true;
                Console.WriteLine($"Order #{order.Id} created/updated with total {order.Total} kr. Booking marked as completed.");
                
                // 4 Mark booking as completed
                booking.Status = BookingStatus.Completed;
                await CustomerService.UpdateBookingAsync(booking);
            }
            catch (Exception ex)
            {
                errorMessage = $"Error creating order: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
            finally
            {
                isLoading = false;
            }
        }


    }
}
