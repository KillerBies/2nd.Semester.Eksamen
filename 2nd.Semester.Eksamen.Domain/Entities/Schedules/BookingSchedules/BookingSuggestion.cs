using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules.BookingSchedules
{
    public class BookingSuggestion
    {
        public List<BookingItem> Items { get; set; } = new();
        public DateTime Start => Items.First().Start;
        public DateTime End => Items.Last().End;
    }
}
