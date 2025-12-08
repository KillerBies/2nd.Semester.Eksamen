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
using _2nd.Semester.Eksamen.Application.DTO;
using System.Reflection.Metadata.Ecma335;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using System.Net.Http.Headers;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ISnapshotRepository _snapshotRepository;
        private readonly IGenerateInvoice _generateInvoice;
        private readonly IDiscountRepository _discountRepository;
        private readonly IBookingRepository _bookingRepository;
        
        public InvoiceService(ISnapshotRepository snapshotRepository, IGenerateInvoice generateInvoice, IDiscountRepository discountRepository, IBookingRepository bookingRepository)
        {
            _snapshotRepository = snapshotRepository;
            _generateInvoice = generateInvoice;
            _discountRepository = discountRepository;
            _bookingRepository = bookingRepository;
        }

       

        public async Task CreateSnapshotInDBAsync(Order order)
        {
            OrderSnapshot snapshot;
            try
            {
                var discount = await _discountRepository.GetByIdAsync(order.AppliedDiscountId);
                var booking = await _bookingRepository.GetByIDAsync(order.BookingId);
                 snapshot = new OrderSnapshot(order, discount, booking);
                await _snapshotRepository.CreateNewAsync(snapshot);
                
            }
            catch (Exception)
            { throw new Exception("Noget gik galt ved oprettele af ordren"); }

            
            
                var dbSnapshot = await _snapshotRepository.GetByIdAsync(snapshot.Id);
              // snapshot.PdfInvoice = _generateInvoice.GenerateInvoicePDF(dbSnapshot);
        }
        
         

        public async Task <List<OrderSnapshotDTO>>? GetAllSnapshotsAsync()
        {
            try
            {
                
                List<OrderSnapshot> orderSnapshots = await _snapshotRepository.GetAllOrderSnapshotsAsync();
                
                List<OrderSnapshotDTO> orderSnapshotDTOs = new();
                foreach (OrderSnapshot orderSnapshot in orderSnapshots)
                {
                    OrderSnapshotDTO orderSnapshotDTO = new OrderSnapshotDTO(orderSnapshot);
                    orderSnapshotDTOs.Add(orderSnapshotDTO);
                }
            return orderSnapshotDTOs;
            }
            catch (Exception)
            { throw new Exception("Noget gik galt ved hentning af ordrer"); }
        }

        
    
        
    
    }

        

}






