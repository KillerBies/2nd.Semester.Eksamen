using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Exceptions
{
    public class GeneralBookingException : Exception
    {
        public GeneralBookingException() { }
        public GeneralBookingException(string message) : base(message) { }
        public GeneralBookingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
