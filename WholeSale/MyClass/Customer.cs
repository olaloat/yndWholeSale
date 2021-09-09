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
    public partial class Customer
    {
    }


    static class masterCustomer
    {
        public static List<Customer> List = new List<Customer>();

    


        public static void getdataMaster()
        {
            //  List<Product> myPrd = new List<Product>();

            List = (from a in  global.yndInven.Customers select a).ToList();
            Log.print("load new master customer");




            //   return myPrd;


        }

        public static Customer getByID(int id) {
            return  List.ToList().Where(w => w.customerId == id).FirstOrDefault();
        }

  

    }

}
