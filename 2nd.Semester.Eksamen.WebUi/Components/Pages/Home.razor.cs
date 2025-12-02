using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class Home
    {
        private List<Treatment> treatments = new();
        [Inject] private ICustomerRepository _CustomerRepository { get; set; }
        [Inject] private ITreatmentBookingRepository _treatmentBookingRepository { get; set; }
        [Inject] private ITreatmentRepository _treatmentRepository { get; set; }
        [Inject] private IEmployeeRepository _employeeRepository { get; set; }
        public async void InjectData()
        {
            var customer1 = new PrivateCustomer("poop", Domain.Entities.Persons.Gender.Male, new DateOnly(2004, 2, 1), "shit", new Address("oiio", "323212", "fda", "43f"), "12345678", "21w@dwq.com", "", false);
            var customer2 = new PrivateCustomer("pwop", Domain.Entities.Persons.Gender.Male, new DateOnly(2004, 2, 1), "sfgds", new Address("oiio", "323217", "fda", "43f"), "12345578", "21w@dwq.com","",false);
            var address1 = new Address("New York", "10001", "Main St", "101");
            var address2 = new Address("Los Angeles", "90001", "Elm St", "202");
            var employee1 = new Employee("Alice", "Johnson", "alice.johnson@example.com", "55511141", address1, 1.2m, "5 years", "Technician", "POOPING, SHITTING, PISSING, CUMMING", "Female", new TimeOnly(08, 0), new TimeOnly(16,0));
            var employee2 = new Employee("Bob", "Smith", "bob.smith@example.com", "45552222", address2, 1.5m, "10 years", "Electrician", "POOPING, SHITTING, PISSING, CUMMING, joking, gooning, pulling my finger and farting", "Male", new TimeOnly(09,0), new TimeOnly(17,0));
            var treatment1 = new Treatment("Deep Tissue Massage", 75.00m, "Intense massage targeting deep muscle layers to relieve tension.", "Massage", TimeSpan.FromMinutes(60));

            var treatment2 = new Treatment("Facial Rejuvenation", 50.00m, "Revitalizing facial treatment to cleanse and hydrate skin.", "Skincare", TimeSpan.FromMinutes(45));
            await _CustomerRepository.CreateNewCustomerAsync(customer1);
            await _CustomerRepository.CreateNewCustomerAsync(customer2);
            await _treatmentRepository.CreateNewAsync(treatment1);
            await _treatmentRepository.CreateNewAsync(treatment2);
            await _employeeRepository.CreateNewAsync(employee2);
            await _employeeRepository.CreateNewAsync(employee1);
        }
        private async Task getalldata()
        {
            treatments = (List<Treatment>)await _treatmentRepository.GetAllAsync();
        }
    }
}
