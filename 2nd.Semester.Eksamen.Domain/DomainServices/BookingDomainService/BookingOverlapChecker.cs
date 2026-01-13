using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces.TreatmentBookingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService
{
    public class BookingOverlapChecker : IBookingOverlapChecker
    {
        private readonly IBookingQueryRepository _bookingRepository;
        private readonly ITreatmentBookingQueryRepository _treatmentBookingRepository;
        public BookingOverlapChecker(IBookingQueryRepository bookingRepository, ITreatmentBookingQueryRepository treatmentBookingRepository)
        {
            _treatmentBookingRepository = treatmentBookingRepository;
            _bookingRepository = bookingRepository;
        }
        bool IBookingOverlapChecker.OverlapsCustomer(DateTime Start, DateTime End)
        {
            var CustomersBookings = _bookingRepository.GetByCustomerGuidAsync()
            return CustomersBookings.Any(b => b.Overlaps(Start,End));
        }
        bool IBookingOverlapChecker.OverlapsEmployee(DateTime Start, DateTime End)
        {
            return EmployeesBookings.Any(b => b.Overlaps(Start, End));
        }
        https://www.youtube.com/watch?v=HVaGeqe9TPA
    }
}
