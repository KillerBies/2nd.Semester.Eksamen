using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public abstract class Person : Base_Entity
    {
        public Adresse Adresse { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Telefonnummer { get; set; }
        public string Email { get; set; }
    }
}
