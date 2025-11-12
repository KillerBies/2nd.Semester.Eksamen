using _2nd.Semester.Eksamen.Domain.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public class Private : Person
    {
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
        public DateOnly Birthday { get; private set; }
    }
}
