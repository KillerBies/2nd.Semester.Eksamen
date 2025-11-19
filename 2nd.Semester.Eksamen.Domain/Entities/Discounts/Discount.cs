using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Discount : BaseEntity
    {
        //Basic elements of a discount
        public string Name { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        //Bools representing if the campaign is for products or treatments
        public bool AppliesToProduct { get; set; }
        public bool AppliesToTreatment { get; set; }
        //Times the discount has been used
        public int NumberOfUses { get; set; }

        public Discount() { }

        public bool CheckProduct(Product product)
        {
            if (product.GetType() == typeof(Product))
                return AppliesToProduct;
            return AppliesToTreatment;
        }

        public decimal GetDiscountedPrice(ProductSnapshot product)
        {
            return DiscountAmount * product.Price;
        }
    }
}
