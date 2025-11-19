using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Appointment : BaseEntity
    {
        public int? EmployeeId { get; private set; }
        public DateTime? Start { get; private set; }
        public DateTime? End { get; private set; }

        public Employee? Employee { get; private set; }

        public Appointment() { }
        public Appointment(int employeeId, DateTime start, DateTime end)
        {
            EmployeeId = employeeId;
            Start = start;
            End = end;
        }
        public bool Overlaps(DateTime start, DateTime end)
        {
            return Start < end && start < End;
        }
    }
}
