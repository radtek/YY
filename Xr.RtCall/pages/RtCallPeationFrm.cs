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
            PatientList();
        }
        #region 患者列表
        public void PatientList()
        {
            try
            {
                string Url = ConfigurationManager.AppSettings["ServerUrl"];
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospital.id", "12");
                string b = "";
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("cms/holiday/findAll", prament,Method.POST,
                 result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.None:
                            break;
                        case ResponseStatus.Completed:
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                var data = result.Data;
                                b = string.Join(",", data.ToArray());
                                JObject objT = JObject.Parse(b);
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    List<HolidayInfoEntity> a = objT["result"].ToObject<List<HolidayInfoEntity>>();
                                    _context.Send((s) => this.gc_Pateion.DataSource = a,null);
                                }
                                else
                                {
                                    MessageBox.Show(objT["message"].ToString());
                                }
                            }
                            break;
                        case ResponseStatus.Error:
                            break;
                        case ResponseStatus.TimedOut:
                            break;
                        case ResponseStatus.Aborted:
                            break;
                    }
                });
                #region
                // prament.Add("deptId", "");//科室主键
                //prament.Add("doctorId", "");//医生主键
                //prament.Add("workDate", "");//就诊日期
                //prament.Add("period", "");//时段，0 上午，1下午，2晚上
                //prament.Add("status", "");//状态：0预约、1候诊中、2已就诊、null全部
                //string str = "";
                //var client = new RestSharpClient(Url+"cms/holiday/findAll");
                //var Params = "";
                //if (prament.Count != 0)
                //{
                //    Params = "?" + string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                //}
                //client.ExecuteAsync<List<string>>(new RestRequest(Params, Method.POST), result =>
                //{
                //    switch (result.ResponseStatus)
                //    {
                //        case ResponseStatus.None:
                //            break;
                //        case ResponseStatus.Completed:
                //            if (result.StatusCode == HttpStatusCode.OK)
                //            {
                //                var data = result.Data;//返回数据
                //                str = string.Join(",", data.ToArray());
                //            }
                //            break;
                //        case ResponseStatus.Error:
                //            MessageBox.Show("请求错误");
                //            break;
                //        case ResponseStatus.TimedOut:
                //            MessageBox.Show("请求超时");
                //            break;
                //        case ResponseStatus.Aborted:
                //            MessageBox.Show("请求终止");
                //            break;
                //        default:
                //            break;
                //    }
                //});
                #endregion
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
