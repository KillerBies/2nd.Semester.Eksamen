using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record TreatmentSnapshot : ProductSnapshot
    {
        //Snapshot of a treatment that had been booked
        //Snapshot is made at time of payment so no need to change anything here when its made.
        public string? Category { get; private set; }
        public int BookingSnapshotId { get; set; }
        public BookingSnapshot BookingSnapshot { get; set; }

        
        private TreatmentSnapshot() { }

        public TreatmentSnapshot( Treatment treatment)
        {

            Name = treatment.Name;
            PricePerUnit = treatment.Price;
            DiscountedPrice = treatment.DiscountedPrice;
            Category = treatment.Category;
        }


    }

}
