using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.Http;
using Newtonsoft.Json.Linq;

namespace Xr.RtManager.Pages.cms
{
    public partial class MessageManagement : UserControl
    {
        public MessageManagement()
        {
            InitializeComponent();
        }
        #region 消息内容模版列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="hospitalId"></param>
        public void MessageContentTemplateList(int pageNo, int pageSize, string hospitalId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "yyfz/api/messageTemplate/findPage?pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospitalId=" + hospitalId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Message.DataSource = objT["result"]["list"].ToObject<List<dynamic>>();
                    pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                    int.Parse(objT["result"]["pageSize"].ToString()),
                    int.Parse(objT["result"]["pageNo"].ToString()));
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
        #endregion 
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string id = "";
                string hospitalId = "";
                string type = "";
                string content = "";
                String url = AppContext.AppConfig.serverUrl + "yyfz/api/messageTemplate/save?id=" + id + "&hospitalId=" + hospitalId + "&type=" + type + "&content=" + content;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    Xr.Common.MessageBoxUtils.Hint("保存成功");
                    ///this.gc_Holiday.DataSource = objT["result"]["list"].ToObject<List<Holiday>>();
                    //pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                    //int.Parse(objT["result"]["pageSize"].ToString()),
                    //int.Parse(objT["result"]["pageNo"].ToString()));
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion 
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRow = this.gv_Message.GetSelectedRows()[0];
                string name = this.gv_Message.GetRowCellValue(selectRow, "name").ToString();
                string id = this.gv_Message.GetRowCellValue(selectRow, "id").ToString();
                if (DialogResult.Yes == Xr.Common.MessageBoxUtils.Show("是否删除", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
                {
                    String url = AppContext.AppConfig.serverUrl + "yyfz/api/messageTemplate/delete?ids=" + id;
                    String data = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        Xr.Common.MessageBoxUtils.Hint("删除成功");
                    }
                    else
                    {
                        MessageBox.Show(objT["message"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion 
    }
}
