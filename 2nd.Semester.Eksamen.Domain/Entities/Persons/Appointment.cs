using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Appointment : BaseEntity
    {
        public Employee Employee { get; private set; } = null!;
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public Appointment() { }
        public Appointment(Employee employee, DateTime start, DateTime end)
        {
            Employee = employee;
            Start = start;
            End = end;
        }
        public bool Overlaps(DateTime start, DateTime end)
        {
            return Start < end && start < End;
        }
    }
}
