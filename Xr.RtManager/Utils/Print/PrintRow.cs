using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Xr.RtManager.Utils
{
    /// <summary>
    /// 全部采用居中绘制
    /// </summary>
    public class PrintRow
    {
        private string context;
        /// <summary>
        /// 内容
        /// </summary>
        public string Context
        {
            get { return context; }
            set { context = value; }
        }


        private Font drawFont;
        /// <summary>
        /// 字体 
        /// </summary>
        public Font DrawFont
        {
            get { return drawFont; }
            set { drawFont = value; }
        }

        private int printIndex;
        /// <summary>
        /// 打印顺序
        /// </summary>
        public int PrintIndex
        {
            get { return printIndex; }
            set { printIndex = value; }
        }

        private int drawHeight;
        /// <summary>
        /// 绘制位置
        /// </summary>
        public int DrawHeight
        {
            get { return drawHeight; }
            set { drawHeight = value; }
        }

        private Brush drawBrush;

        public Brush DrawBrush
        {
            get { return drawBrush; }
            set { drawBrush = value; }
        }

        private bool isDrawBitMap;

        public bool IsDrawBitMap
        {
            get { return isDrawBitMap; }
            //set { isDrawBitMap = value; }
        }

        Bitmap bmp;

        public Bitmap Bmp
        {
            get { return bmp; }
            //set { bmp = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="context"></param>
        /// <param name="penFont"></param>
        /// <param name="drawBrush"></param>
        /// <param name="drawHeight"></param>
        public PrintRow(int index, string context, Font penFont, Brush drawBrush, int drawHeight)
        {
            this.printIndex = index;
            this.context = context;
            this.drawFont = penFont;
            this.drawBrush = drawBrush;
            this.drawHeight = drawHeight;
            isDrawBitMap = false;
        }

        public PrintRow(int index, string context, Font penFont, Brush drawBrush, int drawHeight, Bitmap bmp)
        {
            this.printIndex = index;
            this.context = context;
            this.drawFont = penFont;
            this.drawBrush = drawBrush;
            this.drawHeight = drawHeight;
            isDrawBitMap = true;
            this.bmp = bmp;
        }

    }
}
