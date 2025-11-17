using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public abstract class BaseDiscount : BaseEntity
    {
        //Basic elements of a discount
        public string Name { get; private set; }
        public decimal Discount { get; private set; }
        //Bools representing if the campaign is for products or treatments
        public bool Product { get; private set; }
        public bool Treatment { get; private set; }
        //Times the discount has been used
        public int NumberOfUses { get; private set; }
    }
}
