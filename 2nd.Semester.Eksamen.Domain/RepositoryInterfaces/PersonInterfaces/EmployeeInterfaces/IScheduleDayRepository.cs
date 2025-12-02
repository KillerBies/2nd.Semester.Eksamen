using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces
{
    public interface IScheduleDayRepository
    {
        public Task<ScheduleDay> GetByIDAsync(int id);
        public Task<IEnumerable<ScheduleDay>> GetByEmployeeIDAsync(int id);
        public Task<IEnumerable<ScheduleDay>> GetAllAsync();
        public Task<IEnumerable<ScheduleDay>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(ScheduleDay ScheduleDay);
        public Task UpdateAsync(ScheduleDay ScheduleDay);
        public Task DeleteAsync(ScheduleDay ScheduleDay);
        public Task BookScheduleAsync(TreatmentBooking TreatmentBooking);
    }
}
