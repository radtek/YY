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
           // PatientList();
        }
        #region 患者列表
        public void PatientList()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", "12");
                prament.Add("doctorId", "12");
                prament.Add("workDate", "12");
                prament.Add("period", "12");
                if (checkEdit1.Checked)
                {
                    prament.Add("status", "0");//候诊中
                }
                else
                {
                    prament.Add("status", "1");//完成
                }
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("cms/holiday/findAll", prament,Method.POST,
                 result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    List<HolidayInfoEntity> a = objT["result"].ToObject<List<HolidayInfoEntity>>();
                                    _context.Send((s) => this.gc_Pateion.DataSource = a,null);
                                    _context.Send((s) => label1.Text=a.Count+"人", null);
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

        /// <summary>
        /// 节假日
        /// </summary>
        public class HolidayInfoEntity
        {
            public String id { set; get; }
            public String name { get; set; }
            public String year { get; set; }
            public String beginDate { get; set; }
            public String endDate { get; set; }
            public String isUse { get; set; }
        }
        #endregion

        #region 菜单选项
        /// <summary>
        /// 菜单选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Pateion_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            //获取选择的行数
            int select = gv_Pateion.SelectedRowsCount;
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
                Form1.pCurrentWin.panel_MainFrm.Controls.Clear();
                RtIntersectionAppointmentFrm rtcpf = new RtIntersectionAppointmentFrm();
                rtcpf.Dock = DockStyle.Fill;
              //  rtcpf.Height = rtcpf.Height + 30;
                Form1.pCurrentWin.panel_MainFrm.Controls.Add(rtcpf);
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 延后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 延后ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion 
    }
}
