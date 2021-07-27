


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
    public partial class Category { 


        public mainResult myResult { get; set; }
    }

    static class masterCategory
    {
        public static List<Category> List = new List<Category>();

        static masterCategory()
        {
        }


        public static void getdataMaster()
        {
            List = (from a in global.yndInven.Categories select a).ToList();
            Log.print("load new master category");
        }
    }
}


