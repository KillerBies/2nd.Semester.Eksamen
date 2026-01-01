using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private ISnapshotRepository _snapshotRepository;
        private IOrderRepository _orderRepository;
        private IBookingRepository _bookingRepository;
        private ITreatmentBookingRepository _treatmentBookingRepository;
        private ITreatmentRepository _treatmentRepository;
        private IDiscountRepository _discountRepository;
        private ICustomerRepository _customerRepository;
        private IHistorySnapShotRepository _historyRepo;
        public HistoryService(ISnapshotRepository snapshotRepository, IOrderRepository orderRepository, IBookingRepository bookingRepository, ITreatmentBookingRepository treatmentBookingRepository, IDiscountRepository discountRepository, ITreatmentRepository treatmentRepository, ICustomerRepository customerRepository, IHistorySnapShotRepository historyRepo)
        {
            _snapshotRepository = snapshotRepository;
            _orderRepository = orderRepository;
            _bookingRepository = bookingRepository;
            _treatmentBookingRepository = treatmentBookingRepository;
            _discountRepository = discountRepository;
            _treatmentRepository = treatmentRepository;
            _customerRepository = customerRepository;
            _historyRepo = historyRepo;
        }
        public async Task<List<OrderHistoryDTO>> GetCustomerOrderHistoryByGuidAsync(Guid customerGuid)
        {
            return (await _snapshotRepository.GetByCustomerGuidAsync(customerGuid)).Select(o => new OrderHistoryDTO(o)).ToList();
        }
        public async Task<List<BookingDTO>> GetCustomerUpcommingHistoryByGuidAsync(Guid customerGuid)
        {
            return (await _bookingRepository.GetByCustomerGuidAsync(customerGuid)).Select(tb => new BookingDTO(tb)).ToList();
        }
        public async Task<List<TreatmentHistoryDTO>> GetEmployeeTreatmentHistoryByGuidAsync(Guid employeeGuid)
        {
            var snap = await _snapshotRepository.GetByEmployeeGuidAsync(employeeGuid);
            var list = new List<TreatmentHistoryDTO>();
            foreach (var s in snap)
            {
                if (s.BookingSnapshot.TreatmentSnapshot.Any(ts => ts.EmployeeGuid == employeeGuid))
                {
                    var treatSnap = s.BookingSnapshot.TreatmentSnapshot.Where(ts => ts.EmployeeGuid == employeeGuid).ToList();
                    if (s.BookingSnapshot.CustomerSnapshot is PrivateCustomerSnapshot pc)
                        list.AddRange(treatSnap.Select(t => new TreatmentHistoryDTO(t, s) { CustomerName = pc.Name + " " + pc.LastName }));
                    else
                        list.AddRange(treatSnap.Select(t => new TreatmentHistoryDTO(t, s)));
                }
            }
            return list;
        }
        public async Task<List<TreatmentBookingDTO>> GetEmployeeUpcommingTreatmentHistoryByGuidAsync(Guid employeeGuid)
        {
            var treatments = await _treatmentBookingRepository.GetByEmployeeGuidAsync(employeeGuid);
            var treatmentDTOs = new List<TreatmentBookingDTO>();
            foreach (var treatment in treatments)
            {
                var customer = (await _bookingRepository.GetByIDAsync(treatment.BookingID)).Customer;
                if (customer is PrivateCustomer pc)
                {
                    treatmentDTOs.Add(new TreatmentBookingDTO(treatment) { CustomerGuid = customer.Guid, CustomerId = customer.Id, CustomerName = pc.Name + " " + pc.LastName });
                }
                else
                {
                    treatmentDTOs.Add(new TreatmentBookingDTO(treatment) { CustomerGuid = customer.Guid, CustomerId = customer.Id, CustomerName = customer.Name });
                }
            }
            return treatmentDTOs;
        }
        public async Task<List<OrderHistoryDTO>> GetProductHistoryByGuidAsync(Guid productGuid)
        {
            return (await _snapshotRepository.GetByProductGuidAsync(productGuid)).Select(o => new OrderHistoryDTO(o)).ToList();
        }
        public async Task<List<OrderHistoryDTO>> GetDiscountHistoryByGuidAsync(Guid discountGuid)
        {
            return (await _snapshotRepository.GetByDiscountGuidAsync(discountGuid)).Select(o => new OrderHistoryDTO(o)).ToList();
        }
        public async Task<List<TreatmentHistoryDTO>> GetTreatmentHistoryByGuidAsync(Guid treatmentGuid)
        {
            var snap = await _snapshotRepository.GetByTreatmentGuidAsync(treatmentGuid);
            var list = new List<TreatmentHistoryDTO>();
            foreach (var s in snap)
            {
                if (s.BookingSnapshot.TreatmentSnapshot.Any(ts => ts.Guid == treatmentGuid))
                {
                    var treatSnap = s.BookingSnapshot.TreatmentSnapshot.Where(ts => ts.Guid == treatmentGuid).ToList();
                    list.AddRange(treatSnap.Select(t => new TreatmentHistoryDTO(t, s)));
                }
            }
            return list;
        }
        public async Task<List<TreatmentBookingDTO>> GetTreatmentUpcommingHistoryByGuidAsync(Guid treatmentGuid)
        {
            return (await _treatmentBookingRepository.GetByTreatmentGuidAsync(treatmentGuid)).Select(tb => new TreatmentBookingDTO(tb)).ToList();
        }

        public async Task<BookingDTO> GetBookingByGuid(Guid bookingGuid)
        {
            var booking = await _bookingRepository.GetByGuidAsync(bookingGuid);
            return booking == null ? new BookingDTO() : new BookingDTO(booking);
        }
        public async Task<OrderHistoryDTO> GetBookingSnapShotByGuid(Guid bookingGuid)
        {
            var booking = await _snapshotRepository.GetByBookingGuidAsync(bookingGuid);
            return booking == null ? new OrderHistoryDTO() : new OrderHistoryDTO(booking);
        }
        public async Task<CustomerDTO> GetCustomerByGuid(Guid customerGuid)
        {
            var customer = await _customerRepository.GetByGuidAsync(customerGuid);
            return customer == null ? new CustomerDTO() : new CustomerDTO(customer);
        }
        public async Task<CustomerSnapshot> GetCustomerSnapshotByGuid(Guid customerGuid)
        {
            var customer = await _historyRepo.GetCustomerSnapShotByGuidAsync(customerGuid);
            return customer == null ? new CustomerSnapshot() : customer;
        }
        public async Task<TreatmentDTO> GetTreatmentByGuid(Guid treatmentGuid)
        {

        }
        public async Task<DiscountOverviewDTO> GetDiscountByGuid(Guid discountGuid)
        {

        }
        public async Task<OrderSnapshotOverviewDTO> GetOrderSnapshotByGuid(Guid orderSnapShotGuid);
        {

        }
    }
}
