



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
    public partial class ProductType
    {


        public mainResult myResult { get; set; }
    }

    static class masterType
    {
        public static List<ProductType> List = new List<ProductType>();

        static masterType()
        {
        }


        public static void getdataMaster()
        {
            List = (from a in global.yndInven.ProductTypes select a).ToList();
            Log.print("load new master type");
        }
    }
}



