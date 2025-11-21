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
        // Description value for EnumExtensions.cs helper method to use to take string from enum value
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
