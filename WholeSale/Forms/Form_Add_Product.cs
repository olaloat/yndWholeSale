using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WholeSale.Model;
using WholeSale.MyClass;

namespace WholeSale.Forms
{
    public partial class Form_Add_Product : Form
    {
        mode activeMode = mode.NEW;
        public Product myProduct { get; set; }
        public  int productId { get; set; }

        public  bool isActionComplete {get ; set;}

        public enum mode
        {
            NEW,
            EDIT
        }
        public Form_Add_Product(mode activeMode = mode.NEW)

        {
            this.activeMode = activeMode;
            InitializeComponent();
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
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }




        private mainResult validateSaveProduct() {

            mainResult rs = new mainResult();

            if (tbProductCode.Text .ToString().Trim().Length==0)
            {
                rs = new mainResult();
                rs.isComplete = false;
                rs.message = "please input product code";

                return rs;
            }




            if (cbUnit.Text.ToString().Trim().Length == 0)
            {

                rs = new mainResult();
                rs.isComplete = false;
                rs.message = "please input Unit .";

                return rs;
            }


            if (tbMinPrice.Text.ToString().Trim().Length == 0)
            {
                rs = new mainResult();
                rs.isComplete = false;
                rs.message = "please input min Price .";

                return rs;

            }


            if (tbMaxPrice.Text.ToString().Trim().Length == 0)
            {


                rs.isComplete = false;
                rs.message = "please input max Price .";

                return rs;




            }


            if (tbPrice.Text.ToString().Trim().Length == 0)
            {

                rs.isComplete = false;
                rs.message = "please input  Price. ";

                return rs;
            }


            if (cbGroup.Text.ToString().Trim().Length == 0)
            {

                rs.isComplete = false;
                rs.message = "please select group .";

                return rs;
            }


            if (cbType.Text.ToString().Trim().Length == 0)
            {
                rs.isComplete = false;
                rs.message = "please select type .";

                return rs;

            }


            if (cbCategory2.Text.ToString().Trim().Length == 0)
            {
                rs.isComplete = false;
                rs.message = "please select category .";

                return rs;

            }


            if (tbProductName.Text.ToString().Trim().Length == 0)
            {
                rs.isComplete = false;
                rs.message = "please input product name.";

                return rs;

            }

     


            double mxP = Convert.ToDouble(tbMaxPrice.Text.ToString());
            double mnP = Convert.ToDouble(tbMinPrice.Text.ToString());
            double P = Convert.ToDouble(tbPrice.Text.ToString());


            if (!(mnP <= P && P <= mxP && mnP <= mxP)) {
                rs = new mainResult();
                rs.isComplete = false;
                rs.message = "price , max , min is worng.";

                return rs;


            }

         
            ynd db = new ynd();

            List<Product> prd = new List<Product>();
            prd = (from a in db.Products where a.productCode == tbProductCode.Text.ToString().Trim() select a).ToList();
            if (prd.Count > 0)
            {
                rs.isComplete = false;
                rs.message = "รหัสสินค้านี้มีอยู่ในระบบแล้ว ไม่สามารถบันทึกซ้ำได้";

                return rs;

            }

            rs.isComplete = true;
            rs.message = "ok";



            return rs;


        }
        private void btSave_Click(object sender, EventArgs e)
        {
            mainResult rs = new mainResult();
            // calidate product code
            if (activeMode == mode.NEW) {
                rs = validateSaveProduct();
                if (!rs.isComplete)
                {
                    mMsgBox.show(rs.message,Modal_MsgBox.icon.error,"Error");
                    return;
                }
                rs = insertDataToDB();

            } else if (activeMode == mode.EDIT) {


                rs = editDataToDB();
            }


                mMsgBox.show(rs.message);
        


         //   mMsgBox.show(rs.message);
            if (rs.isComplete)
            {


                isActionComplete = true;
                Operation.loadProduct();



                this.Dispose();

            }
            else
            {

                isActionComplete = false;
            }



            this.Dispose();
          //  }

        }

