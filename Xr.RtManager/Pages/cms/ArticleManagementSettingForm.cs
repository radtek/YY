﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Xr.Http;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using Xr.RtManager.Module.cms;
using Xr.Common.Controls;
using Xr.Common;

namespace Xr.RtManager.Pages.cms
{
    public partial class ArticleManagementSettingForm : UserControl
    {
        public ArticleManagementSettingForm()
        {
            InitializeComponent();
            First();
            ArticleCategoryCollection();
        }
        #region 类型设置
        List<FirstOne> firs;
        public void First()
        {
            try
            {
                //类型设置
                firs = new List<FirstOne>();
                firs.Add(new FirstOne { id = "", name = "全部" });
                firs.Add(new FirstOne { id = "1", name = "医院" });
                firs.Add(new FirstOne { id = "2", name = "科室" });
                firs.Add(new FirstOne { id = "3", name = "医生" });
                this.lueType.Properties.DataSource = firs;
                lueType.Properties.DisplayMember = "name";
                lueType.Properties.ValueMember = "id";
                this.lookUpEdit1.Properties.DataSource = firs;
                lookUpEdit1.Properties.DisplayMember = "name";
                lookUpEdit1.Properties.ValueMember = "id";
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region 文章栏目
        List<ArticleInfoEntity> list;
        dynamic Woqu;
        #region 文章类别集合
        /// <summary>
        /// 文章类别集合
        /// </summary>
        public void ArticleCategoryCollection()
        {
            try
            {
                list = new List<ArticleInfoEntity>();
                String url = AppContext.AppConfig.serverUrl + "cms/articleCategory/findAll?hospitalId=" + AppContext.Session.hospitalId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    if (objT["result"].ToString() == "" || objT["result"].ToString() == "{}")
                    {
                        this.gc_Article.DataSource = null;
                        return;
                    }
                    Woqu = objT;
                    list = objT["result"].ToObject<List<ArticleInfoEntity>>();
                    //ArticleInfoEntity two = Newtonsoft.Json.JsonConvert.DeserializeObject<ArticleInfoEntity>(objT["result"].ToString());
                    //list.Add(two);
                    for (int i = 0; i < list.Count; i++)
                    {
                        switch (list[i].isUse)
                        {
                            case "0":
                                list[i].isUse = "启用";
                                break;
                            case "1":
                                list[i].isUse = "禁用";
                                break;
                        }
                    }
                    this.gc_Article.DataSource = list;
                    gc_Article.RefreshDataSource();
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                ArticleInfoEntity two = Newtonsoft.Json.JsonConvert.DeserializeObject<ArticleInfoEntity>(Woqu["result"].ToString());
                list.Add(two);
                for (int i = 0; i < list.Count; i++)
                {
                    switch (list[i].isUse)
                    {
                        case "0":
                            list[i].isUse = "启用";
                            break;
                        case "1":
                            list[i].isUse = "禁用";
                            break;
                    }
                }
                this.gc_Article.DataSource = list;
                gc_Article.RefreshDataSource();
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
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id == "" || list[i].id == null)
                    {
                        return;
                    }
                }
                list.Insert(0, new ArticleInfoEntity { createById = "", createByName = "", createDate = "", hospitalId = "", id = "", isUse = "", name = "", updateById = "", updateByName = "", updateDate = "" });
                this.gc_Article.DataSource = list;
                gc_Article.RefreshDataSource();
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
                var selectedRow = gv_Article.GetFocusedRow() as ArticleInfoEntity;
                if (selectedRow == null)
                    return;
                String param = "";
                if (selectedRow.isUse == "启用")
                {
                    param = "?" + "hospitalId=" + AppContext.Session.hospitalId + "&id=" + selectedRow.id + "&name=" + selectedRow.name + "&isUse=" + "0";
                }
                else
                {
                    param = "?" + "hospitalId=" + AppContext.Session.hospitalId + "&id=" + selectedRow.id + "&name=" + selectedRow.name + "&isUse=" + "1";
                }
                String url = AppContext.AppConfig.serverUrl + "cms/articleCategory/save" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("保存成功!");
                    ArticleCategoryCollection();
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
                if (click==1)
                {
                    if (CickInfo != null)
                    {
                        String param = "?" + "id=" + CickInfo.id;
                        String url = AppContext.AppConfig.serverUrl + "cms/articleCategory/delete" + param;
                        String data = HttpClass.httpPost(url);
                        JObject objT = JObject.Parse(data);
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            MessageBoxUtils.Hint("删除成功!");
                            ArticleCategoryCollection();
                        }
                        else
                        {
                            MessageBox.Show(objT["message"].ToString());
                        }
                    }
                }
                else if (click == 2)
                {
                    if (CickInfo != null)
                    {
                        String param = "?" + "id=" + CickInfo.id;
                        String url = AppContext.AppConfig.serverUrl + "cms/article/delete" + param;
                        String data = HttpClass.httpPost(url);
                        JObject objT = JObject.Parse(data);
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            MessageBoxUtils.Hint("删除成功!");
                            groupBox3.Enabled = false;
                            SelectInfoPage(1, pageControl1.PageSize);
                            dcArticle.ClearValue();
                        }
                        else
                        {
                            MessageBox.Show(objT["message"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #endregion
        #region 文章
        #region 获取科室信息
        #region 
        List<TreeList> listoffice;
        List<HospitalInfoEntity> doctorInfoEntity;
        #endregion
        private void lueType_EditValueChanged(object sender, EventArgs e)
        {
            switch (lueType.Text.Trim())
            {
                case "科室":
                    GetDoctorAndDepartment(AppContext.AppConfig.deptCode);
                    break;
                case "医生":
                    Doc = 1;
                    SelectDoctor(AppContext.Session.deptId);
                    break;
            }
           
        }
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            switch (lookUpEdit1.Text.Trim())
            {
                case "科室":
                    GetDoctorAndDepartment(AppContext.AppConfig.deptCode);
                    break;
                case "医生":
                    Doc = 2;
                    SelectDoctor(AppContext.Session.deptId);
                    break;
            }
        }
        public void GetDoctorAndDepartment(string code)
        {
            try
            {
                listoffice = new List<TreeList>();
                //查询科室下拉框数据
                String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + code;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    listoffice = objT["result"].ToObject<List<TreeList>>();
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
                listoffice.Add(new TreeList { id = "", parentId = "", name = "全部" });
                treeKeshi.Properties.DataSource = listoffice;
                treeKeshi.Properties.TreeList.KeyFieldName = "id";
                treeKeshi.Properties.TreeList.ParentFieldName = "parentId"; 
                treeKeshi.Properties.DisplayMember = "name"; 
                treeKeshi.Properties.ValueMember = "id"; 
                treeWKe.Properties.DataSource = listoffice;
                treeWKe.Properties.TreeList.KeyFieldName = "id";
                treeWKe.Properties.TreeList.ParentFieldName = "parentId";
                treeWKe.Properties.DisplayMember = "name";
                treeWKe.Properties.ValueMember = "id";
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 
        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl1_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox3.Enabled = false;
                SelectInfoPage(1,pageControl1.PageSize);
                dcArticle.ClearValue();
            }
            catch (Exception ex)
            {
            }
        }
        #region 查询文章
        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        public void SelectInfoPage(int pageNo, int pageSize) 
        {
            try
            {
                //科室主键<type为2时必填>  医生主键<type为3时必填>  类型：1医院文章、2科室文章、3医生文章 from p in listUser where p.Age > 12 && p.Address == "上海" select p listoffice doctorInfoEntity firs
                #region 
                switch (lueType.Text.Trim())
                {
                    case "科室":
                        if (treeKeshi.Text.Trim() == "" || treeKeshi.Text.Trim() == "全部")
                        {
                            MessageBox.Show("当类型为科室时,科室不能为空");
                        }
                        break;
                    case "医生":
                        if (luDoctords.Text.Trim() == "" || luDoctords.Text.Trim() == "全部")
                        {
                            MessageBox.Show("当类型为医生时,医生不能为空");
                        }
                        break;
                }
                #endregion 
                String url = AppContext.AppConfig.serverUrl + "cms/article?pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + string.Join(",", from p in listoffice where p.name == treeKeshi.Text.Trim() select p.id) + "&doctorId=" + string.Join(",",from d in doctorInfoEntity where d.name == luDoctords.Text.Trim() select d.id )+ "&type=" + string.Join(",",from s in firs where s.name == lueType.Text.Trim() select s.id )+ "&categoryId=" + "" + "&title=" + txtTitle.Text.Trim();
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    if (objT["result"].ToString() == "" || objT["result"].ToString() == "{}")
                    {
                        this.gc_Atrlices.DataSource = null;
                        return;
                    }
                    this.gc_Atrlices.DataSource = objT["result"]["list"].ToObject<List<Article>>(); 
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
        #endregion
        #endregion
        #region 编辑文章信息
        public Article clinicInfoEntity { get; set; }
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
                if (!dcArticle.Validate())
                {
                    return;
                }
                dcArticle.GetValue(clinicInfoEntity);
                #region 
                switch (clinicInfoEntity.type)
                {
                    case "2":
                        if (clinicInfoEntity.deptId == "")
                        {
                            MessageBoxUtils.Hint("请选择科室!");
                            return;
                        }
                        break;
                    case "3":
                        if (clinicInfoEntity.doctorId == "")
                        {
                            MessageBoxUtils.Hint("请选择医生!");
                            return;
                        }
                        break;
                }
                #endregion 
                String param = "?" + PackReflectionEntity<Article>.GetEntityToRequestParameters(clinicInfoEntity,true);
                String url = AppContext.AppConfig.serverUrl + "cms/article/save" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("保存成功!");
                    groupBox3.Enabled = false;
                    SelectInfoPage(1, pageControl1.PageSize);
                    dcArticle.ClearValue();
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                dcArticle.ClearValue();
            }
        }
        #endregion
        #region 设置显示信息
        /// <summary>
        /// 设置显示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Article_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "isUse")
            {
                switch (e.Value.ToString())
                {
                    case "0":
                        e.DisplayText = "启用";
                        break;
                    case "1":
                        e.DisplayText = "禁用";
                        break;
                }
            }
        }
        private void gv_Atrlices_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "isUse")
            {
                switch (e.Value.ToString())
                {
                    case "0":
                        e.DisplayText = "启用";
                        break;
                    case "1":
                        e.DisplayText = "禁用";
                        break;
                }
            }
            if (e.Column.FieldName == "type")//类型：1医院文章、2科室文章、3医生文章
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.DisplayText = "医院文章";
                        break;
                    case "2":
                        e.DisplayText = "科室文章";
                        break;
                    case "3":
                        e.DisplayText = "医生文章";
                        break;
                }
            }
        }
        #endregion
        #region 把值赋值到下面
        /// <summary>
        /// 把值赋值到下面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gc_Atrlices_Click(object sender, EventArgs e)
        {
            click = 2;
            var selectedRow = gv_Atrlices.GetFocusedRow() as Article;
            CickInfo = selectedRow;
            if (selectedRow == null)
                return;
            dcArticle.SetValue(selectedRow);
            groupBox3.Enabled = true;
        }
        #endregion 
        #endregion
        #region 窗体Load
        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArticleManagementSettingForm_Load(object sender, EventArgs e)
        {
            dcArticle.DataType = typeof(Article);
            if (clinicInfoEntity != null)
            {
                dcArticle.SetValue(clinicInfoEntity);
            }
            else
            {
                clinicInfoEntity = new Article();
            }
        }
        #endregion 
        #region 记录点击列表事件
        int click = 0;
        dynamic CickInfo;
        private void gc_Article_Click(object sender, EventArgs e)
        {
                click = 1;
                CickInfo = gv_Article.GetFocusedRow() as ArticleInfoEntity;
        }
        #endregion
        #region 点击科室时获取去当前科室下的医生
        int Doc = 0;
        private void treeKeshi_EditValueChanged(object sender, EventArgs e)
        {
            Doc = 1;
            SelectDoctor(treeKeshi.EditValue.ToString());
        }
        private void treeWKe_EditValueChanged(object sender, EventArgs e)
        {
            Doc = 2;
            SelectDoctor(treeWKe.EditValue.ToString());
        }
        /// <summary>
        /// 获取当前科室医生
        /// </summary>
        /// <param name="dept"></param>
        public void SelectDoctor(string dept)
        {
            try
            {
                doctorInfoEntity = new List<HospitalInfoEntity>();
                // 查询医生下拉框数据
                String url = AppContext.AppConfig.serverUrl + "cms/doctor/findAll?hospital.id=" + AppContext.Session.hospitalId + "&dept.id=" + dept;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    doctorInfoEntity = objT["result"].ToObject<List<HospitalInfoEntity>>();
                    doctorInfoEntity.Insert(0, new HospitalInfoEntity { id = "", name = "全部" });
                    if (Doc == 1)
                    {
                        luDoctords.Properties.DataSource = doctorInfoEntity;
                        luDoctords.Properties.DisplayMember = "name";
                        luDoctords.Properties.ValueMember = "id";

                    }
                    else
                    {
                        luDoctor.Properties.DataSource = doctorInfoEntity;
                        luDoctor.Properties.DisplayMember = "name";
                        luDoctor.Properties.ValueMember = "id";
                    }
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 
        #region  新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl2_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox3.Enabled = true;
                dcArticle.ClearValue();
            }
            catch (Exception)
            {
            }
        }
        #endregion 
        #region 
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gBox = (GroupBox)sender;
            var vSize = e.Graphics.MeasureString(gBox.Text, gBox.Font);
            e.Graphics.DrawLine(Pens.Gray, 1, vSize.Height / 2, 8, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Gray, vSize.Width + 8, vSize.Height / 2, gBox.Width - 2, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Gray, 1, vSize.Height / 2, 1, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Gray, 1, gBox.Height - 2, gBox.Width - 2, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Gray, gBox.Width - 2, vSize.Height / 2, gBox.Width - 2, gBox.Height - 2);
        }
        #endregion 
    }
}