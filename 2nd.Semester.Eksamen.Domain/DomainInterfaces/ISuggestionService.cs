using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainInterfaces
{
    public interface ISuggestionService
    {
        public List<BookingSuggestion> GetBookingSugestions(List<TreatmentBooking> treatments,
                                                                        DateOnly start,
                                                                        int numberOfDaysToCheck,
                                                                        int neededSuggestions,
                                                                        int interval);
    }
}
