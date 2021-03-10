using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WholeSale.Forms
{
    public partial class Modal_Payment : Form
    {


        decimal totalPay = 0;



        Boolean isPrintBill = true;
        public Modal_Payment(decimal _totalPay)
        {
            InitializeComponent();

            totalPay = _totalPay;
        }

        private void button17_Click(object sender, EventArgs e)
        {

            if (validatePayment().isComplete)
            {
                calCulatePayment();
                payment.isComplete = true;

                this.Dispose();
            }
            else {



            }
        }


        private void calCulatePayment() {
            payment.clear();
            decimal pay = 0;
            if (tbxPayIn.Text=="") { pay = 0; } else {pay = System.Convert.ToDecimal(tbxPayIn.Text); }
            payment.totalAmount = totalPay;
            payment.isPrint = chkboxPrintBill.Checked;
            payment.income = pay;

            if (pay - totalPay >0) {
                payment.change = Math.Abs(pay - totalPay) ;
            } else {
                payment.overdue = Math.Abs(pay - totalPay);
            }
           
          
            payment.isComplete = true;


    }

        private mainResult validatePayment() {
            mainResult rs = new mainResult();
            decimal pay = 0;
            if (tbxPayIn.Text.ToString() == "") {

                pay = 0;
            }
            else {

                pay = System.Convert.ToDecimal(tbxPayIn.Text.ToString());

            }

            if (pay < totalPay)
            {


                using (Modal_MsgBox fb = new Modal_MsgBox("จ่ายเงินไม่เพียงพอ"))
                {

                    fb.StartPosition = FormStartPosition.CenterParent;
                    fb.ShowDialog();
                    rs.isComplete = false;

                    return rs;
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

            tbxPayIn.Text = totalPay.ToString();
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


            tbPay.Text = totalPay.ToString();
            tbReturn.Text =   payment.change.ToString();
            tbOverdue.Text = payment.overdue.ToString();




        }
    }
}
