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
    public partial class ClinicSettingForm : UserControl
    {
        public ClinicSettingForm()
        {
            InitializeComponent();
        }
        #region 查询列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="deptId">所属科室</param>
        public void ClinicSettingList(int pageNo, int pageSize, string deptId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "yyfz/api/clinic/findPage?pageNo=" + pageNo + "&pageSize=" + pageSize + "&deptId=" + deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Clinic.DataSource = objT["result"]["list"].ToObject<List<dynamic>>();
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
        #region 获取指定科室的诊间集合
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId">所属上级科室</param>
        public void ClinicsDepartment(string deptId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "yyfz/api/clinic/findPage?deptId=" + deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Clinic.DataSource = objT["result"]["list"].ToObject<List<dynamic>>();
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
        /// id
        ///hospitalId
        ///deptId
        ///code
        ///name
        ///prefix
        ///isUse
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string id = "";
                string hospitalId = "";
                string deptId = "";
                string code = "";
                string name = "";
                string prefix = "";
                string isUse = "";
                String url = AppContext.AppConfig.serverUrl + "yyfz/api/holiday/save?id=" + id + "&hospitalId=" + hospitalId + "&deptId=" + deptId + "&code=" + code + "&name=" + name + "&prefix=" + prefix + "&isUse=" + isUse;
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
                int selectRow = this.gv_Clinic.GetSelectedRows()[0];
                string name = this.gv_Clinic.GetRowCellValue(selectRow, "name").ToString();
                string id = this.gv_Clinic.GetRowCellValue(selectRow, "id").ToString();
                if (DialogResult.Yes == Xr.Common.MessageBoxUtils.Show("是否删除", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
                {
                    String url = AppContext.AppConfig.serverUrl + "yyfz/api/clinic/delete?ids=" + id;
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
