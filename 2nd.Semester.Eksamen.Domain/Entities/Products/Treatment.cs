using _2nd.Semester.Eksamen.Domain.Entities.Personer;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Treatment : Base_Product
    {
        public Treatment_Type Treatment_Type { get; private set; }
        public Customer Customer { get; private set; }
        public Employee Employee { get; private set; }
        public string Employee_Firstname { get; private set; }
        public string Employee_Lastname { get; private set; }
        public Adress Employee_Adress { get; private set; }
        public TimeSpan Duration { get; private set; }
        public List<Product> Products { get; private set; }
    }
}
