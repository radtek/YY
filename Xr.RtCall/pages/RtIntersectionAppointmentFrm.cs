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
    public partial class RtIntersectionAppointmentFrm : UserControl
    {
        public RtIntersectionAppointmentFrm()
        {
            InitializeComponent();
            listTimes.Items.Add("9:00-9:30<5>");
            listTimes.Items.Add("9:30-10:00<5>");
            listTimes.Items.Add("10:00-10:30<5>");
            listTimes.Items.Add("11:00-11:30<5>");
            listTimes.Items.Add("11:30-12:00<5>");
        }
        #region 返回
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButReturn_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.pCurrentWin.panel_MainFrm.Controls.Clear();
                RtCallPeationFrm rtcpf = new RtCallPeationFrm();
                rtcpf.Dock = DockStyle.Fill;
                Form1.pCurrentWin.panel_MainFrm.Controls.Add(rtcpf);
            }
            catch (Exception)
            {
            }
        }
        #endregion 

    }
}
