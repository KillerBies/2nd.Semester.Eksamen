using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Campaign : Discount
    {
        // Elements of a campaign
        //Start and end date of the campaign
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; } = string.Empty;
        //Lists of product categories and treatments that the campaign applies to
        public List<Product> ProductInCampaign { get; set; } = new List<Product>();

        public Campaign() { }

        public bool CheckTime()
        {
            if (DateTime.Now >= Start && DateTime.Now <= End)
                return true;
            return false;
        }

        public bool CheckProduct(Product product)
        {
            if (ProductInCampaign.Contains(product))
                return true;
            return false;
        }

        public DiscountResult GetOrderDiscount(Order order)
        {
            if (CheckTime())
            {
                return new DiscountResult(false, 0, this);
            }

            decimal discountedPrice = 0;
            foreach (ProductSnapshot product in order.Products)
            {
                if (CheckProduct(product))
                    discountedPrice += product.PricePerUnit * discount;
            }

            foreach (TreatmentBooking treatment in order.Booking.Treatments)
            {
                discountedPrice += treatment.Treatment.BasePrice * discount;
            }
            return new DiscountResult(true, discountedPrice, this);
        }
    }
}
