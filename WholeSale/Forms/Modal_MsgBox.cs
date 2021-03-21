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
    public partial class Modal_MsgBox : Form
    {

        public resultOption selectResult;
        public enum resultOption { ok, cancel, yes, no }

        private optionType type ;
        public Modal_MsgBox(string data, optionType msgBoxType = optionType.ok, string button1 = "", string button2 = "", string button3 = "")
        {
            InitializeComponent();
            type = msgBoxType;
            switch (msgBoxType) {
                case optionType.ok:
                    //type = "ok";
                    pnButton.Visible = false;
                    break;
                case optionType.okCancel:  // ok cancel
                    //type = "okCancel";
                    pnButton.Visible = true;

                    btOption1.Visible = false;
                    btOption2.Visible = true;
                    btOption3.Visible = true;

                    button2Text = "Cancel";
                    button3Text = "OK";
                    break;
                case optionType.yseNoOk: // yes no 
                    //type = "yesNo";
                    pnButton.Visible = true;

                    btOption1.Visible = false;
                    btOption2.Visible = true;
                    btOption3.Visible = true;

                    btOption2.Text = "YES";
                    btOption3.Text = "NO";

                    break;
                case optionType.holding: // custom
                    //type = "holding";

                    pnButton.Visible = true;

                   btOption1.Visible = true;
                    btOption1.Text = "เปิดรายการที่พักไว้"; 
                   btOption2.Visible = true;
                    btOption2.Text = "พักรายการนี้";
                    btOption3.Visible = false;
                    btOption2.Enabled = Bill.isHasList;

                   // btOption1.Text = "";



                    break;
                default:

                    break;

            }

            tbMessage.Text = data;
        }

        private string button1Text = "";
        private string button2Text = "";
        private string button3Text = "";

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void btOption1_Click(object sender, EventArgs e)
        {
            switch (type) {

                case optionType.holding:
                    openPageHoldBill();
                    if (Bill.docHeaderID != 0) { this.Dispose(); }

                    break; }
        }

        private void btOption2_Click(object sender, EventArgs e)
        {
            switch (type)
            {

                case optionType.holding:
                   mainResult rs = Bill.holdingBill();
                    tbMessage.Text = rs.message;
                    diableAllbutton();
                    if (rs.isComplete)
                    {
                        tbMessage.ForeColor = Color.Green;
                      
                    }
                    else {

                        tbMessage.ForeColor = Color.Red;
                    }
                    break;
                case optionType.yseNoOk:
                    selectResult = resultOption.yes;
                    this.Dispose();
                    break;
            }

        }

        private void diableAllbutton() {
            btOption1.Enabled = false;
            btOption2.Enabled = false;
            btOption3.Enabled = false;

        }

        private void btOption3_Click(object sender, EventArgs e)
        {
            switch (type)
            {

                case optionType.holding:
                  
                    break;
                case optionType.yseNoOk:
                    selectResult = resultOption.no;
                    this.Dispose();
                    break;
            }
        }


        private void openPageHoldBill() {

            using (Form_Pending fb = new Form_Pending())
            {
                fb.ShowDialog();
              
            }


        }

     
    }

    public enum optionType { ok, okCancel, yseNoOk,holding   }

   




}


