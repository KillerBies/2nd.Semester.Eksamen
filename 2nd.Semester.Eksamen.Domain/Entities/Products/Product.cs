using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Product : Base_Product
    {
        public string Name { get; set; }
        public int Number_Sold { get; set; }
    }
}
