using System;
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
                prament.Add("clinicId", HelperClass.clinicId);//HelperClass.clinicId
                prament.Add("status", Postoperative.EditValue.ToString());
                RestSharpHelper.ReturnResult<List<string>>(InterfaceAddress.findPatientListByDoctor, prament, Method.POST,
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
                                    //if (Postoperative.EditValue != "3")
                                    //{
                                    //    for (int i = 0; i <a.Count; i++)
                                    //    {
                                    //        a[i].workTime = "";
                                    //    }
                                    //}
                                    //if (Postoperative.EditValue=="3")
                                    //{
                                    //   _context.Send((s) =>this.gv_Pateion.Columns["workTime"].Visible = true,null);  //设置列为不显示
                                    //}
                                    //else
                                    //{
                                    //    _context.Send((s) => this.gv_Pateion.Columns["workTime"].Visible = false, null);  //设置列为不显示
                                    //}
                                    _context.Send((s) => this.gc_Pateion.DataSource = a,null);
                                    _context.Send((s) => label1.Text=a.Count+"人", null);
                                }
                                else
                                {
                                    _context.Send((s) => MessageBoxUtils.Hint(objT["message"].ToString(), Form1.pCurrentWin), null);
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
        #endregion 
        #region 刷新按钮
        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinbutNew_Click(object sender, EventArgs e)
        {
            this.skinbutNew.Text = "查询中";
            PatientList();
            //Thread.Sleep(1000);
            this.skinbutNew.Text = "刷新";
        }
        /// <summary>
        /// 单击选择刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Postoperative_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            PatientList();
        }
        #endregion
    }
}
