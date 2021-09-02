using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeSale;
using WholeSale.MyClass;
using static WholeSale.MyClass.global;

namespace WholeSale
{
    class db 
    {

      public  static ynd yndEn = new ynd();
        db() { yndEn = new ynd(); }
        public static mainResult UpdateDocHeader(DocumentDisplay mydoc)
        {
            mainResult rs = new mainResult();

            int docHID = 0;
            mydoc.documentId = 0;
            yndEn.SaveChanges();
            docHID = mydoc.documentId;
            rs.isComplete = true;
            rs.message = docHID.ToString();
            return rs;
        }
        /// <summary>
        /// receive document Header and return docheader data same send and return id that model return
        /// </summary>
        /// <param name="mydoc"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static DocumentDisplay insertDocHeader(DocumentDisplay mydoc)
        {
            //  mainResult rs = new mainResult();
            mydoc.myResult = null;
            mydoc.documentId = 0;
            string docNum = "";
            var lastDoc = (yndEn.Documents.Max(m => m.documentNo));
            if (lastDoc == null) { lastDoc = ""; } else { }
            int docHID = 0;
            if (lastDoc == "")
            {
                docNum = DateTime.Now.ToString("yyyyMMddHHmmss") + "0001";
            }
            else
            {
                int lasteRunning = Convert.ToInt16(lastDoc.Substring(lastDoc.Length - 4, 4));
                docNum = DateTime.Now.ToString("yyyyMMddHHmmss") + (lasteRunning + 1).ToString("000#");
            }
            mydoc.documentNo = docNum;
            Document docAdd = new Document();
            docAdd = Util.copyDataFromChildToParentModel(mydoc, docAdd);
            yndEn.Documents.Add(docAdd);
            yndEn.SaveChanges();
            mydoc.documentId = docAdd.documentId;
            mydoc.myResult = new mainResult() { isComplete =true , message ="OK" , status ="OK"};
            return mydoc;
        }

        public static mainResult insertPaymentAr(Bill myBill , int docId)
        {
            mainResult rs = new mainResult();

            AccountReceive acr = new AccountReceive()
            {
                branchCode = global.BranchCode,
                compCode = global.compCode,
                createBy = global.username,
                createTime = DateTime.Now,
                editTime = DateTime.Now,
                editBy = global.username,
                Amount = myBill.docHeader.totalPriceAfterAllDiscount,
                pay = myBill.payIn,
                change = myBill.change,
                balance = myBill.pending,
                refDocId = 0,
                docId = docId,
                isActive = true,
                status = 1,
            };
            yndEn.AccountReceives.Add(acr);
            yndEn.SaveChanges();

            rs.isComplete = true;
            rs.message = "ok";
            return rs;
        }

        public static Tuple<mainResult, List<DocumentLineDisplay>> insertDocline(List<DocumentLineDisplay> myDocline , int docHid)
        {
            mainResult rs = new mainResult();

            List<DocumentLine> docLineAdd = new List<DocumentLine>();

            //docLineAdd = Util.copyDataFromChildToParentList(myDocline , docLineAdd).ToList();
            foreach (var a in myDocline) {
                // DocumentLine aAdd = a;
                //  aAdd = (DocumentLine)a;
                DocumentLine aAdd = new DocumentLine();
                 aAdd = Util.copyDataFromChildToParentModel(a, aAdd);
               // DocumentLine aAdd = Mapper.Map<Parent>(child);
                aAdd.DocumentId = docHid;
                yndEn.DocumentLines.Add(aAdd);
                yndEn.SaveChanges();
                a.DocumentLineId = aAdd.DocumentLineId;

            }
            rs.isComplete = true;
            rs.message = "OK";
            rs.status = "OK";
            return  Tuple.Create(rs, myDocline);
        }


        public static mainResult insertTransaction(List< DocumentLineDisplay> myDocline)
        {
            mainResult rs = new mainResult();

            foreach(var a in myDocline) {


                for (int i = 1; i <= a.qty; i++)
                {
                    Transaction tr = new Transaction()
                    {
                        branchCode = global.BranchCode,
                        compCode = global.compCode,
                        createBy = global.username,
                        createTime = DateTime.Now,
                        editTime = DateTime.Now,
                        editBy = global.username,
                        movementTypeId = 1,
                        price = a.unitPrice,
                        qty = a.qty,
                        unit = a.unit,
                        productId = a.productId,
                        invoiceLineId = 0,
                        documentLineId = a.DocumentLineId,
                    };
                    yndEn.Transactions.Add(tr);
                    yndEn.SaveChanges();
                }
            }



            rs.isComplete = true;
            rs.message = "EROR";
            rs.status = "STATUS";

            return rs;
        }

