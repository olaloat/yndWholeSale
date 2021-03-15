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
    public partial class Form_Pending : Form
    {


        ynddevEntities yndInven = new ynddevEntities();
        BindingSource bsHoldingList = new BindingSource();
        DataTable dtHodling = new DataTable();
        DataView dvHodling = new DataView();
        public Form_Pending()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_Pending_Load(object sender, EventArgs e)
        {
            loadHoldingBill();
        }


        private void loadHoldingBill()
        {
            var customerSearchList = (from a in yndInven.Documents where  a.status ==1 select a).ToList();
            bsHoldingList = new BindingSource();
            dtHodling = Global.ToDataTable(customerSearchList);
            dvHodling = new DataView(dtHodling);
            bsHoldingList.DataSource = dvHodling;
            dtgHoldingList.DataSource = bsHoldingList;
            this.dtgHoldingList.ReadOnly = true;
            this.dtgHoldingList.AllowUserToAddRows = false;
        }


        //private void dtgHoldingList_CellDoubleClick(object sender, DataGridViewCellEventHandler e)
        //{
        //    if (dtgHoldingList.CurrentCell.RowIndex >= 0)
        //    {

        //        customerInfo.clear();
        //        int row = dtgHoldingList.CurrentCell.RowIndex;


        //        int colHeaderID = dtHodling.Columns["documentId"].Ordinal;
        //        int colDocNum = dtHodling.Columns["documentNo"].Ordinal;
        //        //int colsAddress1 = dtHodling.Columns["address1"].Ordinal;
        //        //int colsAddress2 = dtHodling.Columns["address2"].Ordinal;
        //        //int colsCity = dtHodling.Columns["city"].Ordinal;
        //        //int colsPostal = dtHodling.Columns["postal"].Ordinal;

        //        //string address = dtgHoldingList.Rows[row].Cells[colsAddress1].Value.ToString() + " " + dtgHoldingList.Rows[row].Cells[colsAddress2].Value.ToString() + " " +
        //        //    dtgHoldingList.Rows[row].Cells[colsCity].Value.ToString() + " " + dtgHoldingList.Rows[row].Cells[colsCustomerName].Value.ToString();

        //        Bill.docHeaderID =int.Parse( dtgHoldingList.Rows[row].Cells[colHeaderID].Value.ToString());

        //        Bill.documentNumber = dtgHoldingList.Rows[row].Cells[colDocNum].Value.ToString();
        //        //customerInfo.customerAddress = address;
        //        //customerInfo.customerName = dtgHoldingList.Rows[row].Cells[colsCustomerName].Value.ToString();
        //        //customerInfo.isSelected = true;
        //        this.Dispose();

        //    }
        //}

        private void dtgHoldingList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgHoldingList.CurrentCell.RowIndex >= 0)
            {

                customerInfo.clear();
                int row = dtgHoldingList.CurrentCell.RowIndex;


                int colHeaderID = dtHodling.Columns["documentId"].Ordinal;
                int colDocNum = dtHodling.Columns["documentNo"].Ordinal;
                //int colsAddress1 = dtHodling.Columns["address1"].Ordinal;
                //int colsAddress2 = dtHodling.Columns["address2"].Ordinal;
                //int colsCity = dtHodling.Columns["city"].Ordinal;
                //int colsPostal = dtHodling.Columns["postal"].Ordinal;

                //string address = dtgHoldingList.Rows[row].Cells[colsAddress1].Value.ToString() + " " + dtgHoldingList.Rows[row].Cells[colsAddress2].Value.ToString() + " " +
                //    dtgHoldingList.Rows[row].Cells[colsCity].Value.ToString() + " " + dtgHoldingList.Rows[row].Cells[colsCustomerName].Value.ToString();

                Bill.docHeaderID = int.Parse(dtgHoldingList.Rows[row].Cells[colHeaderID].Value.ToString());
                Bill.isFromHolding = true;
                Bill.documentNumber = dtgHoldingList.Rows[row].Cells[colDocNum].Value.ToString();
                //customerInfo.customerAddress = address;
                //customerInfo.customerName = dtgHoldingList.Rows[row].Cells[colsCustomerName].Value.ToString();
                //customerInfo.isSelected = true;
                this.Dispose();

            }
        }

        private void dtgHoldingList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        //private void dtgHoldingList_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
    }
}
