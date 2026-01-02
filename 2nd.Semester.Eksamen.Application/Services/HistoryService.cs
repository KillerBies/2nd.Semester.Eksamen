using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
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
        private IEmployeeRepository _employeeRepository;
        private IProductRepository _productRepository;
        public HistoryService(ISnapshotRepository snapshotRepository, IOrderRepository orderRepository, IBookingRepository bookingRepository, ITreatmentBookingRepository treatmentBookingRepository, IDiscountRepository discountRepository, ITreatmentRepository treatmentRepository, ICustomerRepository customerRepository, IHistorySnapShotRepository historyRepo, IEmployeeRepository employeeRepository, IProductRepository productRepository)
        {
            _snapshotRepository = snapshotRepository;
            _orderRepository = orderRepository;
            _bookingRepository = bookingRepository;
            _treatmentBookingRepository = treatmentBookingRepository;
            _discountRepository = discountRepository;
            _treatmentRepository = treatmentRepository;
            _customerRepository = customerRepository;
            _historyRepo = historyRepo;
            _employeeRepository = employeeRepository;
            _productRepository = productRepository;
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



        public async Task<DetailsContext?> GetBookingByGuid(Guid bookingGuid)
        {
            var booking = await _bookingRepository.GetByGuidAsync(bookingGuid);
            if(booking != null)
                return new BookingDetailsContext(new BookingDTO(booking));
            var bookingSnap = await _snapshotRepository.GetByBookingGuidAsync(bookingGuid);
            if(bookingSnap != null)
                return new BookingSnapShotContext(new BookingDTO(booking));
            throw new Exception("Booking not found");
        }
        public async Task<DetailsContext?> GetCustomerByGuid(Guid customerGuid)
        {
            var customer = await _customerRepository.GetByGuidAsync(customerGuid);
            if(customer != null)
                return new CustomerDetailsContext(new CustomerDTO(customer));
            var customerSnap =  await _historyRepo.GetCustomerSnapShotByGuidAsync(customerGuid);
            if(customerSnap != null)
                return new CustomerSnapShotDetailsContext(new CustomerDTO(customerSnap));
            throw new Exception("Customer not found");
        }
        public async Task<DetailsContext?> GetTreatmentByGuid(Guid treatmentGuid)
        {
            var treatment = await _treatmentRepository.GetByGuidAsync(treatmentGuid);
            if(treatment != null)
                return new TreatmentContext(new TreatmentDTO(treatment));
            var treatmentSnap = await _historyRepo.GetTreatmentSnapShotByGuidAsync(treatmentGuid);
            if(treatmentSnap != null)
                return new TreatmentSnapshotContext(new TreatmentDTO(treatmentSnap));
            throw new Exception("Treatment not found");
        }
        public async Task<DetailsContext?> GetDiscountByGuid(Guid discountGuid)
        {
            var discount = await _discountRepository.GetByGuidAsync(discountGuid);
            if(discount != null)
                return new DiscountContext(new DiscountOverviewDTO(discount));
            var discountSnap = await _historyRepo.GetDiscountSnapShotByGuid(discountGuid);
            if(discountSnap != null)
                return new DiscountSnapShotContext(new DiscountOverviewDTO(discountSnap));
            throw new Exception("Discount not found");
        }
        public async Task<DetailsContext?> GetOrderSnapshotByGuid(Guid orderSnapShotGuid)
        {
            var orderSnap = await _snapshotRepository.GetByGuidAsync(orderSnapShotGuid);
            if(orderSnap != null)
                return new OrderSnapShotContext(new OrderHistoryDTO(orderSnap));
            throw new Exception("Order Snapshot not found");
        }
        public async Task<DetailsContext?> GetEmployeeByGuid(Guid employeeGuid)
        {
            var employee = await _employeeRepository.GetByGuidAsync(employeeGuid);
            if(employee != null)
                return new EmployeeDetailsContext(new EmployeeDetailsDTO(employee));
            var employeeSnap = await _historyRepo.GetEmployeeSnapShotByGuidAsync(employeeGuid);
            if(employeeSnap != null)
                return new EmployeeSnapShotContext(new EmployeeDetailsDTO(employeeSnap));
            throw new Exception("Employee not found");
        }
        public async Task<DetailsContext?> GetProductByGuid(Guid productGuid)
        {
            var product = await _productRepository.GetByGuidAsync(productGuid);
            if (product != null)
                return new ProductContext(new ProductOverviewDTO(product));
            var productSnap = await _historyRepo.GetProductSnapshotByGuidAsync(productGuid);
            if (productSnap != null)
                return new ProductSnapShotContext(new ProductOverviewDTO(productSnap));
            throw new Exception("Product not found");
        }
    }
}
