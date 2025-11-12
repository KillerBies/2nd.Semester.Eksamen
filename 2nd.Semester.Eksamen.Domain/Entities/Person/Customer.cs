using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public class Customer : Person
    {
        public List<Booking> Visit_History { get; private set; }
        public decimal Point_Balance { get; private set; }
        public Dictionary<Treatment_Type,Punch_Card> Punch_Cards { get; private set; }
    }
}
