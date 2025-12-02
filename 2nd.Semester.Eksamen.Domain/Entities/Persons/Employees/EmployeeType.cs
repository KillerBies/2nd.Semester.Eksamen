using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees
{
    public enum EmployeeType 
    {
        // Description value for EnumExtensions.cs helper method to use to take string from enum value
        [Description("Freelance")]
        Freelance,
        [Description("Staff")]
        Staff,
        [Description("Apprentice")]
        Apprentice
    }
}
