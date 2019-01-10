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
    public partial class ClientVersionForm : UserControl
    {
        public ClientVersionForm()
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
            String param = "?title=" + tbTitle.Text
                + "&&version=" + tbVersion.Text + "&&pageNo=" + pageNo
                + "&&pageSize=" + pageSize;
            String url = AppContext.AppConfig.serverUrl + "sys/clientVersion/list" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                gcDict.DataSource = objT["result"][0]["list"].ToObject<List<ClientVersionEntity>>();
                pageControl1.setData(int.Parse(objT["result"][0]["count"].ToString()),
                int.Parse(objT["result"][0]["pageSize"].ToString()),
                int.Parse(objT["result"][0]["pageNo"].ToString()));
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
            var edit = new ClientVersionEdit();
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("保存成功!");
                SearchData(true, 1, pageControl1.PageSize);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as ClientVersionEntity;
            if (selectedRow == null)
                return;
             MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
             DialogResult dr = MessageBox.Show("确定要删除吗?", "删除版本", messButton);

             if (dr == DialogResult.OK)
             {
                String param = "?id=" + selectedRow.id;
                String url = AppContext.AppConfig.serverUrl + "sys/clientVersion/delete" + param;
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
            var selectedRow = gridView1.GetFocusedRow() as ClientVersionEntity;
            if (selectedRow == null)
                return;
            var edit = new ClientVersionEdit();
            edit.clientVersion = selectedRow;
            edit.Text = "版本修改";
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("修改成功!");
                SearchData(true, pageControl1.CurrentPage, pageControl1.PageSize);
            }
        }

        private void pageControl1_Query(int CurrentPage, int pageSize)
        {
            SearchData(false, CurrentPage, pageSize);
        }
    }
}
