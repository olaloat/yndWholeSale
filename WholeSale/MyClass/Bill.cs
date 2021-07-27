using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeSale.MyClass;
using WholeSale;
using System.Windows.Forms;

namespace WholeSale
{
   public class Bill
    {
        public DocumentDisplay docHeader  // property
        { get; set; }
        public List< DocumentLineDisplay> docLine  // property
        { get; set; }

      public  Customer Cutomer// property
        { get; set; }

        public Vendor vendor  // property
        { get; set; }
        public decimal payIn { get; set; }
        public decimal change { get; set; }
        public decimal pending { get; set; }

         
        public Bill() {
            docHeader = new DocumentDisplay();
            docLine = new List<DocumentLineDisplay>();
            Cutomer = new Customer();
            vendor = new Vendor();
            payIn = 0;
                change = 0;
            pending = 0;
        }

        //public static List<Bill> getListHoldingBill() {

        //    List<Bill> myBillList = new List<Bill>();

        //    myBillList. = (from a in global.yndInven.DocumentLines where a.DocumentId == documentId select a).ToList();


        //    lineList = new List<DocumentLine>();

        //    lineList = Operation.setProductDetailToDoclineList(lineList);
        //    return lineList;





        //    return myBillList;
        //}


        public void print() {


            FlexCell.Grid grdBill = new FlexCell.Grid();

            grdBill.OpenFile(Application.StartupPath.ToString() + "/bill.flx");


        }
        
    }
}
