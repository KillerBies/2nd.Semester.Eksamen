using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.BookingSchedules;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService
{
    public class BookingSuggestionService : ISuggestionService
    {
        private readonly IScheduleDayRepository _dayRepository;
        private Dictionary<int, Dictionary<DateOnly, ScheduleDay>> _days = new();
        public BookingSuggestionService(IScheduleDayRepository scheduleDayRepository)
        {
            _dayRepository = scheduleDayRepository;
        }

        //lav en dictionary med dictionaries.
        //ved starten af functionen load alle employees daage ind
        //tag GetOrCreat funcktionen med her over
        //
        public async Task<List<BookingSuggestion>> GetBookingSugestions(List<TreatmentBooking> treatments,DateOnly startDate,int numberOfDaysToCheck,int neededSuggestions,int interval)
        {
            var suggestions = new List<BookingSuggestion>();
            if (!treatments.Any())
            {
                return suggestions;
            }
            foreach (var treatment in treatments)
            {
                var scheduledDays = await _dayRepository.GetByEmployeeIDAsync(treatment.EmployeeId);
                var scheduleDaysByDate = scheduledDays.ToDictionary(d => d.Date);
                _days[treatment.EmployeeId] = scheduleDaysByDate;
            }

            var plan = treatments.Select(t => new PlanItem
            {
                Treatment = t.Treatment,
                Employee = t.Employee,
                Duration = t.Treatment.Duration,
                EmployeeId = t.EmployeeId,
            }).ToList();

            for (int dayOffset = 0; dayOffset < numberOfDaysToCheck; dayOffset++)
            {
                if (suggestions.Count >= neededSuggestions)
                    break;
                var currentDate = startDate.AddDays(dayOffset);

                // First treatment to generate potential start times
                var first = plan.First();
                var firstDay = GetOrCreateDay(currentDate, first.Employee.WorkStart, first.Employee.WorkEnd, first.EmployeeId);

                var potentialStarts = new List<TimeOnly>();
                var freeSlots = firstDay.GetAllAvailableSlots(first.Duration);
                foreach (var slot in freeSlots)
                {
                    var slotStart = slot.Start;
                    var slotEnd = slot.End;

                    while (slotStart.Add(first.Duration) <= slotEnd)
                    {
                        potentialStarts.Add(slotStart);
                        slotStart = slotStart.AddMinutes(interval);
                    }
                }

                foreach (var start in potentialStarts)
                {
                    if (suggestions.Count >= neededSuggestions)
                        break;

                    var items = new List<BookingItem>();
                    var currentStart = start;
                    var startDateTime = currentDate.ToDateTime(currentStart);
                    var valid = true;

                    foreach (var planItem in plan)
                    {
                        // Get schedule for current employee
                        var day = GetOrCreateDay(currentDate, planItem.Employee.WorkStart, planItem.Employee.WorkEnd, planItem.EmployeeId);
                        var currentEnd = currentStart.Add(planItem.Duration);

                        // Check employee working hours
                        if (currentEnd > planItem.Employee.WorkEnd)
                        {
                            valid = false;
                            break;
                        }

                        // Check if the time slot is free
                        var slotAvailable = day.TimeRanges.Any(r =>
                            r.Type == "Freetime" && 
                            r.Start <= currentStart && //the free timerange start must be before or at the treatments start
                            currentEnd <= r.End //the free timerange must end after or at the end of the treatment
                        );

                        if (!slotAvailable)
                        {
                            valid = false;
                            break;
                        }

                        var itemStartDateTime = currentDate.ToDateTime(currentStart);
                        if (itemStartDateTime < DateTime.Now)
                        {
                            valid = false;
                            break;
                        }

                        //if its availalbe then add it to the list (do this for each treatment, if any of the treatments arent available then valid becomes false)
                        items.Add(new BookingItem
                        {
                            Treatment = planItem.Treatment,
                            Employee = planItem.Employee,
                            Start = itemStartDateTime,
                            End = itemStartDateTime.Add(planItem.Duration),
                        });

                        // Move start to the end of this treatment
                        currentStart = currentEnd;
                    }
                    //if non of the treatments failed to find a free timespot, add it to the list of suggestions.
                    if (valid)
                    {
                        suggestions.Add(new BookingSuggestion { Items = items });
                    }
                }
            }
            return suggestions;
        }
        private ScheduleDay GetOrCreateDay(DateOnly date, TimeOnly workStart, TimeOnly workEnd, int employeeId)
        {
            if (!_days[employeeId].TryGetValue(date, out var day))
            {
                day = new ScheduleDay(date, workStart, workEnd);
                _days[employeeId][date] = day;
            }
            return day;
        }

    }
}
