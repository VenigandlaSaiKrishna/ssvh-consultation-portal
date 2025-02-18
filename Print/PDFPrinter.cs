using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.IdentityModel.Tokens;
using SSVH_Consultation_Poratl.Models;

public class PDFPrinter
{
    public byte[] GenerateConsultationReport(Consultation consultation)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Rectangle a9Size = new Rectangle(105, 147);
            Document doc = new Document(a9Size, 2, 2, 8, 8);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            writer.PageEvent = new PdfHeaderFooter();
            doc.Open();
            doc.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6))
            {
                SpacingAfter = 15
            });

            PdfPTable table = new PdfPTable(2)
            {
                WidthPercentage = 95,
                HorizontalAlignment = Element.ALIGN_CENTER
            };

            decimal? toatalFees = 0M;

            if (consultation.AdmissionFees > 0)
                toatalFees += consultation.AdmissionFees;

            if (consultation.AcadamicFees > 0)
                toatalFees += consultation.AcadamicFees;

            //if (consultation.BooksAmount > 0)
            //    toatalFees += consultation.BooksAmount;

            //if (consultation.UniformAmount > 0)
            //    toatalFees += consultation.UniformAmount;

            //if (consultation.BeltTieSocksAmount > 0)
            //    toatalFees += consultation.BeltTieSocksAmount;

            if (consultation.TransportAmount > 0)
                toatalFees += consultation.TransportAmount;

            if (consultation.IsAcadamicFeesDiscount)
                toatalFees -= consultation.AcadamicFeesDiscount;

            table.SetWidths(new float[] { 50f, 50f });
            AddRow(table, "Academic Year", consultation.AcadamicYear);
            AddRow(table, "Student Name", consultation.StudentName);
            AddRow(table, "Parent Name", consultation.ParentName);
            AddRow(table, "Mobile No", consultation.Mobile);
            AddRow(table, "Class", consultation.ClassName);
            AddRow(table, "Academic Fees", $"{consultation.AcadamicFees}");
            AddRow(table, "Admission Fees", $"{consultation.AdmissionFees}");
            //AddRow(table, "Books Fees", $"{consultation.BooksAmount}");
            //AddRow(table, "Uniform Fees", $"{consultation.UniformAmount}");
            //AddRow(table, "Belt/Tie/Socks Fees", $"{consultation.BeltTieSocksAmount}");
            AddRow(table, "Transport Fees", $"{consultation.TransportAmount}");
            AddRow(table, "Total Fees", $"{toatalFees}");
            doc.Add(table);
            if (writer.PageNumber == writer.PageNumber)
            {
                Paragraph footer = new Paragraph("Thank you!", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 5))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 5
                };
                doc.Add(footer);
                Paragraph footerContact = new Paragraph("Contact: 90596 29689, 73375 53175", FontFactory.GetFont(FontFactory.HELVETICA, 5))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 5
                };
                doc.Add(footerContact);
            }
            doc.Close();
            return ms.ToArray();
        }
    }

    private void AddRow(PdfPTable table, string field, object value)
    {
        Font font = FontFactory.GetFont(FontFactory.COURIER, 5);
        string formattedValue;

        if (field.ToLowerInvariant().Contains("fees"))
        {
            if (string.IsNullOrEmpty(value.ToString()))
                formattedValue = "₹ 0/-";
            else
                formattedValue = Convert.ToDecimal(value) > 0 ? $"₹ {Convert.ToDecimal(value):N0}/-" : "₹ 0/-";
        }
        else
        {
            formattedValue = value?.ToString() ?? "N/A";
        }

        PdfPCell cell1 = new PdfPCell(new Phrase(field, font))
        {
            Border = Rectangle.BOX,
            Padding = 2,
            HorizontalAlignment = Element.ALIGN_RIGHT
        };
        PdfPCell cell2 = new PdfPCell(new Phrase(formattedValue, font))
        {
            Border = Rectangle.BOX,
            Padding = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        };
        table.AddCell(cell1);
        table.AddCell(cell2);
    }
}
