using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis;
using WebUIServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages
{
    public partial class CreateEmployee
    {
        [Inject] private CreateEmployeeCommand Command { get; set; }
        [Inject] public IEmployeeUpdateService _updateService { get; set; }

        [Inject] public EmployeeSpecialtyService SpecialtyService { get; set; }
        [Inject] public ITreatmentService _treatmentService { get; set; }
        private EmployeeInputDTO Input { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        public string ErrorMessage { get; set; } = "";
        [Parameter] public bool IsEdit { get; set; } = false;
        [Parameter] public int EditEmployeeID { get; set; }
        private string _errorMessage = "";
        private EditContext _editContext;
        private ValidationMessageStore _messageStore;
        public class SpecialtyItem
        {
            public string Specialty { get; set; }
            public bool Status { get; set; }
        }
        private List<SpecialtyItem> specialtyItems = new();
        List<string> specialties = new();
        List<string> manuallyAddedSpecialties = new();
        string newSpecialty = "";


        protected override async Task OnInitializedAsync()
        {
            specialties = await _treatmentService.GetAllUniqueSpecialtiesAsync();
            specialtyItems = specialties.Select(s => new SpecialtyItem() { Specialty = s }).ToList();
            if (IsEdit)
            {
                Input = await _updateService.GetEmployeeInputDTOByIdAsync(EditEmployeeID);
                foreach (var specialty in Input.SpecialtiesList)
                {
                    string editSpecialty = specialty.TrimEnd().Trim(',');
                    var item = specialtyItems.FirstOrDefault(si => si.Specialty == editSpecialty);
                    if (item == null)
                    {
                        manuallyAddedSpecialties.Add(editSpecialty);
                    }
                    else
                    {
                        var specialtyItem = specialtyItems.FirstOrDefault(si => si.Specialty == editSpecialty);
                        if (specialtyItem != null)
                        {
                            specialtyItem.Status = true;
                        }
                    }
                }
            }
            else
            {
                Input = new EmployeeInputDTO
                {
                    Address = new AddressInputDTO(),
                    Type = EmployeeType.Staff, // Default value
                    Appointments = new List<Appointment>(),
                    TreatmentHistory = new List<Treatment>(),
                    BasePriceMultiplier = 1.0m, // default

                };
            }
            _editContext = new EditContext(Input);
            _messageStore = new ValidationMessageStore(_editContext);
        }


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

        protected async Task CreateEmployeeAsync()
        {
            if(Input.WorkEnd <= Input.WorkStart)
            {
                _messageStore.Add(_editContext.Field(nameof(Input.WorkEnd)),"Ugyldig Start og Slut tider: Start skal være før Slut");
                return;
            }
            ErrorMessage = "";
            List<string> requiredSpecialties = new();

            foreach (var specialtyItem in specialtyItems)
            {
                if (specialtyItem.Status == true)
                {

                    requiredSpecialties.Add(specialtyItem.Specialty);
                }
            }
            requiredSpecialties.AddRange(manuallyAddedSpecialties); 
            Input.SpecialtiesList = requiredSpecialties.Select(s => s + ", ").ToList();
            try
            {
                if(IsEdit)
                {
                    _updateService.UpdateEmployee(Input);
                }
                else
                {
                    await Command.ExecuteAsync(Input);
                }
                Nav.NavigateTo("/employees");
                await OnClose.InvokeAsync();
            }
            catch
            {

            }


        }


        public void RemoveSpecialty(Guid id)
        {
            SpecialtyService.RemoveSpecialty(Input, id);
        }


    }
}
