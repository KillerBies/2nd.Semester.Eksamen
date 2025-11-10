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
        public Medarbejder Medarbejder { get; private set; }
        public string Medarbejder_Fornavn { get; private set; }
        public string Medarbejder_Efternavn { get; private set; }
        public Adresse Medarbejder_Adresse { get; private set; }
        public TimeSpan Varighed { get; private set; }
        public List<Produkt> Produkter { get; private set; }
    }
}
