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
    public partial class DictForm : UserControl
    {
        public DictForm()
        {
            InitializeComponent();
        }

        private JObject obj { get; set; }

        private void UserForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 243, 243);
            SearchData(true, 1, pageControl1.PageSize);

        }

        public void SearchData(bool flag, int pageNo, int pageSize)
        {
            String param = "?type=" + cbType.SelectedValue
                + "&&description=" + tbDescription.Text + "&&pageNo=" + pageNo
                + "&&pageSize=" + pageSize;
            String url = AppContext.AppConfig.serverUrl + "sys/sysDict/findAll" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                gcDict.DataSource = objT["result"]["list"].ToObject<List<DictEntity>>();
                pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                int.Parse(objT["result"]["pageSize"].ToString()),
                int.Parse(objT["result"]["pageNo"].ToString()));
                if (flag)
                {
                    cbType.DataSource = objT["result"]["typeList"];
                    cbType.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            SearchData(false, 1, pageControl1.PageSize);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            var edit = new DictEdit();
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("保存成功!");
                SearchData(true, 1, pageControl1.PageSize);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as DictEntity;
            if (selectedRow == null)
                return;
             MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
             DialogResult dr = MessageBox.Show("确定要删除吗?", "删除字典", messButton);

             if (dr == DialogResult.OK)
             {
                String param = "?id=" + selectedRow.id;
                String url = AppContext.AppConfig.serverUrl + "sys/sysDict/delete" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("删除成功!");
                    SearchData(false, pageControl1.CurrentPage, pageControl1.PageSize);
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
             }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as DictEntity;
            if (selectedRow == null)
                return;
            var edit = new DictEdit();
            edit.dict = selectedRow;
            edit.Text = "字典修改";
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("修改成功!");
                SearchData(true, pageControl1.CurrentPage, pageControl1.PageSize);
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as DictEntity;
            if (selectedRow == null)
                return;
            var edit = new DictEdit();
            DictEntity dict = new DictEntity();
            dict.type = selectedRow.type;
            dict.description = selectedRow.description;
            dict.sort = selectedRow.sort;
            edit.dict = dict;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("添加成功!");
                SearchData(true, pageControl1.CurrentPage, pageControl1.PageSize);
            }
        }

        private void pageControl1_Query(int CurrentPage, int pageSize)
        {
            SearchData(false, CurrentPage, pageSize);
        }
    }
}
