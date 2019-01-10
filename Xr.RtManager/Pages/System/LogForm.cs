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
using System.Runtime.InteropServices;
using Xr.Http;

namespace Xr.RtManager
{
    public partial class LogForm : UserControl
    {
        //[DllImport("user32.dll")]
        //public static extern IntPtr GetActiveWindow();

        //[DllImport("user32.dll")]
        //public static extern IntPtr SetActiveWindow(IntPtr hwnd);

        public LogForm()
        {
            InitializeComponent();
        }

        private JObject obj { get; set; }

        private void LogForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 243, 243);
            String url = AppContext.AppConfig.serverUrl + "sys/sysOffice/findAll";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<OfficeEntity> officeList = objT["result"].ToObject<List<OfficeEntity>>();
                treeCompany.Properties.DataSource = officeList; //绑定数据           
                treeCompany.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeCompany.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeCompany.Properties.DisplayMember = "name";  //要在树里展示的
                treeCompany.Properties.ValueMember = "id";    //对应的value

                treeOffice.Properties.DataSource = officeList; //绑定数据           
                treeOffice.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeOffice.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeOffice.Properties.DisplayMember = "name";  //要在树里展示的
                treeOffice.Properties.ValueMember = "id";    //对应的value

                SearchData(true, 1, pageControl1.PageSize);
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        public void SearchData(bool flag, int pageNo, int pageSize)
        {
            String exception = "";
            if (checkEdit1.Checked) exception = "true"; //不为空白字符串就行
            String param = "?companyId=" + treeCompany.EditValue
                + "&&officeId=" + treeOffice.EditValue + "&&createByName=" + tbUserName.Text
                + "&&requestUri=" + tbURL.Text + "&&exception=" + exception
                + "&&beginDate=" + deStart.EditValue + "&&endDate=" + deEnd.EditValue
                + "&&pageNo=" + pageNo + "&&pageSize=" + pageSize;
            String url = AppContext.AppConfig.serverUrl + "sys/sysLog/findAll" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                gcDict.DataSource = objT["result"]["list"].ToObject<List<LogEntity>>();
                pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                int.Parse(objT["result"]["pageSize"].ToString()),
                int.Parse(objT["result"]["pageNo"].ToString()));
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
            var selectedRow = gridView1.GetFocusedRow() as LogEntity;
            if (selectedRow == null)
                return;
            String text = "";
            text += "用户代理:"+selectedRow.userAgent + "\r\n";
            text += "提交参数:"+selectedRow.param;
            if (checkEdit1.CheckState == CheckState.Checked)
            {
                text += "\r\n" + "异常信息" + selectedRow.exception;
            }
            int i = gridView1.GetFocusedDataSourceRowIndex();
            //IntPtr activeForm = GetActiveWindow(); // 先得到当前的活动窗体 
            MessageBoxUtils.HintTextEdit(text, 174, 208 + ((i + 1) * 29), gcDict.Width);
            //SetActiveWindow(activeForm); // 在把焦点还给之前的活动窗体
            
        }

        private void pageControl1_Query(int CurrentPage, int pageSize)
        {
            SearchData(false, CurrentPage, pageSize);
        }
    }
}
