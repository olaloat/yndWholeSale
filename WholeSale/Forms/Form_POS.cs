
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Data.Entity;
//using Microsoft.Reporting.WebForms;
using System.IO;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
//using Microsoft.Reporting.WebForms;

namespace WholeSale.Forms
{
    public partial class Form_POS : Form


    {

        ynddevEntities yndInven = new ynddevEntities();
        List<Product> mstProduct = new List<Product>();
        Document MydocHeader = new Document();
        List<DocumentLine> MydocLine = new List<DocumentLine>();
        Bill myBill = new Bill();
        BindingSource bdsProduct = new BindingSource();
        public static string productCodeSelect = "";

        int cursor = 0;


        public static string selectedProductCode
        {
            get
            {
                return productCodeSelect;
            }
            set
            {
                productCodeSelect = value;
            }
        }
        private readonly object stbScan;

        private void loadMaster()
        {
            mstProduct = (from a in yndInven.Products select a).ToList();// yndInven.Products.ToList();
        }




        public Form_POS()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_POS_Load(object sender, EventArgs e)

        {
            cursor = 1;
            clearTxtShowScreen();
            loadMaster();
            setSummary();
            MydocLine = new List<DocumentLine>();
            this.dataGridView1 = new DataGridView();

            var bs = new BindingSource();

            bs.DataSource = MydocLine;
            dataGridView3.DataSource = bs;
            dataGridView3.Refresh();
            this.dataGridView3.Columns["DocumentLineId"].Visible = false;
            this.dataGridView3.Columns["invoiceId"].Visible = false;
            this.dataGridView3.Columns["invoiceNo"].Visible = false;
            this.dataGridView3.Columns["productId"].Visible = false;

            this.dataGridView3.Columns["createBy"].Visible = false;
            this.dataGridView3.Columns["CreateTime"].Visible = false;
            this.dataGridView3.Columns["editBy"].Visible = false;
            this.dataGridView3.Columns["compCode"].Visible = false;
            this.dataGridView3.Columns["branchCode"].Visible = false;

            this.dataGridView3.Columns["DocumentNo"].Visible = false;
            this.dataGridView3.Columns["DocumentId"].Visible = false;
            this.dataGridView3.Columns["Document"].Visible = false;
            this.dataGridView3.Columns["transactions"].Visible = false;
            this.dataGridView3.Columns["vat"].Visible = false;


            this.dataGridView3.Columns["vatType"].Visible = false;
            this.dataGridView3.Columns["dcPrice"].Visible = false;
            this.dataGridView3.Columns["isActive"].Visible = false;
            this.dataGridView3.Columns["editTime"].Visible = false;
            this.dataGridView3.Columns["amountIncludeVat"].Visible = false;



            this.dataGridView3.Columns["productName"].HeaderText = "ชื่อสินค้า";
            this.dataGridView3.Columns["unit"].HeaderText = "หน่วย";
            this.dataGridView3.Columns["Qty"].HeaderText = "จำนวน";
            this.dataGridView3.Columns["unitPrice"].HeaderText = "ราคา/หน่วย";
            this.dataGridView3.Columns["amount"].HeaderText = "ราคารวม";
            this.dataGridView3.Columns["discountUnit"].HeaderText = "ส่วนลด/หน่วย";
            this.dataGridView3.Columns["discountTotal"].HeaderText = "ส่วนลดรวม";

            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.AllowUserToAddRows = false;


        }



        private void button15_Click(object sender, EventArgs e)
        {

        }



        private void tbxQty_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void setSummary()
        {
            lbAmount.Text = myBill.totalAmountBeforeVat.ToString();
            lbVat.Text = myBill.totalVat.ToString();
            //  lbAmpintIncludeVat.Text = myBill.totalAmountIncludeVat.ToString();
            lbDiscount.Text = myBill.discount.ToString();
            //  lbTotal.Text = (myBill.totalAmountBeforeVat - myBill.discount).ToString(); // lbAmpintIncludeVat.Text;
            lbAmountAfterDiscount.Text = (myBill.totalAmountBeforeVat - myBill.discount).ToString();
            lbAmountIncludeVat.Text = lbAmountAfterDiscount.Text;
            var totalAmount = System.Convert.ToDecimal(lbAmountIncludeVat.Text);
            label33.Text = (totalAmount - (totalAmount * 7 / 107)).ToString("#.##");


        }

