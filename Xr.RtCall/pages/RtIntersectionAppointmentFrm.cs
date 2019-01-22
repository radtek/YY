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
            GetCardType();
            Dictionary<int, DateTime> dc = new Dictionary<int, DateTime>();
            for (int i = 18; i < 25; i++)
            {
                dc.Add(i, System.DateTime.Now);
            }
            reservationCalendar1.ValidDateList = dc;
            reservationCalendar1.SetGridClanderValue();
            _context = SynchronizationContext.Current;
        }
        #region 获取卡类型
        public void GetCardType()
        {
            try
            {
                 Dictionary<string, string> prament = new Dictionary<string, string>();
                 prament.Add("type", "card_type");
                 Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sys/sysDict/findByType", prament, Method.POST,
                result =>
                {
                    #region 
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    _context.Send((s) => lueIsUse.Properties.DataSource = objT["result"].ToObject<List<Xr.RtCall.Model.CardType>>(), null);
                                    _context.Send((s) =>  lueIsUse.Properties.DisplayMember = "label", null); _context.Send((s) =>  lueIsUse.Properties.ValueMember = "value", null);
                                }
                                else
                                {
                                    MessageBox.Show(objT["message"].ToString());
                                }
                            }
                            break;
                    }
                    #endregion
                });
            }
            catch (Exception ex)
            {
            }
        }
        #endregion 
        #region 医生排班日期
        public void DoctorScheduling()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", "12");
                prament.Add("deptId", "2");//科室主键
                prament.Add("doctorId", "1");//医生主键
                prament.Add("type", "1");//类型：0公开预约号源、1诊间预约号源
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/doctorScheduPlan/findByDeptAndDoctor", prament, Method.POST,
               result =>
               {
                   switch (result.ResponseStatus)
                   {
                       case ResponseStatus.None:
                           break;
                       case ResponseStatus.Completed:
                           if (result.StatusCode == System.Net.HttpStatusCode.OK)
                           {
                               JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                               if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                               {
                                  
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
        List<string> list;
        public void TimeScheduling()
        {
            try
            {
                list = new List<string>();
               // AddContronl();
                //this.menuList.setDataSources(null);
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", "12");//医院主键
                prament.Add("deptId", "2");//科室主键
                prament.Add("doctorId", "1");//医生主键
                prament.Add("workDate", "2019-01-16");//排班日期
                prament.Add("type", "1");//类型：0公开预约号源、1诊间预约号源
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/doctorScheduPlan/findTimeNum", prament, Method.POST,
                result =>
                {
                    #region
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                   
                                    List<Xr.RtCall.Model.TimeNum> timenum = objT["result"].ToObject<List<Xr.RtCall.Model.TimeNum>>();
                                    foreach (var item in timenum)
                                    {
                                        list.Add(item.beginTime + "-" + item.endTime + "" + "<" + item.num + ">");
                                    }
                                     _context.Send((s) => this.menuList.setDataSources(list), null);
                                }
                                else
                                {
                                    MessageBox.Show(objT["message"].ToString());
                                }
                            }
                            break;
                    }
                    #endregion
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
                prament.Add("cradNo", label14.Text.Trim());//卡号
                prament.Add("tempPhone", "");//手机号
                prament.Add("note", "");//备注
                #region 
                if (radioButton8.Checked)
                {
                    prament.Add("visitType", "0");//就诊类别：0.初诊 ，1.复诊
                }
                else
                {
                    prament.Add("visitType", "1");//就诊类别：0.初诊 ，1.复诊
                }
                if (radioButton10.Checked)
                {
                    prament.Add("addressType", "0");//地址类别：0市内、1市外  
                }
                else
                {
                    prament.Add("addressType", "1");//地址类别：0市内、1市外  
                }
                if (radioButton6.Checked)
                {
                    prament.Add("isShfz", "1");//术后复诊：0是、1否
                }
                else
                {
                    prament.Add("isShfz", "0");//出院复诊：0是、1否
                }
                if (radioButton12.Checked)
                {
                    prament.Add("isYwzz", "1");//院外转诊：0是、1否
                }
                else
                {
                    prament.Add("isYwzz", "0");//出院复诊：0是、1否
                }
                #endregion
                prament.Add("registerWay", "1");//预约途径：0分诊台、1诊间、2自助机、3公众号、4市平台
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
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                   
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
        /// <summary>
        /// 点击日期控件
        /// </summary>
        /// <param name="SelectedDate"></param>
        private void reservationCalendar1_SelectDate(DateTime SelectedDate)
        {
            label8.Text = SelectedDate.ToString("yyyy-MM-dd");
            TimeScheduling();
            //MessageBox.Show(SelectedDate.ToShortDateString());
        }

        /// <summary>
        /// 给控件赋可选择值
        /// </summary>
        /// <param name="SelectedMonth"></param>
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
        #region 点击菜单控件
        /// <summary>
        /// 点击菜单控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuList_MenuItemClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            DateTime endTime =Convert.ToDateTime(label.Text.Trim().Substring(6,5)).AddMinutes(10);
            label13.Text = endTime.ToString("HH:mm");
        }
        #endregion 
    }
}
