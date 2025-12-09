using NUnit.Framework;
using System;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Domain.Test.DomainTests.PersonTests
{
    // Dummy class to test abstract Person
    public class TestPerson : Person
    {
        public TestPerson(string name, Address address, string phone, string email)
            : base(name, address, phone, email)
        { }
    }

    [TestFixture]
    public class PersonDomainTests
    {
        private Address CreateAddress()
        {
            return new Address("Odense", "5100", "Odense Vej", "11");
        }

        [Test]
        public void TrySetName_Should_Work_With_ValidName()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetName("Jeff");
            Assert.That(result, Is.True);
            Assert.That(person.Name, Is.EqualTo("Jeff"));
        }

        [Test]
        public void TrySetName_Should_Reject_InvalidName()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetName("Albert52524!$");
            Assert.That(result, Is.False);
            Assert.That(person.Name, Is.EqualTo("John"));
        }

        [Test]
        public void TrySetPhoneNumber_Should_Work_With_ValidNumber()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetPhoneNumber("87654321");
            Assert.That(result, Is.True);
            Assert.That(person.PhoneNumber, Is.EqualTo("87654321"));
        }

        [Test]
        public void TrySetPhoneNumber_Should_Reject_InvalidNumber()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetPhoneNumber("1234abcd");
            Assert.That(result, Is.False);
            Assert.That(person.PhoneNumber, Is.EqualTo("12345678"));
        }

        [Test]
        public void TrySetPhoneNumber_Should_Reject_WrongLength()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetPhoneNumber("1234567");
            Assert.That(result, Is.False);
            Assert.That(person.PhoneNumber, Is.EqualTo("12345678"));
        }

        [Test]
        public void TrySetEmail_Should_Work_With_ValidEmail()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetEmail("John400@example.com");
            Assert.That(result, Is.True);
            Assert.That(person.Email, Is.EqualTo("John400@example.com"));
        }

        [Test]
        public void TrySetEmail_Should_Reject_InvalidEmail()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetEmail("Johnexample.com");
            Assert.That(result, Is.False);
            Assert.That(person.Email, Is.EqualTo("John.Doe@test.com"));
        }

        [Test]
        public void TrySetEmail_Should_Reject_EmailWithSpaces()
        {
            var person = new TestPerson("John", CreateAddress(), "12345678", "John.Doe@test.com");
            bool result = person.TrySetEmail("John.Doe @example.com");
            Assert.That(result, Is.False);
            Assert.That(person.Email, Is.EqualTo("John.Doe@test.com"));
        }
    }
}
