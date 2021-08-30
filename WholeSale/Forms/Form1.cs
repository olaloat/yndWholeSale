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
        List<Employee> myEmployee = new List<Employee>();
        string username = "";

        public Form1()
        {
            InitializeComponent();
            StartTimer();
            myEmployee = db.getUserName();
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

        #region "UI"
        private void generateButonEmployee()
        {

         

            foreach (var a in myEmployee)
            {
                RadioButton rdEmp = createNewRadioButton(a.username);
                flowLayoutPanel1.Controls.Add(rdEmp);

            }

          

         

      

        }


        private RadioButton createNewRadioButton( string name) {

            RadioButton rdb1 = new RadioButton();
            rdb1.Appearance = System.Windows.Forms.Appearance.Button;
         rdb1.BackColor = System.Drawing.Color.White;
         rdb1.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkOrange;
         rdb1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         rdb1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         rdb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(121)))), ((int)(((byte)(255)))));
         rdb1.Location = new System.Drawing.Point(3, 3);
         rdb1.Name = "radioButton1";
         rdb1.Size = new System.Drawing.Size(126, 66);
         rdb1.TabIndex = 0;
         rdb1.TabStop = true;
            rdb1.Text = name;
         rdb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         rdb1.UseVisualStyleBackColor = false;
            // 

            rdb1.CheckedChanged += new EventHandler(selectUser);
            return rdb1;
        }


        private Button createNewNumpadButton(string num) {

            Button btnNum = new Button();
           btnNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           btnNum.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           btnNum.Location = new System.Drawing.Point(3, 3);
           btnNum.Name = "btnNum" + num;
            btnNum.Size = new System.Drawing.Size(90, 65);
           btnNum.TabIndex = 4;
           btnNum.Text = num;
            btnNum.UseVisualStyleBackColor = true;
            btnNum.Click += new EventHandler(clickNumPass);

            return btnNum;
        }

        private Button createBnBackSpace()
        {
            Button btn = new Button();
           btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           btn.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         
           btn.Name = "button12";
           btn.Size = new System.Drawing.Size(188, 65);
           btn.TabIndex = 6;
           btn.Text = "Clear";
            btn.UseVisualStyleBackColor = true;
            btn.Click += new EventHandler(clickClearPass);

            return btn;
        }

        #endregion


        private void  selectUser(object sender, EventArgs e) {
            RadioButton rdb = (RadioButton)sender;
             username = rdb.Text;
        }

        private void clickNumPass(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            txtPassword.Text += btn.Text.ToString();        }


        private void clickClearPass(object sender, EventArgs e)
        {


            txtPassword.Text = ""; ;
        }



        System.Windows.Forms.Timer tmr = null;
        private void StartTimer()
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(timer1_Tick);
            tmr.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            generateButonEmployee();
            createNumPad();



        }

        private void button11_Click(object sender, EventArgs e)
        {

            mainResult rs = login();
            if (rs.isComplete) {

                using (Form_Dashboard fb = new Form_Dashboard())
                {


                    ynd ynd = new ynd();
                    global.compCode = "001";
                    global.plantCode = "001";
                    global.BranchCode = "001";
                    global.username = username;

                    fb.ShowDialog();
                }



                
            } else {
                mMsgBox.show("รหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบ", Modal_MsgBox.icon.error, "Error");

                txtPassword.Text = "";

            }


        }

        private void createNumPad() {
            for (var i =1 ; i <=10; i++){
                Button btnNum = new Button();
                 btnNum = createNewNumpadButton(i.ToString());
                flowLayoutPnNumPad.Controls.Add(btnNum);
            }

            Button btnBkSpace = createBnBackSpace();
            flowLayoutPnNumPad.Controls.Add(btnBkSpace);
        }


        private void numClick() {


        }


        private mainResult login() {
            mainResult res = new mainResult();
        var filterUserPass =    myEmployee.Where(w => w.passcode == txtPassword.Text.ToString() && w.username ==username ).ToList();
            if (filterUserPass.Count > 0) {

                res.isComplete = true;
                res.message = "login succesfully";
                txtPassword.Text = "";

            }
            else {

               
                res.isComplete = false;
                res.message = "Passcode ไม่ถูกต้องกรุณา ตรวจสอบ";
            }

            

            return res;

        }

    }
}
