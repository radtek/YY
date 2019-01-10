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
    public partial class HospitalSettingsForm : UserControl
    {
        public HospitalSettingsForm()
        {
            InitializeComponent();
        }

        public HospitalInfoEntity hospitalInfo { get; set; }

        private void HospitalSettingsForm_Load(object sender, EventArgs e)
        {
            dcHospitalInfo.DataType = typeof(HospitalInfoEntity);
            //查询医院类型下拉框数据
            String url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=hospital_type";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueHospitalType.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueHospitalType.Properties.DisplayMember = "label";
                lueHospitalType.Properties.ValueMember = "value";
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
            String url = AppContext.AppConfig.serverUrl + "cms/hospital/list?pageNo="+pageNo;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                gcHospitalInfo.DataSource = objT["result"]["list"].ToObject<List<HospitalInfoEntity>>();
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
            groupBox1.Enabled = true;
            hospitalInfo = new HospitalInfoEntity();
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            hospitalInfo = new HospitalInfoEntity();
            var selectedRow = gridView1.GetFocusedRow() as HospitalInfoEntity;
            if (selectedRow == null)
                return;
            String url = AppContext.AppConfig.serverUrl + "cms/hospital/findById?id=" + selectedRow.id;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                hospitalInfo = objT["result"].ToObject<HospitalInfoEntity>();
                dcHospitalInfo.SetValue(hospitalInfo);
                //显示图片
                logoServiceFilePath = hospitalInfo.logoUrl;
                pictureServiceFilePath = hospitalInfo.pictureUrl;
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
            if (!dcHospitalInfo.Validate())
            {
                return;
            }
            dcHospitalInfo.GetValue(hospitalInfo);
            if (logoServiceFilePath == null || logoServiceFilePath.Length == 0)
            {
                dcHospitalInfo.ShowError(pbLogo, "请先上传文件");
                return;
            }
            hospitalInfo.logoUrl = logoServiceFilePath;
            if (pictureServiceFilePath == null || pictureServiceFilePath.Length == 0)
            {
                dcHospitalInfo.ShowError(pbPicture, "请先上传文件");
                return;
            }
            hospitalInfo.pictureUrl = pictureServiceFilePath;
            //请求接口
            String param = "?" + PackReflectionEntity<HospitalInfoEntity>.GetEntityToRequestParameters(hospitalInfo);
            String url = AppContext.AppConfig.serverUrl + "cms/hospital/save" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                MessageBoxUtils.Hint("保存成功！");
                pageControl1_Query(pageControl1.CurrentPage, pageControl1.PageSize);
                groupBox1.Enabled = false;
                //清除值
                dcHospitalInfo.ClearValue();
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
            var selectedRow = gridView1.GetFocusedRow() as HospitalInfoEntity;
            if (selectedRow == null)
                return;
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除吗?", "删除医院信息", messButton);

            if (dr == DialogResult.OK)
            {
                String param = "?id=" + selectedRow.id;
                String url = AppContext.AppConfig.serverUrl + "cms/hospital/delete" + param;
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
            edit.text = hospitalInfo.information;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                hospitalInfo.information = edit.text;
            }
        }
    }
}
