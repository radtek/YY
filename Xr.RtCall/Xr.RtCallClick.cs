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

namespace Xr.RtCall
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 初始化Socket的SDK
        /// </summary>
        HPSocketCS.TcpClient client = new HPSocketCS.TcpClient();
        public static Form1 pCurrentWin = null;//初始化的时候窗体对象赋值
        public SynchronizationContext _context;
        public Form1()
        {
            InitializeComponent();
            #region 
            this.SetStyle(ControlStyles.ResizeRedraw |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            #endregion 
            this.Size = new Size(615,78);
            pCurrentWin = this;
            _context = SynchronizationContext.Current;
        }
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
                        //按ESC键后退出对话框
                        if (MessageBox.Show("真的要退出程序吗？", "退出程序", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            this.Close();
                            //Environment.Exit(Environment.ExitCode);
                            // Application.ExitThread();
                        }
                        break;
                }
            }
            return false;
        }
        #endregion
        #region
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("真的要退出程序吗？", "退出程序", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
        #region 下一位
        /// <summary>
        /// 下一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButNext_Click(object sender, EventArgs e)
        {
            RtCallMessageFrm rcf = new RtCallMessageFrm();
            HostingForm f = new HostingForm();
            f.Height = rcf.Height + 30;
            f.Width = rcf.Width;
            f.panelControl1.Controls.Add(rcf);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
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
                this.Size = new Size(615, 500);
                skinbutBig.Text = "收缩";
                //this.skinbutLook.Visible = true;
                RtCallPeationFrm rtcpf = new RtCallPeationFrm();
                rtcpf.Dock = DockStyle.Fill;
                this.panel_MainFrm.Controls.Add(rtcpf);
            }
            else
            {
                this.Size = new Size(615, 78);
                skinbutBig.Text = "展开";
               // this.skinbutLook.Visible = false;
            }
           
        }
        #endregion 
        #region 
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
        private void skinbutLook_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("id", "");//医生坐诊记录主键
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
                string str = "";
                var client = new RestSharpClient("/yyfz/api/sitting/openStop");
                var Params = "";
                if (prament.Count != 0)
                {
                    Params = "?" + string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                }
                client.ExecuteAsync<List<string>>(new RestRequest(Params, Method.POST), result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.None:
                            break;
                        case ResponseStatus.Completed:
                            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var data = result.Data;//返回数据
                                str = string.Join(",", data.ToArray());
                                string a = "";
                                if (isStop == 1)
                                {
                                    a = "继续开诊";
                                }
                                else
                                {
                                    a = "临时停诊";
                                }
                                _context.Send((s) =>
                               this.skinbutLook.Text = a
                                , null);
                            }
                            break;
                        case ResponseStatus.Error:
                            MessageBox.Show("请求错误");
                            break;
                        case ResponseStatus.TimedOut:
                            MessageBox.Show("请求超时");
                            break;
                        case ResponseStatus.Aborted:
                            MessageBox.Show("请求终止");
                            break;
                        default:
                            break;
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 
    }
}
