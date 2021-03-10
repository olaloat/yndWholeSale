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

namespace WholeSale.Forms
{
    public partial class Form_Add_Product : Form
    {
        public Form_Add_Product()
        {
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

        private void btSave_Click(object sender, EventArgs e)
        {
            var rs = validateData();
            if (!rs.isComplete)
            {

               // rs = insertDataToDB();
                //string message = rs.message;
                //string title = "ผลการทำงาน";
                //MessageBox.Show(message, title);




              
             
                using (Modal_MsgBox msg = new Modal_MsgBox(rs.message))
                {
                    msg.StartPosition = FormStartPosition.CenterParent;
                    msg.ShowDialog();

                }
            }



            if (rs.isComplete)
            {
                rs = insertDataToDB();
                //string message = rs.message;
                //string title = "ผลการทำงาน";
                //MessageBox.Show(message, title);


                using (Modal_MsgBox msg = new Modal_MsgBox(rs.message))
                {
                    msg.StartPosition = FormStartPosition.CenterParent;
                    msg.ShowDialog();

                }

                Form_Search_Product.atusAddNewProduct = true;
                productMaintain.haveNewProduct = true;
                this.Dispose();
             


            }

        }

        private mainResult validateData() {

            mainResult rs = new mainResult();
            ynddevEntities db = new ynddevEntities();
         
            List<Product> prd = new List<Product>();
            prd = (from a in db.Products where a.productCode.Contains(tbProductCode.Text) select a).ToList();
            if (prd.Count > 0) {
                rs.isComplete = false;
                rs.message = "รหัสสินค้านี้มีอยู่ในระบบแล้ว ไม่สามารถบันทึกซ้ำได้";

                return rs;

            }

            rs.isComplete = true;
            rs.message = "ok";

            return rs;



        }


        private mainResult insertDataToDB() {
            mainResult rs = new mainResult();
            try { 
           
            ynddevEntities db = new ynddevEntities();
            Product prd = new Product() {
                productCode =tbProductCode.Text.Trim(),
                productName =tbProductName.Text.Trim(),
                price = Convert.ToDecimal(tbPrice.Text.Trim()) ,
                maxPrice = Convert.ToDecimal(tbMaxPrice.Text.Trim()),
                 minPrice = Convert.ToDecimal(tbMinPrice.Text.Trim()),
                  previousPrice =0,
                   branchCode=Global.branchCode,
                   compCode=Global.compCode,
                    categoryId=Convert.ToInt16(cbCategory2.SelectedValue),
                     createBy ="ADMIN",
                      createTime =DateTime.Now,
                       editTime =DateTime.Now,
                       editBy = "ADMIN",
                        groupId = Convert.ToInt16(cbGroup.SelectedValue),
                         isActive =true,
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
        }

        private void setMaster (){
           ynddevEntities db = new ynddevEntities();
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

      
    }
}
