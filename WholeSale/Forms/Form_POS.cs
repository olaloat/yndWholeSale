
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
       // List<Product> mstProduct = new List<Product>();
        Document MydocHeader = new Document();
        List<DocumentLine> MydocLine = new List<DocumentLine>();
        Bill myBill = new Bill();
        BindingSource bdsProduct = new BindingSource();
        public static string productCodeSelect = "";


        public static string  selectedProductCode
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

        //private void loadMaster()
        //{
        //    Global.mstProduct = (from a in yndInven.Products select a).ToList();// yndInven.Products.ToList();
        //}




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

            productMaintain.loadMaster();
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
            //  this.dataGridView3.Columns["dcPrice"].Visible = false;
            this.dataGridView3.Columns["discountTotal"].Visible = false;
            
            this.dataGridView3.Columns["discountUnit"].Visible = false;
            this.dataGridView3.Columns["isActive"].Visible = false;
            this.dataGridView3.Columns["editTime"].Visible = false;
            this.dataGridView3.Columns["amountIncludeVat"].Visible = false;



            this.dataGridView3.Columns["productName"].HeaderText = "ชื่อสินค้า";
            this.dataGridView3.Columns["unit"].HeaderText = "หน่วย";
            this.dataGridView3.Columns["Qty"].HeaderText = "จำนวน";
            this.dataGridView3.Columns["unitPrice"].HeaderText = "ราคา/หน่วย";
            this.dataGridView3.Columns["amount"].HeaderText = "ราคารวม";
            //this.dataGridView3.Columns["discountUnit"].HeaderText = "ส่วนลด/หน่วย";
            this.dataGridView3.Columns["dcPrice"].HeaderText = "ส่วนลด/หน่วย";
            // this.dataGridView3.Columns["discountTotal"].HeaderText = "ส่วนลดรวม";
            this.dataGridView3.Columns["dcPriceTotal"].HeaderText = "ส่วนลดรวม";

            this.dataGridView3.Columns["Qty"].DefaultCellStyle.Format = "0.00##";
            this.dataGridView3.Columns["unitPrice"].DefaultCellStyle.Format = "0.00##";
            this.dataGridView3.Columns["amount"].DefaultCellStyle.Format = "0.00##";
            this.dataGridView3.Columns["dcPrice"].DefaultCellStyle.Format = "0.00##";
            this.dataGridView3.Columns["dcPriceTotal"].DefaultCellStyle.Format = "0.00##";

            //this.dgvDynamics1.Columns.Items[i].DefaultCellStyle.Format = "0.00##";
            //this.dgvDynamics1.Columns.Items[i].ValueType = GetType(Double)


            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            dataGridView3.Columns.Add(btnEdit);
            btnEdit.HeaderText = "Edit";
            btnEdit.Text = "Edit";
            btnEdit.Name = "btn";
            btnEdit.UseColumnTextForButtonValue = true;

            dataGridView3.Columns.Add(btnDel);
            btnDel.HeaderText = "Delete";
            btnDel.Text = "Delete";
            btnDel.Name = "btn";
            btnDel.UseColumnTextForButtonValue = true;

            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.AllowUserToAddRows = false;


        }
        private void dataGridView3_CellClick_1(object sender, DataGridViewCellEventArgs e)

              
        {

            string  prodCodeSelect = "";

            if (e.RowIndex >= 0) {
                prodCodeSelect = this.dataGridView3.Rows[e.RowIndex].Cells["productId"].Value.ToString();

                if (this.dataGridView3.Columns[e.ColumnIndex].HeaderText == "Edit") {
                    DocumentLine prdslct = new DocumentLine();

                     prdslct = MydocLine.Where(w => w.productId.ToString() == prodCodeSelect.ToString()).FirstOrDefault();


                    editLine(prdslct);


                   
            }

            if (this.dataGridView3.Columns[e.ColumnIndex].HeaderText == "Delete")
            {

                deleteLine(prodCodeSelect);
            }

                setSummary();

        }


    }


        private void editLine(DocumentLine prdSlect) {

            using (Modal_Edit fb = new Modal_Edit(prdSlect))
            {
                //productMaintain.clear();
                //productCodeSelect = "";
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();



                var bs = new BindingSource();

                bs.DataSource = MydocLine;
                dataGridView3.DataSource = bs;

                dataGridView3.Refresh();
                setSummary();
                Bill.isHasList = Bill.list.Count > 0 ? true : false;



            }
        }



        private void deleteLine(string productCode)
        {

            var bs = new BindingSource();
            var myDelete = MydocLine.Where(w => w.productId == int.Parse(productCode)).ToList();

        
            if (myDelete.Count() > 0)
            {
                foreach ( var item in myDelete) {
                    MydocLine.Remove(item);
                }
              
            }



            bs.DataSource = MydocLine;
            dataGridView3.DataSource = bs;

            dataGridView3.Refresh();
            setSummary();
            Bill.isHasList = Bill.list.Count > 0 ? true : false;
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }



        private void tbxQty_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void setSummary()
        {






            myBill.totalDiscountLine = MydocLine.Sum(s => s.dcPriceTotal).Value;
            myBill.totalDiscount = myBill.totalDiscountLine + myBill.finalDiscount;
            myBill.totalAmountBeforeDiscount = MydocLine.Sum(s => s.amount);


            myBill.totalAmountIncludeVat = myBill.totalAmountBeforeDiscount - myBill.totalDiscount;
            myBill.totalAmountBeforeVat =Math.Round( (myBill.totalAmountIncludeVat - (myBill.totalAmountIncludeVat * 7 / 107)) , 2);//.ToString("#.##");
            myBill.totalVat = myBill.totalAmountIncludeVat - myBill.totalAmountBeforeVat;



            lbAmount.Text = myBill.totalAmountBeforeDiscount.ToString("#,##0.#0");
            lbVat.Text = myBill.totalVat.ToString("#,##0.#0");
          //  lbAmpintIncludeVat.Text = myBill.totalAmountIncludeVat.ToString();
            lbDiscount.Text = myBill.totalDiscount.ToString("#,##0.#0");
            //  lbTotal.Text = (myBill.totalAmountBeforeVat - myBill.discount).ToString(); // lbAmpintIncludeVat.Text;
            lbAmountAfterDiscount.Text = (myBill.totalAmountBeforeDiscount - myBill.totalDiscount).ToString("#,##0.#0");
            lbAmountIncludeVat.Text = lbAmountAfterDiscount.Text;
           
            label33.Text = myBill.totalAmountBeforeVat.ToString("#,##0.#0");

           
        }

        private void clearData() {
            myBill = new Bill();
            //var bs = new BindingSource();
            //bs.DataSource = new DocumentLine();
            //dataGridView3.DataSource = bs;

            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            setSummary();
        }

        private void clearSummary() {

            myBill.totalDiscountLine = 0;
            myBill.totalDiscount = 0;
            myBill.totalAmountBeforeDiscount = 0;


            myBill.totalAmountIncludeVat = 0;
            myBill.totalAmountBeforeVat = 0;
            myBill.totalVat = 0;



            lbAmount.Text = "0";
            lbVat.Text = "0";
            lbDiscount.Text = "0";
            lbAmountAfterDiscount.Text = "0";
            lbAmountIncludeVat.Text = "0";

            label33.Text = "0";



        }



        private void addData (DocumentLine dcLine)
        {
            var mySelectedProduct = myBill.filterProd(Global.mstProduct, (tbScan.Text));
            if (mySelectedProduct.Count >= 1)
            {

                MydocLine = myBill.setList(mySelectedProduct.FirstOrDefault(), int.Parse(tbxQty.Text));
                this.dataGridView1 = new DataGridView();
                if (dcLine != null) {


                  //  foreach (var a in MydocLine.LastOrDefault) {
                        MydocLine.LastOrDefault().discountTotal = dcLine.discountTotal;
                        MydocLine.LastOrDefault().discountUnit = dcLine.discountUnit;
                        MydocLine.LastOrDefault().dcPrice = dcLine.dcPrice;
                        MydocLine.LastOrDefault().dcPriceTotal = dcLine.dcPriceTotal;
                  //  }
                 


                }


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



       private void OpenPayment()
        {
            using (Modal_Payment fb = new Modal_Payment(myBill.totalAmountIncludeVat))
            {

                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

                if (payment.isComplete)
                {

                    myBill.pay = fb.pay;
                    myBill.pending = fb.pending;
                    myBill.change = fb.change;

                    myBill.payBill();
                    string msgText = "จ่ายเงินสำเร็จ " + Environment.NewLine;
                    msgText += "ราคารวม  = " + payment.totalAmount.ToString() + " บาท" + Environment.NewLine;
                    msgText += "จ่ายเงิน  =" + payment.income.ToString() + " บาท" + Environment.NewLine;
                    msgText += "เงินทอน  =" + payment.change.ToString() + " บาท" + Environment.NewLine;
                    using (Modal_MsgBox msg = new Modal_MsgBox(msgText))
                    {
                        msg.StartPosition = FormStartPosition.CenterParent;
                        msg.ShowDialog();
                    }
                    clearData();
                }else
                {

                     


                        myBill.finalDiscount = 0;
                        setSummary();
               





                }
            }



        }

        private void btPayment_Click(object sender, EventArgs e)
        {
      
            if (Bill.list.Count > 0)
            {
                OpenPayment();
            }
            else {
                using (Modal_MsgBox fb = new Modal_MsgBox("ไม่มีรายการสินค้า"))
                {

                    fb.StartPosition = FormStartPosition.CenterParent;
                    fb.ShowDialog();
                 
                }
            }
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
            if (e.KeyChar == (char)13)
            {
                addData(null);
                tbScan.Text = "";
                tbxQty.Text = "1";
            }
        }

        private void btSearchProduct_Click(object sender, EventArgs e)
        {
            using (Form_Search_Product fb = new Form_Search_Product(Global.mstProduct , true))
            {
                productMaintain.clear();
                productCodeSelect = "";
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

                if (productMaintain.haveNewProduct) { productMaintain.loadMaster(); }
                if   (productCodeSelect != "") {

                    tbScan.Text = productCodeSelect;
                    productCodeSelect = "";
                    tbScan.Focus();
                    SendKeys.SendWait("{ENTER}");


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
              
                if (customerInfo.isSelected ) {

                    lbCustomer.Text = customerInfo.customerName;
                    lbAddress.Text = customerInfo.customerAddress;

                    customerInfo.clear();
                }
               
            }
        }

        private void tbScan_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPuase_Click(object sender, EventArgs e)


        {
            optionType enumType = new optionType();

            if (MydocLine.Count > 0) {

                enumType = optionType.holding;


            } else {
                enumType = optionType.openHolding;
            }
            using (Modal_MsgBox msg = new Modal_MsgBox("ท่านต้องการจะดำเนินการ เรื่องใด ?", enumType))
            {
                msg.StartPosition = FormStartPosition.CenterParent;
                msg.ShowDialog();
            if (   msg. selectResult != Modal_MsgBox.resultOption.cancel) { 

                if (enumType==optionType.openHolding)
                {
                    msg.Dispose();
                    loadDocument(Bill.docHeaderID);
                    Bill.isFromHolding = true;
                }

                if (enumType == optionType.holding)
                {
                    clearData();
                }

                }

            }
        }


        private void loadDocument(int docH) {
            var doclist = (from a in yndInven.DocumentLines where a.DocumentId == docH  && a.isActive ==true select a).ToList();
            var docHeader = (from a in yndInven.Documents where a.documentId == docH && a.isActive == true  select a).ToList();
            myBill = new Bill();
            Bill.docHeaderID = docH;
            Bill.documentNumber = docHeader.Select(s => s.documentNo.Trim()).FirstOrDefault().ToString();
            label7.Text = Bill.documentNumber;
            foreach (DocumentLine docline in doclist) {
              
               tbxQty.Text = decimal.ToInt64(docline.qty).ToString();
               tbScan.Text = Global.mstProduct.Where(w => w.productId == docline.productId).Select(s => s.productCode).FirstOrDefault().ToString();
                addData(docline); // add discount form data base document line. 
                tbScan.Text = "";
                tbxQty.Text = "1";
            }



        }

        private void btCancel_Click(object sender, EventArgs e)
        {
           

            using (Modal_MsgBox msg = new Modal_MsgBox("ยืนยันการ การยกเลิกรายการ ?", optionType.yseNoOk))
            {
                msg.StartPosition = FormStartPosition.CenterParent;
                msg.ShowDialog();


                if (msg.selectResult== Modal_MsgBox.resultOption.yes) {
                    if (Bill.documentNumber.ToString().Trim().Length>0) {


                        Bill.disableDocHeader(Bill.docHeaderID);
                        Bill.disableDocLine(Bill.docHeaderID);
                    }
                    clearData();

                }

                if (msg.selectResult == Modal_MsgBox.resultOption.no)
                {
                   

                }



            }
        }

        private void btFinalDc_Click(object sender, EventArgs e)
        {
            //using (Modal_FinalDc msg = new Modal_FinalDc())
            //{

            //    payment.totalAmount = Bill.list.Sum(s => s.amount);
            //    msg.StartPosition = FormStartPosition.CenterParent;
            //    msg.ShowDialog();


              



            //}



            //////////////////////////
            if (Bill.list.Count > 0)
            {
                using (Modal_FinalDc fb = new Modal_FinalDc())
                {
                    payment.totalAmount = myBill.totalAmountBeforeDiscount;
                    payment.totalDiscountInline = myBill.totalDiscountLine;
                    payment.dicountBill = myBill.finalDiscount;
                    fb.StartPosition = FormStartPosition.CenterParent;
                 
                    fb.ShowDialog();

                    if (fb.isFinalComplete)
                    {
                        myBill.finalDiscount = fb.finalDiscount;
                        setSummary();
                        OpenPayment();
                    }
                    else {

                        myBill.finalDiscount = fb.finalDiscount;
                        setSummary();
                    }
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
            /////////////////////////
        }

     
    }




}





