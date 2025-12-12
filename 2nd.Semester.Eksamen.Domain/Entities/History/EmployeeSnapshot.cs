using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record EmployeeSnapshot : BaseSnapshot
    {
        public string Name { get; set; }
        public decimal BasePriceMultiplier { get; set; }
        public EmployeeSnapshot() 
        { 
        }
    }
}
