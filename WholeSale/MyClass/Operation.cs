using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeSale.MyClass;
using WholeSale;
using System.ComponentModel;
using System.Data.Entity;

namespace WholeSale
{

    public class Operation
    {

        #region "method"

        public static void prepareMasterData()
        {


        }//load all new master data
        public static void loadProduct() {

            masterProduct.getdataMaster();

        }// load master product

        public static mainResult holdNewBill(  Bill myBill )//Document docHeader, List<DocumentLine> docuemntLineList)
        {
            //===== status defualt  = hodl ==============
            myBill. docHeader.status = (int)global.statusList.HOLD;
            var docResult = db.insertDocHeader(myBill.docHeader);
            var docLineResult = db.insertDocline(myBill.docLine, myBill. docHeader.documentId);

            mainResult rs = new mainResult() { isComplete = true, status = "OK", message = "บันทึกรายการสำเร็จ" };

                return rs;
        }

        public static mainResult updateHoldBill(Bill myBill) {

            mainResult rs = db.updateBill(myBill);
            return rs;
        }



        public static void loadDocument() {

            masterDocument.getdata();

        }
        public static void loadAddress() {
            // master.getdatemaster();

        }//load master address
        public static void loadCustomer() {
            masterCustomer.getdataMaster();

        }// load master customer
        public static void loadCategoly()
        {

            masterCategory.getdataMaster();

        }// load categoly 
        public static void loadProducType()
        {

            masterType.getdataMaster();

        }// load type 
        //  public static Bill  openBill() {return  new Bill(); }// open new Bill
        public static void renewDataDocumentHeader() { }//clear data document Header
        public static void renewDataDocumentLine() { }//clear data document line

        public static DocumentDisplay setSummaryData(DocumentDisplay documentHeader ,List<DocumentLineDisplay> docLine,// Customer customer, 
            decimal discount) {

        //    int customerId = customer.customerId;
            decimal qty = docLine.Sum(s => s.qty);
            decimal totalPriceLineBeforeDiscount = docLine.Sum(s => s.totalPriceBeforeDiscount);
            decimal totalDcLine = docLine.Sum(s => s.totalDiscount);
            decimal totalPriceAfterDiscountLine = totalPriceLineBeforeDiscount - totalDcLine;
            decimal endDc = discount;
            decimal totalDc = totalDcLine + endDc;
            decimal totalPriceBeforeDiscount = totalPriceLineBeforeDiscount;
            decimal totalPriceAfterDiscount = totalPriceBeforeDiscount - totalDc;// final price
            decimal totalPriceBeforeVat = Math.Round((totalPriceAfterDiscount - (totalPriceAfterDiscount * 7 / 107)), 2);
            decimal totalVat = totalPriceAfterDiscount - totalPriceBeforeVat;
            
             documentHeader. branchCode = global.BranchCode;
             documentHeader. compCode = global.compCode;
             documentHeader. createBy = global.username;
             documentHeader. qty = qty;
             documentHeader. totalVat = totalVat;
             documentHeader. totalPriceBeforeDiscount = totalPriceBeforeDiscount;
             documentHeader. totalLineDiscount = totalDcLine;
             documentHeader. endDiscount = endDc;
             documentHeader. totalDiscount = totalDc;
             documentHeader. totalPriceAfterDiscountLine = totalPriceAfterDiscount;
             documentHeader. totalPriceAfterAllDiscount = totalPriceAfterDiscountLine;
             documentHeader. totalPriceBeforeVat = totalPriceBeforeVat;
             documentHeader. createTime = DateTime.UtcNow;
            // documentHeader. customerId = customerId;
             //documentHeader. documentId = 0;
             documentHeader. DocumentLines = null;
             documentHeader. documentNo = "";
             documentHeader. editBy = "";
             documentHeader. editTime = DateTime.UtcNow;
             documentHeader. isActive = true;
             documentHeader. isOrder = true;
             documentHeader. isTax = true;
             documentHeader. orderId = 1;
             documentHeader. paidType = 0;
             documentHeader. remark = "";
             //documentHeader.  status = 0;
            documentHeader.vendorId = 1;
          

            return documentHeader;





        }


        public static void setUiGrid(DocumentLine docList)
        {

        }
        public static void setUiSummary(DocumentHeader docHeader)
        {
        }
        public static void setDefulatUIheader() { }


        // set qty before scan or select from master product.
        public static void setQty(int _qty)
        {
            int qty = _qty;
        }

        // // add data by scan ======================//
        //public static void  scan(string prodCode)
        // {

        // } // scan prd
        public static Product filterProduct() { Product prdFilter = new Product(); return prdFilter; }// filter prd
        public static List<DocumentLineDisplay> addList(List<DocumentLineDisplay> list, Product MyProd)
        {

            list.Add(setProdToDocline(MyProd));


            return list;


        }

        public static List<DocumentLineDisplay> removeList(List<DocumentLineDisplay> list, int prdId) {


            var myDelete = list.Where(w => w.productId == prdId).ToList();


            if (myDelete.Count() > 0)
            {
                foreach (var item in myDelete)
                {
                    list.Remove(item);
                }

            }

            return list;
        }
        public static DocumentHeader updateDocHeader()
        {
            DocumentHeader myUpdate = new DocumentHeader();


            return myUpdate;


        }


