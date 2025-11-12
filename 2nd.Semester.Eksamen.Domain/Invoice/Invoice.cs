using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Invoice
{
    public class Invoice
    {
        public string Invoice_Number { get; set; }
        public DateTime Invoice_Date { get; set; }
        public string Sellers_Name { get; set; }
        public string Sellers_Address { get; set; }
        public string Sellers_CVR { get; set; }
        public string Customer_Name { get; set; }
        public  string Customer_Address { get; set; }
        public string Customer_CVR { get; set; }
        public List<Invoice_Item> Items { get; set; }
        public decimal Total_Amount { get; set; }
        public decimal Total_Discount { get; set; }
        public decimal VAT_Amount { get; set; }
    }
}
