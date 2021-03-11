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
namespace WholeSale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                fb.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ynddevEntities ynd = new ynddevEntities();
            //var userList = (from a in ynd.Employees where a.fName == "satit" select new Employee() { fName = a.fName, lName = a.lName }).ToList();
            //var userList2 = ynd.Employees.Where(w => w.fName == "satit").Select(s => new Employee() { fName = s.fName, lName = s.lName }).ToList();
            //List<Employee> employee = new List<Employee>();
            var employee = ynd.Employees.Select(s => new { fName = s.fName, lName = s.lName}).ToList();
             
            //var test = ynd.Employees.Find(1);

            //for (int i = 0; i < employee.Count; i++)
            //{
            //    Button btn = new Button();
            //    //btn.Text =  "<" + i.ToString().PadLeft(3, '0') + ">";
            //    btn.Text = employee.
            //    btn.BackColor = Color.White;
            //    btn.FlatStyle = FlatStyle.Flat;
            //    btn.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            //    btn.ForeColor = Color.FromArgb(41, 121, 255);
            //    btn.TextAlign = ContentAlignment.MiddleCenter;
            //    btn.UseVisualStyleBackColor = true;
            //    btn.TabIndex = i;
            //    btn.Size = new System.Drawing.Size(126, 66);
            //    btn.Name = "btnLoginName" + i.ToString();
            //    flowLayoutPanel1.Controls.Add(btn);
            //}

            foreach(var a in employee)
            {
                Button btn = new Button();
                //btn.Text =  "<" + i.ToString().PadLeft(3, '0') + ">";
                btn.Text = a.fName;
                btn.BackColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                btn.ForeColor = Color.FromArgb(41, 121, 255);
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.UseVisualStyleBackColor = true;
                //btn.TabIndex = ;
                btn.Size = new System.Drawing.Size(126, 66);
                btn.Name = a.fName;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
    }
}
