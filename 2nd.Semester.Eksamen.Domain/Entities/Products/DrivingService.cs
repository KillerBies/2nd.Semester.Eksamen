using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class DrivingService: Product
    {
        //Driving service for customers who want distant service
        //Distance in kilometers
        public decimal Distance { get; set; }
        public TimeSpan Duration { get; set; }
        public Address ServiceAddress { get; set; }
    }
}
