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
using Xr.Common;

namespace Xr.RtManager.Pages.cms
{
    public partial class MessageManagement : UserControl
    {
        public MessageManagement()
        {
            InitializeComponent();
            MessageContentTemplateList(AppContext.Session.hospitalId);
            //Date = new List<MessageInfoEntity> { 
            //new MessageInfoEntity{name="测试",code="01000",StardTime="2019-01-08",EndTime="2019-05-08"},
            //};
            //this.gc_Message.DataSource = Date;
        }
        #region 测试
        public List<MessageInfoEntity> Date = new List<MessageInfoEntity>();
        #endregion 
        #region 消息内容模版列表
        /// <summary>
        /// 消息内容模版列表
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="hospitalId"></param>
        public void MessageContentTemplateList(string hospitalId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "cms/messageTemplate/list?hospital.id=" + hospitalId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Message.DataSource = objT["result"]["list"].ToObject<List<MessageInfoEntity>>();
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
                LogClass.WriteLog("获取消息内容模版列表错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 查询消息模板详情
        /// <summary>
        /// 查询消息模板详情
        /// </summary>
        /// <param name="id">id</param>
        public void QueryMessageTemplateDetails(string id)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "api/cms/messageTemplate/findById?id=" + id;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    //this.gc_Message.DataSource = objT["result"]["list"].ToObject<List<dynamic>>();
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
        #region 获取指定医院下消息模板列表
        /// <summary>
        /// 获取指定医院下消息模板列表
        /// </summary>
        /// <param name="id">医院ID</param>
        public void GetSpecifiedHospital(string id)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "api/cms/messageTemplate/findAll?hospital.id=" + id;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    //this.gc_Message.DataSource = objT["result"]["list"].ToObject<List<dynamic>>();
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
                var edit = new MessageSettingEdit();
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    MessageBoxUtils.Hint("保存成功!");
                    MessageContentTemplateList(AppContext.Session.hospitalId);
                    //  SearchData(true, 1, pageControl1.PageSize);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion 
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = gv_Message.GetFocusedRow() as MessageInfoEntity;
                if (selectedRow == null)
                    return;
                var edit = new MessageSettingEdit();
                edit.messageInfoEntity = selectedRow;
                edit.Text = "节假日修改";
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    MessageBoxUtils.Hint("修改成功!");
                    MessageContentTemplateList(AppContext.Session.hospitalId);
                    // SearchData(true, pageControl1.CurrentPage, pageControl1.PageSize);
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
                var selectedRow = gv_Message.GetFocusedRow() as MessageInfoEntity;
                if (selectedRow == null)
                    return;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除消息内容模板", messButton);

                if (dr == DialogResult.OK)
                {
                    String param = "?id=" + selectedRow.id;
                    String url = AppContext.AppConfig.serverUrl + "cms/messageTemplate/delete" + param;
                    String data = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        Xr.Common.MessageBoxUtils.Hint("删除成功");
                        MessageContentTemplateList(AppContext.Session.hospitalId);
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
