using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ISnapshotRepository _snapshotRepository;
        




        public async Task CreateSnapshotInDBAsync(Order order, int customDiscount)
        {

            try
            {
                OrderSnapshot snapshot = new OrderSnapshot(order, customDiscount);
                await _snapshotRepository.CreateNewAsync(snapshot);
            }
            catch (Exception)
            { throw new Exception("Noget gik galt ved oprettele af ordren"); }
        }
        
        public async Task CreateInvoicePDF(OrderSnapshot orderSnapshot)
        {


        }

        

    }





}
