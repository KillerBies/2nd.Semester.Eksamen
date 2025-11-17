using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public abstract class Person : BaseEntity
    {
        //Basic elements of a person
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
    public Person(string name, Address address, string phoneNumber, string email)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
