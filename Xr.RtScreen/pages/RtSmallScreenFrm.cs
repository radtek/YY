﻿using System;
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

namespace Xr.RtScreen.pages
{
    public partial class RtSmallScreenFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtSmallScreenFrm()
        {
            InitializeComponent();
            _context = new SynchronizationContext();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.UpdateStyles();
            SpeakVoicemainFrom speakVoiceform = new SpeakVoicemainFrom();//语音播放窗体
            speakVoiceform.Show(this);
        }
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
                prament.Add("","");
                Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>("api/sch/screen/findRoomScreenDataOne", prament, Method.POST, result =>
                {
                    LogClass.WriteLog("请求结果：" + string.Join(",", result.Data.ToArray()));
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    _context.Send((s) => MessageBox.Show("获取陈宫"), null);
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
    }
}
