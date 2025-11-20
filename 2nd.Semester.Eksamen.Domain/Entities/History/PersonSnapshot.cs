using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public class PersonSnapshot
    {
        public string? Name { get; private set; }
        public Address? Address { get; private set; }
        public PersonSnapshot(Person person)
        {
            Name = person.Name;
            Address = person.Address;
        }
    }
}
