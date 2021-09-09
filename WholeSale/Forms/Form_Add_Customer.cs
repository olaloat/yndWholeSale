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
using WholeSale.MyClass;

namespace WholeSale.Forms
{
    public partial class Form_Add_Customer : Form
    {
       option. mode activeMode = option. mode.NEW;
        public Customer myCustomer { get; set; }
        public bool isActionComplete { get; set; }
        public int customerID { get; set; }

        public bool IsUpdateComplete { get; set; }


        public Form_Add_Customer(option.mode mode = option.mode.NEW )
        {

            this.activeMode = mode;
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            using (Form_Add_Customer fb = new Form_Add_Customer())
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

            mainResult rs = new mainResult();
            // calidate product code
            if (activeMode == option.mode.NEW)
            {
                rs = validateData();
                if (!rs.isComplete)
                {
                    mMsgBox.show(rs.message);
                    return;
                }
                rs = insertDataToDB();

            }
            else if (activeMode == option.mode.EDIT)
            {


                rs = editDataToDB(myCustomer);
            }


            mMsgBox.show(rs.message);
            if (rs.isComplete) {



                IsUpdateComplete = true;
             //   Operation.loadCustomer();



                this.Dispose();

            } else {

                IsUpdateComplete = false;
            }
         

        }


        private mainResult editDataToDB(Customer myCustomer)
        {
            mainResult rs = new mainResult();
            try
            {

                //Customer cust = new Customer()
                //{
                myCustomer. customerName = tbCustName.Text.Trim();
                 myCustomer.  customerLevel = Convert.ToInt16(tbCustLevel.Text.ToString().Trim());
                 myCustomer.  contactName = tbContactName.Text.Trim();
                 myCustomer.  address1 = tbAddress1.Text.Trim();
                 myCustomer.  address2 = tbAddress2.Text.Trim();
                 myCustomer.  city = tbCity.Text.Trim();
                 myCustomer.  tel = tbTel.Text.Trim();
                 myCustomer.  fax = tbFax.Text.Trim();
                 myCustomer.  customerCurPnt = Convert.ToInt16(tbCurPnt.Text.ToString().Trim());
                 myCustomer.  postal = tbPostal.Text.Trim();
                 myCustomer.  mobile = tbMobile.Text.Trim();
                 myCustomer.  email = tbEmail.Text.Trim();
                 myCustomer.  customerTolPnt = Convert.ToInt16(tbTolPnt.Text.ToString().Trim());

                
              myCustomer. editTime = DateTime.Now;
             myCustomer. editBy = "ADMIN";
           // };

          


                rs = db.updateCustomer(myCustomer);

            }
            catch (Exception e)
            {
                string xxx = e.ToString();
                rs.isComplete = false;
                rs.message = "ERROR";
                rs.isComplete = true;
                rs.message = e.ToString();
                return rs;
            }


            rs.isComplete = true;
            rs.message = "บันทึกสำเร็จ";

            return rs;



        }



        private mainResult insertDataToDB()
        {
            mainResult rs = new mainResult();
            try
            {

                ynd db = new ynd();
                Customer cust = new Customer();

                //{
                //       customerName= tbCustName.Text.Trim(),
                //      customerLevel = Convert.ToInt16(tbCustLevel.Text.ToString().Trim()),
                //      contactName=       tbContactName.Text.Trim(),
                //      address1 =    tbAddress1.Text.Trim(),
                //      address2 =    tbAddress2.Text.Trim(),
                //      city =    tbCity.Text.Trim(),
                //      tel =  tbTel.Text.Trim(),
                //     fax =   tbFax.Text.Trim(),
                //customerCurPnt = Convert.ToInt16(tbCurPnt.Text.ToString().Trim()),
                //    postal  =   tbPostal.Text.Trim(),
                // mobile =   tbMobile.Text.Trim(),
                // email =   tbEmail.Text.Trim(),
                // customerTolPnt = Convert.ToInt16(tbTolPnt.Text.ToString().Trim()),
                //};





        cust.      customerName = tbCustName.Text.Trim();
        cust.            customerLevel = int.Parse(tbCustLevel.Text.ToString().Trim());
        cust.            contactName = tbContactName.Text.Trim();
        cust.            address1 = tbAddress1.Text.Trim();
        cust.            address2 = tbAddress2.Text.Trim();
        cust.            city = tbCity.Text.Trim();
        cust.            tel = tbTel.Text.Trim();
        cust.           fax = tbFax.Text.Trim();
        cust.      customerCurPnt = int.Parse(tbCurPnt.Text.ToString().Trim());
        cust.          postal = tbPostal.Text.Trim();
        cust.       mobile = tbMobile.Text.Trim();
        cust.       email = tbEmail.Text.Trim();
        cust. customerTolPnt = int.Parse(tbTolPnt.Text.ToString().Trim());

                cust.branchCode = global.BranchCode;
                cust.compCode = global.compCode;
                cust.createBy = "ADMIN";
                cust.createTime = DateTime.Now;
                cust.editTime = DateTime.Now;
                cust.editBy = "ADMIN";
                db.Customers.Add(cust);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                string xxx = e.Message. ToString();
                rs.isComplete = false;
                rs.message = "ERROR";
                rs.isComplete = true;
                rs.message = e.ToString();
                return rs;
            }


            rs.isComplete = true;
            rs.message = "บันทึกสำเร็จ";

            return rs;

        }


