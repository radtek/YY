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
    public partial class HolidaySettingForm : UserControl
    {
        public HolidaySettingForm()
        {
            InitializeComponent();
            HolidaySettingList(AppContext.Session.hospitalId);
        }
        public List<HolidayInfoEntity> Data = new List<HolidayInfoEntity>();
        #region 节假日列表
        /// <summary>
        /// 节假日列表
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="hospitalId">所属医院</param>
        public void HolidaySettingList(string hospitalId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "cms/holiday/list?hospital.id=" + hospitalId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Holiday.DataSource = objT["result"]["list"].ToObject<List<HolidayInfoEntity>>();
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
                LogClass.WriteLog("获取节假日列表错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 查询节假日详情
        public void CheckHolidayDetails(string id)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "api/cms/holiday/findById?id=" + id;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    //this.gc_Holiday.DataSource = objT["result"]["list"].ToObject<List<HolidayInfoEntity>>();
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
        #region 查询指定医院下节假日列表
       /// <summary>
        /// 查询指定医院下节假日列表
       /// </summary>
       /// <param name="id">医院ID</param>
        public void QueryDesignatedHospital(string id)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "api/cms/holiday/findAll?hospital.id=" +id;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    //this.gc_Holiday.DataSource = objT["result"]["list"].ToObject<List<HolidayInfoEntity>>();
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
        #region 查询时间段内属于节假日的日期
        /// <summary>
        /// 查询时间段内属于节假日的日期
        /// </summary>
        /// <param name="hospitalId">医院ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        public void TheDateHolidayPeriod(string hospitalId, string beginDate, string endDate)
        {
            try
            {
               String url = AppContext.AppConfig.serverUrl + "api/cms/holiday/checkHolidayForDate?hospitalId=" + hospitalId + "&beginDate=" + beginDate + "&endDate=" + endDate;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    //this.gc_Holiday.DataSource = objT["result"]["list"].ToObject<List<HolidayInfoEntity>>();
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
                groupBox1.Enabled = true;
                dcHodily.ClearValue();
                //var edit = new HolidaySettingEdit();
                //if (edit.ShowDialog() == DialogResult.OK)
                //{
                //    MessageBoxUtils.Hint("保存成功!");
                //    HolidaySettingList(AppContext.Session.hospitalId);
                //    //  SearchData(true, 1, pageControl1.PageSize);
                //}
            }
            catch (Exception)
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
                var selectedRow = gv_Holiday.GetFocusedRow() as HolidayInfoEntity;
                if (selectedRow == null)
                    return;
                dcHodily.SetValue(selectedRow);
                groupBox1.Enabled = true ;
                //var edit = new HolidaySettingEdit();
                //edit.holidayInfoEntity = selectedRow;
                //edit.Text = "节假日修改";
                //if (edit.ShowDialog() == DialogResult.OK)
                //{
                //    MessageBoxUtils.Hint("修改成功!");
                //    HolidaySettingList(AppContext.Session.hospitalId);
                // //   SearchData(true, pageControl1.CurrentPage, pageControl1.PageSize);
                //}
            }
            catch (Exception)
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
                var selectedRow = gv_Holiday.GetFocusedRow() as HolidayInfoEntity;
                if (selectedRow == null)
                    return;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除节假日", messButton);

                if (dr == DialogResult.OK)
                {
                    String param = "?id=" + selectedRow.id;
                    String url = AppContext.AppConfig.serverUrl + "cms/holiday/delete" + param;
                    String data = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        Xr.Common.MessageBoxUtils.Hint("删除成功");
                        HolidaySettingList(AppContext.Session.hospitalId);
                    }
                    else
                    {
                        MessageBox.Show(objT["message"].ToString());
                    }
                 
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region 控制编辑
        /// <summary>
        /// 控制编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Holiday_ShowingEditor(object sender, CancelEventArgs e)
        {
            int selectRow = gv_Holiday.GetSelectedRows()[0];
            string name = this.gv_Holiday.GetRowCellValue(selectRow, "name").ToString();
            if (name != "")
            {
                //if (obj["code"].ToString() != "")
                //{
                e.Cancel = true;
                //  }
            }
        }
        #endregion
        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butContronl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dcHodily.Validate())
                {
                    return;
                }
                dcHodily.GetValue(holidayInfoEntity);
                String param = "?" + PackReflectionEntity<HolidayInfoEntity>.GetEntityToRequestParameters(holidayInfoEntity, true);
                String url = AppContext.AppConfig.serverUrl + "cms/holiday/save" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("保存成功!");
                    HolidaySettingList(AppContext.Session.hospitalId);
                    dcHodily.ClearValue();
                    groupBox1.Enabled = false ;
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
        #region 
        public HolidayInfoEntity holidayInfoEntity { get; set; }
        private void HolidaySettingForm_Load(object sender, EventArgs e)
        {
            dcHodily.DataType = typeof(HolidayInfoEntity);
            if (holidayInfoEntity != null)
            {
                dcHodily.SetValue(holidayInfoEntity);
            }
            else
            {
                holidayInfoEntity = new HolidayInfoEntity();
            }
        }
        #endregion
    }
}
