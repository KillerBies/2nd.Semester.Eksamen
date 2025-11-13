using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Product : BaseProduct
    {
        //Products sold or used
        //it inherits from baseproduct to snapshot its critical info at time of use or purchase
        //incase the product is changed or deleted.
        public int NumberSold { get; set; }
    }
}
