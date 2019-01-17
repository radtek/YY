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
    public partial class ClinicSettingForm : UserControl
    {
        public ClinicSettingForm()
        {
            InitializeComponent();
            //Date = new List<ClinicInfoEntity> { 
            //new ClinicInfoEntity{name="测试",code="10202",parent="测试",showSort="1",printSort="1",createDate="2019-01-08",updateDate="2018-18-08"},
            //};
            //this.gc_Clinic.DataSource = Date;
        }
        #region 测试
        public List<ClinicInfoEntity> Date = new List<ClinicInfoEntity>();
        #endregion 
        #region 查询列表
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="deptId">所属科室</param>
        public void ClinicSettingList(string hospitalid, string deptId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "cms/clinic/list?hospital.id" + hospitalid + "&dept.id=" + deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Clinic.DataSource = objT["result"]["list"].ToObject<List<ClinicInfoEntity>>();
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
                LogClass.WriteLog("查询列表错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 编号或者编码查询诊室信息详情
        /// <summary>
        /// 编号或者编码查询诊室信息详情
        /// </summary>
        /// <param name="code">编号或者编码</param>
        public void SelectInforList(string code)
        {
            try
            {
                String url = "";
                if (radioGroup2.EditValue == "1")
                {
                    url = AppContext.AppConfig.serverUrl + "api/cms/clinic/findById?id=" + code;//编号
                }
                else
                {
                    url = AppContext.AppConfig.serverUrl + "api/cms/clinic/findByCode?id=" + code;//编码
                }
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Clinic.DataSource = objT["result"]["list"].ToObject<List<ClinicInfoEntity>>();
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
        #region 获取指定医院科室下诊室列表
       /// <summary>
        /// 获取指定医院科室下诊室列表
       /// </summary>
       /// <param name="hospital">医院ID</param>
       /// <param name="deptId">科室ID</param>
        public void ClinicsDepartment(string hospital, string deptId)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "api/cms/clinic/findAll?hospital.id=" + hospital + "&dept.id=" + deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Clinic.DataSource = objT["result"]["list"].ToObject<List<ClinicInfoEntity>>();
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
                LogClass.WriteLog("获取指定科室的诊间集合错误信息：" + ex.Message);
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
                var edit = new ClinicSettingEdit();
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    MessageBoxUtils.Hint("保存成功!");
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
                var selectedRow = this.gv_Clinic.GetFocusedRow() as ClinicInfoEntity;
                if (selectedRow == null)
                    return;
                var edit = new ClinicSettingEdit();
                edit.clinicInfoEntity = selectedRow;
                edit.Text = "诊室修改";
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    MessageBoxUtils.Hint("修改成功!");
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
                var selectedRow = gv_Clinic.GetFocusedRow() as ClinicInfoEntity;
                if (selectedRow == null)
                    return;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除诊室", messButton);

                if (dr == DialogResult.OK)
                {
                    String param = "?ids=" + selectedRow.id;
                    String url = AppContext.AppConfig.serverUrl + "yyfz/api/clinic/delete" + param;
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
        #region 编号或者编码查询诊室信息
        private void butSelect_Click(object sender, EventArgs e)
        {
            SelectInforList(this.teCode.Text.Trim());
        }
        #endregion
    }
}
