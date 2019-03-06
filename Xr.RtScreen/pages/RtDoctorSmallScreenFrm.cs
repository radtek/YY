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
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.UpdateStyles();
            Control.CheckForIllegalCrossThreadCalls = false;
            _context = new SynchronizationContext();
            pictureBox1.ImageLocation = "man.png";
            GetDoctorSmallScreenInfo();
            time();
        }
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
                                        _context.Send((s) => scrollingTexts1.ScrollText = smallscreen.doctorIntro, null);
                                    }
                                    _context.Send((s) => scrollingText1.ScrollText = smallscreen.waitPatient, null);
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
    }
}
