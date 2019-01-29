using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Http;
using System.IO;
using System.Net;
using Xr.Common;

namespace Xr.RtManager.Pages.scheduling
{
    public partial class RemarksEdit : Form
    {
        public RemarksEdit()
        {
            InitializeComponent();
        }

        public String deptId { get; set; }
        public String doctorId { get; set; }
        public String beginDate { get; set; }
        public String endDate{ get; set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String param = "beginDate=" + beginDate + "&endDate=" + endDate
    + "&hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + deptId
    + "&doctorId=" + doctorId + "&remarks=" + meRemarks.Text;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/updateRemarks?" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