        private void clearData()
        {
            myBill = new Bill();
            //var bs = new BindingSource();
            //bs.DataSource = new DocumentLine();
            //dataGridView3.DataSource = bs;

            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            setSummary();
            clearTxtShowScreen();
        }

        private void clearTxtShowScreen()
        {
            lblDoc.Text = "-";
            lblIssueDate.Text = "-";
            lblRefDoc.Text = "-";
            lblStatus.Text = "-";
            lblEmpName.Text = Global.username;
            lbCustomer.Text = "-";
            lbAddress.Text = "-";
            lblDelivery.Text = "-";
        }

        private void addData()
        {
            var mySelectedProduct = myBill.filterProd(mstProduct, (tbScan.Text));
            if (mySelectedProduct.Count > 0)
            {

                MydocLine = myBill.setList(mySelectedProduct.FirstOrDefault(), int.Parse(tbxQty.Text));
                this.dataGridView1 = new DataGridView();



                var bs = new BindingSource();

                bs.DataSource = MydocLine;
                dataGridView3.DataSource = bs;

                dataGridView3.Refresh();
                setSummary();
                Bill.isHasList = Bill.list.Count > 0 ? true : false;
            }
            else
            {


                using (Modal_MsgBox msg = new Modal_MsgBox("ไม่พบรายการสินค้าที่ท่านสแกน"))
                {
                    msg.StartPosition = FormStartPosition.CenterParent;
                    msg.ShowDialog();

                }
            }
        }


        private void deleteData()
        {
            Bill.isHasList = Bill.list.Count > 0 ? true : false;
        }






        private void button22_Click(object sender, EventArgs e)
        {


        }

        private void button21_Click(object sender, EventArgs e)
        {


            myBill.payBill();

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btPayment_Click(object sender, EventArgs e)
        {
            //LocalReport report = new LocalReport();
            //string path = Path.GetDirectoryName(Application.ExecutablePath);
            //string fullPath = Path.GetDirectoryName(Application.ExecutablePath).Remove(path.Length - 10) + @"\Report\rpBill.rdlc";
            //report.ReportPath = fullPath;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("productCode");
            //dt.Columns.Add("productName");
            //dt.Columns.Add("qty");
            //dt.Rows.Add( "1111","ddddd","5");
            //dt.Rows.Add("1111", "ddddd", "5");
            //dt.Rows.Add("1111", "ddddd", "5");
            //dt.Rows.Add("1111", "ddddd", "5");
            //dt.Rows.Add("1111", "ddddd", "5");
            //report.DataSources.Add(new ReportDataSource("productLIst", dt));
            //Print(report);


            //myBill.payBill();



            if (Bill.list.Count > 0)
            {

                using (Modal_Payment fb = new Modal_Payment(Bill.list.Sum(s => s.amount)))
                {

                    fb.StartPosition = FormStartPosition.CenterParent;
                    fb.ShowDialog();






                    string msgText = "จ่ายเงินสำเร็จ " + Environment.NewLine;

                    msgText += "ราคารวม  = " + payment.totalAmount.ToString() + " บาท" + Environment.NewLine;
                    msgText += "จ่ายเงิน  =" + payment.income.ToString() + " บาท" + Environment.NewLine;
                    msgText += "เงินทอน  =" + payment.change.ToString() + " บาท" + Environment.NewLine;





                    using (Modal_MsgBox msg = new Modal_MsgBox(msgText))
                    {

                        msg.StartPosition = FormStartPosition.CenterParent;
                        msg.ShowDialog();

                    }
                    if (payment.isComplete)
                    {
                        myBill.payBill();

                    }


                    clearData();






                }


            }
            else
            {

                using (Modal_MsgBox fb = new Modal_MsgBox("ไม่มีรายการสินค้า"))
                {

                    fb.StartPosition = FormStartPosition.CenterParent;
                    fb.ShowDialog();

                }


            }

            tbScan.Clear();
            tbScan.Focus();



        }

        private static List<Stream> m_streams;
        public static void Print(LocalReport report)
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

            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                //  printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                //   m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void tbScan_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            cursor = 1;
            if (e.KeyChar == (char)13)
            {
                addData();
                tbScan.Clear();
                tbxQty.Text = "1";
                tbScan.Focus();
            }
        }

