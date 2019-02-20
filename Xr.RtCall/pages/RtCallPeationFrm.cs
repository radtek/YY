﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using Xr.Http.RestSharp;
using System.Net;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.RtCall.Model;
using Xr.Common;

namespace Xr.RtCall.pages
{
   // public delegate void CalculatorDelegate(string num1, Dictionary<string, string> prament); // 委托,声明在类之外
    public partial class RtCallPeationFrm : UserControl
    {
        public SynchronizationContext _context;
        public static RtCallPeationFrm RTCallfrm = null;//初始化的时候窗体对象赋值
        public RtCallPeationFrm()
        {
            InitializeComponent();
            #region 
            this.SetStyle(ControlStyles.ResizeRedraw |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            _context = SynchronizationContext.Current;
            RTCallfrm = this;
            #endregion 
            PatientList();
        }
        #region 患者列表
        /// <summary>
        /// 患者列表
        /// </summary>
        public void PatientList()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);//医院主键
                prament.Add("deptId", HelperClass.deptId);//科室主键
                prament.Add("doctorId", HelperClass.doctorId);//医生主键
                prament.Add("workDate", "2019-01-10");//坐诊日期 DateTime.Now.ToString("yyyy-MM-dd")当前日期
                prament.Add("period", "2");//坐诊时段
                if (checkEdit1.Checked)
                {
                    prament.Add("status", "0");//候诊中
                }
                else
                {
                    prament.Add("status", "1");//完成
                }
               RestSharpHelper.ReturnResult<List<string>>("api/sch/registerTriage/findPatientListByDoctor", prament, Method.POST,
                 result =>
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
                                    List<Patient> a = objT["result"].ToObject<List<Patient>>();
                                    a.Add(new Patient { cradNo = "02102337", cradType = "0", patientName = "1", queueNum = "1", visitTime = "1", triageTime = "1", registerWay = "1", regTime = "1", regVisitTime = "1", status = "1" });
                                    _context.Send((s) => this.gc_Pateion.DataSource = a,null);
                                    _context.Send((s) => label1.Text=a.Count+"人", null);
                                }
                                else
                                {
                                    _context.Send((s) => MessageBoxUtils.Hint(objT["message"].ToString()), null);
                                }
                            }
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
               Log4net.LogHelper.Error("获取患者列表错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 右键菜单
        /// <summary>
        /// 复诊预约
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 复诊预约ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = this.gv_Pateion.GetFocusedRow() as Patient;
                  if (selectedRow == null)
                    return;
                Form1.pCurrentWin.panel_MainFrm.Controls.Clear();
                RtIntersectionAppointmentFrm rtcpf = new RtIntersectionAppointmentFrm(selectedRow);
                rtcpf.Dock = DockStyle.Fill;
                Form1.pCurrentWin.panel_MainFrm.Controls.Add(rtcpf);
            }
            catch (Exception ex)
            {
               Log4net.LogHelper.Error("复诊预约错误信息："+ex.Message);
            }
        }
        /// <summary>
        /// 延后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 延后ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = this.gv_Pateion.GetFocusedRow() as Patient;
                if (selectedRow == null)
                    return;
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("患者延后错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 刷新按钮
        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinbutNew_Click(object sender, EventArgs e)
        {
            PatientList();
        }
        #endregion 
    }
}
