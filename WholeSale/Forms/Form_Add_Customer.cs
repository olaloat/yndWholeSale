using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WholeSale.Model;

namespace WholeSale.Forms
{
    public partial class Form_Add_Customer : Form
    {
        public Form_Add_Customer()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            using (Form_Add_Product fb = new Form_Add_Product())
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            var rs = new mainResult();

                rs = insertDataToDB();
               
                using (Modal_MsgBox msg = new Modal_MsgBox(rs.message))
                {
                    msg.StartPosition = FormStartPosition.CenterParent;
                    msg.ShowDialog();

                }

            if (rs.isComplete) {

                Form_Search_Customer.isAddCustomerComplete = true;

                this.Dispose(); }
            
             


            

        }

        private mainResult validateData() {

            mainResult rs = new mainResult();
            ynddevEntities db = new ynddevEntities();
         
            List<Product> prd = new List<Product>();
            prd = (from a in db.Products where a.productCode.Contains(tbCustName.Text) select a).ToList();
            if (prd.Count > 0) {
                rs.isComplete = false;
                rs.message = "รหัสสินค้านี้มีอยู่ในระบบแล้ว ไม่สามารถบันทึกซ้ำได้";

                return rs;

            }

            rs.isComplete = true;
            rs.message = "ok";

            return rs;



        }


        private mainResult insertDataToDB() {
            mainResult rs = new mainResult();
            try { 
           
            ynddevEntities db = new ynddevEntities();
                Customer cust = new Customer() {
                    customerName = tbCustName.Text.Trim(),
                    contactName =tbContactName.Text.Trim(),
                customerLevel = int.Parse(tbCustLevel.Text.Trim()),
                postal = tbPostal.Text.Trim() ,
                city = tbCity.Text.Trim(),
                 mobile = tbMobile.Text.Trim(),
                    customerCurPnt = int.Parse(tbCurPnt.Text.Trim()),
                    customerTolPnt = int.Parse(tbTolPnt.Text.Trim()),
                    address1 = tbAddress1.Text.Trim(),
                    address2 = tbAddress1.Text.Trim(),
                    tel = tbTel.Text.Trim(),
                    fax = tbFax.Text.Trim(),
                    email = tbEmail.Text.Trim(),
                   branchCode=Global.branchCode,
                   compCode=Global.compCode,
                     createBy ="ADMIN",
                      createTime =DateTime.Now,
                       editTime =DateTime.Now,
                       editBy = "ADMIN"
            };

            db.Customers.Add(cust);
            db.SaveChanges();

            }
            catch (Exception e)
            {
                string xxx = e.ToString();
                rs.isComplete = false;
                rs.message = "ERROR";
             
                rs.message = e.ToString();
                return rs;
            }


            rs.isComplete = true;
            rs.message = "บันทึกสำเร็จ";

            return rs;

        }

        private void Form_Add_customer_Load(object sender, EventArgs e)
        {
         
        }

        private void tbTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = validateNumber(e);
        }

        private void tbPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = validateNumber(e);
        }

        private void tbMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = validateNumber(e);
        }

        private void tbCurPnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = validateNumber(e);
        }

        private void tbTolPnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = validateNumber(e);
        }

        private void tbCustLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = validateNumber(e);
        }

        private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = validateNumber(e);
        }




        private bool validateNumber(KeyPressEventArgs e) {
        //private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        //{
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ' ') //The  character represents a backspace
            {
                return false; //Do not reject the input
            }
            else
            {
                return true; //Reject the input
            }

            return true;
     //   }
    }
    }
}
