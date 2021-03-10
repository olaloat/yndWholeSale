
using Microsoft.Reporting.WinForms;

using System;


using System.Collections.Generic;


using System.ComponentModel;


using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WholeSale
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

 



        private static List<Stream> m_streams;
        private static int m_currentPageIndex = 0;
   

        private void button1_Click(object sender, EventArgs e)
        {
            //string sql = "select productId, productCode, productName, productTypeId, productRefId, categoryId, typeId, groupId, unitId, minPrice, maxPrice, previousPrice, price, reqSn, ctlExpDate, image1Id, image2Id, image3Id, orderBy, isActive, createBy, createTime, editBy, editTime, compCode, branchCode from Product";
            string sql = "select top 30 productCode, productName, price from Product";
            var dt = GetDataTable(sql);
            LocalReport report = new LocalReport();
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath).Remove(path.Length - 10) + @"\Report\rpProduct.rdlc";
            report.ReportPath = fullPath;
            report.DataSources.Add(new ReportDataSource("dsProduct", dt));
            int printQty = Convert.ToInt32(1);
            for (int i = 0; i < printQty; i++)
            {
                PrintToPrinter(report);
            }
        }

        public DataTable GetDataTable(string sql)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                var dt = new DataTable();
                con.Open();
                SqlDataAdapter adpt = new SqlDataAdapter(sql, con);
                adpt.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);

        }

        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                 <OutputFormat>EMF</OutputFormat>
                <PageWidth>3in</PageWidth>
                <PageHeight>10.6in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0.1in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
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

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }

























}
