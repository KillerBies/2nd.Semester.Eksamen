using _2nd.Semester.Eksamen.Domain.Entities.Person;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public class Employee : Person
    {
        public Employee_Type Type { get; private set; }
        public Specialties Specialty { get; private set; }
        public int Experience_Level { get; private set; }
        public List<Treatment> Treatments { get; private set; }
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
    }
}
