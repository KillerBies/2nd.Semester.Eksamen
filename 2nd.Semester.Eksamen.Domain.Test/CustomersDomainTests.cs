using NUnit.Framework;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;

namespace DomainTests
{
    [TestFixture]
    public class CustomersDomainTests
    {
        private Address CreateVejleAddress()
        {
            return new Address("Vejle", "7100", "Havnegade", "12A");
        }

        private Customer CreateBaseCustomer()
        {
            return new Customer(
                name: "John Doe",
                address: CreateVejleAddress(),
                phoneNumber: "12345678",
                email: "John.Doe@example.com",
                notes: "Test customer",
                saveAsCustomer: true
            );
        }

        private CompanyCustomer CreateCompanyCustomer()
        {
            return new CompanyCustomer(
                name: "John Doe",
                cvrnumber: "12345678",
                address: CreateVejleAddress(),
                phonenumber: "12345678",
                email: "John.Doe@example.com",
                notes: "Company test",
                saveAsCustomer: true
            );
        }

        private PrivateCustomer CreatePrivateCustomer()
        {
            return new PrivateCustomer(
                lastname: "Doe",
                gender: Gender.Female,
                birthday: new DateOnly(2000, 1, 1),
                name: "John",
                address: CreateVejleAddress(),
                phonenumber: "12345678",
                email: "John.Doe@example.com",
                notes: "Private test",
                saveAsCustomer: true
            );
        }

        // BASE CUSTOMER

        [Test]
        public void AddVisit_BaseCustomer_IncrementsVisitCount()
        {
            var customer = CreateBaseCustomer();

            bool success = customer.AddVisit();

            Assert.That(success, Is.True);
            Assert.That(customer.NumberOfVisists, Is.EqualTo(1));
        }

        [Test]
        public void AddVisit_BaseCustomer_MultipleTimes()
        {
            var customer = CreateBaseCustomer();

            customer.AddVisit();
            customer.AddVisit();
            customer.AddVisit();

            Assert.That(customer.NumberOfVisists, Is.EqualTo(3));
        }

        // COMPANY CUSTOMER

        [Test]
        public void AddVisit_CompanyCustomer_IncrementsVisitCount()
        {
            var customer = CreateCompanyCustomer();

            customer.AddVisit();

            Assert.That(customer.NumberOfVisists, Is.EqualTo(1));
        }

        [Test]
        public void AddVisit_CompanyCustomer_MultipleTimes()
        {
            var customer = CreateCompanyCustomer();

            customer.AddVisit();
            customer.AddVisit();

            Assert.That(customer.NumberOfVisists, Is.EqualTo(2));
        }

        // PRIVATE CUSTOMER

        [Test]
        public void AddVisit_PrivateCustomer_IncrementsVisitCount()
        {
            var customer = CreatePrivateCustomer();

            customer.AddVisit();

            Assert.That(customer.NumberOfVisists, Is.EqualTo(1));
        }

        [Test]
        public void AddVisit_PrivateCustomer_MultipleTimes()
        {
            var customer = CreatePrivateCustomer();

            customer.AddVisit();
            customer.AddVisit();
            customer.AddVisit();
            customer.AddVisit();

            Assert.That(customer.NumberOfVisists, Is.EqualTo(4));
        }
    }
}
