using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Http;
using System.IO;
using System.Net;
using Xr.Common;
using System.Threading;
using System.Text.RegularExpressions;

namespace Xr.RtManager.Pages.scheduling
{
    public partial class ModifyNumberSourceEdit : Form
    {
        public ModifyNumberSourceEdit()
        {
            InitializeComponent();
        }

        Xr.Common.Controls.OpaqueCommand cmd;

        public ScheduledEntity scheduled { get; set; }

        private void ModifyNumberSourceEdit_Load(object sender, EventArgs e)
        {
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            String sdName = "";
            if (scheduled.period.Equals("0")) sdName = "上午";
            if (scheduled.period.Equals("1")) sdName = "下午";
            if (scheduled.period.Equals("2")) sdName = "晚上";
            if (scheduled.period.Equals("3")) sdName = "全天";
            label1.Text = "科室：" + scheduled.deptName + "        医生：" + scheduled.doctorName + "        日期：" + scheduled.workDate + sdName;
            String param = "hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + scheduled.deptId
                + "&doctorId=" + scheduled.doctorId + "&workDate=" + scheduled.workDate
                + "&period=" + scheduled.period;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findScheduList?" + param;
            cmd.ShowOpaqueLayer(0f);
            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<SourceDataEntity> sourceDataList = objT["result"].ToObject<List<SourceDataEntity>>();
                    gcSourceData.DataSource = sourceDataList;
                    cmd.HideOpaqueLayer();
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                    return;
                }
            });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<SourceDataEntity> sourceDataList = gcSourceData.DataSource as List<SourceDataEntity>;
            String scheduSets = Newtonsoft.Json.JsonConvert.SerializeObject(sourceDataList);
            String param = "scheduSets=" + scheduSets;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/updateScheduList?" + param;
            cmd.ShowOpaqueLayer();
            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    cmd.HideOpaqueLayer();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                    return;
                }
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 多线程异步后台处理某些耗时的数据，不会卡死界面
        /// </summary>
        /// <param name="time">线程延迟多少</param>
        /// <param name="workFunc">Func委托，包装耗时处理（不含UI界面处理），示例：(o)=>{ 具体耗时逻辑; return 处理的结果数据 }</param>
        /// <param name="funcArg">Func委托参数，用于跨线程传递给耗时处理逻辑所需要的对象，示例：String对象、JObject对象或DataTable等任何一个值</param>
        /// <param name="workCompleted">Action委托，包装耗时处理完成后，下步操作（一般是更新界面的数据或UI控件），示列：(r)=>{ datagirdview1.DataSource=r; }</param>
        protected void DoWorkAsync(int time, Func<object, object> workFunc, object funcArg = null, Action<object> workCompleted = null)
        {
            var bgWorkder = new BackgroundWorker();


            //Form loadingForm = null;
            //System.Windows.Forms.Control loadingPan = null;
            bgWorkder.WorkerReportsProgress = true;
            bgWorkder.ProgressChanged += (s, arg) =>
            {
                if (arg.ProgressPercentage > 1) return;

            };

            bgWorkder.RunWorkerCompleted += (s, arg) =>
            {

                try
                {
                    bgWorkder.Dispose();

                    if (workCompleted != null)
                    {
                        workCompleted(arg.Result);
                    }
                }
                catch (Exception ex)
                {
                    cmd.HideOpaqueLayer();
                    throw new Exception(ex.InnerException.Message);
                }
            };

            bgWorkder.DoWork += (s, arg) =>
            {
                bgWorkder.ReportProgress(1);
                var result = workFunc(arg.Argument);
                arg.Result = result;
                bgWorkder.ReportProgress(100);
                Thread.Sleep(time);
            };

            bgWorkder.RunWorkerAsync(funcArg);
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //正整数验证 @"^[1-9]\d*$"
            var regex = new Regex(@"^[0-9]*[1-9][0-9]*$", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            if (!regex.IsMatch(e.Value.ToString()))
            {
                e.ErrorText = "只能输入正整数！";
                e.Valid = false;
                return;
            }
        }
    }
}