        private void tbScan_MouseClick(object sender, MouseEventArgs e)
        {
            cursor = 1;
        }

        private void tbScan_MouseEnter(object sender, EventArgs e)
        {
            cursor = 1;
        }

        private void btSearchProduct_Click(object sender, EventArgs e)
        {
            using (Form_Search_Product fb = new Form_Search_Product(mstProduct))
            {
                productMaintain.clear();
                productCodeSelect = "";
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

                if (productMaintain.haveNewProduct) { loadMaster(); }
                if (productCodeSelect != "")
                {

                    tbScan.Text = productCodeSelect;
                    productCodeSelect = "";
                    tbScan.Focus();
                    SendKeys.SendWait("{ENTER}");
                    tbScan.Clear();

                }
            }
        }

        private void btCustomer_Click(object sender, EventArgs e)
        {
            using (Form_Search_Customer fb = new Form_Search_Customer())
            {

                customerInfo.clear();

                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

                if (customerInfo.isSelected)
                {

                    lbCustomer.Text = customerInfo.customerName;
                    lbAddress.Text = customerInfo.customerAddress;
                    tbScan.Clear();
                    tbScan.Focus();
                    customerInfo.clear();
                }

            }
        }

        private void tbPuase_Click(object sender, EventArgs e)
        {
            using (Modal_MsgBox msg = new Modal_MsgBox("ท่านต้องการจะดำเนินการ เรื่องใด ?", optionType.holding))
            {
                msg.StartPosition = FormStartPosition.CenterParent;
                msg.ShowDialog();


                if (Bill.isFromHolding)
                {
                    msg.Dispose();
                    loadDocument(Bill.docHeaderID);
                    Bill.isFromHolding = true;
                }


                if (Bill.list.Count == 0)
                {
                    clearData();
                }

                tbScan.Clear();
                tbScan.Focus();

            }
        }


        private void loadDocument(int docH)
        {
            var doclist = (from a in yndInven.DocumentLines where a.DocumentId == docH select a).ToList();
            myBill = new Bill();
            Bill.docHeaderID = docH;
            Bill.documentNumber = doclist.Select(s => s.DocumentNo).FirstOrDefault().ToString();
            lblDoc.Text = Bill.documentNumber;
            foreach (DocumentLine docline in doclist)
            {

                tbxQty.Text = decimal.ToInt64(docline.qty).ToString();
                tbScan.Text = mstProduct.Where(w => w.productId == docline.productId).Select(s => s.productCode).FirstOrDefault().ToString();
                addData();
                tbScan.Text = "";
                tbxQty.Text = "1";
            }



        }

        private void tbxQty_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                tbScan.Clear();
                tbScan.Focus();
            }
        }

        private void tbxQty_Enter(object sender, EventArgs e)
        {
            //tbxQty.SelectAll();
            cursor = 2;
        }

        private void tbxQty_MouseEnter(object sender, EventArgs e)
        {
            //tbxQty.SelectAll();
            cursor = 2;
        }

        private void tbxQty_MouseClick(object sender, MouseEventArgs e)
        {
            //tbxQty.SelectAll();
            cursor = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KeyData("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KeyData("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KeyData("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KeyData("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            KeyData("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            KeyData("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            KeyData("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            KeyData("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            KeyData("9");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            KeyData("0");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            KeyData(".");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            KeyData("BACKSPACE");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            KeyData("ENTER");
        }

        private void KeyData(string  i)
        {
            switch (cursor)
            {
                case 1:
                        tbScan.Focus();
                        SendKeys.SendWait("{" + i + "}");
                        //e.Handled = true;
                    break;
                case 2:

                        tbxQty.Focus();
                        SendKeys.SendWait("{" + i + "}");
                    if (i == "ENTER")
                    {
                        cursor = 1;
                    }
                        //e.Handled = true;
                    break;

                default:

                    break;
            }
        }

        private void btDelivery_Click(object sender, EventArgs e)
        {
            tbScan.Clear();
            tbScan.Focus();
        }
    }


}





