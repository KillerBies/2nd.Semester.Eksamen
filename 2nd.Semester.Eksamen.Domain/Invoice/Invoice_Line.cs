using _2nd.Semester.Eksamen.Domain.Entities;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Invoice
{
    public class Invoice_Item
    {
        public Base_Product Product { get; set; }
    }
}
