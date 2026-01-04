using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebUIServices;
using static _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO.EmployeeUpdateDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages
{


    public partial class UpdateEmployee : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject]
        private UpdateEmployeeCommand Command { get; set; }

        [Inject] IEmployeeService EmployeeService { get; set; }

        [Inject] NavigationManager Nav { get; set; }

        [Inject]
        public EmployeeSpecialtyService SpecialtyService { get; set; }

        private EmployeeDetailsDTO Employee;

        private string newSpecialty;

        private bool loaded = false;
        protected EmployeeUpdateDTO Input { get; set; } = new EmployeeUpdateDTO
        {

        };
        protected override async Task OnInitializedAsync()
        {

            //var employee = await _repo.GetByIDAsync(Id);
            var employee = await EmployeeService.GetByIdAsync(Id);

            if (employee == null)
            {
                // Handle null employee, e.g., redirect or show message
                Nav.NavigateTo("/employees");
                return;
            }
            Input.FirstName = employee.FirstName;
            Input.LastName = employee.LastName;
            Input.Email = employee.Email;
            Input.PhoneNumber = employee.PhoneNumber;

            Input.Specialties = employee.Specialty?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new SpecialtyItemBase
                {
                    Id = Guid.NewGuid(),
                    Value = x.Trim()
                })
                .ToList()
                ?? new List<SpecialtyItemBase>();
            Input.BasePriceMultiplier = employee.BasePriceMultiplier;

            // Convert string -> enum
            Input.Gender = Enum.GetValues<Gender>()
                .FirstOrDefault(g => g.GetDescription() == employee.Gender);

            Input.Type = Enum.GetValues<EmployeeType>()
                .FirstOrDefault(t => t.GetDescription() == employee.Type);

            Input.ExperienceLevel = Enum.GetValues<ExperienceLevels>()
                .FirstOrDefault(e => e.GetDescription() == employee.Experience);

            if (employee.StreetName != null || employee.HouseNumber != null || employee.City != null || employee.PostalCode != null)
            {
                Input.Address.StreetName = employee.StreetName;
                Input.Address.HouseNumber = employee.HouseNumber;
                Input.Address.City = employee.City;
                Input.Address.PostalCode = employee.PostalCode;
            }
            }
        
        protected async Task UpdateEmployeeAsync()
        {
            await Command.ExecuteAsync(Id, Input);
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