        private mainResult validateData() {

            mainResult rs = new mainResult();
            ynd db = new ynd();

            List<Product> prd = new List<Product>();
            prd = (from a in db.Products where a.productCode ==tbProductCode.Text.ToString().Trim() select a).ToList();
            if (prd.Count > 0) {
                rs.isComplete = false;
                rs.message = "รหัสสินค้านี้มีอยู่ในระบบแล้ว ไม่สามารถบันทึกซ้ำได้";

                return rs;

            }

            rs.isComplete = true;
            rs.message = "ok";

            return rs;



        }


        private mainResult editDataToDB() {
            mainResult rs = new mainResult();
            try
            {

                Product myprod = new Product() {
                    // Product myEditPrd = db.Products.Where(w => w.productId {== myProduct.productId).FirstOrDefault();
                    productId = productId,
               productCode = tbProductCode.Text.Trim(),
               productName = tbProductName.Text.Trim(),
               price = Convert.ToDecimal(tbPrice.Text.Trim()),
               maxPrice = Convert.ToDecimal(tbMaxPrice.Text.Trim()),
               minPrice = Convert.ToDecimal(tbMinPrice.Text.Trim()),
               previousPrice = 0,
               branchCode = global.BranchCode,
               compCode = global.compCode,
               categoryId = Convert.ToInt16(cbCategory2.SelectedValue),
             //  createBy = "ADMIN",
             //  createTime = DateTime.Now,
               editTime = DateTime.Now,
               editBy = global.username,
                    groupId = Convert.ToInt16(cbGroup.SelectedValue),
               isActive = true,
               typeId = Convert.ToInt16(cbType.SelectedValue),
               productTypeId = Convert.ToInt16(cbType.SelectedValue),
               unitId = Convert.ToInt16(cbUnit.SelectedValue),
            };


                 rs = db.updateProduct(myprod);

            }
            catch (Exception e)
            {
                string xxx = e.ToString();
                rs.isComplete = false;
                rs.message = "ERROR";
                rs.isComplete = true;
                rs.message = e.ToString();
                return rs;
            }


            rs.isComplete = true;
            rs.message = "บันทึกสำเร็จ";

            return rs;



        }

        private mainResult insertDataToDB() {
            mainResult rs = new mainResult();
            try {

                ynd db = new ynd();
                Product prd = new Product() {
                    productCode = tbProductCode.Text.Trim(),
                    productName = tbProductName.Text.Trim(),
                    price = Convert.ToDecimal(tbPrice.Text.Trim()),
                    maxPrice = Convert.ToDecimal(tbMaxPrice.Text.Trim()),
                    minPrice = Convert.ToDecimal(tbMinPrice.Text.Trim()),
                    previousPrice = 0,

                    categoryId = Convert.ToInt16(cbCategory2.SelectedValue),


                    branchCode = global.BranchCode,
                    compCode = global.compCode,
                    createBy = global.username,
                    createTime = DateTime.Now,
                    editTime = DateTime.Now,
                    editBy = global.username,


                    groupId = Convert.ToInt16(cbGroup.SelectedValue),
                    isActive = active,
                    typeId = Convert.ToInt16(cbType.SelectedValue),
                    productTypeId = Convert.ToInt16(cbType.SelectedValue),
                    unitId = Convert.ToInt16(cbUnit.SelectedValue),



                };

                db.Products.Add(prd);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                string xxx = e.ToString();
                rs.isComplete = false;
                rs.message = "ERROR";
                rs.isComplete = true;
                rs.message = e.ToString();
                return rs;
            }


            rs.isComplete = true;
            rs.message = "บันทึกสำเร็จ";

            return rs;

        }

