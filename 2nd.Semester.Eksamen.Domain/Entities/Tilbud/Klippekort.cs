using _2nd.Semester.Eksamen.Domain.Entities.Personer;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Klippekort : Base_Entity
    {
        public Kunde Kunde { get; private set; }
        public int AntalKlip { get; private set; }
        public Behandling Behandling { get; private set; }
    }
}
