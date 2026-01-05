using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO
{
    public class EmployeeDTO
    {
        public Guid EmployeeGuid { get; set; }
        public int EmployeeId { get; set; } = 0;
        public string Name { get; set; } = null!;
        public string ExperienceLevel { get; set; } = "";
        public decimal BasePriceMultiplier { get; set; } = 1;
        public string Specialties { get; set; }

        public EmployeeDTO(Employee emp) 
        {
            if(emp != null)
            {
                EmployeeId = emp.Id;
                Name = emp.Name;
                ExperienceLevel = emp.ExperienceLevel;
                BasePriceMultiplier = emp.BasePriceMultiplier;
                Specialties = emp.Specialties;
                EmployeeGuid = emp.Guid;
            }
        }
        public EmployeeDTO(EmployeeDTO emp)
        {
            EmployeeId = emp.EmployeeId;
            Name = emp.Name;
            ExperienceLevel = emp.ExperienceLevel;
            BasePriceMultiplier = emp.BasePriceMultiplier;
            Specialties = emp.Specialties;
        }
        public EmployeeDTO(TreatmentSnapshot ts)
        {
            EmployeeGuid = ts.EmployeeGuid;
            Name = ts.EmployeeName;
        }
        public EmployeeDTO() { }
    }
}
