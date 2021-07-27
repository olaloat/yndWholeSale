using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WholeSale.Forms;

namespace WholeSale
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmTestPrint());
            //  Application.Run(new Form_POS());
        // Application.Run(new Form_POS());
            Application.Run(new Form1());
        }
    }
}
