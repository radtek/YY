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

namespace Xr.Common.Controls
{
    /// <summary>
    /// 带边框和圆角的Panel
    /// </summary>
    [ToolboxItem(true)]
    public class BorderPanelButton : BorderPanel
    {
        public BorderPanelButton()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // BorderPanelButton
            // 
            this.Click += new System.EventHandler(this.BorderPanelButton_Click);
            this.MouseEnter += new System.EventHandler(this.BorderPanelButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.BorderPanelButton_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private bool isCheck ;
        [DefaultValue(false)]
        [Description("是否选中")]
        public bool IsCheck
        {
            get { return isCheck; }
            set
            {
                //如果赋的值与原值不同
                if (value != isCheck)
                {
                    //就触发该事件!
                    WhenMyValueChange();
                }
                if (value == true)
                {
                    if (this.Parent != null)
                    {
                        foreach (Control c in this.Parent.Controls)
                        {
                            if (c is BorderPanelButton && c != this)
                            {
                                (c as BorderPanelButton).IsCheck = false;
                            }
                        }
                        
                    }
                }
                 
                isCheck = value;
            }
        }
        private void WhenMyValueChange()
        {
            if (isCheck)
            {
                this.FillColor1 = Color.FromArgb(60, 195, 245);
                this.FillColor2 = Color.FromArgb(60, 195, 245);
                //RefreshFillBrush();
            }
            else
            {
                this.FillColor1 = Color.DarkBlue;
                this.FillColor2 = Color.DarkBlue;
                //RefreshFillBrush();
            }
        }
        private void BorderPanelButton_MouseEnter(object sender, EventArgs e)
        {
            if (!isCheck)
            {
                this.FillColor1 = Color.FromArgb(55, 190, 240);
                this.FillColor2 = Color.FromArgb(55, 190, 240);
            }
            else
            {
                this.FillColor1 = Color.DarkBlue;
                this.FillColor2 = Color.DarkBlue;
            }
        }

        private void BorderPanelButton_MouseLeave(object sender, EventArgs e)
        {
            if (!isCheck)
            {
                this.FillColor1 = Color.FromArgb(60, 195, 245);
                this.FillColor2 = Color.FromArgb(60, 195, 245);
            }
            else
            {
                this.FillColor1 = Color.DarkBlue;
                this.FillColor2 = Color.DarkBlue;
            }
        }

        private void BorderPanelButton_Click(object sender, EventArgs e)
        {
            if (isCheck)
            { 
                IsCheck = false;
                this.FillColor1 = Color.FromArgb(60, 195, 245);
                this.FillColor2 = Color.FromArgb(60, 195, 245);
            }
            else
            {
                IsCheck = true;
                this.FillColor1 = Color.DarkBlue;
                this.FillColor2 = Color.DarkBlue;
            }

        }

    }
}
