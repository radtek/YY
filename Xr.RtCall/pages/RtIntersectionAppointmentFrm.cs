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
using Xr.RtCall.Model;

namespace Xr.RtCall.pages
{
    public partial class RtIntersectionAppointmentFrm : UserControl
    {
        public SynchronizationContext _context;
        int selectLue = 0;
        public RtIntersectionAppointmentFrm(Xr.RtCall.Model.Patient patient)
        {
            InitializeComponent();
            label14.Text = patient.cradNo;
            GetCardType();//获取卡类型
            DoctorScheduling();//获取医生排班日期
            selectLue = Convert.ToInt32(patient.cradType);
            _context = SynchronizationContext.Current;
        }
        #region 获取卡类型
        /// <summary>
        /// 获取卡类型
        /// </summary>
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
                                Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    List<CardType> lsit=objT["result"].ToObject<List<CardType>>();
                                    _context.Send((s) => lueIsUse.Properties.DataSource =lsit, null);
                                    _context.Send((s) =>  lueIsUse.Properties.DisplayMember = "label", null); 
                                    _context.Send((s) =>  lueIsUse.Properties.ValueMember = "value", null);
                                    _context.Send((s) => lueIsUse.ItemIndex=selectLue, null);
                                }
                                else
                                {
                                    _context.Send((s) => Xr.Common.MessageBoxUtils.Hint(objT["message"].ToString()), null);
                                }
                            }
                            break;
                    }
                    #endregion
                });
            }
            catch (Exception ex)
            {
               Log4net.LogHelper.Error("获取卡类型错误信息："+ex.Message);
            }
        }
        #endregion 
        #region 医生排班日期
       
        /// <summary>
        ///  医生排班日期
        /// </summary>
        public void DoctorScheduling()
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);
                prament.Add("deptId", HelperClass.deptId);//科室主键
                prament.Add("doctorId", HelperClass.doctorId);//医生主键
                prament.Add("type", "1");//类型：0公开预约号源、1诊间预约号源
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/doctorScheduPlan/findByDeptAndDoctor", prament, Method.POST,
               result =>
               {
                   switch (result.ResponseStatus)
                   {
                       case ResponseStatus.Completed:
                           if (result.StatusCode == System.Net.HttpStatusCode.OK)
                           {
                               Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                               JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                               if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                               {
                                   JObject obj = JObject.Parse(@"{""code"":200,""message"":""操作成功"",""result"":[{""workDate"":""2019-02-01""},{""workDate"":""2019-02-02""},{""workDate"":""2019-02-18""},{""workDate"":""2019-02-21""},{""workDate"":""2019-02-22""},{""workDate"":""2019-02-23""},{""workDate"":""2019-03-03""},{""workDate"":""2019-03-13""},{""workDate"":""2019-03-14""},{""workDate"":""2019-03-15""},{""workDate"":""2019-03-16""},{""workDate"":""2019-03-17""},{""workDate"":""2019-03-18""},{""workDate"":""2019-03-19""},{""workDate"":""2019-03-20""},{""workDate"":""2019-03-21""},{""workDate"":""2019-03-22""},{""workDate"":""2019-03-23""},{""workDate"":""2019-03-24""},{""workDate"":""2019-04-13""},{""workDate"":""2019-04-15""},{""workDate"":""2019-06-15""}],""state"":true}");
                                   List<Dictionary<int, DateTime>> dcs = new List<Dictionary<int, DateTime>>();
                                   List<AvaDateEntity> list = obj["result"].ToObject<List<AvaDateEntity>>();
                                   Dictionary<int, DateTime> dc1 = new Dictionary<int, DateTime>();
                                   string Month = list[0].month;
                                   foreach (var item in list)
                                   {
                                       if (item.month != Month)
                                       {
                                           dcs.Add(dc1);
                                           dc1 = new Dictionary<int, DateTime>();
                                           Month = item.month;
                                       }
                                       dc1.Add(Int32.Parse(item.day), System.DateTime.Now);
                                   }
                                   dcs.Add(dc1);
                                   _context.Send((s) => reservationCalendar1.ChangeValidDate(dcs),null);
                               }
                               else
                               {
                                   _context.Send((s) => Xr.Common.MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1), null);
                               }
                           }
                           break;
                   }
               });
            }
            catch (Exception ex)
            {
               Log4net.LogHelper.Error("获取医生排班号源错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 日期排班号源
        List<string> list;
        /// <summary>
        /// 日期排班号源
        /// </summary>
        /// <param name="time">日期</param>
        public void TimeScheduling(string time)
        {
            try
            {
                list = new List<string>();
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("hospitalId", HelperClass.hospitalId);//医院主键
                prament.Add("deptId", HelperClass.deptId);//科室主键
                prament.Add("doctorId", HelperClass.doctorId);//医生主键
                prament.Add("workDate", time);//排班日期
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
                                Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {

                                    List<TimeNum> timenum = objT["result"].ToObject<List<TimeNum>>();
                                    List<TimeNum> timenums = timenum.OrderByDescending(o => o.beginTime).ToList();
                                    foreach (var item in timenums)
                                    {
                                        list.Add(item.beginTime + "-" + item.endTime + "" + "<" + item.num + ">");
                                    }
                                    _context.Send((s) => this.menuList.setDataSources(list, true), null);
                                }
                                else
                                {
                                 _context.Send((s) =>Xr.Common.MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1),null);
                                }
                            }
                            break;
                    }
                    #endregion
                });
            }
            catch (Exception ex)
            {
              Log4net.LogHelper.Error("获取日期排班号源错误信息：" + ex.Message);
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
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("返回按钮错误信息："+ex.Message);
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
                prament.Add("scheduPlanId", "10");//排班记录主键
                prament.Add("patientId", "000675493100");//患者主键
                prament.Add("patientName", "李鹏真");//患者姓名
                prament.Add("cradType", "1");//卡类型
                prament.Add("cradNo", label14.Text.Trim());//卡号
                prament.Add("tempPhone", "17666476268");//手机号
                prament.Add("note", "");//备注
                prament.Add("visitType", VisitCategory.EditValue.ToString());//就诊类别：0.初诊 ，1.复诊
                prament.Add("addressType", AddressCategory.EditValue.ToString());//地址类别：0市内、1市外  
                prament.Add("isShfz", Postoperative.EditValue.ToString());//术后复诊：0是、1否
                prament.Add("isYwzz", Foreign.EditValue.ToString());//院外转诊：0是、1否
                prament.Add("isCyfz", Discharged.EditValue.ToString());//出院复诊：0是、1否
                prament.Add("isMxb", Chronic.EditValue.ToString());//慢性病：0是、1否
                prament.Add("registerWay", "1");//预约途径：0分诊台、1诊间、2自助机、3公众号、4市平台
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/doctorScheduRegister/confirmBooking", prament, Method.POST,
                result =>
                {
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Log4net.LogHelper.Info("请求结果：" + string.Join(",", result.Data.ToArray()));
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                    _context.Send((s) => Xr.Common.MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1), null);
                                }
                                else
                                {
                                    _context.Send((s) => Xr.Common.MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1), null);
                                }
                            }
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Xr.Common.MessageBoxUtils.Show("确认预约错误信息" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Log4net.LogHelper.Error("确认预约错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 日期控件
        /// <summary>
        /// 点击日期控件
        /// </summary>
        /// <param name="SelectedDate"></param>
        private void reservationCalendar1_SelectDate(DateTime SelectedDate)
        {
            label8.Text = SelectedDate.ToString("yyyy-MM-dd");
            string time = "2019-02-19";
            TimeScheduling(time);
            //MessageBox.Show(SelectedDate.ToShortDateString());
        }

        /// <summary>
        /// 给控件赋可选择值
        /// </summary>
        /// <param name="SelectedMonth"></param>
        //private void reservationCalendar1_ChangeMonth(DateTime SelectedMonth)
        //{
        //    Dictionary<int, DateTime> dc = new Dictionary<int, DateTime>();
        //    for (int i = 10; i < SelectedMonth.Month + 20; i++)
        //    {
        //        dc.Add(i, System.DateTime.Now);
        //    }
        //    reservationCalendar1.ValidDateList = dc;
        //    reservationCalendar1.SetGridClanderValue();
        //}
        #endregion 
        #region 点击菜单控件
        /// <summary>
        /// 点击菜单控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuList_MenuItemClick(object sender, EventArgs e)
        {
            Label label = null;
            if (typeof(Label).IsInstanceOfType(sender))
            {
                label = (Label)sender;
            }
            else
            {
                Xr.Common.Controls.PanelEx panelEx = (Xr.Common.Controls.PanelEx)sender;
                label = (Label)panelEx.Controls[0];
            }
            //DateTime endTime = Convert.ToDateTime(label.Text.Trim().Substring(6, 5)).AddMinutes(10);
            label13.Text = label.Text.Trim().Substring(0, 11);//endTime.ToString("HH:mm");
        }
        #endregion 
    }
}
