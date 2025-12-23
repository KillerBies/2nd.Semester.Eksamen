using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using WebUIServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages
{
    public partial class CreateEmployee
    {
        [Inject]
        private CreateEmployeeCommand Command { get; set; }

        [Inject]
        public EmployeeSpecialtyService SpecialtyService { get; set; }
        [Inject] public ITreatmentService _treatmentService { get; set; }

        [Inject] NavigationManager Nav { get; set; }
        protected EmployeeInputDTO Employee { get; set; } = new();
        public class SpecialtyItem
        {
            public string Specialty { get; set; }
            public bool Status { get; set; }
        }
        private List<SpecialtyItem> specialtyItems = new();
        List<string> specialties = new();
        List<string> manuallyAddedSpecialties = new();
        string newSpecialty = "";
        void AddSpecialty()
        {
            if (!string.IsNullOrWhiteSpace(newSpecialty))
            {
                manuallyAddedSpecialties.Add(newSpecialty.Trim());
                newSpecialty = "";
            }
        }
        void RemoveSpecialty(string item)
        {
            manuallyAddedSpecialties.Remove(item);
        }

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
        protected override async Task OnInitializedAsync()
        {
            specialties = await _treatmentService.GetAllUniqueSpecialtiesAsync();
            specialtyItems = specialties.Select(s => new SpecialtyItem() { Specialty = s }).ToList();
        }

        public void RemoveSpecialty(Guid id)
        {
            SpecialtyService.RemoveSpecialty(Input, id);
        }


    }
}
