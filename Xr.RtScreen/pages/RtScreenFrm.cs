using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xr.RtScreen.pages
{
    public partial class RtScreenFrm : UserControl
    {
        public RtScreenFrm()
        {
            InitializeComponent();
        }
        #region 

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                         this.panelControl2.ClientRectangle,
                         Color.Red,//7f9db9
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Red,
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Red,
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Transparent,
                         1,
                         ButtonBorderStyle.Solid);
        }

      
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                        this.panelControl1.ClientRectangle,
                        Color.Red,//7f9db9
                        1,
                        ButtonBorderStyle.Solid,
                        Color.Red,
                        1,
                        ButtonBorderStyle.Solid,
                        Color.Red,
                        1,
                        ButtonBorderStyle.Solid,
                        Color.Transparent,
                        1,
                        ButtonBorderStyle.Solid);
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                        this.panelControl3.ClientRectangle,
                        Color.Red,//7f9db9
                        1,
                        ButtonBorderStyle.Solid,
                        Color.Red,
                        1,
                        ButtonBorderStyle.Solid,
                        Color.Red,
                        1,
                        ButtonBorderStyle.Solid,
                        Color.Transparent,
                        1,
                        ButtonBorderStyle.Solid);
        }

        private void scrollingText1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                       this.panelControl1.ClientRectangle,
                       Color.Red,//7f9db9
                       1,
                       ButtonBorderStyle.Solid,
                       Color.Red,
                       1,
                       ButtonBorderStyle.Solid,
                       Color.Red,
                       1,
                       ButtonBorderStyle.Solid,
                       Color.Transparent,
                       1,
                       ButtonBorderStyle.Solid);
        }

        private void scrollingTexts1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                      this.panelControl3.ClientRectangle,
                      Color.Red,//7f9db9
                      1,
                      ButtonBorderStyle.Solid,
                      Color.Red,
                      1,
                      ButtonBorderStyle.Solid,
                      Color.Red,
                      1,
                      ButtonBorderStyle.Solid,
                      Color.Red,
                      1,
                      ButtonBorderStyle.Solid);
        }
        #endregion
    }
}
