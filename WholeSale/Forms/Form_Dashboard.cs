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
    public partial class Form_Dashboard : Form
    {
        public Form_Dashboard()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FrmPOS fb = new FrmPOS())
            {
                fb.ShowDialog();
            }
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


        private void button2_Click(object sender, EventArgs e)
        {
            using (Form_Add_Product fb = new Form_Add_Product())
            {
                fb.ShowDialog();
            }
        }

        //private void btnProduct_Click(object sender, EventArgs e)
        //{
        //    masterProduct.getdataMaster();
        //    using (Form_Search_Product fb = new Form_Search_Product(masterProduct.List))
        //    {
        //        //productMaintain.clear();
        //        Operation.loadProduct();
        //        Operation.loadCustomer();

        //        fb.StartPosition = FormStartPosition.CenterParent;
        //        fb.ShowDialog();

        //        //productMaintain.loadMaster();
        //    }
        //}

        //private void btCustomer_Click(object sender, EventArgs e)
        //{
        //    using (Form_Search_Product fb = new Form_Search_Product(global.mstProduct, true))
        //    {
        //        productMaintain.clear();
        //        fb.StartPosition = FormStartPosition.CenterParent;
        //        fb.ShowDialog();


        //    }

        //}

        private void btnCust_Click(object sender, EventArgs e)
        {
            using (Form_Search_Customer fb = new Form_Search_Customer(option.mode.EDIT))
            {
                Operation.loadCustomer();
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

            }
        }

        private void btnProduct_Click_1(object sender, EventArgs e)
        {
           // masterProduct.getdataMaster();
            using (Form_Search_Product fb = new Form_Search_Product(Form_Search_Product.mode.edit))
            {
                //productMaintain.clear();
                Operation.loadProduct();
              

                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

                //productMaintain.loadMaster();
            }
        }
    }
}
