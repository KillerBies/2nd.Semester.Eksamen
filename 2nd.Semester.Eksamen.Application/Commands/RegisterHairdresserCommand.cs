using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public record RegisterHairdresserCommand(
        string Name,
        Address Address,
        string PhoneNumber,
        string Email,
        EmployeeType Type,
        TreatmentType Specialty,
        ExperienceLevels ExperienceLevel,
        Gender Gender,
        int Age,
        decimal BasePriceMultiplier
    );
}
