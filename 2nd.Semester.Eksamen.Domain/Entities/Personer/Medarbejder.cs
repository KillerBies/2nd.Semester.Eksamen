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
        public enum Type { Freelancerne, Fastansatte }
        public enum Fagområde { IT, Marketing, Økonomi, HR, Salg }
        public List<Booking> Bookinger { get; private set; }

    }
}
