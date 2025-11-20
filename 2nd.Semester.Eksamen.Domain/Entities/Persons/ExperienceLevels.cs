using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public enum ExperienceLevels
    {
        [Description("Apprentice")]
        Apprentice,
        [Description("Junior")]
        Junior,
        [Description("Mid Level")]
        MidLevel,
        [Description("Senior")]
        Senior
    }
}
