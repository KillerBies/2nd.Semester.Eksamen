using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Base_Rabat : Base_Entity
    {
        public bool Status { get; private set; }
        public decimal RabatProcent { get; private set; }
    }
}
