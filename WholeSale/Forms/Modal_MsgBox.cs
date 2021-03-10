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
        public Modal_MsgBox(string data)
        {
            InitializeComponent();
            tbMessage.Text = data;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
