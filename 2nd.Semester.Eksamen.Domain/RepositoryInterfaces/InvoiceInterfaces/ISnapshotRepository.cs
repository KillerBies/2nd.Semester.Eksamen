using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.History;


namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces
{
    public interface ISnapshotRepository
    {
        public  Task CreateNewAsync(OrderSnapshot orderSnapshot);

        public Task<List<OrderSnapshot>>? GetAllOrderSnapshotsAsync();
        public Task<OrderSnapshot?> GetByIdAsync(int id);
        
      




    }
}
