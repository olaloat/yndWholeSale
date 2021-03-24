using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeSale
{
    static class Global
    {

        public  enum statusList { PAY, HOLD, DELETE , CLOSE }
        public static string compCode = "yndk";
        public static string branchCode = "0001";
        public static string username = "nattawut";
        public static statusList status;

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



    }



    public class mainResult
    {

        public string message = "";
        public string status = "";
        public bool isComplete = false;

    }


    public static class payment {
        public static decimal totalAmount = 0;
        public static decimal income = 0;
        public static decimal change = 0;

        public static decimal overdue = 0;
        public static Boolean isPrint = true;
        public static Boolean isComplete = false;

        public static bool inp1000 = false;
        public static bool inp100 = false;
        public static bool inp500 = false;


      
        public static decimal totalDiscountInline = 0;
        public static decimal dicountBill = 0;
        public static decimal totalNetPay = 0;
        public static decimal totalDiscount = 0;

        public static void clearbt() {
            inp1000 = false;
            inp100 = false;
            inp500 = false;

        }
        public static void clear() {
            totalAmount = 0;
            income = 0;
            change = 0;

            overdue = 0;
            isPrint = true;
            isComplete = false;
            inp1000 = false;
            inp100 = false;
            inp500 = false;


        }



    }

    public static class customerInfo {

        public static string customerName = "";
        public static string customerAddress = "";
        public static bool isSelected = false;


        public static void clear() {
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
    }
    public static class util {
        public static FlexCell.Grid autoFit(FlexCell.Grid grd) {

            for (int i = 0; i <= grd.Cols - 1; i++) {

                grd.Column(i).AutoFit();

            }


            return grd;
        }


        public static FlexCell.Grid chnageGridColumnName(FlexCell.Grid grd, DataTable dt, string oldName, string newName) {

            grd.Cell(0, (dt.Columns[oldName].Ordinal + 1)).Text = newName;
            return grd;
        }



    }


    public class customMSG{
    string option1 = "";
    string option2 = "";
    string option3 = "";


}

}



