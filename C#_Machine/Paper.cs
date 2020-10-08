//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
////using Microsoft.Office.Interop.Excel;
//using Microsoft.CSharp.RuntimeBinder;
//using CherngerUI;
//using OpenCvSharp;

//namespace SensorTechnology
//{
//    class Paper
//    {
//        public static void Excelwrite()
//        {
//            string pathFile = System.IO.Directory.GetCurrentDirectory() + "\\報表\\AOI檢測空白報表.xlsx";
//            string Savepath = System.IO.Directory.GetCurrentDirectory() + "\\報表\\" + "PaPer"+".xlsx";

//            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
//            Microsoft.Office.Interop.Excel.Workbook  workbook1=excelApp.Workbooks.Open(pathFile);
//            Microsoft.Office.Interop.Excel.Worksheet wSheet=new Microsoft.Office.Interop.Excel.Worksheet();
//            wSheet = workbook1.Worksheets[1];
            

//            // 讓Excel文件可見
//            //excelApp.Visible = true;

//            // 停用警告訊息
//            excelApp.DisplayAlerts = false;

//            // 加入新的活頁簿
//            //excelApp.Workbooks.Add(Type.Missing);

//            // 引用第一個活頁簿
//            //wBook = excelApp.Workbooks[1];

//            //// 設定活頁簿焦點
//            //wBook.Activate();
//            //寫入報表


//            excelApp.Cells[16, 3] = new IplImage(new CvSize(10,10),BitDepth.U8,1); //檢測數量
//            //excelApp.Cells[16, 8] = "12345"; //PASS數量
//            //excelApp.Cells[18, 8] = "12345"; //FAIL數量
//            //excelApp.Cells[16, 13] = "12345"; //良率
//            //excelApp.Cells[18, 13] = "12345"; //不良率
//            //excelApp.Cells[16, 18] = "12345"; //dppM
//            //excelApp.Cells[16, 23] = "12345"; //檢測時間
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //excelApp.Cells[6, 4] = "12345"; //工單編號
//            //wSheet.get_Range("1" + (0 + 37).ToString(), "2" + (0 + 37).ToString()).Merge(false);
//            //wSheet.Shapes.AddPicture("xzxc", Microsoft.Office.Core.MsoTriState.msoCTrue, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 0, 0);
//            //int num = 23;

//            //if (num > 22)//瑕疵超過22個
//            //{
//            //    for (int i = 23; i < num + 1; i++)
//            //    {
//            //        //合併儲存格
//            //        wSheet.get_Range("A" + (i + 37).ToString(), "B" + (i + 37).ToString()).Merge(false);
//            //        wSheet.get_Range("C" + (i + 37).ToString(), "F" + (i + 37).ToString()).Merge(false);
//            //        wSheet.get_Range("G" + (i + 37).ToString(), "T" + (i + 37).ToString()).Merge(false);
//            //        wSheet.get_Range("U" + (i + 37).ToString(), "Y" + (i + 37).ToString()).Merge(false);
//            //        //格線
//            //        wSheet.get_Range("A" + (i + 37).ToString(), "Y" + (i + 37).ToString()).Borders.LineStyle = 1;
//            //        //字體大小
//            //        wSheet.get_Range("A" + (i + 37).ToString(), "Y" + (i + 37).ToString()).Font.Size = 16;
//            //        //字型
//            //        wSheet.get_Range("A" + (i + 37).ToString(), "Y" + (i + 37).ToString()).Columns.Font.Name = "微軟正黑體";
//            //        //填值
//            //        excelApp.Cells[i + 37, 1] = i.ToString();//編號

//            //        //自動列寬
//            //        wSheet.get_Range("A" + (i + 37).ToString(), "Y" + (i + 37).ToString()).EntireColumn.AutoFit();

//            //    }
//            //}

//            ////寫入NGIC資料
//            //for(int i=1;i<num+1;i++)
//            //{
//            //    excelApp.Cells[i + 37, 3] = i.ToString();//Fail Ic Tray位置
//            //    excelApp.Cells[i + 37, 7] = i.ToString();//Fail Ic圖檔檔名(工單編號-客戶編號-IC類別-IC球數-檢測日期-NG產品不良項目-檢測Fail順序)
//            //    excelApp.Cells[i + 37, 21] = i.ToString();//備註

//            //}

//            ////居中
//            //wSheet.get_Range("A1", "Y" + (num + 37).ToString()).HorizontalAlignment = XlHAlign.xlHAlignCenter;

//            workbook1.SaveCopyAs(Savepath);


//            //釋放資源
//            wSheet = null;
//            workbook1.Close();
//            workbook1 = null;
//            excelApp.Quit();
//            excelApp = null;

//        }
//    }
//}
