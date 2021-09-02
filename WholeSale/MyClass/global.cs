using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeSale.MyClass


{
    public class option {

        public enum mode { SELECT, EDIT, NEW }

    }


    static class global
    {
        public static ynd yndInven;
        public static int defultQty = 1;


        public static string compCode { get; set; }

        public static string plantCode { get; set; }

        public static string BranchCode { get; set; }

        public static string username { get; set; }

        public enum optionType { ok, okCancel, yseNoOk, holding, openHolding }

        //public enum mode {SELECT , EDIT , NEW  }
        // public static modeList mode;

        public enum statusList { NONE, PAY, HOLD, DELETE, CLOSE }
        public static statusList status;
        public static List<Product> mstProduct = new List<Product>();


        //public enum modeEdit
        //{
        //    NEW,
        //    EDIT
        //}








        public enum ButtonSelect { ok, yes, no, cancel, close }

        static global() {
            Log.print("reload global data");
            yndInven = new ynd();
            //   masterProduct.getdatemaster();


        }

        public static void setParamGlobal() {
            Log.print("========  set param global ========");
            Log.print("======== this is method " +
                System.Reflection.MethodBase.GetCurrentMethod().Name);
            // get current method name 


            defultQty = 1;
            compCode = "";
            plantCode = "";
            BranchCode = "";
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }




        public static bool checkDisableKey(string columnName)
        {

            bool isDisable = false;
            if (columnName.Substring(columnName.Length - 2, 2) == "Id")
            {
                isDisable = true;
                return isDisable;
            }
            if (columnName.ToUpper() == "CREATEBY" ||
                    columnName.ToUpper() == "CREATETIME" ||
                columnName.ToUpper() == "EDITBY" ||
                columnName.ToUpper() == "EDITTIME"

                    )
            {
                isDisable = true;
                return isDisable;
            }

            return isDisable;

        }





    }

    class result {
        public result()
        {
            bool isComplete = false;
            string message = "";
            string detail = "";


        }


    }

    static class Log {

        public static void print(String msg) {
            Console.WriteLine("====================" + msg + "=============================");

        }

    }


    public class mainResult
    {

        public string message = "";
        public string status = "";
        public bool isComplete = false;

    }

    public class prodResultSaveDB  :mainResult{
        public int ProdID;
    }


    public static class payment
    {
        public static decimal totalPriceAfterDiscount = 0; // ยอด final ที่ต้องจ่าย
        public static decimal change = 0; // เงินทอน
        public static decimal pay = 0; // ยอดที่ต้องจ่าย
        public static decimal payIn = 0; // จำนวนที่จ่าย
        public static decimal overdue = 0; // ค้างจ่าย
        public static Boolean isPrint = true;
        public static Boolean isComplete = false;
        public static bool inp1000 = false;
        public static bool inp100 = false;
        public static bool inp500 = false;



        //public static decimal totalDiscountInline = 0;
        //public static decimal dicountBill = 0;
        //public static decimal totalNetPay = 0;
        //public static decimal totalDiscount = 0;

        public static void clearbt()
        {
            inp1000 = false;
            inp100 = false;
            inp500 = false;

        }
        public static void clear()
        {
            totalPriceAfterDiscount = 0;
            change = 0;
            pay = 0;
            payIn = 0;
            overdue = 0;
            isPrint = true;
            isComplete = false;
            inp1000 = false;
            inp100 = false;
            inp500 = false;


        }



    }

    public static class customerInfo
    {

        public static string customerName = "";
        public static string customerAddress = "";
        public static bool isSelected = false;


        public static void clear()
        {
            customerName = "";
            customerAddress = "";
            isSelected = false;
        }


    }

    public static class productMaintain
    {

        public static bool haveNewProduct = false;
        public static void clear()
        {
            haveNewProduct = false;
        }

        public static void loadMaster()
        {
            ynd yndInven = new ynd();

            global.mstProduct = (from a in yndInven.Products select a).ToList();// yndInven.Products.ToList();
        }
    }


    public class customMSG
    {
        string option1 = "";
        string option2 = "";
        string option3 = "";


    }


}
