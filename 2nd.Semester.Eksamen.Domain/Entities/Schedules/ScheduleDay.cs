using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules
{
    public class ScheduleDay
    {
        public DateOnly Date { get; private set; }
        private List<TimeRange> _timeRanges = new List<TimeRange>();
        public IReadOnlyList<TimeRange> TimeRanges => _timeRanges.OrderBy(r => r.Start).ToList();
        public ScheduleDay(DateTime date, TimeOnly workStart, TimeOnly workEnd)
        {
            Date = DateOnly.FromDateTime(date);
            var start = Date.ToDateTime(workStart);
            var end = Date.ToDateTime(workEnd);
            _timeRanges.Add(new TimeRange
            {
                Name = "Freetime",
                Type = TimeRangeType.Freetime,
                Start = start,
                End = end
            });
        }

        public bool AvailableInTimeRange(TimeRange timerange)
        {
            return !(TimeRanges.Any(tr => tr.HasOverlap(timerange) && tr.Type!=TimeRangeType.Freetime));
        }
        public bool CheckIfAvailable(TimeSpan duration)
        {
            return _timeRanges.Any(r => r.Type == TimeRangeType.Freetime && r.Duration >= duration);
        }

        public TimeRange? GetAvailableTimeRange(TimeSpan duration)
        {
            return _timeRanges.FirstOrDefault(r => r.Type == TimeRangeType.Freetime && r.Duration >= duration);
        }

        public IEnumerable<TimeRange> GetAllAvailableSlots(TimeSpan duration)
        {
            return _timeRanges
                .Where(r => r.Type == TimeRangeType.Freetime && r.Duration >= duration)
                .OrderBy(r => r.Start);
        }

        public IEnumerable<TimeRange> GetOverlappingFreetime(TimeRange booking)
        {
            return _timeRanges
                .Where(r => r.Type == TimeRangeType.Freetime && r.HasOverlap(booking))
                .OrderBy(r => r.Start);
        }

        public bool AddBooking(TimeRange booking)
        {
            /*
             check if the timeRange overlaps with any existing TimeRanges
                if it does, throw an exception
            if it doesn't, add it to the TimeRanges list
            reschedule the TimeRanges list to account for the new booking
            do this by splitting any existing TimeRanges that overlap with the new booking into multiple TimeRanges
            and adjusting the start and end times of the existing TimeRanges accordingly
             */
            var free = _timeRanges.FirstOrDefault(r => r.Type == TimeRangeType.Freetime && r.Start <= booking.Start && r.End >= booking.End);
            if (free == null)
                return false;
            // before the booking
            _timeRanges.Remove(free);

            //If free time timerange Starts at booking start, no freetime before booking (only one free time needed after booking)
            // Split freetime if needed
            if (free.Start < booking.Start) //if freetime starts before booking start then we need a freetime timerange before booking
                _timeRanges.Add(new TimeRange { Name = "Freetime", Type = TimeRangeType.Freetime, Start = free.Start, End = booking.Start });

            if (free.End > booking.End)//if freetime ends after booking end then we need a freetime timerange after booking
                _timeRanges.Add(new TimeRange { Name = "Freetime", Type = TimeRangeType.Freetime, Start = booking.End, End = free.End });

            booking.Type = TimeRangeType.Booked;
            _timeRanges.Add(booking);
            MergeAdjacentFreetime();
            return true;
        }
        //We need to merge adjacent free time ranges when a booking is made to avoid a bunch of small free time ranges like: [9-10 free][11-12 free] should become [9-12 free]
        private void MergeAdjacentFreetime()
        {
            var merged = new List<TimeRange>();

            foreach (var current in _timeRanges.OrderBy(r => r.Start))
            {
                if (merged.Count == 0)
                {
                    merged.Add(current);
                    continue;
                }

                var last = merged.Last();
                if (last.Type == TimeRangeType.Freetime &&
                    current.Type == TimeRangeType.Freetime &&
                    last.End == current.Start)
                {
                    last.End = current.End; // merge
                }
                else
                {
                    merged.Add(current);
                }
            }

            _timeRanges = merged;
        }
        public bool DeleteBooking(DateTime start, DateTime end)
        {
            var booking = _timeRanges.FirstOrDefault(r => r.Type == TimeRangeType.Booked &&
                                                          r.Start == start &&
                                                          r.End == end);
            if (booking == null) return false;

            _timeRanges.Remove(booking);
            _timeRanges.Add(new TimeRange
            {
                Name = "Freetime",
                Type = TimeRangeType.Freetime,
                Start = start,
                End = end
            });

            MergeAdjacentFreetime();
            return true;
        }
    }
}
