using _2nd.Semester.Eksamen.Domain.Entities.Person;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Employee : Person
    {
        public EmployeeType Type { get; private set; }
        public Specialties Specialty { get; private set; }
        public ExperienceLevels ExperienceLevel { get; private set; }
        public List<Treatment> TreatmentHistory { get; private set; }
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
        public decimal BasePriceMultiplier { get; private set; }
    }
}
