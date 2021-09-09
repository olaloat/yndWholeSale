using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
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

        bool isUpPict = false;
        string pathPict = "";
        mode activeMode = mode.NEW;
        public Product myProduct { get; set; }
        public Picture myPicture { get; set; }
        public  int productId { get; set; }

        public  bool isActionComplete {get ; set;}

        public string pathPicture = "";

        public enum mode
        {
            NEW,
            EDIT
        }
        public Form_Add_Product(mode activeMode = mode.NEW)

        {
            this.activeMode = activeMode;
            this.pathPict = "";
       
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


        private mainResult validateDuplicateProductCode() {
            ynd db = new ynd();
            List<Product> prd = new List<Product>();
            mainResult rs = new mainResult();
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

        private mainResult validateSaveProduct(mode modeValidate) {
            mainResult rs = new mainResult();

           // if (modeValidate == mode.NEW) { 
            rs = validateDuplicateProductCode();
                 if (!rs.isComplete)
            {
                
              

                return rs;
            }

            //}

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
         
            rs.isComplete = true;
            rs.message = "ok";
            return rs;
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            prodResultSaveDB rsPrd = new prodResultSaveDB();
            mainResult rsPct = new mainResult();
            Byte[] btpct;
            Picture pct = new Picture();
            if (pictureBox1.Image!=null) {

                btpct = Util.convertImageToByte(pictureBox1);
                // set instance picture model 
              
                pct = Util.setStadardInfo(pct);
                pct.image = btpct;
            }




           
         




            if (activeMode == mode.NEW) {
                tbProductCode.ReadOnly = false;
              mainResult   rsVld  = validateSaveProduct(activeMode);
                if (!rsVld.isComplete)
                {
                    mMsgBox.show(rsVld.message,Modal_MsgBox.icon.error,"Error");
                    return;
                }
                //  insert product to db
                rsPrd = insertDataToDB();



                pct.productId = rsPrd.ProdID;
                if (pct.image==null) {

                    pct.isActive = false;
                }
                else
                {
                    rsPct = db.insertPictureProduction(pct);


                }


                // insert picture
          

                //update  id to production

            }
            else if (activeMode == mode.EDIT) {
                tbProductCode.ReadOnly = true;
                mainResult rsVld = validateSaveProduct(activeMode);
                if (!rsVld.isComplete)
                {
                    mMsgBox.show(rsVld.message, Modal_MsgBox.icon.error, "Error");
                    return;
                }


                rsPrd = editDataToDB();

         
                pct.productId = rsPrd.ProdID;
                if (pct.image == null)
                {

                    pct.isActive = false;
                }
                else {
                    rsPct = db.insertPictureProduction(pct);

                }
          


               
            }
                mMsgBox.show(rsPct.message);
            if (rsPct.isComplete)
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


        private prodResultSaveDB editDataToDB() {
            prodResultSaveDB rs = new prodResultSaveDB();
            //try
            //{

            ynd myEn = new ynd();
         
            Product myEdit = (from a in myEn.Products where a.productId == myProduct.productId select a).FirstOrDefault();

            Product myprod = new Product();


            myprod = Util.setStadardInfo(myprod, global.mode.EDIT);
            myprod.productId = myEdit.productId;
          myprod.productCode = tbProductCode.Text.Trim();
          myprod.productName = tbProductName.Text.Trim();
          myprod.price = Convert.ToDecimal(tbPrice.Text.Trim());
          myprod.maxPrice = Convert.ToDecimal(tbMaxPrice.Text.Trim());
          myprod.minPrice = Convert.ToDecimal(tbMinPrice.Text.Trim());
          myprod.previousPrice = myEdit.previousPrice;
          myprod.groupId = Convert.ToInt16(cbGroup.SelectedValue);
            myprod.categoryId = Convert.ToInt16(cbCategory2.SelectedValue);
            myprod.isActive = active;
          myprod.typeId = Convert.ToInt16(cbType.SelectedValue);
          myprod.productTypeId = Convert.ToInt16(cbType.SelectedValue);
          myprod.unitId = Convert.ToInt16(cbUnit.SelectedValue);
            myprod.createBy = myEdit.createBy;
            myprod.createTime = myEdit.createTime;


            rs = db.updateProduct(myprod);

            //rs.isComplete = true;
            //rs.message = "บันทึกสำเร็จ";

            return rs;



        }




        private mainResult insertBalance(int id ) {
            mainResult rs = new mainResult();

            try
            {
                ynd db = new ynd();
                Balance balance = new Balance();

                balance = Util.setStadardInfo(balance);
                balance.productId = id;
                balance.productName = tbProductName.Text.Trim();
                balance.qty = global.defualtQtyBalance;
                balance.avgPrice = 0;
                balance.maxPrice = 0;
                balance.maxPriceQty = 0;
                balance.minPrice = 0;
                balance.minPriceQty = 0;
                balance.isActive = true;


                db.Balances.Add(balance);
                db.SaveChanges();
                rs.isComplete = true;
                rs.message = "บันทึกสำเร็จ";
                rs.status = "OK";


            } catch (Exception e) {
                string xxx = e.ToString();
                rs.isComplete = false;
                rs.message = "ERROR :";
                rs.message += e.ToString();
          
                return rs;
            }
            return rs;
        }


        private prodResultSaveDB insertPrd()
        {
            prodResultSaveDB rs = new prodResultSaveDB();
            try
            {
                ynd db = new ynd();
                Product prd = new Product()
                {
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

                rs.ProdID = prd.productId;
                rs.isComplete = true;
                rs.message = "บันทึกสำเร็จ";
                rs.status = "OK";

            }
            catch (Exception e)
            {
                string xxx = e.ToString();
                rs.isComplete = false;
                rs.message = "ERROR :";
                rs.message += e.ToString();
                rs.ProdID = 0;
                return rs;
            }


            return rs;

        }
        private prodResultSaveDB insertDataToDB() {
            prodResultSaveDB rsprd = new prodResultSaveDB();
            mainResult rsBalance = new mainResult();

            try {

            rsprd = insertPrd();


                if (!rsprd.isComplete)
                {


                    rsprd.isComplete = false;
                    rsprd.message = "ERROR :";
                    rsprd.message += rsBalance.message;
                    rsprd.ProdID = 0;
                    return rsprd;


                }

                rsBalance = insertBalance(rsprd.ProdID );

            if (!rsBalance.isComplete) {

              
               rsprd.isComplete = false;
               rsprd.message = "ERROR :";
                rsprd.message += rsBalance.message;
                rsprd.ProdID = 0;
                return rsprd;


            }


            }
            catch (Exception e)
            {
                string xxx = e.ToString();
                rsprd.isComplete = false;
                rsprd.message = "ERROR :";
                rsprd.message += e.ToString();
                rsprd.ProdID = 0;
                return rsprd;
            }


            return rsprd;

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
                myPicture = loadPicture(productId);
                setProdToControl();
            }
        }
        private Product loadProduct(int id) {
            ynd myEn = new ynd();


            var prodSelect = myEn.Products.Where(w => w.productId == id).FirstOrDefault();

            return prodSelect;
        }


        private Picture loadPicture(int id)
        {
            ynd myEn = new ynd();


            var picture = myEn.Pictures.Where(w => w.productId == id && w.isActive ==true).FirstOrDefault();

            return picture;
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


            if (myPicture !=null) {
                byte[] _ImageData = new byte[0];
                _ImageData = (byte[])myPicture.image;
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_ImageData);
                pictureBox1.Image = System.Drawing.Image.FromStream(_MemoryStream);

            }
          //if (myProduct.isActive == null) { this.active = false; }
          //  else{
                this.active =(bool) myProduct.isActive;
          //  }


            setButtonActive(active);





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

            //if (active) {
            //    btnActive.BackColor = Color.Green;


            //    btnActive.Text = "Active";
            //} else {
            //    btnActive.Text = "InActive";

            //    btnActive.BackColor = Color.Red;
            //}

            setButtonActive(active);
        }

        private void setButtonActive(bool _active) {


            if (_active)
            {
                btnActive.BackColor = Color.Green;


                btnActive.Text = "Active";
            }
            else
            {
                btnActive.Text = "InActive";

                btnActive.BackColor = Color.Red;
            }

        }

        private void btAttachPict_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {

                isUpPict = true;
                // display image in picture box  
                pictureBox1.Image = new Bitmap(open.FileName);
                // image file path  
                tbxPath.Text = open.FileName;

            
            }
        }

      
    }
}
