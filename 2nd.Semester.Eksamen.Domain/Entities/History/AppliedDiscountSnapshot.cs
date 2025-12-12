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
        public string? Name { get; private set; }
        public decimal? ProductDiscount { get; private set; }
        public decimal? TreatmentDiscount { get; private set; }
        
        public OrderSnapshot OrderSnapshot { get; set; }
        private AppliedDiscountSnapshot() { }
        public AppliedDiscountSnapshot(Discount discount)
        {
            if (discount == null) return;
            Name = discount.Name;
            ProductDiscount = discount.ProductDiscount * 100; 
            TreatmentDiscount = discount.TreatmentDiscount * 100;
        }
    }
}
