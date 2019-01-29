﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Common;
using Xr.Http;
using Xr.RtManager.Utils;
using Xr.Common.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraEditors;
using System.Globalization;


namespace Xr.RtManager.Pages.booking
{
    public partial class AppointmentStatisticsForm : UserControl
    {
        Xr.Common.Controls.OpaqueCommand cmd;
        /// <summary>
        /// 查询日期格式类型
        /// </summary>
        String QueryDateType = "day";
        public AppointmentStatisticsForm()
        {
            InitializeComponent();
            
           

        }
        private void UserForm_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.FromArgb(243, 243, 243);
            //Infor();
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            gridBand3.Caption = gridBand15.Caption = gridBand32.Caption = gridBand13.Caption = "开放总额\r\n(预约+现场)";

            //科室下拉框数据
            lueDept.Properties.DataSource = AppContext.Session.deptList;
            lueDept.Properties.DisplayMember = "name";
            lueDept.Properties.ValueMember = "id";
            //默认选中第一个
            lueDept.EditValue = AppContext.Session.deptList[0].id;
            //配置时间格式
            setDateFomartDefult(true);

            TabpageName = xtraTabControl1.SelectedTabPage.Text;

        }
        /// <summary>
        /// 记录Tab页是否已加载数据
        /// </summary>
        Dictionary<Int32, String> PageInfo = new Dictionary<Int32, String>();
        String TabpageName;
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabpageName = xtraTabControl1.SelectedTabPage.Text;
            if (PageInfo.Count != 0)
            {
                if (VerifyInfo())
                {
                    QueryInfo();
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (VerifyInfo())
            {
                PageInfo.Clear();
                QueryInfo();
            }
        }
        QueryParam CurrentParam=new QueryParam();
        private bool VerifyInfo()
        { //deStart.Text,
            //deEnd.Text
            if (deStart.EditValue == null)
            {
                MessageBox.Show("请选择开始日期");
                return false; 
            }
            if (deEnd.EditValue == null)
            {
                MessageBox.Show("请选择结束日期");
                return false; 
            }

               DateTime dtStart = new DateTime();
               DateTime dtEnd = new DateTime();
               if (QueryDateType != "day")
               {
                   dtStart = DateTime.ParseExact(deStart.Text+"-01", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                   dtEnd = DateTime.ParseExact(deEnd.Text+"-01", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
               }
               else
               {
                   dtStart = DateTime.ParseExact(deStart.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                   dtEnd = DateTime.ParseExact(deEnd.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
               }
               if (dtEnd <dtStart )
               {
                   MessageBox.Show("结束日期需大于开始日期");
                   return false; 
               }

               CurrentParam.hospitalId = AppContext.Session.hospitalId;
               CurrentParam.deptId = lueDept.EditValue.ToString();
               CurrentParam.reportType = QueryDateType;
               CurrentParam.startDate = deStart.Text;
               CurrentParam.endDate = deEnd.Text;
            
            return true;
        }
        private void QueryInfo()
        {           
            if (PageInfo.Count < 3)
            {
                // 弹出加载提示框
                //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(WaitingForm));
                cmd.ShowOpaqueLayer(225, true);

                // 开始异步
                BackgroundWorkerUtil.start_run(bw_DoWork, bw_RunWorkerCompleted, null, false);
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<String> Results = new List<String>();//lueDept.EditValue
                //String param = "hospitalId=12&deptId=2&reportType=month&startDate=2019-01&endDate=2019-01";
                String param = "hospitalId={0}&deptId={1}&reportType={2}&startDate={3}&endDate={4}";
                param = String.Format(
                    param, CurrentParam.hospitalId,
                    CurrentParam.deptId,
                    CurrentParam.reportType,
                    CurrentParam.startDate,
                    CurrentParam.endDate);
                String url=String.Empty;
                if (TabpageName == "汇总信息")
                {
                #region 预约途径总数统计[0]
                    if (PageInfo.ContainsKey(0))
                    {
                        e.Result = Results;
                        return;
                    }
                 url = AppContext.AppConfig.serverUrl + "sch/report/bespeakWayReprot?" + param;
                Results.Add( HttpClass.loginPost(url));
                //Results.Add(@"{""code"":200,""message"":""操作成功"",""result"":[{""bespeakCount"":2,""registerWay"":""分诊台""},{""bespeakCount"":1,""registerWay"":""诊间""},{""bespeakCount"":0,""registerWay"":""自助机""},{""bespeakCount"":0,""registerWay"":""公众号""},{""bespeakCount"":0,""registerWay"":""卫计局平台""},{""bespeakCount"":0,""registerWay"":""官网""},{""bespeakCount"":3,""registerWay"":""预约总数""}],""state"":true}");
                    
                #endregion
                #region 日期预约总数统计（折线图）
                //日期预约总数统计（折线图）
                url = AppContext.AppConfig.serverUrl + "sch/report/dateBespeakReprot?" + param;
                Results.Add( HttpClass.loginPost(url));
                // Results.Add(@"{""code"":200,""message"":""操作成功"",""result"":[{""registerTime"": ""2019-01"",""bespeakCount"": 300},{""registerTime"": ""2019-02"",""bespeakCount"": 500},{""registerTime"": ""2019-03"",""bespeakCount"": 600},{""registerTime"": ""2019-04"",""bespeakCount"": 700},{""registerTime"": ""2019-05"",""bespeakCount"": 800}],""state"":true}");
                #endregion
                #region 科室开放数量预约总数概率统计
                //科室开放数量预约总数概率统计
                 url = AppContext.AppConfig.serverUrl + "sch/report/deptOpenBespeakChanceReprot?" + param;
                Results.Add( HttpClass.loginPost(url));
                //Results.Add(@"{""code"":200,""message"":""操作成功"",""result"":[{""deptName"":""急诊科"",""breakNum"":""0"",""breakRate"":""0%"",""openNum"":96,""visitNum"":""0"",""visitRate"":""0%"",""bespeakRate"":""3.13%"",""bespeakNum"":3},{""deptName"":""内科"",""breakNum"":""0"",""breakRate"":""0%"",""openNum"":96,""visitNum"":""0"",""visitRate"":""0%"",""bespeakRate"":""3.13%"",""bespeakNum"":3}],""state"":true}");
                #endregion
                }
                else if (TabpageName == "详细信息")
                {
                    #region 详细信息
                    if (PageInfo.ContainsKey(1))
                    {
                        e.Result = Results;
                        return;
                    }
                    //详细信息
                    url = AppContext.AppConfig.serverUrl + "sch/report/deptOpenBespeakReprot?" + param;
                    Results.Add(HttpClass.loginPost(url));
                    //Results.Add(@"{""code"":200,""message"":""操作成功"",""result"":[{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2}],""state"":true}");
                    #endregion
                }
                else
                {
                    if (PageInfo.ContainsKey(2))
                    {
                        e.Result = Results;
                        return;
                    }
                    #region 慢病统计
                    //慢病统计
                    url = AppContext.AppConfig.serverUrl + "sch/report/deptOpenBespeakReprotForNCD?" + param;
                    Results.Add(HttpClass.loginPost(url));
                    //Results.Add(@"{""code"":200,""message"":""操作成功"",""result"":[{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2},{""deptName"":""急诊科"",""yyNum"":2,""openNum"":96,""yyFzNum"":1,""xcCzNum"":0,""yyCzNum"":1,""xcFzNum"":1,""xcNum"":1,""deptId"":2}],""state"":true}");
                    #endregion
                }

                e.Result = Results;
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                List<String> datas = e.Result as List<String>;
                if (datas.Count == 0)
                { 
                    return;
                }
                JObject objT=new JObject();
                if (TabpageName == "汇总信息")
                {
                    #region 预约途径总数统计[0]
                     objT = JObject.Parse(datas[0]);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        List<RegisterWayEntity> list = objT["result"].ToObject<List<RegisterWayEntity>>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (i < 7)
                            {
                                Label labName = tPanel_registerWay.GetControlFromPosition(i, 0) as Label;
                                labName.Text = list[i].registerWay;
                                Label labNum = tPanel_registerWay.GetControlFromPosition(i, 1) as Label;
                                labNum.Text = list[i].bespeakCount;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(objT["message"].ToString());
                        return;
                    }
                    #endregion
                    #region 日期预约总数统计（折线图）[1]
                    objT = JObject.Parse(datas[1]);
                    if (string.Compare(objT["state"].ToString(), "true", true) != 0)
                    {
                        MessageBox.Show(objT["message"].ToString());
                        return;
                    }
                    else
                    {
                        List<ChartInfoEntity> chartList = objT["result"].ToObject<List<ChartInfoEntity>>();
                        chartControl1.CreateSeries("统计折线图", ViewType.Line, chartList, "registerTime", "bespeakCount");

                    }
                    #endregion
                    #region 科室开放数量预约总数概率统计[2]
                    objT = JObject.Parse(datas[2]);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        List<StatisticsInfoEntity> list = objT["result"].ToObject<List<StatisticsInfoEntity>>();
                        this.gc_statistics.DataSource = list;

                    }
                    else
                    {
                        MessageBox.Show(objT["message"].ToString());
                        return;
                    }
                    #endregion
                    PageInfo.Add(0, TabpageName);
                }
                else if (TabpageName == "详细信息")
                {
                #region 详细信息
                objT = JObject.Parse(datas[0]);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List < DeptInfoEntity > list= objT["result"].ToObject<List<DeptInfoEntity>>();
                    list[0].check = "1";
                    gv_deptInfoCheckRowHandle = 0;
                    this.gc_deptInfo.DataSource = list;
                    // 开始异步
                    BackgroundWorkerUtil.start_run(bw_DoWorkGetDocList, bw_RunWorkerGetDocListCompleted, null, false);
                    PageInfo.Add(1, TabpageName);
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
                #endregion
                }
                else{
                #region 慢病统计
                objT = JObject.Parse(datas[0]);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DeptInfoEntity> list = objT["result"].ToObject<List<DeptInfoEntity>>();
                    list[0].check = "1";
                    gv_deptNCDInfoCheckRowHandle = 0;
                    this.gc_deptInfoNCD.DataSource = list;
                    // 开始异步
                    BackgroundWorkerUtil.start_run(bw_DoWorkGetNCDDocList, bw_RunWorkerGetNCDDocListCompleted, null, false);
                    PageInfo.Add(2, TabpageName);
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
                #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // 关闭加载提示框
                //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                cmd.HideOpaqueLayer();
            }
        }


        int gv_deptInfoCheckRowHandle;
        
        private void gv_deptInfo_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            //gv.SetRowCellValue(0, gv.Columns["check"], "1");
            //当前行 置为 选中状态
            if (gv_deptInfoCheckRowHandle != e.RowHandle)
            {
                //将当前点击的选中
                gv.SetRowCellValue(e.RowHandle, gv.Columns["check"], "1");
                //将前面选中的取消 0表示 未选中
                gv.SetRowCellValue(gv_deptInfoCheckRowHandle, gv.Columns["check"], "0");

                gv_deptInfoCheckRowHandle = e.RowHandle;
                // 弹出加载提示框
                //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(WaitingForm));
                cmd.ShowOpaqueLayer(225, true);
                // 开始异步
                BackgroundWorkerUtil.start_run(bw_DoWorkGetDocList, bw_RunWorkerGetDocListCompleted, null, false);

                
            }
        }
    
        private void bw_DoWorkGetDocList(object sender, DoWorkEventArgs e)
        {
            try
            {
                //String param = "hospitalId=12&deptId=2&reportType=day&startDate=2019-01-01&endDate=2019-01-20";
                String param = "hospitalId={0}&deptId={1}&reportType={2}&startDate={3}&endDate={4}";
                param = String.Format(
                    param, CurrentParam.hospitalId,
                     gv_deptInfo.GetRowCellValue(gv_deptInfoCheckRowHandle, gv_deptInfo.Columns["deptId"]).ToString(),
                    CurrentParam.reportType,
                    CurrentParam.startDate,
                    CurrentParam.endDate);
                String url = AppContext.AppConfig.serverUrl + "sch/report/doctorBespeakReprot?" + param;
                e.Result = HttpClass.loginPost(url);
                //e.Result = @"{""code"":200,""message"":""操作成功"",""result"":[{""czNum"":1,""yyNum"":3,""openNum"":84,""doctorId"":1,""fzNum"":2,""doctorName"":""张医生""},{""czNum"":""0"",""yyNum"":""0"",""openNum"":""0"",""doctorId"":10,""fzNum"":""0"",""doctorName"":""1232""},{""czNum"":""0"",""yyNum"":""0"",""openNum"":12,""doctorId"":15,""fzNum"":""0"",""doctorName"":""杰大哥""},{""czNum"":""0"",""yyNum"":""0"",""openNum"":""0"",""doctorId"":13,""fzNum"":""0"",""doctorName"":""21321""}],""state"":true}";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
        private void bw_RunWorkerGetDocListCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                String data = e.Result as String;
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DocInfoEntity> list = objT["result"].ToObject<List<DocInfoEntity>>();
                    this.gc_docInfo.DataSource = list;

                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                try
                {
                    // 关闭加载提示框
                    //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                    cmd.HideOpaqueLayer();
                }
                catch
                { }
            }
        }
        int gv_deptNCDInfoCheckRowHandle;
        private void gv_deptNCDInfo_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            //gv.SetRowCellValue(0, gv.Columns["check"], "1");
            //当前行 置为 选中状态
            if (gv_deptNCDInfoCheckRowHandle != e.RowHandle)
            {
                //将当前点击的选中
                gv.SetRowCellValue(e.RowHandle, gv.Columns["check"], "1");
                //将前面选中的取消 0表示 未选中
                gv.SetRowCellValue(gv_deptNCDInfoCheckRowHandle, gv.Columns["check"], "0");

                gv_deptNCDInfoCheckRowHandle = e.RowHandle;
                // 弹出加载提示框
                //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(WaitingForm));
                cmd.ShowOpaqueLayer(225, true);
                // 开始异步
                BackgroundWorkerUtil.start_run(bw_DoWorkGetNCDDocList, bw_RunWorkerGetNCDDocListCompleted, null, false);

                
            }
        }
        private void bw_DoWorkGetNCDDocList(object sender, DoWorkEventArgs e)
        {
            try
            {
                //String param = "hospitalId=12&deptId=2&reportType=day&startDate=2019-01-01&endDate=2019-01-20";
                String param = "hospitalId={0}&deptId={1}&reportType={2}&startDate={3}&endDate={4}";
                param = String.Format(
                    param, CurrentParam.hospitalId,
                     gv_deptInfoNCD.GetRowCellValue(gv_deptNCDInfoCheckRowHandle, gv_deptInfoNCD.Columns["deptId"]).ToString(),
                    CurrentParam.reportType,
                    CurrentParam.startDate,
                    CurrentParam.endDate);
                String url = AppContext.AppConfig.serverUrl + "sch/report/doctorBespeakReprotForNCD?" + param;
                e.Result = HttpClass.loginPost(url);
                //e.Result = @"{""code"":200,""message"":""操作成功"",""result"":[{""czNum"":1,""yyNum"":3,""openNum"":84,""doctorId"":1,""fzNum"":2,""doctorName"":""张医生""},{""czNum"":""0"",""yyNum"":""0"",""openNum"":""0"",""doctorId"":10,""fzNum"":""0"",""doctorName"":""1232""},{""czNum"":""0"",""yyNum"":""0"",""openNum"":12,""doctorId"":15,""fzNum"":""0"",""doctorName"":""杰大哥""},{""czNum"":""0"",""yyNum"":""0"",""openNum"":""0"",""doctorId"":13,""fzNum"":""0"",""doctorName"":""21321""}],""state"":true}";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
        private void bw_RunWorkerGetNCDDocListCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                String data = e.Result as String;
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DocInfoEntity> list = objT["result"].ToObject<List<DocInfoEntity>>();
                    this.gc_docInfoNCD.DataSource = list;

                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                try
                {
                    // 关闭加载提示框
                    //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                    cmd.HideOpaqueLayer();
                }
                catch
                { }
            }
        }
        #region 表格样式实现
        /// <summary>
        /// 自定义表头颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandedGridView1_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            //背景颜色没有设置且为空，则默认
            if (e.Band.AppearanceHeader == null || (e.Band.AppearanceHeader.BackColor == Color.Empty && !e.Band.AppearanceHeader.Options.UseBackColor))
                return;
            Rectangle rect = e.Bounds;
            //rect.Inflate(-1, -1);

            // 填充标题颜色.
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, e.Band.AppearanceHeader.BackColor)), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            Pen pen = new Pen(Color.LightGray, 0.1f);
            e.Graphics.DrawRectangle(pen, rect);
            // 绘制过滤和排序按钮.
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }


