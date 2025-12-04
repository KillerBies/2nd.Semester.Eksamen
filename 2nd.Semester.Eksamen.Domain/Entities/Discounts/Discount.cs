using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    [NotMapped]
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

        public bool TrySetName(string name)
        {
            if (NameCheck(name))
            {
                Name = name.Trim();
                return true;
            }
            return false;
        }

        public bool NameCheck(string name)
        {
            string nameTest = name.Trim(new char[] { ' ', '-', '.', '\'' });
            if (nameTest.All(char.IsLetter) && nameTest != "")
                return true;
            return false;
        }

        public bool TrySetDiscountAmount(string discountAmount)
        {
            return decimal.TryParse(
                discountAmount,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,       //avoids surprises with ',' vs '.' depending on the machines locale
                out _);
        }
    }
}
