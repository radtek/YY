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
using System.Threading;

namespace Xr.RtManager
{
    public partial class UserEdit : Form
    {
        public UserEdit()
        {
            InitializeComponent();
        }

        Xr.Common.Controls.OpaqueCommand cmd;
        public UserEntity user { get; set; }
        private String oldLoginName;
        String filePath = "";
        String serviceFilePath = "";

        CheckBox chkbLast;

        private void UserEdit_Load(object sender, EventArgs e)
        {
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            cmd.ShowOpaqueLayer(0f);
            richEditor1.ImagUploadUrl = AppContext.AppConfig.serverUrl;
            dcUser.DataType = typeof(UserEntity);
            richEditor1.ImagUploadUrl = AppContext.AppConfig.serverUrl;
            String url = AppContext.AppConfig.serverUrl + "sys/sysOffice/findAll";
            this.DoWorkAsync((o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
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
                    cmd.HideOpaqueLayer();
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            });

            this.DoWorkAsync((o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysRole/findAll");
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
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
                    cmd.HideOpaqueLayer();
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }

                if (user != null)
                {
                    this.DoWorkAsync((o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                    {
                        data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysUser/getUser?userId=" + user.id);
                        return data;

                    }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                    {
                        cmd.HideOpaqueLayer();
                        objT = JObject.Parse(data2.ToString());
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            user = objT["result"].ToObject<UserEntity>();
                            dcUser.SetValue(user);
                            oldLoginName = user.loginName;
                            //设置多选框选择
                            String[] strArr = user.roleIds.Split(',');
                            foreach (string str in strArr)
                            {
                                for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
                                {
                                    if (str.Equals(checkedListBoxControl1.Items[i].Value))
                                    {
                                        checkedListBoxControl1.Items[i].CheckState = CheckState.Checked;
                                        break;
                                    }
                                }
                            }
                            richEditor1.LoadText(user.remarks);

                            //显示图片
                            if (user.imgPath != null && user.imgPath.Length > 0)
                            {
                                try
                                {
                                    WebClient web = new WebClient();
                                    var bytes = web.DownloadData(user.imgPath);
                                    this.pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
                                }
                                catch (Exception ex)
                                {
                                    LogClass.WriteLog(ex.Message);
                                }
                            }
                            //WebClient web = new WebClient();
                            //var bytes = web.DownloadData("http://127.0.0.1:8080/dzkb/uploadFileDir/user_1/2018-12-19/asuo_Splash_0.jpg");
                            //this.pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
                        }
                        else
                        {
                            MessageBox.Show(objT["message"].ToString());
                        }
                    });
                }
                else
                {
                    user = new UserEntity();
                    cmd.HideOpaqueLayer();
                }
            });
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
            user.remarks = richEditor1.InnerHtml;
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
            String url = AppContext.AppConfig.serverUrl + "sys/sysUser/save?";
            cmd.ShowOpaqueLayer(255, true);
            this.DoWorkAsync((o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url, param);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                cmd.HideOpaqueLayer();
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            });
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

                cmd.ShowOpaqueLayer(255, true);
                this.DoWorkAsync((o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                {
                    String data = HttpClass.PostForm(url, lstPara);
                    return data;

                }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                {
                    cmd.HideOpaqueLayer();
                    JObject objT = JObject.Parse(data.ToString());
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        WebClient web = new WebClient();
                        serviceFilePath = objT["result"][0].ToString();
                        var bytes = web.DownloadData(serviceFilePath);
                        this.pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
                        MessageBoxUtils.Hint("上传图片成功");
                    }
                    else
                    {
                        MessageBox.Show(objT["message"].ToString());
                        return;
                    }
                });
            }
            else
            {
                MessageBox.Show("请选择要上传的文件");
            }
        }

        /// <summary>
        /// 多线程异步后台处理某些耗时的数据，不会卡死界面
        /// </summary>
        /// <param name="workFunc">Func委托，包装耗时处理（不含UI界面处理），示例：(o)=>{ 具体耗时逻辑; return 处理的结果数据 }</param>
        /// <param name="funcArg">Func委托参数，用于跨线程传递给耗时处理逻辑所需要的对象，示例：String对象、JObject对象或DataTable等任何一个值</param>
        /// <param name="workCompleted">Action委托，包装耗时处理完成后，下步操作（一般是更新界面的数据或UI控件），示列：(r)=>{ datagirdview1.DataSource=r; }</param>
        protected void DoWorkAsync(Func<object, object> workFunc, object funcArg = null, Action<object> workCompleted = null)
        {
            var bgWorkder = new BackgroundWorker();


            //Form loadingForm = null;
            //System.Windows.Forms.Control loadingPan = null;
            bgWorkder.WorkerReportsProgress = true;
            bgWorkder.ProgressChanged += (s, arg) =>
            {
                if (arg.ProgressPercentage > 1) return;

            };

            bgWorkder.RunWorkerCompleted += (s, arg) =>
            {


                bgWorkder.Dispose();

                if (workCompleted != null)
                {
                    workCompleted(arg.Result);
                }
            };

            bgWorkder.DoWork += (s, arg) =>
            {
                bgWorkder.ReportProgress(1);
                var result = workFunc(arg.Argument);
                arg.Result = result;
                bgWorkder.ReportProgress(100);
                Thread.Sleep(500);
            };

            bgWorkder.RunWorkerAsync(funcArg);
        }

    }
}
