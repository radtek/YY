using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json.Linq;
//using BedsideSettle.Pages;
using Xr.RtManager.Utils;
using System.Drawing.Drawing2D;
using Xr.Http;
using System.Threading;
using Xr.Common;

namespace Xr.RtManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
        }

        Xr.Common.Controls.OpaqueCommand cmd;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbLoginName.Text.Trim().Length == 0 && tbPassword.Text.Trim().Length == 0)
            {
                return;
            }
            cmd.ShowOpaqueLayer(1f);
            String param = "username=" + tbLoginName.Text + "&password=" + tbPassword.Text;
            String url = AppContext.AppConfig.serverUrl + "login?" + param;
            this.DoWorkAsync(500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.loginPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    //将数据存到session
                    AppContext.Session.userName = tbLoginName.Text; //登录名
                    AppContext.Session.password = tbPassword.Text;  //密码
                    AppContext.Session.name = objT["result"]["name"].ToString(); //用户姓名
                    AppContext.Session.officeId = objT["result"]["officeId"].ToString(); //机构id
                    AppContext.Session.menuList = objT["result"]["menuList"].ToObject<List<MenuEntity>>(); //菜单列表
                    AppContext.Session.UserId = objT["result"]["id"].ToString(); //用户id
                    AppContext.Session.userType = objT["result"]["userType"].ToString(); 
                    AppContext.Session.loginDate = objT["result"]["loginDate"].ToString(); //登录时间，目前没作用
                    //获取所有科室
                    param = "hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
                    url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?" + param;
                    this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                    {
                        data = HttpClass.httpPost(url);
                        return data;

                    }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                    {
                        objT = JObject.Parse(data2.ToString());
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            AppContext.Session.deptList = objT["result"].ToObject<List<DeptEntity>>();
                            //配置文件中的科室编码为""，默认为全院
                            if (AppContext.AppConfig.deptCode == null || AppContext.AppConfig.deptCode.Trim().Length == 0)
                            {
                                if (AppContext.Session.deptList.Count > 0)
                                {
                                    AppContext.Session.hospitalId = AppContext.Session.deptList[0].hospitalId;
                                    AppContext.Session.deptId = "";
                                    AppContext.Session.deptName = "全院";
                                }
                            }
                            else
                            {
                                foreach (DeptEntity dept in AppContext.Session.deptList)
                                {
                                    if (AppContext.AppConfig.deptCode.Equals(dept.code))
                                    {
                                        AppContext.Session.hospitalId = dept.hospitalId;
                                        AppContext.Session.deptId = dept.id;
                                        AppContext.Session.deptName = dept.name;
                                        break;
                                    }
                                }
                            }
                            cmd.HideOpaqueLayer();
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            cmd.HideOpaqueLayer();
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    });
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            });
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
            //这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。 
            System.Environment.Exit(0); 
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            return;
            //if (tbLoginName.Text.Trim().Length == 0)
            //{
            //    return;
            //}
            //String serverUrl = ConfigurationManager.AppSettings["serverUrl"].ToString();
            //String jsonStr = HttpClass.HRequest(serverUrl + "sys/bedLogin/login?userName=" + tbLoginName.Text + "&&password=" + tbPassword.Text);
            //JObject objT = JObject.Parse(jsonStr);
            //if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            //{
            //    //ModifyPasswordForm form = new ModifyPasswordForm(tbLoginName.Text, tbPassword.Text);
            //    //form.ShowDialog();
            //    //if(form.DialogResult == DialogResult.OK)
            //    //    MessageBoxUtils.Hint("修改密码成功");
            //    //MainForm mian = new MainForm();
            //    //mian.ModifyPassword(tbLoginName.Text, tbPassword.Text);
            //    //mian.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show(objT["message"].ToString());
            //}

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
                    cmd.HideOpaqueLayer();
                    throw new Exception(ex.InnerException.Message);
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

        
    }
}
