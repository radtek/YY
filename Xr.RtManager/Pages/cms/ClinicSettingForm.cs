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
using Xr.RtManager.Module.cms;

namespace Xr.RtManager.Pages.cms
{
    public partial class ClinicSettingForm : UserControl
    {
        private Form MainForm; //主窗体
        Xr.Common.Controls.OpaqueCommand cmd;
        
        public ClinicSettingForm()
        {
            InitializeComponent();
            MainForm = (Form)this.Parent;
            pageControl1.MainForm = MainForm;
            cmd = new Xr.Common.Controls.OpaqueCommand(AppContext.Session.waitControl);
            cmd.ShowOpaqueLayer(225, false);
            SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
            cmd.HideOpaqueLayer();
            deptId = "";
            #region 科室列表
            GetKeShiList();
            //List<Xr.Common.Controls.Item> itemList = new List<Xr.Common.Controls.Item>();
            //foreach (DeptEntity dept in AppContext.Session.deptList)
            //{
            //    Xr.Common.Controls.Item item = new Xr.Common.Controls.Item();
            //    item.name = dept.name;
            //    item.value = dept.id;
            //    item.tag = dept.hospitalId;
            //    item.parentId = dept.parentId;
            //    itemList.Add(item);
            //}
            //this.menuControl1.setDataSource(itemList);
            //menuControl1.EditValue(AppContext.Session.deptId);
            //treeWKe.Properties.DataSource = AppContext.Session.deptList;
            //treeWKe.Properties.TreeList.KeyFieldName = "id";
            //treeWKe.Properties.TreeList.ParentFieldName = "parentId";
            //treeWKe.Properties.DisplayMember = "name";
            //treeWKe.Properties.ValueMember = "id";
            #endregion 
        }
        #region 查询科室列表
        public void GetKeShiList()
        {
            try
            {
                String param = "hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
                String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?" + param;
                  String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                  List<DeptEntity> list=objT["result"].ToObject<List<DeptEntity>>();
                  List<Xr.Common.Controls.Item> itemList = new List<Xr.Common.Controls.Item>();
                  foreach (DeptEntity dept in list)
                  {
                      Xr.Common.Controls.Item item = new Xr.Common.Controls.Item();
                      item.name = dept.name;
                      item.value = dept.id;
                      item.tag = dept.hospitalId;
                      item.parentId = dept.parentId;
                      itemList.Add(item);
                  }
                  this.menuControl1.setDataSource(itemList);
                  menuControl1.EditValue(AppContext.Session.deptId);
                  treeWKe.Properties.DataSource = itemList;
                  treeWKe.Properties.TreeList.KeyFieldName = "value";
                  treeWKe.Properties.TreeList.ParentFieldName = "parentId";
                  treeWKe.Properties.DisplayMember = "name";
                  treeWKe.Properties.ValueMember = "value";
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, MainForm);
                Log4net.LogHelper.Error("诊室设置下查询科室列表信息错误："+ex.Message);
            }
        }
        #endregion 
        #region 测试
        public List<ClinicInfoEntity> Date = new List<ClinicInfoEntity>();
        #endregion 
        #region 查询列表
        List<ClinicInfoEntity> clinicInfo;
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
                    for (int i = 0; i < clinicInfo.Count; i++)//Convert.ToInt32(objT["result"]["count"])
                    {
                        String name = objT["result"]["list"][i]["dept"]["name"].ToString();
                        clinicInfo[i].deptname = name;
                    }
                    this.gc_Clinic.DataSource = clinicInfo;
                   
