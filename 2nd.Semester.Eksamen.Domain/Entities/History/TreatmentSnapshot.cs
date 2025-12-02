using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public class TreatmentSnapshot : BaseEntity
    {
        //Snapshot of a treatment that had been booked
        //Snapshot is made at time of payment so no need to change anything here when its made.
        public string? Name { get; set; }
        public decimal? BasePrice { get; set; }
        public string? Category { get; set; }
        public TreatmentSnapshot() { }
        public TreatmentSnapshot(Treatment treatment)
        {
            Name = treatment.Name;
            Category = treatment.Category;
        }

        public void Snap(decimal price) 
        {
            BasePrice = price;
        }
    }
}
