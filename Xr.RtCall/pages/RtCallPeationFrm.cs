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

namespace Xr.RtCall.pages
{
    public partial class RtCallPeationFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtCallPeationFrm()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }
        #region 患者列表
        public void PatientList()
        {
            try
            {
               Dictionary<string, string> prament = new Dictionary<string, string>();
               prament.Add("","");
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
                                   this.gc_Pateion.DataSource = str
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
