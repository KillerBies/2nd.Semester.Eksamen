using _2nd.Semester.Eksamen.Application;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using _2nd.Semester.Eksamen.WebUi.Components.Shared;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class BookingForm
    {
        [Inject]
        private BookingFormService BookingFormService { get; set; }

        private BookingDTO Booking = new();
        private List<TreatmentDTO> AllTreatments = new();
        private List<EmployeeDTO> AllEmployees = new();
        private EditContext EditContext;
        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(Booking);
            await GetData();
        }
        private async Task GetData()
        {
            AllTreatments = (await BookingFormService.GetAllTreatmentsAsync()).ToList();
            AllEmployees = (await BookingFormService.GetAllEmployeesAsync()).ToList();
        }

        private void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit Called...");
        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        private void RemoveTreatment(TreatmentBookingDTO treatment)
        {
            Booking.TreatmentBookingDTOs.Remove(treatment);
        }

        private void AddTreatment()
        {
            Booking.TreatmentBookingDTOs.Add(new TreatmentBookingDTO());
        }
    }
}
