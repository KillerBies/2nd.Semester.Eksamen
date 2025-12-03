using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces
{
    internal interface ISnapshotRepository
    {
        public  Task CreateNewAsync(Order order);

    }
}
