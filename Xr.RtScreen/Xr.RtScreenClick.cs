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

namespace Xr.RtScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            GetDoctorAndClinc();
            #region 
            switch (AppContext.AppConfig.StartupScreen)
            {
                case "1":
                    RtScreenFrm rcf = new RtScreenFrm();
                    rcf.Dock = DockStyle.Fill;
                    this.panelControl1.Controls.Add(rcf);
                    break;
                case "2":
                    RtSmallScreenFrm rscf = new RtSmallScreenFrm();
                    rscf.Dock = DockStyle.Fill;
                    this.panelControl1.Controls.Add(rscf);
                    break;
                case "3":
                    RtDoctorSmallScreenFrm rdscf = new RtDoctorSmallScreenFrm();
                    this.panelControl1.Width = rdscf.Width;
                    this.panelControl1.Height = rdscf.Height;
                    //rdscf.Dock = DockStyle.Fill;
                    this.panelControl1.Controls.Add(rdscf);
                    this.Size = new Size(this.panelControl1.Width,this.panelControl1.Height);
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
                    HelperClass.list = list;
                }
                //查询科室数据
                String urls = AppContext.AppConfig.serverUrl + InterfaceAddress.dept + "?hospital.code=" + AppContext.AppConfig.hospitalCode;
                String datas = HttpClass.httpPost(urls);
                JObject objTs = JObject.Parse(datas);
                if (string.Compare(objTs["state"].ToString(), "true", true) == 0)
                {
                    HelperClass.DepartmentList = objTs["result"].ToObject<List<HelperClinc>>();
                }
                //查询诊室数据
                String urlss = AppContext.AppConfig.serverUrl + InterfaceAddress.clin + "?code=" + AppContext.AppConfig.clinicCode;
                String datass = HttpClass.httpPost(urlss);
                JObject objTss = JObject.Parse(datass);
                if (string.Compare(objTss["state"].ToString(), "true", true) == 0)
                {
                    //Clinc clinc = Newtonsoft.Json.JsonConvert.DeserializeObject<Clinc>(objT["result"].ToString());
                    HelperClass.clincId = objTss["result"]["clinicId"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("叫号获取科室和医院主键错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 键盘按Esc关闭窗体
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        //private readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;//模拟鼠标移动
        //private readonly int MOUSEEVENTF_MOVE = 0x0001;//模拟鼠标左键按下
        //private readonly int MOUSEEVENTF_LEFTUP = 0x0004;//模拟鼠标左键抬起
        //private readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;//鼠标绝对位置
        //private readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        //private readonly int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        //private readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        //private readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;// 模拟鼠标中键抬起 

        [System.Runtime.InteropServices.DllImport("user32")]
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
                        if (Xr.Common.MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                        {
                            this.Close();
                        }
                        break;
                }
            }
            return false;
        }
        #endregion
    }
}
