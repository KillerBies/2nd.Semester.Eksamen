using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class TreatmentBooking : BaseEntity
    {
        //This is a booking of a treatment it contians info about the planned date and time of the treatment
        public Booking Booking { get; set; } = null!;
        
        //Employee details
        public int EmployeeID { get; private set; }
        public PersonSnapshot? Employee { get; private set; }


        //Treatment info
        public int TreatmentID { get; private set; }
        public TreatmentSnapshot? Treatment { get; private set; }
        public List<ProductSnapshot>? ProductsUsed { get; private set; }


        //Treatment booking details
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }


        public TreatmentBooking() { }
        public TreatmentBooking(Treatment treatment, Person employee)
        {
            Employee = new PersonSnapshot(employee);
            EmployeeID = employee.Id;
            ProductsUsed = treatment.Products;
            Treatment = new TreatmentSnapshot(treatment);
        }



        //method to set time range of treatment booking
        public bool TrySetTimeRange(DateTime start, DateTime end)
        {
            if (end > start) //if start and end are correct
            {
                Start = start;
                End = end;
                return true;
            }
            else if (end < start) //if start and end are swapped
            {
                Start = end;
                End = start;
                return true;
            }
            return false; //if start and end are the same
        }
    }
}
