using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class TreatmentBookingDTO
    {
        public TreatmentDTO Treatment { get; set; } = new();
        public EmployeeDTO Employee { get; set; } = new();
        public DateTime Start { get; set; } = new();
        public DateTime End { get; set; } = new();
        public decimal Price { get; set; } = new();
        public void UpdatePrice(List<TreatmentDTO> allTreatments, List<EmployeeDTO> allEmployees)
        {
            if (Treatment.TreatmentId != 0 && Employee.EmployeeId != 0)
            {
                Price = Employee.BasePriceMultiplier * Treatment.BasePrice;
            }
        }

    }
}
