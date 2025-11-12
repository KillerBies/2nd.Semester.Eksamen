using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public abstract class Customer : Person
    {
        //Base class for customers
        public List<Booking> BookingHistory { get; private set; }
        public decimal PointBalance { get; private set; }
        public Dictionary<TreatmentType, PunchCard> PunchCards { get; private set; }
        public string Notes { get; private set; }

    }
}