        private void Form_Add_Product_Load(object sender, EventArgs e)
        {
            setMaster();
            clearControl();
            if (activeMode == mode.NEW) {
                myProduct = new Product();
             //   clearControl();
            }

            if (activeMode == mode.EDIT) {
                //   clearControl();
              myProduct =  loadProduct(productId);
                setProdToControl();
            }
        }
        private Product loadProduct(int id) {
            ynd myEn = new ynd();


            var prodSelect = myEn.Products.Where(w => w.productId == id).FirstOrDefault();

            return prodSelect;
        }
        private void setProdToControl() {

            tbProductCode.Text = myProduct.productCode.ToString();
            tbProductName.Text = myProduct.productName.ToString();
            cbCategory2.SelectedValue = myProduct.categoryId;
            cbType.SelectedValue = myProduct.typeId;
            cbGroup.SelectedValue = myProduct.groupId;
            tbPrice.Text = myProduct.price.ToString();
            tbMaxPrice.Text = myProduct.maxPrice.ToString();
            tbMinPrice.Text = myProduct.minPrice.ToString();
            tbStandardPack.Text = "";
            cbUnit.SelectedValue = myProduct.unitId;
            tbMFG.Text = "";
            tbSupplier.Text = "";
            pictureBox1.Image = null;
            tbxPath.Text = "";




        }

        private void setMaster (){
           ynd db = new ynd();
           //List< Category > ctg = new List<Category>();
            //List < ProductType > pdtype = new List<ProductType>();
            //List<Unit> unit = new List<Unit>();
            var ctg = db.Categories.ToList();
            var pdtype = db.ProductTypes.ToList();
            var unit = db.Units.ToList();
            var grp = db.Groups.ToList();

            //pdtype = (from a in db.ProductTypes select new ProductType { productTypeId = a.productTypeId, productTypeName = a.productTypeName }).ToList();

            //unit = (from a in db.Units select new Unit { unitId = a.unitId, unitName = a.unitName }).ToList();

         

            cbCategory2.DataSource = ctg.ToList();
            cbCategory2.ValueMember = "categoryId";
            cbCategory2.DisplayMember = "categoryName";


            cbType.DataSource = pdtype.ToList();
            cbType.ValueMember = "productTypeId";
            cbType.DisplayMember = "productTypeName";

            cbUnit.DataSource = unit.ToList();
            cbUnit.ValueMember = "unitId";
            cbUnit.DisplayMember = "unitName";


            cbGroup.DataSource = grp.ToList();
            cbGroup.ValueMember = "groupId";
            cbGroup.DisplayMember = "groupName";

        }

        private void tbPrice_TextChanged(object sender, EventArgs e)
        {
             tbPrice.Text=inputCurreny(tbPrice.Text);
        }



        private string inputCurreny(string input) {
            string output = "";
            string s = input;

            float f;
            if (float.TryParse(s, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out f))
            {
                // OK
                output = input;

            }
            else
            {
                // WRONG
                output = "";
            }

            return output;
        }

        private void tbMaxPrice_TextChanged(object sender, EventArgs e)
        {
            tbMaxPrice.Text = inputCurreny(tbMaxPrice.Text);
        }

        private void tbMinPrice_TextChanged(object sender, EventArgs e)
        {
            tbMinPrice.Text = inputCurreny(tbMinPrice.Text);
        }


        private void clearControl() {
            tbProductCode.Text = "";
tbProductName.Text = "";
            cbCategory2.SelectedIndex = -1;
            cbType.SelectedIndex = -1;
cbGroup.SelectedIndex = -1;
            tbPrice.Text = "";
            tbMaxPrice.Text = "0";
            tbMinPrice.Text = "0";
            tbStandardPack.Text = "";
cbUnit.SelectedIndex = -1;
            tbMFG.Text = "";
            tbSupplier.Text = "";
pictureBox1.Image = null;
            tbxPath.Text = "";


        }

        private void btClear_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }


        bool active = true;
        private void btnActive_Click(object sender, EventArgs e)
        {
            active = !active;

            if (active) {
                btnActive.BackColor = Color.Green;


                btnActive.Text = "Active";
            } else {
                btnActive.Text = "InActive";

                btnActive.BackColor = Color.Red;
            }
        }
    }
}
