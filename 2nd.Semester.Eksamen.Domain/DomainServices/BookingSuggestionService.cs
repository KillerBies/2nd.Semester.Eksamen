using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices
{
    public class BookingSuggestionService : ISuggestionService
    {
        public async Task<List<BookingSuggestion>> GetBookingSugestions(List<BookingTreatment> treatments,
                                                                        DateOnly start,
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

            var dayTasks = Enumerable
                .Range(0, numberOfDaysToCheck)
                .Select(offset => Task.Run(() =>
                    ProcessSingleDay(plan, start.AddDays(offset), neededSuggestions, interval)
                ))
                .ToList();

            var results = await Task.WhenAll(dayTasks);

            return results
                .SelectMany(r => r)
                .OrderBy(r => r.Start)
                .Take(neededSuggestions)
                .ToList();
        }

        private List<BookingSuggestion> ProcessSingleDay(
            List<PlanItem> plan,
            DateOnly date,
            int maxSuggestions,
            int interval)
        {
            var suggestions = new List<BookingSuggestion>();
            var first = plan.First();
            var day1 = first.Schedule.GetOrCreateDay(date, first.Employee.WorkStart, first.Employee.WorkEnd);

            var potentialStarts =
                day1.GetAllAvailableSlots(first.Duration)
                    .SelectMany(slot =>
                        Enumerable.Range(0, (int)((slot.End - slot.Start).TotalMinutes / interval))
                            .Select(i => slot.Start.AddMinutes(i * interval))
                            .Where(start => start + first.Duration <= slot.End)
                    ).ToList();

            foreach (var start in potentialStarts)
            {
                if (suggestions.Count >= maxSuggestions)
                    break;

                var chain = plan.Aggregate(
                    new
                    {
                        Valid = true,
                        CurrentStart = start,
                        CurrentEnd = start + first.Duration,
                        Items = new List<BookingItem>
                        {
                        new BookingItem
                        {
                            Treatment = first.Treatment,
                            Employee = first.Employee,
                            Start = start,
                            End = start + first.Duration
                        }
                        }
                    },
                    (acc, p) =>
                    {
                        if (!acc.Valid) return acc;
                        if (p == first) return acc;

                        var day = p.Schedule.GetOrCreateDay(date, p.Employee.WorkStart, p.Employee.WorkEnd);
                        var nextEnd = acc.CurrentEnd + p.Duration;

                        var valid = day.TimeRanges.Any(r =>
                            r.Type == TimeRangeType.Freetime &&
                            r.Start <= acc.CurrentEnd &&
                            nextEnd <= r.End
                        );

                        if (!valid)
                            return new { Valid = false, acc.CurrentStart, acc.CurrentEnd, acc.Items };

                        acc.Items.Add(new BookingItem
                        {
                            Treatment = p.Treatment,
                            Employee = p.Employee,
                            Start = acc.CurrentEnd,
                            End = nextEnd
                        });

                        return new { Valid = true, acc.CurrentStart, CurrentEnd = nextEnd, acc.Items };
                    });

                if (chain.Valid)
                    suggestions.Add(new BookingSuggestion { Items = chain.Items });
            }

            return suggestions;
        }
    }
}
