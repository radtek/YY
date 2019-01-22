using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Xr.Http.RestSharp;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Xr.RtCall.pages
{
    public partial class RtIntersectionAppointmentFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtIntersectionAppointmentFrm()
        {
            InitializeComponent();
            Dictionary<int, DateTime> dc = new Dictionary<int, DateTime>();
            for (int i = 18; i < 25; i++)
            {
                dc.Add(i, System.DateTime.Now);
            }
            reservationCalendar1.ValidDateList = dc;
            reservationCalendar1.SetGridClanderValue();
            _context = SynchronizationContext.Current;
        }
        #region 医生排班日期
        public void DoctorScheduling()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", "");//科室主键
                prament.Add("doctorId", "");//医生主键
                prament.Add("type", "");//类型：0公开预约号源、1诊间预约号源
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/clinicCall/inPlace", prament, Method.POST,
               result =>
               {
                   switch (result.ResponseStatus)
                   {
                       case ResponseStatus.None:
                           break;
                       case ResponseStatus.Completed:
                           if (result.StatusCode == System.Net.HttpStatusCode.OK)
                           {
                               var data = result.Data;
                               string b = string.Join(",", data.ToArray());
                               JObject objT = JObject.Parse(b);
                               if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                               {
                                   //if (isStop == 1)
                                   //{
                                   //    _context.Send((s) => this.skinbutLook.Text = "继续开诊", null);
                                   //}
                                   //else
                                   //{
                                   //    _context.Send((s) => this.skinbutLook.Text = "临时停诊", null);
                                   //}
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
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region 日期排班号源
        public void TimeScheduling()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", "");//科室主键
                prament.Add("doctorId", "");//医生主键
                prament.Add("workDate", "");//排班日期
                prament.Add("type", "");//类型：0公开预约号源、1诊间预约号源
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/clinicCall/inPlace", prament, Method.POST,
                result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.None:
                            break;
                        case ResponseStatus.Completed:
                            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var data = result.Data;
                                string b = string.Join(",", data.ToArray());
                                JObject objT = JObject.Parse(b);
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    //if (isStop == 1)
                                    //{
                                    //    _context.Send((s) => this.skinbutLook.Text = "继续开诊", null);
                                    //}
                                    //else
                                    //{
                                    //    _context.Send((s) => this.skinbutLook.Text = "临时停诊", null);
                                    //}
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
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region 返回
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butReturn_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.pCurrentWin.panel_MainFrm.Controls.Clear();
                RtCallPeationFrm rtcpf = new RtCallPeationFrm();
                rtcpf.Dock = DockStyle.Fill;
                Form1.pCurrentWin.panel_MainFrm.Controls.Add(rtcpf);
            }
            catch (Exception)
            {
            }
        }
        #endregion 
        #region  确认预约
        /// <summary>
        /// 确认预约
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButYes_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("scheduPlanId", "");//排班记录主键
                prament.Add("patientId", "");//患者主键
                prament.Add("patientName", "");//患者姓名
                prament.Add("cradType", "");//卡类型
                prament.Add("cradNo", "");//卡号
                prament.Add("tempPhone", "");//手机号
                prament.Add("note", "");//备注
                prament.Add("visitType", "");//就诊类别：0.初诊 ，1.复诊
                prament.Add("addressType", "");//地址类别：0市内、1市外   
                prament.Add("isShfz", "");//术后复诊：0是、1否
                prament.Add("isYwzz", "");//院外转诊：0是、1否
                prament.Add("isCyfz", "");//出院复诊：0是、1否
                prament.Add("registerWay", "");//预约途径：0分诊台、1诊间、2自助机、3公众号、4市平台
                //string str = "";
                //var client = new RestSharpClient("/yyfz/api/register/confirmBooking");
                //var Params = "";
                //if (prament.Count != 0)
                //{
                //    Params = "?" + string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                //}
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/clinicCall/inPlace", prament, Method.POST,
                result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.None:
                            break;
                        case ResponseStatus.Completed:
                            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var data = result.Data;
                                string b = string.Join(",", data.ToArray());
                                JObject objT = JObject.Parse(b);
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    //if (isStop == 1)
                                    //{
                                    //    _context.Send((s) => this.skinbutLook.Text = "继续开诊", null);
                                    //}
                                    //else
                                    //{
                                    //    _context.Send((s) => this.skinbutLook.Text = "临时停诊", null);
                                    //}
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
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region 
        private void reservationCalendar1_SelectDate(DateTime SelectedDate)
        {
            MessageBox.Show(SelectedDate.ToShortDateString());
        }

        private void reservationCalendar1_SelectDateTest(DateTime SelectedDate)
        {
            MessageBox.Show(SelectedDate.ToShortDateString() + "测试事件");
        }

        private void reservationCalendar1_ChangeMonth(DateTime SelectedMonth)
        {
            Dictionary<int, DateTime> dc = new Dictionary<int, DateTime>();
            for (int i = 10; i < SelectedMonth.Month + 20; i++)
            {
                dc.Add(i, System.DateTime.Now);
            }
            reservationCalendar1.ValidDateList = dc;
            reservationCalendar1.SetGridClanderValue();
        }
        #endregion 
    }
}
