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
        public string? Name { get; protected set; }
        public decimal? ProductDiscount { get; protected set; }
        public decimal? TreatmentDiscount { get; protected set; }
        
        public OrderSnapshot OrderSnapshot { get; protected set; }
        private AppliedDiscountSnapshot() { }
        public AppliedDiscountSnapshot(Discount discount) : base(discount.RefrenceId)
        {
            if (discount == null) return;
            Name = discount.Name;
            ProductDiscount = discount.ProductDiscount * 100; 
            TreatmentDiscount = discount.TreatmentDiscount * 100;
        }
    }
}
