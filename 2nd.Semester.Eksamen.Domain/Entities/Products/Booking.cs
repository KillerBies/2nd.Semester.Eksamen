using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Booking : BaseEntity
    {
        //Elements of a booking
        //Customer who made the booking
        public Customer Customer { get; private set; }
        public string CustomerName { get; private set; }
        public Address CustomerAddress { get; private set; }
        //Start end of booking date and time
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        //Treatments included in the booking
        public List<Treatment> Treatments { get; private set; }
    }
}
