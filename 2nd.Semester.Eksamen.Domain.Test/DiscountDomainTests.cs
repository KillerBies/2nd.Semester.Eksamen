using NUnit.Framework;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;

namespace DomainTests
{
    [TestFixture]
    public class DiscountDomainTests
    {
        [Test]
        public void GetDiscountAmountFor_WithTreatment_ReturnsTreatmentDiscount()
        {
            var discount = new Discount("Test Discount", treatmentDiscount: 25m, productDiscount: 10m);
            var treatment = new Treatment();

            decimal amount = discount.GetDiscountAmountFor(treatment);

            Assert.That(amount, Is.EqualTo(25m));
        }

        [Test]
        public void GetDiscountAmountFor_WithRegularProduct_ReturnsProductDiscount()
        {
            var discount = new Discount("Test Discount", treatmentDiscount: 25m, productDiscount: 10m);
            var product = new Product();

            decimal amount = discount.GetDiscountAmountFor(product);

            Assert.That(amount, Is.EqualTo(10m));
        }

        [Test]
        public void GetDiscountAmountFor_WhenDiscountsAreZero_ReturnsZeroCorrectly()
        {
            var discount = new Discount("No Discount", 0m, 0m);
            var product = new Product();

            decimal amount = discount.GetDiscountAmountFor(product);

            Assert.That(amount, Is.EqualTo(0m));
        }

        [Test]
        public void GetDiscountAmountFor_TreatmentWithZeroTreatmentDiscount_ReturnsZero()
        {
            var discount = new Discount("Weird Discount", 0m, 50m);
            var treatment = new Treatment();

            decimal amount = discount.GetDiscountAmountFor(treatment);

            Assert.That(amount, Is.EqualTo(0m));
        }
    }
}
