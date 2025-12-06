using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;

namespace _2nd.Semester.Eksamen.Domain.Entities.Discounts
{
    public class Campaign : Discount
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; } = string.Empty;

        public List<Product> ProductsInCampaign { get; set; } = new List<Product>();

        public Campaign() { }

        public Campaign(string name, decimal treatmentDiscount, decimal productDiscount, DateTime start, DateTime end)
            : base(name, treatmentDiscount, productDiscount)
        {
            Start = start;
            End = end;
        }


        public bool TryAddProductToCampaign(Product product)
        {
            if (!ProductsInCampaign.Contains(product))
            {
                ProductsInCampaign.Add(product);
                return true;
            }
            return false;
        }

        public bool CheckTime()
        {
            return DateTime.Now >= Start && DateTime.Now <= End;
        }

        public bool CheckProduct(Product product)
        {
            return ProductsInCampaign.Any(p => p.Id == product.Id);
        }

        public DiscountResult GetOrderDiscount(Order order, Booking booking)
        {
            if (!CheckTime())
                return new DiscountResult(false, 0, this);

            decimal totalDiscount = 0;

            // 1️ Normal order products
            foreach (var line in order.Products)
            {
                var product = line.LineProduct;

                if (CheckProduct(product))
                {
                    decimal discountAmount = GetDiscountForProduct(product);
                    totalDiscount += discountAmount * line.NumberOfProducts;
                }
            }

            // 2️ Treatments in the booking
            foreach (var treatmentBooking in booking.Treatments)
            {
                var treatment = treatmentBooking.Treatment;

                if (CheckProduct(treatment))
                {
                    decimal discountAmount = GetDiscountForProduct(treatment);
                    totalDiscount += discountAmount;
                }

            }

            return new DiscountResult(true, totalDiscount, this);
        }
    }
}
