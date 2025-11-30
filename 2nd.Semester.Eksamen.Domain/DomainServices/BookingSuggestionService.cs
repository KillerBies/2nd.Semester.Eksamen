using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2nd.Semester.Eksamen.Domain.DomainServices
{
    public class BookingSuggestionService : ISuggestionService
    {
        public async Task<List<BookingSuggestion>> GetBookingSugestions(List<TreatmentBooking> treatments,
                                                                        DateOnly startdate,
                                                                        int numberOfDaysToCheck,
                                                                        int neededSuggestions,
                                                                        int interval)
        {
            var plan = treatments.Select(t => new PlanItem
            {
                Treatment = t.Treatment,
                Employee = t.Employee,
                Duration = t.Treatment.Duration,
                Schedule = t.Employee.Schedule
            }).ToList();

            var suggestions = new List<BookingSuggestion>();
            if (!plan.Any()) return suggestions;

            var first = plan.First();
            var firstDay = first.Schedule.GetOrCreateDay(startdate, first.Employee.WorkStart, first.Employee.WorkEnd);

            // Get all possible start times for the first treatment
            var potentialStarts = firstDay.GetAllAvailableSlots(first.Duration)
                .SelectMany(slot =>
                    Enumerable.Range(0, (int)((slot.End - slot.Start).TotalMinutes / interval))
                        .Select(i => slot.Start.AddMinutes(i * interval))
                        .Where(start => start + first.Duration <= slot.End)
                ).ToList();

            foreach (var start in potentialStarts)
            {
                if (suggestions.Count >= neededSuggestions)
                    break;

                var currentStart = start;
                var valid = true;
                var items = new List<BookingItem>();

                // Schedule treatments in order
                foreach (var planItem in plan)
                {
                    var day = planItem.Schedule.GetOrCreateDay(startdate, planItem.Employee.WorkStart, planItem.Employee.WorkEnd);
                    var currentEnd = currentStart + planItem.Duration;

                    // Check if the time range is free for this employee
                    var slotAvailable = day.TimeRanges.Any(r =>
                        r.Type == TimeRangeType.Freetime &&
                        r.Start <= currentStart &&
                        currentEnd <= r.End
                    );

                    if (!slotAvailable)
                    {
                        valid = false;
                        break;
                    }

                    items.Add(new BookingItem
                    {
                        Treatment = planItem.Treatment,
                        Employee = planItem.Employee,
                        Start = currentStart,
                        End = currentEnd
                    });

                    // Move start to end of this treatment for next treatment
                    currentStart = currentEnd;
                }

                if (valid)
                    suggestions.Add(new BookingSuggestion { Items = items });
            }

            return suggestions;
        }
    }
}
