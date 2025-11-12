using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Campaign: Base_Discount
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
    }
}
