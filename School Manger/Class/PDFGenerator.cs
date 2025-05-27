using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace School_Manger.Class
{
    public class PDFGenerator
    {
        public List<PDFTable> Tbl = new List<PDFTable>();
        public List<PDFTable> TblPer = new List<PDFTable>();
        public List<Paragraph> Titles = new List<Paragraph>();
        public List<Paragraph> TitlesPer = new List<Paragraph>();
        public List<Paragraph> Footer = new List<Paragraph>();
        public List<Paragraph> FooterPer = new List<Paragraph>();
        public List<Paragraph> Graphs = new List<Paragraph>();
        public List<Paragraph> GraphsPer = new List<Paragraph>();
        public MemoryStream Generate()
        {
            var stream = new MemoryStream();

            using (var writer = new PdfWriter(stream))
            {
                writer.SetCloseStream(false);

                using (var pdf = new PdfDocument(writer))
                {
                    string fontPath = Path.Combine(Environment.CurrentDirectory,"wwwroot", "fonts", "B-NAZANIN.ttf");
                    PdfFont font = PdfFontFactory.CreateFont(fontPath,PdfEncodings.IDENTITY_H);
                    var document = new Document(pdf);

                    foreach(Paragraph gra in Titles)
                    {
                        document.Add(gra);
                    }
                    foreach(Paragraph gra in TitlesPer)
                    {
                        gra.SetBaseDirection(BaseDirection.RIGHT_TO_LEFT)
                           .SetFont(font);
                        document.Add(gra);
                    }

                    foreach (Paragraph gra in Graphs)
                    {
                        document.Add(gra);
                    }
                    foreach (Paragraph gra in GraphsPer)
                    {
                        gra.SetBaseDirection(BaseDirection.RIGHT_TO_LEFT)
                           .SetFont(font);
                        document.Add(gra);
                    }
                    foreach (PDFTable tbl in Tbl)
                    {
                        document.Add(tbl.Table);
                    }
                    foreach (PDFTable tbl in TblPer)
                    {
                        tbl.Table.SetBaseDirection(BaseDirection.RIGHT_TO_LEFT)
                           .SetFont(font);
                        document.Add(tbl.Table);
                    }

                    foreach (Paragraph gra in Footer)
                    {
                        document.Add(gra);
                    }
                    foreach (Paragraph gra in FooterPer)
                    {
                        gra.SetBaseDirection(BaseDirection.RIGHT_TO_LEFT)
                           .SetFont(font);
                        document.Add(gra);
                    }

                    document.Close();
                }
            }

            stream.Position = 0;
            return stream;
        }

    }
    public class PDFTable
    {
        public Table Table { get; set; }
        public PDFTable(int Columns,int SetWidth, HorizontalAlignment alignment)
        {
            Table = new Table(Columns)
                .SetHorizontalAlignment(alignment)
                .SetWidth(UnitValue.CreatePercentValue(SetWidth));
        }
        public PDFTable AddRow(string Title,object Value)
        {
            Table.AddCell(new Cell().Add(new Paragraph(Title)).SetTextAlignment(TextAlignment.CENTER));
            Table.AddCell(new Cell().Add(new Paragraph(Value.ToString())).SetTextAlignment(TextAlignment.CENTER));
            return this;
        }
    }
}