        public static mainResult updateBalance(List<DocumentLineDisplay> myDocline)
        {
            mainResult rs = new mainResult();
            Balance myBalance = new Balance();
            try
            {
                foreach (var a in myDocline)
                {
                    //=============== update balcnce ====================================
                    myBalance = yndEn.Balances.Where(p => p.productId == a.productId).FirstOrDefault();
                    myBalance.qty = myBalance.qty - a.qty;
                    yndEn.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                rs.isComplete = false;
                rs.message = ex.Message.ToString();
                return rs;
            }
            rs.isComplete = true;
            rs.message = "ok";
            return rs;
        }

        

        public static mainResult updateCustomer(Customer myCutEdit)
        {
            mainResult rs = new mainResult();

            try
            {
                using (var context = new ynd())
                {

                    foreach (var myCustUpdate in context.Customers.Where(p => p.customerId == myCutEdit.customerId).ToList())
                    {
                        myCustUpdate.customerName = myCutEdit.customerName;
                              myCustUpdate.    customerLevel = myCutEdit              .customerLevel  ;
                              myCustUpdate.    contactName = myCutEdit                .contactName    ;
                              myCustUpdate.    address1 = myCutEdit                   .address1       ;
                              myCustUpdate.    address2 = myCutEdit                   .address2       ;
                              myCustUpdate.    city = myCutEdit                       .city           ;
                              myCustUpdate.    tel = myCutEdit                        .tel            ;
                              myCustUpdate.    fax = myCutEdit                        .fax            ;
                              myCustUpdate.    customerCurPnt  = myCutEdit            .customerCurPnt ;
                              myCustUpdate.    postal = myCutEdit                     .postal         ;
                        myCustUpdate.    mobile = myCutEdit                     .mobile         ;
                        myCustUpdate.    email = myCutEdit                      .email          ;
                        myCutEdit.customerTolPnt = myCutEdit.customerTolPnt;



                        context.SaveChanges();

                    }





                }

            }
            catch (Exception e)
            {

                rs = new mainResult() { isComplete = false, message = "Error :" + e.Message, status = "ERROR" };
                return rs;

            }


            rs = new mainResult() { isComplete = true, message = "ทำรายการสำเร็จ", status = "OK" };
            return rs;
        }


        public static prodResultSaveDB updateProduct(Product myProdEdit)
        {
            prodResultSaveDB rs = new prodResultSaveDB();
            int prdLineId = 0;
            try { 
            using (var context = new ynd())
            {

                foreach (var myProdUpdate in context.Products.Where(p => p.productId == myProdEdit.productId).ToList())
                {
                   myProdUpdate.productCode = myProdEdit.productCode;
                    myProdUpdate.productName = myProdEdit.productName;
                    myProdUpdate.price = myProdEdit.price;
                   myProdUpdate.maxPrice = myProdEdit.maxPrice;
                    myProdUpdate.minPrice = myProdEdit.minPrice;
                    myProdUpdate.previousPrice = 0;
                    myProdUpdate.branchCode = global.BranchCode;
                    myProdUpdate.compCode = global.compCode;
                    myProdUpdate.categoryId = myProdEdit.categoryId;
                    myProdUpdate.createBy = "ADMIN";
                   myProdUpdate.createTime = DateTime.Now;
                   myProdUpdate.editTime = DateTime.Now;
                   myProdUpdate.editBy = "ADMIN";
                    myProdUpdate.groupId = myProdEdit.groupId;
                   myProdUpdate.isActive = true;
                   myProdUpdate.typeId = myProdEdit.typeId;
                    myProdUpdate.productTypeId = myProdEdit.productTypeId;
                    myProdUpdate.unitId = myProdEdit.unitId;

                    context.SaveChanges();

                }




              
            }

            }
            catch (Exception e) {

                 rs = new prodResultSaveDB() { isComplete = false, message = "Error :" + e.Message, status = "OK" , ProdID = 0 };
                return rs;

            }


             rs = new prodResultSaveDB() { isComplete = true, message = "ทำรายการสำเร็จ", status = "OK" , ProdID = myProdEdit.productId };
            return rs;
        }
        public static mainResult updateBill(Bill billUpdated)
        {
            mainResult rs = new mainResult();
            try { 
            //==============update dochader ==============
            using (var context = new ynd())
            {

                    updateAllFeidlDocument(billUpdated.docHeader);

                //==============  disable docline ============

                    foreach (var dclDisable in context.DocumentLines.Where(p => p.DocumentId == billUpdated.docHeader.documentId).ToList())
                {
                    dclDisable.isActive = false;
                }
                context.SaveChanges();

                     var resultInsert = db.insertDocline(billUpdated.docLine, billUpdated.docHeader.documentId);

                    //  context.SaveChanges();
                }


                rs = new mainResult() { isComplete = true, message = "ทำรายการสำเร็จ", status = "OK" };
                    return rs;

            }
            catch(Exception e) {
                rs = new mainResult() { isComplete = false, message = "ทำรายการไม่สำเร็จ", status = "ERROR" };
                return rs;
            }
        }



        public static Document updateAllFeidlDocument(Document newdata) {
            ynd yndEn = new ynd();
            foreach (var x in yndEn.Documents.Where(w => w.documentId == newdata.documentId).ToList()) {
                x.documentId = newdata.documentId;
                x.documentNo = newdata.documentNo;
                x.orderId = newdata.orderId;
                x.customerId = newdata.customerId;
                x.vendorId = newdata.vendorId;
                x.isOrder = newdata.isOrder;
                x.isTax = newdata.isTax;
                x.paidType = newdata.paidType;
                x.remark = newdata.remark;
                x.status = newdata.status;
                x.isActive = newdata.isActive;
                x.createBy = newdata.createBy;
                x.createTime = newdata.createTime;
                x.editBy = newdata.editBy;
                x.editTime = newdata.editTime;
                x.compCode = newdata.compCode;
                x.branchCode = newdata.branchCode;
                x.totalVat = newdata.totalVat;
                x.totalLineDiscount = newdata.totalLineDiscount;
                x.totalPriceBeforeDiscount = newdata.totalPriceBeforeDiscount;
                x.endDiscount = newdata.endDiscount;
                x.totalDiscount = newdata.totalDiscount;
                x.totalPriceAfterDiscountLine = newdata.totalPriceAfterDiscountLine;
                x.totalPriceAfterAllDiscount = newdata.totalPriceAfterAllDiscount;
                x.totalPriceBeforeVat = newdata.totalPriceBeforeVat;
                x.qty = newdata.qty;
            }

            yndEn.SaveChanges();

            return newdata;
        }


        public static mainResult disableDocuments(int docId)
        {
            mainResult rs = new mainResult() { isComplete = false};



            try {
                #region db update
                ynd context = new ynd();
                var docDisable = (from a in yndEn.Documents where a.documentId == docId select a).FirstOrDefault();

                docDisable.isActive = false;

                context.SaveChanges();
                rs = new mainResult() { isComplete = true, message = "ทำรายการสำเร็จ", status = "OK" };
                return rs;


                #endregion
            }
            catch(Exception e) {
                rs = new mainResult() { isComplete = true, message = "ทำรายการไม่สำเร็จ :" + e.Message.ToString(), status = "OK" };
                return rs;

            }
        
            //return rs;
        }

        public static List<Employee> getUserName() {
         
            List<Employee> myEnp = new List<Employee>();
            ynd yndEn = new ynd();
            myEnp = yndEn.Employees.Where(w => w.isActive == true).ToList();
           
                return myEnp;
        }

        //select empId, username, [password], passcode, token, fName, lName from dbo.Employee where isActive = 1


           public static mainResult insertPictureProduction(   Picture myPict)
        {
            mainResult rs = new mainResult();

            try {

                ynd Myen = new ynd();


                //chek already exit picture of this product id
                var pictList = (from a in Myen.Pictures where a.isActive == true && a.productId == myPict.productId select a).ToList();
                if (pictList.Count > 0) {

                    foreach (var a in pictList) {
                        a.isActive = false;
                        Myen.SaveChanges();

                    }


                }
                 Myen = new ynd();
                Myen.Pictures.Add(myPict);
                Myen.SaveChanges();


            }
            catch (Exception ex) {
                rs.isComplete = false;
                rs.message = ex.Message.ToString();
                return rs;

            }
          
            rs.isComplete = true;
            rs.message = "ok";
            return rs;
        }


    }
}
