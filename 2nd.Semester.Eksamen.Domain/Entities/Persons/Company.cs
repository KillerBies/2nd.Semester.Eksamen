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
    public class Company: Customer
    {
        //Customer who is a company
        public string CVRNumber { get; private set; }

    }
}
