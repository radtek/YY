using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.Utils.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Xr.Common.Controls;

namespace Xr.RtManager.Control
{
    /// <summary>
    /// 诊室按钮
    /// </summary>
    [ToolboxItem(true)]
    public class RoomPanelButton : BorderPanelButton
    {
        public RoomPanelButton()
        {
            InitializeComponent();
        }

        private string _Text;
         [Description("按钮文字")]
        public  string BtnText
        {
            get { return _Text; }
            set { _Text = value; }
        }
         private string _RoomText=String.Empty;
         [Description("诊室文字")]
         public string RoomText
         {
             get { return _RoomText; }
             set { _RoomText = value; }
         }
         private Font _RoomFont = new Font("微软雅黑", 16f);
         [DefaultValue(typeof(Font), "微软雅黑,16px")]
         [Description("诊室文字样式")]
         public Font RoomFont
         {
             get { return _RoomFont; }
             set { _RoomFont = value; }
         }
         private Font _BtnFont=new Font("微软雅黑",18f);
        [DefaultValue(typeof(Font), "微软雅黑,18px")]
         [Description("按钮文字样式")]
         public Font BtnFont
         {
             get { return _BtnFont; }
             set { _BtnFont = value; }
         }
        bool _EnableCheck;
        public String noonName= "上午";
        private NoonSpan noon;
        /// <summary>
        /// 获取或设置按钮风格
        /// </summary>
        [DefaultValue(NoonSpan.Moring)]
        public NoonSpan Noon
        {
            get { return noon; }
            set
            {
                noon = value;
                if (noon == NoonSpan.Moring)
                {
                    noonName = "上午";
                }
                else if (noon == NoonSpan.Afternoon)
                {
                   noonName = "下午";
                }
                else 
                {
                   noonName = "晚上";
                }
            }
        }
        private Font _TipFont = new Font("微软雅黑", 12f);
        [DefaultValue(typeof(Font), "微软雅黑,12px")]
        [Description("按钮附件文字样式")]
        public Font TipFont
        {
            get { return _TipFont; }
            set { _TipFont = value; }
        }
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // BorderPanelButton
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BorderPanelButton_Paint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }


        private void BorderPanelButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var textSize = e.Graphics.MeasureString(this._Text, this._BtnFont);
            var textNoonSize = e.Graphics.MeasureString(this.noonName, this._TipFont);
            var textRoomSize = e.Graphics.MeasureString(this._RoomText, this._RoomFont);
            Brush br;
            if (IsCheck)
            {
                br = Brushes.White;
            }
            else
            {
                br = Brushes.Black; 
            }
           
            e.Graphics.DrawString(this._Text, _BtnFont, br, (this.Width - (int)textSize.Width) / 2, (this.Height - (int)textSize.Height)/2);
            RectangleF[] rects = {new Rectangle(this.Width - 60, 1, 59, 30), new Rectangle(0, this.Height - 30, this.Width - 1, 29) };
            SolidBrush bush = new SolidBrush(Color.LightGray);
            if (!this.Enabled)
            {
                SolidBrush bushBG = new SolidBrush(Color.OrangeRed);
                RectangleF BGRec = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
                RectangleF stopRec = new Rectangle(1, 1, 60, 30);
                bush = new SolidBrush(Color.Red);
                e.Graphics.FillRectangle(bushBG, BGRec);
                e.Graphics.FillRectangle(bush, stopRec);
                e.Graphics.DrawString("暂停", _TipFont, Brushes.White, (stopRec.Width - (int)textNoonSize.Width) / 2, (stopRec.Height - (int)textNoonSize.Height) / 2);
                e.Graphics.DrawString(this._Text, _BtnFont, br, (this.Width - (int)textSize.Width) / 2, (this.Height - (int)textSize.Height) / 2);
            }
            bush = new SolidBrush(Color.LightGray);
            e.Graphics.FillRectangles(bush, rects);
            e.Graphics.DrawString(this.noonName, _TipFont, br, (rects[0].Width - (int)textNoonSize.Width) / 2 + this.Width - 60, (rects[0].Height - (int)textNoonSize.Height) / 2);
            e.Graphics.DrawString(this._RoomText, _RoomFont, br, (this.Width - (int)textRoomSize.Width) / 2, this.Height - 30 + (30 - (int)textRoomSize.Height) / 2);//底部
        }
        /// <summary>
        /// 按钮风格
        /// </summary>
        public enum NoonSpan
        {
            /// <summary>
            /// 上午
            /// </summary>
            Moring,

            /// <summary>
            /// 下午
            /// </summary>
            Afternoon,

            /// <summary>
            /// 晚上
            /// </summary>
            Night
        }
    }
}
