using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class InvoiceService
    {

        




        public async Task SaveAsInvoiceDB(Order order, int customDiscount)
        {
            
            OrderSnapshot snapshot = new OrderSnapshot(order, customDiscount);


        }


        

    }





}
