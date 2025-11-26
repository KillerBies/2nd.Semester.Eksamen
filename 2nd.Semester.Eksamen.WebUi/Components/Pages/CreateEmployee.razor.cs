using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.WebUi.Services;
using Microsoft.AspNetCore.Components;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class CreateEmployee
    {
        [Inject]
        private CreateEmployeeCommand Command { get; set; }

        [Inject]
        public EmployeeSpecialtyService SpecialtyService { get; set; }

        protected EmployeeInputDTO Employee { get; set; } = new();

        protected EmployeeInputDTO Input { get; set; } = new EmployeeInputDTO
        {
            Address = new AddressInputDTO(),
            Type = EmployeeType.Staff, // Default value
            Appointments = new List<Appointment>(),
            TreatmentHistory = new List<Treatment>(),
            BasePriceMultiplier = 1.0m, // default

        };

        protected async Task CreateEmployeeAsync()
        {
            await Command.ExecuteAsync(Input);

        }
        public void AddSpecialty()
        {
            SpecialtyService.AddSpecialty(Input);
        }

        public void RemoveSpecialty(Guid id)
        {
            SpecialtyService.RemoveSpecialty(Input, id);
        }


    }
}
