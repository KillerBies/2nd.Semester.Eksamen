using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules
{
    public class ScheduleDay : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateOnly Date { get; private set; }
        public ICollection<TimeRange> TimeRanges { get; set; } = new List<TimeRange>();
        public ScheduleDay() { }
        public ScheduleDay(DateOnly date, TimeOnly workStart, TimeOnly workEnd)
        {
            Date = date;
            var start = workStart;
            var end = workEnd;
            TimeRanges.Add(new TimeRange(start, end)
            {
                Type = "Freetime",
            });
        }

        public bool BookDayForVacation(Guid id, TimeOnly start, TimeOnly end)
        {
            if (TimeRanges.Any(tr => tr.Type != "Freetime" && tr.Type != "Unavailable")) return false;
            TimeRanges = new List<TimeRange>();
            var vacation = new TimeRange(start, end) { Type = "Unavailable", ActivityId = id };
            TimeRanges.Add(vacation);
            return true;
        }
        public bool AvailableInTimeRange(TimeRange timerange)
        {
            return !TimeRanges.Any(tr => tr.HasOverlap(timerange) && tr.Type!= "Freetime");
        }
        public bool CheckIfAvailable(TimeSpan duration)
        {
            return TimeRanges.Any(r => r.Type == "Freetime" && r.Duration >= duration);
        }

        public TimeRange? GetAvailableTimeRange(TimeSpan duration)
        {
            return TimeRanges.FirstOrDefault(r => r.Type == "Freetime" && r.Duration >= duration);
        }

        public IEnumerable<TimeRange> GetAllAvailableSlots(TimeSpan duration)
        {
            return TimeRanges.Where(r => r.Type == "Freetime" && r.Duration >= duration).ToList().OrderBy(r => r.Start);
        }

        public IEnumerable<TimeRange> GetOverlappingFreetime(TimeRange booking)
        {
            return TimeRanges
                .Where(r => r.Type == "Freetime" && r.HasOverlap(booking))
                .OrderBy(r => r.Start);
        }

        public bool AddBooking(TreatmentBooking treatment, Guid bookingID, string treatmentname = null)
        {
            /*
             check if the timeRange overlaps with any existing TimeRanges
                if it does, throw an exception
            if it doesn't, add it to the TimeRanges list
            reschedule the TimeRanges list to account for the new booking
            do this by splitting any existing TimeRanges that overlap with the new booking into multiple TimeRanges
            and adjusting the start and end times of the existing TimeRanges accordingly
             */
            string treatmentName = "";
            if(treatmentname != null)
            {
                treatmentName = treatmentname;
            }
            else if(treatment.Treatment != null)
            {
                treatmentName = treatment.Treatment.Name;
            }
                var booking = new TimeRange(TimeOnly.FromDateTime(treatment.Start), TimeOnly.FromDateTime(treatment.End)) { Name = treatmentName, Type = "Booked", ActivityId = bookingID };
            var free = TimeRanges.FirstOrDefault(r => r.Type == "Freetime" && r.Start <= booking.Start && r.End >= booking.End);
            if (free == null)
                throw new Exception();
            // before the booking
            TimeRanges.Remove(free);

            //If free time timerange Starts at booking start, no freetime before booking (only one free time needed after booking)
            // Split freetime if needed
            if (free.Start < booking.Start) //if freetime starts before booking start then we need a freetime timerange before booking
                TimeRanges.Add(new TimeRange(free.Start,booking.Start) { Name = "Freetime", Type = "Freetime" });

            if (free.End > booking.End)//if freetime ends after booking end then we need a freetime timerange after booking
                TimeRanges.Add(new TimeRange(booking.End, free.End) { Name = "Freetime", Type = "Freetime" });

            booking.Type = "Booked";
            TimeRanges.Add(booking);
            MergeAdjacentFreetime();
            return true;
        }
        //We need to merge adjacent free time ranges when a booking is made to avoid a bunch of small free time ranges like: [9-10 free][11-12 free] should become [9-12 free]
        private void MergeAdjacentFreetime()
        {
            var merged = new List<TimeRange>();

            foreach (var current in TimeRanges.OrderBy(r => r.Start))
            {
                if (merged.Count == 0)
                {
                    merged.Add(current);
                    continue;
                }

                var last = merged.Last();
                if (last.Type == "Freetime" &&
                    current.Type == "Freetime" &&
                    last.End == current.Start)
                {
                    last.End = current.End; 
                }
                else
                {
                    merged.Add(current);
                }
            }

            TimeRanges = merged;
        }
        public bool CancelBooking(TreatmentBooking treatment)
        {
            var _start = TimeOnly.FromDateTime(treatment.Start);
            var _end = TimeOnly.FromDateTime(treatment.End);
            var booking = TimeRanges.FirstOrDefault(r => r.Type == "Booked" &&
                                                          r.Start == _start &&
                                                          r.End == _end);
            if (booking == null) return false;

            TimeRanges.Remove(booking);
            TimeRanges.Add(new TimeRange(_start, _end)
            {
                Name = "Freetime",
                Type = "Freetime",
            });

            MergeAdjacentFreetime();
            return true;
        }
        public bool UpdateDaySchedule(TreatmentBooking treatment, Guid bookingId)
        {
            // Find booked TimeRange by ActivityId only
            var bookingRange = TimeRanges.FirstOrDefault(r => r.Type == "Booked" && r.ActivityId == bookingId);
            if (bookingRange == null) return false;

            var oldStart = bookingRange.Start;
            var oldEnd = bookingRange.End;

            var newStart = TimeOnly.FromDateTime(treatment.Start);
            var newEnd = TimeOnly.FromDateTime(treatment.End);

            bookingRange.Start = newStart;
            bookingRange.End = newEnd;

            if (newStart > oldStart)
                TimeRanges.Add(new TimeRange(oldStart, newStart) { Type = "Freetime" });

            if (newEnd < oldEnd)
                TimeRanges.Add(new TimeRange(newEnd, oldEnd) { Type = "Freetime" });

            MergeAdjacentFreetime();
            return true;
        }

    }
}
