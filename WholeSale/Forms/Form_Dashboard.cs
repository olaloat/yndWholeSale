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
            using (Form_POS fb = new Form_POS())
            {
                fb.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Form_Add_Product fb = new Form_Add_Product())
            {
                fb.ShowDialog();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            using (Form_Search_Product fb = new Form_Search_Product(Global.mstProduct))
            {
                productMaintain.clear();
               
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

                productMaintain.loadMaster();
            }
        }
    }
}
