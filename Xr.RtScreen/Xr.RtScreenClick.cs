using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xr.RtScreen.pages;
using System.Configuration;
using Xr.RtScreen.Models;
using Newtonsoft.Json.Linq;
using Xr.Http;
using RestSharp;
using System.Net;
using Xr.Common;

namespace Xr.RtScreen
{
    public partial class Form1 : Form
    {
        public static Form1 pCurrentWin = null;//初始化的时候窗体对象赋值
        public static String ScreenType { get; set; }
         LodingFrm loadingfrm;
         SplashScreenManager loading;
        public Form1()
        {
            InitializeComponent();
            #region 
            this.SetStyle(ControlStyles.ResizeRedraw |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            pCurrentWin = this;
            loadingfrm = new LodingFrm(this);
            //将Loaing窗口，注入到 SplashScreenManager 来管理
            loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            #endregion
            Log4net.LogHelper.Info("程序启动");
            GetDoctorAndClinc();
            //this.TopMost = true;
            #region 
            switch (AppContext.AppConfig.StartupScreen)
            {
                case "1":
                    ScreenType = "1";
                    RtScreenFrm rcf = new RtScreenFrm();
                    rcf.Dock = DockStyle.Fill;
                    this.panelControl1.Controls.Add(rcf);
                    break;
                case "2":
                    ScreenType = "2";
                    RtSmallScreenFrm rscf = new RtSmallScreenFrm();
                    rscf.Dock = DockStyle.Fill;
                    this.panelControl1.Controls.Add(rscf);
                    break;
                case "3":
                    ScreenType = "3";
                    RtDoctorSmallScreenFrm rdscf = new RtDoctorSmallScreenFrm();
                    //this.panelControl1.Width = rdscf.Width;
                    //this.panelControl1.Height = rdscf.Height;
                    rdscf.Dock = DockStyle.Fill;
                    this.panelControl1.Controls.Add(rdscf);
                    break;
                default:
                    MessageBoxUtils.Show("配置的启动屏不正确，请检查后重启", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                    System.Environment.Exit(0);
                    break;
            }
            #endregion 
        }
        #region 获取医院和科室主键
        public void GetDoctorAndClinc()
        {
            try
            {
                //查询医院数据
                String url = AppContext.AppConfig.serverUrl + InterfaceAddress.hostal + "?code=" + AppContext.AppConfig.hospitalCode;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<HelperClassDoctor> list = new List<HelperClassDoctor>();
                    HelperClassDoctor two = Newtonsoft.Json.JsonConvert.DeserializeObject<HelperClassDoctor>(objT["result"].ToString());
                    list.Add(two);
                    if (list[0] == null)
                    {
                        loading.CloseWaitForm();
                        MessageBoxUtils.Show("未查询到医院信息，请检查后重启", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                        System.Environment.Exit(0);
                    }
                    HelperClass.list = list;
                }
                else
                {
                    loading.CloseWaitForm();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                    System.Environment.Exit(0);
                }
                //查询科室数据
                String urls = AppContext.AppConfig.serverUrl + InterfaceAddress.dept + "?hospital.code=" + AppContext.AppConfig.hospitalCode;
                String datas = HttpClass.httpPost(urls);
                JObject objTs = JObject.Parse(datas);
                if (string.Compare(objTs["state"].ToString(), "true", true) == 0)
                {
                    HelperClass.DepartmentList = objTs["result"].ToObject<List<HelperClinc>>();
                }
                else
                {
                    loading.CloseWaitForm();
                    MessageBoxUtils.Show(objTs["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                    System.Environment.Exit(0);
                }
                HelperClass.clincId = AppContext.AppConfig.clinicCode;
                //查询诊室数据
                //String urlss = AppContext.AppConfig.serverUrl + InterfaceAddress.clin + "?code=" + AppContext.AppConfig.clinicCode;
                //String datass = HttpClass.httpPost(urlss);
                //JObject objTss = JObject.Parse(datass);
                //if (string.Compare(objTss["state"].ToString(), "true", true) == 0)
                //{
                //    //Clinc clinc = Newtonsoft.Json.JsonConvert.DeserializeObject<Clinc>(objT["result"].ToString());
                //    HelperClass.clincId = objTss["result"]["clinicId"].ToString();
                //}
                //else
                //{
                //    loading.CloseWaitForm();
                //    MessageBoxUtils.Show(objTss["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                //    System.Environment.Exit(0);
                //}
            }
            catch (Exception ex)
            {
                loading.CloseWaitForm();
                MessageBoxUtils.Show("程序启动出现错误,请检查后重启", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                System.Environment.Exit(0);
                Log4net.LogHelper.Error("叫号获取科室和医院主键错误信息：" + ex.Message);
            }
            finally {
                loading.CloseWaitForm();
            }
        }
        #endregion 
        #region 键盘按Esc关闭窗体
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        /// <summary>
        /// 重写按键监视方法，用于操作窗体
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;
            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        if (Xr.Common.MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1,this) == DialogResult.OK)
                        {
                            Log4net.LogHelper.Info("退出系统成功");
                            System.Environment.Exit(0);
                        }
                        break;
                }
            }
            return false;
        }
        #endregion
        #region 窗口最大化和最小化
        private void 最大化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Maximized;
        }

        private void 最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            //this.WindowState = FormWindowState.Minimized;
            this.Size = new Size(1000,600);
        }
        #endregion
    }
}
