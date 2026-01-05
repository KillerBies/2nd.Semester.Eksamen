using _2nd.Semester.Eksamen.Application;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.Services.PersonService;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;
namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.TreatmentPages
{
    public partial class TreatmentOverview
    {

        [Inject] public ITreatmentService _treatmentService { get; set; }
        [Inject] public NavigationManager Navi { get; set; }

        private List<TreatmentDTO> Treatments = new();
        private TreatmentDTO? selectedTreatment;

        private bool CreateTreatment = false;
        private bool EditTreatment = false;
        public bool isVisible = false;
        public int PendingTreatments { get; set; } = 0;
        public int CompleteTreatments { get; set; } = 0;
        private string SearchTermName = "";

        private List<TreatmentDTO> FilterdTreatments =>
            Treatments
                .Where(t =>
                    string.IsNullOrWhiteSpace(SearchTermName) ||
                    t.Name.Contains(SearchTermName, StringComparison.OrdinalIgnoreCase))
                .ToList();

        protected override async Task OnInitializedAsync()
        {
            Treatments = await _treatmentService.GetAllTreatmentsAsDTOAsync();
            CompleteTreatments = await _treatmentService.GetNumberOfCompletedTreatments();
            PendingTreatments = await _treatmentService.GetNumberOfPendingTreatments();
        }

        private void ShowOverlay(TreatmentDTO treatment)
        {
            selectedTreatment = treatment;
            isVisible = true;
        }


        private async Task DeleteTreatment(int id)
        {
            await _treatmentService.DeleteByIdDbAsync(id);
            Refresh();
        }



        private bool toggleBookingWarning = false;
        private bool LoadFailed = false;
        private bool OpenEdit = false;
        private bool ShowDelete = false;
        private bool ShowDetails = false;
        public DetailsContext DeleteContext { get; set; }

        public Stack<DetailsContext> ContextStack { get; set; } = new Stack<DetailsContext>();
        public DetailsContext CurrentContext => ContextStack.Peek();

        private void Refresh()
        {
            Navi.Refresh(true);
        }


        private void OnEditBooking(BookingEditContext context)
        {
            if (context.Booking == null)
                return;
            Navi.NavigateTo($"/BookingForm/{context.Booking.CustomerId}/{context.Booking.BookingId}");
        }

        private async Task Select(TreatmentDTO treatment)
        {
            ContextStack.Clear();
            ContextStack.Push(new TreatmentContext(treatment));
            ShowDetails = true;
        }
        private void Delete(DetailsContext context)
        {
            DeleteContext = context;
            ShowDelete = true;
        }
        private void AddBookingToCustomer(int customerId)
        {
            if (customerId <= 0)
                return;
            Navi.NavigateTo($"/BookingForm/{customerId}");
        }
    }
}
