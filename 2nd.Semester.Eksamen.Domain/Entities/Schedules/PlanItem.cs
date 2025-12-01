using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules
{
    internal class PlanItem
    {
        public Treatment Treatment { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public TimeSpan Duration { get; set; }
        public EmployeeSchedule Schedule { get; set; } = null!;
    }
}
