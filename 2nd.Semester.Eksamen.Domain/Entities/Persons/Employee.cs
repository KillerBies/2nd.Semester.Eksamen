using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Employee : Person
    {
        //Employee details
        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; } // FK
        public Address Address { get; private set; } = null!;
        public string Type { get; private set; }  = null!; // Shown as an enum in DTO and blazor
        public string LastName { get; private set; } = null!;
        public string Specialty { get; private set; } = null!;
        public string ExperienceLevel { get; private set; } = null!; // Shown as an enum in DTO and blazor
        public string Gender { get; private set; } // Shown as an enum in DTO and blazor



        //Treatment details
        public List<TreatmentBooking> Appointments { get; private set; } = new List<TreatmentBooking>();
        public List<Booking> TreatmentHistory { get; private set; } = new List<Booking>();
        public decimal BasePriceMultiplier { get; private set; } = 0;


        public Employee() { }
        public Employee(string firstname, string lastname, string type, string specialty, string experience, string gender/*,Address address*/)
        {
            //Address = address;
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
            return !Appointments.Any(tr => tr.Overlaps(start, end)); //checks if the employee is available at the given time range
        }

        //method to add to treatment history
        public bool TryAddToTreatmentHistory(Booking booking)
        {
            if (booking != null && booking.Status == BookingStatus.Completed)
            {
                TreatmentHistory.Add(booking);
                return true;
            }
            return false;
        }

        public bool TrySetType(string type)
        {
            if (!string.IsNullOrWhiteSpace(type))
            {
                Type = type;
                return true;
            }
            return false;
        }

        public bool TrySetSpecialty(string specialty)
        {
            if (!string.IsNullOrWhiteSpace(specialty))
            {
                Specialty = specialty;
                return true;
            }
            return false;
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
