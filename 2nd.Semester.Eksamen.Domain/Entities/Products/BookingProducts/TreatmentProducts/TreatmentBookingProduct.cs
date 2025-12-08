using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts
{
    public class TreatmentBookingProduct : BaseEntity
    {
        public int TreatmentBookingID { get; set; }
        public TreatmentBooking TreatmentBooking { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int NumberOfProducts { get; set; }
        public TreatmentBookingProduct() { }

        public TreatmentBookingProduct(Product product, int numberOfProducts)
        {
            Product = product;
            NumberOfProducts = numberOfProducts;
        }

    }
}
