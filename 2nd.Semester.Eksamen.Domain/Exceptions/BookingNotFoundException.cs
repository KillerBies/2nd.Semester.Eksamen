using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Exceptions
{
    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException() { }
        public BookingNotFoundException(string message) : base(message) { }
        public BookingNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
