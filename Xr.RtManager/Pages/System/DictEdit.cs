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

namespace Xr.RtManager
{
    public partial class DictEdit : Form
    {
        public DictEdit()
        {
            InitializeComponent();
        }

        public DictEntity dict { get; set; }

        private void DictEdit_Load(object sender, EventArgs e)
        {
            dcDict.DataType = typeof(DictEntity);
            if (dict != null)
            {
                dcDict.SetValue(dict);
            }
            else
            {
                dict = new DictEntity();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcDict.Validate())
            {
                return;
            }
            dcDict.GetValue(dict);
            String param = "?" + PackReflectionEntity<DictEntity>.GetEntityToRequestParameters(dict);
            String url = AppContext.AppConfig.serverUrl + "sys/sysDict/save" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
