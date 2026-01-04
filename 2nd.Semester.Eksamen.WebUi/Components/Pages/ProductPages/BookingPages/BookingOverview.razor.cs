using _2nd.Semester.Eksamen.Application;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using Microsoft.AspNetCore.Components;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.BookingPages
{
    public partial class BookingOverview
    {
        [Inject] NavigationManager Navi { get; set; }
        [Inject] IBookingOverviewService _bookingOverviewService { get; set; }

        private List<BookingDTO> bookingList = new();
        private List<BookingDTO> CompletedBookings { get; set; } = new();

        private BookingDTO SelectedBooking { get; set; } = new();
        private BookingSnapshot SelectedCompletedBooking { get; set; }

        private bool toggleBookingWarning = false;
        private bool LoadFailed = false;
        private bool OpenEdit = false;
        private bool ShowDelete = false;
        private bool ShowDetails = false;
        public DetailsContext DeleteContext { get; set; }

        public Stack<DetailsContext> ContextStack { get; set; } = new Stack<DetailsContext>();
        public DetailsContext CurrentContext => ContextStack.Peek();
        private DateTime CurrentTime => DateTime.Now;

        private string SearchTermCustomer = "";
        private DateTime? SearchTermDateStart;
        private DateTime? SearchTermDateEnd;

        private IEnumerable<BookingDTO> FilteredBookings =>
            bookingList
                .Where(b =>
                    string.IsNullOrWhiteSpace(SearchTermCustomer) ||
                    b.Customer.Name.Contains(SearchTermCustomer, StringComparison.OrdinalIgnoreCase))
                .Where(b =>
                    (!SearchTermDateStart.HasValue || b.Start.Date >= SearchTermDateStart.Value.Date) &&
                    (!SearchTermDateEnd.HasValue || b.Start.Date <= SearchTermDateEnd.Value.Date));

        private IEnumerable<BookingDTO> FilterdCompletedBookings =>
            CompletedBookings
                .Where(b =>
                    string.IsNullOrWhiteSpace(SearchTermCustomer) ||
                    b.Customer.Name.Contains(SearchTermCustomer, StringComparison.OrdinalIgnoreCase))
                .Where(b =>
                    (!SearchTermDateStart.HasValue || b.Start>= SearchTermDateStart.Value) &&
                    (!SearchTermDateEnd.HasValue || b.End <= SearchTermDateEnd.Value));

        private int TodayCount =>
            bookingList.Count(b => b.Start.Date == CurrentTime.Date);

        private int PendingCount =>
            bookingList.Count(b => b.Status == BookingStatus.Pending);

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CompletedBookings = (await _bookingOverviewService.GetAllCompletedBookings());
                var Guids = CompletedBookings.Select(b => b.BookingGuid).ToHashSet();
                bookingList = (await _bookingOverviewService.GetAllBookingsAsync()).Where(t => !Guids.Contains(t.BookingGuid)).ToList();
            }
            catch
            {
                LoadFailed = true;
            }
        }



        void DeleteBookingWarning()
        {
            toggleBookingWarning = true;
        }

        private async Task ConfirmDeleteBooking()
        {
            await _bookingOverviewService.DeleteBookingAsync(SelectedBooking);
            Refresh();
        }
        private void Refresh()
        {
            Navi.Refresh(true);
        }
        private async Task BookingSelect(DetailsContext booking)
        {
            ContextStack.Clear();
            ContextStack.Push(booking);
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
        private void OnEditBooking(BookingEditContext context)
        {
            if (context.Booking == null)
                return;
            Navi.NavigateTo($"/BookingForm/{context.Booking.CustomerId}/{context.Booking.BookingId}");
        }
    }
}
