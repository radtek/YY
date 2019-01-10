using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Http;
using System.IO;
using System.Net;
using Xr.Common;

namespace Xr.RtManager
{
    public partial class ClientVersionEdit : Form
    {
        public ClientVersionEdit()
        {
            InitializeComponent();
        }

        String filePath = "";
        String serviceFilePath = "";

        public ClientVersionEntity clientVersion { get; set; }

        private void ClientVersionEdit_Load(object sender, EventArgs e)
        {
            String url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=client_version_type";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueType.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueType.Properties.DisplayMember = "label";
                lueType.Properties.ValueMember = "value";
                if (clientVersion == null) lueType.ItemIndex = 0;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
            dcClientVersion.DataType = typeof(ClientVersionEntity);
            if (clientVersion != null)
            {
                data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/clientVersion/get?id=" + clientVersion.id);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    clientVersion = objT["result"].ToObject<ClientVersionEntity>();
                    serviceFilePath = clientVersion.updateFilePath;
                    String[] strArr = clientVersion.updateFilePath.Split(new char[] { '/' });
                    dcClientVersion.SetValue(clientVersion);
                    teFileName.Text = strArr[strArr.Length - 1];
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            else
            {
                clientVersion = new ClientVersionEntity();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcClientVersion.Validate())
            {
                return;
            }
            dcClientVersion.GetValue(clientVersion);
            if (serviceFilePath == null || serviceFilePath.Length == 0)
            {
                dcClientVersion.ShowError(teFileName, "请先上传文件");
                return;
            }
            clientVersion.updateFilePath = serviceFilePath;
            String param = "?" + PackReflectionEntity<ClientVersionEntity>.GetEntityToRequestParameters(clientVersion);
            String url = AppContext.AppConfig.serverUrl + "sys/clientVersion/save" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "请选择文件";
                fileDialog.Filter = "所有文件(*txt*)|*.*"; //设置要选择的文件的类型
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = fileDialog.FileName;//返回文件的完整路径  
                    teFileName.Text = fileDialog.SafeFileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (filePath != "")
            {
                string url = AppContext.AppConfig.serverUrl + "common/uploadFile";
                List<FormItemModel> lstPara = new List<FormItemModel>();
                FormItemModel model = new FormItemModel();
                // 文件
                model.Key = "multipartFile";
                int l = filePath.Length;
                int i = filePath.LastIndexOf("\\") + 1;
                model.FileName = filePath.Substring(i, l - i);
                model.FileContent = new FileStream(filePath, FileMode.Open);
                lstPara.Add(model);

                String data = HttpClass.PostForm(url, lstPara);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    serviceFilePath = objT["result"][0].ToString();
                    MessageBoxUtils.Hint("上传文件成功");
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选折要上传的文件");
            }
        }
    }
}
