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

        private List<Customer> GenerateRandomCustomers(int count)
        {
            var random = new Random();
            int Private = random.Next(0, count + 1);
            List<Customer> cus = new();
            cus.AddRange(GenerateRandomPrivateCustomer(Private));
            cus.AddRange(GenerateRandomCompanyCustomers(count - Private));
            return cus;
        }
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
            Loading = true;

            foreach (var emp in GenerateRandomEmployees(50))
            {
                await _employeeRepository.CreateNewAsync(emp);
            }
            foreach (var comcus in GenerateRandomCustomers(50))
            {
                await _CustomerRepository.CreateNewAsync(comcus);
            }
            foreach (var treat in GenerateRandomTreatments(50))
            {
                await _treatmentRepository.CreateNewAsync(treat);
            }
            foreach (var prod in GenerateRandomProducts(50))
            {
                await productRepository.CreateNewAsync(prod);
            }
            foreach (var dis in GenerateRandomLoyaltyDiscounts(50))
            {
                await discountRepository.CreateNewAsync(dis);
            }
            foreach (var dis in GenerateRandomDiscoutns(50))
            {
                await discountRepository.CreateNewAsync(dis);
            }
            Loading = false;
        }


        private List<PrivateCustomer> GenerateRandomPrivateCustomer(int count)
        {
            var random = new Random();

            string[] firstNames = {
    "Hans", "Marie", "Peter", "Anna", "Lars", "Sofie", "Jens", "Ida", "Thomas", "Emma",
    "Oliver", "Laura", "Emil", "Amalie", "Mathias", "Nanna", "Sebastian", "Freja", "Alexander", "Liva",
    "Morten", "Camilla", "Rasmus", "Julie", "Christian", "Clara", "Nicklas", "Sara", "Victor", "Maja"
};

            string[] lastNames = {
    "Andersen", "Larsen", "Nielsen", "Hansen", "Christensen", "Pedersen", "Madsen", "Jørgensen", "Lund", "Svendsen",
    "Olsen", "Mortensen", "Knudsen", "Rasmussen", "Kristensen", "Johansen", "Michelsen", "Poulsen", "Lindberg", "Friis"
};

            string[] cities = {
    "København", "Aarhus", "Odense", "Aalborg", "Roskilde", "Esbjerg", "Vejle", "Horsens", "Silkeborg", "Helsingør",
    "Fredericia", "Næstved", "Randers", "Kolding", "Herning", "Hillerød", "Holbæk", "Svendborg", "Hjørring", "Taastrup"
};

            string[] streets = {
    "Østerbrogade", "Banegårdsgade", "Vestergade", "Algade", "Nørrebrogade", "Skomagergade", "Torvegade", "Hovedgade", "Parkvej", "Søndergade",
    "Frederiksberg Allé", "Vesterbrogade", "Gammel Kongevej", "Bredgade", "Strøget", "Amagerbrogade", "Ågade", "Prinsensgade", "Kattesundet", "Slotsgade"
};


            var customers = new List<PrivateCustomer>();


            for (int i = 0; i < count; i++)
            {
                var firstname = firstNames[random.Next(firstNames.Length)];
                var lastname = lastNames[random.Next(lastNames.Length)];
                var city = cities[random.Next(cities.Length)];
                var street = streets[random.Next(streets.Length)];
                var houseNumber = random.Next(1, 100).ToString();
                var postalCode = (1000 + random.Next(9000)).ToString();


                var address = new Address(city, postalCode, street, houseNumber);


                var gender = (Gender)random.Next(0, 2);



                var year = DateTime.Now.Year - random.Next(18, 81);
                var month = random.Next(1, 13);
                var day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
                var birthdate = new DateOnly(year, month, day);


                var phoneNumber = random.Next(20000000, 99999999).ToString();
                var email = firstname.ToLower().Replace(" ", ".") + "." + lastname.ToLower() + "@example.com";


                var notes = "";
                var isVip = random.NextDouble() < 0.1;


                customers.Add(new PrivateCustomer(
                lastname,
                gender,
                birthdate,
                firstname,
                address,
                phoneNumber,
                email,
                notes,
                isVip
                ));
            }
            return customers.DistinctBy(c => c.Name + c.LastName).ToList();
        }



        private List<Employee> GenerateRandomEmployees(int count)
        {
            var random = new Random();


            string[] firstNames = {
    "Mikkel", "Sara", "Jonas", "Emma", "Lucas", "Clara", "Frederik", "Sofie", "Oscar", "Ida",
    "Oliver", "Laura", "Emil", "Amalie", "Mathias", "Nanna", "Sebastian", "Freja", "Alexander", "Liva"
};

            string[] lastNames = {
    "Jensen", "Larsen", "Nielsen", "Hansen", "Christensen", "Pedersen", "Madsen", "Jørgensen", "Lund", "Svendsen",
    "Olsen", "Mortensen", "Andersen", "Knudsen", "Rasmussen", "Kristensen", "Johansen", "Michelsen", "Poulsen", "Lindberg"
};

            string[] cities = {
    "København", "Aarhus", "Odense", "Aalborg", "Roskilde", "Esbjerg", "Vejle", "Horsens", "Silkeborg", "Helsingør",
    "Fredericia", "Næstved", "Randers", "Kolding", "Herning", "Hillerød", "Holbæk", "Svendborg", "Hjørring", "Taastrup"
};

            string[] streets = {
    "Østerbrogade", "Banegårdsgade", "Vestergade", "Algade", "Nørrebrogade", "Skomagergade", "Torvegade", "Hovedgade", "Parkvej", "Søndergade",
    "Frederiksberg Allé", "Vesterbrogade", "Gammel Kongevej", "Bredgade", "Strøget", "Amagerbrogade", "Ågade", "Prinsensgade", "Kattesundet", "Slotsgade"
};

            string[] specialtiesList = {
    "Haircut", "Beard trim", "Styling", "Coloring", "Massage", "Facial",
    "Manicure", "Pedicure", "Highlights", "Keratin", "Scalp Treatment",
    "Extensions", "Straightening", "Relaxing", "Perm", "Bridal Styling",
    "Kids Haircut", "Eyelash Extension", "Eyebrow Shaping", "Hot Stone Massage"
};
            var employees = new List<Employee>();


            for (int i = 0; i < count; i++)
            {
                var firstname = firstNames[random.Next(firstNames.Length)];
                var lastname = lastNames[random.Next(lastNames.Length)];
                var city = cities[random.Next(cities.Length)];
                var street = streets[random.Next(streets.Length)];
                var houseNumber = random.Next(1, 100).ToString();
                var postalCode = (1000 + random.Next(9000)).ToString();


                var address = new Address(city, postalCode, street, houseNumber);


                var basePriceMultiplier = Math.Round(1.1m + (decimal)random.NextDouble() * 0.5m, 2);


                var experience = ((ExperienceLevels)random.Next(0, 4)).ToString();
                var type = ((EmployeeType)random.Next(0, 3)).ToString();
                var gender = ((Gender)random.Next(0, 2)).ToString();


                var specialties = string.Join(", ", specialtiesList.OrderBy(x => random.Next()).Take(random.Next(1, 4)));


                var workStartHour = random.Next(7, 11);
                var workStart = new TimeOnly(workStartHour, 0);
                var workEnd = new TimeOnly(workStartHour + random.Next(7, 9), 0);


                employees.Add(new Employee(
                firstname,
                lastname,
                firstname.ToLower() + "." + lastname.ToLower() + "@firma.dk",
                random.Next(20000000, 99999999).ToString(),
                address,
                basePriceMultiplier,
                experience,
                type,
                specialties,
                gender,
                workStart,
                workEnd
                ));
            }
            return employees.DistinctBy(c => c.Name + c.LastName + c.Type).ToList();
        }
        private List<CompanyCustomer> GenerateRandomCompanyCustomers(int count)
        {
            var random = new Random();

            string[] companyNames = {
    "Nordic Solutions", "Aarhus Tech", "Copenhagen Consulting", "Odense Logistics",
    "Aalborg Industries", "Roskilde Media", "Esbjerg Energy", "Vejle Design",
    "Horsens Transport", "Silkeborg IT", "Odense Hair Co.", "Copenhagen Beauty Ltd.",
    "Aarhus Spa & Wellness", "Nordic Hair Supplies", "Vejle Cosmetics",
    "Silkeborg Salon Equipment", "Horsens Skincare Solutions", "Roskilde Makeover",
    "Esbjerg Beauty Products", "Aalborg Hair Care"
};

            string[] cities = {
    "København", "Aarhus", "Odense", "Aalborg", "Roskilde", "Esbjerg",
    "Vejle", "Horsens", "Silkeborg", "Helsingør", "Fredericia", "Næstved",
    "Randers", "Kolding", "Herning", "Hillerød", "Holbæk", "Svendborg",
    "Hjørring", "Taastrup"
};

            string[] streets = {
    "Østerbrogade", "Banegårdsgade", "Vestergade", "Algade", "Nørrebrogade",
    "Skomagergade", "Torvegade", "Hovedgade", "Parkvej", "Søndergade",
    "Frederiksberg Allé", "Vesterbrogade", "Gammel Kongevej", "Bredgade",
    "Strøget", "Amagerbrogade", "Ågade", "Prinsensgade", "Kattesundet", "Slotsgade"
};


            var companyCustomers = new List<CompanyCustomer>();


            for (int i = 0; i < count; i++)
            {
                var companyName = companyNames[random.Next(companyNames.Length)];
                var city = cities[random.Next(cities.Length)];
                var street = streets[random.Next(streets.Length)];
                var houseNumber = random.Next(1, 100).ToString();
                var postalCode = (1000 + random.Next(9000)).ToString();


                var address = new Address(city, postalCode, street, houseNumber);


                var phoneNumber = random.Next(20000000, 99999999).ToString();
                var email = companyName.ToLower().Replace(" ", "") + "@example.com";


                var cvrNumber = random.Next(10000000, 99999999).ToString();


                var notes = "";
                var saveAsCustomer = random.NextDouble() < 0.8;


                companyCustomers.Add(new CompanyCustomer(
                companyName,
                cvrNumber,
                address,
                phoneNumber,
                email,
                notes,
                saveAsCustomer
                ));
            }
            return companyCustomers.DistinctBy(c => c.Name).ToList();
        }

        private List<Treatment> GenerateRandomTreatments(int count)
        {
            var random = new Random();


            string[] treatmentNames = {
    "Haircut", "Beard Trim", "Hair Coloring", "Styling", "Facial",
    "Manicure", "Pedicure", "Hair Treatment", "Makeup", "Hair Highlights",
    "Deep Conditioning", "Scalp Treatment", "Keratin Treatment", "Eyebrow Shaping",
    "Eyelash Extension", "Hot Stone Massage", "Hair Extensions", "Hair Straightening",
    "Bridal Hair Styling", "Kids Haircut", "Hair Relaxing", "Hair Perm", "Facial Peel"
};

            string[] categories = {
    "Hair", "Spa", "Beauty", "Nails", "Skin", "Bridal", "Kids"
};

            string[] specialties = {
    "Haircut", "Beard trim", "Styling", "Coloring", "Massage", "Facial",
    "Manicure", "Pedicure", "Highlights", "Keratin", "Scalp Treatment",
    "Extensions", "Straightening", "Relaxing", "Perm", "Bridal Styling",
    "Kids Haircut", "Eyelash Extension", "Eyebrow Shaping", "Hot Stone Massage"
};

            string[] descriptions = {
    "Standard service", "Premium service", "Quick service", "Luxury service",
    "Customized treatment", "Professional styling", "Gentle on hair",
    "Relaxing and rejuvenating", "Special care for sensitive skin",
    "Long-lasting results", "Perfect for events and weddings",
    "Child-friendly service", "Expertly done", "Revitalizing experience"
};


            var treatments = new List<Treatment>();


            for (int i = 0; i < count; i++)
            {
                var name = treatmentNames[random.Next(treatmentNames.Length)];
                var price = Math.Round(100 + (decimal)random.NextDouble() * 400, 2);
                var description = descriptions[random.Next(descriptions.Length)];
                var category = categories[random.Next(categories.Length)];
                var duration = TimeSpan.FromMinutes(random.Next(15, 121));
                var requiredSpecialties = specialties.OrderBy(x => random.Next()).Take(random.Next(1, 4)).ToList();


                treatments.Add(new Treatment(name, price, description, category, duration, requiredSpecialties));
            }
            return treatments.DistinctBy(c => c.Name + c.Category).ToList();
        }


        private List<Product> GenerateRandomProducts(int count)
        {
            var random = new Random();


            string[] productNames = {
    "Shampoo", "Conditioner", "Hair Gel", "Hair Spray", "Beard Oil",
    "Facial Cream", "Hand Cream", "Body Lotion", "Nail Polish", "Makeup Kit",
    "Hair Mask", "Keratin Serum", "Hair Mousse", "Hair Wax", "Hair Pomade",
    "Leave-in Conditioner", "Hair Oil", "Facial Cleanser", "Sunscreen Lotion",
    "Cuticle Oil", "Hair Brush", "Hair Comb", "Curling Cream", "Hair Perfume"
};

            string[] descriptions = {
    "Standard quality", "Premium quality", "For daily use", "Luxury product",
    "For sensitive skin", "Professional use", "Revitalizing formula", "Sulfate-free",
    "Moisturizing effect", "Color-safe", "For shiny hair", "For healthy scalp",
    "Quick absorption", "Long-lasting effect", "Nourishing ingredients"
};
            string[] categories = {
    "Hair", "Spa", "Beauty", "Nails", "Skin", "Bridal", "Kids"
};

            var products = new List<Product>();


            for (int i = 0; i < count; i++)
            {
                var name = productNames[random.Next(productNames.Length)];
                var price = Math.Round(50 + (decimal)random.NextDouble() * 250, 2);
                var description = descriptions[random.Next(descriptions.Length)];
                var category = categories[random.Next(categories.Length)];

                products.Add(new Product(name, price, description) {Category=category});
            }
            return products.DistinctBy(c => c.Name + c.Category).ToList(); ;
        }

        private List<LoyaltyDiscount> GenerateRandomLoyaltyDiscounts(int count)
        {
            var random = new Random();


            var loyaltyDiscounts = new List<LoyaltyDiscount>();
            var discountTypes = Enum.GetValues(typeof(LoyaltyDiscountType));


            for (int i = 0; i < discountTypes.Length; i++)
            {
                var discountType = (LoyaltyDiscountType)discountTypes.GetValue(i);
                var minimumVisits = random.Next(5, 51);
                var treatmentDiscount = (decimal)random.NextDouble();
                var productDiscount = (decimal)random.NextDouble();


                var name = discountType.ToString() + " Loyalty Discount";


                loyaltyDiscounts.Add(new LoyaltyDiscount(
                minimumVisits,
                discountType.ToString(),
                name,
                treatmentDiscount,
                productDiscount
                )
                { AppliesToTreatment = random.Next(0, 2) == 0, AppliesToProduct = random.Next(0, 2) == 0});
            }
            return loyaltyDiscounts;
        }


        private List<Campaign> GenerateRandomDiscoutns(int count)
        {
            var random = new Random();


            string[] campaignNames = {
    "Spring Sale", "Summer Promo", "Autumn Special", "Winter Discount",
    "Holiday Offer", "Black Friday", "New Year Sale", "Valentine's Deal",
    "Easter Promo", "Back to School", "Mother's Day Special", "Father's Day Offer",
    "Halloween Special", "Anniversary Promo", "Loyalty Week", "Flash Sale",
    "Weekend Special", "Student Discount Week", "Bridal Season Offer", "Winter Glow Campaign"
};
            var campaigns = new List<Campaign>();


            for (int i = 0; i < count; i++)
            {
                var name = campaignNames[random.Next(campaignNames.Length)];
                var treatmentDiscount = (decimal)random.NextDouble();
                var productDiscount = (decimal)random.NextDouble();

                var start = DateTime.Now.AddDays(random.Next(0, 365));
                var end = start.AddDays(random.Next(1, 31));


                campaigns.Add(new Campaign(name, treatmentDiscount, productDiscount, start, end) { AppliesToProduct= random.Next(0, 2) == 0,AppliesToTreatment= random.Next(0, 2) == 0,});
            }
            return campaigns;
        }
    }
}