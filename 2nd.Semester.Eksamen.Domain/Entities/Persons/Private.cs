using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Private : Customer
    {
        //Customer who is a private person
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
        public DateOnly Birthday { get; private set; }

        public Private(
            string name,
            Address address,
            string phoneNumber,
            string email,
            string notes,
            Gender gender,
            int age,
            DateOnly birthday
        ) : base(name, address, phoneNumber, email, notes)
        {
            Gender = gender;
            Age = age;
            Birthday = birthday;
        }
    }
}
