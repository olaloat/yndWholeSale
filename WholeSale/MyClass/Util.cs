using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WholeSale.MyClass
{
    static class Util
    {

        public static FlexCell.Grid autoFit(FlexCell.Grid grd)
        {

            for (int i = 0; i <= grd.Cols - 1; i++)
            {

                grd.Column(i).AutoFit();

            }


            return grd;
        }


        public static FlexCell.Grid chnageGridColumnName(FlexCell.Grid grd, DataTable dt, string oldName, string newName)
        {

            grd.Cell(0, (dt.Columns[oldName].Ordinal + 1)).Text = newName;
            return grd;
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


        public static List<T2> copyDataFromChildToParentList<T1, T2>(this List<T1> data, T2 target) where T2 : new()
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T2));
            DataTable table = new DataTable();
            //foreach (PropertyDescriptor prop in properties)
            //    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            List<T2> dataOutput = new List<T2>();
            foreach (T1 item in data)
            {
                T2 newItem = new T2();



                foreach (PropertyDescriptor prop in properties)
                {
                    prop.SetValue(newItem, prop.GetValue(item) ?? DBNull.Value);

                }
                dataOutput.Add(newItem);


            }
            //    IList<T> docLine = new IList<T>();
            return dataOutput;
        }


        public static T2 copyDataFromChildToParentModel<T1, T2>(this T1 data, T2 target) where T2 : new()
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T2));

            T2 dataOutput = new T2();

            foreach (PropertyDescriptor prop in properties)
            {

                //if (prop.Name.ToString().ToUpper() == "")
                //{

                //    var xxx = 0000;
                //}
                try {


                    prop.SetValue(dataOutput, prop.GetValue(data) ?? DBNull.Value);


                } catch { }

                //   data)!=DBNull.Value) {


            }
            return dataOutput;
        }


        public static mainResult checkKeyIsAvailabel<T>(this T data , string KeyName ) where T : new()
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            mainResult res = new mainResult();

            foreach (PropertyDescriptor prop in properties)
            {
                try {
                    if (prop.Name.ToString().ToUpper() == KeyName.ToString().ToUpper()) {


                        if (prop.GetValue(data) != null) {

                            if ((int)prop.GetValue(data) != 0)
                            {

                                res = new mainResult() { isComplete = true, status = "OK", message = "is available." };
                            }
                        }
                    }
                }
                catch(Exception e){

                    res = new mainResult() { isComplete = false, status = "error", message = "not available." };
                }
                
            }
            return res;
        }


        public static T setStadardInfo<T>(T model  , global.mode mode = global.mode.NEW) where T : new()
        {

            foreach (var property in typeof(T).GetProperties())
            {


                if (global.mode.NEW == mode) {
                    if (property.Name.ToUpper() == "CREATETIME") // setting value for x
                    {
                        property.SetValue(model, DateTime.Now);
                    }
                    if (property.Name.ToUpper() == "CREATEBY") // setting value for x
                    {
                        property.SetValue(model, global.username);
                    }
                }
              
                if (property.Name.ToUpper() == "EDITTIME") // setting value for x
                {
                    property.SetValue(model, DateTime.Now);
                }


              
                if (property.Name.ToUpper() == "EDITBY") // setting value for x
                {
                    property.SetValue(model, global.username);
                }


                if (property.Name.ToUpper() == "COMPCODE") // setting value for x
                {
                    property.SetValue(model, global.compCode);
                }

                if (property.Name.ToUpper() == "BRANCHCODE") // setting value for x
                {
                    property.SetValue(model, global.BranchCode);
                }

                if (property.Name.ToUpper() == "ISACTIVE") // setting value for x
                {
                    property.SetValue(model, true);
                }
            }


            return model;
        }




        public static bool validateNumber(KeyPressEventArgs e)
        {
            //private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
            //{
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ' ') //The  character represents a backspace
            {
                return false; //Do not reject the input
            }
            else
            {
                return true; //Reject the input
            }

            return true;
            //   }
        }



        public static byte[] convertImageToByte(PictureBox pict)
        {

            System.IO.MemoryStream SndImageStream = new System.IO.MemoryStream();
            pict.Image.Save(SndImageStream, System.Drawing.Imaging.ImageFormat.Bmp);

        //    pict.image.Save(SndImageStream, pict.BackgroundImage.RawFormat);
            byte[] data = SndImageStream.GetBuffer();

            return data;

        }

    }




    static class mMsgBox{


        public static DialogResult show(string msg, Forms.Modal_MsgBox.icon icon = Forms.Modal_MsgBox.icon.information,string title ="Information")
        {

            using (Forms.Modal_MsgBox fb = new Forms.Modal_MsgBox())
            {
                fb.show(msg, title,icon);
                fb.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                fb.ShowDialog();
                return fb.result;
            }

        }

        public static DialogResult show(string msg, Forms.Modal_MsgBox.MessageBoxButtons button, Forms.Modal_MsgBox.icon icon = Forms.Modal_MsgBox.icon.information, string title = "Information")
        {
            using (Forms.Modal_MsgBox fb = new Forms.Modal_MsgBox())
            {
                fb.show(msg, title, button,icon);
                fb.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                fb.ShowDialog();
                return fb.result;
            }
        }




    }






}
