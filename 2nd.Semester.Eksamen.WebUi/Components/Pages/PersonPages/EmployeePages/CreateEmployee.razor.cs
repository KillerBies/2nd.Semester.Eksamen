using _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using Microsoft.AspNetCore.Components;
using WebUIServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Components.Pages.PersonPages.EmployeePages
{
    public partial class CreateEmployee
    {
        [Inject]
        private CreateEmployeeCommand Command { get; set; }

        [Inject]
        public EmployeeSpecialtyService SpecialtyService { get; set; }

        [Inject] NavigationManager Nav { get; set; }
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
            Nav.NavigateTo("/employees");

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
