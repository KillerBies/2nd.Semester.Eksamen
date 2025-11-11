using _2nd.Semester.Eksamen.Domain.Entities.Personer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Booking : Base_Entity
    {
        public Customer Customer { get; private set; }
        public string Customer_Firstname { get; private set; }
        public string Customer_Lastname { get; private set; }
        public Adress Customer_Adress { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public decimal Total { get; private set; }
        public List<Treatment> Treatments { get; private set; }
    }
}
