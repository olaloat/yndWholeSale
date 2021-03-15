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
    public partial class Form_Daily_Report : Form
    {
        public Form_Daily_Report()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_Daily_Report_Load(object sender, EventArgs e)
        {
            ynddevEntities ynd = new ynddevEntities();
            var employee = ynd.Employees.Where(w => w.isActive == true).Select(s => new { empId = s.empId, fName = s.fName}).ToList();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt = Global.ToDataTable(employee);
            dt.Rows.Add(new Object[] { 0, "ทั้งหมด" });
            ds.Tables.Add(dt);
            foreach (var a in employee)
            {
                cboEmp.DataSource = ds.Tables[0];
                cboEmp.ValueMember = "empId";
                cboEmp.DisplayMember = "fName";
            }

            cboEmp.Text = "ทั้งหมด";
            cboStatus.Text = "ขายสำเร็จ";
        }
    }
}
