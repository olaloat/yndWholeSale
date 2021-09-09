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
//using Microsoft.Reporting.WinForms;
using WholeSale;
using WholeSale.MyClass;


namespace WholeSale
{
  public  class UI
    {


        public UI() {
            Log.print("create new  UI");
        }
        DataGridView grid = new DataGridView();

        public static DataGridView prepareGridDocumentLine( DataGridView grid2, List<DocumentLineDisplay> docLine) {
            DocumentLineDisplay ddd = new DocumentLineDisplay();
            var bs = new BindingSource();
            bs.DataSource = docLine;
            grid2.DataSource = bs;
            grid2.Refresh();
            grid2.DataSource = bs;
            grid2.Refresh();
            grid2.AllowUserToAddRows = false;

            for (var a =0;  a <=grid2.ColumnCount-1;a++) {
                grid2.Columns[a].Visible = false;
            }
            //grid2.Columns["DocumentLineId"].Visible = false;
            //grid2.Columns["invoiceId"].Visible = false;
            //grid2.Columns["invoiceNo"].Visible = false;
            //grid2.Columns["productId"].Visible = false;
            //grid2.Columns["createBy"].Visible = false;
            //grid2.Columns["CreateTime"].Visible = false;
            //grid2.Columns["editBy"].Visible = false;
            //grid2.Columns["compCode"].Visible = false;
            //grid2.Columns["branchCode"].Visible = false;
            //grid2.Columns["DocumentNo"].Visible = false;
            //grid2.Columns["DocumentId"].Visible = false;
            //grid2.Columns["Document"].Visible = false;
            //grid2.Columns["transactions"].Visible = false;
            //grid2.Columns["totalVat"].Visible = false;
            //grid2.Columns["unit"].Visible = false;
            //grid2.Columns["unitVat"].Visible = false;
            //grid2.Columns["vatType"].Visible = false;
            //grid2.Columns["totalPriceBeforeVat"].Visible = false;
            //grid2.Columns["totalPriceBeforeDiscount"].Visible = false;
            //grid2.Columns["isActive"].Visible = false;
            //grid2.Columns["editTime"].Visible = false;


            grid2.Columns["productName"].Visible = true; // "ชื่อสินค้า";
            grid2.Columns["unit"].Visible = true; // "หน่วย";
            grid2.Columns["Qty"].Visible = true; // "จำนวน";
            grid2.Columns["unitPrice"].Visible = true; // "ราคา/หน่วย";
            grid2.Columns["totalPriceAfterDiscount"].Visible = true; // "ราคารวม";
            grid2.Columns["discountUnit"].Visible = true; // "ส่วนลด/หน่วย";
            grid2.Columns["totalDiscount"].Visible = true; // "ส่วนลดรวม";






            grid2.Columns["productName"].HeaderText = "ชื่อสินค้า";
            grid2.Columns["unit"].HeaderText = "หน่วย";
            grid2.Columns["Qty"].HeaderText = "จำนวน";
            grid2.Columns["unitPrice"].HeaderText = "ราคา/หน่วย";
            grid2.Columns["totalPriceAfterDiscount"].HeaderText = "ราคารวม";
            grid2.Columns["discountUnit"].HeaderText = "ส่วนลด/หน่วย";
            grid2.Columns["totalDiscount"].HeaderText = "ส่วนลดรวม";
            grid2.Columns["Qty"].DefaultCellStyle.Format = "0.00##";
            grid2.Columns["unitPrice"].DefaultCellStyle.Format = "0.00##";
            grid2.Columns["totalPriceBeforeDiscount"].DefaultCellStyle.Format = "0.00##";
            grid2.Columns["discountUnit"].DefaultCellStyle.Format = "0.00##";
            grid2.Columns["totalPriceAfterDiscount"].DefaultCellStyle.Format = "0.00##";

            //this.dgvDynamics1.Columns.Items[i].DefaultCellStyle.Format = "0.00##";
            //this.dgvDynamics1.Columns.Items[i].ValueType = GetType(Double)


            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            grid2.Columns.Add(btnEdit);
            btnEdit.HeaderText = "Edit";
            btnEdit.Text = "Edit";
            btnEdit.Name = "btn";
            btnEdit.UseColumnTextForButtonValue = true;

            grid2.Columns.Add(btnDel);
            btnDel.HeaderText = "Delete";
            btnDel.Text = "Delete";
            btnDel.Name = "btn";
            btnDel.UseColumnTextForButtonValue = true;

            grid2.ReadOnly = true;
            grid2.AllowUserToAddRows = false;



            return grid2;
                }

