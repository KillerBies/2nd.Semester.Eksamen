using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Employee : Person
    {
        //Employee details
        public EmployeeType? Type { get; private set; }
        public string? LastName { get; private set; }
        public string? Specialty { get; private set; }
        public ExperienceLevels? ExperienceLevel { get; private set; }
        public Gender? Gender { get; private set; }



        //Treatment details
        public List<Appointment>? Appointments { get; private set; }
        public List<Booking>? TreatmentHistory { get; private set; }
        public decimal? BasePriceMultiplier { get; private set; }


        public Employee() { }
        public Employee(string firstname, string lastname, EmployeeType type, string specialty, ExperienceLevels experience, Gender gender)
        {
            TrySetLastName(firstname, lastname);
            Type = type;
            Specialty = specialty;
            ExperienceLevel = experience;
            Gender = gender;
            TreatmentHistory = new List<Booking>();
            Appointments = new List<Appointment>();

        }


        //method to set last and first name of private person
        public bool TrySetLastName(string firstname, string lastname)
        {
            TrySetName(firstname); //sets firstname of person class
            if (NameCheck(lastname))
            {
                LastName = lastname.Trim();
                return true;
            }
            return false;
        }

        //method to set base price multiplier
        public bool TrySetBasePriceMultiplier(decimal multiplier)
        {
            if (multiplier > 0)
            {
                BasePriceMultiplier = multiplier;
                return true;
            }
            return false;
        }

        //method to add to work schedule
        public bool TryAddToWorkSchedule(DateTime start, DateTime end)
        {
            if (IsAvailable(start, end)) //checks if new time range overlaps with any existing time ranges
            {
                Appointments.Add(new Appointment(Id,start,end));
                return true;
            }
            return false;
        }

        //method to check if employee is available at given time range
        public bool IsAvailable(DateTime start, DateTime end)
        {
            return !Appointments.Any(tr => tr.Overlaps(start, end)); //checks if the employee is available at the given time range
        }

        //method to add to treatment history
        public bool TryAddToTreatmentHistory(Booking booking)
        {
            if (booking != null && booking.Status == Products.BookingStatus.Completed)
            {
                TreatmentHistory.Add(booking);
                return true;
            }
            return false;
        }


    }
}
