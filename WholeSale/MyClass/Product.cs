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
    public partial class Product
    {
       
        public int qty { get; set; }
        public string unitName { get; set; }
        public string typeName { get; set; }
        public string categoryName { get; set; }

       


            
            public mainResult myResult  { get; set; }


}

    static class masterProduct{
     public   static List<Product> List = new List<Product>();
        public static void getdataMaster() {
            //  List<Product> myPrd = new List<Product>();
            ynd myEn = new ynd();
            List = (from a in myEn.Products select a).ToList();
            Log.print("load new master product");
         //   return myPrd;
        }
    }



}

