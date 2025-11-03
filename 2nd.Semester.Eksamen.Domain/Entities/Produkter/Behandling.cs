using _2nd.Semester.Eksamen.Domain.Entities.Personer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Behandling : Base_Produkt
    {
        public Behandlings_Type Behandlings_Type { get; set; }
        public TimeSpan Estimeret_Tidsforbrug { get; set; }
        public Medarbejder Medarbejder { get; set; }
    }
}
