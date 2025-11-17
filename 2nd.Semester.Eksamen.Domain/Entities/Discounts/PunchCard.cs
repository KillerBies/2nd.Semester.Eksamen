using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class PunchCard : BaseEntity
    {
        public Customer Customer { get; private set; }
        public int PunchNumber { get; private set; }
        public TreatmentType Treatment { get; private set; }
    }
}
