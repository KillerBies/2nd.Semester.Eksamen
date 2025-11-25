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

        public string Type { get; private set; }  = null!; // Shown as an enum in DTO and blazor
        public string LastName { get; private set; } = null!;
        public string Specialty { get; private set; } = null!;
        public string ExperienceLevel { get; private set; } = null!; // Shown as an enum in DTO and blazor
        public string? Gender { get; private set; } // Shown as an enum in DTO and blazor
        public TimeRange WokringHours { get; private set; }



        //Treatment details
        public EmployeeSchedule Schedule { get; private set; } = new();
        public List<TreatmentBooking> Appointments { get; private set; } = new List<TreatmentBooking>();
        public List<Booking> TreatmentHistory { get; private set; } = new List<Booking>();
        public decimal BasePriceMultiplier { get; private set; } = 0;


        public Employee() { }
        public Employee(string firstname, string lastname, string type, string specialty, string experience, string gender, TimeRange workingHours)
        {
            WokringHours = workingHours;
            TrySetLastName(firstname, lastname);
            Type = type;
            Specialty = specialty;
            ExperienceLevel = experience;
            Gender = gender;
            TreatmentHistory = new List<Booking>();
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
            string specialty,
            string gender
        ) : base(firstname, address, phoneNumber, email)
            {
                TrySetLastName(firstname, lastname);
                ExperienceLevel = experience;
                Type = type;
                Specialty = specialty;
                Gender = gender;

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
        //maybe make a funktion that can tell if they have a spot open some time 
        //check any free timespace maybe they should have free timepsace as a variable
        //give employees a free timespace parameter
        //this should be a list of timeranges
        //time ranges is an object with a datetime end and start and a duration parameter
        //this is precalculated when a treatment booking is added to the employee
        //when an employee is made they should also inform us of what days they want them to work on
        //and what timeranges they are available at.
        //ACTUALLY maybe give employees a schema or scheduel containing timeranges. Here we put in timeranges like breaks vecations and so on. We can then check if they have a spot open by checking the length of every timespan between timerange end and timerange start. Or by precalculating it. Then we check if the treatment fits into their shceduel. this shcema can be a list of years contaning a list of months contaning a list weeks containing a list of workdays each containing their own working horus and so on.


        //method to add to treatment history
        public bool TryAddToTreatmentHistory(Booking booking)
        {
            if (booking != null || booking.Status == BookingStatus.Completed)
            {
                TreatmentHistory.Add(booking);
                return true;
            }
            return false;
        }

    }
}
