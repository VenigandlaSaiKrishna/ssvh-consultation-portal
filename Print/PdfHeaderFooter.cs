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
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "print_logo.png");
                Image logo = Image.GetInstance(imagePath);
                logo.ScaleToFit(100f, 50f);
                logo.Alignment = Element.ALIGN_CENTER;
                PdfPCell logoCell = new PdfPCell(logo)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingBottom = 0f
                };
                headerTable.AddCell(logoCell);
            }
            catch { }
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, color: BaseColor.RED);
            PdfPCell titleCell = new PdfPCell(new Phrase("Consultation Details", titleFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 5f
            };
            headerTable.AddCell(titleCell);
            //float yPos = document.PageSize.Height - 30;
            //headerTable.WriteSelectedRows(0, -1, 5, yPos, writer.DirectContent);

            headerTable.WriteSelectedRows(0, -3, 5, document.PageSize.Height, writer.DirectContent);
        }
        _firstPage = false;
    }
}
