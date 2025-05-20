using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SSVH_Consultation_Poratl.Models;

public class PDFPrinter
{
    private bool _firstPage = true;     
    public byte[] GenerateConsultationReport(Consultation consultation)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Rectangle a9Size = new Rectangle(100f, 300f);
            Document doc = new Document(a9Size, 5, 5, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            //writer.PageEvent = new PdfHeaderFooter();
            doc.Open();

            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 98f;
            headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            if (_firstPage)
            {
                try
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "print_logo.png");
                    Image logo = Image.GetInstance(imagePath);
                    logo.ScaleToFit(92f, 60f);
                    PdfPCell logoCell = new PdfPCell(logo);
                    logoCell.Border = Rectangle.NO_BORDER;
                    logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    headerTable.AddCell(logoCell);

                }
                catch { }
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.RED);
                PdfPCell titleCell = new PdfPCell(new Phrase("Consultation Details", titleFont));
                titleCell.Border = Rectangle.NO_BORDER;
                titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerTable.AddCell(titleCell);
                doc.Add(headerTable);
            }
            _firstPage = false;
            
            doc.Add(new Paragraph(".", FontFactory.GetFont(FontFactory.HELVETICA, 6))
            {
                PaddingTop = 15f
            });

            float[] columnWidths = new float[] { 1.3f, 1.7f };

            PdfPTable table = new PdfPTable(2)
            {
                WidthPercentage = 98f,
                HorizontalAlignment = Element.ALIGN_MIDDLE,
                PaddingTop = 200f
            };
            

            table.SetWidths(columnWidths);

            table.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            table.LockedWidth = true;


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
            AddRow(table, "Term Fees", "4 * " + $"{Convert.ToDecimal(consultation.AcadamicFees/4):N0}/-");
            AddRow(table, "Academic Fees", $"{consultation.AcadamicFees}");
            AddRow(table, "Admission Fees", $"{consultation.AdmissionFees}");
            AddRow(table, "Books Fees", $"{consultation.BooksAmount}");
            AddRow(table, "Uniform Fees", $"{consultation.UniformAmount + consultation.BeltTieSocksAmount}");
            //AddRow(table, "Belt/Tie/Socks Fees", $"{consultation.BeltTieSocksAmount}");
            AddRow(table, "Transport Fees", $"{consultation.TransportAmount}");
            //AddRow(table, "Total Fees", $"{toatalFees}");
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
                Paragraph footerContactLine = new Paragraph("*****************************************", FontFactory.GetFont(FontFactory.HELVETICA, 5))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 5
                };
                doc.Add(footerContactLine);
            }
            doc.Close();
            return ms.ToArray();
        }
    }

    private void AddRow(PdfPTable table, string field, object value)
    {
        Font font = FontFactory.GetFont(FontFactory.COURIER, 5);
        string formattedValue;

        if (field.ToLowerInvariant().Contains("fees") && !field.ToLowerInvariant().Contains("term fees"))
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
            Padding = 1,
            HorizontalAlignment = Element.ALIGN_RIGHT,
            NoWrap = false,
        };
        PdfPCell cell2 = new PdfPCell(new Phrase(formattedValue, font))
        {
            Border = Rectangle.BOX,
            Padding = 2,
            HorizontalAlignment = Element.ALIGN_LEFT,
            NoWrap = false
        };
        table.AddCell(cell1);
        table.AddCell(cell2);
    }
}
