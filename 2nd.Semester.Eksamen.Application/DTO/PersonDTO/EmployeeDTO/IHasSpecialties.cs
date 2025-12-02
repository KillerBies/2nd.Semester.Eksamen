using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO
{
    public interface IHasSpecialties
    {
        List<SpecialtyItemBase> Specialties { get; set; }
    }
    public class SpecialtyItemBase
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
