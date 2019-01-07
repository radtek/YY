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

namespace Xr.RtManager
{
    public partial class RoleDistributionForm : UserControl
    {
        public RoleDistributionForm()
        {
            InitializeComponent();
        }

        public String id { get; set; }

        private void RoleDistributionForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 243, 243);
            SearchData();
        }

        private void SearchData()
        {
            String url = AppContext.Session.serverUrl + "sys/sysRole/assign?roleId=" + id;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                gcUser.DataSource = objT["result"].ToObject<List<UserEntity>>();
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var edit = new RoleDistributionEdit();
            edit.roleId = this.id;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                SearchData();
                MessageBoxUtils.Hint(edit.msg, true);
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            var selectedRow = gvUser.GetFocusedRow() as UserEntity;
            if (selectedRow == null)
                return;
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要移除吗?", "删除用户", messButton);

            if (dr == DialogResult.OK)
            {
                String url = AppContext.Session.serverUrl + "sys/sysRole/outrole?userId=" + selectedRow.id + "&&roleId=" + id;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint(objT["message"].ToString());
                    SearchData();
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
        }
    }
}
