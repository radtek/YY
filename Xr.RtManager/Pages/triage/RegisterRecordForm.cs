using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xr.Common;
using Xr.Common.Controls;
using Xr.Http;
using Xr.RtManager.Utils;

namespace Xr.RtManager.Pages.triage
{

    public partial class RegisterRecordForm : Form
    {
        public List<PatientReservationInfoEntity> list{get; set;}

        public String registerId;//预约id，医生停诊用的

        public RegisterRecordForm()
        {
            InitializeComponent();
        }

        private void RegisterRecordForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 刷新界面数据
        /// </summary>
        private void RefreshData()
        {
            panel1.Controls.Clear();
            foreach (var item in list)
            {
                ReservationInfoPanel pan = new ReservationInfoPanel();
                pan.Dock = DockStyle.Top;
                pan.lab_state.Text = item.statusTxt;
                pan.lab_dept.Text = item.deptName;
                pan.lab_doc.Text = item.doctorName;
                pan.lab_name.Text = item.patientName;
                pan.lab_reservationTime.Text = item.registerTime;
                pan.obj = item;
                if (item.status.Equals("0"))//待签到
                {
                    pan.btn_Operation.Text = "预约签到";
                }
                else if (item.status.Equals("1"))//已签到(候诊中)
                {
                    pan.btn_Operation.Text = "取消候诊";
                }
                else if (item.status.Equals("2"))//就诊中
                {
                    pan.btn_Operation.Visible = false;
                }
                else if (item.status.Equals("3"))//已就诊(需要显示补打按钮)
                {
                    pan.btn_Operation.Text = "复诊签到";
                    pan.btn_buDa.Visible = true;
                    pan.btn_buDa.Tag = item.registerId;
                    pan.BtnBuDaClick += new Xr.Common.Controls.ReservationInfoPanel.BuDaClick(this.btn_BuDa_Click);
                }
                else if (item.status.Equals("6"))//医生停诊需要转诊
                {
                    pan.btn_Operation.Text = "选择医生";
                }

                pan.BtnOperationClick += new Xr.Common.Controls.ReservationInfoPanel.OperationClick(this.btn_Operation_Click);

                panel1.Controls.Add(pan);
            }
        }

        private void btn_Operation_Click(object sender, EventArgs e, Object obj)
        {
            PatientReservationInfoEntity pr = obj as PatientReservationInfoEntity;
            if (pr.status == "0") //未分诊，调用签到接口
            {
                String param = "registerId=" + pr.registerId;
                String url = AppContext.AppConfig.serverUrl + "sch/registerTriage/signIn?" + param;
                this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                {
                    String data = HttpClass.httpPost(url);
                    return data;

                }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                {
                    //cmd.HideOpaqueLayer();
                    JObject objT = JObject.Parse(data.ToString());
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        RefreshData();
                        //打印小票
                        PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), pr.patientName, objT["result"]["waitingNum"].ToString(), objT["result"]["currentTime"].ToString(), objT["result"]["tipMsg"].ToString(), objT["result"]["doctorTip"].ToString());
                        string message = "";
                        if (!print.Print(ref message))
                        {
                            MessageBoxUtils.Show("打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                        }
                        else
                        {
                            MessageBoxUtils.Hint("打印小票完成", this);
                        }
                    }
                    else
                    {
                        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                    }
                });
            }
            else if (pr.status == "1") //已签到取消候诊
            {
                String param = "registerId=" + pr.registerId;
                String url = AppContext.AppConfig.serverUrl + "sch/registerTriage/cancelWaiting?" + param;
                this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                {
                    String data = HttpClass.httpPost(url);
                    return data;

                }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                {
                    //cmd.HideOpaqueLayer();
                    JObject objT = JObject.Parse(data.ToString());
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        RefreshData();
                    }
                    else
                    {
                        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                    }
                });
            }
            else if (pr.status == "2") //就诊中不需要操作
            {
                return;
            }
            else if (pr.status == "3") //已就诊，调用复诊签到接口
            {
                String param = "registerId=" + pr.registerId;
                String url = AppContext.AppConfig.serverUrl + "sch/registerTriage/reviewSignIn?" + param;
                this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                {
                    String data = HttpClass.httpPost(url);
                    return data;

                }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                {
                    //cmd.HideOpaqueLayer();
                    JObject objT = JObject.Parse(data.ToString());
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        RefreshData();
                        //打印小票
                        PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), pr.patientName, objT["result"]["waitingNum"].ToString(), objT["result"]["currentTime"].ToString(), objT["result"]["tipMsg"].ToString(), objT["result"]["doctorTip"].ToString());
                        string message = "";
                        if (!print.Print(ref message))
                        {
                            MessageBoxUtils.Show("打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                        }
                        else
                        {
                            MessageBoxUtils.Hint("打印小票完成", this);
                        }
                    }
                    else
                    {
                        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                    }
                });
            }

            else if (pr.status == "6") //该患者预约的医生已停诊，请选择其他医生签到
            {
                this.registerId = pr.registerId;
                DialogResult = DialogResult.No;
            }
        }

        /// <summary>
        /// 补打指引单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_BuDa_Click(object sender, EventArgs e, Object obj)
        {
            PatientReservationInfoEntity pr = obj as PatientReservationInfoEntity;
            String param = "registerId=" + pr.registerId;
            String url = AppContext.AppConfig.serverUrl + "sch/registerTriage/waitingList?" + param;
            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                //cmd.HideOpaqueLayer();
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), pr.patientName, objT["result"]["waitingNum"].ToString(), objT["result"]["currentTime"].ToString(), objT["result"]["tipMsg"].ToString(), objT["result"]["doctorTip"].ToString());
                    string message = "";
                    if (!print.Print(ref message))
                    {
                        MessageBoxUtils.Show("打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                    }
                    else
                    {
                        MessageBoxUtils.Hint("打印小票完成", this);
                    }
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                }
            });
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
                    if (ex.InnerException != null)
                        throw new Exception(ex.InnerException.Message);
                    else
                        
                        throw new Exception(ex.Message);
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

    }
}
