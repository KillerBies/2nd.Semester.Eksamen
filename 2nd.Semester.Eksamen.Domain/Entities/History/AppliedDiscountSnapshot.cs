using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record AppliedDiscountSnapshot : BaseSnapshot
    {
        public string Name { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public int OrderSnapshotId { get; set; }
        public OrderSnapshot OrderSnapshot { get; set; }
        private AppliedDiscountSnapshot() { }

        public AppliedDiscountSnapshot(Discount discount)
        {
            Name = discount.Name;
            //DiscountAmount = discount.DiscountAmount; // make it take discount.ProductDiscount and discount.TreatmentDiscount as discount now has field for both product and treatment now
        }


    }

}
