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
        public DateTime BookingStart { get; private set; }
        public DateTime BookingSlut { get; private set; }
        public decimal Total { get; private set; }
        public List<Behandling> Behandlinger { get; private set; }
    }
}
