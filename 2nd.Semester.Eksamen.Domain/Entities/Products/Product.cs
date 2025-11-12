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
        public int NumberSold { get; set; }
    }
}
