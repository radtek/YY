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
            SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
            this.gv_Clinic.Appearance.EvenRow.BackColor = Color.FromArgb(150, 237, 243, 254);
            gv_Clinic.Appearance.OddRow.BackColor = Color.FromArgb(150, 199, 237, 204);
            gv_Clinic.OptionsView.EnableAppearanceEvenRow = true;
            gv_Clinic.OptionsView.EnableAppearanceOddRow = true;
            //ClinicSettingList(AppContext.Session.hospitalId,AppContext.Session.deptId);
            #region 注释
            //foreach (var item in AppContext.Session.deptList)
            //{
            //    Xr.Common.Controls.BorderPanelButton bpb = new Xr.Common.Controls.BorderPanelButton();
            //    bpb.BackColor = Color.Transparent;
            //    bpb.BorderColor = Color.Transparent;
            //    bpb.BorderWidth = 1;
            //    bpb.BtnFont = new Font("微软雅黑", 12);
            //    bpb.Dock = DockStyle.Top;
            //    bpb.FillColor1 = Color.Transparent;
            //    bpb.FillColor2 = Color.Transparent;
            //    bpb.Height = 25;
            //    bpb.BtnText = item.name;
            //    bpb.Click += new EventHandler(bpb_Click);
            //    groupBox1.Controls.Add(bpb);
            //}
            #endregion 
            #region 科室列表
            List<Xr.Common.Controls.Item> itemList = new List<Xr.Common.Controls.Item>();
            foreach (DeptEntity dept in AppContext.Session.deptList)
            {
                Xr.Common.Controls.Item item = new Xr.Common.Controls.Item();
                item.name = dept.name;
                item.value = dept.id;
                item.tag = dept.hospitalId;
                itemList.Add(item);
            }
            this.menuControl1.setDataSource(itemList);
            #endregion 
        }
        #region 测试
        public List<ClinicInfoEntity> Date = new List<ClinicInfoEntity>();
        #endregion 
        #region 查询列表
        List<ClinicInfoEntity> clinicInfo;
        #region 
        //public void ClinicSettingList(string hospitalid, string deptId)
        //{
        //    try
        //    {
        //        clinicInfo = new List<ClinicInfoEntity>();
        //        String url = AppContext.AppConfig.serverUrl + "cms/clinic/list?pageNo=" + 1 + "&pageSize=" + 20 +"&hospital.id=" + hospitalid + "&dept.id=" + deptId;
        //        String data = HttpClass.httpPost(url);
        //        JObject objT = JObject.Parse(data);
        //        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
        //        {
        //            if (objT["result"].ToString() == "" || objT["result"].ToString() == "{}")
        //            {
        //                this.gc_Clinic.DataSource = null;
        //                return;
        //            }
        //            clinicInfo = objT["result"]["list"].ToObject<List<ClinicInfoEntity>>();
        //            for (int i = 0; i <Convert.ToInt32(objT["result"]["count"]); i++)
        //            {
        //                String name = objT["result"]["list"][i]["dept"]["name"].ToString();
        //                clinicInfo[i].deptname = name;
        //            }
        //            this.gc_Clinic.DataSource = clinicInfo;
        //            pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
        //            int.Parse(objT["result"]["pageSize"].ToString()),
        //            int.Parse(objT["result"]["pageNo"].ToString()));
        //        }
        //        else
        //        {
        //            MessageBox.Show(objT["message"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogClass.WriteLog("查询列表错误信息：" + ex.Message);
        //    }
        //}
        #endregion 
        public void SearchData(int pageNo, int pageSize, string hospitalId, string deptId)
        {
            try
            {
                clinicInfo = new List<ClinicInfoEntity>();
                String url = AppContext.AppConfig.serverUrl + "cms/clinic/list?pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospital.id=" + hospitalId + "&dept.id=" +deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    if (objT["result"].ToString() == "" || objT["result"].ToString() == "{}")
                    {
                        this.gc_Clinic.DataSource = null;
                        return;
                    }
                    clinicInfo = objT["result"]["list"].ToObject<List<ClinicInfoEntity>>();
                    for (int i = 0; i < Convert.ToInt32(objT["result"]["count"]); i++)
                    {
                        String name = objT["result"]["list"][i]["dept"]["name"].ToString();
                        clinicInfo[i].deptname = name;
                    }
                    this.gc_Clinic.DataSource = clinicInfo;
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
                if (this.radioButton1.Checked)
                {
                    url = AppContext.AppConfig.serverUrl + "cms/clinic/findById?id=" + code;//编号
                }
                else if (this.radioButton2.Checked)
                {
                    url = AppContext.AppConfig.serverUrl + "cms/clinic/findByCode?id=" + code;//编码
                }
                else
                {
                    SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
                    return;
                }
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    if (objT["result"].ToString()=="")
                    {
                        this.gc_Clinic.DataSource = null;
                        return;
                    }
                    clinicInfo = new List<ClinicInfoEntity>();
                    string a = objT["result"].ToString();//Dept
                    ClinicInfoEntity two = Newtonsoft.Json.JsonConvert.DeserializeObject<ClinicInfoEntity>(a);
                    String name = objT["result"]["dept"]["name"].ToString();
                    two.deptname = name;
                    clinicInfo.Add(two);
                    this.gc_Clinic.DataSource = clinicInfo;
                    SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
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
                String url = AppContext.AppConfig.serverUrl + "cms/clinic/findAll?hospital.id=" + hospital + "&dept.id=" + deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    if ( objT["result"].ToString()=="[]")
                    {
                        this.gc_Clinic.DataSource = null;
                        return;
                    }
                    clinicInfo = objT["result"].ToObject<List<ClinicInfoEntity>>();
                    for (int i = 0; i < clinicInfo.Count; i++)
                    {
                        String name = objT["result"][i]["dept"]["name"].ToString();
                        clinicInfo[i].deptname = name;
                    }
                    this.gc_Clinic.DataSource = clinicInfo;
                    SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
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
                groupBox3.Enabled = true;
                dcClinc.ClearValue();
                //var edit = new ClinicSettingEdit();
                //if (edit.ShowDialog() == DialogResult.OK)
                //{
                //    MessageBoxUtils.Hint("保存成功!");
                //    ClinicSettingList(AppContext.Session.hospitalId, AppContext.Session.deptId);
                //    //SearchData(true, 1, pageControl1.PageSize);
                //}
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
                dcClinc.SetValue(selectedRow);
                groupBox3.Enabled = true;
                //var edit = new ClinicSettingEdit();
                //edit.clinicInfoEntity = selectedRow;
                //edit.Text = "诊室修改";
                //if (edit.ShowDialog() == DialogResult.OK)
                //{
                //    MessageBoxUtils.Hint("修改成功!");
                //    ClinicSettingList(AppContext.Session.hospitalId, AppContext.Session.deptId);
                //   // SearchData(true, pageControl1.CurrentPage, pageControl1.PageSize);
                //}
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
                    String param = "?id=" + selectedRow.id;
                    String url = AppContext.AppConfig.serverUrl + "cms/clinic/delete" + param;
                    String data = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        Xr.Common.MessageBoxUtils.Hint("删除成功");
                        SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
                        //ClinicSettingList(AppContext.Session.hospitalId, AppContext.Session.deptId);
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
        #region 编号或者编码查询诊室信息或者查询所有信息
        private void butSelect_Click(object sender, EventArgs e)
        {
            this.groupBox3.Enabled = false;
            SelectInforList(this.teCode.Text.Trim());
        }
        #endregion
        #region 科室列表点击事件 
        private void menuControl1_MenuItemClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            string hospitalId = label.Tag.ToString();
            string deptId = label.Name;
            SearchData(1, pageControl1.PageSize, hospitalId, deptId);
        }
        #endregion 
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butContronl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dcClinc.Validate())
                {
                    return;
                }
                dcClinc.GetValue(clinicInfoEntity);
                String param = "?" + PackReflectionEntity<ClinicInfoEntity>.GetEntityToRequestParameters(clinicInfoEntity, true);
                String url = AppContext.AppConfig.serverUrl + "cms/clinic/save" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("保存成功!");
                    dcClinc.ClearValue();
                    SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
                   // ClinicSettingList(AppContext.Sess, AppContext.Session.hospitalId, AppContext.Session.deptIdion.hospitalId, AppContext.Session.deptId);
                    groupBox3.Enabled = false;
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
        #region 辅助控件
        #region RidionButton控件判断
        #region 单选按钮
        private void gv_Clinic_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "isUse")
            {
                switch (e.Value.ToString().Trim())
                {
                    case "0":
                        e.DisplayText = "启用";//
                        break;
                    case "1":
                        e.DisplayText = "禁用";//
                        break;
                }
            }
        }
        #endregion 
        #region 单选按钮
        bool rdbcheck = false;
        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (rdbcheck)
            {
                radioButton1.Checked = false;
                rdbcheck = false;
            }
            else
            {
                radioButton1.Checked = true;
                rdbcheck = true;
            }
        }
        bool rdbchecks = false;
        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (rdbchecks)
            {
                radioButton2.Checked = false;
                rdbchecks = false;
            }
            else
            {
                radioButton2.Checked = true;
                rdbchecks = true;
            }
        }
        #endregion 
        #endregion
        #region 窗体Load事件
        public ClinicInfoEntity clinicInfoEntity { get; set; }
        private void ClinicSettingForm_Load(object sender, EventArgs e)
        {
            dcClinc.DataType = typeof(ClinicInfoEntity);
            if (clinicInfoEntity != null)
            {
                dcClinc.SetValue(clinicInfoEntity);
            }
            else
            {
                clinicInfoEntity = new ClinicInfoEntity();
            }
        }
        #endregion 
        #endregion 
    }
}
