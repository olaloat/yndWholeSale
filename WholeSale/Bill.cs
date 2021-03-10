using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing;
using System.Configuration;

namespace WholeSale
{
    class Bill
    {
        //public Bill()
        //{
        //    totalQty = 0;
        //    totalAmountBeforeVat = 0;
        //    totalAmountIncludeVat = 0;
        //    totalVat = 0;
        //    //  Mylist = new List<DocumentLine>();
        //    //  MySummary = new List<Document>();
        //    editBy = "";
        //    MyBill = new Bill();

        //}
        private static List<Stream> m_streams;
        private static int m_currentPageIndex = 0;


        public int totalQty = 0;
        public decimal totalAmountBeforeVat = 0;
        public decimal totalAmountIncludeVat = 0;
        public decimal totalVat = 0;
        public decimal discount = 0;
        // private List<DocumentLine> Mylist = new List<DocumentLine>();
        // private List<Document> MySummary = new List<Document>();
        //   public Bill MyBill ;
        public string editBy = "";
        public List<DocumentLine> list = new List<DocumentLine>();

        public List<DocumentLine> setList(Product MyProd, int qty)
        {
            var checkCount = list.Where(w => w.productId == MyProd.productId).Count();
            if (checkCount > 0)  // check  case  have same product in list
            {
                foreach (DocumentLine us in list.Where(u => u.productId == MyProd.productId))
                {
                    us.qty += qty;
                    us.amount = (us.qty) * MyProd.price;
                };
            }
            else  // check  case  have no same product in list
            {
                DocumentLine MyTemp = new DocumentLine()
                {
                    branchCode = Global.branchCode,
                    compCode = Global.compCode,
                    createBy = "",
                    createTime = DateTime.Now,
                    editBy = "",
                    editTime = DateTime.Now,
                    productId = MyProd.productId,
                    DocumentNo = "",
                    invoiceNo = "",
                    productName = MyProd.productName,
                    qty = qty,
                    unit = "PC",
                    unitPrice = MyProd.price,
                    amount = MyProd.price * qty,
                    //back to fix
                    vat = 0,
                    amountIncludeVat = MyProd.price * qty + 0,
                    discountTotal = 0,
                    discountUnit = 0


                    //back to fix

                };
                list.Add(MyTemp);

            }
            CalculateSummary();
            return list;
        }


        private void CalculateSummary() {
            totalAmountBeforeVat = list.Sum(s => s.amount);
            totalAmountIncludeVat = list.Sum(s => s.amountIncludeVat);
            totalVat = list.Sum(s => s.vat);



        }

        private void printPaper(DataTable dt)
        {
            LocalReport report = new LocalReport();
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath).Remove(path.Length - 10) + @"\Report\rpBill.rdlc";
            report.ReportPath = fullPath;
            report.DataSources.Add(new ReportDataSource("dsSetBill", dt));
            int printQty = Convert.ToInt32(1);
            for (int i = 0; i < printQty; i++)
            {
                PrintToPrinter(report);
            }
        }

