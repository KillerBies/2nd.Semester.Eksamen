using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Order : Base_Entity
    {
        public Booking Booking { get; set; }
        public List<OrderProdukt> Produkter { get; set; }
    }
}