                    pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                    int.Parse(objT["result"]["pageSize"].ToString()),
                    int.Parse(objT["result"]["pageNo"].ToString()));
                    cmd.HideOpaqueLayer();
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                }
            }
            catch (Exception ex)
            {
                cmd.HideOpaqueLayer();
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, MainForm);
                Log4net.LogHelper.Error("查询科室列表错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 编号或者编码查询诊室信息详情
        /// <summary>
        /// 查询诊室信息详情
        /// </summary>
        /// <param name="code"></param>
        public void SelectInforList(string deptId)
        {
            SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, deptId);
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
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("获取指定医院科室下诊室列表错误信息：" + ex.Message);
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
               // radioGroup2.SelectedIndex = 0;
                var selectedRow =new ClinicInfoEntity();
                selectedRow.deptId = deptId;
                selectedRow.isUse="0";
                dcClinc.SetValue(selectedRow);

            }
            catch (Exception ex)
            {
                //radioGroup2.SelectedIndex = 0;
                var selectedRow = new ClinicInfoEntity();
                selectedRow.deptId = deptId;
                selectedRow.isUse = "0";
                dcClinc.SetValue(selectedRow);
                Log4net.LogHelper.Error("诊室新增错误信息：" + ex.Message);
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
                selectedRow.deptId =string.Join(",", from n in AppContext.Session.deptList where n.name == selectedRow.deptname select n.id);
                dcClinc.SetValue(selectedRow);
                groupBox3.Enabled = true;
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("诊室修改错误信息：" + ex.Message);
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
                DialogResult dr = MessageBoxUtils.Show("确定要删除吗?", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm);
                if (dr == DialogResult.OK)
                {
                    String param = "?id=" + selectedRow.id;
                    String url = AppContext.AppConfig.serverUrl + "cms/clinic/delete" + param;
                    String data = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        Xr.Common.MessageBoxUtils.Hint("删除成功", MainForm);
                        if (deptId != "")
                        {
                            SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, deptId);
                        }
                        else
                        {
                            SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
                        }
                        //SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
                    }
                    else
                    {
                        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, MainForm);
                Log4net.LogHelper.Error("诊室删除错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 编号或者编码查询诊室信息或者查询所有信息
        private void butSelect_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.ShowOpaqueLayer(255, true);
                SelectInforList(deptId);
                this.groupBox3.Enabled = false;
                dcClinc.ClearValue();
            }
            catch
            {
                
            }           
        }
        #endregion
        #region 科室列表点击事件 
        string deptId = "";
        private void menuControl1_MenuItemClick(object sender, EventArgs e)
        {
            cmd.ShowOpaqueLayer(255, true);
            Label label = null;
            if (typeof(Label).IsInstanceOfType(sender))
            {
                label = (Label)sender;
            }
            else
            {
                Xr.Common.Controls.PanelEx panelEx = (Xr.Common.Controls.PanelEx)sender;
                label = (Label)panelEx.Controls[0];
            }
            string hospitalId = label.Tag.ToString();
            if (label.Name != deptId)
            {
                this.groupBox3.Enabled = false;
                dcClinc.ClearValue();
            }
            deptId = label.Name;
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
                    MessageBoxUtils.Hint("保存成功!", MainForm);
                    if (deptId != "")
                    {
                        SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, deptId);
                    }
                    else
                    {
                        SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
                    }
                    groupBox3.Enabled = false;
                    dcClinc.ClearValue();
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, MainForm);
                Log4net.LogHelper.Error("诊室保存错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 辅助控件
        #region 控件判断
        #region 显示格式
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
        #region 加粗GroupBox线条颜色
        /// <summary>
        /// 画边框线条颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
           // GroupBox gBox = (GroupBox)sender;
           // //e.Graphics.Clear(gBox.BackColor);
           ////e.Graphics.DrawString(gBox.Text, gBox.Font, Brushes.Gray, 10, 1);
           // var vSize = e.Graphics.MeasureString(gBox.Text, gBox.Font);
           // e.Graphics.DrawLine(Pens.Gray, 1, vSize.Height / 2, 8, vSize.Height / 2);
           // e.Graphics.DrawLine(Pens.Gray, vSize.Width + 8, vSize.Height / 2, gBox.Width - 2, vSize.Height / 2);
           // e.Graphics.DrawLine(Pens.Gray, 1, vSize.Height / 2, 1, gBox.Height - 2);
           // e.Graphics.DrawLine(Pens.Gray, 1, gBox.Height - 2, gBox.Width - 2, gBox.Height - 2);
           // e.Graphics.DrawLine(Pens.Gray, gBox.Width - 2, vSize.Height / 2, gBox.Width - 2, gBox.Height - 2);
        }
        #endregion 
        #region 分页跳转事件
        private void pageControl1_Query(int CurrentPage, int PageSize)
        {
            if (deptId != "")
            {
                cmd.ShowOpaqueLayer(225, false);
                SearchData(CurrentPage, PageSize, AppContext.Session.hospitalId, deptId);
            }
            else
            {
                cmd.ShowOpaqueLayer(225, false);
                SearchData(CurrentPage, PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
            }
          //  SearchData(CurrentPage,PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
        }
        #endregion 
        #endregion
    }
}
