using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Campaign: Discount
    {
        // Elements of a campaign
        //Start and end date of the campaign
        public DateTime? Start { get; set; }
        public DateTime? End { get;  set; }
        public string? Description { get; set; }
        //Lists of product categories and treatments that the campaign applies to
        public List<string>? ProductCategories { get; set; } = new List<string>();
        public List<Treatment>? Treatments { get; set; } = new List<Treatment>();

        public Campaign() { }

    }
}
