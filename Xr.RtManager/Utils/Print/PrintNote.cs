﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace Xr.RtManager.Utils
{
    public class PrintNote
    {
        
        private string ViceTitle;
        private string Typename;
        private string SpeakNum;
        private string Name;
        private string PatientID;
        private string WaitSize;

        public PrintNote(string ViceTitle, string Typename, string SpeakNum, string Name, string PatientID, string WaitSize)
        {
            this.ViceTitle = ViceTitle;
            this.Typename = Typename;
            this.SpeakNum = SpeakNum;
            this.Name = Name;
            this.PatientID = PatientID;
            this.WaitSize = WaitSize;
        }
        public bool Print(ref string message)
        {
            try
            {
                if (PatientID != "")
                {
                    // 1.设置条形码规格
                    EncodingOptions encodeOption = new EncodingOptions();
                    encodeOption.Height = 50; // 必须制定高度、宽度
                    encodeOption.Width = 65;
                    // 2.生成条形码图片并保存
                    ZXing.BarcodeWriter wr = new BarcodeWriter();
                    wr.Options = encodeOption;
                    wr.Format = BarcodeFormat.CODE_128; //  条形码规格
                    Bitmap img = wr.Write(PatientID); // 生成图片
                    Bitmap tempBmp = new Bitmap(img);

                    PrintRow srow0 = new PrintRow(0, "中山市博爱医院", new Font("宋体", 18), System.Drawing.Brushes.Black, -1);
                    PrintRow srow1 = new PrintRow(1, ViceTitle, new Font("宋体", 16), System.Drawing.Brushes.Black, 40);
                    PrintRow srow2 = new PrintRow(2, Typename + "号:" + SpeakNum, new Font("宋体", 14), System.Drawing.Brushes.Black, 75);
                    PrintRow srow3 = new PrintRow(3, "姓名:" + Name, new Font("宋体", 14), System.Drawing.Brushes.Black, 105);
                    //PrintRow srow4 = new PrintRow(4, "级别:" + level, new Font("宋体", 14), System.Drawing.Brushes.Black, 135);
                    PrintRow srow4 = new PrintRow(4, "编号:", new Font("宋体", 14), System.Drawing.Brushes.Black, 135, tempBmp);
                    PrintRow srow5 = new PrintRow(5, "-------------------------------------------", new Font("宋体", 10), System.Drawing.Brushes.Black, 205);
                    PrintRow srow6 = new PrintRow(6, "请您到等候休息区等待", new Font("宋体", 16), System.Drawing.Brushes.Green, 215);
                    PrintRow srow7 = new PrintRow(7, String.Format("(目前候诊人数：{0}人)", WaitSize), new Font("宋体", 16), System.Drawing.Brushes.Green, 240);
                    PrintRow srow8 = new PrintRow(8, "注意屏幕提示(过号重排)", new Font("宋体", 12), System.Drawing.Brushes.Black, 265);
                    PrintRow srow9 = new PrintRow(9, DateTime.Now.ToString(), new Font("宋体", 14), System.Drawing.Brushes.Black, 285);
                    Order tempOrders = new Order(new List<PrintRow>() { srow0, srow1, srow2, srow3, srow4, srow5, srow6, srow7, srow8, srow9 });
                    PrintOrder.Print(ConfigurationManager.AppSettings["PrinterName"],tempOrders);
                }
                else//非实名
                {
                        PrintRow srow0 = new PrintRow(0, "中山市博爱医院", new Font("宋体", 18), System.Drawing.Brushes.Black, -1);
                        PrintRow srow1 = new PrintRow(1, ViceTitle, new Font("宋体", 16), System.Drawing.Brushes.Black, 40);
                        PrintRow srow2 = new PrintRow(2, Typename + "号:" + SpeakNum, new Font("宋体", 14), System.Drawing.Brushes.Black, 75);
                        PrintRow srow3 = new PrintRow(3, "-------------------------------------------", new Font("宋体", 10), System.Drawing.Brushes.Black, 105);
                        PrintRow srow4 = new PrintRow(4, "请您到等候休息区等待", new Font("宋体", 16), System.Drawing.Brushes.Green, 135);
                        PrintRow srow5 = new PrintRow(5, String.Format("(目前候诊人数：{0}人)", WaitSize), new Font("宋体", 16), System.Drawing.Brushes.Green, 155);
                        PrintRow srow6 = new PrintRow(6, "注意屏幕提示(过号重排)", new Font("宋体", 12), System.Drawing.Brushes.Black, 180);
                        PrintRow srow7 = new PrintRow(7, DateTime.Now.ToString(), new Font("宋体", 14), System.Drawing.Brushes.Black, 195);
                        Order tempOrders = new Order(new List<PrintRow>() { srow0, srow1, srow2, srow3, srow4, srow5, srow6, srow7});
                        PrintOrder.Print(ConfigurationManager.AppSettings["PrinterName"],tempOrders);
                    
                }

                return true;
                //}
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                message = e.Message;
                return false;
            }
        }
    }
}