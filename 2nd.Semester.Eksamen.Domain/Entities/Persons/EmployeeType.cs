using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public enum EmployeeType 
    {
        [Description("Freelance")]
        Freelance,
        [Description("Staff")]
        Staff,
        [Description("Apprentice")]
        Apprentice
    }
}
