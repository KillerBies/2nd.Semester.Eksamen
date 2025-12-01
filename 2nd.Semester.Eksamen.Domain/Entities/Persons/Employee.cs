using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Employee : Person
    {
        //Employee details

        public string Type { get;  set; }  = null!; // Shown as an enum in DTO and blazor
        public string LastName { get;  set; } = null!;
        public List<String> Specialties { get;  set; } = null!;
        public string ExperienceLevel { get;  set; } = null!; // Shown as an enum in DTO and blazor
        public string? Gender { get;  set; } // Shown as an enum in DTO and blazor
        public TimeSpan WorkStart { get;  set; }
        public TimeSpan WorkEnd { get;  set; }



        //Treatment details
        public EmployeeSchedule Schedule { get;  set; } = new();
        public List<TreatmentBooking> Appointments { get;  set; } = new List<TreatmentBooking>();
        public List<Booking> Bookings { get;  set; } = new List<Booking>();
        public decimal BasePriceMultiplier { get;  set; } = 0;


        public Employee() { }
        public Employee(string firstname, string lastname, string type, List<string> specialties, string experience, string gender, TimeSpan workStart, TimeSpan workEnd)
        {
            WorkEnd = workEnd;
            WorkStart = workStart;
            TrySetLastName(firstname, lastname);
            Type = type;
            Specialties = specialties;
            ExperienceLevel = experience;
            Gender = gender;
            Bookings = new List<Booking>();
            Appointments = new List<TreatmentBooking>();

        }
        public Employee(
            string firstname,
            string lastname,
            string email,
            string phoneNumber,
            Address address,
            decimal basePriceMultiplier,
            string experience,
            string type,
            List<string> specialties,
            string gender,
            TimeSpan workStart,
            TimeSpan workEnd
        ) : base(firstname, address, phoneNumber, email)
            {
                TrySetLastName(firstname, lastname);
                ExperienceLevel = experience;
                Type = type;
                Specialties = specialties;
                Gender = gender;
                WorkEnd = workEnd;
                WorkStart = workStart;
                TrySetBasePriceMultiplier(basePriceMultiplier);
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
                //Appointments.Add(new TreatmentBooking(this,start,end));
                return true;
            }
            return false;
        }

        //method to check if employee is available at given time range
        public bool IsAvailable(DateTime start, DateTime end)
        {
            //issue: this only checks on the given time and date not if they have a spot available some day for them
            return !Appointments.Any(tr => tr.Overlaps(start, end)); //checks if the employee is available at the given time range
        }


        //method to add to treatment history
        public bool TryAddToTreatmentHistory(Booking booking)
        {
            if (booking != null || booking.Status == BookingStatus.Completed)
            {
                Bookings.Add(booking);
                return true;
            }
            return false;
        }

    }
}
