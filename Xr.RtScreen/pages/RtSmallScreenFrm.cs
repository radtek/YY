using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Xr.RtScreen.VoiceCall;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;
using Xr.RtScreen.Models;

namespace Xr.RtScreen.pages
{
    public partial class RtSmallScreenFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtSmallScreenFrm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.UpdateStyles();
            _context = new SynchronizationContext();
            Control.CheckForIllegalCrossThreadCalls = false;
            GetSmallScreenInfo();
            DepartmentWaiting();
            SpeakVoicemainFrom speakVoiceform = new SpeakVoicemainFrom();//语音播放窗体
            speakVoiceform.setFormTextValue += new Xr.RtScreen.VoiceCall.SpeakVoicemainFrom.setTextValue(form2_setFormTextValue);
            speakVoiceform.Show(this);
            time();
        }
        #region 消息实时传递
        //第五步：实现事件
        void form2_setFormTextValue(string textValue)
        {
            //具体实现。
            scrollingText1.ScrollText = textValue;
        }
        #endregion 
        #region 画线条
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                         this.tableLayoutPanel2.ClientRectangle,
                         Color.Red,//7f9db9
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Transparent,
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Red,
                         1,
                         ButtonBorderStyle.Solid,
                         Color.Red,
                         1,
                         ButtonBorderStyle.Solid);
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pp = new Pen(Color.Red);
            e.Graphics.DrawRectangle(pp, e.ClipRectangle.X - 1, e.ClipRectangle.Y - 1, e.ClipRectangle.X + e.ClipRectangle.Width - 0, e.ClipRectangle.Y + e.ClipRectangle.Height - 0);
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // 单元格重绘 
            Pen pp = new Pen(Color.Red);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + this.Width - 1, e.CellBounds.Y + this.Height - 1);
        }
        Point downPoint;
        private void scrollingText1_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void scrollingText1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion
        #region 获取信息
        public void GetSmallScreenInfo()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);
                prament.Add("deptId", HelperClass.deptId);
                prament.Add("clinicId", HelperClass.clincId);
                Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>(InterfaceAddress.findRoomScreenDataOne, prament, Method.POST, result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    SmallScreenClass smallscreen = Newtonsoft.Json.JsonConvert.DeserializeObject<SmallScreenClass>(objT["result"].ToString());
                                    _context.Send((s) => label7.Text = smallscreen.clinicName + smallscreen.doctorName, null);
                                    _context.Send((s) => label2.Text = smallscreen.visitPatient, null);
                                    _context.Send((s) => label8.Text = smallscreen.nextPatient, null);
                                    _context.Send((s) => scrollingText2.ScrollText = smallscreen.waitPatient, null);
                                    _context.Send((s) => label5.Text = smallscreen.signInNum, null);
                                }
                                else
                                {
                                    MessageBox.Show(objT["message"].ToString());
                                }

                            }
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("获取诊室小屏错误信息："+ex.Message);
            }
        }
        #endregion
        #region  科室候诊说明
        /// <summary>
        ///  科室候诊说明
        /// </summary>
        public void DepartmentWaiting()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", HelperClass.deptId);
                Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>(InterfaceAddress.findWaitingDesc, prament, Method.POST, result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    _context.Send((s) => scrollingTexts1.ScrollText = objT["result"]["waitingDesc"].ToString(), null);
                                }
                                else
                                {
                                    _context.Send((s) => Xr.Common.MessageBoxUtils.Hint(objT["message"].ToString()), null);
                                }
                            }
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("大屏获取科室候诊说明错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 时间指针
        public void time()
        {
            if (!timer1.Enabled)
            {
                timer1.Interval = Convert.ToInt32(AppContext.AppConfig.RefreshTime);
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetSmallScreenInfo();
        }
        #endregion  
    }
}
