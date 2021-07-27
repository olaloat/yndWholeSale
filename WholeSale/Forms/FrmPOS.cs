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
using WholeSale;
using System.Reflection;
using WholeSale.report;

namespace WholeSale.Forms
{
    public partial class FrmPOS : Form
    {

       
        public FrmPOS()
        {
            this.DoubleBuffered = true;

            //BackgroundWorker bgw = new BackgroundWorker();
            //bgw.RunWorkerAsync();
            //bgw.DoWork += new DoWorkEventHandler(dowork);
            //bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completed);


            InitializeComponent();
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


        #region param

        UI myUI = new UI();
        List<DocumentLineDisplay> MydocLine = new List<DocumentLineDisplay>();
        DocumentDisplay myDocHeader = new DocumentDisplay();
        Customer myCustomer = new Customer();
        Vendor myVendor = new Vendor();
        



        
        decimal myDicount;
        int qty = 1;
        #endregion

        #region "controller"
        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmPOS_LOAD(object sender, EventArgs e)
        {
            // DoubleBuffered = true;
           // Panel pnLoading = new Panel();
           // this.Controls.Add(pnLoading);

           //pnLoading.Visible = true;
           //pnLoading.BringToFront();
           //pnLoading.Dock = DockStyle.Fill;
            Log.print("Open App");
            global.setParamGlobal();//start global load all master
            Log.print("product master count = " + masterProduct.List.Count.ToString());
            setGlobalInformation();
            setDefult();

           // testrun.run();
        }

        private void setDefult() {
            clearData();// data list
            Operation.loadProduct();
            Operation.loadCustomer();
            int qty = global.defultQty;
            defultGrid();
            setUiSummary(myDocHeader);

         
            // setUiDocHeader(fb.MyCustSelected);

            Customer myCust = masterCustomer.List.ToList().Where(w => w.customerId == 1).FirstOrDefault();
            setUIcustomer(myCust);
            DocumentDisplay doc = new DocumentDisplay() { createTime = System.DateTime.Now, status = 0, documentNo = "" };
            setUiDocHeader(doc);
        }


        private void dataGridView3_CellClick_1(object sender, DataGridViewCellEventArgs e)
        { }

        private void button15_Click(object sender, EventArgs e)
        {
        }



        private void tbxQty_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void button22_Click(object sender, EventArgs e)
        {
        }

        private void button21_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btPayment_Click(object sender, EventArgs e)
        {
            if (MydocLine.Count > 0)
            {
                OpenPayment();

            }
            else
            {
                mMsgBox.show("ไม่มีรายการสินค้า", Modal_MsgBox.icon.error);
            }
        }


        private void setDefultScan() {
            tbxQty.Text = "1";
            tbScan.Text = "";
        }


        private void setProductTolist(string prdCode , int qty) {
            Product myProd = scan(prdCode, qty);
            if (!myProd.myResult.isComplete)
            {
                mMsgBox.show(myProd.myResult.message);
            }
            else
            {
                // add new prd
                if (MydocLine.Where(w => w.productId == myProd.productId).ToList().Count <= 0)
                {
                    MydocLine = Operation.addList(MydocLine, myProd);
                } // update prd
                else
                {
                    MydocLine = Operation.updateDocline(MydocLine, myProd);
                }
                setGridAndSummary();
            }
            setDefultScan();
        }


        private void setGridAndSummary() {
            UI.setGrid(ref dataGridView2, MydocLine);
        
            myDocHeader = Operation.setSummaryData( myDocHeader,   MydocLine, myCustomer, myDicount);
            setUiSummary(myDocHeader);
        }

        private void tbScan_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                setProductTolist(tbScan.Text, int.Parse(tbxQty.Text));
            }
        }

        private void btSearchProduct_Click(object sender, EventArgs e)
        {
            using (Form_Search_Product fb = new Form_Search_Product(Form_Search_Product.mode.select))
            {
                productMaintain.clear();
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();
                if (fb.isSelected) {
                    string code = fb.productCode;
                    if (code != "")
                    {
                        tbScan.Text = code;
                        setProductTolist(code , int.Parse(tbxQty.Text));

                     
                    }
                }
            }



            //===========================
          //  List<document xxx =Util. copyDataFromChildToParentModel(MydocLine);



            //===========================
        }


