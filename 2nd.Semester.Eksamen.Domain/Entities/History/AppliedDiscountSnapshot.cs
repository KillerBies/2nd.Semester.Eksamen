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
        public string Name { get; init; }
        public decimal DiscountAmount { get; init; }


        public AppliedDiscountSnapshot() { }

        public AppliedDiscountSnapshot(Discount discount)
        {
            Name = discount.Name;
            DiscountAmount = discount.DiscountAmount;
        }


    }

}
