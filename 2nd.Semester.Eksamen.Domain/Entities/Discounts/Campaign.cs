using _2nd.Semester.Eksamen.Domain.Entities.Products;

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
        public List<Product> ProductsInCampaign { get; set; } = new List<Product>();

        public Campaign() { }

        public Campaign(string name, decimal discountAmount, DateTime start, DateTime end) : base(name, discountAmount)
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

        public bool TryAddStartTime()
    }
}
