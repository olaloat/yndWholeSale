using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeSale.MyClass;

namespace WholeSale
{
    public  class DocumentDisplay :Document
    {

        public string customerName { get; set; }
        public string vendorName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }

        public string city { get; set; }

        public string postalCode { get; set; }

        public string tel { get; set; }

        public string mobile { get; set; }

        public string fax { get; set; }

        public string email { get; set; }
        public string contactName { get; set; }
        public mainResult myResult { get; set; }
    }


    public  class DocumentLineDisplay : DocumentLine
    {

        public string productCode { get; set; }

        public mainResult myResult { get; set; }
    }


    static class masterDocument
    {
        public static List<DocumentDisplay> List = new List<DocumentDisplay>();
        public static List<DocumentLineDisplay> lineList = new List<DocumentLineDisplay>();
        public static void getdata()
        {
           var ListDocDB = (from a in global.yndInven.Documents where
                     a.status == (int)global.statusList.HOLD && a.isActive == true
                    select a).ToList();

            List = new List<DocumentDisplay>();
            foreach (var db in ListDocDB)
            {
                var a = new DocumentDisplay();
                var myCust = masterCustomer.List.Where(w => w.customerId == a.customerId).FirstOrDefault();
                if (myCust != null) {
                a = (DocumentDisplay)db;
                a.customerName = myCust.customerName;
                a.address1 = myCust.address1;
                a.address2 = myCust.address2;
                a.city = myCust.city;
                a.tel = myCust.tel;
                a.mobile = myCust.mobile;
                a.fax = myCust.fax;
                a.email = myCust.email;
                a.contactName = myCust.contactName;
            }
                List.Add(a);

            }


            Log.print("load new master document");
        }

        public static List<DocumentLineDisplay> getLineList(int documentId) {
            lineList = new List<DocumentLineDisplay>();

            List<DocumentLine> lineListDB = (from a in global.yndInven.DocumentLines where a.DocumentId == documentId select a).ToList();
            lineList = lineListDB.OfType<DocumentLineDisplay>().ToList();// convert parent to child class
            lineList = Operation.setProductDetailToDoclineList(lineList);
            return lineList;

        }
    }


     class test {

        public string x1 { get; set; }
        public string x2 { get; set; }
    }

     class test2 : test
    {

        public string x3 { get; set; }

    }


    static class  testrun {

        public static void run() {

            test2 xxx = new test2() { x1 = "1", x2 = "2", x3 = "3" };
            test ddd = new test2() { x1 = "1", x2 = "2", x3 = "3" };
            List<test> listTest = new List<test>();
            listTest.Add(ddd);
        }


    }
}
