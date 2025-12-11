using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using QuestPDF.Infrastructure;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace _2nd.Semester.Eksamen.Infrastructure.PDFManagement
{

    public class InvoicePDFCreator : IDocument
    {
        private byte[] _logo;
        public OrderSnapshot InvoiceOrder { get; set; }
        public InvoicePDFCreator(OrderSnapshot orderSnapshot)
        {
            InvoiceOrder = orderSnapshot;
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(
                "_2nd.Semester.Eksamen.Infrastructure.Assets.Logo.png"
            );

            using var ms = new MemoryStream();
            stream.CopyTo(ms);

            _logo = ms.ToArray();
        }

        public void Compose(IDocumentContainer container)
        {

            container.Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Scale(0.5f).Image(_logo);
                    var scale = 0.8f;
                    var address = InvoiceOrder.BookingSnapshot.CustomerSnapshot.AddressSnapshot;


                    column.Item().Text("");
                    column.Item().Text("");
                    column.Item().Text("");
                    column.Item().Scale(scale).Text("Kundeinformation:").SemiBold() ;
                    //Checks if customer is privatecustomer, if true inserts first and last name.
                    if (InvoiceOrder.BookingSnapshot.CustomerSnapshot is PrivateCustomerSnapshot privateCustomer)
                    {
                        column.Item().Scale(scale).Text($"Kunde: {privateCustomer.Name + " " + privateCustomer.LastName}");
                    }
                    if (InvoiceOrder.BookingSnapshot.CustomerSnapshot is CompanyCustomerSnapshot companyCustomer)
                    {
                        column.Item().Scale(scale).Text($"Kunde: {companyCustomer.Name}");
                        column.Item().Scale(scale).Text($"CVR: {companyCustomer.CVR}");
                    }

                    column.Item().Scale(scale).Text($"{address.PostalCode} {address.City}");
                        column.Item().Scale(scale).Text($"{address.StreetName} {address.HouseNumber}");

                });

                row.RelativeItem().Border(1).Column(column =>
                {
                    var padding = 5;
                    //INVOICE BOX UPPER RIGHT OF PAGE
                    column.Item().BorderBottom(1).Padding(padding).Text("Faktura for behandlinger").SemiBold();
                    column.Item().Text("");
                    column.Item().Padding(padding).Text($"Faktura #{InvoiceOrder.Id}");
                    column.Item().Text("");
                    column.Item().Padding(padding).Text($"Betalt dato: {InvoiceOrder.DateOfPayment}").SemiBold();
                    column.Item().Text("");

                    

                });
            });
        }


        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);
                column.Item().Element(ComposeTable);
                column.Item().Text("");
                column.Item().Text("");
                if (InvoiceOrder.AppliedDiscountSnapshot.ProductDiscount != null)
                { 
                column.Item().AlignRight().Text($"Produktrabat: {InvoiceOrder.AppliedDiscountSnapshot.ProductDiscount}");
                }
                if (InvoiceOrder.AppliedDiscountSnapshot.TreatmentDiscount != null)
                {
                    column.Item().AlignRight().Text($"Behandlingsrabat: {InvoiceOrder.AppliedDiscountSnapshot.TreatmentDiscount}");
                }
                
                column.Item().AlignRight().Text($"Moms: {InvoiceOrder.VAT}");
                column.Item().AlignRight().Text($"Total: {InvoiceOrder.TotalAfterDiscount}");
            });
        }

        private void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {

                // /
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
                // //
                table.Header(header =>
                {
                    header.Cell().Element(CellStyling).Text("Produkt:");
                    header.Cell().Element(CellStyling).AlignRight().Text("Mængde:");
                    header.Cell().Element(CellStyling).AlignRight().Text("Pris:");
                    header.Cell().Element(CellStyling).AlignRight().Text("Total:");
                    static IContainer CellStyling(IContainer container)
                    {

                        return container.DefaultTextStyle(c => c.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });
                // ///
                foreach (var item in InvoiceOrder.BookingSnapshot.TreatmentSnapshot) //Runs through list of treatments
                {
                    table.Cell().Element(CellStyling).Text($"{item.Name}");
                   if item.OrderLines.
                    table.Cell().Element(CellStyling).Text($"1");
                    table.Cell().Element(CellStyling).AlignRight().Text($"{item.PriceWithMultiplier}");
                    table.Cell().Element(CellStyling).AlignRight().Text($"{item.DiscountedPrice}");

                    static IContainer CellStyling(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }

                }
                foreach (var item in InvoiceOrder.OrderLinesSnapshot) //Runs through list of Products
                {
                    table.Cell().Element(CellStyling).Text(item.ProductSnapshot.Name);
                    table.Cell().Element(CellStyling).AlignRight().Text($"{item.ProductSnapshot.PricePerUnit}");
                    table.Cell().Element(CellStyling).AlignRight().Text($"{item.NumberOfProducts}");
                    table.Cell().Element(CellStyling).AlignRight().Text($"{item.ProductSnapshot.DiscountedPrice}");
                    static IContainer CellStyling(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }

                }
            });
        }

        

        private void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(c =>
            {
                c.CurrentPageNumber();
                c.Span(" / ");
                c.TotalPages();
            });
        }
    }
}
