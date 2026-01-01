using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IHistoryService
    {
        //Service for History related operations.
        public Task<List<OrderHistoryDTO>> GetCustomerOrderHistoryByGuidAsync(Guid customerGuid);
        public Task<List<BookingDTO>> GetCustomerUpcommingHistoryByGuidAsync(Guid customerGuid);
        public Task<List<TreatmentHistoryDTO>> GetEmployeeTreatmentHistoryByGuidAsync(Guid employeeGuid);
        public Task<List<TreatmentBookingDTO>> GetEmployeeUpcommingTreatmentHistoryByGuidAsync(Guid employeeGuid);
        public Task<List<OrderHistoryDTO>> GetProductHistoryByGuidAsync(Guid productGuid);
        public Task<List<OrderHistoryDTO>> GetDiscountHistoryByGuidAsync(Guid discountGuid);
        public Task<List<TreatmentHistoryDTO>> GetTreatmentHistoryByGuidAsync(Guid treatmentGuid);
        public Task<List<TreatmentBookingDTO>> GetTreatmentUpcommingHistoryByGuidAsync(Guid treatmentGuid);
        public Task<BookingDTO> GetBookingByGuid(Guid bookingGuid);
        public Task<OrderHistoryDTO> GetBookingSnapShotByGuid(Guid bookingGuid);
        public Task<CustomerDTO> GetCustomerByGuid(Guid customerGuid);
        public Task<CustomerSnapshot> GetCustomerSnapshotByGuid(Guid customerGuid);
        public Task<TreatmentDTO> GetTreatmentByGuid(Guid treatmentGuid);
        public Task<DiscountOverviewDTO> GetDiscountByGuid(Guid discountGuid);
        public Task<OrderSnapshotOverviewDTO> GetOrderSnapshotByGuid(Guid orderSnapShotGuid);
    }
}
