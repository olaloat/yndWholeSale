using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using WholeSale.MyClass;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace WholeSale.Forms
{
    public partial class PrintBill : Form
    {

        DocumentDisplay docHeader;
        List<DocumentLineDisplay>  listDocLine;

        private static int m_currentPageIndex = 0;
        public PrintBill(DocumentDisplay _docHeader, List<DocumentLineDisplay>  docLine)
        {
            this.docHeader = _docHeader;
            this.listDocLine = docLine;
            InitializeComponent();

        }

        private void PrintBill_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtDocline = new DataTable();
            List<DocumentDisplay> lisDocH = new List<DocumentDisplay>();
            lisDocH.Add(docHeader);

            dt = Util.ToDataTable(lisDocH);
            dtDocline = Util.ToDataTable(listDocLine);

            ReportDataSource rpDsHeader = new ReportDataSource("dsHeader", dt);
            ReportDataSource rpDsLine = new ReportDataSource("dsLine", dtDocline);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rpDsHeader);
            this.reportViewer1.LocalReport.DataSources.Add(rpDsLine);
            this.reportViewer1.RefreshReport();

            LocalReport report = new LocalReport();
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath).Remove(path.Length - 10) + @"\Report\Report1.rdlc";
            report.ReportPath = fullPath;
            report.DataSources.Clear();
            report.DataSources.Add(rpDsHeader);
            report.DataSources.Add(rpDsLine);

          //  report.DataSources.Add(new ReportDataSource("dsSetBill", dt));
            int printQty = Convert.ToInt32(1);
            for (int i = 0; i < printQty; i++)
            {
                PrintToPrinter(report);
            }
        }

        public mainResult print() {
            mainResult rs = new mainResult();

            DataTable dt = new DataTable();
            DataTable dtDocline = new DataTable();
            List<DocumentDisplay> lisDocH = new List<DocumentDisplay>();
            lisDocH.Add(docHeader);

            dt = Util.ToDataTable(lisDocH);
            dtDocline = Util.ToDataTable(listDocLine);

            ReportDataSource rpDsHeader = new ReportDataSource("dsHeader", dt);
            ReportDataSource rpDsLine = new ReportDataSource("dsLine", dtDocline);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rpDsHeader);
            this.reportViewer1.LocalReport.DataSources.Add(rpDsLine);
            this.reportViewer1.RefreshReport();

            LocalReport report = new LocalReport();
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath).Remove(path.Length - 10) + @"\Report\Report1.rdlc";
            report.ReportPath = fullPath;
            report.DataSources.Clear();
            report.DataSources.Add(rpDsHeader);
            report.DataSources.Add(rpDsLine);

            //  report.DataSources.Add(new ReportDataSource("dsSetBill", dt));
            int printQty = Convert.ToInt32(1);
            for (int i = 0; i < printQty; i++)
            {
                PrintToPrinter(report);
            }


            return rs;
        }

        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);

        }

        private void BillBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void DocumentLineDisplayBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private static List<Stream> m_streams;
        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        //==================================================
        //public static void Print()
        //{
        //    if (m_streams == null || m_streams.Count == 0)
        //        throw new Exception("Error: no stream to print.");
        //    PrintDocument printDoc = new PrintDocument();
        //    if (!printDoc.PrinterSettings.IsValid)
        //    {
        //        throw new Exception("Error: cannot find the default printer.");
        //    }
        //    else
        //    {
        //        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
        //        m_currentPageIndex = 0;
        //        printDoc.Print();
        //    }
        //}
        //==================================================

        #region "print"
        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                 <OutputFormat>EMF</OutputFormat>
                <PageWidth>3.5in</PageWidth>
                <PageHeight>10in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
            if (print)
            {
                Print();
            }
        }

        public static void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height
                );
            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }


        #endregion




        //=========================

    }
}
