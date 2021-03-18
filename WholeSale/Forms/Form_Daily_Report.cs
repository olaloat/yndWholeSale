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
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy";
            GetEmp();
            Reset();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ynddevEntities ynd = new ynddevEntities();
            //var document = ynd.Documents.Where(w => w.compCode == "yndk" && w.branchCode == "0001" && w.createTime.CompareTo(dateTimePicker1.Value) >= 0 && w.createTime.CompareTo(dateTimePicker2.Value.AddDays(1)) <= 0 && w.isActive == false ).Select(s => new { doc = s.documentNo, customer = s.customerId, vat = s.totalVat, dc = s.totalDc, qty = s.totalQty, price = s.totalPrice, paid = s.paidType, status = s.status, by = s.createBy, date = s.createTime });
            DateTime dateStart = dateTimePicker1.Value;
            DateTime dateEnd = dateTimePicker2.Value;
            string compCode = "yndk";
            string branchCode = "0001";
            //var document = ynd.Documents.Where(w => w.compCode == "yndk" && w.branchCode == "0001" && w.createTime >= dateStart && w.createTime < dateEnd.AddDays(1)).ToList();// and w.
            //document = document.Select(s => new { doc = s.documentNo, customer = s.customerId, vat = s.totalVat, dc = s.totalDc, qty = s.totalQty, price = s.totalPrice, paid = s.paidType, status = s.status, by = s.createBy, date = s.createTime });
            //document = document.Select(s => new { doc = s.documentNo, customer = s.customerId}).ToList();
            //dataGridView1.DataSource = document;
            var document = ynd.spGetDocument(dateStart, dateEnd.AddDays(1), compCode, branchCode).ToList();
            var document2 = document.Where(i => i.isActive == false).ToList();
            if (cboEmp.Text != "ทั้งหมด")
            {
                 document2 = document.Where(i => i.createBy == cboEmp.Text).ToList();
            }

            switch (cboStatus.Text)
            {
                case "สำเร็จ":
                    document2 = document.Where(i => i.status == "สำเร็จ").ToList();
                    break;
                case "พักบิล":
                    document2 = document.Where(i => i.status == "พักบิล").ToList();
                    break;
                default:

                    break;
            }

        
            //document2 = document2.Where(i => i.status == 1).ToList();
            dataGridView1.DataSource = document2;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void GetEmp()
        {
            ynddevEntities ynd = new ynddevEntities();
            var employee = ynd.Employees.Where(w => w.isActive == true).Select(s => new { empId = s.empId, fName = s.fName }).ToList();
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
        }

        private void Reset()
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            cboEmp.Text = "ทั้งหมด";
            cboStatus.Text = "สำเร็จ";
            //MessageBox.Show(DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
}
