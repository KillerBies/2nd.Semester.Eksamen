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
    public class Company: Customer
    {
        //Customer who is a company
        public string CVRNumber { get; private set; }

        public Company(
            string name,
            Address address,
            string phoneNumber,
            string email,
            string notes,
            string cvrNumber
            ) : base (name, address, phoneNumber, email, notes)
        {
            CVRNumber = cvrNumber;
        }
    }
}
