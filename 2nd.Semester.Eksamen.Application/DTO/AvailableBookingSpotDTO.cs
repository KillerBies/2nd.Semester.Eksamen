using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class AvailableBookingSpotDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<TreatmentBookingDTO> Treatments { get; set; }
        public bool ContainsWantedEmployees { get; set; }
    }
}
