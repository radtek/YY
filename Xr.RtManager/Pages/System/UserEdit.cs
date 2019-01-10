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
using DevExpress.XtraEditors;
using System.IO;
using System.Net;
using Xr.Common;
using System.Text.RegularExpressions;
using Xr.Http;

namespace Xr.RtManager
{
    public partial class UserEdit : Form
    {
        public UserEdit()
        {
            InitializeComponent();
        }

        public UserEntity user { get; set; }
        private String oldLoginName;
        String filePath = "";
        String serviceFilePath = "";

        CheckBox chkbLast;

        private void UserEdit_Load(object sender, EventArgs e)
        {
            dcUser.DataType = typeof(UserEntity);
            String url = AppContext.AppConfig.serverUrl + "sys/sysOffice/findAll";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<OfficeEntity> officeList = objT["result"].ToObject<List<OfficeEntity>>();
                treeCompany.Properties.DataSource = officeList; //绑定数据           
                treeCompany.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeCompany.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeCompany.Properties.DisplayMember = "name";  //要在树里展示的
                treeCompany.Properties.ValueMember = "id";    //对应的value
                treeCompany.EditValue = "2";

                treeOffice.Properties.DataSource = officeList; //绑定数据           
                treeOffice.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeOffice.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeOffice.Properties.DisplayMember = "name";  //要在树里展示的
                treeOffice.Properties.ValueMember = "id";    //对应的value
                treeOffice.EditValue = "2";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }

            data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysRole/findAll");
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<RoleEntity> roleList = objT["result"].ToObject<List<RoleEntity>>();
                foreach (RoleEntity role in roleList)
                {
                    checkedListBoxControl1.Items.Add(role.id, role.name);
                }
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }

            if (user != null)
            {
                data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysUser/getUser?userId=" + user.id);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    user = objT["result"].ToObject<UserEntity>();
                    dcUser.SetValue(user);
                    oldLoginName = user.loginName;
                    //设置多选框选择
                    String[] strArr = user.roleIds.Split(',');
                    foreach(string str in strArr){
                        for(int i=0; i<checkedListBoxControl1.Items.Count; i++){
                            if(str.Equals(checkedListBoxControl1.Items[i].Value)){
                                checkedListBoxControl1.Items[i].CheckState = CheckState.Checked;
                                break;
                            }
                        }
                    }
                    //显示图片
                    if (user.imgPath != null && user.imgPath.Length > 0)
                    {
                        WebClient web = new WebClient();
                        var bytes = web.DownloadData(user.imgPath);
                        this.pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
                    }
                    //WebClient web = new WebClient();
                    //var bytes = web.DownloadData("http://127.0.0.1:8080/dzkb/uploadFileDir/user_1/2018-12-19/asuo_Splash_0.jpg");
                    //this.pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            else
            {
                user = new UserEntity();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcUser.Validate())
            {
                return;
            }
            if (user.id == null || (user.id != null && !user.password.Equals(tePassword.Text)))
            {
                if (radioGroup1.EditValue.Equals("1"))
                {
                    //密码复杂度验证
                    var regex = new Regex(@"
                        (?=.*[0-9])                     #必须包含数字
                        (?=.*[a-zA-Z])                  #必须包含小写或大写字母
                        (?=([\x21-\x7e]+)[^a-zA-Z0-9])  #必须包含特殊符号
                        .{8,16}                         #至少8个字符，最多16个字符
                        ", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                    if (!regex.IsMatch(tePassword.Text))
                    {
                        dcUser.ShowError(tePassword, "密码数字+字母+符号三种组合以上,至少8位数,最多16位数");
                        return;
                    }
                }
                else
                {
                    //密码复杂度验证
                    var regex = new Regex(@"
                        (?=.*[0-9])                     #必须包含数字
                        (?=.*[a-zA-Z])                  #必须包含小写或大写字母
                        (?=([\x21-\x7e]+)[^a-zA-Z0-9])  #必须包含特殊符号
                        .{6,16}                         #至少6个字符，最多16个字符
                        ", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                    if (!regex.IsMatch(tePassword.Text))
                    {
                        dcUser.ShowError(tePassword, "密码数字+字母+符号三种组合以上,至少6位数,最多16位数");
                        return;
                    }
                }
            }
            if (user.id == null)
            {
                if (!tePassword.Text.Equals(tePassword2.Text))
                {
                    dcUser.ShowError(tePassword2, "密码不一致");
                    return;
                }
            }
            else
            {
                if (tePassword2.Text.Length > 0)
                {
                    if (!tePassword.Text.Equals(tePassword2.Text))
                    {
                        dcUser.ShowError(tePassword2, "密码不一致");
                        return;
                    }
                }
            }

            dcUser.GetValue(user);
            user.imgPath = serviceFilePath;
            //多选框单独进行验证
            string roleIds = string.Empty;
            for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
            {
                if (checkedListBoxControl1.GetItemChecked(i))
                {
                    roleIds = roleIds + checkedListBoxControl1.GetItemValue(i) + ",";
                }
            }
            if (roleIds == string.Empty) { dcUser.ShowError(checkedListBoxControl1, "用户角色至少选一个"); return; }
            roleIds = roleIds.Substring(0, roleIds.Length - 1);
            user.roleIds = roleIds;

            String param = PackReflectionEntity<UserEntity>.GetEntityToRequestParameters(user, true);
            if (oldLoginName != null)
            {
                param = param + "&&oldLoginName=" + oldLoginName;
            }
            String url = AppContext.AppConfig.serverUrl + "sys/sysUser/save?" + param;
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
                    pictureBox1.ImageLocation = filePath; //显示本地图片
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
                string url = AppContext.AppConfig.serverUrl+"common/uploadFile";
                List<FormItemModel> lstPara = new List<FormItemModel>();
                FormItemModel model = new FormItemModel();
                // 文件
                model.Key = "multipartFile";
                int l = filePath.Length;
                int i = filePath.LastIndexOf("\\") + 2;
                model.FileName = filePath.Substring(i, l - i);
                model.FileContent = new FileStream(filePath, FileMode.Open);
                lstPara.Add(model);

                String data = HttpClass.PostForm(url, lstPara);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    WebClient web = new WebClient();
                    var bytes = web.DownloadData(objT["result"][0].ToString());
                    this.pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
                    serviceFilePath = objT["result"].ToString();
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
                MessageBox.Show("请选折要上传的文件");
            }
        }
    }
}
