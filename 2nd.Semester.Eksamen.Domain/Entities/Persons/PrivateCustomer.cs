using _2nd.Semester.Eksamen.Domain.Entities.Person;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class PrivateCustomer : Customer
    {
        //Customer who is a private person
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
        public DateOnly Birthday { get; private set; }
    public PrivateCustomer(string name, Address address, string phoneNumber, string email, Gender gender, DateOnly birthday) : base (name, address, phoneNumber, email)
    {
            Gender = gender;
            Birthday = birthday;
               
    }
    }




}
