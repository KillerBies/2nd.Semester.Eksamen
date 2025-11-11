using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Personer
{
    public class Adress : Base_Entity
    {
        public string Postal_Code { get; set; }
        public string City { get; set; }
        public int House_Number { get; set; }
    }
}
