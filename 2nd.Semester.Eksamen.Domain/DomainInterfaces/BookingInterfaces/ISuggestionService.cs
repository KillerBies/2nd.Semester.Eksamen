using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.BookingSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces
{
    public interface ISuggestionService
    {
        public Task<List<BookingSuggestion>> GetBookingSugestions(List<TreatmentBooking> treatments, DateOnly startDate, int numberOfDaysToCheck, int neededSuggestions, int interval);
    }
}
