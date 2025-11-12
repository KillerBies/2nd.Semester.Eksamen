using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Driving_Service: Product
    {
        public decimal Distance { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
