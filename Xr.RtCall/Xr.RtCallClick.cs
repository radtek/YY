using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xr.RtCall.pages;
using Xr.Http.RestSharp;
using RestSharp;
using Xr.RtCall.Model;
using Newtonsoft.Json.Linq;
using Xr.Common;
using Xr.Http;

namespace Xr.RtCall
{
    public partial class Form1 : Form
    {
       //HPSocketCS.TcpClient client = new HPSocketCS.TcpClient();
        public static Form1 pCurrentWin = null;//初始化的时候窗体对象赋值
        public SynchronizationContext _context;
        public Form1()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
            pCurrentWin = this;
            IsMax = false;
            GetDoctorAndClinc();
            #region 
            Log4net.LogHelper.Info("程序启动成功");
            if (isStop == "1")
            {
                this.skinbutLook.Text = "继续开诊";
                skinbutLook.BaseColor = Color.Red;
            }
            if (Convert.ToBoolean(AppContext.AppConfig.WhetherToDisplay))
            {
                this.skinButton1.Visible = true;
                this.skinButton1.Enabled = false;
            }
            else
            {

                this.skinButton1.Visible = false;
            }
            #endregion 
            time();
        }
        #region 判断是否启动叫号
        /// <summary>
        /// 判断是否启动叫号
        /// </summary>
        public void IsStop(string code)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + InterfaceAddress.IsStop + "?hospitalId=" + HelperClass.hospitalId + "&deptId=" + HelperClass.deptId + "&clinicId=" + HelperClass.clinicId + "&code=" + code;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    Log4net.LogHelper.Info("程序启动成功");
                    if (isStop == "1")
                    {
                        this.skinbutLook.Text = "继续开诊";
                        skinbutLook.BaseColor = Color.Red;
                    }
                }
                else
                {
                    if (MessageBoxUtils.Show(objT["message"].ToString() + "！是否强制使用？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null) == DialogResult.OK)
                    {
                        Log4net.LogHelper.Info("程序启动成功,强制使用诊室");
                        if (isStop == "1")
                        {
                            this.skinbutLook.Text = "继续开诊";
                            skinbutLook.BaseColor = Color.Red;
                        }
                    }
                    else
                    {
                        SaveConfigSeting("0");
                        Application.ExitThread();
                        Application.Exit();
                        Application.Restart();
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                    //} MessageBoxUtils.Show(objT["message"].ToString() + "是否强制使用", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                   
                    //System.Environment.Exit(0);
                    //Application.Run(new SettingFrm());
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show("程序启动出现错误,请重启", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                System.Environment.Exit(0);
                Log4net.LogHelper.Error("是否启动叫号程序错误信息："+ex.Message);
            }
        }
        #region 保存信息到本地配置文件中
        /// <summary>
        /// 保存信息到本地配置文件中
        /// </summary>
        private void SaveConfigSeting(string Setting)
        {
            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = Environment.CurrentDirectory +
                        @"\Xr.RtCall.exe.config"
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                config.AppSettings.Settings["Setting"].Value = Setting;
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件
            }
            catch (Exception ex)
            {
                Xr.Common.MessageBoxUtils.Show("保存配置时出错" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, null);
                Log4net.LogHelper.Error("保存配置文件错误信息：" + ex.Message);
            }
        }
        #endregion
        #endregion 
        #region 帮助事件
        #region 键盘按Esc关闭窗体
        /// <summary>
        /// 重写按键监视方法，用于操作窗体
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;
            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        if (this.Size.Height == 28)
                        {
                            if (MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null) == DialogResult.OK)
                            {
                                Log4net.LogHelper.Info("退出系统成功");
                                StopDoctor("1");
                                System.Environment.Exit(0);
                            }
                        }
                        else
                        {
                            if (MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, Form1.pCurrentWin) == DialogResult.OK)
                            {
                                Log4net.LogHelper.Info("退出系统成功");
                                StopDoctor("1");
                                System.Environment.Exit(0);
                            }
                        }
                        break;
                }
            }
            return false;
        }
        #endregion
        #region 关闭按钮
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.Size.Height == 28)
            {
                if (MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null) == DialogResult.OK)
                {
                    Log4net.LogHelper.Info("退出系统成功");
                    StopDoctor("1");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                if (MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, Form1.pCurrentWin) == DialogResult.OK)
                {
                    Log4net.LogHelper.Info("退出系统成功");
                    StopDoctor("1");
                    System.Environment.Exit(0);
                }
            }
        }
        #endregion 
        #region 鼠标按下移动
        Point downPoint;
        private void GreenFrmPanel_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void GreenFrmPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion
        #region 最大化
        public static bool IsMax { get; set; }
        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinbutBig_Click(object sender, EventArgs e)
        {
            if (skinbutBig.Text == "展开")
            {
                this.panel_MainFrm.Visible = true;
                IsMax = true;
                panel_MainFrm.Controls.Clear();
                this.Size = new Size(515, 480);
                skinbutBig.Text = "收缩";
                RtCallPeationFrm rtcpf = new RtCallPeationFrm();
                rtcpf.Dock = DockStyle.Fill;
                this.panel_MainFrm.Controls.Add(rtcpf);
            }
            else
            {
                this.Size = new Size(515, 28);
                this.panel_MainFrm.Visible = false;
                IsMax = false;
                skinbutBig.Text = "展开";
            }
        }
        #region 发送windows消息
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
        int hWnd,                  // handle to destination window
        int Msg,                   // message
        int wParam,                // first message parameter
        ref COPYDATASTRUCT lParam  // second message parameter
        );
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        const int WM_COPYDATA = 0x004A;
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        public void SendSess(string id)
        {
            int WINDOW_HANDLER = FindWindow("Tfrmworkbench",null); //根据窗口名称，查找窗口
            if (WINDOW_HANDLER != 0)
            {
                MessageBox.Show("YES 找到Tfrmworkbench!!!窗口句柄为：" + WINDOW_HANDLER);
                byte[] sarr =
                System.Text.Encoding.Default.GetBytes(id);//消息内容
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = id;//消息内容
                cds.cbData = len + 1;
                SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cds);
                MessageBox.Show("发送的消息为："+cds.lpData+"字符长度："+cds.cbData+"句柄："+cds.dwData);
            }
            else
            {
                MessageBox.Show("NO 没找到Tfrmworkbench!!!");
            }
        }
        #endregion
        #region 画边框
        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                         this.panelControl3.ClientRectangle,
                         Color.Transparent,//7f9db9
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Transparent,
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Transparent,
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Transparent,
                         1,
                         ButtonBorderStyle.Solid);
        }
        #endregion
        #endregion
        #endregion
        #region 下一位
        /// <summary>
        /// 下一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButNext_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                label2.Left = 0;
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);
                prament.Add("deptId", HelperClass.deptId);
                prament.Add("clinicId", HelperClass.clinicId);
                prament.Add("triageId", HelperClass.triageId);
                prament.Add("doctorId", HelperClass.doctorId);
                RestSharpHelper.ReturnResult<List<string>>(InterfaceAddress.callNextPerson, prament, Method.POST, result =>
                {
                    if (result.ResponseStatus == ResponseStatus.Completed)
                    {
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                            JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                            {
                                _context.Send((s) => HelperClass.triageId = objT["result"][0]["triageId"].ToString(), null);
                                _context.Send((s) => label2.Text = "[" + objT["result"][0]["smallCellShow"].ToString() + "]", null);
                                if (label2.Text.Length > 12)
                                {
                                    _context.Send((s) => timer2.Enabled = true, null);
                                }
                                if (Convert.ToBoolean(AppContext.AppConfig.WhetherToAssign))
                                {
                                    string patientId = objT["result"][0]["patientId"].ToString();
                                    PatientId = patientId;
                                    _context.Send((s) => Assignment(patientId), null);//把患者ID传给医生工作站并让医生工作站回车
                                }
                                if (Convert.ToBoolean(AppContext.AppConfig.WhetherToDisplay))
                                {
                                      _context.Send((s) =>this.skinButton1.Enabled = true,null);
                                }
                                _context.Send((s) => ShuaXin(), null);
                            }
                            else
                            {
                                if (this.Size.Height == 28)
                                {
                                    _context.Send((s) => MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null), null);
                                }
                                else
                                {
                                    _context.Send((s) => MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, Form1.pCurrentWin), null);
                                }
                                _context.Send((s) => label2.Text = "等待呼叫病人 [请稍候...]", null);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,this);
                Log4net.LogHelper.Error("呼叫下一位错误信息："+ex.Message);
            }
        }
        /// <summary>
        /// 是否刷新患者列表
        /// </summary>
        public void ShuaXin()
        {
            if (IsMax)
            {
                RtCallPeationFrm.RTCallfrm.PatientList();
            }
        }
        /// <summary>
        /// 弹出窗口
        /// </summary>
        public void CallNextPatient(string name)
        {
            RtCallMessageFrm rcf = new RtCallMessageFrm(name);
            HostingForm f = new HostingForm();
            f.Height = rcf.Height;
            f.Width = rcf.Width;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.TopMost = true;
            f.Controls.Add(rcf);
            f.ShowDialog();
        }
        #endregion 
        #region 把ID传给医生工作站
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;//模拟鼠标移动
        private readonly int MOUSEEVENTF_MOVE = 0x0001;//模拟鼠标左键按下
        private readonly int MOUSEEVENTF_LEFTUP = 0x0004;//模拟鼠标左键抬起
        private readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;//鼠标绝对位置
        //private readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        //private readonly int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        //private readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        //private readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;// 模拟鼠标中键抬起 

        [DllImport("user32")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        /// <summary>
        /// 把患者ID传给医生工作站(利用鼠标的复制和粘贴)
        /// </summary>
        /// <param name="id"></param>
        public void Assignment(string id)
        {
            string printid = id;
            int cx = Cursor.Position.X + 1;
            int cy = Cursor.Position.Y + 1;
            if (AppContext.AppConfig.OutPutLocationX != "" || AppContext.AppConfig.OutPutLocationY != "")
            {
                int x = Int32.Parse(AppContext.AppConfig.OutPutLocationX);
                int y = Int32.Parse(AppContext.AppConfig.OutPutLocationY);
                Clipboard.SetDataObject(printid);
                //绝对位置
                mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, x * 65535 / Screen.PrimaryScreen.Bounds.Width, y * 65535 / Screen.PrimaryScreen.Bounds.Height, 0, 0);//移动到需要点击的位置
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, x * 65535 / Screen.PrimaryScreen.Bounds.Width, y * 65535 / Screen.PrimaryScreen.Bounds.Height, 0, 0);//点击
                mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, x * 65535 / Screen.PrimaryScreen.Bounds.Width, y * 65535 / Screen.PrimaryScreen.Bounds.Height, 0, 0);//抬起
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, x * 65535 / Screen.PrimaryScreen.Bounds.Width, y * 65535 / Screen.PrimaryScreen.Bounds.Height, 0, 0);//点击
                mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, x * 65535 / Screen.PrimaryScreen.Bounds.Width, y * 65535 / Screen.PrimaryScreen.Bounds.Height, 0, 0);//抬起
                mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, cx * 65535 / Screen.PrimaryScreen.Bounds.Width, cy * 65535 / Screen.PrimaryScreen.Bounds.Height, 0, 0);//移回到点击前的位置
                System.Threading.Thread.Sleep(Int32.Parse(AppContext.AppConfig.sleepOutPutTime));
                keybd_event((byte)Keys.ControlKey, 0, 0, 0);//按下
                keybd_event((byte)Keys.V, 0, 0, 0);
                keybd_event((byte)Keys.ControlKey, 0, 0x2, 0);//松开
                keybd_event((byte)Keys.V, 0, 0x2, 0);
                keybd_event((byte)Keys.Enter, 0, 0, 0);//按下
                keybd_event((byte)Keys.Enter, 0, 0x2, 0);//松开
            }
        }
        #endregion 
        #region 窗体Load事件
        private void Form1_Load(object sender, EventArgs e)
        {
            int x = SystemInformation.PrimaryMonitorSize.Width - this.Width;
            int y = 0;//要让窗体往上走 只需改变 Y的坐标
            this.Location = new Point(x, y);
            this.Size = new Size(515, 28);
           // this.TopMost = true;
           // bool TcpSocket =Convert.ToBoolean(AppContext.AppConfig.StartUpSocket);
            //if (TcpSocket)
            //{
            //    //绑定事件
            //    //开始连接前触发
            //    client.OnPrepareConnect += new TcpClientEvent.OnPrepareConnectEventHandler(client_OnPrepareConnect);
            //    //连接成功后触发
            //    client.OnConnect += new TcpClientEvent.OnConnectEventHandler(client_OnConnect);
            //    //发送消息后触发
            //    client.OnSend += new TcpClientEvent.OnSendEventHandler(client_OnSend);
            //    //收到消息后触发
            //    client.OnReceive += new TcpClientEvent.OnReceiveEventHandler(client_OnReceive);
            //    //连接关闭后触发
            //    client.OnClose += new TcpClientEvent.OnCloseEventHandler(client_OnClose);
            //}
        }
        #endregion 
        #region 诊按钮  0：开诊，1：停诊
        public void StopDoctor(string isStop)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + InterfaceAddress.openStop + "?hospitalId=" + HelperClass.hospitalId + "&deptId=" + HelperClass.deptId + "&clinicId=" + HelperClass.clinicId + "&doctorId=" + HelperClass.doctorId + "&isStop=" + isStop;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    Log4net.LogHelper.Info("退出程序设置医生临时停诊成功,医生ID为：" + HelperClass.doctorId);
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Info("叫号退出时设置当前医生停诊状态错误信息："+ex.Message);
            }
        }
        /// <summary>
        /// 停诊和开诊按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinbutLook_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);//医院ID
                prament.Add("deptId", HelperClass.deptId);//科室ID
                prament.Add("clinicId", HelperClass.clinicId);//诊室ID
                prament.Add("doctorId", HelperClass.doctorId);//医生ID
                int isStop = 0;
                if (skinbutLook.Text == "临时停诊")
                {
                    prament.Add("isStop", "1");//临时停诊 0：开诊，1：停诊
                    isStop = 1;
                }
                else
                {
                    prament.Add("isStop", "0");//临时停诊 0：开诊，1：停诊
                    isStop = 0;
                }
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>(InterfaceAddress.openStop, prament, Method.POST,
               result =>
               {
                   switch (result.ResponseStatus)
                   {
                       case ResponseStatus.Completed:
                           if (result.StatusCode == System.Net.HttpStatusCode.OK)
                           {
                               Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                               JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                               if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                               {
                                   if (isStop == 1)
                                   {
                                       _context.Send((s) => MessageBoxUtils.Hint("操作成功!",this), null);
                                       _context.Send((s) => this.skinbutLook.Text = "继续开诊", null);
                                      // _context.Send((s) => skinButNext.Enabled=false, null);
                                       _context.Send((s) => skinbutLook.BaseColor = Color.Red, null);
                                   }
                                   else
                                   {
                                       _context.Send((s) => MessageBoxUtils.Hint("操作成功!",this), null);
                                       _context.Send((s) => this.skinbutLook.Text = "临时停诊", null);
                                      // _context.Send((s) => skinButNext.Enabled = true, null);
                                       _context.Send((s) => skinbutLook.BaseColor = Color.FromArgb(59, 175, 218), null);
                                   }
                               }
                               else
                               {
                                   if (this.Height==28)
                                   {
                                       _context.Send((s) => MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null), null);
                                   }
                                   else
                                   {
                                       _context.Send((s) => MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, Form1.pCurrentWin), null);
                                   }
                               }
                           }
                           break;
                   }
               });
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("设置临时停诊和开诊错误信息："+ex.Message);
            }
        }
        #endregion 
        #region 获取主键
        /// <summary>
        /// 获取当前医生是否临时停诊
        /// </summary>
        public String isStop { get; set; }
        /// <summary>
        /// 获取主键
        /// </summary>
        public void GetDoctorAndClinc()
        {
            try
            {
                if (AppContext.AppConfig.hospitalCode == "")
                {
                    Xr.Common.MessageBoxUtils.Show("请检查配置的医院编码是否正确", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, null);
                    System.Environment.Exit(0);
                    return;
                }
                #region 
                //查询医院数据
                //String url = AppContext.AppConfig.serverUrl + InterfaceAddress.hostal + "?code=" + AppContext.AppConfig.hospitalCode;
                //String data = HttpClass.httpPost(url);
                //JObject objT = JObject.Parse(data);
                //if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                //{
                //    List<HelperClassDoctor> list = new List<HelperClassDoctor>();
                //    HelperClassDoctor two = Newtonsoft.Json.JsonConvert.DeserializeObject<HelperClassDoctor>(objT["result"].ToString());
                //    list.Add(two);
                //    HelperClass.list = list;
                //}
                //else
                //{
                //    Xr.Common.MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, null);
                //    System.Environment.Exit(0);
                //    return;
                //}
                //// 查询科室数据
                //String urls = AppContext.AppConfig.serverUrl + InterfaceAddress.dept + "?hospital.code=" + AppContext.AppConfig.hospitalCode;
                //String datas = HttpClass.httpPost(urls);
                //JObject objTs = JObject.Parse(datas);
                //if (string.Compare(objTs["state"].ToString(), "true", true) == 0)
                //{
                //    HelperClass.Departmentlist = objTs["result"].ToObject<List<HelperClassDoctorID>>();
                //}
                #endregion 
                //查询医院ID,科室ID,诊室ID,医生ID,医生停开诊状态
                HelperClass.Code = EncryptionClass.UserOrPassWordInfor(System.Windows.Forms.Application.StartupPath + "\\doctorCode.txt");
                String curls = AppContext.AppConfig.serverUrl + InterfaceAddress.doctor + "?hospitalCode=" + AppContext.AppConfig.hospitalCode + "&deptCode=" + AppContext .AppConfig.deptCode+ "&clinicName=" + AppContext.AppConfig.ClincName + "&doctorCode=" + HelperClass.Code;
                String cdatas = HttpClass.httpPost(curls);
                JObject cobjTs = JObject.Parse(cdatas);
                if (string.Compare(cobjTs["state"].ToString(), "true", true) == 0)
                {
                    List<StardIsFrom> list = cobjTs["result"].ToObject<List<StardIsFrom>>();
                    HelperClass.hospitalId = list[0].hospitalId;
                    HelperClass.deptId = list[0].deptId;
                    HelperClass.clinicId = list[0].clinicId;
                    HelperClass.doctorId = list[0].doctorId;
                    isStop = list[0].isStop;
                }
                else
                {
                    MessageBoxUtils.Show(cobjTs["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("叫号启动获取主键错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 解决绘制控件时的闪烁问题
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);
        }
        #endregion 
        #region 防止以外关闭时用来设置医生停诊状态
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopDoctor("1");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopDoctor("1");
        }
        #endregion 
        #region 循环来查询医生的开诊状态
        public void time()
        {
            if (!timer1.Enabled)
            {
                timer1.Interval = 1*60*1000;//一分钟去查询一次
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetDoctorStart();
        }
        public void GetDoctorStart()
        {
            try
            {
                String curls = AppContext.AppConfig.serverUrl + InterfaceAddress.GetIsStop + "?hospitalId=" + HelperClass.hospitalId + "&deptId=" + HelperClass.deptId + "&doctorId="+HelperClass.doctorId + "&clinicId=" + HelperClass.clinicId;
                String cdatas = HttpClass.httpPost(curls);
                JObject cobjTs = JObject.Parse(cdatas);
                if (string.Compare(cobjTs["state"].ToString(), "true", true) == 0)
                {
                  String DoctorisStop = cobjTs["result"]["isStop"].ToString();
                  if (DoctorisStop=="1")
                  {
                      this.skinbutLook.Text = "继续开诊";
                      skinbutLook.BaseColor = Color.Red;
                  }
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("循环获取医生的开诊状态错误信息："+ex.Message);
            }
        }
        #endregion 
        #region 设置label超出滚动
        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Left -= 4;
            if (label2.Right < 0)
            {  
                label2.Left = this.panel1.Width; 
            }
        }
        #endregion 
        #region 完成就诊
        public String PatientId { get; set; }//患者的ID
        private void skinButton1_Click(object sender, EventArgs e)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + InterfaceAddress.visitWin + "?patientId=" + PatientId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.skinButton1.Enabled = false;
                    label2.Text = "等待呼叫病人[请稍候...]";
                    //MessageBoxUtils.Show("当前患者已完成就诊！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("完成就诊按钮错误信息："+ex.Message);
            }
        }
        #endregion 
    }
}
