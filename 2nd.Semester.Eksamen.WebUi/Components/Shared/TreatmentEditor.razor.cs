using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Mono.TextTemplating;
using System.Text.RegularExpressions;

namespace _2nd.Semester.Eksamen.WebUi.Components.Shared
{
    public partial class TreatmentEditor
    {
        [Parameter] public EventCallback<TreatmentBookingDTO> OnRemove { get; set; }
        [Parameter] public EventCallback<TreatmentBookingDTO> OnSelectedParameterChange { get; set; }
        [Parameter] public EventCallback<TreatmentBookingDTO> OnChange { get; set; }
        [Parameter] public List<TreatmentDTO> PossibleTreatments { get; set; }
        [Parameter] public List<EmployeeDTO> PossibleEmployees { get; set; }
        [Parameter] public TreatmentBookingDTO TreatmentBooking { get; set; }
        [Parameter] public EventCallback<int> OnUp { get; set; }
        [Parameter] public EventCallback<int> OnDown { get; set; }
        [Parameter] public int Index { get; set; }
        [CascadingParameter] EditContext EditContext { get; set; }


        private async Task UpPress() => await OnUp.InvokeAsync(Index);
        private async Task DownPress() => await OnDown.InvokeAsync(Index);

        private async Task Remove() => await OnRemove.InvokeAsync(TreatmentBooking);

        private string SelectedTreatmentCategory
        {
            get => TreatmentBooking.Treatment.Category;
            set
            {
                if (TreatmentBooking.Treatment.Category != value)
                {
                    TreatmentBooking.Treatment.Category = value;
                    SelectedTreatmentId = 0;
                    SelectedEmployeeId = 0;
                    TreatmentBooking.Price = 0;
                }
                Change();
            }
        }

        private int SelectedTreatmentId
        {
            get => TreatmentBooking.Treatment.TreatmentId;
            set
            {
                if (TreatmentBooking.Treatment.TreatmentId != value)
                {
                    TreatmentBooking.Treatment.TreatmentId = value;
                    var selectedTreatment = PossibleTreatments.FirstOrDefault(pt => pt.TreatmentId == value);
                    if (selectedTreatment != null)
                    {
                        TreatmentBooking.Treatment.BasePrice = selectedTreatment.BasePrice;
                        TreatmentBooking.Treatment.Name = selectedTreatment.Name;
                        TreatmentBooking.Treatment.Duration = selectedTreatment.Duration;
                    }
                    SelectedEmployeeId = 0;
                    TreatmentBooking.Price = 0;
                    Change();
                }
            }
        }

        private int SelectedEmployeeId
        {
            get => TreatmentBooking.Employee.EmployeeId;
            set
            {
                if (TreatmentBooking.Employee.EmployeeId != value)
                {
                    TreatmentBooking.Employee.EmployeeId = value;
                    var selectedEmployee = PossibleEmployees.FirstOrDefault(pt => pt.EmployeeId == value);
                    if (selectedEmployee != null)
                    {
                        TreatmentBooking.Employee.BasePriceMultiplier = selectedEmployee.BasePriceMultiplier;
                        TreatmentBooking.Employee.Name = selectedEmployee.Name;
                        TreatmentBooking.Employee.ExperienceLevel = selectedEmployee.ExperienceLevel;
                    }
                    TreatmentBooking.UpdatePrice();
                    Change();
                }
            }
        }
        private async Task NotifyChange()
        {
            await OnSelectedParameterChange.InvokeAsync(TreatmentBooking);
        }
        private bool AnyTreatments()
        {
            return PossibleTreatments.Count() != 0;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine("Render!");
        }
        private void Change()
        {
            OnChange.InvokeAsync(TreatmentBooking);
        }





    }
}


