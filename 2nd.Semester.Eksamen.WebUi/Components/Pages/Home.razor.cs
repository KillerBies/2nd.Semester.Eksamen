using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.InfrastructureServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class Home
    {
        [Inject] private ICustomerRepository _CustomerRepository { get; set; }
        [Inject] private ITreatmentBookingRepository _treatmentBookingRepository { get; set; }
        [Inject] private ITreatmentRepository _treatmentRepository { get; set; }
        [Inject] private IEmployeeRepository _employeeRepository { get; set; }
        [Inject] private BookingQueryService _bookingQueryService { get; set; }
        [Inject] private BookingApplicationService _bookingApplicationService { get; set; }
        [Inject] private IProductRepository productRepository { get; set; }
        [Inject] private IDiscountRepository discountRepository { get; set; }

        //List of bookings For frontpage
        private List<BookingDTO> bookings { get; set; } = new();

        //Current time for calculations and showing
        private DateTime CurrentTime => DateTime.Now;

        //Stats
        private int TreatmentCount => bookings.Select(b => b.TreatmentBookingDTOs.Count).Sum();
        private int CustomerCount => bookings.Select(b => b.Customer.id).Distinct().Count();
        private int BookingsCount => bookings.Count();


        //For cancel booking warning
        private bool ShowBookingWarning { get; set; } = false;

        //For booking Payment sidepage
        private bool ShowBookingPayment { get; set; } = false;

        //Booking Selected on cancel or show details
        private BookingDTO selectedBooking { get; set; } = new();

        //Booking selected on payment (including from the details window)
        private BookingDTO PayBooking { get; set; }

        //error message for showing
        private string errorMessage { get; set; } = "";
        public bool Loading { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            //Try get the bookings not yet started. If non make error message no bookings found, Else if exception error message becomes "No connection to database"
            try
            {
                bookings = await _bookingQueryService.GetUpcomingBookingsAsync();
                if (!bookings.Any()) errorMessage = "Ingen Bookinger Blev Fundet.";
            }
            catch
            {
                errorMessage = "Ingen forbindelse til databasen.";
            }
        }

        //For when user clicks pay. Initializes pay window
        private async void Pay(BookingDTO booking)
        {
            PayBooking = booking;
            ShowBookingPayment = true;
        }

        //for refreshing page when payment is done or something else happened that requires a refresh to update page
        public void Refresh()
        {
            Navi.Refresh(true);
        }

        //to get booking warning when user presses cancel
        public void GetBookingWarning(BookingDTO booking)
        {
            ShowBookingWarning = true;
            selectedBooking = booking;
        }

        //on confirmation of delete from cancelation window
        private async Task ConfirmCancelBooking()
        {
            try
            {
                await _bookingApplicationService.CancelBookingAsync(selectedBooking);
                ShowBookingWarning = false;
                Refresh();
            }
            catch
            {
                errorMessage = "Noget gik galt i forbindelse med at aflyse bookingen!";
            }
        }

        //TEMP// 
        //Only for demo use to showcase layout and the like.
        public async Task InjectData()
        {
            // Loyalty discounts
            var loyaltyDiscounts = new List<LoyaltyDiscount>
    {
        new LoyaltyDiscount { Name = "Bronze Loyalty", DiscountType = "Bronze", MinimumVisits = 5 },
        new LoyaltyDiscount { Name = "Silver Loyalty", DiscountType = "Silver", MinimumVisits = 10 },
        new LoyaltyDiscount { Name = "Gold Loyalty", DiscountType = "Gold", MinimumVisits = 20 }
    };

            // Campaigns
            var campaigns = new List<Campaign>
    {
        new Campaign { Name = "Winter Campaign", Start = new DateTime(2025,12,1), End = new DateTime(2026,2,28), Description = "Winter Campaign" },
        new Campaign { Name = "Spring Campaign", Start = new DateTime(2026,3,1), End = new DateTime(2026,5,31), Description = "Spring Campaign" },
        new Campaign { Name = "Summer Campaign", Start = new DateTime(2026,6,1), End = new DateTime(2026,8,31), Description = "Summer Campaign" },
        new Campaign { Name = "Autumn Campaign", Start = new DateTime(2026,9,1), End = new DateTime(2026,11,30), Description = "Autumn Campaign" }
    };

            // Private customers
            var privateCustomers = new List<PrivateCustomer>
            { new ("Andersen", Gender.Male,   new DateOnly(1985, 3, 12), "Hans",
            new Address("København", "2100", "Østerbrogade", "24"),
            "22334455", "hans.andersen@example.com", "", false),

        new ("Larsen", Gender.Female, new DateOnly(1990, 7, 5), "Marie",
            new Address("Aarhus", "8000", "Banegårdsgade", "11"),
            "33445566", "marie.larsen@example.com", "Prefers morning appointments", true),

        new ("Nielsen", Gender.Male, new DateOnly(1978, 11, 23), "Peter",
            new Address("Odense", "5000", "Vestergade", "7"),
            "44556677", "peter.nielsen@example.com", "", false),

        new ("Hansen", Gender.Female, new DateOnly(1995, 1, 17), "Anna",
            new Address("Aalborg", "9000", "Algade", "3"),
            "55667788", "anna.hansen@example.com", "Allergic to some products", false),

        new ("Christensen", Gender.Male, new DateOnly(1982, 9, 2), "Lars",
            new Address("Roskilde", "4000", "Skomagergade", "19"),
            "66778899", "lars.christensen@example.com", "", true),

        new ("Pedersen", Gender.Female, new DateOnly(2000, 5, 29), "Sofie",
            new Address("Esbjerg", "6700", "Torvegade", "42"),
            "77889900", "sofie.pedersen@example.com", "Student", false),

        new ("Madsen", Gender.Male, new DateOnly(1988, 4, 14), "Jens",
            new Address("Vejle", "7100", "Hovedgade", "8"),
            "88990011", "jens.madsen@example.com", "", false),

        new ("Jørgensen", Gender.Female, new DateOnly(1992, 12, 8), "Ida",
            new Address("Horsens", "8700", "Parkvej", "15"),
            "99001122", "ida.jorgensen@example.com", "", false),

        new ("Olsen", Gender.Male, new DateOnly(1975, 6, 30), "Thomas",
            new Address("Silkeborg", "8600", "Søndergade", "21"),
            "20112233", "thomas.olsen@example.com", "", true),

        new ("Mortensen", Gender.Female, new DateOnly(1987, 10, 9), "Emma",
            new Address("Helsingør", "3000", "Strandvejen", "5"),
            "21223344", "emma.mortensen@example.com", "", false),

        new ("Knudsen", Gender.Male, new DateOnly(1991, 2, 18), "Oliver",
            new Address("Fredericia", "7000", "Gothersgade", "10"),
            "22335544", "oliver.knudsen@example.com", "", false),

        new ("Rasmussen", Gender.Female, new DateOnly(1984, 8, 27), "Laura",
            new Address("Næstved", "4700", "Ringstedgade", "33"),
            "23446655", "laura.rasmussen@example.com", "", true),

        new ("Kristensen", Gender.Male, new DateOnly(1998, 3, 6), "Emil",
            new Address("Randers", "8900", "Houmeden", "14"),
            "24557766", "emil.kristensen@example.com", "", false),

        new ("Johansen", Gender.Female, new DateOnly(1993, 11, 19), "Amalie",
            new Address("Kolding", "6000", "Slotsgade", "9"),
            "25668877", "amalie.johansen@example.com", "", false),

        new ("Michelsen", Gender.Male, new DateOnly(1980, 5, 1), "Mathias",
            new Address("Herning", "7400", "Bredgade", "18"),
            "26779988", "mathias.michelsen@example.com", "", true),

        new ("Poulsen", Gender.Female, new DateOnly(2001, 9, 14), "Nanna",
            new Address("Hillerød", "3400", "Torvet", "6"),
            "27880099", "nanna.poulsen@example.com", "New customer", false),

        new ("Lindberg", Gender.Male, new DateOnly(1986, 7, 22), "Sebastian",
            new Address("Holbæk", "4300", "Ahlgade", "27"),
            "28991100", "sebastian.lindberg@example.com", "", false),

        new ("Friis", Gender.Female, new DateOnly(1997, 4, 3), "Freja",
            new Address("Svendborg", "5700", "Møllergade", "12"),
            "29112233", "freja.friis@example.com", "", false),

        new ("Svendsen", Gender.Male, new DateOnly(1979, 12, 25), "Alexander",
            new Address("Hjørring", "9800", "Østergade", "20"),
            "30223344", "alexander.svendsen@example.com", "", true),

        new ("Lund", Gender.Female, new DateOnly(1989, 6, 11), "Liva",
            new Address("Taastrup", "2630", "Hovedgaden", "4"),
            "31334455", "liva.lund@example.com", "", false)
    };

            // Employees
            var employees = new List<Employee>
    { new("Mikkel", "Jensen", "mikkel.jensen@firma.dk", "20112233",
        new Address("København", "2100", "Østerbrogade", "10"),
        1.20m, "Junior", "Stylist",
        "Haircut, Styling", "Male",
        new TimeOnly(8, 0), new TimeOnly(16, 0)),

        new("Sara", "Larsen", "sara.larsen@firma.dk", "21113344",
            new Address("Aarhus", "8000", "Banegårdsgade", "5"),
            1.35m, "Mid", "Stylist",
            "Coloring, Highlights", "Female",
            new TimeOnly(9, 0), new TimeOnly(17, 0)),

        new("Jonas", "Nielsen", "jonas.nielsen@firma.dk", "22224455",
            new Address("Odense", "5000", "Vestergade", "12"),
            1.50m, "Senior", "Stylist",
            "Haircut, Beard trim", "Male",
            new TimeOnly(8, 0), new TimeOnly(16, 0)),

        new("Emma", "Hansen", "emma.hansen@firma.dk", "23335566",
            new Address("Aalborg", "9000", "Algade", "3"),
            1.25m, "Mid", "Therapist",
            "Massage, Facial", "Female",
            new TimeOnly(10, 0), new TimeOnly(18, 0)),

        new("Lucas", "Christensen", "lucas.christensen@firma.dk", "24446677",
            new Address("Roskilde", "4000", "Skomagergade", "18"),
            1.40m, "Senior", "Stylist",
            "Keratin, Straightening", "Male",
            new TimeOnly(9, 0), new TimeOnly(17, 0)),

        new("Clara", "Pedersen", "clara.pedersen@firma.dk", "25557788",
            new Address("Esbjerg", "6700", "Torvegade", "22"),
            1.15m, "Junior", "Therapist",
            "Manicure, Pedicure", "Female",
            new TimeOnly(8, 0), new TimeOnly(15, 0)),

        new("Frederik", "Madsen", "frederik.madsen@firma.dk", "26668899",
            new Address("Vejle", "7100", "Hovedgade", "9"),
            1.45m, "Senior", "Manager",
            "Bridal Styling", "Male",
            new TimeOnly(7, 0), new TimeOnly(15, 0)),

        new("Sofie", "Jørgensen", "sofie.jorgensen@firma.dk", "27779900",
            new Address("Horsens", "8700", "Parkvej", "14"),
            1.30m, "Mid", "Stylist",
            "Extensions, Coloring", "Female",
            new TimeOnly(9, 0), new TimeOnly(17, 0)),

        new("Oscar", "Lund", "oscar.lund@firma.dk", "28880011",
            new Address("Silkeborg", "8600", "Søndergade", "6"),
            1.10m, "Junior", "Stylist",
            "Kids Haircut", "Male",
            new TimeOnly(8, 0), new TimeOnly(15, 0)),

        new("Ida", "Svendsen", "ida.svendsen@firma.dk", "29991122",
            new Address("Helsingør", "3000", "Strandvejen", "20"),
            1.55m, "Senior", "Therapist",
            "Hot Stone Massage", "Female",
            new TimeOnly(10, 0), new TimeOnly(18, 0)),

        new("Oliver", "Olsen", "oliver.olsen@firma.dk", "30112233",
            new Address("Fredericia", "7000", "Gothersgade", "8"),
            1.25m, "Mid", "Stylist",
            "Beard trim, Styling", "Male",
            new TimeOnly(8, 0), new TimeOnly(16, 0)),

        new("Laura", "Mortensen", "laura.mortensen@firma.dk", "31223344",
            new Address("Næstved", "4700", "Ringstedgade", "31"),
            1.40m, "Senior", "Stylist",
            "Coloring, Perm", "Female",
            new TimeOnly(9, 0), new TimeOnly(17, 0)),

        new("Emil", "Andersen", "emil.andersen@firma.dk", "32334455",
            new Address("Randers", "8900", "Houmeden", "13"),
            1.20m, "Mid", "Stylist",
            "Scalp Treatment", "Male",
            new TimeOnly(8, 0), new TimeOnly(16, 0)),

        new("Amalie", "Knudsen", "amalie.knudsen@firma.dk", "33445566",
            new Address("Kolding", "6000", "Slotsgade", "4"),
            1.35m, "Mid", "Therapist",
            "Facial, Relaxing", "Female",
            new TimeOnly(10, 0), new TimeOnly(18, 0)),

        new("Mathias", "Rasmussen", "mathias.rasmussen@firma.dk", "34556677",
            new Address("Herning", "7400", "Bredgade", "17"),
            1.50m, "Senior", "Manager",
            "Team Leadership", "Male",
            new TimeOnly(7, 0), new TimeOnly(15, 0)),

        new("Nanna", "Kristensen", "nanna.kristensen@firma.dk", "35667788",
            new Address("Hillerød", "3400", "Torvet", "6"),
            1.10m, "Junior", "Therapist",
            "Eyelash Extension", "Female",
            new TimeOnly(9, 0), new TimeOnly(16, 0)),

        new("Sebastian", "Johansen", "sebastian.johansen@firma.dk", "36778899",
            new Address("Holbæk", "4300", "Ahlgade", "26"),
            1.45m, "Senior", "Stylist",
            "Highlights, Coloring", "Male",
            new TimeOnly(8, 0), new TimeOnly(16, 0)),

        new("Freja", "Michelsen", "freja.michelsen@firma.dk", "37889900",
            new Address("Svendborg", "5700", "Møllergade", "11"),
            1.30m, "Mid", "Stylist",
            "Styling, Bridal Styling", "Female",
            new TimeOnly(9, 0), new TimeOnly(17, 0)),

        new("Alexander", "Poulsen", "alexander.poulsen@firma.dk", "38990011",
            new Address("Hjørring", "9800", "Østergade", "19"),
            1.55m, "Senior", "Manager",
            "Operations", "Male",
            new TimeOnly(7, 0), new TimeOnly(15, 0)),

        new("Liva", "Lindberg", "liva.lindberg@firma.dk", "39001122",
            new Address("Taastrup", "2630", "Hovedgaden", "2"),
            1.25m, "Mid", "Stylist",
            "Haircut, Styling", "Female",
            new TimeOnly(10, 0), new TimeOnly(18, 0))
    };

            var companyCustomers = new List<CompanyCustomer>
    {  new("Nordic Solutions", "12345678",
            new Address("København", "2100", "Østerbrogade", "12"),
            "33112233", "contact@nordicsolutions.dk", "", true),

        new("Aarhus Tech", "23456789",
            new Address("Aarhus", "8000", "Banegårdsgade", "4"),
            "33223344", "info@aarhustech.dk", "", true),

        new("Copenhagen Consulting", "34567890",
            new Address("København", "1600", "Vesterbrogade", "25"),
            "33334455", "office@cphconsulting.dk", "", true),

        new("Odense Logistics", "45678901",
            new Address("Odense", "5000", "Vestergade", "18"),
            "33445566", "contact@odenselogistics.dk", "", true),

        new("Aalborg Industries", "56789012",
            new Address("Aalborg", "9000", "Algade", "7"),
            "33556677", "info@aalborgindustries.dk", "", true),

        new("Roskilde Media", "67890123",
            new Address("Roskilde", "4000", "Skomagergade", "22"),
            "33667788", "hello@roskildemedia.dk", "", true),

        new("Esbjerg Energy", "78901234",
            new Address("Esbjerg", "6700", "Torvegade", "9"),
            "33778899", "info@esbjergenergy.dk", "", true),

        new("Vejle Design", "89012345",
            new Address("Vejle", "7100", "Hovedgade", "14"),
            "33889900", "contact@vejledesign.dk", "", true),

        new("Horsens Transport", "90123456",
            new Address("Horsens", "8700", "Parkvej", "3"),
            "33990011", "booking@horsenstransport.dk", "", true),

        new("Silkeborg IT", "11223344",
            new Address("Silkeborg", "8600", "Søndergade", "6"),
            "34112233", "support@silkeborgit.dk", "", true),

        new("Odense Hair Co.", "22334455",
            new Address("Odense", "5000", "Strøget", "1"),
            "34223344", "info@odensehairco.dk", "Salon partner", true),

        new("Copenhagen Beauty Ltd.", "33445566",
            new Address("København", "1050", "Gammel Kongevej", "31"),
            "34334455", "sales@cphbeauty.dk", "", true),

        new("Aarhus Spa & Wellness", "44556677",
            new Address("Aarhus", "8000", "Frederiksberg Allé", "17"),
            "34445566", "info@aarhusspa.dk", "Corporate wellness", true),

        new("Nordic Hair Supplies", "55667788",
            new Address("Kolding", "6000", "Bredgade", "20"),
            "34556677", "orders@nordichairsupplies.dk", "", true),

        new("Vejle Cosmetics", "66778899",
            new Address("Vejle", "7100", "Amagerbrogade", "8"),
            "34667788", "info@vejlecosmetics.dk", "", false),

        new("Silkeborg Salon Equipment", "77889900",
            new Address("Silkeborg", "8600", "Ågade", "5"),
            "34778899", "sales@salonequipment.dk", "", true),

        new("Horsens Skincare Solutions", "88990011",
            new Address("Horsens", "8700", "Prinsensgade", "16"),
            "34889900", "info@horsensskincare.dk", "", true),

        new("Roskilde Makeover", "99001122",
            new Address("Roskilde", "4000", "Kattesundet", "11"),
            "34990011", "contact@roskildemakeover.dk", "", false),

        new("Esbjerg Beauty Products", "10112233",
            new Address("Esbjerg", "6700", "Slotsgade", "23"),
            "35112233", "info@esbjergbeauty.dk", "", true),

        new("Aalborg Hair Care", "12123344",
            new Address("Aalborg", "9000", "Østergade", "19"),
            "35223344", "contact@aalborghaircare.dk", "", true)
    };
            var products = new List<Product>
    {
        new Product("Shampoo", 100m, "Standard quality") { Category = "Hair" },
        new Product("Conditioner", 110m, "For daily use") { Category = "Hair" },
        new Product("Hair Gel", 120m, "Professional use") { Category = "Hair" },
        new Product("Hair Spray", 130m, "Long-lasting effect") { Category = "Hair" },
        new Product("Beard Oil", 150m, "Nourishing ingredients") { Category = "Hair" },
        new Product("Facial Cream", 200m, "For sensitive skin") { Category = "Skin" },
        new Product("Hand Cream", 90m, "Moisturizing effect") { Category = "Skin" },
        new Product("Body Lotion", 180m, "Sulfate-free") { Category = "Skin" },
        new Product("Nail Polish", 70m, "For shiny nails") { Category = "Nails" },
        new Product("Makeup Kit", 250m, "Luxury product") { Category = "Beauty" },
        new Product("Hair Mask", 220m, "Revitalizing formula") { Category = "Hair" },
        new Product("Keratin Serum", 300m, "Professional use") { Category = "Hair" },
        new Product("Hair Mousse", 120m, "Quick absorption") { Category = "Hair" },
        new Product("Hair Wax", 130m, "For daily use") { Category = "Hair" },
        new Product("Hair Pomade", 140m, "Premium quality") { Category = "Hair" },
        new Product("Leave-in Conditioner", 150m, "Moisturizing effect") { Category = "Hair" },
        new Product("Hair Oil", 180m, "Nourishing ingredients") { Category = "Hair" },
        new Product("Facial Cleanser", 200m, "For sensitive skin") { Category = "Skin" },
        new Product("Sunscreen Lotion", 170m, "Standard quality") { Category = "Skin" },
        new Product("Cuticle Oil", 80m, "For daily use") { Category = "Nails" }
    };
            var treatments = new List<Treatment>
    {  new("Haircut", 150m, "Standard service", "Hair", TimeSpan.FromMinutes(30), new List<string> { "Haircut" }),
        new("Beard Trim", 100m, "Quick service", "Hair", TimeSpan.FromMinutes(20), new List<string> { "Beard trim" }),
        new("Hair Coloring", 350m, "Professional styling", "Hair", TimeSpan.FromMinutes(90), new List<string> { "Coloring", "Styling" }),
        new("Styling", 200m, "Premium service", "Hair", TimeSpan.FromMinutes(45), new List<string> { "Styling" }),
        new("Facial", 250m, "Relaxing and rejuvenating", "Spa", TimeSpan.FromMinutes(60), new List<string> { "Facial" }),
        new("Manicure", 120m, "Child-friendly service", "Nails", TimeSpan.FromMinutes(30), new List<string> { "Manicure" }),
        new("Pedicure", 130m, "Revitalizing experience", "Nails", TimeSpan.FromMinutes(40), new List<string> { "Pedicure" }),
        new("Hair Treatment", 300m, "Gentle on hair", "Hair", TimeSpan.FromMinutes(60), new List<string> { "Haircut", "Hair Treatment" }),
        new("Makeup", 220m, "Perfect for events and weddings", "Beauty", TimeSpan.FromMinutes(50), new List<string> { "Styling" }),
        new("Hair Highlights", 400m, "Long-lasting results", "Hair", TimeSpan.FromMinutes(90), new List<string> { "Highlights", "Coloring" }),
        new("Deep Conditioning", 280m, "Expertly done", "Hair", TimeSpan.FromMinutes(60), new List<string> { "Keratin", "Hair Treatment" }),
        new("Scalp Treatment", 270m, "Customized treatment", "Hair", TimeSpan.FromMinutes(45), new List<string> { "Scalp Treatment" }),
        new("Keratin Treatment", 380m, "Luxury service", "Hair", TimeSpan.FromMinutes(90), new List<string> { "Keratin" }),
        new("Eyebrow Shaping", 110m, "Standard service", "Beauty", TimeSpan.FromMinutes(20), new List<string> { "Eyebrow Shaping" }),
        new("Eyelash Extension", 300m, "Premium service", "Beauty", TimeSpan.FromMinutes(60), new List<string> { "Eyelash Extension" }),
        new("Hot Stone Massage", 400m, "Relaxing and rejuvenating", "Spa", TimeSpan.FromMinutes(60), new List<string> { "Massage" }),
        new("Hair Extensions", 450m, "Perfect for events and weddings", "Hair", TimeSpan.FromMinutes(120), new List<string> { "Extensions" }),
        new("Hair Straightening", 350m, "Professional styling", "Hair", TimeSpan.FromMinutes(90), new List<string> { "Straightening" }),
        new("Bridal Hair Styling", 500m, "Expertly done", "Bridal", TimeSpan.FromMinutes(120), new List<string> { "Bridal Styling" }),
        new("Kids Haircut", 90m, "Child-friendly service", "Kids", TimeSpan.FromMinutes(25), new List<string> { "Kids Haircut" })
    };

            Loading = true;
            foreach(var cus in privateCustomers)
            {
                await _CustomerRepository.CreateNewAsync(cus);
            }
            foreach(var cus in companyCustomers)
            {
                await _CustomerRepository.CreateNewAsync(cus);
            }
            foreach(var pro in products)
            {
                await productRepository.CreateNewAsync(pro);
            }
            foreach(var treat in treatments)
            {
                await _treatmentRepository.CreateNewAsync(treat);
            }
            foreach(var emp in employees)
            {
                await _employeeRepository.CreateNewAsync(emp);
            }
            foreach(var discount in campaigns)
            {
                await discountRepository.CreateNewAsync(discount);
            }
            foreach(var discount in loyaltyDiscounts)
            {
                await discountRepository.CreateNewAsync(discount);
            }

            Loading = false;






        }

    }
}