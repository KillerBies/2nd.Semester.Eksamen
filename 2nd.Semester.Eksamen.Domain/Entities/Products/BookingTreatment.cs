using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class BookingTreatment:Treatment
    {
        //Elements of a booked treatment. It contains products used and the employee doing the treatment.
        public Employee Employee { get; private set; }
        public string EmployeeName { get; private set; }
        public Address EmployeeAddress { get; private set; }
        public List<Product> Products { get; private set; }
    }
}
