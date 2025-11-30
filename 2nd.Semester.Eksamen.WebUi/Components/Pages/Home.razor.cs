using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class Home
    {
        private List<Treatment> treatments = new();
        [Inject] private ICustomerRepository _CustomerRepository {  get; set; }
        [Inject] private ITreatmentBookingRepository _treatmentBookingRepository { get; set; }
        [Inject] private ITreatmentRepository _treatmentRepository { get; set; }
        [Inject] private IEmployeeRepository _employeeRepository { get; set; }
        public async void InjectData()
        {
            var customer1 = new PrivateCustomer("poop",Domain.Entities.Persons.Gender.Male,new DateOnly(2004,2,1),"shit",new Address("oiio","323212","fda","43f"),"12345678","21w@dwq.com");
            var customer2 = new PrivateCustomer("pwop", Domain.Entities.Persons.Gender.Male, new DateOnly(2004, 2, 1), "sfgds", new Address("oiio", "323217", "fda", "43f"), "12345578", "21w@dwq.com");
            var address1 = new Address("New York", "10001", "Main St", "101");
            var address2 = new Address("Los Angeles", "90001", "Elm St", "202");
            var employee1 = new Employee(
    firstname: "Alice",
    lastname: "Johnson",
    email: "alice.johnson@example.com",
    phoneNumber: "55511141",
    address: address1,
    basePriceMultiplier: 1.2m,
    experience: "5 years",
    type: "Technician",
    specialties: new List<string> { "Plumbing", "Heating" },
    gender: "Female",
    workStart: new TimeOnly(8, 0),   // 8:00 AM
    workEnd: new TimeOnly(16, 0)     // 4:00 PM
);
            var employee2 = new Employee(
    firstname: "Bob",
    lastname: "Smith",
    email: "bob.smith@example.com",
    phoneNumber: "45552222",
    address: address2,
    basePriceMultiplier: 1.5m,
    experience: "10 years",
    type: "Electrician",
    specialties: new List<string> { "Wiring", "Lighting" },
    gender: "Male",
    workStart: new TimeOnly(9, 0),   // 9:00 AM
    workEnd: new TimeOnly(17, 0)     // 5:00 PM
);
            var treatment1 = new Treatment(
    name: "Deep Tissue Massage",
    price: 75.00m,
    discription: "Intense massage targeting deep muscle layers to relieve tension.",
    category: "Massage",
    duration: TimeSpan.FromMinutes(60)
);

            var treatment2 = new Treatment(
                name: "Facial Rejuvenation",
                price: 50.00m,
                discription: "Revitalizing facial treatment to cleanse and hydrate skin.",
                category: "Skincare",
                duration: TimeSpan.FromMinutes(45)
            );
            await _CustomerRepository.CreateNewCustomerAsync(customer1);
            await _CustomerRepository.CreateNewCustomerAsync(customer2);
            await _treatmentRepository.CreateNewAsync(treatment1);
            await _treatmentRepository.CreateNewAsync(treatment2);
            await _employeeRepository.CreateNewAsync(employee2);
            await _employeeRepository.CreateNewAsync(employee1);
        }
        private async Task getalldata()
        {
            treatments = (List<Treatment>) await _treatmentRepository.GetAllAsync();
        }
    }
}
