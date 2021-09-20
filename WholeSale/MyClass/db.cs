using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

            try
            {
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

                if (mydoc.documentNo != null)
                {

                    if (mydoc.documentNo.ToString().Trim().Length != 0)
                    {

                        mydoc.documentNo = mydoc.documentNo;
                    }
                    else
                    {
                        mydoc.documentNo = docNum;
                    }
                }
                else {

                    mydoc.documentNo = docNum;

                }


                // mydoc.documentNo = docNum;
                Document docAdd = new Document();
                docAdd = Util.copyDataFromChildToParentModel(mydoc, docAdd);
                docAdd.documentId = 0;
                if (docAdd.DocumentLines != null) { docAdd.DocumentLines = null; }
                docAdd = Util.setStadardInfo(docAdd, global.mode.NEW);
              
                yndEn.Documents.Add(docAdd);
                yndEn.SaveChanges();
                mydoc.documentId = docAdd.documentId;
                mydoc.myResult = new mainResult() { isComplete = true, message = "OK", status = "OK" };
                return mydoc;


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            //   mydoc = new mainResult() { isComplete = false, message = "Error :" + e.Message, status = "ERROR" };
                return mydoc;
            }
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
            ynd dbInsert = new ynd();
            try { 

            List<DocumentLine> docLineAdd = new List<DocumentLine>();

            //docLineAdd = Util.copyDataFromChildToParentList(myDocline , docLineAdd).ToList();
            foreach (var a in myDocline) {
                // DocumentLine aAdd = a;
                //  aAdd = (DocumentLine)a;
                DocumentLine aAdd = new DocumentLine();
                 aAdd = Util.copyDataFromChildToParentModel(a, aAdd);
                    // DocumentLine aAdd = Mapper.Map<Parent>(child);

                    if (aAdd.Document != null) { aAdd.Document = null; }
                aAdd.DocumentId = docHid;
                    aAdd.DocumentLineId = 0; // aAdd.DocumentLineId;
                    aAdd.isActive = true;
                    dbInsert = new ynd();
                    dbInsert.DocumentLines.Add(aAdd);
                    dbInsert.SaveChanges();
                    a.DocumentLineId = aAdd.DocumentLineId;

            }

            }
            catch (Exception e) {
                rs.isComplete = false;
                rs.message = e.Message.ToString();
                rs.status = "error";
                return Tuple.Create(rs, myDocline);
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

                ynd context = new ynd();
                context.Customers.Add(myCutEdit);
                context.Entry(myCutEdit).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                //using (var context = new ynd())
                //{
                   

                //    //foreach (var myCustUpdate in context.Customers.Where(p => p.customerId == myCutEdit.customerId).ToList())
                //    //{
                //    //    myCustUpdate.customerName = myCutEdit.customerName;
                //    //          myCustUpdate.    customerLevel = myCutEdit              .customerLevel  ;
                //    //          myCustUpdate.    contactName = myCutEdit                .contactName    ;
                //    //          myCustUpdate.    address1 = myCutEdit                   .address1       ;
                //    //          myCustUpdate.    address2 = myCutEdit                   .address2       ;
                //    //          myCustUpdate.    city = myCutEdit                       .city           ;
                //    //          myCustUpdate.    tel = myCutEdit                        .tel            ;
                //    //          myCustUpdate.    fax = myCutEdit                        .fax            ;
                //    //          myCustUpdate.    customerCurPnt  = myCutEdit            .customerCurPnt ;
                //    //          myCustUpdate.    postal = myCutEdit                     .postal         ;
                //    //    myCustUpdate.    mobile = myCutEdit                     .mobile         ;
                //    //    myCustUpdate.    email = myCutEdit                      .email          ;
                //    //    myCutEdit.customerTolPnt = myCutEdit.customerTolPnt;



                //      //  context.SaveChanges();

                //  //  }





                //}

            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                rs = new mainResult() { isComplete = false, message = "Error :" + e.Message, status = "ERROR" };
                    return rs;
            }
            //catch (Exception e)
            //{

            //    rs = new mainResult() { isComplete = false, message = "Error :" + e.Message, status = "ERROR" };
            //    return rs;

            //}


            rs = new mainResult() { isComplete = true, message = "ทำรายการสำเร็จ", status = "OK" };
            return rs;
        }


        public static prodResultSaveDB updateProduct(Product myProdEdit )
        {
            prodResultSaveDB rs = new prodResultSaveDB();
            int prdLineId = 0;

            Product myProdUpdate = new Product();
          //  try { 
            using (var context = new ynd())
            {

                ////foreach (var myProdUpdate in context.Products.Where(p => p.productId == myProdEdit.productId).ToList())
                ////{
                //myProdUpdate = Util.setStadardInfo(myProdUpdate , global.mode.EDIT);

                //myProdUpdate.productId = myProdEdit.productId;
                //myProdUpdate.productCode = myProdEdit.productCode;
                //myProdUpdate.productName = myProdEdit.productName;
                //myProdUpdate.price = myProdEdit.price;
                //myProdUpdate.maxPrice = myProdEdit.maxPrice;
                //myProdUpdate.minPrice = myProdEdit.minPrice;
                //myProdUpdate.previousPrice = myProdEdit.previousPrice;
                ////myProdUpdate.branchCode = global.BranchCode;
                ////myProdUpdate.compCode = global.compCode;
                //myProdUpdate.categoryId = myProdEdit.categoryId;
                //myProdUpdate.createBy = myProdEdit.createBy;
                //myProdUpdate.createTime = myProdEdit.createTime;
                ////myProdUpdate.editTime = DateTime.Now;
                ////myProdUpdate.editBy = "ADMIN";
                //myProdUpdate.groupId = myProdEdit.groupId;
                //myProdUpdate.isActive = myProdEdit.isActive;
                //myProdUpdate.typeId = myProdEdit.typeId;
                //myProdUpdate.productTypeId = myProdEdit.productTypeId;
                //myProdUpdate.unitId = myProdEdit.unitId;

                ////    context.SaveChanges();

                ////}

                ////  myProdEdit


                      context.Products.Add(myProdEdit);
                    context.Entry(myProdEdit).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();





            }

            //}
            //catch (Exception e) {

            //     rs = new prodResultSaveDB() { isComplete = false, message = "Error :" + e.Message, status = "OK" , ProdID = 0 };
            //    return rs;

            //}


             rs = new prodResultSaveDB() { isComplete = true, message = "ทำรายการสำเร็จ", status = "OK" , ProdID = myProdEdit.productId };
            return rs;
        }
        public static mainResult updateBill(Bill billUpdated)
        {
            mainResult rs = new mainResult();
            try {
                //==============update dochader ==============
                //using (var context = new ynd())
                //{

                var context = new ynd();

                    updateAllFeidlDocument(billUpdated.docHeader);

                //==============  disable docline ============

                    foreach (var dclDisable in context.DocumentLines.Where(p => p.DocumentId == billUpdated.docHeader.documentId).ToList())
                {
                    dclDisable.isActive = false;
                }
                context.SaveChanges();
                context = new ynd();


                     var resultInsert = db.insertDocline(billUpdated.docLine, billUpdated.docHeader.documentId);

                //  context.SaveChanges();
                //  }
                if (!resultInsert.Item1.isComplete) {
                    rs = new mainResult() { isComplete = false, message = resultInsert.Item1.message, status = "OK" };
                    return rs;
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
         

            using (var db = new ynd())
            {

                Document docUpdate = (from a in db.Documents where a.documentId == newdata.documentId select a).FirstOrDefault();

                docUpdate.documentId = newdata.documentId;
                docUpdate.documentNo = newdata.documentNo;
                docUpdate.orderId = newdata.orderId;
                docUpdate.customerId = newdata.customerId;
                docUpdate.vendorId = newdata.vendorId;
                docUpdate.isOrder = newdata.isOrder;
                docUpdate.isTax = newdata.isTax;
                docUpdate.paidType = newdata.paidType;
                docUpdate.remark = newdata.remark;
                docUpdate.status = newdata.status;
                docUpdate.isActive = newdata.isActive;
                docUpdate.createBy = newdata.createBy;
                docUpdate.createTime = newdata.createTime;
                docUpdate.editBy = global.username;
                docUpdate.editTime = DateTime.Now;
                docUpdate.compCode = global.compCode;
                docUpdate.branchCode = global.plantCode;
                docUpdate.totalVat = newdata.totalVat;
                docUpdate.totalLineDiscount = newdata.totalLineDiscount;
                docUpdate.totalPriceBeforeDiscount = newdata.totalPriceBeforeDiscount;
                docUpdate.endDiscount = newdata.endDiscount;
                docUpdate.totalDiscount = newdata.totalDiscount;
                docUpdate.totalPriceAfterDiscountLine = newdata.totalPriceAfterDiscountLine;
                docUpdate.totalPriceAfterAllDiscount = newdata.totalPriceAfterAllDiscount;
                docUpdate.totalPriceBeforeVat = newdata.totalPriceBeforeVat;
                docUpdate.qty = newdata.qty;
               db.Documents.Attach(docUpdate);


                db.Documents.Add(docUpdate);
                db.Entry(docUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                // db.Documents.Add(docUpdate);
                //  db.Entry(docDisable).Property(x => x.isActive).IsModified = true;
               // db.SaveChanges();

            }
          
         
        

            return newdata;
        }




        public static mainResult  updateStatus<T>(T model ,global.statusList status ) where T : class
        {
            mainResult rs = new mainResult();
            ynd context = new ynd();
            PropertyDescriptorCollection properties =
             TypeDescriptor.GetProperties(typeof(T));

            try {



        
           


            foreach (PropertyDescriptor prop in properties)
            {

                if (prop.Name.ToString().ToUpper() == "STATUS") {
                    int x = (int)status;


                    prop.SetValue(model,(int)status);
                }
          

            }

            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();

            }
            catch (Exception e)
            {
                rs = new mainResult() { isComplete = false, message = "update status fail.", status = "fail" };

            }
            rs = new mainResult() { isComplete = true, message = "update status successfully.", status = "OK" };

            return rs;
        }





        public static mainResult disableDocumentLine(int docLineId)
        {
            mainResult rs = new mainResult() { isComplete = false };



            try
            {
                #region db update

                if (docLineId == 0) {
                    rs = new mainResult() { isComplete = true, message = "OK" };
return rs; }
              
                using (var db = new ynd())
                {

                    var docDisable = (from a in db.DocumentLines where a.DocumentLineId == docLineId select a).FirstOrDefault();

                    docDisable.isActive = false;
                    db.DocumentLines.Attach(docDisable);
                    db.Entry(docDisable).Property(x => x.isActive).IsModified = true;
                    db.SaveChanges();
                }
                rs = new mainResult() { isComplete = true, message = "ทำรายการสำเร็จ", status = "OK" };
                return rs;


                #endregion
            }
            catch (Exception e)
            {
                rs = new mainResult() { isComplete = true, message = "ทำรายการไม่สำเร็จ :" + e.Message.ToString(), status = "OK" };
                return rs;

            }

            //return rs;
        }


        public static mainResult disableDocuments(Bill doc) {


            mainResult rs = new mainResult() { isComplete = false };


            try {
                rs = disableDocumentHeaders(doc.docHeader.documentId);
                if (rs.isComplete)
                {
                    foreach (var a in doc.docLine)
                    {

                        rs = disableDocumentLine(a.DocumentLineId);
                    }

                }


            }
            catch (Exception e) {
                rs = new mainResult() { isComplete = true, message = e.Message.ToString() };
                return rs;
            }
        

            rs = new mainResult() { isComplete = true };
            return rs;
        }

        public static mainResult disableDocumentHeaders(int docId)
        {
            mainResult rs = new mainResult() { isComplete = false};



            try {
                #region db update
               // ynd context = new ynd();
           
                using (var db = new ynd())
                {

                    var docDisable = (from a in db.Documents where a.documentId == docId select a).FirstOrDefault();

                    docDisable.isActive = false;
                    db.Documents.Attach(docDisable);
                    db.Entry(docDisable).Property(x => x.isActive).IsModified = true;
                    db.SaveChanges();
                }
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
