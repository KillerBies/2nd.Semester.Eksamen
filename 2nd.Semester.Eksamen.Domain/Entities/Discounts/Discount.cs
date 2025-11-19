using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public Discount(string name, decimal discountAmount)
        {
            Name = name;
            DiscountAmount = discountAmount;
        }
        public Discount() { }


        public void UseDiscount()
        {
            NumberOfUses++;
        }

    }
}
