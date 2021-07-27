using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WholeSale.Forms;
using System.Data.Entity;
using WholeSale.MyClass;
namespace WholeSale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Graphics dc = e.Graphics;
        //    dc.DrawImageUnscaled(Background, 0, 0);
        //    base.OnPaint(e);
        //}

        Bitmap Background, Backgroundtemp;

        private void initialize() {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
           // Backgroundtemp = new Bitmap(Properties.Resources.non);
            Background = new Bitmap(Backgroundtemp, Background.Width, Backgroundtemp.Height);

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkHide_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHide.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (Form_Dashboard fb = new Form_Dashboard())
            {


                ynd ynd = new ynd();
                global.compCode = "001";
                global.plantCode = "001";
                global.BranchCode = "001";
                global.username = "oat";

                fb.ShowDialog();
            }
        }
    }
}
