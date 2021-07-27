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


  
    public partial class Modal_FinalDc : Form
    {
        decimal totalPriceBeforeDiscount     =0;
        decimal totalDiscountLine            =0;
        decimal totalDiscountEnd             =0;
        decimal totalPriceAfterDiscount      =0;
        decimal totalDiscountAll             = 0;
        
     public bool isOkClick { get; set; }
        public DocumentDisplay DocumentHeader
        {
            get; set;
        }

        public Modal_FinalDc(DocumentDisplay myDocumentHeader)
        {
            InitializeComponent();
            DocumentHeader = myDocumentHeader;
            clearTextBox();
            ClearData();
            setDefualData();
            setSummary();
            setDataToUI();

        }

        private void setDefualData() {
             totalPriceBeforeDiscount = DocumentHeader.totalPriceBeforeDiscount;
             totalDiscountLine = DocumentHeader.totalLineDiscount;
             totalDiscountEnd = 0;
             totalPriceAfterDiscount = DocumentHeader.totalPriceAfterAllDiscount ;
             totalDiscountAll = DocumentHeader.totalDiscount;

        }

        private void setSummary() {


            DocumentHeader.totalPriceBeforeDiscount = totalPriceBeforeDiscount;
            DocumentHeader.totalLineDiscount = totalDiscountLine;
            DocumentHeader.endDiscount = totalDiscountEnd;
            DocumentHeader.totalPriceAfterAllDiscount = totalPriceAfterDiscount;
            DocumentHeader.totalDiscount = totalDiscountAll;
        }

        private void setDataToUI() {
            // 
            tbTotalPriceBeforeDiscount.Text = DocumentHeader.totalPriceBeforeDiscount.ToString();
             tbTotalDiscountInline.Text = DocumentHeader.totalLineDiscount.ToString();
            tbEndDiscount.Text = DocumentHeader.endDiscount.ToString();
            tbTotalDiscountAfterDiscount.Text = DocumentHeader.totalPriceAfterAllDiscount.ToString();
            tbTotalDiscount.Text = DocumentHeader.totalDiscount.ToString();

        }

        private void ClearData() {
            totalPriceBeforeDiscount = 0;
            totalDiscountLine = 0;
            totalDiscountEnd = 0;
            totalPriceAfterDiscount = 0;
            totalDiscountAll = 0;
            isOkClick = false;
        }


        private void clearTextBox()
        {

            tbTotalPriceBeforeDiscount.Text = "0.00";
            tbTotalDiscountInline.Text = "0.00";
            tbEndDiscount.Text = "0.00";
            tbTotalDiscountAfterDiscount.Text = "0.00";
            tbTotalDiscount.Text = "0.00";

        }




        private mainResult validateDiscount() {
           
            mainResult rs = new mainResult();
            rs.isComplete = true;
            if (totalPriceAfterDiscount < 0)
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

                mMsgBox.show(rs.message);
               
             
            }
            else
            {
                isOkClick = true;
                this.Dispose();


            }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {

            if (tbEndDiscount.Text.Trim().Length == 0)
            {


                tbEndDiscount.Text = "0.";

            }

            if (tbEndDiscount.Text.Contains("."))
            {




            }
            else
            {

                tbEndDiscount.Text += ".";
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (tbEndDiscount.Text.Trim().Length == 0)
            {




            }
            else
            {
                tbEndDiscount.Text += "0";


            }
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            if (tbEndDiscount.Text.Trim().Length == 0)
            {




            }
            else
            {
                tbEndDiscount.Text += "00";


            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "7";
          
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text += "9";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (tbEndDiscount.Text.Length > 0) { tbEndDiscount.Text = tbEndDiscount.Text.Substring(0, tbEndDiscount.Text.Length - 1); }
        }

        private void btnClearNum_Click(object sender, EventArgs e)
        {
            tbEndDiscount.Text = "";
            //calCulatePayment();
            //setSummary();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            isOkClick = false;
            this.Dispose();
        }

        private void tbDiscountBill_TextChanged(object sender, EventArgs e)
        {
            calCulatePayment();
            setSummary();
            setDataToUI();
            btnClearNum.Visible = tbEndDiscount.Text.Length > 0 ? true : false;
        }

 



        private void calCulatePayment()
        {
            decimal dis = 0;
            if (tbEndDiscount.Text == "") { dis = 0; } else { dis = System.Convert.ToDecimal(tbEndDiscount.Text); }
         
            totalDiscountEnd = dis;
            totalDiscountAll = totalDiscountLine + totalDiscountEnd;
            totalPriceAfterDiscount = totalPriceBeforeDiscount - totalDiscountAll;
          
       






        }

        //private void Modal_FinalDc_Load(object sender, EventArgs e)
        //{

        //    //clearAllTextBox();
        //    tbSumTotal.Text = payment.totalPriceAfterDiscount.ToString();
        //    tbDiscountBill.Text = payment.dicountBill.ToString();
        //    tbTotalDiscountInline.Text = payment.totalDiscountInline.ToString();
        //    calCulatePayment();
        //    setSummary();
        //}


        private void clearAllTextBox() {
            tbTotalDiscountInline.Text = "";
            tbTotalPriceBeforeDiscount.Text = "";
            tbEndDiscount.Text = "";
            tbTotalDiscountInline.Text = "";
            tbTotalDiscount.Text = "";

                
        
        
        
        }
    }
}
