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

 
        public enum MessageBoxButtons
        {
            OK,
            OKCancel,
            RetryCancel,
            YesNo,
            YesNoCancel
        }

        public enum icon
        {
            information,
            error,
            warning
        }

        public MessageBoxButtons buttonType;

        public DialogResult result { get; set; } = DialogResult.Cancel;
        //public DialogResult   show(string msg ,string title= "Informarion", icon icon=icon.information)
        //{ InitializeComponent();
        //    this.lbHeader.Text = title.ToString();
        //    this.tbMessage.Text = msg.ToString();
        //    DialogResult result = new DialogResult();
        //    setIcon(icon);
        //    return result;

        //}
        public DialogResult show(string msg, string title="Informarion" ,icon icon= icon.information)
        { InitializeComponent();

            this.tbMessage.Text = msg.ToString();
            DialogResult result = new DialogResult();
            setIcon(icon);
            return result;
        }

        public DialogResult show(string msg, string title ="Information" , MessageBoxButtons button = MessageBoxButtons.OK, icon icon = icon.information)
        {
            InitializeComponent();
            showButton(button);
            this.lbHeader.Text = title;
            this.tbMessage.Text = msg.ToString();
            setIcon(icon);

            DialogResult result = new DialogResult();
            return result;


        }

        private void setIcon(icon type) {

            switch (type)
            {
                case icon.information:
                   // lbHeader.Text = "Information";
                    lbHeader.ForeColor = Color.Blue;
                    pnClose.BackColor = Color.Blue;
                    pcbIcon.Image = Properties.Resources.Information;
                    break;

                case icon.error:
                    //lbHeader.Text = "Error";
                    pnClose.BackColor = Color.Red;
                    lbHeader.ForeColor = Color.Red;
                    pcbIcon.Image = Properties.Resources.error;
                    break;


                case icon.warning:
                  //  lbHeader.Text = "Warning";
                    pnClose.BackColor = Color.Gold;
                    lbHeader.ForeColor = Color.Black;
                    pcbIcon.Image = Properties.Resources.warning;
                    break;

            }

        }

        public void hideButton() {

            tblOk.Visible = false;
            tblYesNo.Visible = false;
            tblYesNoCancel.Visible = false;
        }

        public void showButton(MessageBoxButtons buttonType) {

            
            switch (buttonType)
            {
                case MessageBoxButtons.OK:
                  
                    tblOk.Visible = true;
                    tblOk.Dock = DockStyle.Bottom;
                    break;

                case MessageBoxButtons.YesNo:
                    tblYesNo.Visible = true;
                    tblYesNo.Dock = DockStyle.Bottom;
                    break;


                case MessageBoxButtons.YesNoCancel:
                    tblYesNoCancel.Visible = true;
                    tblYesNoCancel.Dock = DockStyle.Bottom;
                    break;

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            this.Dispose();
        }

        private void lbHeader_Click(object sender, EventArgs e)
        {

        }

        private void btnYesNoCancel_yes_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            this.Dispose();
        }

        private void btnYesNoCancel_No_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            this.Dispose();
        }

        private void btnYesNoCancel_cancel_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            this.Dispose();
        }

        private void btnYesNo_yes_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            this.Dispose();

        }

        private void btnYesNo_No_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            this.Dispose();
        }
    }





}


