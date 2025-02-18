using iTextSharp.text;
using iTextSharp.text.pdf;

public class PdfHeaderFooter : PdfPageEventHelper
{
    private bool _firstPage = true;

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfPTable headerTable = new PdfPTable(1);
        headerTable.TotalWidth = document.PageSize.Width - 10;
        headerTable.DefaultCell.Border = Rectangle.NO_BORDER;
        if (_firstPage)
        {
            try
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Logo.png");
                Image logo = Image.GetInstance(imagePath);
                logo.ScaleToFit(50, 20);
                PdfPCell logoCell = new PdfPCell(logo)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingBottom = 1
                };
                headerTable.AddCell(logoCell);
            }
            catch { }
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, color: BaseColor.RED);
            PdfPCell titleCell = new PdfPCell(new Phrase("Consultation Receipt", titleFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 1
            };
            headerTable.AddCell(titleCell);
            headerTable.WriteSelectedRows(0, -1, 5, document.PageSize.Height - 5, writer.DirectContent);
        }
        _firstPage = false;
    }
}
