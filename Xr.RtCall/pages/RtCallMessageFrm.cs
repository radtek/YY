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
        #region 过号下一位
        /// <summary>
        /// 过号下一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinbutNew_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("", "");
                string str = "";
                var client = new RestSharpClient("/yyfz/api/register/findToDoctor");
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
    }
}
