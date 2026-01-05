using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
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


        public Task<DetailsContext?> GetBookingByGuid(Guid bookingGuid);
        public Task<DetailsContext?> GetCustomerByGuid(Guid customerGuid);
        public Task<DetailsContext?> GetTreatmentByGuid(Guid treatmentGuid);
        public Task<DetailsContext?> GetDiscountByGuid(Guid discountGuid);
        public Task<DetailsContext?> GetOrderSnapshotByGuid(Guid orderSnapShotGuid);
        public Task<DetailsContext?> GetEmployeeByGuid(Guid employeeGuid);
        public Task<DetailsContext?> GetProductByGuid(Guid productGuid);



        public Task<BookingDTO> GetBookingScheduleAsync(Guid treatmentGuid);
    }
}
