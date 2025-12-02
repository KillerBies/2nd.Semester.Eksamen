using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO
{
    public class WaitListDTO
    {
        public bool WantsToBeNotified { get; set; }
        public List<AvailableDayDTO> AvailableDays { get; set; } = new List<AvailableDayDTO>();
    }
}
