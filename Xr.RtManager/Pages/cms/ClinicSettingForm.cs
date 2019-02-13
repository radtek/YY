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
        Xr.Common.Controls.OpaqueCommand cmd;
        public ClinicSettingForm()
        {
            InitializeComponent();
            cmd = new Xr.Common.Controls.OpaqueCommand(AppContext.Session.waitControl);
            cmd.ShowOpaqueLayer(225, false);
            SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
            cmd.HideOpaqueLayer();
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
                item.parentId = dept.parentId;
                itemList.Add(item);
            }
            this.menuControl1.setDataSource(itemList);
            treeWKe.Properties.DataSource = AppContext.Session.deptList;
            treeWKe.Properties.TreeList.KeyFieldName = "id";
            treeWKe.Properties.TreeList.ParentFieldName = "parentId";
            treeWKe.Properties.DisplayMember = "name";
            treeWKe.Properties.ValueMember = "id";
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
        /// 查询诊室信息详情
        /// </summary>
        /// <param name="code"></param>
        public void SelectInforList()
        {
            SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
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
                selectedRow.deptId =string.Join(",", from n in AppContext.Session.deptList where n.name == selectedRow.deptname select n.id);
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
            SelectInforList();
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
                    SearchData(1, pageControl1.PageSize, AppContext.Session.hospitalId, AppContext.Session.deptId);
                   // ClinicSettingList(AppContext.Sess, AppContext.Session.hospitalId, AppContext.Session.deptIdion.hospitalId, AppContext.Session.deptId);
                    groupBox3.Enabled = false;
                    dcClinc.ClearValue();
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
        #endregion
    }
}
