using NUnit.Framework;
using System;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace DomainTests
{
    [TestFixture]
    public class AddressDomainTests
    {
        private Address CreateAddress()
        {
            return new Address("Kolding", "6000", "Vejle Vej", "16");
        }

        [Test]
        public void TrySetCity_Should_Update_When_Valid()
        {
            var address = CreateAddress();
            bool result = address.TrySetCity("Odense");

            Assert.That(result, Is.True);
            Assert.That(address.City, Is.EqualTo("Odense"));
        }

        [Test]
        public void TrySetCity_Should_Reject_Empty()
        {
            var address = CreateAddress();
            bool result = address.TrySetCity("");

            Assert.That(result, Is.False);
            Assert.That(address.City, Is.EqualTo("Kolding"));
        }

        [Test]
        public void TrySetPostalCode_Should_Update_When_Valid()
        {
            var address = CreateAddress();
            bool result = address.TrySetPostalCode("5000");

            Assert.That(result, Is.True);
            Assert.That(address.PostalCode, Is.EqualTo("5000"));
        }

        [Test]
        public void TrySetPostalCode_Should_Reject_Empty()
        {
            var address = CreateAddress();
            bool result = address.TrySetPostalCode("   ");

            Assert.That(result, Is.False);
            Assert.That(address.PostalCode, Is.EqualTo("6000"));
        }

        [Test]
        public void TrySetStreetName_Should_Update_When_Valid()
        {
            var address = CreateAddress();
            bool result = address.TrySetStreetName("Main Street");

            Assert.That(result, Is.True);
            Assert.That(address.StreetName, Is.EqualTo("Main Street"));
        }

        [Test]
        public void TrySetStreetName_Should_Reject_Empty()
        {
            var address = CreateAddress();
            bool result = address.TrySetStreetName("");

            Assert.That(result, Is.False);
            Assert.That(address.StreetName, Is.EqualTo("Vejle Vej"));
        }

        [Test]
        public void TrySetHouseNumber_Should_Update_When_Valid()
        {
            var address = CreateAddress();
            bool result = address.TrySetHouseNumber("12");

            Assert.That(result, Is.True);
            Assert.That(address.HouseNumber, Is.EqualTo("12"));
        }

        [Test]
        public void TrySetHouseNumber_Should_Reject_Empty()
        {
            var address = CreateAddress();
            bool result = address.TrySetHouseNumber(" ");

            Assert.That(result, Is.False);
            Assert.That(address.HouseNumber, Is.EqualTo("16"));
        }

        [Test]
        public void UpdateCity_Should_Throw_When_Invalid()
        {
            var address = CreateAddress();
            Assert.Throws<ArgumentException>(() => address.UpdateCity(""));
        }

        [Test]
        public void UpdatePostalCode_Should_Throw_When_Invalid()
        {
            var address = CreateAddress();
            Assert.Throws<ArgumentException>(() => address.UpdatePostalCode("   "));
        }

        [Test]
        public void UpdateStreetName_Should_Throw_When_Invalid()
        {
            var address = CreateAddress();
            Assert.Throws<ArgumentException>(() => address.UpdateStreetName(""));
        }

        [Test]
        public void UpdateHouseNumber_Should_Throw_When_Invalid()
        {
            var address = CreateAddress();
            Assert.Throws<ArgumentException>(() => address.UpdateHouseNumber(" "));
        }

        [Test]
        public void UpdateMethods_Should_Work_With_Valid_Input()
        {
            var address = CreateAddress();

            address.UpdateCity("Aarhus");
            address.UpdatePostalCode("8000");
            address.UpdateStreetName("Banegårdsgade");
            address.UpdateHouseNumber("20");

            Assert.That(address.City, Is.EqualTo("Aarhus"));
            Assert.That(address.PostalCode, Is.EqualTo("8000"));
            Assert.That(address.StreetName, Is.EqualTo("Banegårdsgade"));
            Assert.That(address.HouseNumber, Is.EqualTo("20"));
        }
    }
}
