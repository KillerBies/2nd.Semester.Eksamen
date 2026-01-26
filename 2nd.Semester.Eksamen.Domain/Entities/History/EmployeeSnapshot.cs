using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record EmployeeSnapshot : BaseSnapshot
    {
        public string Name { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public decimal BasePriceMultiplier { get; protected set; }
        
        public EmployeeSnapshot(Employee employee) : base(employee.RefrenceId)
        {
            Name = employee.Name + employee.LastName;
            BasePriceMultiplier = employee.BasePriceMultiplier;
            PhoneNumber = employee.PhoneNumber;
        }
        public EmployeeSnapshot() 
        { 
        }
    }
}
