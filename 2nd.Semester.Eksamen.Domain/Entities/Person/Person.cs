using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public abstract class Person : Base_Entity
    {
        public Adress Adress { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Phonenumber { get; private set; }
        public string Email { get; private set; }
        public string Notes { get; private set; }
    }
}
