using _2nd.Semester.Eksamen.Domain.Entities.Personer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Booking : Base_Entity
    {
        public Kunde Kunde { get; set; }
        public Medarbejder Medarbejder { get; set; }
        public DateTime BookingStart { get; private set; }
        public DateTime BookingSlut { get; private set; }
        public decimal Pris { get; set; }
        public decimal Rabat { get; set; }
        public decimal Total { get; set; }
        public List<Behandling> Behandlinger { get; set; }
        public bool UdkørendeService { get; set; }
        //metoder skal laves til at sætte dato og tidspunkt korrekt så det ikke overlapper med andre bookinger eller sigselv
    }
}
