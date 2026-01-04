using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class TreatmentHistoryDTO
    {
        public int OrderId { get; set; }
        public Guid OrderGuid { get; set; }
        public Guid BookingGuid { get; set; }
        public int BookingId { get; set; }
        public DateOnly Date { get; set; }
        public Guid TreatmentGuid { get; set; }
        public int TreatmentSnapsohtId { get; set; }
        public string TreatmentName { get; set; }
        public decimal Price { get; set; }
        public string EmployeeName { get; set; }
        public string Category { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string CustomerName { get; set; }
        public Guid CustomerGuid { get; set; }
        public TreatmentHistoryDTO(TreatmentSnapshot treatmentSnapshot, OrderSnapshot os)
        {
            TreatmentGuid = treatmentSnapshot.Guid;
            TreatmentSnapsohtId = treatmentSnapshot.Id;
            TreatmentName = treatmentSnapshot.Name;
            Price = treatmentSnapshot.PricePerUnit;
            EmployeeName = treatmentSnapshot.EmployeeName;
            EmployeeGuid = treatmentSnapshot.EmployeeGuid;
            CustomerName = os.BookingSnapshot.CustomerSnapshot.Name;
            CustomerGuid = os.BookingSnapshot.CustomerSnapshot.Guid;
            OrderId = os.Id;
            Date = os.DateOfPayment;
            OrderGuid = os.Guid;
            BookingGuid = os.BookingSnapshot.Guid;
            Duration = treatmentSnapshot.Duration;
            Category = treatmentSnapshot.Category;
            BookingId = os.BookingSnapshot.Id;
        }
    }
}
