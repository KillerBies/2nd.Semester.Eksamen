using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.PDFManagement;
using QuestPDF.Companion;
using QuestPDF.Fluent;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.InvoiceRepositories
{
    public class GenerateInvoice : IGenerateInvoice
    {


        public Byte[] GenerateInvoicePDF(OrderSnapshot orderSnapshot)
        {
            var document = new InvoicePDFCreator(orderSnapshot);

            Byte[] pdfAsBytes = document.GeneratePdf();
            return pdfAsBytes;

        }
        public void ShowCompanionInvoicePDF(OrderSnapshot orderSnapshot)
        {
            var document = new InvoicePDFCreator(orderSnapshot);
            document.ShowInCompanion();
        }
    }
}