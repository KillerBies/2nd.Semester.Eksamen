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

        // Which products/treatments this campaign affects
        public List<Product> ProductsInCampaign { get; set; } = new List<Product>();

        public Campaign() { }

        public Campaign(string name, decimal discountAmount, DateTime start, DateTime end)
            : base(name, discountAmount)
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

        public bool CheckProduct(int productId)
        {
            return ProductsInCampaign.Any(p => p.Id == productId);
        }

        public bool CheckTreatment(int treatmentId)
        {
            return ProductsInCampaign.Any(p => p.Id == treatmentId);
        }

        public DiscountResult GetOrderDiscount(Order order, Booking booking)
        {
            if (!CheckTime()) // Only active campaigns
                return new DiscountResult(false, 0, this);

            decimal discountedPrice = 0;

            // 1️⃣ Normal products in the order
            foreach (var line in order.Products)
            {
                if (CheckProduct(line.LineProduct))
                {
                    discountedPrice += (line.LineProduct.Price * line.NumberOfProducts) * DiscountAmount;
                }
            }

            // 2️⃣ Treatments in the booking
            foreach (var treatmentBooking in booking.Treatments)
            {
                if (CheckProduct(treatmentBooking.Treatment))
                {
                    discountedPrice += treatmentBooking.Treatment.Price * DiscountAmount;
                }

                // Optional: if you want to include ProductsUsed in treatments
                foreach (var tbp in treatmentBooking.ProductsUsed)
                {
                    if (CheckProduct(tbp.Product))
                        discountedPrice += tbp.Product.Price * DiscountAmount;
                }
            }

            return new DiscountResult(true, discountedPrice, this);
        }

    }
}
