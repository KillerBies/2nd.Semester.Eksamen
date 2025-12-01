using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public class CreateTreatmentBookingCommand
    {
        public int TreatmentId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public CreateTreatmentBookingCommand(TreatmentBookingDTO treatmentBooking)
        {
            TreatmentId = treatmentBooking.Treatment.TreatmentId;
            EmployeeId = treatmentBooking.Employee.EmployeeId;
            Start = treatmentBooking.Start;
            End = treatmentBooking.End;
        }
    }
}