        public DataTable GetDataTable(string sql)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                var dt = new DataTable();
                con.Open();
                SqlDataAdapter adpt = new SqlDataAdapter(sql, con);
                adpt.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);

        }

        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                 <OutputFormat>EMF</OutputFormat>
                <PageWidth>3in</PageWidth>
                <PageHeight>1000in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0.1in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();




            //////////////



              

                report.Render("Image", deviceInfo, CreateStream, out warnings);
             
         

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
            {
                Print();
            }

            //////////////

        }


        public static void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height
                
                );

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        public List<Product> filterProd(List<Product> MyProd, string prodCode)
        {
            MyProd = MyProd.Where(w => w.productCode.Equals(prodCode)).ToList();

            return MyProd;
        }


        private void updateData(List<Product> filterProduct, int qty) {
            if (filterProduct.Count > 0)
            {

                // Mylist.Where(o => o.productId > filterProduct.Select( s=> s.productId).FirstOrDefault()).FirstOrDefault();
            }
            else {


            }


        }

      
        private void printBill(string docNum) {
            //string sql = "EXEC spGetDocumentBill @docNum = '" + docNum + "';";
            //    // "select top 30 productCode, productName, price from Product";
            //var dt = GetDataTable(sql);

            ynddevEntities ynd = new ynddevEntities();
            var docBill = ynd.spGetDocumentBill(docNum).ToList();

            var dt = Global.ToDataTable(docBill);

            printPaper(dt);

        }


  


        public mainResult payBill() {
            mainResult rs = new mainResult();
            var ynd = new ynddevEntities();


        //    printBill(list);


            #region "docHeader "
            var lastDoc = (ynd.Documents.Max(m => m.documentNo));


            if (lastDoc == null) { lastDoc = ""; } else { }


            string docNum = "";
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
            Document myDocH = new Document()
            {
                branchCode = Global.branchCode,
                compCode = Global.compCode,
                createBy = Global.username,
                createTime = DateTime.Now,
                editTime = DateTime.Now,
                editBy = Global.username,
                customerId = 1,
                documentNo = docNum,
                totalVat = list.Sum(x=> x.vat),
                totalDc = list.Sum(x => x.dcPrice),
                totalPrice = list.Sum(x => x.amount),
                totalQty = list.Sum(x => x.qty),

            };

            ynd.Documents.Add(myDocH);
            ynd.SaveChanges();
            docHID = myDocH.documentId;

            #endregion


            using (var context = new ynddevEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {



                    #region "documentLine"

                    foreach (DocumentLine dc in list)
                    {
                        dc.DocumentId = docHID;
                        dc.DocumentNo = docNum;
                        context.DocumentLines.Add(dc);
                        context.SaveChanges();

                        #region "Transaction"
                        for (int i = 1; i <= dc.qty; i++)
                        {
                            Transaction tr = new Transaction()
                            {
                                branchCode = Global.branchCode,
                                compCode = Global.compCode,
                                createBy = Global.username,
                                createTime = DateTime.Now,
                                editTime = DateTime.Now,
                                editBy = Global.username,
                                movementTypeId = 1,
                                price = dc.unitPrice,
                                qty = dc.qty,
                                unit = dc.unit,
                                productId = dc.productId,
                                invoiceLineId =0,
                                documentLineId = dc.DocumentLineId,



                            };
                            context.Transactions.Add(tr);
                            context.SaveChanges();
                        }
                      

                        #endregion


                        #region "Balance"
                        Balance myBalance = new Balance();

                        foreach (var a in list)
                        {
                            //=============== update balcnce ====================================
                            myBalance = context.Balances.Where(p => p.productId == a.productId).FirstOrDefault();
                            myBalance.qty = myBalance.qty - a.qty;
                            context.SaveChanges();
                        }



                        #endregion

                    }


                    #endregion

                    //context.SaveChanges();
                    dbContextTransaction.Commit();
                    //   newBill();
                }
            }


            printBill(docNum);

            return rs;
        }

        private void newBill() {


            list = new List<DocumentLine>();
        }



        //private void printBill(List<DocumentLine> list) {
        //    int rowPrd = 28;
        //    int colQty = 2;
        //    int colprdName = 4;
        //    int colPrice = 16;

        //    //var flex = new FlexCell.Grid();
        //    flex.OpenFile(Application.StartupPath + "/bill.flx");

        //    flex.Cell(1, 1).Text = "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy";

        //    foreach (DocumentLine a in list)
        //    {

        //        flex.InsertRow(rowPrd, 1);
        //        flex.Cell(rowPrd, colQty).Text = a.qty.ToString();
        //        flex.Cell(rowPrd, colprdName).Text = a.productName;
        //        flex.Cell(rowPrd, colPrice).Text = a.amount.ToString();
        //        rowPrd += 1;

        //    }

        //  //  flex.PrintPreview();


        //}


        public void createForm() {
            billPaper form = new billPaper();
          
            DataGridView dtgBill = new DataGridView();

            // set cell ============================











        }



    }

    public class billPaper
    {
        #region "margin"
        public cell marginTop = new cell() {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false };

        public cell marginBottom = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false
        };

        public cell marginRight = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false
        };

        public cell marginLeft = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false
        };



        #endregion

        #region "heder"

        public cell icon = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false
            , text = "icon"
        };

        public cell compName = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false
           ,
            text = "ynd coperation"
        };

        public cell adress = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "center of  khonkan thailand "
        };

        public cell taxInfo = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "เลขประจำตัวผู้เสียภาษี 1111111111111111 "
        };

        public cell Detail = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = " โทร  0899999999 "
        };





        #endregion


        #region "billHeader"
        public cell billHeader = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "ใบเสร็จรับเงิน "
        };

        public cell docNum = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = " ynd xxxxxxxxxx "
        };


        public cell lbEmployee = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = " พนักงานขาย "
        };


        public cell employee = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = " satit pongpimol "
        };

        public cell lbCreategDate = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = " วันที่ "
        };
        #endregion

        public cell CreategDate = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "12/02/2021 "
        };

        #region "List"

        public cell qty = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "1"
        };

        public cell ProdName = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "เบียสิงห์ ลัง 12 ขวด 630 ml."
        };

        public cell price = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "630.00"
        };
        #endregion

        #region "summary"

        public cell labelSumQty = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "จำนวนรวม"
        };

        public cell sumQty = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "999"
        };

        public cell lbSumAmount = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "รวมเป้นเงิน"
        };
        public cell SumAmount = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "10,000.00"
        };

        public cell lbDiscount = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "ส่วนลด"
        };


        public cell discount = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "300.00"
        };

        public cell lbPriceAfterDiscount = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "จำนวนเงินหลังหักส่วนลด"
        };

        public cell priceAfterDiscount = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "654.21"
        };

        public cell lbVat = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "ภาษีมูลค่าเพิ่ม 7% "

        };

        public cell vat = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "vat"

        };

        public cell lbExcludeVat = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "ราคาไม่รวมภาษีมูลค่าเพิ่ม"

        };

        public cell excludeVat = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "ราคาไม่รวมภาษีมูลค่าเพิ่ม"

        };

        public cell lbTotal = new cell()
        {
            rowStart = 1,
            colStart = 1,
            rowEnd = 1,
            ColEnd = 1,
            cellHeight = 1,
            cellWidth = 1,
            isMerge = false,
            text = "รวมทั้งสิ้น"

        };
        #endregion



    }



    public class cell {
        public int rowStart = 0;
        public int rowEnd = 0;
        public int colStart = 0;
        public int ColEnd = 0;
        public bool isMerge = false;
        public double cellHeight = 0.00;
        public double cellWidth = 0.00;
        public string text = "";


    }

    public class billHeaderAtribute{
        public string atribute = "xxxxxx";



}




}
