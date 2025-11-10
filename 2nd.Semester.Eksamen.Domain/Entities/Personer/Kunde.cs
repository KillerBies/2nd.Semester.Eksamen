using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public class Kunde : Person
    {
        public DateOnly Fødselsdag { get; private set; }
        public List<Booking> BesøgHistorik { get; private set; }
        public decimal PointSaldo { get; private set; }
        public string Notater { get; private set; }
        public Dictionary<Behandlings_Type,Klippekort> Klippekort { get; private set; }
    }
}
