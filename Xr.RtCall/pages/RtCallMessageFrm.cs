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
    public partial class RtCallMessageFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtCallMessageFrm()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }
        #region 完成下一位/过号下一位
        /// <summary>
        /// 完成下一位/过号下一位
        /// </summary>
        /// <param name="triageId">分诊记录主键，第一次可空</param>
        /// <param name="type">操作类型：0完成、1过号</param>
        public void PatientOkAndNext(string triageId,string type)
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("triageId", triageId);
                prament.Add("Type", type);
                string str = "";
                var client = new RestSharpClient("/yyfz/api/call/callNextPerson");
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
                                   MessageBox.Show("")
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
        #region 过号下一位
        /// <summary>
        /// 过号下一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinbutNew_Click(object sender, EventArgs e)
        {
            PatientOkAndNext("","1");
        }
        #endregion 
        #region 患者到诊
        /// <summary>
        /// 患者到诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButton1_Click(object sender, EventArgs e)
        {
            PatientOkAndNext("", "0");
        }
        #endregion 
    }
}
