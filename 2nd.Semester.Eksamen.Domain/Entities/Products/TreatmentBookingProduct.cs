using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class TreatmentBookingProduct : BaseEntity
    {
        public int TreatmentBookingID { get; set; }
        public TreatmentBooking TreatmentBooking { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public int NumberOfProducts { get; set; }
        public TreatmentBookingProduct() { }
    }
}
