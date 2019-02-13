using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xr.Http;
using Newtonsoft.Json.Linq;
using Xr.Common;

namespace Xr.RtManager.Pages.scheduling
{
    public partial class ScheduledListForm : UserControl
    {
        Xr.Common.Controls.OpaqueCommand cmd;

        public ScheduledListForm()
        {
            InitializeComponent();
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            //cmd.ShowOpaqueLayer(225, true);
            //cmd.HideOpaqueLayer();
        }

        private void ScheduledListForm_Load(object sender, EventArgs e)
        {
            gridView1.OptionsView.AllowCellMerge = true;
            //这里不能直接用，不然下面添加全部科室会导致session里面的科室列表也添加了全部科室
            List<DeptEntity> deptList = new List<DeptEntity>();
            foreach(DeptEntity deptEntity in AppContext.Session.deptList){
                deptList.Add(deptEntity);
            }
            DeptEntity dept = new DeptEntity();
            dept.id = "";
            dept.name = "全部科室";
            deptList.Insert(0, dept);
            lueDept.Properties.DataSource = deptList;
            lueDept.Properties.DisplayMember = "name";
            lueDept.Properties.ValueMember = "id";
            lueDept.ItemIndex = 0;

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
            repositoryItemLookUpEdit1.Properties.DataSource = dictList;
            repositoryItemLookUpEdit1.Properties.DisplayMember = "label";
            repositoryItemLookUpEdit1.Properties.ValueMember = "value";
            repositoryItemLookUpEdit1.ShowHeader = false;
            repositoryItemLookUpEdit1.ShowFooter = false;
        }

        private void lueDept_EditValueChanged(object sender, EventArgs e)
        {
            String param = "pageNo=1&pageSize=10000&hospital.id=" + AppContext.Session.hospitalId + "&dept.id=" + lueDept.EditValue;
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
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void SearchData()
        {
            String param = "beginDate=" + deBegin.Text + "&endDate=" + deEnd.Text
    + "&hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + lueDept.EditValue
    + "&doctorId=" + lueDoctor.EditValue;
            cmd.ShowOpaqueLayer(225, true);
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findByPropertys?" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<ScheduledEntity> scheduledList = objT["result"].ToObject<List<ScheduledEntity>>();
                List<ScheduledEntity> dataSource = new List<ScheduledEntity>();
                for (int i = 0; i < scheduledList.Count; i++)
                {
                    ScheduledEntity scheduled = scheduledList[i];
                    scheduled.num = (i + 1).ToString();
                    dataSource.Add(scheduled);
                }
                cmd.HideOpaqueLayer();
                gcScheduled.DataSource = dataSource;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
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
            edit.deptId = lueDept.EditValue.ToString();
            edit.doctorId = lueDoctor.EditValue.ToString();
            edit.beginDate = deBegin.Text;
            edit.endDate = deEnd.Text;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("修改成功!");
                SearchData();
            }
        }

        private void repositoryItemLookUpEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int selectRow = gridView1.GetSelectedRows()[0];

            if (e.OldValue.Equals("1"))
            {
                this.gridView1.SetRowCellValue(selectRow, gridView1.Columns["status"], e.OldValue); 
                MessageBoxUtils.Hint("不能修改停诊状态的排班");
                return;
            }

            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要修改状态吗?", "修改状态", messButton);

            if (dr == DialogResult.OK)
            {
                var selectedRow = gridView1.GetFocusedRow() as ScheduledEntity;
                String period = "";
                if(selectedRow.am.Equals("√"))
                    period = "0";
                else if(selectedRow.pm.Equals("√"))
                    period = "1";
                else if(selectedRow.night.Equals("√"))
                    period = "2";
                else if(selectedRow.allday.Equals("√"))
                    period = "3";
                String param = "deptId=" + selectedRow.deptId + "&doctorId=" + selectedRow.doctorId
                    + "&period=" + period + "&workDate=" + selectedRow.workDate
                    + "&status=" + e.NewValue + "&hospitalId=" + AppContext.Session.hospitalId;
                String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/updatestatus?" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("修改成功!");
                }
                else
                {
                    this.gridView1.SetRowCellValue(selectRow, gridView1.Columns["status"], e.OldValue); 
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            else
            {
                this.gridView1.SetRowCellValue(selectRow, gridView1.Columns["status"], e.OldValue); 
            }
        }
    }
}
