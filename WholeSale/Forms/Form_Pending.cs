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
    public partial class Form_Pending : Form
    {


       // ynddevEntities yndInven = new ynddevEntities();
        BindingSource bsHoldingList = new BindingSource();
        DataTable dtHodling = new DataTable();
        DataView dvHodling = new DataView();
        ynd ynd = new ynd();
     


        List<DocumentDisplay> myDocumentList = new List<DocumentDisplay>();
        public DocumentDisplay myDocHeader { get; set; }
        public List<DocumentLineDisplay> myDocLine { get; set; }
        public bool isSelected { get; set; }
      

        public Form_Pending()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void clearData() {
            myDocHeader = new DocumentDisplay();
            isSelected = false;

          
        }
        private void Form_Pending_Load(object sender, EventArgs e)
        {

            clearData();
         //   defultGrid();
            Operation.loadDocument();
            dtgHoldingList = UI.prepareGridDocument(dtgHoldingList, masterDocument.List);
    }


    //private void defultGrid()
    //    {
    //        dtgHoldingList = UI.prepareGridDocument(dtgHoldingList, myDocumentList);
    //    }


        private void loadHoldingBill()
        {

            var docList = (from a in ynd.Documents where  a.status ==(int)global.statusList.HOLD  && a.isActive == true select a).ToList();
            bsHoldingList = new BindingSource();
            dtHodling = Util.ToDataTable(docList);
            dvHodling = new DataView(dtHodling);
            bsHoldingList.DataSource = dvHodling;
            dtgHoldingList.DataSource = bsHoldingList;


            this.dtgHoldingList.ReadOnly = true;
            this.dtgHoldingList.AllowUserToAddRows = false;
        }




        private void dtgHoldingList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgHoldingList.CurrentCell.RowIndex >= 0)
            {

                if (e.RowIndex >= 0)
                {
                    if (dtgHoldingList.Columns[e.ColumnIndex].HeaderText.ToString().ToUpper() == "SELECT")
                    {

                        //};
                        int docId = (int)dtgHoldingList.Rows[e.RowIndex].Cells["documentId"].Value;
                        // List<Document> myList = masterDocument.List;
                        // myDocHeader = myList.ToList().Where(w => w.documentId == docId).FirstOrDefault();
                        myDocHeader = masterDocument.List.Where(w=> w.documentId ==  docId).FirstOrDefault();
                        myDocLine = masterDocument.getLineList(docId).ToList();
                        isSelected = true;
                        this.Dispose();
                    }
                }
            }
        }

 
    }
}