        private void OnCustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            //背景颜色没有设置且为空，则默认
            if (e.Column == null || (e.Column.AppearanceHeader.BackColor == Color.Empty && !e.Column.AppearanceHeader.Options.UseBackColor))
                return;
            Rectangle rect = e.Bounds;
            //rect.Inflate(-1, -1);

            // 填充标题颜色.
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, e.Column.AppearanceHeader.BackColor)), rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            Pen pen = new Pen(Color.LightGray, 0.1f);
            e.Graphics.DrawRectangle(pen, rect);
            // 绘制过滤和排序按钮.
            foreach (DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Handled = true;
        }


        bool firstfootcell = true;
        /// <summary>
        /// 自定义表格尾部数据统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            int dx = e.Bounds.Height;
            //Brush brush = e.Cache.GetGradientBrush(e.Bounds, Color.Transparent, Color.Transparent, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            Rectangle r = e.Bounds;
            r.Inflate(1, -1);
            Pen p = new Pen(Color.Gray);
            e.Graphics.DrawRectangle(p, r.X - 1, r.Y, r.Width, r.Height);
            //ControlPaint.DrawBorder(e.Graphics, r, Color.Gray, ButtonBorderStyle.Solid);
            //r.Inflate(-1, -1);
            //e.Graphics.FillRectangle(brush, r);
            //r.Inflate(-2, 0);
            e.Appearance.DrawString(e.Cache, e.Info.DisplayText, r);
            if (firstfootcell)
            {
                e.Graphics.DrawRectangle(p, r.X, r.Y, r.Width, r.Height);
                e.Appearance.DrawString(e.Cache, "总计", r);

                firstfootcell = false;
            }
            e.Handled = true;
        }
        private void gc_deptInfo_Paint(object sender, PaintEventArgs e)
        {
            firstfootcell = true;
        }
        #endregion
        
        private void rBtn_Click(object sender, EventArgs e)
        {
            if (rBtn_day.IsCheck)
            {
                QueryDateType = "day"; 
            }
            else
            {
                QueryDateType = "month";
            }
            setDateFomartDefult(rBtn_day.IsCheck);
        }
        private void deStart_EditValueChanged(object sender, EventArgs e)
        {
            deEnd.EditValue = deStart.EditValue;
        }
        /// <summary>
        /// //配置时间格式
        /// </summary>
        /// <param name="flag">true为日期查询 false为月份查询</param>
        void setDateFomartDefult(bool flag)
        {
            if (flag)
            {
                this.deStart.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
                this.deStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.deStart.Properties.EditFormat.FormatString = "yyyy-MM-dd";
                this.deStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.deStart.Properties.Mask.EditMask = "yyyy-MM-dd";
                this.deStart.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;

                this.deEnd.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
                this.deEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.deEnd.Properties.EditFormat.FormatString = "yyyy-MM-dd";
                this.deEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.deEnd.Properties.Mask.EditMask = "yyyy-MM-dd";
                this.deEnd.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
                this.deEnd.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));

            }
            else
            {
                this.deStart.Properties.DisplayFormat.FormatString = "yyyy-MM";
                this.deStart.Properties.EditFormat.FormatString = "yyyy-MM";
                this.deStart.Properties.Mask.EditMask = "yyyy-MM";
                this.deStart.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearView;
                this.deStart.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));

                this.deEnd.Properties.DisplayFormat.FormatString = "yyyy-MM";
                this.deEnd.Properties.EditFormat.FormatString = "yyyy-MM";
                this.deEnd.Properties.Mask.EditMask = "yyyy-MM";
                this.deEnd.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearView;
                this.deEnd.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));
          
            }
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl2_Click(object sender, EventArgs e)
        {
            WebBrowser webBrowser1 = new WebBrowser();
            webBrowser1.Url = new Uri(@"http://192.168.11.42:8080/yyfz/api/sch/report/exportExcelReport");
        }






    }
    #region 实体类

    /// <summary>
    /// 查询参数实体
    /// </summary>
    public class QueryParam
    { 
           /*String param = "hospitalId={0}&deptId={1}&reportType={2}&startDate={3}&endDate={4}";
                param = String.Format(
                    param, AppContext.Session.hospitalId, 
                    lueDept.EditValue.ToString(),
                    QueryDateType,
                    deStart.Text,
                    deEnd.Text);
            */
        /// <summary>
        /// 医院ID
        /// </summary>
        public String hospitalId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public String deptId { get; set; }

        /// <summary>
        /// 日期类型
        /// </summary>
        public String reportType { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public String startDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public String endDate { get; set; }
    }
    /// <summary>
    /// 预约途径实体类
    /// </summary>
    public class RegisterWayEntity
    {
        /// <summary>
        /// 预约途径
        /// </summary>
        public String registerWay { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public String bespeakCount { get; set; }
    }

    /// <summary>
    /// 统计折线图信息实体类
    /// </summary>
    public class ChartInfoEntity
    {
        /// <summary>
        /// 时间X轴
        /// </summary>
        public String registerTime { get; set; }

        /// <summary>
        /// 人数Y轴
        /// </summary>
        public Int32 bespeakCount { get; set; }
    }

    /// <summary>
    /// 统计表格信息实体类
    /// </summary>
    public class StatisticsInfoEntity
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public String deptName { get; set; }
        /// <summary>
        /// 开放名额
        /// </summary>
        public String openNum { get; set; }
        /// <summary>
        /// 预约人数
        /// </summary>
        public String bespeakNum { get; set; }
        /// <summary>
        /// 预约率
        /// </summary>
        public String bespeakRate { get; set; }
        /// <summary>
        /// 就诊人数
        /// </summary>
        public String visitNum { get; set; }
        /// <summary>
        /// 就诊率
        /// </summary>
        public String visitRate { get; set; }
        /// <summary>
        /// 爽约人数
        /// </summary>
        public String breakNum { get; set; }
        /// <summary>
        /// 爽约率
        /// </summary>
        public String breakRate { get; set; }
    }
    /// <summary>
    /// 统计科室信息实体类
    /// </summary>
    public class DeptInfoEntity
    {
        private String _check = "0";

        public String check
        {
            get { return _check; }
            set { _check = value; }
        }
        /// <summary>
        /// 科室名称
        /// </summary>
        public String deptName { get; set; }
        /// <summary>
        /// 开放总额
        /// </summary>
        public Int32 openNum { get; set; }
        /// <summary>
        /// 预约人数
        /// </summary>
        public Int32 yyNum { get; set; }
        /// <summary>
        /// 预约初诊
        /// </summary>
        public Int32 yyCzNum { get; set; }
        /// <summary>
        /// 预约复诊
        /// </summary>
        public Int32 yyFzNum { get; set; }
        /// <summary>
        /// 现场人数
        /// </summary>
        public Int32 xcNum { get; set; }
        /// <summary>
        /// 现场初诊
        /// </summary>
        public Int32 xcCzNum { get; set; }
        /// <summary>
        /// 现场复诊
        /// </summary>
        public Int32 xcFzNum { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public Int32 deptId { get; set; }
    }
    /// <summary>
    /// 统计医生信息实体类
    /// </summary>
    public class DocInfoEntity
    {
        /// <summary>
        /// 医生姓名
        /// </summary>
        public String doctorName { get; set; }
        /// <summary>
        /// 开放总额
        /// </summary>
        public Int32 openNum { get; set; }
        /// <summary>
        /// 预约人数
        /// </summary>
        public String yyNum { get; set; }
        /// <summary>
        /// 初诊人数
        /// </summary>
        public Int32 czNum { get; set; }
        /// <summary>
        ///复诊人数
        /// </summary>
        public String fzNum { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public Int32 doctorId { get; set; }
    }
#endregion
}