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
using System.Threading;

namespace Xr.RtManager
{
    public partial class DictEdit : Form
    {
        public DictEdit()
        {
            InitializeComponent();
        }

        Xr.Common.Controls.OpaqueCommand cmd;
        public DictEntity dict { get; set; }

        private void DictEdit_Load(object sender, EventArgs e)
        {
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            dcDict.DataType = typeof(DictEntity);
            if (dict != null)
            {
                dcDict.SetValue(dict);
            }
            else
            {
                dict = new DictEntity();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcDict.Validate())
            {
                return;
            }
            dcDict.GetValue(dict);
            String param = "?" + PackReflectionEntity<DictEntity>.GetEntityToRequestParameters(dict);
            String url = AppContext.AppConfig.serverUrl + "sys/sysDict/save" + param;
            cmd.ShowOpaqueLayer();
            this.DoWorkAsync((o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (r) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                cmd.HideOpaqueLayer();
                JObject objT = JObject.Parse(r.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
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
        /// <param name="workFunc">Func委托，包装耗时处理（不含UI界面处理），示例：(o)=>{ 具体耗时逻辑; return 处理的结果数据 }</param>
        /// <param name="funcArg">Func委托参数，用于跨线程传递给耗时处理逻辑所需要的对象，示例：String对象、JObject对象或DataTable等任何一个值</param>
        /// <param name="workCompleted">Action委托，包装耗时处理完成后，下步操作（一般是更新界面的数据或UI控件），示列：(r)=>{ datagirdview1.DataSource=r; }</param>
        protected void DoWorkAsync(Func<object, object> workFunc, object funcArg = null, Action<object> workCompleted = null)
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


                bgWorkder.Dispose();

                if (workCompleted != null)
                {
                    workCompleted(arg.Result);
                }
            };

            bgWorkder.DoWork += (s, arg) =>
            {
                bgWorkder.ReportProgress(1);
                var result = workFunc(arg.Argument);
                arg.Result = result;
                bgWorkder.ReportProgress(100);
                Thread.Sleep(500);
            };

            bgWorkder.RunWorkerAsync(funcArg);
        }

    }
}
