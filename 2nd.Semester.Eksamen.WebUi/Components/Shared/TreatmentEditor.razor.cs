using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Mono.TextTemplating;
using MudBlazor.Interfaces;
using System.Text.RegularExpressions;

namespace _2nd.Semester.Eksamen.WebUi.Components.Shared
{
    public partial class TreatmentEditor
    {

        //If is edit then initialice the lists for choices on initialization
        //Make page on submit that gives a summary with an ok button
        [Parameter] public EventCallback<TreatmentBookingDTO> OnRemove { get; set; }
        [Parameter] public EventCallback<TreatmentBookingDTO> OnSelectedParameterChange { get; set; }
        [Parameter] public EventCallback<TreatmentBookingDTO> OnChange { get; set; }
        
        [Parameter] public List<TreatmentDTO> PossibleTreatments { get; set; }
        public List<TreatmentDTO> SelectableTreatments { get; set; } = new();
        
        [Parameter] public List<EmployeeDTO> PossibleEmployees { get; set; }
        public List<EmployeeDTO> SelectableEmployees { get; set; } = new();

        [Parameter] public TreatmentBookingDTO TreatmentBooking { get; set; }
        [Parameter] public EventCallback<int> OnUp { get; set; }
        [Parameter] public EventCallback<int> OnDown { get; set; }
        [Parameter] public bool IsEdit { get; set; }
        [Parameter] public int Index { get; set; }
        [CascadingParameter] EditContext EditContext { get; set; }

        private TreatmentDTO selectedTreatment => PossibleTreatments.FirstOrDefault(pt => pt.TreatmentId == TreatmentBooking.Treatment.TreatmentId);
        private EmployeeDTO selectedEmployee => PossibleEmployees.FirstOrDefault(pe => pe.EmployeeId == TreatmentBooking.Employee.EmployeeId);

        protected override async Task OnInitializedAsync()
        {
            if(IsEdit)
            {
                SelectableTreatments = PossibleTreatments.Where(s => s.Category == TreatmentBooking.Treatment.Category).ToList();
                SelectableEmployees = PossibleEmployees.Where(e => TreatmentBooking.Treatment.RequiredSpecialties.All(tr => e.Specialties.Contains(tr))).ToList();
            }
        }
        private void OnCategoryChanged()
        {

            TreatmentBooking.Employee.EmployeeId = 0;
            TreatmentBooking.Treatment.TreatmentId = 0;
            TreatmentBooking.Price = 0;
            if (TreatmentBooking.Treatment.Category == "")
            {
                SelectableTreatments.Clear();
                SelectableEmployees.Clear();
                Change();
                return;
            }
            SelectableTreatments = PossibleTreatments.Where(s => s.Category == TreatmentBooking.Treatment.Category).ToList();

            Change();
        }


        private void OnTreatmentChanged()
        {
            var treatment = selectedTreatment;
            TreatmentBooking.Employee.EmployeeId = 0;
            TreatmentBooking.Price = 0;
            if (treatment == null)
            {
                SelectableEmployees.Clear();
                Change();
                return;
            }
            TreatmentBooking.Treatment.Name = treatment.Name;
            TreatmentBooking.Treatment.Category = treatment.Category;
            TreatmentBooking.Treatment.RequiredSpecialties = treatment.RequiredSpecialties;
            TreatmentBooking.Treatment.BasePrice = treatment.BasePrice;
            TreatmentBooking.Treatment.Duration = treatment.Duration;
            SelectableEmployees = PossibleEmployees.Where(e => TreatmentBooking.Treatment.RequiredSpecialties.All(tr => e.Specialties.Contains(tr))).ToList();

            Change();
        }

        private void OnEmployeeChanged()
        {
            var employee = selectedEmployee;
            if (employee == null)
            {
                Change();
                return;
            }
            TreatmentBooking.Employee.ExperienceLevel = employee.ExperienceLevel;
            TreatmentBooking.Employee.Name = employee.Name;
            TreatmentBooking.Employee.BasePriceMultiplier = employee.BasePriceMultiplier;
            TreatmentBooking.Employee.EmployeeId = employee.EmployeeId;
            TreatmentBooking.UpdatePrice();
            Change();
        }


        private async Task UpPress() => await OnUp.InvokeAsync(Index);
        private async Task DownPress() => await OnDown.InvokeAsync(Index);

        private async Task Remove() => await OnRemove.InvokeAsync(TreatmentBooking);
        private async Task NotifyChange()
        {
            await OnSelectedParameterChange.InvokeAsync(TreatmentBooking);
        }
        private void Change()
        {
            OnChange.InvokeAsync(TreatmentBooking);
        }

    }
}


