using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.RtScreen.RtUserContronl;
using System.Threading;
using Xr.Http.RestSharp;
using RestSharp;
using System.Net;
using Xr.RtScreen.VoiceCall;
using Newtonsoft.Json.Linq;

namespace Xr.RtScreen.pages
{
    public partial class RtScreenFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtScreenFrm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            _context = SynchronizationContext.Current;
            DynamicLayout(this.tableLayoutPanel1, 7, 6);
            #region
            //try
            //{
            this.tableLayoutPanel1.ColumnStyles[0].Width = 30;
            this.tableLayoutPanel1.ColumnStyles[1].Width = 40;
            this.tableLayoutPanel1.ColumnStyles[2].Width = 40;
            this.tableLayoutPanel1.ColumnStyles[3].Width = 80;
            this.tableLayoutPanel1.ColumnStyles[4].Width = 40;
            this.tableLayoutPanel1.ColumnStyles[5].Width = 40;

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            #endregion
            SpeakVoicemainFrom speakVoiceform = new SpeakVoicemainFrom();//语音播放窗体
            speakVoiceform.Show(this);
        }
        #region 医生坐诊诊间列表（定时自动查询）
        public void DoctorSittingConsultations()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", "");//科室主键
                Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>("api/sch/screen/findPublicScreenData", prament, Method.POST, result =>
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
            }
        }
        #endregion
        #region 科室候诊说明
        public void DepartmentWaitingList()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", "");//科室主键
               Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>("", prament, Method.POST, result =>
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
                                    _context.Send((s) => MessageBox.Show("获取陈宫") , null);
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
            }
        }
        #endregion
        #region 呼号信息（定时自动查询）
        public void CallSignInformation()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", "");//科室主键
                string str = "";
                var client = new RestSharpClient("/yyfz/api/call/findCalls");
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
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                var data = result.Data;//返回数据
                                str = string.Join(",", data.ToArray());
                                _context.Send((s) =>
                                    MessageBox.Show("获取陈宫")
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
        #region 动态布局
        /// <summary>
        /// 动态布局
        /// </summary>
        /// <param name="layoutPanel"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void DynamicLayout(TableLayoutPanel layoutPanel, int row, int col)
        {
            try
            {
                layoutPanel.RowCount = row;    //设置分成几行  
                for (int i = 0; i < row; i++)
                {
                    layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }
                layoutPanel.ColumnCount = col;    //设置分成几列  
                for (int i = 0; i < col; i++)
                {
                    layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
                }
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        Label label = new Label();
                        label.Dock = DockStyle.Fill;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.Margin = new Padding(1, 1, 1, 1);
                        label.Font = new Font("微软雅黑", 16, FontStyle.Bold);
                        label.BackColor = Color.Transparent;
                        label.ForeColor = Color.Yellow;
                        label.Name = "label" + i + j;

                        ScrollingText st = new ScrollingText();
                        st.Dock = DockStyle.Fill;
                        st.ScrollText = "";
                        //st.TextAlign = ContentAlignment.MiddleCenter;
                        st.Margin = new Padding(1, 1, 1, 1);
                        st.Font = new Font("微软雅黑", 16, FontStyle.Bold);
                        // st.BackColor = Color.Black;
                        st.ForeColor = Color.Yellow;
                        st.Name = "st" + i + j;
                        #region
                        switch (label.Name)
                        {
                            case "label00":
                                label.Text = "诊室";
                                break;
                            case "label01":
                                label.Text = "在诊患者";
                                break;
                            case "label02":
                                label.Text = "下一位";
                                break;
                            case "label03":
                                label.Text = "候诊患者";
                                break;
                            case "label04":
                                label.Text = "已预约总数";
                                break;
                            case "label05":
                                label.Text = "已签到总数";
                                break;
                        }
                        #endregion
                        if (j == 3 && label.Name != "label03")
                        {
                            layoutPanel.Controls.Add(st);
                            layoutPanel.SetRow(st, i);
                            layoutPanel.SetColumn(st, j);
                        }
                        else
                        {
                            layoutPanel.Controls.Add(label);
                            layoutPanel.SetRow(label, i);
                            layoutPanel.SetColumn(label, j);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 画线条
        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
            try
            {

                ControlPaint.DrawBorder(e.Graphics,
                             this.panelControl2.ClientRectangle,
                             Color.Red,//7f9db9
                             1,
                             ButtonBorderStyle.Solid,
                             Color.Red,
                             1,
                             ButtonBorderStyle.Solid,
                             Color.Red,
                             1,
                             ButtonBorderStyle.Solid,
                             Color.Red,
                             1,
                             ButtonBorderStyle.Solid);

            }
            catch (Exception)
            {

                throw;
            }
        }


        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,
                            this.panelControl1.ClientRectangle,
                            Color.Red,//7f9db9
                            1,
                            ButtonBorderStyle.Solid,
                            Color.Red,
                            1,
                            ButtonBorderStyle.Solid,
                            Color.Red,
                            1,
                            ButtonBorderStyle.Solid,
                            Color.Transparent,
                            1,
                            ButtonBorderStyle.Solid);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,
                            this.panelControl3.ClientRectangle,
                            Color.Red,//7f9db9
                            1,
                            ButtonBorderStyle.Solid,
                            Color.Red,
                            1,
                            ButtonBorderStyle.Solid,
                            Color.Red,
                            1,
                            ButtonBorderStyle.Solid,
                            Color.Transparent,
                            1,
                            ButtonBorderStyle.Solid);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void scrollingText1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,
                           this.panelControl1.ClientRectangle,
                           Color.Red,//7f9db9
                           1,
                           ButtonBorderStyle.Solid,
                           Color.Red,
                           1,
                           ButtonBorderStyle.Solid,
                           Color.Red,
                           1,
                           ButtonBorderStyle.Solid,
                           Color.Transparent,
                           1,
                           ButtonBorderStyle.Solid);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void scrollingTexts1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,
                          this.panelControl3.ClientRectangle,
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
            catch (Exception)
            {

                throw;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,
                         this.panel1.ClientRectangle,
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
            catch (Exception)
            {

                throw;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Pen pp = new Pen(Color.Red);
                e.Graphics.DrawRectangle(pp, e.ClipRectangle.X - 1, e.ClipRectangle.Y - 1, e.ClipRectangle.X + e.ClipRectangle.Width - 0, e.ClipRectangle.Y + e.ClipRectangle.Height - 0);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            try
            {
                Pen pp = new Pen(Color.Red);
                e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + this.Width - 1, e.CellBounds.Y + this.Height - 1);

            }
            catch (Exception)
            {

                throw;
            }
        }

        //private void gridControl1_Paint(object sender, PaintEventArgs e)
        //{
        //      ControlPaint.DrawBorder(e.Graphics,
        //             this.gridControl1.ClientRectangle,
        //             Color.Red,//7f9db9
        //             1,
        //             ButtonBorderStyle.Solid,
        //             Color.Red,
        //             1,
        //             ButtonBorderStyle.Solid,
        //             Color.Red,
        //             1,
        //             ButtonBorderStyle.Solid,
        //             Color.Red,
        //             1,
        //             ButtonBorderStyle.Solid);
        //}
        #endregion
    }
}
