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
    public partial class OfficeForm : UserControl
    {
        public OfficeForm()
        {
            InitializeComponent();
        }

        private JObject obj { get; set; }

        private void OfficeForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 243, 243);
            SearchData();
        }

        private void SearchData()
        {
            String url = AppContext.Session.serverUrl + "sys/sysOffice/findAll";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                treeList1.DataSource = objT["result"].ToObject<List<OfficeEntity>>();
                treeList1.KeyFieldName = "id";//设置ID  
                treeList1.ParentFieldName = "parentId";//设置PreID   
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var edit = new OfficeEdit();
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("保存成功!");
                SearchData();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            String id = Convert.ToString(treeList1.FocusedNode.GetValue("id"));
            var selectedRow = new OfficeEntity();
            if (id == null)
                return;
            selectedRow.id = id;
            var form = new OfficeEdit();
            form.office = selectedRow;
            form.Text = "机构修改";
            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("修改成功!");
                SearchData();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            String id = Convert.ToString(treeList1.FocusedNode.GetValue("id"));
            if (id == null)
                return;
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除吗?", "删除机构", messButton);

            if (dr == DialogResult.OK)
            {
                String url = AppContext.Session.serverUrl + "sys/sysOffice/delete?id=" + id;
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

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var selectedRow = new OfficeEntity();
            selectedRow.parentId = Convert.ToString(treeList1.FocusedNode.GetValue("id"));
            selectedRow.areaId = Convert.ToString(treeList1.FocusedNode.GetValue("areaId"));
            selectedRow.type = Convert.ToString(treeList1.FocusedNode.GetValue("type"));
            selectedRow.grade = Convert.ToString(treeList1.FocusedNode.GetValue("grade"));
            var form = new OfficeEdit();
            form.office = selectedRow;
            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("添加成功!");
                SearchData();
            }
        }
    }
}
