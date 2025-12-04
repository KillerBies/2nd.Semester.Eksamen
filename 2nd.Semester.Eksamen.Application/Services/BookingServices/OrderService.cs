using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly ICustomerService _customerService;

        public OrderService(
            IProductService productService,
            IDiscountService discountService,
            ICustomerService customerService)
        {
            _productService = productService;
            _discountService = discountService;
            _customerService = customerService;
        }

        public async Task<Order> CreateOrUpdateOrderForBookingAsync(int bookingId)
        {
            var booking = await _customerService.GetBookingWithTreatmentsAsync(bookingId);
            if (booking == null)
                throw new System.Exception("Booking not found");

            var productsList = booking.Treatments
                .Where(tb => tb.Treatment != null)
                .Select(tb => new Product
                {
                    Id = tb.Treatment.Id,
                    Name = tb.Treatment.Name,
                    Price = tb.Treatment.Price
                })
                .ToList();

            var (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal) =
                await CalculateBestDiscountsAsync(booking.CustomerId, productsList);

            var order = await _customerService.GetOrderByBookingIdAsync(bookingId);

            if (order == null)
            {
                order = new Order(bookingId, originalTotal, finalTotal, appliedDiscount?.Id ?? 0);
                await _customerService.AddOrderAsync(order);
            }
            else
            {
                order.UpdateTotals(originalTotal, finalTotal, appliedDiscount?.Id);
                await _customerService.UpdateOrderAsync(order);
            }

            return order;
        }


        public async Task<(decimal originalTotal, Discount? appliedDiscount, Discount? loyaltyDiscount, decimal finalTotal)>
        CalculateBestDiscountsAsync(int customerId, List<Product> products)
        {
            if (products == null || !products.Any())
                throw new System.Exception("No products provided for discount calculation");


            decimal originalTotal = products.Sum(p => p.Price);


            var allDiscounts = await _discountService.GetAllDiscountsAsync();
            var regularDiscounts = allDiscounts.Where(d => !d.IsLoyalty && d.AppliesToTreatment).ToList();
            var bestRegular = regularDiscounts.OrderByDescending(d => d.DiscountAmount).FirstOrDefault();


            var customer = await _privateCustomerService.GetCustomerByIdAsync(customerId)
            ?? throw new System.Exception("Customer not found");


            var loyaltyEntity = await _discountService.GetLoyaltyDiscountForVisitsAsync(customer.NumberOfVisists);
            Discount? loyaltyDiscount = null;


            if (loyaltyEntity != null && loyaltyEntity.AppliesToTreatment)
            {
                loyaltyDiscount = new Discount
                {
                    Id = loyaltyEntity.Id,
                    Name = loyaltyEntity.Name,
                    DiscountAmount = loyaltyEntity.DiscountAmount,
                    IsLoyalty = true
                };
            }


            Discount? appliedDiscount = loyaltyDiscount != null && loyaltyDiscount.DiscountAmount > (bestRegular?.DiscountAmount ?? 0)
            ? loyaltyDiscount
            : bestRegular;


            decimal finalTotal = originalTotal;
            if (appliedDiscount != null)
                finalTotal -= originalTotal * appliedDiscount.DiscountAmount;


            return (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal);
        }


        public Task<List<Product>> GetProductsByIdsAsync(List<int> productIds) => _productService.GetProductsByIdsAsync(productIds);
        public Task<List<Discount>> GetAllDiscountsAsync() => _discountService.GetAllDiscountsAsync();
    }
}

    //private readonly IProductService _productService;
    //private readonly IDiscountService _discountService;
    //private readonly IPrivateCustomerService _privateCustomerService;

    //public OrderService(
    //    IProductService productService,
    //    IDiscountService discountService,
    //    IPrivateCustomerService customerService)
    //{
    //    _productService = productService;
    //    _discountService = discountService;
    //    _privateCustomerService = customerService;
    //}


    //public async Task<Order> CreateOrUpdateOrderForBookingAsync(int bookingId)
    //{
    //    // 1) Get the booking with treatments
    //    var booking = await _privateCustomerService.GetBookingWithTreatmentsAsync(bookingId);
    //    if (booking == null)
    //        throw new Exception("Booking not found");

    //    // 2) Convert booked treatments to Products for discount calculation
    //    var productsList = booking.Treatments
    //        .Select(tb => new Product
    //        {
    //            Id = tb.Treatment.Id,
    //            Name = tb.Treatment.Name,
    //            Price = tb.Treatment.Price
    //        })
    //        .ToList();

    //    // 3) Calculate discounts
    //    var (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal) =
    //        await CalculateBestDiscountsAsync(booking.Customer.Id, productsList);

    //    // 4) Create or update the order
    //    var order = await _privateCustomerService.GetOrderByBookingIdAsync(bookingId);

    //    if (order == null)
    //    {
    //        order = new Order(
    //            bookingId,
    //            originalTotal,
    //            finalTotal,
    //            appliedDiscount?.Id ?? 0
    //        );

    //        await _privateCustomerService.AddOrderAsync(order);
    //    }
    //    else
    //    {
    //        order.UpdateTotals(originalTotal, finalTotal, appliedDiscount?.Id);
    //        await _privateCustomerService.UpdateOrderAsync(order);
    //    }

    //    return order;
    //}


    //public async Task<(decimal originalTotal, Discount? appliedDiscount, Discount? loyaltyDiscount, decimal finalTotal)>
    //    CalculateBestDiscountsAsync(int customerId, List<Product> products)
    //{
    //    if (products == null || !products.Any())
    //        throw new Exception("No products provided for discount calculation");

    //    // Original total
    //    decimal originalTotal = products.Sum(p => p.Price);

    //    // Regular discounts
    //    var allDiscounts = await _discountService.GetAllDiscountsAsync();
    //    var regularDiscounts = allDiscounts
    //        .Where(d => !d.IsLoyalty && d.AppliesToTreatment)
    //        .ToList();

    //    var bestRegular = regularDiscounts
    //        .OrderByDescending(d => d.DiscountAmount)
    //        .FirstOrDefault();

    //    // Get customer
    //    var customer = await _privateCustomerService.GetCustomerByIdAsync(customerId)
    //        ?? throw new Exception("Customer not found");

    //    // Loyalty discount
    //    var loyaltyEntity = await _discountService.GetLoyaltyDiscountForVisitsAsync(customer.NumberOfVisists);
    //    Discount? loyaltyDiscount = null;

    //    if (loyaltyEntity != null)
    //    {
    //        loyaltyDiscount = new Discount
    //        {
    //            Id = loyaltyEntity.Id,
    //            Name = loyaltyEntity.Name,
    //            DiscountAmount = loyaltyEntity.DiscountAmount,
    //            IsLoyalty = true
    //        };
    //    }

    //    // Choose discount
    //    Discount? appliedDiscount =
    //        (loyaltyDiscount != null && loyaltyDiscount.DiscountAmount > (bestRegular?.DiscountAmount ?? 0))
    //            ? loyaltyDiscount
    //            : bestRegular;

    //    // Final total
    //    decimal finalTotal = originalTotal;
    //    if (appliedDiscount != null)
    //        finalTotal -= originalTotal * appliedDiscount.DiscountAmount;

    //    return (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal);
    //}


    //public Task<List<Product>> GetProductsByIdsAsync(List<int> productIds)
    //{
    //    return _productService.GetProductsByIdsAsync(productIds);
    //}

    //public Task<List<Discount>> GetAllDiscountsAsync()
    //{
    //    return _discountService.GetAllDiscountsAsync();
    //}

