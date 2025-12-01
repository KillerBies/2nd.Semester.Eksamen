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
        public List<BookingSuggestion> GetBookingSugestions(List<TreatmentBooking> treatments,DateOnly startDate,int numberOfDaysToCheck,int neededSuggestions,int interval)
        {
            var suggestions = new List<BookingSuggestion>();
            System.Diagnostics.Debug.WriteLine($"input (inside) is null? {!treatments.Any()}");
            if (!treatments.Any())
            {
                System.Diagnostics.Debug.WriteLine($"return empty list");
                return suggestions;
            }

            // Build plan items (treatment + employee + duration + schedule)
            System.Diagnostics.Debug.WriteLine($"suggest start");
            var plan = treatments.Select(t => new PlanItem
            {
                Treatment = t.Treatment,
                Employee = t.Employee,
                Duration = t.Treatment.Duration,
                Schedule = t.Employee.Schedule
            }).ToList();

            for (int dayOffset = 0; dayOffset < numberOfDaysToCheck; dayOffset++)
            {
                System.Diagnostics.Debug.WriteLine($"loop start {!treatments.Any()}");
                var currentDate = startDate.AddDays(dayOffset);

                // First treatment to generate potential start times
                var first = plan.First();
                var firstDay = first.Schedule.GetOrCreateDay(currentDate, TimeOnly.FromTimeSpan(first.Employee.WorkStart), TimeOnly.FromTimeSpan(first.Employee.WorkEnd));

                var potentialStarts = new List<DateTime>();
                System.Diagnostics.Debug.WriteLine($"foreach slot loop before start {firstDay == null}");
                foreach(var range in firstDay.TimeRanges)
                {
                    System.Diagnostics.Debug.WriteLine($"timerange in firstday: {range.Duration}");
                }
                foreach (var slot in firstDay.GetAllAvailableSlots(first.Duration))
                {
                    System.Diagnostics.Debug.WriteLine($"foreach slot loop start {firstDay==null}");
                    var slotStart = slot.Start;
                    var slotEnd = slot.End;

                    while (slotStart + first.Duration <= slotEnd)
                    {
                        potentialStarts.Add(slotStart);
                        slotStart = slotStart.AddMinutes(interval);
                        System.Diagnostics.Debug.WriteLine($"potential start recorded");
                    }
                }

                foreach (var start in potentialStarts)
                {
                    System.Diagnostics.Debug.WriteLine($"starts loop start");
                    if (suggestions.Count >= neededSuggestions)
                        break;

                    var currentStart = start;
                    var valid = true;
                    var items = new List<BookingItem>();

                    foreach (var planItem in plan)
                    {
                        System.Diagnostics.Debug.WriteLine($"plan item loop start");
                        // Get schedule for current employee
                        var day = planItem.Schedule.GetOrCreateDay(currentDate, TimeOnly.FromTimeSpan(planItem.Employee.WorkStart), TimeOnly.FromTimeSpan(planItem.Employee.WorkEnd));
                        var currentEnd = currentStart.Add(planItem.Duration);

                        // Check employee working hours
                        if (currentEnd.TimeOfDay > planItem.Employee.WorkEnd)
                        {
                            System.Diagnostics.Debug.WriteLine($"beyond employee workhours");
                            valid = false;
                            break;
                        }

                        // Check if the time slot is free
                        var slotAvailable = day.TimeRanges.Any(r =>
                            r.Type == TimeRangeType.Freetime && 
                            r.Start <= currentStart && //the free timerange start must be before or at the treatments start
                            currentEnd <= r.End //the free timerange must end after or at the end of the treatment
                        );

                        if (!slotAvailable)
                        {
                            System.Diagnostics.Debug.WriteLine($"No slot available");
                            valid = false;
                            break;
                        }

                        System.Diagnostics.Debug.WriteLine($"suggest added");
                        //if its availalbe then add it to the list (do this for each treatment, if any of the treatments arent available then valid becomes false)
                        items.Add(new BookingItem
                        {
                            Treatment = planItem.Treatment,
                            Employee = planItem.Employee,
                            Start = currentStart,
                            End = currentEnd
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
            System.Diagnostics.Debug.WriteLine($"suggest end");
            return suggestions;
        }

    }
}
