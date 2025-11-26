using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.WebUi.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static _2nd.Semester.Eksamen.Application.DTO.EmployeeUpdateDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{


    public partial class EmployeeEdit
    {
        [Parameter] public int Id { get; set; }
        [Inject]
        private UpdateEmployeeCommand Command { get; set; }

        [Inject] IEmployeeService EmployeeService { get; set; }

        [Inject] NavigationManager Nav { get; set; }
        [Inject] private IEmployeeRepository _repo { get; set; }

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

            var employee = await _repo.GetByIDAsync(Id);

            if (employee != null)
            {
                Input.FirstName = employee.Name;
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
            }



            Input.BasePriceMultiplier = employee.BasePriceMultiplier;
                // Convert string -> enum
                Input.Gender = Enum.GetValues<Gender>()
                    .FirstOrDefault(g => g.GetDescription() == employee.Gender);

                Input.Type = Enum.GetValues<EmployeeType>()
                    .FirstOrDefault(t => t.GetDescription() == employee.Type);

                Input.ExperienceLevel = Enum.GetValues<ExperienceLevels>()
                    .FirstOrDefault(e => e.GetDescription() == employee.ExperienceLevel);

                if (employee.Address != null)
                {
                    Input.Address.StreetName = employee.Address.StreetName;
                    Input.Address.HouseNumber = employee.Address.HouseNumber;
                    Input.Address.City = employee.Address.City;
                    Input.Address.PostalCode = employee.Address.PostalCode;
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
