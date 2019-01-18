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
using System.IO;
using System.Net;
using Xr.Common;
using Xr.Common.Controls;

namespace Xr.RtManager.Pages.cms
{
    public partial class DeptSettingsForm : UserControl
    {
        public DeptSettingsForm()
        {
            InitializeComponent();
        }

        public DeptInfoEntity deptInfo { get; set; }

        private void DeptSettingsForm_Load(object sender, EventArgs e)
        {
            dcDeptInfo.DataType = typeof(DeptInfoEntity);
            //查询医院下拉框数据
            String url = AppContext.AppConfig.serverUrl + "cms/hospital/findAll";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueHospital.Properties.DataSource = objT["result"].ToObject<List<HospitalInfoEntity>>();
                lueHospital.Properties.DisplayMember = "name";
                lueHospital.Properties.ValueMember = "id";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
            
            //查询宣传显示下拉框数据
            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=show_hide";
            data = HttpClass.httpPost(url);
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueIsShow.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueIsShow.Properties.DisplayMember = "label";
                lueIsShow.Properties.ValueMember = "value";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
            //查询状态下拉框数据
            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=is_use";
            data = HttpClass.httpPost(url);
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueIsUse.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueIsUse.Properties.DisplayMember = "label";
                lueIsUse.Properties.ValueMember = "value";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
            SearchData(1, pageControl1.PageSize);
        }

        public void SearchData(int pageNo, int pageSize)
        {
            String param = "pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
            String url = AppContext.AppConfig.serverUrl + "cms/dept/list?"+param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                gcDeptInfo.DataSource = objT["result"]["list"].ToObject<List<DeptInfoEntity>>();
                pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                int.Parse(objT["result"]["pageSize"].ToString()),
                int.Parse(objT["result"]["pageNo"].ToString()));
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void pageControl1_Query(int CurrentPage, int PageSize)
        {
            SearchData(CurrentPage, PageSize);
        }



        #region 选择与上传图片
        String logoFilePath = "";
        String logoServiceFilePath = "";
        
