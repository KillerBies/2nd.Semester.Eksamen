using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class BaseProduct : BaseEntity
    {
        //Basic elements of a product
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