        private mainResult validateData()
        {

            mainResult rs = new mainResult();
            //ynd db = new ynd();

            //List<Product> prd = new List<Product>();
            //prd = (from a in db.Customers where a.customerId.Contains(tbcus.Text) select a).ToList();
            //if (prd.Count > 0)
            //{
            //    rs.isComplete = false;
            //    rs.message = "รหัสสินค้านี้มีอยู่ในระบบแล้ว ไม่สามารถบันทึกซ้ำได้";

            //    return rs;

            //}

            rs.isComplete = true;
            rs.message = "ok";

            return rs;



        }



        //private mainResult validateData() {

        //    mainResult rs = new mainResult();
        //    ynddevEntities db = new ynddevEntities();

        //    List<Product> prd = new List<Product>();
        //    prd = (from a in db.Products where a.productCode.Contains(tbCustName.Text) select a).ToList();
        //    if (prd.Count > 0) {
        //        rs.isComplete = false;
        //        rs.message = "รหัสสินค้านี้มีอยู่ในระบบแล้ว ไม่สามารถบันทึกซ้ำได้";

        //        return rs;

        //    }

        //    rs.isComplete = true;
        //    rs.message = "ok";

        //    return rs;



        //}


        //private mainResult insertDataToDB() {
        //    mainResult rs = new mainResult();
        //    try { 

        //    ynddevEntities db = new ynddevEntities();
        //        Customer cust = new Customer() {
        //            customerName = tbCustName.Text.Trim(),
        //            contactName =tbContactName.Text.Trim(),
        //        customerLevel = int.Parse(tbCustLevel.Text.Trim()),
        //        postal = tbPostal.Text.Trim() ,
        //        city = tbCity.Text.Trim(),
        //         mobile = tbMobile.Text.Trim(),
        //            customerCurPnt = int.Parse(tbCurPnt.Text.Trim()),
        //            customerTolPnt = int.Parse(tbTolPnt.Text.Trim()),
        //            address1 = tbAddress1.Text.Trim(),
        //            address2 = tbAddress1.Text.Trim(),
        //            tel = tbTel.Text.Trim(),
        //            fax = tbFax.Text.Trim(),
        //            email = tbEmail.Text.Trim(),
        //           branchCode=Global.branchCode,
        //           compCode=Global.compCode,
        //             createBy ="ADMIN",
        //              createTime =DateTime.Now,
        //               editTime =DateTime.Now,
        //               editBy = "ADMIN"
        //    };

        //    db.Customers.Add(cust);
        //    db.SaveChanges();

        //    }
        //    catch (Exception e)
        //    {
        //        string xxx = e.ToString();
        //        rs.isComplete = false;
        //        rs.message = "ERROR";

        //        rs.message = e.ToString();
        //        return rs;
        //    }


        //    rs.isComplete = true;
        //    rs.message = "บันทึกสำเร็จ";

        //    return rs;

        //}

     


        private Customer loadCustomer(int id)
        {
            ynd myEn = new ynd();


            var customerSelect = myEn.Customers.Where(w => w.customerId == id).FirstOrDefault();

            return customerSelect;
        }

        private void setCustDataToControl(Customer myCust) {
            tbCustName.Text = myCust.customerName.ToString();
            tbCustLevel.Text = myCust.customerLevel.ToString(); ;
            tbContactName.Text = myCust.contactName.ToString();
            tbAddress1.Text = myCust.address1.ToString();
            tbAddress2.Text = myCust.address2.ToString();
            tbCity.Text = myCust.city.ToString();
            tbTel.Text = myCust.tel.ToString();
            tbFax.Text = myCust.fax.ToString();
            tbCurPnt.Text = myCust.customerCurPnt.ToString();
            tbPostal.Text = myCust.postal.ToString();
            tbMobile.Text = myCust.mobile.ToString();
            tbEmail.Text = myCust.email.ToString();
            tbTolPnt.Text = myCust.customerTolPnt.ToString();


        }

        private void tbTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Util.validateNumber(e);
        }

        private void tbPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Util.validateNumber(e);
        }

        private void tbMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Util.validateNumber(e);
        }

        private void tbCurPnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Util.validateNumber(e);
        }

        private void tbTolPnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Util.validateNumber(e);
        }

        private void tbCustLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Util.validateNumber(e);
        }

        private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Util.validateNumber(e);
        }

     


  

        private void btClear_Click(object sender, EventArgs e)
        {
            clearControl();
        }


        private void clearControl() {
            tbCustName.Text = "";
tbCustLevel          .Text="0";
tbContactName        .Text="";
tbAddress1           .Text="";
tbAddress2           .Text="";
tbCity               .Text="";
tbTel                .Text="";
tbFax                .Text="";
tbCurPnt             .Text="";
tbPostal             .Text="";
tbMobile             .Text="";
tbEmail              .Text="";
            tbTolPnt.Text= "";



        }

        private void tbCustLevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form_Add_Customer_Load_1(object sender, EventArgs e)
        {

            clearControl();
            if (activeMode == option.mode.NEW)
            {
                myCustomer = new Customer();
                //   clearControl();
            }

            if (activeMode == option.mode.EDIT)
            {
                //   clearControl();
                myCustomer = loadCustomer(customerID);
                setCustDataToControl(myCustomer);
            }
        }
    }
}