        private void buttonControl1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "请选择文件";
                fileDialog.Filter = "所有文件(*txt*)|*.*"; //设置要选择的文件的类型
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    logoFilePath = fileDialog.FileName;//返回文件的完整路径   
                    pbLogo.ImageLocation = logoFilePath; //显示本地图片
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (logoFilePath != "")
            {
                string url = AppContext.AppConfig.serverUrl + "common/uploadFile";
                List<FormItemModel> lstPara = new List<FormItemModel>();
                FormItemModel model = new FormItemModel();
                // 文件
                model.Key = "multipartFile";
                int l = logoFilePath.Length;
                int i = logoFilePath.LastIndexOf("\\") + 2;
                model.FileName = logoFilePath.Substring(i, l - i);
                model.FileContent = new FileStream(logoFilePath, FileMode.Open);
                lstPara.Add(model);

                String data = HttpClass.PostForm(url, lstPara);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    WebClient web = new WebClient();
                    var bytes = web.DownloadData(objT["result"][0].ToString());
                    this.pbLogo.Image = Bitmap.FromStream(new MemoryStream(bytes));
                    logoServiceFilePath = objT["result"][0].ToString();
                    MessageBoxUtils.Hint("上传图片成功");
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择要上传的文件");
            }
        }

        
        String pictureFilePath = "";
        String pictureServiceFilePath = "";
        private void buttonControl3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "请选择文件";
                fileDialog.Filter = "所有文件(*txt*)|*.*"; //设置要选择的文件的类型
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureFilePath = fileDialog.FileName;//返回文件的完整路径   
                    pbPicture.ImageLocation = pictureFilePath; //显示本地图片
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonControl2_Click(object sender, EventArgs e)
        {
            if (pictureFilePath != "")
            {
                string url = AppContext.AppConfig.serverUrl + "common/uploadFile";
                List<FormItemModel> lstPara = new List<FormItemModel>();
                FormItemModel model = new FormItemModel();
                // 文件
                model.Key = "multipartFile";
                int l = pictureFilePath.Length;
                int i = pictureFilePath.LastIndexOf("\\") + 2;
                model.FileName = pictureFilePath.Substring(i, l - i);
                model.FileContent = new FileStream(pictureFilePath, FileMode.Open);
                lstPara.Add(model);

                String data = HttpClass.PostForm(url, lstPara);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    WebClient web = new WebClient();
                    var bytes = web.DownloadData(objT["result"][0].ToString());
                    this.pbPicture.Image = Bitmap.FromStream(new MemoryStream(bytes));
                    pictureServiceFilePath = objT["result"][0].ToString();
                    MessageBoxUtils.Hint("上传图片成功");
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择要上传的文件");
            }
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //清除值
            dcDeptInfo.ClearValue();
            pbLogo.Image = null;
            pbLogo.Refresh();
            logoServiceFilePath = null;
            pbPicture.Image = null;
            pbPicture.Refresh();
            pictureServiceFilePath = null;

            groupBox1.Enabled = true;
            deptInfo = new DeptInfoEntity();
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            //清除值
            dcDeptInfo.ClearValue();
            pbLogo.Image = null;
            pbLogo.Refresh();
            logoServiceFilePath = null;
            pbPicture.Image = null;
            pbPicture.Refresh();
            pictureServiceFilePath = null;

            deptInfo = new DeptInfoEntity();
            var selectedRow = gridView1.GetFocusedRow() as DeptInfoEntity;
            if (selectedRow == null)
                return;
            String url = AppContext.AppConfig.serverUrl + "cms/dept/findById?id=" + selectedRow.id;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                deptInfo = objT["result"].ToObject<DeptInfoEntity>();
                deptInfo.hospitalId = deptInfo.hospital.id;
                deptInfo.parentId = deptInfo.parent.id;
                dcDeptInfo.SetValue(deptInfo);
                //显示图片
                logoServiceFilePath = deptInfo.logoUrl;
                pictureServiceFilePath = deptInfo.pictureUrl;
                WebClient web;
                if (logoServiceFilePath != null && logoServiceFilePath.Length > 0)
                {
                    web = new WebClient();
                    var bytes = web.DownloadData(logoServiceFilePath);
                    this.pbLogo.Image = Bitmap.FromStream(new MemoryStream(bytes));
                }
                if (pictureServiceFilePath != null && pictureServiceFilePath.Length > 0)
                {
                    web = new WebClient();
                    var bytes = web.DownloadData(pictureServiceFilePath);
                    this.pbPicture.Image = Bitmap.FromStream(new MemoryStream(bytes));
                }
                groupBox1.Enabled = true;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //检验并取值
            if (!dcDeptInfo.Validate())
            {
                return;
            }
            dcDeptInfo.GetValue(deptInfo);

            if (logoServiceFilePath == null || logoServiceFilePath.Length == 0)
            {
                dcDeptInfo.ShowError(pbLogo, "请先上传文件");
                return;
            }
            deptInfo.logoUrl = logoServiceFilePath;
            if (pictureServiceFilePath == null || pictureServiceFilePath.Length == 0)
            {
                dcDeptInfo.ShowError(pbPicture, "请先上传文件");
                return;
            }
            deptInfo.pictureUrl = pictureServiceFilePath;
            
            //请求接口
            String param = "?" + PackReflectionEntity<DeptInfoEntity>.GetEntityToRequestParameters(deptInfo, true);
            String url = AppContext.AppConfig.serverUrl + "cms/dept/save" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                MessageBoxUtils.Hint("保存成功！");
                pageControl1_Query(pageControl1.CurrentPage, pageControl1.PageSize);
                groupBox1.Enabled = false;
                //清除值
                dcDeptInfo.ClearValue();
                pbLogo.Image = null;
                pbLogo.Refresh();
                logoServiceFilePath = null;
                pbPicture.Image = null;
                pbPicture.Refresh();
                pictureServiceFilePath = null;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as DeptInfoEntity;
            if (selectedRow == null)
                return;
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除吗?", "删除科室信息", messButton);

            if (dr == DialogResult.OK)
            {
                String param = "?id=" + selectedRow.id;
                String url = AppContext.AppConfig.serverUrl + "cms/dept/delete" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("删除成功!");
                    SearchData(pageControl1.CurrentPage, pageControl1.PageSize);
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var edit = new RichEditorForm();
            edit.text = deptInfo.synopsis;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                deptInfo.synopsis = edit.text;
            }
        }

        private void lueHospital_EditValueChanged(object sender, EventArgs e)
        {
            if (lueHospital.EditValue == null || lueHospital.EditValue.ToString().Length == 0)
            {
                lueParentDept.Properties.DataSource = null;
                return;
            }
            HospitalInfoEntity hospitalInfo = lueHospital.GetSelectedDataRow() as HospitalInfoEntity;
            //查询上级科室下拉框数据
            String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?hospital.code=" + hospitalInfo.code;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DeptEntity> deptLsit = objT["result"].ToObject<List<DeptEntity>>();
                DeptEntity dept = new DeptEntity();
                dept.id = "0";
                dept.name = "无";
                deptLsit.Insert(0, dept);
                lueParentDept.Properties.DataSource = deptLsit;
                lueParentDept.Properties.DisplayMember = "name";
                lueParentDept.Properties.ValueMember = "id";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }
    }
}
