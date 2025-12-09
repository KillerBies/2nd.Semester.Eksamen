using NUnit.Framework;
using System;
using System.Linq;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;

namespace DomainTests
{
    [TestFixture]
    public class EmployeeTests
    {
        private Employee CreateEmployee()
        {
            return new Employee(
                "John",                             // firstname
                "Doe",                             // lastname
                "John.Doe@example.com",                 // email
                "12345678",                         // phoneNumber
                new Address("Vejle", "7100", "Kolding Vej", "41"), // address
                1.0m,                               // basePriceMultiplier
                "Expert",                           // experience
                "Hair",                             // type
                "Massage",                           // specialties
                "Male",                            // gender
                new TimeOnly(9, 0),                 // workStart
                new TimeOnly(17, 0)                 // workEnd
            );
        }

        private Treatment CreateDummyTreatment()
        {
            return new Treatment(
                "Massage",
                100m,
                "Full body massage",
                "Massage",
                TimeSpan.FromMinutes(60)
            );
        }

        [Test]
        public void TrySetLastName_Should_Set_When_Valid()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetLastName("John", "Butcher");

            Assert.That(result, Is.True);
            Assert.That(emp.LastName, Is.EqualTo("Butcher"));
        }

        [Test]
        public void TrySetLastName_Should_Reject_Invalid()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetLastName("John", "   ");

            Assert.That(result, Is.False);
            Assert.That(emp.LastName, Is.EqualTo("Doe"));
        }

        [Test]
        public void TrySetBasePriceMultiplier_Should_Accept_Positive()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetBasePriceMultiplier(2.5m);

            Assert.That(result, Is.True);
            Assert.That(emp.BasePriceMultiplier, Is.EqualTo(2.5m));
        }

        [Test]
        public void TrySetBasePriceMultiplier_Should_Reject_Negative()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetBasePriceMultiplier(-1);

            Assert.That(result, Is.False);
            Assert.That(emp.BasePriceMultiplier, Is.EqualTo(1.0m));
        }

        [Test]
        public void IsAvailable_Should_Return_True_When_No_Overlap()
        {
            var emp = CreateEmployee();
            var treatment = CreateDummyTreatment();

            emp.Appointments.Add(new TreatmentBooking(
                treatment,
                emp,
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 11, 0, 0)
            ));

            bool available = emp.IsAvailable(
                new DateTime(2025, 1, 1, 11, 0, 0),
                new DateTime(2025, 1, 1, 12, 0, 0)
            );

            Assert.That(available, Is.True);
        }

        [Test]
        public void IsAvailable_Should_Return_False_When_Overlapping()
        {
            var emp = CreateEmployee();
            var treatment = CreateDummyTreatment();

            emp.Appointments.Add(new TreatmentBooking(
                treatment,
                emp,
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 11, 0, 0)
            ));

            bool available = emp.IsAvailable(
                new DateTime(2025, 1, 1, 10, 30, 0),
                new DateTime(2025, 1, 1, 10, 45, 0)
            );

            Assert.That(available, Is.False);
        }

        [Test]
        public void TrySetType_Should_Set_When_Valid()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetType("Staff");

            Assert.That(result, Is.True);
            Assert.That(emp.Type, Is.EqualTo("Staff"));
        }

        [Test]
        public void TrySetSpecialties_Should_Set_When_Valid()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetSpecialties("Haircut");

            Assert.That(result, Is.True);
            Assert.That(emp.Specialties, Is.EqualTo("Haircut"));
        }

        [Test]
        public void TryAddSpecialty_Should_Append_When_Valid()
        {
            var emp = CreateEmployee();
            bool result = emp.TryAddSpecialty("Coloring");

            Assert.That(result, Is.True);
            Assert.That(emp.Specialties, Does.Contain("Coloring"));
        }

        [Test]
        public void TrySetExperience_Should_Set_When_Valid()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetExperience("Senior");

            Assert.That(result, Is.True);
            Assert.That(emp.ExperienceLevel, Is.EqualTo("Senior"));
        }

        [Test]
        public void TrySetGender_Should_Set_When_Valid()
        {
            var emp = CreateEmployee();
            bool result = emp.TrySetGender("Male");

            Assert.That(result, Is.True);
            Assert.That(emp.Gender, Is.EqualTo("Male"));
        }

        [Test]
        public void TrySetAddress_Should_Update_Existing()
        {
            var emp = CreateEmployee();
            emp.TrySetAddress("Aarhus", "8000", "New Street", "10");

            Assert.That(emp.Address.City, Is.EqualTo("Aarhus"));
            Assert.That(emp.Address.PostalCode, Is.EqualTo("8000"));
            Assert.That(emp.Address.StreetName, Is.EqualTo("New Street"));
            Assert.That(emp.Address.HouseNumber, Is.EqualTo("10"));
        }
    }
}