        public static DataGridView prepareGridDocument(DataGridView grid2, List<DocumentDisplay> documentList)
        {
            DocumentLineDisplay ddd = new DocumentLineDisplay();
            var bs = new BindingSource();
            bs.DataSource = documentList;
            grid2.DataSource = bs;
            grid2.Refresh();

            grid2.AllowUserToAddRows = false;

            DataGridViewButtonColumn btnSelect = new DataGridViewButtonColumn();
            grid2.Columns.Add(btnSelect);
            btnSelect.HeaderText = "Select";
            btnSelect.Text = "Select";
            btnSelect.Name = "Select";


            grid2.Columns["documentId"].Visible = false;
            grid2.Columns["orderId"].Visible = false;
            grid2.Columns["customerId"].Visible = false;
            grid2.Columns["vendorId"].Visible = false;


            grid2.Columns["isOrder"].Visible = false;
            grid2.Columns["isTax"].Visible = false;
            grid2.Columns["paidType"].Visible = false;
            grid2.Columns["remark"].Visible = false;
            grid2.Columns["status"].Visible = false;
            grid2.Columns["isActive"].Visible = false;
            grid2.Columns["compCode"].Visible = false;
            grid2.Columns["branchCode"].Visible = false;
            grid2.Columns["createBy"].Visible = false;
            grid2.Columns["createTime"].Visible = false;
            grid2.Columns["editBy"].Visible = false;
            grid2.Columns["editTime"].Visible = false;

            grid2.Columns["editTime"].Visible = false;
            grid2.Columns["documentLines"].Visible = false;
            grid2.Columns["vendorName"].Visible = false;
            grid2.Columns["myResult"].Visible = false;

            grid2.Columns["documentNo"].HeaderText = "เลขที่เอกสาร";
            grid2.Columns["customerName"].HeaderText = "ชื่อลูกค้า";
            grid2.Columns["address1"].HeaderText = "ที่อยู่1";
            grid2.Columns["address2"].HeaderText = "ที่อยู่2";
            grid2.Columns["city"].HeaderText = "จังหวัด";
            grid2.Columns["postalCode"].HeaderText = "รหัสไปรษณี";
            grid2.Columns["tel"].HeaderText = "โทร(สำนักงาน)";
            grid2.Columns["mobile"].HeaderText = "โทร(มือถือ)";
            grid2.Columns["fax"].HeaderText = "FAX";
            grid2.Columns["email"].HeaderText = "อีเมล";

            grid2.Columns["totalVat"].HeaderText = "Vat";
            grid2.Columns["totalLineDiscount"].HeaderText = "ส่วนลดในรายการ";
            grid2.Columns["totalPriceBeforeDiscount"].HeaderText = "ยอดเงินก่อนส่วนลด";
            grid2.Columns["endDiscount"].HeaderText = "ส่วนลดท้ายบิล";
            grid2.Columns["totalDiscount"].HeaderText = "ส่วนลดทั้งหมด";
            grid2.Columns["totalPriceAfterDiscountLine"].HeaderText = "ยอดเงินหลังหักส่วนลดในรายการ";
            grid2.Columns["totalPriceAfterAllDiscount"].HeaderText = "ยอดเงินสุทธิ";
            grid2.Columns["totalPriceBeforeVat"].HeaderText = "ยอดเงินก่อน Vat";
            grid2.Columns["qty"].HeaderText = "จำนวน";

 

            int Indx = 0;
            grid2.Columns["select"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["customerName"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["contactName"].DisplayIndex = Indx; Indx += 1;

            grid2.Columns["qty"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["totalPriceBeforeDiscount"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["totalLineDiscount"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["endDiscount"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["totalDiscount"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["totalPriceAfterDiscountLine"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["totalPriceBeforeVat"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["totalVat"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["totalPriceAfterAllDiscount"].DisplayIndex = Indx; Indx += 1;
        






            grid2.Columns["address1"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["address2"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["city"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["postalCode"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["tel"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["mobile"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["fax"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["email"].DisplayIndex = Indx; Indx += 1;


          
       
            btnSelect.UseColumnTextForButtonValue = true;
            grid2.ReadOnly = true;
            grid2.AllowUserToAddRows = false;
            grid2.Refresh();
            return grid2;
        }



        public static DataGridView prepareGridCustomer(DataGridView grid2, List<Customer> customerList , option.mode mode =option.mode.SELECT )
        {
            DocumentLineDisplay ddd = new DocumentLineDisplay();
            var bs = new BindingSource();
            bs.DataSource = customerList;
            grid2.DataSource = bs;
            grid2.Refresh();

            grid2.Columns["customerId"].Visible = false;
            grid2.Columns["customerLevel"].Visible = false;
            grid2.Columns["customerCurPnt"].Visible = false;
            grid2.Columns["customerTolPnt"].Visible = false;


            grid2.Columns["orderBy"].Visible = false;
            grid2.Columns["isActive"].Visible = false;
            grid2.Columns["createBy"].Visible = false;
            grid2.Columns["createTime"].Visible = false;
            grid2.Columns["editBy"].Visible = false;
            grid2.Columns["editTime"].Visible = false;
            grid2.Columns["compCode"].Visible = false;
            grid2.Columns["branchCode"].Visible = false;
            grid2.Columns["orders"].Visible = false;


            grid2.Columns["customerName"].HeaderText = "ชื่อ";
            grid2.Columns["contactName"].HeaderText = "ชื่อที่ใช้ติดต่อ";
            grid2.Columns["address1"].HeaderText = "ที่อยู่1";
            grid2.Columns["address2"].HeaderText = "ที่อยู่2";
            grid2.Columns["city"].HeaderText = "จังหวัด";
            grid2.Columns["postal"].HeaderText = "รหัสไปรษณี";
            grid2.Columns["tel"].HeaderText = "โทร(สำนักงาน)";
            grid2.Columns["mobile"].HeaderText = "โทร(มือถือ)";
            grid2.Columns["fax"].HeaderText = "FAX";
            grid2.Columns["email"].HeaderText = "อีเมล";

        

           

          

            DataGridViewButtonColumn btnSelect = new DataGridViewButtonColumn();
            grid2.Columns.Add(btnSelect);


            switch (mode) {

                case option.mode.SELECT:
                    btnSelect.HeaderText = "select";
                    btnSelect.Text = "select";
                    btnSelect.Name = "select";
                    break;
                case option.mode.EDIT:
                    btnSelect.HeaderText = "EDIT";
                    btnSelect.Text = "EDIT";
                    btnSelect.Name = "EDIT";
                    break;

            }
            int Indx = 0;
            grid2.Columns[btnSelect.Text].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["customerName"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["contactName"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["address1"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["address2"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["city"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["postal"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["tel"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["mobile"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["fax"].DisplayIndex = Indx; Indx += 1;
            grid2.Columns["email"].DisplayIndex = Indx; Indx += 1;

            btnSelect.UseColumnTextForButtonValue = true;

            grid2.ReadOnly = true;
            grid2.AllowUserToAddRows = false;
            return grid2;
        }


        public static void  setGrid(ref DataGridView  grd, List<DocumentLineDisplay> MydocLine) {

            var bs = new BindingSource();

            bs.DataSource = MydocLine;
            grd.DataSource = bs;

            grd.Refresh();

        }








    }
}
