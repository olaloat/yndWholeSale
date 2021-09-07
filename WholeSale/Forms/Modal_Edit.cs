using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WholeSale.MyClass;

namespace WholeSale.Forms
{
    public partial class Modal_Edit : Form
    {
       
        public List<DocumentLineDisplay> listEdited {
            get; set;
        }

        private DocumentLineDisplay docLineEdit = new DocumentLineDisplay();

        private int productIDSelect { get; set; }

        public Modal_Edit(List<DocumentLineDisplay>  list , int productSelect)
        {
           
            InitializeComponent();

            listEdited = list;
            productIDSelect = productSelect;
            docLineEdit = list.Where(w => w.productId == productIDSelect).FirstOrDefault();
            tbxProduct.Text = docLineEdit.productName.ToString();
            tbxUnit.Text = docLineEdit.unit.ToString();
            tbxPrice.Text = (docLineEdit.unitPrice - docLineEdit.discountUnit).ToString();
            tbxQty.Text = docLineEdit.qty.ToString();
            tbxTotalPrice.Text = docLineEdit.totalPriceAfterDiscount.ToString();
            tbxUnit.Focus();


        }

        private void button17_Click(object sender, EventArgs e)
        {
           
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

                tbxPrice.Text = "";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tbxPrice_TextChanged(object sender, EventArgs e)
        {
            summary();

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

            summary();
        }

       private void summary()
        {

            //  var dddd= tbxQty.ToString() != "" ? tbxQty.ToString() : 0;
            var summary = (tbxQty.Text.ToString() == "" ? 0 : Convert.ToDecimal(tbxQty.Text.ToString())) * (tbxPrice.Text.ToString() == "" ? 0 : Convert.ToDecimal(tbxPrice.Text.ToString()));
                //* decimal.Parse(tbxPrice.Text.ToString());
            tbxTotalPrice.Text = summary.ToString();

        }

        private mainResult checkCompleteValue() {
            mainResult rs = new mainResult() { isComplete = false, message = "", status = "" };
            if (tbxQty.Text.ToString().Trim().Length != 0 && tbxPrice.Text.ToString().Trim().Length != 0) {
                 rs = new mainResult() { isComplete = true, message = "OK", status = "OK" };
            } else {
                 rs = new mainResult() { isComplete = false, message = "ยังไม่ได้ใส่จำนวน QTY หรือ Price กรุณาตรวจสอบ", status = "ERROR" };
            }

            return rs;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            var rs = checkCompleteValue();
            if (!rs.isComplete) {

                mMsgBox.show(rs.message);
                return; }
            mainResult myResultCheck = CheckMaxMinPrice(decimal.Parse(tbxPrice.Text), productIDSelect);

            if (!myResultCheck.isComplete)
            {
                mMsgBox.show(myResultCheck.message , Modal_MsgBox.icon.error , "ERROR");
                //using (Modal_MsgBox msg = new Modal_MsgBox(myResultCheck.message))
                //{
                //    msg.StartPosition = FormStartPosition.CenterParent;
                //    msg.ShowDialog();

                //}

            }
            else
            {
                foreach (DocumentLineDisplay a in listEdited) {
                    if (a.productId == productIDSelect) {
                        decimal discountUnit = a.unitPrice - decimal.Parse(tbxPrice.Text.ToString());
                        a.qty = int.Parse(tbxQty.Text.ToString());
                        a.discountUnit = discountUnit;
                        a.totalPriceBeforeDiscount = a.qty * a.unitPrice;
                        a.totalDiscount = a.qty * discountUnit;
                        a.totalPriceAfterDiscount = a.totalPriceBeforeDiscount - a.totalDiscount;
                    }

                }
                this.Dispose();
            }



        }


        private mainResult CheckMaxMinPrice(decimal price , int productId) {
            mainResult rs = new mainResult();

            
            List<Product> myFilterProduct = masterProduct.List.Where(w => w.productId == productId).ToList();


            if (myFilterProduct.Where(w  => w.productId == productId  && w.maxPrice>=price && w.minPrice <=price).ToList().Count  >0 ) {
                rs.isComplete = true;
                rs.message = "OK";

                return rs;

            }
            if (myFilterProduct.Where(w => w.productId == productId & (w.maxPrice < price || w.minPrice > price)).ToList().Count > 0)
            {


                rs.isComplete = false;
                rs.message = "ราคาที่แก้ไขต้อง ไม่น้อยกว่า " + myFilterProduct.Select(s => s.minPrice).FirstOrDefault() + " และ ไม่มากกว่า " + myFilterProduct.Select(s => s.maxPrice).FirstOrDefault();
           
            }
            else {
                rs.isComplete = true;
                rs.message = "OK";


            }

            return rs;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //myEdit = new DocumentLine();
            //myEdit = myPrdLoad;
            this.Dispose();
        }
















    }
}
