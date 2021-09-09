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

namespace WholeSale
{
    public partial class Form_POS : Form


    {

        ynddevEntities yndInven = new ynddevEntities();
        List<Product> mstProduct = new List<Product>();
        Document MydocHeader = new Document();
        List<DocumentLine> MydocLine = new List<DocumentLine>();
        Bill myBill = new Bill();
        BindingSource bdsProduct = new BindingSource();
        private readonly object stbScan;

        private void loadMaster() {
            mstProduct = (from a in yndInven.Products select a ).ToList();// yndInven.Products.ToList();
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

       

        private void setSummary() {
            lbAmount.Text = myBill.totalAmountBeforeVat.ToString();
            lbVat.Text = myBill.totalVat.ToString();
            lbAmpintIncludeVat.Text = myBill.totalAmountIncludeVat.ToString();
            lbDiscount.Text = myBill.discount.ToString();
            lbTotal.Text = (myBill.totalAmountBeforeVat - myBill.discount).ToString(); // lbAmpintIncludeVat.Text;
            lbAmountAfterDiscount.Text = (myBill.totalAmountBeforeVat -myBill.discount).ToString();
        }

        private void tbScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                addData();
                tbScan.Text ="";

            }
        }

        private void addData() {
            var mySelectedProduct = myBill.filterProd(mstProduct, (tbScan.Text));
            if (mySelectedProduct.Count >= 1)
            {

                MydocLine = myBill.setList(mySelectedProduct.FirstOrDefault(), int.Parse(tbxQty.Text));
                this.dataGridView1 = new DataGridView();



                var bs = new BindingSource();

                bs.DataSource = MydocLine;
                dataGridView3.DataSource = bs;

                dataGridView3.Refresh();
                setSummary();
            }
            else {

                string message = "ไม่พบรายการสินค้าที่ท่านสแกน";
                string title = "กรุณาตรวจสอบ";
                MessageBox.Show(message, title);
            }
        }


        private void deleteData() {

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
    }




}
