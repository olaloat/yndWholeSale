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
    public partial class Form_Search_Customer : Form
    {

        List<Customer> MyCustList = new List<Customer>();
        public int customerId { get; set; }

        public Customer MyCustSelected { get; set; }

        public bool isSelected { get; set; }
        option.mode activeMode = option. mode.SELECT;

        static bool isAddCustomerComplete = false;

      


        public Form_Search_Customer(option.mode _mode = option.mode.SELECT)
    
        {

            activeMode = _mode;
            InitializeComponent();

        

        }

        private void defultGrid()
        {
            dtgCustomer = UI.prepareGridCustomer(dtgCustomer, MyCustList , activeMode);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void clearData() {
            isSelected = false;
            MyCustSelected = new Customer();// = 0;

        }

        private void Form_Search_Customer_Load(object sender, EventArgs e)
        {
          
            clearData();
            Operation.loadCustomer();
            int qty = global.defultQty;
            defultGrid();
            loadCustomer();
        }
        ynd yndInven = new ynd();
        List<Customer> mstCustomer = new List<Customer>();
        BindingSource bsCustomer = new BindingSource();
        DataTable dtCustomer = new DataTable();
        DataView dvCustomer = new DataView();

   

        private void loadCustomer()
        {
            yndInven = new ynd();
            var customerSearchList = (from a in yndInven.Customers select a  ).ToList();
            bsCustomer = new BindingSource();
            dtCustomer = global.ToDataTable(customerSearchList);
            dvCustomer = new DataView(dtCustomer);
            bsCustomer.DataSource = dvCustomer;
            dtgCustomer.DataSource = bsCustomer;
            this.dtgCustomer.ReadOnly = true;
            this.dtgCustomer.AllowUserToAddRows = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Form_Add_Customer fb = new Form_Add_Customer())
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();
                if (fb.IsUpdateComplete)
                {
                    loadCustomer();
                    // setDefualtGrid();
                    //load product again


                }
            }
        }

        private void tbCustCode_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void tbCustName_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void tbAddress_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }


        private void filterData()
        {

            string filter = "";

            //if (tbCustCode.Text.ToString().Trim() != "")
            //{
            //    filter += "cast(customerId as varchar(100)) like '%" + tbCustCode.Text + "%' and";
            //}
            if (tbCustName.Text.ToString().Trim() != "")
            {   
                filter += "customerName like '%" + tbCustName.Text + "%' and";
            }
            if (tbAddress.Text.ToString().Trim() != "")
            {
                filter += "Address1+Address2 like '%" + tbAddress.Text + "%' and";
            }
         
            if (filter == "") { dvCustomer.RowFilter = null; }
            else
            {
                filter = filter.Substring(0, filter.Length - 3);
                dvCustomer.RowFilter = filter; } // " productCode like '%" + tbProdCode.Text + "%' or productName like '%" + tbProdName.Text +"%'" +" or category like '%" + cbCatagory.Text + "%'" +" or type like '%" + cbType.Text + "%'";
        }

   
        private void dtgCustomer_CellDoubleClick(object sender, DataGridViewCellEventHandler e)
        {
            if (dtgCustomer.CurrentCell.RowIndex >= 0)
            {

                customerInfo.clear();
                int row = dtgCustomer.CurrentCell.RowIndex;


                int colsCustomerName = dtCustomer.Columns["customerName"].Ordinal;
                int colsAddress1 = dtCustomer.Columns["address1"].Ordinal;
                int colsAddress2 = dtCustomer.Columns["address2"].Ordinal;
                int colsCity = dtCustomer.Columns["city"].Ordinal;
                int colsPostal = dtCustomer.Columns["postal"].Ordinal;

                string address = dtgCustomer.Rows[row].Cells[colsAddress1].Value.ToString() + " " + dtgCustomer.Rows[row].Cells[colsAddress2].Value.ToString() + " " +
                    dtgCustomer.Rows[row].Cells[colsCity].Value.ToString() + " " + dtgCustomer.Rows[row].Cells[colsCustomerName].Value.ToString();


                customerInfo.customerAddress = address;
                customerInfo.customerName = dtgCustomer.Rows[row].Cells[colsCustomerName].Value.ToString();
                customerInfo.isSelected = true;
                this.Dispose();

            }
        }



        private void dtgCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try {
            if (e.RowIndex >= 0 && e.ColumnIndex >=0)
            {

                string columnButonName = dtgCustomer.Columns[e.ColumnIndex].HeaderText.ToString().ToUpper();
                if (columnButonName.ToUpper() == "SELECT")
                {
                   
                    string strIndx = dtgCustomer.Rows[e.RowIndex].Cells["customerId"].Value.ToString();
                    int indx =int.Parse(strIndx);
                    //  int indx = (int)strIndx;
                    MyCustSelected = masterCustomer.getByID(indx);

                    

                    isSelected = true;
                    this.Dispose();
                    return;
                }


                if (columnButonName.ToUpper() == "EDIT")
                {

                    int custId = (int)this.dtgCustomer.Rows[e.RowIndex].Cells["customerID"].Value;



                    using (Form_Add_Customer fb = new Form_Add_Customer(option.mode.EDIT))
                    {
                     
                        fb.customerID = custId;
                        fb.StartPosition = FormStartPosition.CenterParent;
                        fb.ShowDialog();
                        if (fb.IsUpdateComplete)
                        {
                            loadCustomer();



                        }
                    }

                }







            }



            }
            catch (Exception err)
            {



                mMsgBox.show(err.Message.ToString(), Modal_MsgBox.icon.error, "ERROR");
            }


        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }
    }
}
