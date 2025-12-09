using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class LoyaltyDiscountDTO
    {
        [Required(ErrorMessage = "Navn er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 1, ErrorMessage = "Rabat på behandling skal være mellem 0 og 100%.")]
        public decimal TreatmentDiscount { get; set; }

        [Range(0, 1, ErrorMessage = "Rabat på produkter skal være mellem 0 og 100%.")]
        public decimal ProductDiscount { get; set; }

        public bool AppliesToTreatment { get; set; }
        public bool AppliesToProduct { get; set; }


        // Loyalty Discount Fields
        [StringLength(100, ErrorMessage = "Rabattypen må højst være 100 tegn.")]
        public string? DiscountType { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Minimum besøg skal være et positivt tal.")]
        public int MinimumVisits { get; set; }
    }
}
