using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Produkter
{
    public class Treatment : BaseProduct
    {
        //Elements of a treatment. Its info is stored in the database.
        public TreatmentType Treatment_Type { get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}