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

namespace Xr.RtCall.pages
{
    public partial class RtIntersectionAppointmentFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtIntersectionAppointmentFrm()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
            listTimes.Items.Add("9:00-9:30<5>");
            listTimes.Items.Add("9:30-10:00<5>");
            listTimes.Items.Add("10:00-10:30<5>");
            listTimes.Items.Add("11:00-11:30<5>");
            listTimes.Items.Add("11:30-12:00<5>");
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
                string str = "";
                var client = new RestSharpClient("/yyfz/api/scheduPlan/findByDeptAndDoctor");
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
                                    MessageBox.Show("剩余号源数")
                                    //this..DataSource = str
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
                string str = "";
                var client = new RestSharpClient("/yyfz/api/scheduPlan/findTimeNum");
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
                                    MessageBox.Show("剩余号源数")
                                    //this..DataSource = str
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
        #region 返回
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButReturn_Click(object sender, EventArgs e)
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
                string str = "";
                var client = new RestSharpClient("/yyfz/api/register/confirmBooking");
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
                                    MessageBox.Show("预约成功")
                                    //this..DataSource = str
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
    }
}