        public static DocumentLineDisplay setProdToDocline(Product myProd) {
            DocumentLineDisplay newDocLine = new DocumentLineDisplay()
            {




                createTime = DateTime.Now,
                editTime = DateTime.Now,
                productId = myProd.productId,
                DocumentNo = "",
                invoiceNo = "",
                productName = myProd.productName,
                qty = myProd.qty,
                unit = "PC",

                unitPrice = myProd.price,
                unitVat = 0,
                discountUnit = 0,
                totalDiscount = 0,
                totalPriceBeforeDiscount = myProd.price *myProd.qty,
                totalPriceAfterDiscount = myProd.price * myProd.qty,
                totalVat = 0,
                totalPriceBeforeVat = myProd.price * myProd.qty,

                compCode = global.compCode,
                branchCode = global.BranchCode,
                createBy = global.username,
                editBy = global.username,

                isActive = true
            };

            return newDocLine;

        }

        // add data by search data from master product form 
        public static void openFormMasterProduct() { }
        public static Product selectProduct() {
            Product prdSelect = new Product();
            return prdSelect;
        }

        // add on prd to list 
        public static DocumentLineDisplay filterDocLineWithPrd() {
            DocumentLineDisplay docLineFilter = new DocumentLineDisplay(); return docLineFilter;
        }
        public static List<DocumentLineDisplay> updateDocline(List<DocumentLineDisplay> list, Product prd)
        {
            var myFilterDocList = list.Where(w => w.productId == prd.productId).Count();
            if (myFilterDocList > 0) {

                foreach (DocumentLineDisplay us in list.Where(u => u.productId == prd.productId))
                {
                    us.qty += prd.qty;
                    us.totalPriceBeforeVat = (us.qty) * prd.price;
                };


            }

            return list; }

        // edit docline
        public static DocumentLineDisplay filterDocumentLine(Product prdFilter) { DocumentLineDisplay doclineFilter = new DocumentLineDisplay(); return doclineFilter; }


        //calulate other eg. vat 
        public static DocumentLineDisplay calculateDoclineVat(DocumentLineDisplay docLine) { return docLine; }
       // public static DocumentHeader calculateDocHeaderVat(DocumentHeader docHeader) { return docHeader; }

        public static mainResult pay(Bill bill) {


            if (Util.checkKeyIsAvailabel(bill.docHeader, "documentid").isComplete)
            {

                var resDisable = db.disableDocuments(bill.docHeader);

                if (!resDisable.isComplete) {
                  
                    return resDisable;
                }
            }
            //if document from hold bill.


            DocumentDisplay docInsert = new DocumentDisplay();
             docInsert = db.insertDocHeader(bill.docHeader);

        

            if (docInsert.myResult.isComplete)
            {

              var rs =
                 db.insertDocline(bill.docLine, docInsert.documentId);
                if (rs.Item1.isComplete)
                {
                    var rsInsertAr = db.insertPaymentAr(bill, docInsert.documentId);
                    if (rsInsertAr.isComplete)
                    {
                        var rsUpdateBalance = db.updateBalance(rs.Item2);
                        if (rsUpdateBalance.isComplete)
                        {
                            var rsInsertTR = db.insertTransaction(rs.Item2);
                            if (rsInsertTR.isComplete) {


                                //pay from ge hold bill
                                if (bill.docHeader.documentId != null)
                                {
                                    if (bill.docHeader.documentId != 0)
                                    {
                                        db.disableDocumentHeaders(bill.docHeader.documentId);

                                        foreach (var a in bill.docLine) {
                                            db.disableDocumentLine(a.DocumentLineId);
                                        }

                                    }
                                }



                                mainResult result = new mainResult() { isComplete = true, message = "บันทึกสำเร็จ", status = "OK" };
                                return result;

                            }
                            else {
                                mainResult result = new mainResult() { isComplete = false, message = "บันทึกไม่สำเร็จ", status = "Error" };
                                return result;
                            }

                        }
                        else
                        {
                            mainResult result = new mainResult() { isComplete = false, message = "บันทึกไม่สำเร็จ", status = "Error" };
                            return result;
                        }
                    }
                    else
                    {
                        mainResult result = new mainResult() { isComplete = false, message = "บันทึกไม่สำเร็จ", status = "Error" };
                        return result;
                    }

                }
                else {
                    mainResult result = new mainResult() { isComplete = false, message = "บันทึกไม่สำเร็จ", status = "Error" };
                    return result;
                }
            }
            else {

                mainResult result = new mainResult() { isComplete = false, message = "บันทึกไม่สำเร็จ", status = "Error" };
                return result;
            }





        }
        //public static void holdBill() { }

        //public static void loadBill() { }

        public static List<DocumentLineDisplay> setProductDetailToDoclineList(List<DocumentLineDisplay>  docLineList ){

            foreach (var a  in docLineList) {

                a.productName = masterProduct.List.Where(w => w.productId == a.productId).FirstOrDefault().productName.ToString();
                a.productCode = masterProduct.List.Where(w => w.productId == a.productId).FirstOrDefault().productCode.ToString();


            }
            return docLineList;

        }

        public static DocumentDisplay setCustIdToDocument(DocumentDisplay myDocHeader , int custId) {
            myDocHeader.customerId = custId;

            return myDocHeader;
        }




        
        #endregion


    }
}
