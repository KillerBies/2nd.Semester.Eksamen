using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts
{
    public class TreatmentBooking : BaseEntity
    {
        //This is a booking of a treatment it contians info about the planned date and time of the treatment
        public int BookingID { get; protected set; }
        public Booking Booking { get; protected set; } = null!;

        //Employee details
        public int EmployeeId { get; protected set; }
        public Employee Employee { get; protected private set; }

        //Treatment info
        public int TreatmentId { get; protected set; }
        public Treatment Treatment { get; protected set; } = null!;
        public List<TreatmentBookingProduct> TreatmentBookingProducts { get; private set; } = new List<TreatmentBookingProduct>();


        //Treatment booking details
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }
        public decimal Price { get; protected set; }


        public TreatmentBooking() { }
        public TreatmentBooking(Treatment treatment, Employee employee, DateTime start, DateTime end, decimal price)
        {
            TrySetTimeRange(start, end);
            Price = price;
            Employee = employee;
            EmployeeId = employee.Id;
            Treatment = treatment;
            TreatmentId = treatment.Id;
        }
        public TreatmentBooking(int treatmentId, int employeeId, DateTime start, DateTime end, decimal price)
        {
            TrySetTimeRange(start, end);
            EmployeeId = employeeId;
            TreatmentId = treatmentId;
            Price = price;
        }
        public TreatmentBooking(Treatment treatment, int employeeId, DateTime start, DateTime end)
        {
            TrySetTimeRange(start, end);
            EmployeeId = employeeId;
            Treatment = treatment;
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
        public bool ChangeEmployee(Employee employee)
        {
            if (!employee.Appointments.Any(a => a.Overlaps(Start, End)))
            {
                Employee = employee;
                return true;
            }
            return false;
        }
        public bool Overlaps(DateTime start, DateTime end)
        {
            return Start < end && start < End;
        }
        public void AddToBooking(int Bookingid)
        {
            BookingID = Bookingid;
        }
    }
}
