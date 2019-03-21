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
using System.Web;
using Xr.Common.Controls;
using DevExpress.XtraTreeList.Nodes;

namespace Xr.RtManager
{
    public partial class UserEdit : Form
    {
        public UserEdit()
        {
            InitializeComponent();
        }

        private Form MainForm; //主窗体
        Xr.Common.Controls.OpaqueCommand cmd;
        public UserEntity user { get; set; }
        private String oldLoginName;
        String filePath = "";
        String serviceFilePath = "";
        String password;

        private void UserEdit_Load(object sender, EventArgs e)
        {
            labelControl1.Font = new Font("微软雅黑", 18, FontStyle.Regular, GraphicsUnit.Pixel);
            labelControl1.ForeColor = Color.FromArgb(255, 0, 0);
            labelControl2.Font = new Font("微软雅黑", 18, FontStyle.Regular, GraphicsUnit.Pixel);
            labelControl2.ForeColor = Color.FromArgb(255, 0, 0);
            labelControl3.Font = new Font("微软雅黑", 18, FontStyle.Regular, GraphicsUnit.Pixel);
            labelControl3.ForeColor = Color.FromArgb(255, 0, 0);
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            cmd.ShowOpaqueLayer(0f);
            richEditor1.ImagUploadUrl = AppContext.AppConfig.serverUrl;
            dcUser.DataType = typeof(UserEntity);
            richEditor1.ImagUploadUrl = AppContext.AppConfig.serverUrl;

            //获取科室数据
            String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?hospital.code=" + AppContext.AppConfig.hospitalCode;
            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data2.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DeptEntity> deptList = objT["result"].ToObject<List<DeptEntity>>();
                    //DeptEntity dept = new DeptEntity();
                    //dept.code = "`";
                    //dept.name = "全院";
                    //dept.id = "`";
                    //deptList.Insert(0, dept);
                    treeDept.Properties.DataSource = deptList;
                    treeDept.Properties.TreeList.KeyFieldName = "id";
                    treeDept.Properties.TreeList.ParentFieldName = "parentId";
                    treeDept.Properties.DisplayMember = "name";
                    treeDept.Properties.ValueMember = "id";
                    //treeDept.EditValue = "`";
                    //这个应该是个选择多选框触发的事件
                    treeDept.Properties.TreeList.AfterCheckNode += (s, a) =>
                    {
                        a.Node.Selected = true;
                        GetSelectedRoleIDandName();
                    };


                    this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                    {
                        String data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysRole/findAll");
                        return data;

                    }, null, (data3) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                    {
                        objT = JObject.Parse(data3.ToString());
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
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                            return;
                        }

                        if (user != null)
                        {
                            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                            {
                                String data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysUser/getUser?userId=" + user.id);
                                return data;

                            }, null, (data4) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                            {
                                cmd.HideOpaqueLayer();
                                objT = JObject.Parse(data4.ToString());
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    user = objT["result"].ToObject<UserEntity>();
                                    dcUser.SetValue(user);
                                    DefaultChecked(user.deptIds);//默认选择所属科室
                                    GetSelectedRoleIDandName();
                                    treeDept.RefreshEditValue();

                                    //修改的时候密码显示为空
                                    password = user.password;
                                    tePassword.Text = "";
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
                                            Xr.Log4net.LogHelper.Error(ex.Message);
                                        }
                                    }
                                    //WebClient web = new WebClient();
                                    //var bytes = web.DownloadData("http://127.0.0.1:8080/dzkb/uploadFileDir/user_1/2018-12-19/asuo_Splash_0.jpg");
                                    //this.pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
                                }
                                else
                                {
                                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
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
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                    return;
                }
            });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcUser.Validate())
            {
                return;
            }
            if (user.id == null)
            {
                if (tePassword.Text.Length == 0)
                {
                    dcUser.ShowError(tePassword, "密码不能为空");
                    return;
                }
            }
            if (user.id == null || (user.id != null && !user.password.Equals(tePassword.Text) && tePassword.Text.Length != 0))
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
            if (user.id != null && tePassword.Text.Length == 0)
            {
                user.password = password;
            }
            //编辑框的内容要进行转码，不然后台获取的数据会异常缺失数据
            user.remarks = HttpUtility.UrlEncode(richEditor1.InnerHtml, Encoding.UTF8);
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

            if (keyId == null || keyId.Trim().Length == 0)
            {
                dcUser.ShowError(treeDept, "请选择可操作科室");
                return;
            }
            user.deptIds = keyId;

            String param = PackReflectionEntity<UserEntity>.GetEntityToRequestParameters(user, true);
            if (oldLoginName != null)
            {
                param = param + "&&oldLoginName=" + oldLoginName;
            }
            String url = AppContext.AppConfig.serverUrl + "sys/sysUser/save?";
            cmd.ShowOpaqueLayer(255, true);
            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
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
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
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
                Xr.Log4net.LogHelper.Error(ex.Message);
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, this);
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
                int i = filePath.LastIndexOf("\\") + 2;
                model.FileName = filePath.Substring(i, l - i);
                model.FileContent = new FileStream(filePath, FileMode.Open);
                lstPara.Add(model);

                cmd.ShowOpaqueLayer(255, true);
                this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
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
                        MessageBoxUtils.Hint("上传图片成功", this);
                    }
                    else
                    {
                        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                        return;
                    }
                });
            }
            else
            {
                MessageBoxUtils.Show("请选择要上传的文件", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
            }
        }

        /// <summary>
        /// 多线程异步后台处理某些耗时的数据，不会卡死界面
        /// </summary>
        /// <param name="time">线程延迟多少</param>
        /// <param name="workFunc">Func委托，包装耗时处理（不含UI界面处理），示例：(o)=>{ 具体耗时逻辑; return 处理的结果数据 }</param>
        /// <param name="funcArg">Func委托参数，用于跨线程传递给耗时处理逻辑所需要的对象，示例：String对象、JObject对象或DataTable等任何一个值</param>
        /// <param name="workCompleted">Action委托，包装耗时处理完成后，下步操作（一般是更新界面的数据或UI控件），示列：(r)=>{ datagirdview1.DataSource=r; }</param>
        protected void DoWorkAsync(int time, Func<object, object> workFunc, object funcArg = null, Action<object> workCompleted = null)
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

                try
                {
                    bgWorkder.Dispose();

                    if (workCompleted != null)
                    {
                        workCompleted(arg.Result);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException!=null)
                        throw new Exception(ex.InnerException.Message);
                    else
                        throw new Exception(ex.Message);
                }
            };

            bgWorkder.DoWork += (s, arg) =>
            {
                bgWorkder.ReportProgress(1);
                var result = workFunc(arg.Argument);
                arg.Result = result;
                bgWorkder.ReportProgress(100);
                Thread.Sleep(time);
            };

            bgWorkder.RunWorkerAsync(funcArg);
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureViewer pv = new PictureViewer(pictureBox1.Image);
            pv.Show();
        }

        #region 树状多选下拉框相应的事件

        String keyId;//选中的id字符串(1,2,3)
        String keyName;//选中的显示值字符串(科室1,科室2,科室3)

        private List<int> lstCheckedKeyID = new List<int>();//选择局ID集合
        private List<string> lstCheckedKeyName = new List<string>();//选择局Name集合

        private void GetSelectedRoleIDandName()
        {
            this.lstCheckedKeyID.Clear();
            this.lstCheckedKeyName.Clear();

            if (treeDept.Properties.TreeList.Nodes.Count > 0)
            {
                foreach (TreeListNode root in treeDept.Properties.TreeList.Nodes)
                {
                    GetCheckedKeyID(root);
                }
            }
            keyId= "";
            keyName= "";
            foreach (int id in lstCheckedKeyID)
            {
                keyId += id + ",";
            }
            if (keyId.Length > 0)
                keyId = keyId.Substring(0, keyId.Length - 1);

            foreach (string name in lstCheckedKeyName)
            {
                keyName += name + ",";
            }
            if (keyName.Length > 0)
                keyName = keyName.Substring(0, keyName.Length-1);
        }

        /// <summary>
        /// 获取选择状态的数据主键ID集合
        /// </summary>
        /// <param name="parentNode">父级节点</param>
        private void GetCheckedKeyID(TreeListNode parentNode)
        {
            if (parentNode.CheckState != CheckState.Unchecked)
            {
                DeptEntity dept = treeDept.Properties.TreeList.GetDataRecordByNode(parentNode) as DeptEntity;
                if (dept != null)
                {
                    int KeyFieldName = int.Parse(dept.id);
                    string DisplayMember = dept.name;
                    if (!lstCheckedKeyID.Contains(KeyFieldName))
                    {
                        lstCheckedKeyID.Add(KeyFieldName);
                    }
                    if (!lstCheckedKeyName.Contains(DisplayMember))
                    {
                        lstCheckedKeyName.Add(DisplayMember);
                    }
                }
            }
            if (parentNode.Nodes.Count == 0)
            {
                return;//递归终止
            }
            foreach (TreeListNode node in parentNode.Nodes)
            {
                if (node.CheckState != CheckState.Unchecked)
                {
                    DeptEntity dept = treeDept.Properties.TreeList.GetDataRecordByNode(node) as DeptEntity;
                    if (dept != null)
                    {
                        int KeyFieldName = int.Parse(dept.id);
                        string DisplayMember = dept.name;
                        lstCheckedKeyID.Add(KeyFieldName);
                        lstCheckedKeyName.Add(DisplayMember);
                    }
                }
                GetCheckedKeyID(node);
            }

        }

        //下拉框关闭修改文本框的值
        private void treeDept_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            e.DisplayText = keyName;
        }

        /// <summary>
        /// 默认选中事件
        /// </summary>
        /// <param name="rid"></param>
        private void DefaultChecked(string rid)
        {
            if (rid == null) return;
            List<DeptEntity> deptList = treeDept.Properties.DataSource as List<DeptEntity>;
            List<DeptEntity> deptedList = new List<DeptEntity>();
            String[] arr = rid.Split(',');
            foreach (String id in arr)
            {
                foreach (DeptEntity dept in deptList)
                {
                    if (dept.id.Equals(id))
                    {
                        deptedList.Add(dept);
                        continue;
                    }
                }
            }

            if (treeDept.Properties.TreeList.Nodes.Count > 0)
            {
                foreach (TreeListNode nd in treeDept.Properties.TreeList.Nodes)
                {
                    for (int i = 0; i < deptedList.Count; i++)
                    {
                        String checkedkeyid = deptedList[i].id;
                        if (treeDept.Properties.TreeList.FindNodeByKeyID(checkedkeyid) != null)
                        {
                            treeDept.Properties.TreeList.FindNodeByKeyID(checkedkeyid).Checked = true;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
