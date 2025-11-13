using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class BookingTreatment : BaseProduct
    {
        //Elements of a booked treatment. It contains products used and the employee doing the treatment.
        //it inherits Price, Discription and Name from base_produkt to snapshot the treatments critical info at booking.
        //the same goes for Employee
        //in case its either changed or deleted
        public Employee Employee { get; private set; }
        public string EmployeeName { get; private set; }
        public Address EmployeeAddress { get; private set; }
        public List<Product> Products { get; private set; }
        public Treatment Treatment { get; private set; }
    }
}
