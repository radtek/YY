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
    public partial class RoleForm : UserControl
    {
        public RoleForm()
        {
            InitializeComponent();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 243, 243);
            SearchData();
        }

        private void SearchData()
        {
            String url = AppContext.AppConfig.serverUrl + "sys/sysRole/findAll";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                gcRole.DataSource = objT["result"].ToObject<List<RoleEntity>>();
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var edit = new RoleEdit();
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("保存成功!");
                SearchData();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            var selectedRow = gvRole.GetFocusedRow() as RoleEntity;
            if (selectedRow == null)
                return;
            var edit = new RoleEdit();
            RoleEntity role = new RoleEntity();
            role.id = selectedRow.id;
            edit.role = role;
            edit.Text = "角色修改";
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("修改成功!");
                SearchData();
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var selectedRow = gvRole.GetFocusedRow() as RoleEntity;
            if (selectedRow == null)
                return;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("id", selectedRow.id);
            MainForm form = (MainForm)this.Parent.TopLevelControl;
            form.JumpInterface("RoleDistributionForm", "角色分配", data);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var selectedRow = gvRole.GetFocusedRow() as RoleEntity;
            if (selectedRow == null)
                return;
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除吗?", "删除角色", messButton);

            if (dr == DialogResult.OK)
            {
                String url = AppContext.AppConfig.serverUrl + "sys/sysRole/delete?id=" + selectedRow.id;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("删除成功!");
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
