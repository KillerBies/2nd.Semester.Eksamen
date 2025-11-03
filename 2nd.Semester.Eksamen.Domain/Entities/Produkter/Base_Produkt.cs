using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public abstract class Base_Produkt : Base_Entity
    {
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public decimal Pris { get; set; }
    }
}
