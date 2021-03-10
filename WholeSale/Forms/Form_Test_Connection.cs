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
    public partial class Form_Test_Connection : Form
    {
        public Form_Test_Connection()
        {
            InitializeComponent();
        }

        private void Form_Test_Connection_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Hello, Conection");
        }
    }
}
