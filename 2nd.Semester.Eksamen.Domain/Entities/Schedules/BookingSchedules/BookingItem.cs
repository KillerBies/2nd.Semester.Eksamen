using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules.BookingSchedules
{
    public class BookingItem
    {
        public Treatment Treatment { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
