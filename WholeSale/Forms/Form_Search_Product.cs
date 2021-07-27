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
using WholeSale.MyClass;



namespace WholeSale.Forms
{
    public partial class Form_Search_Product : Form
    {

        public string productCode { get; set; }
        public bool isSelected { get; set; }

        //bool posMode = false;

        static bool isAddProuctComplete = false;
        mode activeMode = mode.select;

        public enum mode
        {
          select  ,
          edit
        }


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
        public Form_Search_Product( mode mode = mode.select)
        {
            activeMode = mode;

            InitializeComponent();
         


            //InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            isSelected = false;
            this.Dispose();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            using (Form_Add_Product fb = new Form_Add_Product())
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();
                if (fb.isActionComplete)
                {

                    activeMode = mode.select;
                    loadProduct();
                    // setDefualtGrid();
                    //load product again
                   // activeMode = mode.select;

                }
            }
        }

        ynd yndInven = new ynd();
    //    List<productInfo> mstProduct = new List<productInfo>();
        DataView dvProd = new DataView();

        //private void grid_doubleClick((object sender, EventArgs e) { 




        //}

      

        private void loadProduct()
        {

            masterProduct.getdataMaster();
            var productSearchList = (from a in masterProduct.List
                                     join b in yndInven.Categories on a.categoryId equals b.categoryId
                                     join c in yndInven.ProductTypes on a.productTypeId equals c.productTypeId
                                     join d in yndInven.Units
on a.unitId equals d.unitId
                                     select new
                                     {
                                         productId= a.productId,
                                         productCode = a.productCode,
                                         productName = a.productName,
                                         price = a.price,
                                         unit = d.unitName,
                                         type = c.productTypeName,
                                         category = b.categoryName
                                     }).ToList();


            productBindingSource = new BindingSource();
            //this.dtg_prd = new DataGridView();

            //productBindingSource.DataSource = productSearchList;

            //dtg_prd.DataSource = productBindingSource;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowHeadersVisible = false;
            DataTable dtPrd = new DataTable();
            dtPrd = global.ToDataTable(productSearchList);
            dvProd = new DataView(dtPrd);
            productBindingSource.DataSource = dvProd;
            dataGridView1.Visible = false;
           // dataGridView1.AutoRedraw = false;
            dataGridView1.DataSource = dvProd;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //dataGridView1 = util.autoFit(grdProd);

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Visible = false;

            }

            dataGridView1.Columns["productCode"].Visible = true;
            dataGridView1.Columns["productName"].Visible = true;
            dataGridView1.Columns["unit"].Visible = true;
            dataGridView1.Columns["price"].Visible = true;
            dataGridView1.Columns["type"].Visible = true;
            dataGridView1.Columns["category"].Visible = true;



            dataGridView1.Columns["productCode"].HeaderText = "รหัสสินค้า";
            dataGridView1.Columns["productName"].HeaderText = "ชื่อสินค้า";
            dataGridView1.Columns["unit"].HeaderText = "หน่วย";
            dataGridView1.Columns["price"].HeaderText = "ราคา/หน่วย";
            dataGridView1.Columns["type"].HeaderText = "ประเภท";
            dataGridView1.Columns["category"].HeaderText = "หมวดหมู่";


            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();


            if (activeMode == mode.select) {
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Select";
                btn.Text = "Select";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;

            }

            if (activeMode == mode.edit)
            {
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Edit";
                btn.Text = "Edit";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;

            }



            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Refresh();
            dataGridView1.Visible = true;
           // dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
         //  dataGridView1.RowHeadersVisible = true;

        }
        private void Form_Search_Product_Load(object sender, EventArgs e)
        {
            loadProduct();
            loadMasterFilter();
            setMasterDataToUI();




        }

        private void setMasterDataToUI() {


            var ctg = masterCategory.List.ToList();
            var pdtype = masterType.List.ToList();


            cbType.DataSource = ctg;
            cbType.ValueMember = "categoryId";
            cbType.DisplayMember = "categoryName";
            cbType.SelectedIndex = -1;

            //cbCatagory.DataSource = ctg;
            //cbCatagory.ValueMember = "categoryId";
            //cbCatagory.DisplayMember = "categoryName";

            //cbType.DataSource = pdtype;
            //cbType.ValueMember = "productTypeId";
            //cbType.DisplayMember = "productTypeName";


            cbCatagory.DataSource = pdtype;
            cbCatagory.ValueMember = "productTypeId";
            cbCatagory.DisplayMember = "productTypeName";
            cbCatagory.SelectedIndex = -1;

        }

        private void loadMasterFilter() {
            Operation.loadProducType();
            Operation.loadCategoly();


        }

        string flterPrdCode = "";
        string filterPrdName = "";
        string filterType = "";
        string filterCategory = "";



        private void filterData()
        {

            string filter = "";

            if (tbProdCode.Text.ToString().Trim() != "")
            {
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


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          

            if (e.RowIndex >= 0)
            {

                string columnButonName = dataGridView1.Columns[e.ColumnIndex].HeaderText.ToString().ToUpper();
               // if (dataGridView1.Columns[e.ColumnIndex].HeaderText.ToString().ToUpper() == "SELECT")
                    if (columnButonName == "SELECT")
                    {

                    productCode = this.dataGridView1.Rows[e.RowIndex].Cells["productCode"].Value.ToString();
                    isSelected = true;
                    this.Dispose();
                    return;
                }

              //  if (dataGridView1.Columns[e.ColumnIndex].HeaderText.ToString().ToUpper() == "EDIT")

                    if(columnButonName == "EDIT")
                {

                 int   productID = (int)this.dataGridView1.Rows[e.RowIndex].Cells["productID"].Value;



                    using (Form_Add_Product fb = new Form_Add_Product(Form_Add_Product.mode.EDIT))
                    {

                        fb.productId = productID;
                        fb.StartPosition = FormStartPosition.CenterParent;
                        fb.ShowDialog();
                        if (isAddProuctComplete)
                        {
                            loadProduct();
                        


                        }
                    }

                }



            }
        }



    }
}
