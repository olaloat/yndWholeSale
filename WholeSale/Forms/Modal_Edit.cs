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
    public partial class Modal_Edit : Form
    {


        
        public Modal_Edit(DocumentLine productSelect)
        {
            firstLoad = true;
            myPrdLoad = new DocumentLine();
            myPrdLoad = productSelect;
            myEdit = productSelect;
            InitializeComponent();
        }



        DocumentLine myEdit = new DocumentLine();
        DocumentLine myPrdLoad = new DocumentLine();
        bool firstLoad = true;

        private mainResult validateDiscount()
        {

            mainResult rs = new mainResult();
            rs.isComplete = true;
            if (payment.totalNetPay < 0)
            {
                rs.isComplete = false;
                rs.message = "ส่วนลดมากกว่าราคารวมสินค้า";

            }
            else
            {


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


            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));



           
        }

        private void btn0_Click(object sender, EventArgs e)
        {



            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
           
        }

        private void btn00_Click(object sender, EventArgs e)
        {


            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
          
        }


        private void buutonCalculatorClick(string keyText) {

            switch (tbxEditFocus)
            {

                case tbxListEnum.price:
                    tbxPrice.Focus();

                    switch (keyText) {
                        case ".":

                            if (tbxPrice.Text.Trim().Length == 0)
                            {
                                tbxPrice.Text = "0.";
                            }

                            if (tbxPrice.Text.Contains("."))
                            {

                            }
                            else
                            {
                                tbxPrice.Text += ".";
                            }
                            break;


                        case "0":

                            if (tbxPrice.Text.Trim().Length == 0)
                            {




                            }
                            else
                            {
                                tbxPrice.Text += "0";


                            }
                            break;


                        case "00":

                            if (tbxPrice.Text.Trim().Length == 0)
                            {




                            }
                            else
                            {
                                tbxPrice.Text += "0";


                            }
                            break;

                        case "del":


                          
                                if (tbxPrice.Text.Length > 0) { tbxPrice.Text = tbxPrice.Text.Substring(0, tbxPrice.Text.Length - 1); }
                          

                          

                            break;


                        default:
                            tbxPrice.Text += keyText;
                            break;

                    }

              
                    break;
                case tbxListEnum.qty:
                    tbxQty.Focus();

                    switch (keyText)
                    {
                        case ".":

                            if (tbxQty.Text.Trim().Length == 0)
                            {
                                tbxQty.Text = "0.";
                            }

                            if (tbxQty.Text.Contains("."))
                            {

                            }
                            else
                            {
                                tbxQty.Text += ".";
                            }
                            break;


                        case "0":

                            if (tbxQty.Text.Trim().Length == 0)
                            {




                            }
                            else
                            {
                                tbxQty.Text += "0";


                            }
                            break;


                        case "00":

                            if (tbxQty.Text.Trim().Length == 0)
                            {




                            }
                            else
                            {
                                tbxQty.Text += "0";


                            }
                            break;

                        case "del":



                            if (tbxQty.Text.Length > 0) { tbxQty.Text = tbxQty.Text.Substring(0, tbxQty.Text.Length - 1); }




                            break;
                        default:
                            tbxQty.Text += keyText;
                            break;

                    }

                  

                    break;


            }


        }

        private void btn1_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));



         
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
        }



        private void TypeToTextBox(string type) {

        
        }


        private void btn3_Click(object sender, EventArgs e)
        {


            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));

        }

        private void btn4_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));

        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            buutonCalculatorClick((btn.Text));
        }

        private void btnDel_Click(object sender, EventArgs e)
        {


         
            buutonCalculatorClick(("del"));




        }

        private void btnClearNum_Click(object sender, EventArgs e)
        {
         

            //if ((tbxPrice as Control).Focused)
            //{
                tbxPrice.Text = "";
            //}

            //if ((tbxQty as Control).Focused)
            //{
            //    tbxQty.Text = "";



            //}
            //calCulatePayment();
            //setSummary();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {



            this.Dispose();
        }

        private void tbxPrice_TextChanged(object sender, EventArgs e)
        {

            if (firstLoad) { return; }
            calCulatePayment();
            setSummary();
            btnClearNum.Visible = tbxPrice.Text.Length > 0 ? true : false;
        }

        //private void setSummary()
        //{

        //    tbNetPay.Text = payment.totalNetPay.ToString();
        //    tbTotalDiscount.Text = payment.totalDiscount.ToString();
        //}

        private void setSummary() {

            tbxTotalPrice.Text = (myEdit.amount - myEdit.discountTotal).ToString();
        
        }

        private void calCulatePayment()
        {



            decimal price = 0;
            decimal qty = 0;


            if (tbxPrice.Text.ToString() == "") { price = 0; } else { price = decimal.Parse(tbxPrice.Text); }
            if (tbxQty.Text.ToString() == "") { qty = 0; } else { qty = decimal.Parse(tbxQty.Text); }


            myEdit.discountUnit = myEdit.unitPrice - price;
            myEdit.qty = qty;
                myEdit.amount = myEdit.qty * myEdit.unitPrice;
                myEdit.discountTotal = myEdit.qty * myEdit.discountUnit;


          

     





        }

        private void Modal_Edit_Load(object sender, EventArgs e)
        {
            tbxPrice.Text = (myPrdLoad.unitPrice - myPrdLoad.discountUnit).ToString();
            tbxProduct.Text = myPrdLoad.productName.ToString();
            tbxQty.Text = myPrdLoad.qty.ToString();
            tbxTotalPrice.Text = myPrdLoad.amount.ToString();
            tbxUnit.Text = myPrdLoad.unit.ToString();
            tbxPrice_Click(null, null);


            firstLoad = false;

        }


        private tbxListEnum tbxEditFocus;
        private enum tbxListEnum { price , qty}
        private void tbxPrice_Click(object sender, EventArgs e)
        {
            tbxEditFocus = tbxListEnum.price;

            tbxPrice.BackColor = Color.Yellow;
            tbxQty.BackColor = Color.White;
        }

        private void tbxQty_Click_1(object sender, EventArgs e)
        {
            tbxEditFocus = tbxListEnum.qty;

            tbxPrice.BackColor = Color.White;
            tbxQty.BackColor = Color.Yellow;
        }

        private void tbxQty_TextChanged(object sender, EventArgs e)
        {

            if ( firstLoad) { return; }
            calCulatePayment();
            setSummary();
           // btnClearNum.Visible = tbxPrice.Text.Length > 0 ? true : false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        //private void Modal_FinalDc_Load(object sender, EventArgs e)
        //{

        //    clearAllTextBox();
        //    tbSumTotal.Text = payment.totalAmount.ToString();
        //    tbTotalDiscountInline.Text = payment.totalDiscountInline.ToString();
        //    calCulatePayment();
        //    setSummary();
        //}


        //private void clearAllTextBox()
        //{
        //    tbTotalDiscountInline.Text = "";
        //    tbSumTotal.Text = "";
        //    tbxPrice.Text = "";
        //    tbTotalDiscountInline.Text = "";
        //    tbTotalDiscount.Text = "";





        //}














    }
}
