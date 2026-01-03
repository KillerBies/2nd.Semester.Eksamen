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




        private async Task Select(TreatmentDTO treatment)
        {
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
