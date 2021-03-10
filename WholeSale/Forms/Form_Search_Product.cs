using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WholeSale.Model;


namespace WholeSale.Forms
{
    public partial class Form_Search_Product : Form
    {

        List<Product> masterProduct = new List<Product>();
        FlexCell.Grid grdProd = new FlexCell.Grid();

        static bool isAddProuctComplete = false;
        public static bool atusAddNewProduct
        {
            get
            {
                return isAddProuctComplete;
            }
            set
            {
                isAddProuctComplete = value;
            }
        }
        public Form_Search_Product(List<Product>  _masterProduct)
        {


     
            InitializeComponent();
            masterProduct = _masterProduct;
       

            //InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            using (Form_Add_Product fb = new Form_Add_Product())
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();
                if (isAddProuctComplete)
                {
                    loadProduct();
                    //load product again


                }
            }
        }

        ynddevEntities yndInven = new ynddevEntities();
        List<productInfo> mstProduct = new List<productInfo>();
        DataView dvProd = new DataView();

        //private void grid_doubleClick((object sender, EventArgs e) { 




        //}



        private void loadProduct() {

            this.pnGrid.Controls.Add(grdProd);
            grdProd.Dock = System.Windows.Forms.DockStyle.Fill;
            //  grdProd.Click += new EventHandler(grid_doubleClick);

            var productSearchList = (from a in yndInven.Products
                                     join b in yndInven.Categories on a.categoryId equals b.categoryId
                                     join c in yndInven.ProductTypes on a.productTypeId equals c.productTypeId
                                     join d in yndInven.Units
on a.unitId equals d.unitId
                                     select new
                                     {
                                         productCode = a.productCode,
                                         productName = a.productName,
                                         price = a.price,
                                         unit = d.unitName,
                                         type = c.productTypeName,
                                         category = b.categoryName
                                     }).ToList();// yndInven.Products.ToList();
                                                 //  mstProduct = new List<Product>();
                                                 //  this.dataGridView2 = new DataGridView();
            productBindingSource = new BindingSource();
            //this.dtg_prd = new DataGridView();

            productBindingSource.DataSource = productSearchList;

            dtg_prd.DataSource = productBindingSource;

            DataTable dtPrd = new DataTable();
            dtPrd = Global.ToDataTable(productSearchList);
            dvProd = new DataView(dtPrd);
            grdProd.AutoRedraw = false;
            grdProd.DataSource = dvProd;
            grdProd = util.autoFit(grdProd);






            grdProd = util.chnageGridColumnName(grdProd, dtPrd, "productCode", "รหัสสินค้า");
            grdProd = util.chnageGridColumnName(grdProd, dtPrd, "productName", "ชื่อสินค้า");
            grdProd = util.chnageGridColumnName(grdProd, dtPrd, "unit", "หน่วย");
            grdProd = util.chnageGridColumnName(grdProd, dtPrd, "price", "ราคา/หน่วย");
            grdProd = util.chnageGridColumnName(grdProd, dtPrd, "type", "ประเภท");
            grdProd = util.chnageGridColumnName(grdProd, dtPrd, "category", "หมวดหมู่");
            grdProd.DisplayRowNumber = true;


            grdProd.AutoRedraw = true;
            grdProd.Locked = true;







            grdProd.Refresh();
            // this.dataGridView2.Columns["ProductID"].Visible = false;


            grdProd.DoubleClick += new FlexCell.Grid.DoubleClickEventHandler(GrdList_DoubleClick);

        }
        private void Form_Search_Product_Load(object sender, EventArgs e)
        {
            loadProduct();




        }



        string flterPrdCode = "";
        string filterPrdName = "";
        string filterType = "";
        string filterCategory = "";
     


        private void filterData() {

            string filter = "";

            if (tbProdCode.Text.ToString().Trim() !="") {
                filter += " productCode like '%" + tbProdCode.Text + "%' and";
            }
            if (tbProdName.Text.ToString().Trim() != "")
            {
                filter += " productName like '%" + tbProdName.Text + "%' and";
            }
            if (cbCatagory.Text.ToString().Trim() != "")
            {
                filter += " category like '%" + cbCatagory.Text + "%' and";
            }
            if (cbType.Text.ToString().Trim() != "")
            {
                filter += " type like '%" + cbType.Text + "%' and";
            }
            if (filter.Length > 0) { filter = filter.Substring(0, filter.Length - 3); }
            dvProd.RowFilter = filter;// " productCode like '%" + tbProdCode.Text + "%' or productName like '%" + tbProdName.Text +"%'" +" or category like '%" + cbCatagory.Text + "%'" +" or type like '%" + cbType.Text + "%'";
       }

        private void tbProdCode_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void tbProdName_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void cbCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void GrdList_DoubleClick(object Sender, EventArgs e)
        {

            if (grdProd.ActiveCell.Row > 0) {

                Form_POS.selectedProductCode = grdProd.Cell(grdProd.ActiveCell.Row,1).Text.ToString();

                this.Dispose();

            }
            
        }
    }
}