        private void setUIcustomer(Customer selectedCustomer) {
            if (selectedCustomer == null) return;
            lbCustomer.Text = selectedCustomer.customerName.ToString();
            lbAddress.Text = selectedCustomer.address1;
            label22.Text = selectedCustomer.address2;


        }
        private void btCustomer_Click(object sender, EventArgs e)
        {
            using (Form_Search_Customer fb = new Form_Search_Customer(option.mode.SELECT))
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

                if (fb.isSelected)
                {
                    //myDocHeader = fb.MyCustSelected;
                    myCustomer = fb.MyCustSelected;
                    myDocHeader = Operation.setCustIdToDocument(myDocHeader, fb.MyCustSelected.customerId);
                    setUIcustomer(fb.MyCustSelected);



                }


            }
        }



        private void tbScan_TextChanged(object sender, EventArgs e)
        {
        }




        private void tbPuase_Click(object sender, EventArgs e)
        {


            if (MydocLine.Count() > 0) {
                    DialogResult result = mMsgBox.show("ยืนยันการพักรายการนี้", Modal_MsgBox.MessageBoxButtons.YesNo);
                    if (result.Equals(DialogResult.Yes))
                    {

                    mainResult rs = new mainResult();
                    Bill billHold = new Bill() { docLine = MydocLine, docHeader = myDocHeader };

                    // update bill hold
                    if (billHold.docHeader.status == (int) global.statusList.HOLD) {
                        rs = Operation.updateHoldBill(billHold);
                    }

                    // insert new hold bill
                    if (billHold.docHeader.status==(int)global.statusList.NONE)
                    {
                         
                        rs = Operation.holdNewBill(billHold);
                    }
                   

                    if (rs.isComplete) {
                        mMsgBox.show(rs.message);
                        setDefult();
                    } else {
                        mMsgBox.show(rs.message ,Modal_MsgBox.icon.error);
                    }
                       // mMsgBox.show("บันทึกสำเร็จ", Modal_MsgBox.icon.information);
                    }
                    else
                    {

                        mMsgBox.show("ไม่ save แล้วกดทำไม??", Modal_MsgBox.icon.warning);
                    }
            } else {
                // mMsgBox.show("ยังไม่มีรายการสินค้า",Modal_MsgBox.icon.error);
                ////   hold  /////////////
                using (Form_Pending fb = new Form_Pending())
                {
                    fb.StartPosition = FormStartPosition.CenterParent;
                    fb.ShowDialog();
                    if (fb.isSelected)
                    {
                        foreach ( var a in fb.myDocLine) {
                            setProductTolist(a.productCode.ToString(), (int)a.qty);

                          //  setProductTolist(tbScan.Text, int.Parse(tbxQty.Text));
                        }
                        var myCust = masterCustomer.List.Where(w => w.customerId == fb.myDocHeader.customerId).FirstOrDefault();
                        setUIcustomer(myCust);
                        myDocHeader = new DocumentDisplay();
                        MydocLine = new List<DocumentLineDisplay>();
                        myDocHeader = fb.myDocHeader;
                        MydocLine = fb.myDocLine.ToList();
                        UI.setGrid(ref dataGridView2, MydocLine);
                        setUiDocHeader(myDocHeader);
                        setUiSummary(myDocHeader);

                    }
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)




        {


            Bill xxx = new Bill();
            xxx.print();

            if (MydocLine.Count<=0) { return; }

            if (myDocHeader.documentId !=0   ) {
                //==========================  cancel doc from holding ==================================
                DialogResult rsSelected = mMsgBox.show("รายการนี้เป็นรายการที่เปิดมาจากรายการที่พักไว้ หากท่านยืนยันรายการนี้จะถูกยกเลิกออกจากระบบ",
                    Modal_MsgBox.MessageBoxButtons.YesNo, Modal_MsgBox.icon.warning, "ยืนยันการยกเลิกรายการ");

                if (rsSelected ==DialogResult.Yes) {
                    //====== disable doc ========================================================================
                    mainResult rs = db.disableDocuments(myDocHeader.documentId);

                    if (rs.isComplete)
                    {
                        mMsgBox.show(rs.message, Modal_MsgBox.icon.information, "ผลการยกเลิกรายการ");
                        setDefult();
                    }
                    else
                    {
                        mMsgBox.show(rs.message, Modal_MsgBox.icon.error, "Error");

                    }
                }

            
          

            }
            else {
                //==========================  cancel temp doc ==================================

                DialogResult rsSelected = mMsgBox.show("ยืนยันเพื่อยกเลิกรายการที่ดำเนินการอยู่",
               Modal_MsgBox.MessageBoxButtons.YesNo, Modal_MsgBox.icon.warning, "ยืนยันการยกเลิกรายการ");

                if (rsSelected == DialogResult.Yes)
                {
                    setDefult();

                }


            }
       

         





        }

        private void btFinalDc_Click(object sender, EventArgs e)
        {
            if (MydocLine.Count > 0)
            {
                using (Modal_FinalDc fb = new Modal_FinalDc(myDocHeader))
                {
                    fb.StartPosition = FormStartPosition.CenterParent;
                    fb.ShowDialog();
                    if (fb.isOkClick) {
                        myDocHeader = fb.DocumentHeader;
                        OpenPayment();
                    }
                }
            }
            else
            {
                mMsgBox.show("ไม่มีรายการสินค้า", Modal_MsgBox.icon.error);
            }
        }
        #endregion

        #region "method"


        private Product scan(string prdCode, int qty) {
            Product myPrd = new Product();
            if (masterProduct.List.Where(w => w.productCode == prdCode).ToList().Count > 0) {
                myPrd = masterProduct.List.Where(w => w.productCode == prdCode).FirstOrDefault();
                myPrd.qty = qty;
                myPrd.myResult = new mainResult();
                myPrd.myResult.isComplete = true;
                myPrd.myResult.message = "OK";
            } else {
                myPrd.myResult = new mainResult();
                myPrd.myResult.isComplete = false;
                myPrd.myResult.message = "ไม่พบรายการสินค้าที่ท่านสแกน";
            }
            return myPrd;
        }


        private void setGlobalInformation() {
            global. compCode ="001";
            global.plantCode = "001";
            global.BranchCode = "001";
            global.username = "oat";
        }
        private void clearData() {
        myUI = new UI();
        MydocLine = new List<DocumentLineDisplay> ();
        myDocHeader = new DocumentDisplay() {
           compCode =  global. compCode  , 
           branchCode = global.BranchCode,
           createBy = global.username,
            editBy = global.username,
        };
        }
        private void defultGrid() {
           // dataGridView2.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView2, true, null);
            dataGridView2 = UI.prepareGridDocumentLine(dataGridView2, MydocLine);
        }

      

        private void setUiSummary(DocumentDisplay docHeader)
        {
            if (docHeader !=null) {
                 lbPreiceBeforeDC.Text = Convert.ToString(docHeader.totalPriceBeforeDiscount);
                if (lbPreiceBeforeDC.Text.ToString() =="") { lbPreiceBeforeDC.Text = "0"; }
                decimal priceBefoereDc = decimal.Parse(lbPreiceBeforeDC.Text);
                lbPreiceBeforeDC.Text = priceBefoereDc.ToString("#,##0.#0");
                lbVat.Text = docHeader.totalVat.ToString("#,##0.#0");
                lbDiscount.Text = docHeader.totalDiscount.ToString("#,##0.#0");
                lbPriceAfterDiscount.Text = (docHeader.totalPriceAfterAllDiscount ).ToString("#,##0.#0");
                lbAmountIncludeVat.Text = (docHeader.totalPriceAfterAllDiscount).ToString("#,##0.#0");
                lbPriceBeforeVat.Text = (docHeader.totalPriceAfterAllDiscount - docHeader.totalVat).ToString("#,##0.#0");
            }
        }


        private void setUiDocHeader(DocumentDisplay docHeader )
        {

            lbDocNo.Text = docHeader.documentNo;
            lbRef.Text = "";
            lbStatus.Text = Enum.GetName(typeof(global.statusList), docHeader.status); ;
            lbIssueDate.Text = docHeader.createTime.ToString("dd.MM.yyyy");

         
        }


        private void editLine(int prdSlect)
        {
            using (Modal_Edit fb = new Modal_Edit( MydocLine ,prdSlect))
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();
                MydocLine = fb.listEdited;
                setGridAndSummary();
            }
        }

        private void OpenPayment()
        {

            mainResult rs = new mainResult();
            using (Modal_Payment fb = new Modal_Payment(myDocHeader , MydocLine , myCustomer , myVendor))
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();
                rs.isComplete = fb.payComplete;
                setDefult();
            }
        }


        #endregion

        #region database


        #endregion



        #region UI
        private void prepareGrid() {
        }

        #endregion

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int prodCodeSelect = 0;
            if (e.RowIndex >= 0)
            {
                prodCodeSelect = int.Parse(this.dataGridView2.Rows[e.RowIndex].Cells["productId"].Value.ToString());

                if (this.dataGridView2.Columns[e.ColumnIndex].HeaderText == "Edit")
                {
                    editLine( prodCodeSelect);
                }

                if (this.dataGridView2.Columns[e.ColumnIndex].HeaderText == "Delete")
                {
                    Operation.removeList(MydocLine , prodCodeSelect);
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = MydocLine;
                dataGridView2.DataSource = bs;
                dataGridView2.Refresh();
                myDocHeader = Operation.setSummaryData(myDocHeader ,MydocLine, myCustomer, myDicount);
                setUiSummary(myDocHeader);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmPOS_Activated(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            using (PrintBill fb = new PrintBill(myDocHeader  , MydocLine))
            {
                fb.StartPosition = FormStartPosition.CenterParent;
                fb.ShowDialog();

            }
        }
    }


    //public static class ExtensionMethods
    //{
    //    public static void DoubleBuffered(this DataGridView dgv, bool setting)
    //    {
    //        Type dgvType = dgv.GetType();
    //        PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
    //        pi.SetValue(dgv, setting, null);
    //    }
    //}

}
