using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IInvoiceService
    {
        public Task CreateSnapshotInDBAsync(Order order);
        public Task<List<OrderSnapshotDTO>> GetAllSnapshotsAsync();
        public Task GenerateInvoiceById(int orderSnapshotId);


    }


}
