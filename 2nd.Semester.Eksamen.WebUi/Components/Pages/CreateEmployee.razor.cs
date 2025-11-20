using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using Microsoft.AspNetCore.Components;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class CreateEmployee
    {
        [Inject]
        private CreateEmployeeCommand Command { get; set; }

        protected EmployeeInputModel Employee { get; set; } = new();

        protected EmployeeInputModel Input { get; set; } = new EmployeeInputModel
        {
            Address = new AddressInputModel(),
            Type = EmployeeType.Staff, // Default value
            Appointments = new List<Appointment>(),
            TreatmentHistory = new List<Treatment>(),
            BasePriceMultiplier = 1.0m, // default

        };

        protected async Task CreateEmployeeAsync()
        {
            await Command.ExecuteAsync(Input);

        }

    }
}
