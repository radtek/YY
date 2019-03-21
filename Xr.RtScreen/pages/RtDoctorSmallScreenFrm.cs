using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;
using Xr.RtScreen.Models;
///Auto:wzw
///Time:2019-01-07
namespace Xr.RtScreen.pages
{
    public partial class RtDoctorSmallScreenFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtDoctorSmallScreenFrm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
        ControlStyles.OptimizedDoubleBuffer |
        ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            Control.CheckForIllegalCrossThreadCalls = false;
            _context = new SynchronizationContext();
            pictureBox1.ImageLocation = "man.png";
            GetDoctorSmallScreenInfo();
            time();
        }
        #region 
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);
        }
        #endregion
        #region 获取信息
        string doctorIntro = "";
        public void GetDoctorSmallScreenInfo()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);
                prament.Add("deptId", HelperClass.deptId);
                prament.Add("clinicId", HelperClass.clincId);
                Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>(InterfaceAddress.findRoomScreenDataTwo, prament, Method.POST, result =>
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
                                    DoctorSmallScreenClass smallscreen = Newtonsoft.Json.JsonConvert.DeserializeObject<DoctorSmallScreenClass>(objT["result"].ToString());
                                    _context.Send((s) => label1.Text = smallscreen.clinicName, null);
                                    _context.Send((s) => label2.Text = smallscreen.doctorName, null);
                                    _context.Send((s) => label3.Text = smallscreen.doctorExcellence + smallscreen.doctorJob, null);
                                    _context.Send((s) => label5.Text = smallscreen.clinicName, null);
                                    _context.Send((s) => label6.Text = smallscreen.visitPatient, null);
                                    if (doctorIntro != smallscreen.doctorIntro)
                                    {
                                        doctorIntro = smallscreen.doctorIntro;
                                        _context.Send((s) => GetDoctorInfo(smallscreen.doctorIntro), null);
                                    }
                                    _context.Send((s) => GetWasitPatrent(smallscreen.waitPatient), null);
                                    _context.Send((s) => label8.Text = smallscreen.nextPatient, null);
                                    _context.Send((s) => GetImage(smallscreen.doctorHeader), null);
                                }
                                else
                                {
                                    _context.Send((s) => Xr.Common.MessageBoxUtils.Hint(objT["message"].ToString(), Form1.pCurrentWin), null);
                                }
                            }
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("获取诊室小屏错误信息：" + ex.Message);
            }
        }
        public void GetImage(dynamic value)
        {
            try
            {
                this.pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(value).GetResponse().GetResponseStream());
            }
            catch
            {
            }
        }
        public void GetWasitPatrent(string wasitPatient)
        {
            try
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
             {
                 scrollingText1.ScrollText = wasitPatient;
             });
            }
            catch
            {

            }
        }
        public void GetDoctorInfo(string docotrinfo)
        {
            try
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    scrollingTexts1.ScrollText =StripHTML(docotrinfo);
                });
            }
            catch
            {
            }
        }
        /// <summary>
        /// 去除HTML标记 
        /// </summary>
        /// <param name="strHtml">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string StripHTML(string strHtml)
        {
            string[] aryReg = { @"<script[^>]*?>.*?</script>", @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", @"([\r\n])[\s]+", @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);", @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);", @"&(copy|#169);", @"&#(\d+);", @"-->", @"<!--.*\n" };
            string[] aryRep = { "", "", "", "\"", "&", "<", ">", " ", "\xa1", "\xa2", "\xa3", "\xa9", "", "\r\n", "" };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", ""); strOutput.Replace(">", "");
            strOutput.Replace("\r\n", ""); return strOutput;
        }
        #endregion
        #region 画线条
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pp = new Pen(Color.White);
            e.Graphics.DrawRectangle(pp, e.ClipRectangle.X - 1, e.ClipRectangle.Y - 1, e.ClipRectangle.X + e.ClipRectangle.Width - 0, e.ClipRectangle.Y + e.ClipRectangle.Height - 0);
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // 单元格重绘 
            Pen pp = new Pen(Color.White);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + this.Width - 1, e.CellBounds.Y + this.Height - 1);
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
            GetDoctorSmallScreenInfo();
        }
        #endregion
        #region 跟随鼠标移动
        Point downPoint;
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Form1.pCurrentWin.Location = new Point(Form1.pCurrentWin.Location.X + e.X - downPoint.X,
                    Form1.pCurrentWin.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion 
    }
}
