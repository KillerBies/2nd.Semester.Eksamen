using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public class Medarbejder : Person
    {
        public Medarbejder_Type Type { get; private set; }
        public Fagområder Fagområde { get; private set; }
        public int Erfaringsniveau { get; private set; }
        public List<Booking> Bookinger { get; private set; }

    }
}
