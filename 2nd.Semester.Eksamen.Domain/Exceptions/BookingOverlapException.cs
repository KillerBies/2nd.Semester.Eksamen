using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Exceptions
{
    public class BookingOverlapException : Exception
    {
        public BookingOverlapException() { }
        public BookingOverlapException(string message) : base(message) 
        { 
        }
        public BookingOverlapException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
