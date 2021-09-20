using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WholeSale.MyClass;

namespace WholeSale.Forms
{
    public partial class Modal_Payment : Form
    {
        /// <summary>
        ///  defualt status of payment
        /// </summary>
       global.statusList status =global.statusList.PAY;
        decimal totalPay = 0;
        decimal payIn =0;
        decimal change = 0;
        decimal pending = 0;
        Bill myBill = new Bill();
        DocumentDisplay documentHeader = new DocumentDisplay();
       public bool payComplete { get; set; }
          mainResult rs { get; set; }
        Boolean isPrintBill = true;
        List<DocumentLineDisplay> documentLine = new List<DocumentLineDisplay>();

        private void clear() {


             totalPay = 0;
             payIn = 0;
             change = 0;
             pending = 0;
        }
        public Modal_Payment(DocumentDisplay docHeader , List<DocumentLineDisplay> documentLine , Customer customer , Vendor vendor )
        {
           
            InitializeComponent();
            clear();
            payment.clear();
            this.documentHeader = docHeader;
            this.documentLine = documentLine;
            
            myBill = new Bill();
            myBill.docHeader = documentHeader;
            myBill.docLine = documentLine;
            myBill.Cutomer = customer;
            myBill.vendor = vendor;
            setSummary();



        }

   

        private void button17_Click(object sender, EventArgs e)
        {

            if (validatePayment().isComplete)
            {

                if (tbxPayIn.Text.ToString().Length == 0) { payIn = 0; } else { payIn = Convert.ToDecimal(tbxPayIn.Text.ToString()); }
                if (tbReturn.Text.ToString().Length == 0) { change = 0; } else { change = Convert.ToDecimal(tbReturn.Text.ToString()); }
                if (tbOverdue.Text.ToString().Length == 0) { pending = 0; } else { pending  = Convert.ToDecimal(tbOverdue.Text.ToString()); }


                calCulatePayment();
                payment.isComplete = true;
                payIn = payIn;
                change = change;
                pending = pending;
                myBill.payIn = payIn;
                myBill.change = change;
                myBill.pending = pending;
                myBill.docHeader.status = (int)this.status;
              mainResult rs =  Operation.pay(myBill);
                if (rs.isComplete) {

                    if (chkboxPrintBill.Checked) {
                        Printer prnt = new Printer(myBill.docHeader , myBill.docLine);
                        prnt.print();


                    }
                    string msgText = "จ่ายเงินสำเร็จ " + Environment.NewLine;
                    msgText += "ราคารวม  = " + myBill.docHeader.totalPriceAfterAllDiscount.ToString() + " บาท" + Environment.NewLine;
                    msgText += "จ่ายเงิน  =" + payIn.ToString() + " บาท" + Environment.NewLine;
                    msgText += "เงินทอน  =" + change.ToString() + " บาท" + Environment.NewLine;
                    mMsgBox.show(msgText);
                    rs.isComplete = true;
                    rs.message = "ทำรายการสำเร็จ";
                    rs.status = "OK";
                    payComplete = true;
                }
                else {
                    mMsgBox.show(rs.message);
                    rs.isComplete = false;
                    rs.message = "ทำรายการไม่สำเร็จ";
                    rs.status = "ERROR";
                    payComplete = false;
                }
              
        
               

                this.Dispose();
            }
            else {

                //mMsgBox.show("ทำรายการไม่สำเร็จ");
                //rs.isComplete = false;
                //rs.message = "ทำรายการไม่สำเร็จ";
                //rs.status = "ERROR";

            }
        }

       
        private void calCulatePayment() {
            payment.clear();
           // decimal payIn = 0;
            if (tbxPayIn.Text=="") { payIn = 0; } else { payIn = System.Convert.ToDecimal(tbxPayIn.Text); }
            payment.totalPriceAfterDiscount = documentHeader.totalPriceAfterAllDiscount;
            payment.isPrint = chkboxPrintBill.Checked;
            payment.payIn = payIn;

            if (payIn - payment.totalPriceAfterDiscount > 0) {
                payment.change = Math.Abs(payIn - payment.totalPriceAfterDiscount) ;
            } else {
                payment.overdue = Math.Abs(payIn - payment.totalPriceAfterDiscount);
            }
           
          
            payment.isComplete = true;


    }

        private mainResult validatePayment() {
            mainResult rs = new mainResult();
           // payIn = payment.totalPriceAfterDiscount;
            if (tbxPayIn.Text.ToString() == "") {

                payIn = 0;
            }
            else {

                payIn = System.Convert.ToDecimal(tbxPayIn.Text.ToString());

            }

            if (payIn < payment.totalPriceAfterDiscount)
            {
               // mMsgBox.show()
              DialogResult result =  mMsgBox.show("ยืนยันการจ่ายเงินแบบคงค้าง?.", Modal_MsgBox.MessageBoxButtons.YesNo, Modal_MsgBox.icon.warning,"ยืนยันการบันทึก");

                if (result .Equals(DialogResult.Yes)) {

                    //Pay    
                    rs.isComplete = true;

                }
                else {

                    rs.isComplete = false;
                }
            }
            else {


                rs.isComplete = true;


            }

       

            return rs;

        }

   
        private void bt9_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "9";
            payment.clearbt();

        }

