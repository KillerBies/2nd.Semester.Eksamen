using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Campaign: BaseDiscount
    {
        // Elements of a campaign
        //Start and end date of the campaign
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public string Description { get; private set; }
        //Lists of product categories and treatments that the campaign applies to
        public List<string> ProductCategories { get; private set; }
        public List<string> Treatments { get; private set; }

    }
}
