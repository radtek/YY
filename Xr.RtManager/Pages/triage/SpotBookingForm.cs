using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Common;
using Xr.Http;
using Xr.Common.Controls;

namespace Xr.RtManager
{
    public partial class SpotBookingForm : UserControl
    {
        Xr.Common.Controls.OpaqueCommand cmd;
        public SpotBookingForm()
        {
            InitializeComponent();
            //cmd = new Xr.Common.Controls.OpaqueCommand(this);
            //cmd.ShowOpaqueLayer(225, true);
            cmd = new Xr.Common.Controls.OpaqueCommand(panel1);
            Dictionary<int, DateTime> dc = new Dictionary<int, DateTime>();
            for (int i = 17; i < 25; i++)
            {
                dc.Add(i, System.DateTime.Now);
            }
            reservationCalendar1.ValidDateList = dc;
            reservationCalendar1.SetGridClanderValue();
        }

        private JObject obj { get; set; }

        private void UserForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 243, 243);
            SearchData(true, 1);

        }

        public void SearchData(bool flag, int pageNo)
        {
          
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            SearchData(false, 1);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            var edit = new ClientVersionEdit();
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("保存成功!");
                SearchData(true, 1);
            }
        }
        
        private void btnDel_Click(object sender, EventArgs e)
        {


            //BorderPanel m_OpaqueLayer = new BorderPanel();
            //m_OpaqueLayer.Size = this.Size;
            //m_OpaqueLayer.BringToFront();
            //this.Controls.Add(m_OpaqueLayer);

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
           
        }
        private void skinButton_Click(object sender, EventArgs e)
        {
            cmd.HideOpaqueLayer();
        }
        private void reservationCalendar1_SelectDate(DateTime SelectedDate)
        {
            MessageBox.Show(SelectedDate.ToShortDateString());
        }

        private void reservationCalendar1_ChangeMonth(DateTime SelectedMonth)
        {
            Dictionary<int, DateTime> dc = new Dictionary<int, DateTime>();
            for (int i = 10; i < SelectedMonth.Month + 20; i++)
            {
                dc.Add(i, System.DateTime.Now);
            }
            reservationCalendar1.ValidDateList = dc;
            reservationCalendar1.SetGridClanderValue();
        }

        private void reservationCalendar1_SelectDateTest(DateTime SelectedDate)
        {
            MessageBox.Show(SelectedDate.ToShortDateString() + "测试事件");
        }
    }
}
