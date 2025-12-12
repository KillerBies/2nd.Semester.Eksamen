using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.History;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces
{
    public interface IGenerateInvoice
    {

        public Byte[] GenerateInvoicePDF(OrderSnapshot orderSnapshot);
        public void ShowCompanionInvoicePDF(OrderSnapshot orderSnapshot);
    }
}
