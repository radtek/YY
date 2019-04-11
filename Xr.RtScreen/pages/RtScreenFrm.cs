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
using Xr.RtScreen.Models;

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
            Control.CheckForIllegalCrossThreadCalls = false;
            this.tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);//开启控件双缓冲以防止绘制时出现闪烁问题
            DoctorSittingConsultations();
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
            scrollingText1.ScrollText = textValue;
        }
        #endregion 
        #region 医生坐诊诊间列表（定时自动查询）
        List<ScreenClass> clinicInfo;
        int a = -1;
        /// <summary>
        /// 医生坐诊诊间列表
        /// </summary>
        public void DoctorSittingConsultations()
        {
            try
            {
                clinicInfo = new List<ScreenClass>();
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospital.id", HelperClass.hospitalId);
                prament.Add("dept.id", HelperClass.deptId);
                Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>(InterfaceAddress.findPublicScreenData, prament, Method.POST, result =>
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
                                    clinicInfo = objT["result"].ToObject<List<ScreenClass>>();
                                    if (clinicInfo.Count != a)
                                    {
                                        _context.Send((s) => DynamicLayout(this.tableLayoutPanel1, clinicInfo.Count + 1, 6), null);
                                        _context.Send((s) => this.tableLayoutPanel1.ColumnStyles[0].Width = 45, null);
                                        _context.Send((s) => this.tableLayoutPanel1.ColumnStyles[1].Width = 45, null);
                                        _context.Send((s) => this.tableLayoutPanel1.ColumnStyles[2].Width = 45, null);
                                        _context.Send((s) => this.tableLayoutPanel1.ColumnStyles[3].Width = 75, null);
                                        _context.Send((s) => this.tableLayoutPanel1.ColumnStyles[4].Width = 35, null);
                                        _context.Send((s) => this.tableLayoutPanel1.ColumnStyles[5].Width = 30, null);
                                    }
                                    a = clinicInfo.Count;
                                    _context.Send((s) => Assignment(), null);
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
                Log4net.LogHelper.Error("科室大屏查询错误信息："+ex.Message);
            }
        }
        #region 给控件赋值
        /// <summary>
        /// 给控件赋值
        /// </summary>
        public void Assignment()
        {
            try
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    foreach (Control c in this.tableLayoutPanel1.Controls)
                    {
                        if (c is Label)
                        {
                            for (int g = 0; g < clinicInfo.Count; g++)
                            {
                                if (c.Name != "label00" && c.Name != "label01" && c.Name != "label02" && c.Name != "label03" && c.Name != "label04" && c.Name != "label05" && c.Name != "label06")
                                {
                                    if (c.Name == "label" + (g + 1) + 0)//诊室
                                    {
                                        if (clinicInfo[g].isStop=="1")
                                        {
                                            c.Text = clinicInfo[g].name + "(停诊)";
                                            c.ForeColor = Color.Red;
                                        }
                                        else
                                        {
                                            c.Text = clinicInfo[g].name + "(正常)";
                                            c.ForeColor = Color.Yellow;
                                        }
                                        g = g + 1;
                                        break;
                                    }
                                    if (c.Name == "label" + (g + 1) + 1)//在诊患者
                                    {
                                        c.Text = clinicInfo[g].visitPatient;
                                        g = g + 1;
                                        break;
                                    }
                                    if (c.Name == "label" + (g + 1) + 2)//下一位患者
                                    {
                                        c.Text = clinicInfo[g].nextPatient;
                                        g = g + 1;
                                        break;
                                    }
                                    if (c.Name == "label" + (g + 1) + 4)//预约
                                    {
                                        c.Text = clinicInfo[g].bespeakNum;
                                        g = g + 1;
                                        break;
                                    }
                                    if (c.Name == "label" + (g + 1) + 5)
                                    {
                                        c.Text = clinicInfo[g].waitNum;
                                        g = g + 1;
                                        break;
                                    }
                                }
                            }
                        }
                        if (c is ScrollingText)
                        {
                            for (int g = 0; g < clinicInfo.Count; g++)
                            {
                                if (c.Name == "st" + (g + 1) + 3)
                                {
                                    SetProperty(c, clinicInfo[g].waitPatient);//通过反射给控件的属性赋值
                                    g = g + 1;
                                    break;
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("给控件赋值时的错误信息：" + ex.Message);
            }
        }
        #region 指定对象指定属性名的属性赋值
        /// <summary>
        /// 指定对象指定属性名的属性赋值
        /// </summary>
        /// <param name="control">所属控件</param>
        /// <param name="Value">设置的值</param>
        public void SetProperty(Control control,object Value)
        {
            try
            {
                Type type = control.GetType();
                System.Reflection.PropertyInfo proinfo = type.GetProperty("ScrollText");
                if (proinfo != null)
                {
                    proinfo.SetValue(control, Value, null);
                }
                else
                {
                    proinfo.SetValue(control, "", null);
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("给滚动控件赋值时错误信息"+ex.Message);
            }
        }
        #endregion 
        #endregion
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
                if (HelperClass.deptId == ""||HelperClass.deptId == null)
                {
                    Xr.Common.MessageBoxUtils.Show("请检查配置的科室是否正确！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                    System.Environment.Exit(0);
                }
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
                                    _context.Send((s) => Xr.Common.MessageBoxUtils.Hint(objT["message"].ToString(), Form1.pCurrentWin), null);
                                    _context.Send((s) => System.Environment.Exit(0), null);
                                }
                            }
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("大屏获取科室候诊说明错误信息："+ex.Message);
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
        #region 科室候诊说明
        public void DepartmentWaitingList()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", HelperClass.deptId);//科室主键
               Xr.RtScreen.Models.RestSharpHelper.ReturnResult<List<string>>("", prament, Method.POST, result =>
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
                                    _context.Send((s) => MessageBox.Show("获取陈宫") , null);
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
                Log4net.LogHelper.Error("获取科室候诊说明错误信息："+ex.Message);
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
                layoutPanel.Controls.Clear();
                layoutPanel.RowStyles.Clear();
                layoutPanel.ColumnStyles.Clear();
                layoutPanel.RowCount = row;    //设置分成几行  
                panelControl2.Height = row * 45;
                this.panelControl2.Dock = DockStyle.Top;
                this.panelControl3.Dock = DockStyle.Fill;
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
                        st.Margin = new Padding(1, 1, 1, 1);
                        st.Font = new Font("微软雅黑", 16, FontStyle.Bold);
                        st.ForeColor = Color.Yellow;
                        st.Name = "st" + i + j;
                        st.TextScrollSpeed = 10;
                        st.TextScrollDistance = 1;
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
                                label.Text = "候诊人数";
                                break;
                        }
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
                        #endregion
                    }
                }
                #region 
                //switch (row)
                //{
                //    case 1:
                //    case 2:
                //    case 3:
                //    case 4:
                //    case 5:
                //case 6:
                //case 7:
                //panelControl2.Height = row * 40;
                //this.panelControl2.Dock = DockStyle.Top;
                //this.panelControl3.Dock = DockStyle.Fill;
                //        break;
                //    default:
                //        this.panelControl2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                //        this.panelControl3.Dock = DockStyle.Bottom;
                //        break;
                //}
                #endregion 
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("绘制控件时出现错误：" + ex.Message);
            }
        }
        #endregion
        #region 画线条
        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
            try
            {

                ControlPaint.DrawBorder(e.Graphics,this.panelControl2.ClientRectangle, Color.White,
                             1,ButtonBorderStyle.Solid,Color.White,
                             1,ButtonBorderStyle.Solid,Color.White,
                             1,ButtonBorderStyle.Solid,Color.White,
                             1,ButtonBorderStyle.Solid);

            }
            catch
            {
            }
        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,this.panelControl1.ClientRectangle,Color.White,
                            1,ButtonBorderStyle.Solid,Color.White,
                            1,ButtonBorderStyle.Solid,Color.White,
                            1,ButtonBorderStyle.Solid,Color.Transparent,
                            1,ButtonBorderStyle.Solid);

            }
            catch 
            {
            }
        }
        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,this.panelControl3.ClientRectangle,Color.White,
                            1,ButtonBorderStyle.Solid,Color.White,
                            1,ButtonBorderStyle.Solid,Color.White,
                            1,ButtonBorderStyle.Solid,Color.Transparent,
                            1,ButtonBorderStyle.Solid);

            }
            catch
            {
            }
        }
        private void scrollingText1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,this.panelControl1.ClientRectangle,Color.White,
                           1,ButtonBorderStyle.Solid,Color.White,
                           1,ButtonBorderStyle.Solid,Color.White,
                           1,ButtonBorderStyle.Solid,Color.Transparent,
                           1,ButtonBorderStyle.Solid);

            }
            catch
            {
            }
        }
        private void scrollingTexts1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,this.panelControl3.ClientRectangle,Color.Transparent,
                          1,ButtonBorderStyle.Solid,Color.Transparent,
                          1,ButtonBorderStyle.Solid,Color.White,
                          1,ButtonBorderStyle.Solid,Color.White,
                          1,ButtonBorderStyle.Solid);

            }
            catch
            {
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,this.panel1.ClientRectangle,Color.White,
                         1,ButtonBorderStyle.Solid,Color.Transparent,
                         1,ButtonBorderStyle.Solid,Color.White,
                         1,ButtonBorderStyle.Solid,Color.White,
                         1,ButtonBorderStyle.Solid);

            }
            catch
            {
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Pen pp = new Pen(Color.White);
                e.Graphics.DrawRectangle(pp, e.ClipRectangle.X - 1, e.ClipRectangle.Y - 1, e.ClipRectangle.X + e.ClipRectangle.Width - 0, e.ClipRectangle.Y + e.ClipRectangle.Height - 0);

            }
            catch
            {
            }
        }
        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            try
            {
                Pen pp = new Pen(Color.White);
                e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + this.Width - 1, e.CellBounds.Y + this.Height - 1);

            }
            catch 
            {
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
        #region 时间指针
        public void time()
        {
            if (!timer1.Enabled)
            {
                timer1.Interval =Convert.ToInt32(AppContext.AppConfig.RefreshTime);
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DoctorSittingConsultations();
        }
        #endregion
        #region 更随移动
        Point downPoint;
        private void scrollingText1_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void scrollingText1_MouseMove(object sender, MouseEventArgs e)
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
