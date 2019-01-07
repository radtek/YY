using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xr.RtCall.pages
{
    public partial class RtCallFrm : UserControl
    {
        public RtCallFrm()
        {
            InitializeComponent();
            this.Height = 40;
        }
        /// <summary>
        /// 下一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RtCallMessageFrm rcf = new RtCallMessageFrm();
            Xr.RtCall.Form1 f = new Form1();
            f.panelControl1.Controls.Clear();
            f.Height = rcf.Height+30;
            f.Width = rcf.Width;
            f.labBoxInfor.Text = "";
            f.panelControl1.Controls.Add(rcf);
            f.Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            RtCallFrm rc = new RtCallFrm();
            RtCallPeationFrm rcf = new RtCallPeationFrm();
            Xr.RtCall.Form1 f = new Form1();
            //f.panelControl1.Controls.Clear();
            f.Height = rcf.Height + 30;
            f.Width = rcf.Width;
            f.labBoxInfor.Text = "叫号";
            rcf.Location = new System.Drawing.Point(0, rc.Location.Y + rc.Height);
            f.panelControl1.Controls.Add(rcf);
            f.Show();
        }
    }
}
