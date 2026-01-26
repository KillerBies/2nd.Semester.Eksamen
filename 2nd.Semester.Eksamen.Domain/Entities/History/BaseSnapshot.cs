using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record BaseSnapshot
    {
        public int Id { get; protected set; }
        public Guid RefrenceId { get; protected set; }
        public BaseSnapshot(Guid refrenceId)
        {
            RefrenceId = refrenceId;
        }
        public BaseSnapshot() { }
    }
}
