using _2nd.Semester.Eksamen.Domain.Entities.Personer;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Behandling : Base_Produkt
    {
        public Kunde Kunde { get; private set; }
        public Medarbejder Ansvarlig_Medarbejder { get; private set; }
        public TimeSpan Varighed { get; private set; }
        public Behandlings_Type Behandlings_Type { get; private set; }
        public TimeSpan Estimeret_Tidsforbrug { get;  set; }
        public Medarbejder Medarbejder { get; set; }
    }
}
