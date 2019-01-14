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
    public partial class HolidaySettingForm : UserControl
    {
        public HolidaySettingForm()
        {
            InitializeComponent();
            Data = new List<Holiday>
            {
               new Holiday {code="2019", name="春节",StardTime="2019-02-03",EndTime="2019-02-10"},
                 new Holiday {code="2019", name="元宵节",StardTime="2019-02-18",EndTime="2019-02-19"},
            };
            this.gc_Holiday.DataSource = Data;
        }
        public List<Holiday> Data = new List<Holiday>();
        public class Holiday
        {
            public string code { get; set; }
            public string name { get; set; }
            public string StardTime { get; set; }
            public string EndTime { get; set; }
        }
        #region 节假日列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="hospitalId">所属医院</param>
        public void HolidaySettingList(int pageNo, int pageSize, string hospitalId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "yyfz/api/holiday/findPage?pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospitalId=" + hospitalId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Holiday.DataSource = objT["result"]["list"].ToObject<List<Holiday>>();
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
                string name = this.gv_Holiday.GetRowCellValue(Data.Count - 1, "name").ToString();
                gv_Holiday.OptionsBehavior.ReadOnly = false;
                if (name != "")
                {
                    Data.Add(new Holiday { code = "", name = "", StardTime = "", EndTime = "" });
                }
                this.gc_Holiday.DataSource = Data;
                gc_Holiday.RefreshDataSource();
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// id	        主键，修改时不能为空
        ///hospitalId	所属医院
        ///name		    节假日名称
        ///year		    年份
        ///beginDate	开始日期
        ///endDate      结束日期
        ///isUse        是否启用
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string id = "";
                string hospitalId = "";
                string name = "";
                string year = "";
                string beginDate = "";
                string endDate = "";
                string isUse = "";
                String url = AppContext.AppConfig.serverUrl + "yyfz/api/holiday/save?id=" + id + "&hospitalId=" + hospitalId + "&name=" + name + "&year=" + year + "&beginDate=" + beginDate + "&endDate=" + endDate + "&isUse=" + isUse;
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
                int selectRow = gv_Holiday.GetSelectedRows()[0];
                string name = this.gv_Holiday.GetRowCellValue(selectRow, "name").ToString();
                string id = this.gv_Holiday.GetRowCellValue(selectRow, "id").ToString();
                if (DialogResult.Yes == Xr.Common.MessageBoxUtils.Show("是否删除" + name + "节假日", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
                {
                    String url = AppContext.AppConfig.serverUrl + "yyfz/api/holiday/delete?ids=" + id;
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
    }
}
