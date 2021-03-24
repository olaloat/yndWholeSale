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
    public partial class Modal_FinalDc : Form
    {
        public Modal_FinalDc()
        {
            InitializeComponent();
        }


        private mainResult validateDiscount() {
           
            mainResult rs = new mainResult();
            rs.isComplete = true;
            if (payment.totalNetPay < 0)
            {
                rs.isComplete = false;
                rs.message = "ส่วนลดมากกว่าราคารวมสินค้า";

            }
            else {

              
            }

                return rs;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            mainResult rs = new mainResult();
            rs = validateDiscount();
            if (!rs.isComplete)
            {
             

                    using (Modal_MsgBox fb = new Modal_MsgBox(rs.message))
                    {

                        fb.StartPosition = FormStartPosition.CenterParent;
                        fb.ShowDialog();
                        rs.isComplete = false;

                     
                    }








                
             
            }
            else
            {



            }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {

            if (tbDiscountBill.Text.Trim().Length == 0)
            {


                tbDiscountBill.Text = "0.";

            }

            if (tbDiscountBill.Text.Contains("."))
            {




            }
            else
            {

                tbDiscountBill.Text += ".";
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (tbDiscountBill.Text.Trim().Length == 0)
            {




            }
            else
            {
                tbDiscountBill.Text += "0";


            }
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            if (tbDiscountBill.Text.Trim().Length == 0)
            {




            }
            else
            {
                tbDiscountBill.Text += "00";


            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "7";
          
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text += "9";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (tbDiscountBill.Text.Length > 0) { tbDiscountBill.Text = tbDiscountBill.Text.Substring(0, tbDiscountBill.Text.Length - 1); }
        }

        private void btnClearNum_Click(object sender, EventArgs e)
        {
            tbDiscountBill.Text = "";
            //calCulatePayment();
            //setSummary();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tbDiscountBill_TextChanged(object sender, EventArgs e)
        {
            calCulatePayment();
            setSummary();
            btnClearNum.Visible = tbDiscountBill.Text.Length > 0 ? true : false;
        }

        private void setSummary()
        {

            tbNetPay.Text = payment.totalNetPay.ToString();
            tbTotalDiscount.Text = payment.totalDiscount.ToString();
        }



        private void calCulatePayment()
        {
            decimal dis = 0;
            if (tbDiscountBill.Text == "") { dis = 0; } else { dis = System.Convert.ToDecimal(tbDiscountBill.Text); }
            payment.dicountBill = dis;
            payment.totalDiscount = payment.totalDiscountInline + payment.dicountBill;

            payment.totalNetPay = payment.totalAmount - payment.totalDiscount;
       






        }

        private void Modal_FinalDc_Load(object sender, EventArgs e)
        {

            clearAllTextBox();
            tbSumTotal.Text = payment.totalAmount.ToString();
            tbTotalDiscountInline.Text = payment.totalDiscountInline.ToString();
            calCulatePayment();
            setSummary();
        }


        private void clearAllTextBox() {
            tbTotalDiscountInline.Text = "";
            tbSumTotal.Text = "";
            tbDiscountBill.Text = "";
            tbTotalDiscountInline.Text = "";
            tbTotalDiscount.Text = "";

                
        
        
        
        }
    }
}
