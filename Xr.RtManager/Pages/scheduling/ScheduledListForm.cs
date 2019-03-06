using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xr.Http;
using Newtonsoft.Json.Linq;
using Xr.Common;
using System.Threading;
using System.ComponentModel;

namespace Xr.RtManager.Pages.scheduling
{
    public partial class ScheduledListForm : UserControl
    {
        private Form MainForm; //主窗体
        Xr.Common.Controls.OpaqueCommand cmd;

        public ScheduledListForm()
        {
            InitializeComponent();
        }

        private void ScheduledListForm_Load(object sender, EventArgs e)
        {
            MainForm = (Form)this.Parent;
            deBegin.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            deEnd.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            cmd = new Xr.Common.Controls.OpaqueCommand(AppContext.Session.waitControl);
            cmd.ShowOpaqueLayer(0f);
            String param = "hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
            String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?" + param;
            this.DoWorkAsync( 0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DeptEntity> deptList = objT["result"].ToObject<List<DeptEntity>>();
                    if (AppContext.AppConfig.deptCode.Trim().Length == 0)
                    {
                        DeptEntity dept = new DeptEntity();
                        dept.id = " ";
                        dept.parentId = "";
                        dept.name = "全部科室";
                        deptList.Insert(0, dept);
                    }
                    treeDept.Properties.DataSource = deptList;
                    treeDept.Properties.TreeList.KeyFieldName = "id";
                    treeDept.Properties.TreeList.ParentFieldName = "parentId";
                    treeDept.Properties.DisplayMember = "name";
                    treeDept.Properties.ValueMember = "id";
                    treeDept.EditValue = deptList[0].id;

                    gridView1.OptionsView.AllowCellMerge = true;
                    //设置表格中状态下拉框的数据
                    List<DictEntity> dictList = new List<DictEntity>();
                    DictEntity dict = new DictEntity();
                    dict.value = "0";
                    dict.label = "正常";
                    dictList.Add(dict);
                    dict = new DictEntity();
                    dict.value = "1";
                    dict.label = "停诊";
                    dictList.Add(dict);
                    repositoryItemLookUpEdit1.DataSource = dictList;
                    repositoryItemLookUpEdit1.DisplayMember = "label";
                    repositoryItemLookUpEdit1.ValueMember = "value";
                    repositoryItemLookUpEdit1.ShowHeader = false;
                    repositoryItemLookUpEdit1.ShowFooter = false;
                    SearchData();
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                    return;
                }
            });
        }

        private void treeDept_EditValueChanged(object sender, EventArgs e)
        {
            String param = "pageNo=1&pageSize=10000&hospital.id=" + AppContext.Session.hospitalId + "&dept.id=" + treeDept.EditValue;
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/list?" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DoctorInfoEntity> doctorList = objT["result"]["list"].ToObject<List<DoctorInfoEntity>>();
                DoctorInfoEntity doctor = new DoctorInfoEntity();
                doctor.id = "";
                doctor.name = "全部医生";
                doctorList.Insert(0, doctor);
                lueDoctor.Properties.DataSource = doctorList;
                lueDoctor.Properties.DisplayMember = "name";
                lueDoctor.Properties.ValueMember = "id";
                lueDoctor.ItemIndex = 0;
            }
            else
            {
                MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                return;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            cmd.ShowOpaqueLayer();
            SearchData();
        }

        private void SearchData()
        {
            String param = "beginDate=" + deBegin.Text + "&endDate=" + deEnd.Text
    + "&hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + treeDept.EditValue
    + "&doctorId=" + lueDoctor.EditValue;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findByPropertys?" + param;
            this.DoWorkAsync(500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<ScheduledEntity> scheduledList = objT["result"].ToObject<List<ScheduledEntity>>();
                    List<ScheduledEntity> dataSource = new List<ScheduledEntity>();
                    for (int i = 0; i < scheduledList.Count; i++)
                    {
                        ScheduledEntity scheduled = scheduledList[i];
                        scheduled.num = (i + 1).ToString();
                        if (scheduled.period.Equals("0"))
                            scheduled.am = "√";
                        else if (scheduled.period.Equals("1"))
                            scheduled.pm = "√";
                        else if (scheduled.period.Equals("2"))
                            scheduled.night = "√";
                        else if (scheduled.period.Equals("3"))
                            scheduled.allday = "√";
                        dataSource.Add(scheduled);
                    }
                    gcScheduled.DataSource = dataSource;
                    cmd.HideOpaqueLayer();
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                    return;
                }
            });
        }

        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            int rowHandle1 = e.RowHandle1;
            int rowHandle2 = e.RowHandle2;
            string deptName1 = gridView1.GetRowCellValue(rowHandle1, gridView1.Columns["deptName"]).ToString(); //获取科室列值
            string deptName2 = gridView1.GetRowCellValue(rowHandle2, gridView1.Columns["deptName"]).ToString();
            string workDate1 = gridView1.GetRowCellValue(rowHandle1, gridView1.Columns["workDate"]).ToString(); //获取日期列值
            string workDate2 = gridView1.GetRowCellValue(rowHandle2, gridView1.Columns["workDate"]).ToString();
            if (e.Column.FieldName == "deptName") 
            {
                if (deptName1 != deptName2)
                {
                    e.Merge = false; //值相同的2个单元格是否要合并在一起
                    e.Handled = true; //合并单元格是否已经处理过，无需再次进行省缺处理
                }
            }
            else if (e.Column.FieldName == "workDate")
            {
                if (!(deptName1 == deptName2 && workDate1 == workDate2))
                {
                    e.Merge = false; 
                    e.Handled = true; 
                }
            }
            else
            {
                e.Merge = false; 
                e.Handled = true; 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var edit = new RemarksEdit();
            edit.deptId = treeDept.EditValue.ToString();
            edit.doctorId = lueDoctor.EditValue.ToString();
            edit.beginDate = deBegin.Text;
            edit.endDate = deEnd.Text;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                Thread.Sleep(300);
                SearchData();
                MessageBoxUtils.Hint("修改成功!", MainForm);
            }
        }

        private void repositoryItemLookUpEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int selectRow = gridView1.GetSelectedRows()[0];

            if (e.OldValue.Equals("1"))
            {
                this.gridView1.SetRowCellValue(selectRow, gridView1.Columns["status"], e.OldValue);
                MessageBoxUtils.Hint("不能修改停诊状态的排班", MainForm);
                return;
            }

            if (MessageBoxUtils.Show("确定要修改状态吗?", MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                var selectedRow = gridView1.GetFocusedRow() as ScheduledEntity;
                String period = "";
                if (selectedRow.am.Equals("√"))
                    period = "0";
                else if (selectedRow.pm.Equals("√"))
                    period = "1";
                else if (selectedRow.night.Equals("√"))
                    period = "2";
                else if (selectedRow.allday.Equals("√"))
                    period = "3";
                String param = "deptId=" + selectedRow.deptId + "&doctorId=" + selectedRow.doctorId
                    + "&period=" + period + "&workDate=" + selectedRow.workDate
                    + "&status=" + e.NewValue + "&hospitalId=" + AppContext.Session.hospitalId;
                String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/updatestatus?" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("修改成功!", MainForm);
                }
                else
                {
                    this.gridView1.SetRowCellValue(selectRow, gridView1.Columns["status"], e.OldValue);
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                }
            }
            else
            {
                this.gridView1.SetRowCellValue(selectRow, gridView1.Columns["status"], e.OldValue); 
            }
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

        private void ScheduledListForm_Resize(object sender, EventArgs e)
        {
            cmd.rectDisplay = this.DisplayRectangle;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as ScheduledEntity;
            if (selectedRow == null)
                return;
            var edit = new ModifyNumberSourceEdit();
            edit.scheduled = selectedRow;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("修改成功!", MainForm);
                //this.DoWorkAsync(500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                //{
                //    Thread.Sleep(2700);
                //    return null;

                //}, null, (r) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                //{
                //    cmd.ShowOpaqueLayer(255, true);
                //    SearchData(true, pageControl1.CurrentPage, pageControl1.PageSize);
                //});
            }
        }
        
    }
}
