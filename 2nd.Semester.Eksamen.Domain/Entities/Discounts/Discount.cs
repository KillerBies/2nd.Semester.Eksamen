using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2nd.Semester.Eksamen.Domain.Entities.Discounts
{
    [NotMapped]
    public class Discount : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public decimal TreatmentDiscount { get; set; }
        public decimal ProductDiscount { get; set; }

        public bool IsLoyalty { get; set; }

        public bool AppliesToTreatment { get; set; }
        public bool AppliesToProduct {  get; set; }
        public int NumberOfUses { get; set; }

        public Discount() { }

        public Discount(string name, decimal treatmentDiscount, decimal productDiscount)
        {
            Name = name;
            TreatmentDiscount = treatmentDiscount;
            ProductDiscount = productDiscount;
        }
        public decimal GetDiscountAmountFor(Product product)
        {
            return product is Treatment ? TreatmentDiscount : ProductDiscount;
        }

        public decimal GetDiscountForProduct(Product product)
        {
            if (product is Treatment)
                return TreatmentDiscount;

            return ProductDiscount;
        }
    }
}
