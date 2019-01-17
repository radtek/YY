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

namespace Xr.RtManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginFrom_Load(object sender, EventArgs e)
        {
            //标题居中
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            Double ws = g.MeasureString("*", this.Font).Width;
            String tmp = " ";
            Double tw = 0;
            while ((tw + ws) < startingPoint) { tmp += "*"; tw += ws; }
            tmp += "*";
            this.Text = tmp.Replace("*", "  ") + this.Text.Trim();
        }



        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (tbLoginName.Text.Trim().Length == 0 && tbPassword.Text.Trim().Length == 0)
            {
                return;
            }

            // 弹出加载提示框
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(WaitingForm));
            // 开始异步
            BackgroundWorkerUtil.start_run(bw_DoWork, bw_RunWorkerCompleted, null, false);
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                String param = "username=" + tbLoginName.Text + "&password=" + tbPassword.Text;
                String url = AppContext.AppConfig.serverUrl + "login?" + param;
                e.Result = HttpClass.loginPost(url);
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                String data = e.Result as String;
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    AppContext.Session.userName = tbLoginName.Text;
                    AppContext.Session.password = tbPassword.Text;
                    AppContext.Session.name = objT["result"]["name"].ToString();
                    AppContext.Session.officeId = objT["result"]["officeId"].ToString();
                    AppContext.Session.menuList = objT["result"]["menuList"].ToObject<List<MenuEntity>>();
                    AppContext.Session.UserId = objT["result"]["id"].ToString();
                    AppContext.Session.userType = objT["result"]["userType"].ToString();
                    String param = "hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
                    String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?" + param;
                    data = HttpClass.httpPost(url);
                    objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        AppContext.Session.deptList = objT["result"].ToObject<List<DeptEntity>>();
                        foreach (DeptEntity dept in AppContext.Session.deptList)
                        {
                            if (AppContext.AppConfig.deptCode.Equals(dept.code))
                            {
                                AppContext.Session.hospitalId = dept.hospitalId;
                                AppContext.Session.deptId = dept.id;
                            }
                        }
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(objT["message"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // 关闭加载提示框
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
