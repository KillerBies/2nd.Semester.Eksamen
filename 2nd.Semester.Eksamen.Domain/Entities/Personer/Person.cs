using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public abstract class Person : Base_Entity
    {
        public Adresse Adresse { get; private set; }
        public string Fornavn { get; private set; }
        public string Efternavn { get; private set; }
        public string Telefonnummer { get; private set; }
        public string Email { get; private set; }
        public string Køn { get; private set; }
        public int Alder { get; private set; }
    }
}
