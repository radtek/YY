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
            #region 双缓冲
            //this.SetStyle(ControlStyles.ResizeRedraw |
            //      ControlStyles.OptimizedDoubleBuffer |
            //      ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            #endregion 
            _context = SynchronizationContext.Current;
            //panel_MainFrm.Controls.Clear();
            //this.panel_MainFrm.Visible = false;
            //panelControl3.Height = 28;
            pCurrentWin = this;
            IsMax = false;
            GetDoctorAndClinc();
            IsStop(EncryptionClass.UserOrPassWordInfor(System.Windows.Forms.Application.StartupPath + "\\doctorCode.txt"));
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
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show("程序启动出现错误,请重启", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                System.Environment.Exit(0);
                Log4net.LogHelper.Error("是否启动叫号程序错误信息："+ex.Message);
            }
        }
        #endregion 
        #region 帮助事件
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
                                System.Environment.Exit(0);
                            }
                        }
                        else
                        {
                            if (MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, Form1.pCurrentWin) == DialogResult.OK)
                            {
                                Log4net.LogHelper.Info("退出系统成功");
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
                    System.Environment.Exit(0);
                }
            }
            else
            {
                if (MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, Form1.pCurrentWin) == DialogResult.OK)
                {
                    Log4net.LogHelper.Info("退出系统成功");
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
                this.Size = new Size(727, 480);
                skinbutBig.Text = "收缩";
                RtCallPeationFrm rtcpf = new RtCallPeationFrm();
                rtcpf.Dock = DockStyle.Fill;
                this.panel_MainFrm.Controls.Add(rtcpf);
            }
            else
            {
                this.Size = new Size(727, 28);
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
                         Color.Black,
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
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);
                prament.Add("deptId", HelperClass.deptId);
                prament.Add("clinicId", HelperClass.clinicId);
                prament.Add("triageId", HelperClass.triageId);
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
                                _context.Send((s) => CallNextPatient(objT["result"][0]["smallCellShow"].ToString()), null);
                            }
                            else
                            {
                                if ( this.Size.Height==28)
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
        /// 弹出窗口
        /// </summary>
        public void CallNextPatient(string name)
        {
            RtCallMessageFrm rcf = new RtCallMessageFrm(name);
            HostingForm f = new HostingForm();
            f.Height = rcf.Height;
            f.Width = rcf.Width;
            f.Controls.Add(rcf);
            f.StartPosition = FormStartPosition.CenterScreen;
            if (this.Size.Height != 28)
            {
                f.Location = new Point((this.Width - this.Width) / 2 + this.Location.X,
                   (this.Height - this.Height) / 2 + this.Location.Y);//相对程序居中
            }
            f.ShowDialog();
        }
        #endregion 
        #region 窗体Load事件
        private void Form1_Load(object sender, EventArgs e)
        {
            int x = SystemInformation.PrimaryMonitorSize.Width - this.Width;
            int y = 0;//要让窗体往上走 只需改变 Y的坐标
            this.Location = new Point(x, y);
            this.Size = new Size(727, 28);
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
                                       _context.Send((s) => skinButNext.Enabled=false, null);
                                       _context.Send((s) => skinButNext.BaseColor=Color.Gray, null);
                                   }
                                   else
                                   {
                                       _context.Send((s) => MessageBoxUtils.Hint("操作成功!",this), null);
                                       _context.Send((s) => this.skinbutLook.Text = "临时停诊", null);
                                       _context.Send((s) => skinButNext.Enabled = true, null);
                                       _context.Send((s) => skinButNext.BaseColor = Color.FromArgb(59, 175, 218), null);
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
        /// 获取主键
        /// </summary>
        public void GetDoctorAndClinc()
        {
            try
            {
                if (AppContext.AppConfig.hospitalCode=="")
                {
                       Xr.Common.MessageBoxUtils.Show("请检查配置的医院编码是否正确", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,null);
                    System.Environment.Exit(0);
                    return;
                }
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
                else
                {
                    Xr.Common.MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,null);
                    System.Environment.Exit(0);
                    return;
                }
                // 查询科室数据
                String urls = AppContext.AppConfig.serverUrl + InterfaceAddress.dept + "?hospital.code=" + AppContext.AppConfig.hospitalCode;
                String datas = HttpClass.httpPost(urls);
                JObject objTs = JObject.Parse(datas);
                if (string.Compare(objTs["state"].ToString(), "true", true) == 0)
                {
                    HelperClass.Departmentlist = objTs["result"].ToObject<List<HelperClassDoctorID>>();
                }
                //查询诊室数据
                String urlss = AppContext.AppConfig.serverUrl + InterfaceAddress.clinc + "?code=" + AppContext.AppConfig.ClincCode;
                String datass = HttpClass.httpPost(urlss);
                JObject objTss = JObject.Parse(datass);
                if (string.Compare(objTss["state"].ToString(), "true", true) == 0)
                {
                    HelperClass.clinicId = objTss["result"]["clinicId"].ToString();
                }
                //查询医生ID
                String curls = AppContext.AppConfig.serverUrl + InterfaceAddress.doctor + "?hospitalId=" + HelperClass.hospitalId + "&deptId=" + HelperClass.deptId + "&clinicId=" + HelperClass.clinicId;
                String cdatas = HttpClass.httpPost(curls);
                JObject cobjTs = JObject.Parse(cdatas);
                if (string.Compare(cobjTs["state"].ToString(), "true", true) == 0)
                {
                    HelperClass.doctorId = cobjTs["result"]["doctorId"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("叫号获取科室主键错误信息：" + ex.Message);
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
    }
}
