using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Kampagne: Base_Rabat
    {
        public DateTime StartDato { get; private set; }
        public DateTime SlutDato { get; private set; }
    }
}
