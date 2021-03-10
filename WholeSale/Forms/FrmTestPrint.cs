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
    public partial class FrmTestPrint : Form
    {
        public FrmTestPrint()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FrmTestPrint_Load(object sender, EventArgs e)
        {
            using (ynddevEntities ynd = new ynddevEntities())
            {
                productBindingSource.DataSource = (from a in ynd.Products select a ).ToList() ;

            
            
            
            
            }
        }
    }
}
