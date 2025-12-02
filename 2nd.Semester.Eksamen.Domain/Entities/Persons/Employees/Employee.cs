using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees
{
    public class Employee : Person
    {
        //Employee Person details
        public string Type { get; private set; } = null!; // Shown as an enum in DTO and blazor
        public string LastName { get; private set; } = null!;
        public string Gender { get; private set; } // Shown as an enum in DTO and blazor




        //Employee Work details
        public string Specialties { get; set; }
        public TimeSpan WorkStart { get; set; }
        public TimeSpan WorkEnd { get; set; }
        public string ExperienceLevel { get; private set; } = null!; // Shown as an enum in DTO and blazor
        public decimal BasePriceMultiplier { get; set; } = 1;

        //Schedule details
        public EmployeeSchedule Schedule { get; set; }
        public List<TreatmentBooking> Appointments { get; set; } = new List<TreatmentBooking>();
        //public List<Treatment> TreatmentHistory { get; set; } = new List<TreatmentBooking>();


        public Employee() { }

        public Employee(
            string firstname,
            string lastname,
            string email,
            string phoneNumber,
            Address address,
            decimal basePriceMultiplier,
            string experience,
            string type,
            string specialties,
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


        ////method to add to treatment history
        //public bool TryAddToTreatmentHistory(Booking booking)
        //{
        //    if (booking != null || booking.Status == BookingStatus.Completed)
        //    {
        //        TreatmentHistory.AddRange(booking.Treatments.Where(t => t.Employee.Id == Id).ToList());
        //        return true;
        //    }
        //    return false;
        //}

        public bool TrySetType(string type)
        {
            if (!string.IsNullOrWhiteSpace(type))
            {
                Type = type;
                return true;
            }
            return false;
        }

        public bool TrySetSpecialties(string specialties)
        {
            if (string.IsNullOrEmpty(specialties)) return false;
            Specialties = specialties;
            return true;
        }

        public bool TryAddSpecialty(string specialty)
        {
            if (string.IsNullOrEmpty(specialty)) return false;
            Specialties += ","+specialty;
            return true;
        }
        public bool TrySetExperience(string experience)
        {
            if (!string.IsNullOrWhiteSpace(experience))
            {
                ExperienceLevel = experience;
                return true;
            }
            return false;
        }

        public bool TrySetGender(string gender)
        {
            if (!string.IsNullOrWhiteSpace(gender))
            {
                Gender = gender;
                return true;
            }
            return false;
        }
        public void TrySetAddress(string city, string postalCode, string streetName, string houseNumber)
        {
            if (Address != null)
            {
                Address.UpdateCity(city);
                Address.UpdatePostalCode(postalCode);
                Address.UpdateStreetName(streetName);
                Address.UpdateHouseNumber(houseNumber);
            }
            else
            {
                // Only create a new Address if there isn't one already
                Address = new Address(city, postalCode, streetName, houseNumber);
            }
        }

    }
}
