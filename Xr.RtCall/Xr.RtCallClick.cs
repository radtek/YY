using HPSocketCS;
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
        HPSocketCS.TcpClient client = new HPSocketCS.TcpClient();
        public static Form1 pCurrentWin = null;//初始化的时候窗体对象赋值
        public SynchronizationContext _context;
        public Form1()
        {
            InitializeComponent();
            #region 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            #endregion 
            this.Size = new Size(727,78);
            pCurrentWin = this;
            _context = SynchronizationContext.Current;
            GetDoctorAndClinc();//获取科室
        }
        #region 帮助事件
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;
            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        if (  MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                        {
                            this.Close();
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
            if (MessageBoxUtils.Show("您确定要退出程序吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                this.Close();
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
                panel_MainFrm.Controls.Clear();
                this.Size = new Size(727, 530);
                skinbutBig.Text = "收缩";
                RtCallPeationFrm rtcpf = new RtCallPeationFrm();
                rtcpf.Dock = DockStyle.Fill;
                this.panel_MainFrm.Controls.Add(rtcpf);
            }
            else
            {
                this.Size = new Size(727, 78);
                skinbutBig.Text = "展开";
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
                prament.Add("clinicId", HelperClass.clinicId);//HelperClass.clinicId
                prament.Add("triageId", HelperClass.triageId);//HelperClass.triageId
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
                                _context.Send((s) => MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1), null);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            f.Height = rcf.Height + 30;
            f.Width = rcf.Width;
            f.panelControl1.Controls.Add(rcf);
            f.StartPosition = FormStartPosition.CenterScreen;
            //int x = SystemInformation.PrimaryMonitorSize.Width - this.Width;
            //f.Location = new Point(x, 0);
            f.Show();
        }
        #endregion 
        #region 窗体Load事件
        private void Form1_Load(object sender, EventArgs e)
        {
            int x = SystemInformation.PrimaryMonitorSize.Width - this.Width;
            int y = 0;//要让窗体往上走 只需改变 Y的坐标
            this.Location = new Point(x, y);
           // this.TopMost = true;
            bool TcpSocket =Convert.ToBoolean(AppContext.AppConfig.StartUpSocket);
            if (TcpSocket)
            {
                //绑定事件
                //开始连接前触发
                client.OnPrepareConnect += new TcpClientEvent.OnPrepareConnectEventHandler(client_OnPrepareConnect);
                //连接成功后触发
                client.OnConnect += new TcpClientEvent.OnConnectEventHandler(client_OnConnect);
                //发送消息后触发
                client.OnSend += new TcpClientEvent.OnSendEventHandler(client_OnSend);
                //收到消息后触发
                client.OnReceive += new TcpClientEvent.OnReceiveEventHandler(client_OnReceive);
                //连接关闭后触发
                client.OnClose += new TcpClientEvent.OnCloseEventHandler(client_OnClose);
            }
        }
        #endregion 
        #region Socket处理
        #region 事件处理方法
        /// <summary>
        /// 开始连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="socket"></param>
        /// <returns></returns>
        private HandleResult client_OnPrepareConnect(TcpClient sender, IntPtr socket)
        {
            return HandleResult.Ok;
        }
        /// <summary>
        /// 异步连接信息
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private HandleResult client_OnConnect(TcpClient sender)
        {
            //异步连接
            return HandleResult.Ok;
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private HandleResult client_OnSend(TcpClient sender, byte[] bytes)
        {
            return HandleResult.Ok;
        }
        /// <summary>
        /// 接受信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private HandleResult client_OnReceive(TcpClient sender, byte[] bytes)
        {
            string recievedStr = Encoding.Default.GetString(bytes);//接收到的数据
            return HandleResult.Ok;
        }

        //当触发了OnClose事件时，表示连接已经被关闭，并且OnClose事件只会被触发一次
        //通过errorCode参数判断是正常关闭还是异常关闭，0表示正常关闭
        //bool close=client.Stop();停止服务
        private HandleResult client_OnClose(TcpClient sender, SocketOperation enOperation, int errorCode)
        {
            if (errorCode == 0)
            {
                LogClass.WriteLog("Socket连接关闭"); 
            }
            else
            {
                LogClass.WriteLog(string.Format("Socket连接异常关闭：{0}，{1}", client.ErrorMessage, client.ErrorCode));
            }
            return HandleResult.Ok;
        }

        #endregion 事件处理方法
        #region Socket请求
        /*
         *   if (client.Connect("192.168.11.47", 5478, false))
            {
                MessageBox.Show("连接成功");
            }
            else
            {
               MessageBox.Show(string.Format("无法建立连接：{0}，{1}", client.ErrorMessage, client.ErrorCode));
            }
         * */
        #endregion
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
                prament.Add("id", HelperClass.doctorId);//医生坐诊记录主键
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
                                       _context.Send((s) => MessageBoxUtils.Hint("操作成功!"), null);
                                       _context.Send((s) => this.skinbutLook.Text = "继续开诊", null);//skinButNext 59, 175, 218
                                       _context.Send((s) => skinButNext.Enabled=false, null);
                                       _context.Send((s) => skinButNext.BaseColor=Color.Gray, null);
                                   }
                                   else
                                   {
                                       _context.Send((s) => MessageBoxUtils.Hint("操作成功!"), null);
                                       _context.Send((s) => this.skinbutLook.Text = "临时停诊", null);
                                       _context.Send((s) => skinButNext.Enabled = true, null);
                                       _context.Send((s) => skinButNext.BaseColor = Color.FromArgb(59, 175, 218), null);
                                   }
                               }
                               else
                               {
                                   _context.Send((s) => MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1), null);
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
                    Xr.Common.MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                    HelperClass.doctorId = cobjTs["result"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("叫号获取科室主键错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 解决绘制控件时的闪烁问题
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }
        #endregion 
    }
}
