using _2nd.Semester.Eksamen.Domain.Entities.Personer;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Punch_Card : Base_Entity
    {
        public Customer Customer { get; private set; }
        public int Punch_Number { get; private set; }
        public Treatment Treatment { get; private set; }
    }
}
