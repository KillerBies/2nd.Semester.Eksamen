//using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
//using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces
//{
//    public interface IScheduleRepository
//    {
//        public Task<EmployeeSchedule> GetByIDAsync(int id);
//        public Task<EmployeeSchedule> GetByEmployeeIDAsync(int id);
//        public Task<IEnumerable<EmployeeSchedule>> GetAllAsync();
//        public Task<IEnumerable<EmployeeSchedule>> GetByFilterAsync(Filter filter);
//        public Task CreateNewAsync(EmployeeSchedule EmployeeSchedule);
//        public Task UpdateAsync(EmployeeSchedule EmployeeSchedule);
//        public Task DeleteAsync(EmployeeSchedule EmployeeSchedule);
//        public Task BookScheduleAsync(TreatmentBooking TreatmentBooking);
//    }
//}
