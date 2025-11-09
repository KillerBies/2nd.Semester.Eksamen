using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Kørsel: Produkt
    {
        public decimal Afstand { get; set; }
        public TimeSpan Varighed { get; set; }
    }
}
