using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using System;
using System.Collections.Generic;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules.BookingSchedules
{
    internal class PlanItem
    {
        public Treatment Treatment { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public TimeSpan Duration { get; set; }
        public EmployeeSchedule Schedule { get; set; } = null!;
    }
}
