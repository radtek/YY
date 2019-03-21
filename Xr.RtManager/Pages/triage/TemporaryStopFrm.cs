using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.Common;
using Xr.Http;

namespace Xr.RtManager.Pages.triage
{
    public partial class TemporaryStopFrm : Form
    {
        public TemporaryStopFrm()
        {
            InitializeComponent();
            GetKeShi();
        }
        #region 关闭
        private void buttonControl2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 
        #region 验证诊室是否被占用
        private void treeClinc_EditValueChanged(object sender, EventArgs e)
        {
            if (treeClinc.Text=="选择诊室"||treeClinc.Text=="")
            {
                return;
            }
            if (!CheckIsTrue(AppContext.Session.hospitalId,treeDoctor.EditValue.ToString(),treeKeshi.EditValue.ToString(),treeClinc.EditValue.ToString(),DateTime.Now.ToString("yyyy-MM-dd"),""))
            {
                treeClinc.Text = "选择诊室";
            }
        }
        public bool CheckIsTrue(string hospitalId, string doctorID, string deptId, string clinicId, string workDate, string period)
        {
            bool Check = false;
            try
            {
                if (clinicId != "")
                {
                    String url = AppContext.AppConfig.serverUrl + "sch/doctorSitting/checkIsExist?hospitalId=" + hospitalId + "&doctorId=" + doctorID + "&deptId=" + deptId + "&clinicId=" + clinicId + "&workDate=" + workDate;
                    String data = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        Check = true;
                    }
                    else
                    {
                        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                        Check = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                Log4net.LogHelper.Error("检查当前科室+日期+诊室是否已经存在错误信息:" + ex.Message);
            }
            return Check;
        }
        #endregion 
        #region 获取科室
        public void GetKeShi()
        {
            try
            {
                List<DeptEntity> deptList = AppContext.Session.deptList;
                List<Xr.Common.Controls.Item> itemList = new List<Xr.Common.Controls.Item>();
                foreach (DeptEntity dept in deptList)
                {
                    Xr.Common.Controls.Item item = new Xr.Common.Controls.Item();
                    item.name = dept.name;
                    item.value = dept.id;
                    item.tag = dept.hospitalId;
                    item.parentId = dept.parentId;
                    itemList.Add(item);
                }
                itemList.Insert(0, new Xr.Common.Controls.Item { value = "", tag = "", name = "请选择", parentId = "" });
                treeKeshi.Properties.DataSource = itemList;
                treeKeshi.Properties.TreeList.KeyFieldName = "value";
                treeKeshi.Properties.TreeList.ParentFieldName = "parentId";
                treeKeshi.Properties.DisplayMember = "name";
                treeKeshi.Properties.ValueMember = "value";
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("临时停诊获取科室错误信息："+ex.Message);
            }
        }
        #endregion 
        #region 获取医生
        private void treeKeshi_EditValueChanged(object sender, EventArgs e)
        {
            GetDoctor(treeKeshi.EditValue.ToString());
        }
        public void GetDoctor(string dept)
        {
            try
            {
               List<HospitalInfoEntity>  doctorInfoEntity = new List<HospitalInfoEntity>();
                // 查询医生下拉框数据
                String url = AppContext.AppConfig.serverUrl + "cms/doctor/findAll?hospital.id=" + AppContext.Session.hospitalId + "&dept.id=" + dept;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    doctorInfoEntity = objT["result"].ToObject<List<HospitalInfoEntity>>();
                    doctorInfoEntity.Insert(0, new HospitalInfoEntity { id = "", name = "请选择" });
                    treeDoctor.Properties.DataSource = doctorInfoEntity;
                    treeDoctor.Properties.DisplayMember = "name";
                    treeDoctor.Properties.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("临时停诊获取医生错误信息："+ex.Message);
            }
        }
        #endregion
        #region 获取诊室
        private void treeDoctor_EditValueChanged(object sender, EventArgs e)
        {
            GetClinc(AppContext.Session.hospitalId,treeKeshi.EditValue.ToString());
        }
        public void GetClinc(string hospitalId, string deptId)
        {
            try
            {
                List<ClinicInfoEntity> clinicInfo = new List<ClinicInfoEntity>();
                String url = AppContext.AppConfig.serverUrl + "cms/clinic/list?hospital.id=" + hospitalId + "&dept.id=" + deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    clinicInfo = new List<ClinicInfoEntity>();
                    clinicInfo = objT["result"]["list"].ToObject<List<ClinicInfoEntity>>();
                    ClinicInfoEntity dept = new ClinicInfoEntity();
                    dept.id = "";
                    dept.name = "选择诊室";
                    clinicInfo.Insert(0, dept);
                    treeClinc.Properties.DataSource = clinicInfo;
                    treeClinc.Properties.DisplayMember = "name";
                    treeClinc.Properties.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("临时停诊获取诊室错误信息："+ex.Message);
            }
        }
        #endregion 
        #region 确认
        private void buttonControl1_Click(object sender, EventArgs e)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/temporaryScene?hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + treeKeshi.EditValue + "&doctorId="+treeDoctor.EditValue + "&clinicId="+treeClinc.EditValue;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint(objT["message"].ToString(), this);
                    this.Close();
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("临时坐诊保存错误信息：" + ex.Message);
            }
        }
        #endregion 
    }
}
