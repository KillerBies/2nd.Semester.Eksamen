using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public record CreateBookingCommand(
        Guid EmployeeId,
        Guid CustomerId,
        DateTime StartTime,
        DateTime EndTime
    );
}
