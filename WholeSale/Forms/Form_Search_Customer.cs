﻿using System;
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
    public partial class Form_Search_Customer : Form
    {
        public Form_Search_Customer()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_Search_Customer_Load(object sender, EventArgs e)
        {
            loadCustomer();


        }
        ynddevEntities yndInven = new ynddevEntities();
        List<Customer> mstCustomer = new List<Customer>();
        BindingSource bsCustomer = new BindingSource();
        DataTable dtCustomer = new DataTable();
        DataView dvCustomer = new DataView();

       public static bool isAddCustomerComplete = false;

        private void loadCustomer()
        {

        //    this.pnGrid.Controls.Add(grdProd);
         //   grdProd.Dock = System.Windows.Forms.DockStyle.Fill;
        
            var customerSearchList = (from a in yndInven.Customers select a  ).ToList();
            bsCustomer = new BindingSource();
            //this.dtg_prd = new DataGridView();

          //  bsCustomer.DataSource = customerSearchList;

          //  dtgCustomer.DataSource = bsCustomer;

         



            dtCustomer = Global.ToDataTable(customerSearchList);
            dvCustomer = new DataView(dtCustomer);
            bsCustomer.DataSource = dvCustomer;
            dtgCustomer.DataSource = bsCustomer;
            this.dtgCustomer.ReadOnly = true;
            this.dtgCustomer.AllowUserToAddRows = false;






            //dtPrd = Global.ToDataTable(productSearchList);
            //dvProd = new DataView(dtPrd);
            //grdProd.AutoRedraw = false;
            //grdProd.DataSource = dvProd;
            //grdProd = util.autoFit(grdProd);






            //grdProd = util.chnageGridColumnName(grdProd, dtPrd, "productCode", "รหัสสินค้า");
            //grdProd = util.chnageGridColumnName(grdProd, dtPrd, "productName", "ชื่อสินค้า");
            //grdProd = util.chnageGridColumnName(grdProd, dtPrd, "unit", "หน่วย");
            //grdProd = util.chnageGridColumnName(grdProd, dtPrd, "price", "ราคา/หน่วย");
            //grdProd = util.chnageGridColumnName(grdProd, dtPrd, "type", "ประเภท");
            //grdProd = util.chnageGridColumnName(grdProd, dtPrd, "category", "หมวดหมู่");
            //grdProd.DisplayRowNumber = true;


            //grdProd.AutoRedraw = true;
            //grdProd.Locked = true;







            //grdProd.Refresh();



            //grdProd.DoubleClick += new FlexCell.Grid.DoubleClickEventHandler(GrdList_DoubleClick);

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
                if (isAddCustomerComplete)
                {
                    loadCustomer();
                    isAddCustomerComplete = false;
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

            if (tbCustCode.Text.ToString().Trim() != "")
            {
                filter += "customerCode like '%" + tbCustCode.Text + "%' and";
            }
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

        //private void dtgCustomer_DoubleClick(object sender, DataGridViewCellMouseEventHandler e)

        //{
           

        //    if (e.RowIndex >= 0) {

        //        customerInfo.clear();


        //        int colsCustomerName = dtCustomer.Columns["customerName"].Ordinal ;
        //        int colsAddress1 = dtCustomer.Columns["address1"].Ordinal ;
        //        int colsAddress2 = dtCustomer.Columns["address2"].Ordinal;
        //        int colsCity = dtCustomer.Columns["city"].Ordinal;
        //        int colsPostal = dtCustomer.Columns["postal"].Ordinal;

        //        string address = dtgCustomer.Rows[e.RowIndex].Cells[colsAddress1].Value.ToString() + " " + dtgCustomer.Rows[e.RowIndex].Cells[colsAddress2].Value.ToString() + " " +
        //            dtgCustomer.Rows[e.RowIndex].Cells[colsCity].Value.ToString() + " " + dtgCustomer.Rows[e.RowIndex].Cells[colsCustomerName].Value.ToString();


        //        customerInfo.customerAddress = address;
        //        customerInfo.customerName = dtgCustomer.Rows[e.RowIndex].Cells[colsCustomerName].Value.ToString();
        //        customerInfo.isSelected = true;
        //        this.Dispose();

        //    }

           




        //}

   
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

        private void dtgCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dtgCustomer_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
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
    }
}
