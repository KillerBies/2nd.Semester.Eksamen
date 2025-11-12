using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public abstract class Base_Product : Base_Entity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