        private void btAll_Click(object sender, EventArgs e)
        {

            tbxPayIn.Text = payment.totalPriceAfterDiscount.ToString();
            payment.clearbt();
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "4";
            payment.clearbt();
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "5";
            payment.clearbt();
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "6";
            payment.clearbt();
        }

        private void bt1000_Click(object sender, EventArgs e)
        {

            btAmountClick(1000);


        }

        private void btAmountClick(int amount) {
            switch (amount)
            {
                case 100:
                    if (payment.inp100)
                    {
                        tbxPayIn.Text = (decimal.Parse(tbxPayIn.Text) + amount).ToString();

                    }
                    else
                    {
                        payment.clearbt();
                        tbxPayIn.Text = (amount).ToString();
                     

                    }
                    payment.inp100 = true;
                    break;
                case 500:
                    if (payment.inp500)
                    {
                        tbxPayIn.Text = (decimal.Parse(tbxPayIn.Text) + amount).ToString();

                    }
                    else
                    {
                        payment.clearbt();
                        tbxPayIn.Text = (amount).ToString();
                     

                    }
                    payment.inp500 = true;
                    break;
                case 1000:

                    if (payment.inp1000)
                    {
                        tbxPayIn.Text = (decimal.Parse(tbxPayIn.Text) + amount).ToString();

                    }
                    else
                    {
                        payment.clearbt();
                        tbxPayIn.Text = (amount).ToString();
                    

                    }
                    payment.inp1000 = true;
                    break;
            }

          


        }

        private void bt1_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "1";
            payment.clearbt();
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "2";
            payment.clearbt();
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "3";
            payment.clearbt();
        }

        private void bt500_Click(object sender, EventArgs e)
        {
            btAmountClick(500);
        }

        private void btDot_Click(object sender, EventArgs e)

        {

            if  (tbxPayIn.Text.Trim().Length == 0) {


                tbxPayIn.Text = "0.";

            }

            if (tbxPayIn.Text.Contains("."))
            {


          

            }
            else {

                tbxPayIn.Text += ".";
            }
          
        }

        private void btZero_Click(object sender, EventArgs e)
        {

            if (tbxPayIn.Text.Trim().Length == 0)
            {




            }
            else {
                tbxPayIn.Text += "0";


            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {

            if (tbxPayIn.Text.Length > 0) { tbxPayIn.Text = tbxPayIn.Text.Substring(0, tbxPayIn.Text.Length - 1); }
        
        }

        private void bt100_Click(object sender, EventArgs e)
        {
            btAmountClick(100);
        }

        private void chkboxPrintBill_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bt7_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "7";
            payment.clearbt();
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text += "8";
            payment.clearbt();
        }

        private void tbxPayIn_TextChanged(object sender, EventArgs e)
        {


            calCulatePayment();

            setSummary();




            btnClearNum.Visible = tbxPayIn.Text.Length>0?true:false;
        

        }


       private void setSummary() {

            payment.totalPriceAfterDiscount =documentHeader.totalPriceAfterAllDiscount;

            tbPay.Text = payment.totalPriceAfterDiscount.ToString();



            tbReturn.Text = "0.00";
            tbOverdue.Text = "0.00";
            if ((payIn - payment.totalPriceAfterDiscount) >= 0) {
                payment.change = payIn - payment.totalPriceAfterDiscount;
  
            }
            else {
                payment.overdue = payment.totalPriceAfterDiscount - payIn;

            }
         //   payment.overdue = payment.totalPriceAfterDiscount - payIn;


            if (payment.change != 0)
            {
                tbReturn.Text = payment.change.ToString();
              
             }
            if (payment.overdue != 0) {

                tbOverdue.Text = payment.overdue.ToString();
            }
            tbPay.Text = decimal.Parse(tbPay.Text.ToString()).ToString("#,##0.#0");
            tbReturn.Text = decimal.Parse(tbReturn.Text.ToString()).ToString("#,##0.#0");
            tbOverdue.Text = decimal.Parse(tbOverdue.Text.ToString()).ToString("#,##0.#0");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            payment.isComplete = false;
            this.Dispose();
        }

        private void btnClearNum_Click(object sender, EventArgs e)
        {
            tbxPayIn.Text = "";
            calCulatePayment();
            setSummary();
        }

        private void Modal_Payment_Load(object sender, EventArgs e)
        {
           // tbPay.Text = payment.totalNetPay.ToString();
        }


        #region Print

        private static int m_currentPageIndex = 0;
        private static List<Stream> m_streams;

        private void Print(Bill myBill)
        {
            DataTable dt = new DataTable();
            DataTable dtDocline = new DataTable();
            List<DocumentDisplay> lisDocH = new List<DocumentDisplay>();
            lisDocH.Add(myBill.docHeader);

            dt = Util.ToDataTable(lisDocH);
            dtDocline = Util.ToDataTable(myBill.docLine);

            ReportDataSource rpDsHeader = new ReportDataSource("dsHeader", dt);
            ReportDataSource rpDsLine = new ReportDataSource("dsLine", dtDocline);

            //this.reportViewer1.LocalReport.DataSources.Clear();
            //this.reportViewer1.LocalReport.DataSources.Add(rpDsHeader);
            //this.reportViewer1.LocalReport.DataSources.Add(rpDsLine);
            //this.reportViewer1.RefreshReport();

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


        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);

        }


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


        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }


        #endregion
    }
}
