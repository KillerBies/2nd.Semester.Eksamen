using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record TreatmentSnapshot : ProductSnapshot
    {
        //Snapshot of a treatment that had been booked
        //Snapshot is made at time of payment so no need to change anything here when its made.
        public string Category { get; private set; }
        public int BookingSnapshotId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public TimeSpan Duration { get; set; }
        public string EmployeeName { get; set; }
        public BookingSnapshot? BookingSnapshot { get; set; }
        public decimal? PriceWithMultiplier { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeSnapshot? EmployeeSnapshot { get; set; }
        private TreatmentSnapshot() { }

        public TreatmentSnapshot(Treatment treatment, BookingSnapshot bookingSnapshot, string empName = "", Guid empGuid = default)
        {
            if(empGuid != default && empName != "")
            {
                EmployeeGuid = empGuid;
                EmployeeName = empName;
            }
            Name = treatment.Name;
            PricePerUnit = treatment.Price;
            DiscountedPrice = treatment.DiscountedPrice;
            Category = treatment.Category;
            BookingSnapshot = bookingSnapshot;
            Guid = treatment.Guid;
            Duration = treatment.Duration;
        }


    }

}
