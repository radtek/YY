using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using Xr.Common;
using Xr.Common.Controls;
using DevExpress.XtraEditors;

namespace Xr.RtManager.Pages.scheduling
{
    public partial class SingleSchedulingForm : UserControl
    {
        public SingleSchedulingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 出诊信息模板
        /// </summary>
        public DefaultVisitEntity defaultVisitTemplate { get; set; }

        private int tipHeight = 0; //排班控件高度

        private void SingleSchedulingForm_Load(object sender, EventArgs e)
        {
            //设置科室列表
            List<Item> itemList = new List<Item>();
            foreach (DeptEntity dept in AppContext.Session.deptList)
            {
                Item item = new Item();
                item.name = dept.name;
                item.value = dept.id;
                item.tag = dept.hospitalId;
                item.parentId = dept.parentId;
                itemList.Add(item);
            }
            mcDept.setDataSource(itemList);

            //查询状态下拉框数据
            String url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=is_use";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueIsUse.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueIsUse.Properties.DisplayMember = "label";
                lueIsUse.Properties.ValueMember = "value";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        private void menuControl2_MenuItemClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            String hospitalId = label.Tag.ToString();
            String deptId = label.Name;
            String deptName = label.Text;

            String param = "pageNo=1&pageSize=10000&hospital.id=" + hospitalId + "&dept.id=" + deptId;
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/list?" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DoctorInfoEntity> doctorList = objT["result"]["list"].ToObject<List<DoctorInfoEntity>>();
                //设置医生列表
                List<Item> itemList = new List<Item>();
                foreach (DoctorInfoEntity doctor in doctorList)
                {
                    Item item = new Item();
                    item.name = doctor.name;
                    item.value = doctor.id;
                    item.tag = doctor.code;
                    itemList.Add(item);
                }
                mcDoctor.setDataSource(itemList);
                    
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        private String workDate;

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //清除排班数据
            panel9.Controls.Clear();

            if (dateEdit1.Text == null || dateEdit1.Text.ToString().Length == 0)
            {
                dataController1.ShowError(dateEdit1, "日期不能为空");
                return;
            }

            workDate = dateEdit1.Text;

            CheckState morning = cbMorning.CheckState;
            CheckState afternoon = cbAfternoon.CheckState;
            CheckState night = cbNight.CheckState;
            CheckState allDay = cbAllAay.CheckState;

            List<String> periodList = new List<String>();
            if (morning == CheckState.Checked) periodList.Add("0");
            if (afternoon == CheckState.Checked) periodList.Add("1");
            if (night == CheckState.Checked) periodList.Add("2");
            if (allDay == CheckState.Checked) periodList.Add("3");
            if(periodList.Count==0){
                dataController1.ShowError(cbAllAay, "至少选一个");
                return;
            }
            String param = null;
            String url = null;
            String data = null;
            JObject objT = null;
            for (int i = 0; i < periodList.Count; i++)
            {
                param = "deptId=" + mcDept.itemName + "&doctorId=" + mcDoctor.itemName
                    + "&hospitalId=" + mcDept.itemTag + "&workDate=" + workDate
                    + "&period=" + periodList[i];
                url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/isExist?" + param;
                data = HttpClass.httpPost(url);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    if (string.Compare(objT["result"].ToString(), "true", true) != 0){
                        String sd = "";
                        if (periodList[i] == "0") sd = "上午";
                        else if (periodList[i] == "1") sd = "下午";
                        else if (periodList[i] == "2") sd = "晚午";
                        else if (periodList[i] == "3") sd = "全天";
                        labMsg.Text = "该日期" + sd + "已有排班，请先在【排班列表中停诊或者删除】";
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            for (int i = periodList.Count-1; i >= 0 ; i--)
            {
                param = "deptId=" + mcDept.itemName + "&doctorId=" + mcDoctor.itemName
                    + "&workDate=" + workDate + "&period=" + periodList[i];
                url = AppContext.AppConfig.serverUrl + "cms/doctorVisitingTime/findByPropertys?"+param;
                data = HttpClass.httpPost(url);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<WorkingDayEntity> workingDayList = objT["result"].ToObject<List<WorkingDayEntity>>();
                    setWorkingDay(workingDayList, periodList[i]);
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }

            for (int i = 0; i < panel9.Controls.Count; i++)
            {
                Panel panel = (Panel)panel9.Controls[i];
                GroupBox gb = (GroupBox)panel.Controls[0];
                if (gb.Height < panel.Height) gb.Height = panel.Height;
            }
        }

        /// <summary>
        /// 生成排班
        /// </summary>
        /// <param name="workingDayList"></param>
        /// <param name="period"></param>
        private void setWorkingDay(List<WorkingDayEntity> workingDayList, String period)
        {
            #region 生成排班
            Panel panelPb = new Panel();
            panelPb.Width = 365;
            panelPb.Dock = DockStyle.Left;
            panelPb.AutoScroll = true;
            GroupBox groupBox = new GroupBox();
            groupBox.AutoSize = true;
            groupBox.Font = new Font("微软雅黑", 10);

            //当前TableLayoutPanel的数量
            List<WorkingDayEntity> wdwpList = getWorkingDayData(workingDayList, period);
            //多少行数据
            int rowNum = wdwpList.Count;
            //period=0:上午 period=1:下午 period=2:晚上 period=3:全天
            TableLayoutPanel tlpMorning = new TableLayoutPanel();
            if (rowNum == 0) tlpMorning.Enabled = false;
            int row = 0;//行数(包括标题)
            String timeInterval = ""; //
            if (rowNum > 3) row = rowNum + 1;
            else row = 4;
            CheckState checkState = CheckState.Unchecked;
            CheckState checkAuto = CheckState.Unchecked;
            if (rowNum == 0)
                checkState = CheckState.Unchecked;
            else if (wdwpList[0].isUse.Equals("0"))
                checkState = CheckState.Checked;
            else
                checkState = CheckState.Unchecked;
            if (rowNum == 0)
                checkAuto = CheckState.Unchecked;
            else if (wdwpList[0].autoSchedule.Equals("0"))
                checkAuto = CheckState.Checked;
            else
                checkAuto = CheckState.Unchecked;
            if (period.Equals("0"))
            {
                timeInterval = "上午";
            }
            else if (period.Equals("1"))
            {
                timeInterval = "下午";
            }
            else if (period.Equals("2"))
            {
                timeInterval = "晚上";
            }
            else if (period.Equals("3"))
            {
                timeInterval = "全天";
            }

            tlpMorning.ColumnCount = 7;
            tlpMorning.RowCount = row;

            tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));

            for (int n = 0; n < row; n++)
            {
                tlpMorning.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            }
            tlpMorning.Size = new System.Drawing.Size(342, row * 30);
            //标题栏
            //Panel panel = new Panel();
            //panel.Dock = DockStyle.Fill;
            //tlpMorning.Controls.Add(panel);
            Label label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Font = new Font("微软雅黑", 10);
            label.Text = "开始";
            tlpMorning.Controls.Add(label, 1, 0);
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Font = new Font("微软雅黑", 10);
            label.Text = "结束";
            tlpMorning.Controls.Add(label, 2, 0);
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Font = new Font("微软雅黑", 10);
            label.Text = "现场";
            tlpMorning.Controls.Add(label, 3, 0);
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Font = new Font("微软雅黑", 10);
            label.Text = "公开";
            tlpMorning.Controls.Add(label, 4, 0);
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Font = new Font("微软雅黑", 10);
            label.Text = "诊间";
            tlpMorning.Controls.Add(label, 5, 0);
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Font = new Font("微软雅黑", 10);
            label.Text = "应急";
            tlpMorning.Controls.Add(label, 6, 0);

            bool teEnabled = true;//当行数小于3的时候，空白文本框需要设为不可选
            String start = "";
            String end = "";
            String scene = "";
            String open = "";
            String room = "";
            String emergency = "";

            for (int r = 1; r < row; r++)
            {
                if (r > rowNum)
                {
                    start = "";
                    end = "";
                    scene = "";
                    open = "";
                    room = "";
                    emergency = "";
                    teEnabled = false;
                }
                else
                {
                    start = wdwpList[r - 1].beginTime;
                    end = wdwpList[r - 1].endTime;
                    scene = wdwpList[r - 1].numSource;
                    open = wdwpList[r - 1].numOpen;
                    room = wdwpList[r - 1].numClinic;
                    emergency = wdwpList[r - 1].numYj;
                    teEnabled = true;
                }
                for (int c = 0; c < 7; c++)
                {
                    if (r == 1 && c == 0)
                    {
                        //第一行第一列
                        CheckBox checkBox = new CheckBox();
                        checkBox.Dock = DockStyle.Fill;
                        checkBox.Font = new Font("微软雅黑", 10);
                        checkBox.Text = timeInterval;
                        checkBox.CheckState = checkState;
                        tlpMorning.Controls.Add(checkBox, 0, 1);
                    }
                    //else if (r == 2 && c == 0)
                    //{
                    //    //第二行第一列
                    //    //需跨1行
                    //    CheckBox checkBox = new CheckBox();
                    //    checkBox.Dock = DockStyle.Fill;
                    //    checkBox.Font = new Font("微软雅黑", 10);
                    //    checkBox.ForeColor = Color.FromArgb(255, 153, 102);
                    //    checkBox.Text = "自动排班";
                    //    checkBox.CheckState = checkAuto;
                    //    tlpMorning.SetRowSpan(checkBox, 2);
                    //    tlpMorning.Controls.Add(checkBox, c, r);
                    //}
                    else if (c == 0)
                    {
                        //不做处理
                    }
                    else
                    {
                        if (c == 1)
                        {
                            //第二列
                            TextEdit textEdit = new TextEdit();
                            textEdit.Properties.AutoHeight = false;
                            textEdit.Dock = DockStyle.Fill;
                            textEdit.Font = new Font("微软雅黑", 10);
                            textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            textEdit.Text = start;
                            textEdit.Enabled = teEnabled;
                            tlpMorning.Controls.Add(textEdit, c, r);
                        }
                        else if (c == 2)
                        {
                            //第三列
                            TextEdit textEdit = new TextEdit();
                            textEdit.Properties.AutoHeight = false;
                            textEdit.Dock = DockStyle.Fill;
                            textEdit.Font = new Font("微软雅黑", 10);
                            textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            textEdit.Text = end;
                            textEdit.Enabled = teEnabled;
                            tlpMorning.Controls.Add(textEdit, c, r);
                        }
                        else if (c == 3)
                        {
                            //第四列 现场
                            TextEdit textEdit = new TextEdit();
                            textEdit.Properties.AutoHeight = false;
                            textEdit.Dock = DockStyle.Fill;
                            textEdit.Font = new Font("微软雅黑", 10);
                            textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            textEdit.Text = scene;
                            textEdit.Enabled = teEnabled;
                            tlpMorning.Controls.Add(textEdit, c, r);
                        }
                        else if (c == 4)
                        {
                            //第五列 公开
                            TextEdit textEdit = new TextEdit();
                            textEdit.Properties.AutoHeight = false;
                            textEdit.Dock = DockStyle.Fill;
                            textEdit.Font = new Font("微软雅黑", 10);
                            textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            textEdit.Text = open;
                            textEdit.Enabled = teEnabled;
                            tlpMorning.Controls.Add(textEdit, c, r);
                        }
                        else if (c == 5)
                        {
                            //第六列 诊间
                            TextEdit textEdit = new TextEdit();
                            textEdit.Properties.AutoHeight = false;
                            textEdit.Dock = DockStyle.Fill;
                            textEdit.Font = new Font("微软雅黑", 10);
                            textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            textEdit.Text = room;
                            textEdit.Enabled = teEnabled;
                            tlpMorning.Controls.Add(textEdit, c, r);
                        }
                        else if (c == 6)
                        {
                            //第七列 应急
                            TextEdit textEdit = new TextEdit();
                            textEdit.Properties.AutoHeight = false;
                            textEdit.Dock = DockStyle.Fill;
                            textEdit.Font = new Font("微软雅黑", 10);
                            textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            textEdit.Text = emergency;
                            textEdit.Enabled = teEnabled;
                            tlpMorning.Controls.Add(textEdit, c, r);
                        }
                    }
                }
            }
            tlpMorning.Location = new System.Drawing.Point(0, 0);
            groupBox.Controls.Add(tlpMorning);
            panelPb.Controls.Add(groupBox);
            panel9.Controls.Add(panelPb);
            #endregion
        }

        /// <summary>
        /// 获取周几上午||下午||晚上的排班数据
        /// </summary>
        /// <param name="workingDayList">排班数据</param>
        /// <param name="period">0：上午，1：下午，2：晚上</param>
        /// <returns></returns>
        private List<WorkingDayEntity> getWorkingDayData(List<WorkingDayEntity> workingDayList, String period)
        {
            List<WorkingDayEntity> workingDayByWeekList = new List<WorkingDayEntity>();
            foreach (WorkingDayEntity workingDay in workingDayList)
            {
                if (workingDay.period.Equals(period))
                    workingDayByWeekList.Add(workingDay);
            }
            return workingDayByWeekList;
        }

        private void mcDoctor_MenuItemClick(object sender, EventArgs e)
        {
            panel8.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (workDate == null)
            {
                MessageBoxUtils.Hint("请先查询后进行排班再保存", HintMessageBoxIcon.Error);
            }
            if (lueIsUse.Text == null || lueIsUse.Text.ToString().Length == 0)
            {
                dataController1.ShowError(lueIsUse, "请选择状态");
                return;
            }

            List<WorkingDayEntity> workingDayList = new List<WorkingDayEntity>();
            //获取排班信息
            int days = panel9.Controls.Count; //排班天数
            for (int i = 0; i < days; i++)
            {
                Panel panel = (Panel)panel9.Controls[i];
                GroupBox groupBoy = (GroupBox)panel.Controls[0];
                TableLayoutPanel tlp = (TableLayoutPanel)groupBoy.Controls[0];
                if (tlp.Enabled)
                {
                    CheckBox cbIsUse = (CheckBox)tlp.GetControlFromPosition(0, 1);
                    //CheckBox cbAuto = (CheckBox)tlp.GetControlFromPosition(0, 2);
                    String period = "";
                    if (cbIsUse.Text.Equals("上午")) period = "0";
                    if (cbIsUse.Text.Equals("下午")) period = "1";
                    if (cbIsUse.Text.Equals("晚上")) period = "2";
                    if (cbIsUse.Text.Equals("全天")) period = "3";
                    //DateTime wsBeginTime = new DateTime();
                    for (int r = 1; r < tlp.RowCount; r++)//行
                    {
                        WorkingDayEntity wordingDay = new WorkingDayEntity();
                        wordingDay.period = period;
                        for (int c = 1; c < tlp.ColumnCount; c++)//列
                        {
                            TextEdit te = (TextEdit)tlp.GetControlFromPosition(c, r);
                            if (c == 1)
                                wordingDay.beginTime = te.Text;
                            else if (c == 2)
                                wordingDay.endTime = te.Text;
                            else if (c == 3)
                                wordingDay.numSite = te.Text;
                            else if (c == 4)
                                wordingDay.numOpen = te.Text;
                            else if (c == 5)
                                wordingDay.numClinic = te.Text;
                            else if (c == 6)
                                wordingDay.numYj = te.Text;
                            //if (period.Equals("3") && r == 1 && c == 1)
                            //{
                            //    wsBeginTime = DateTime.Parse("2008-08-08 " + wordingDay.beginTime + ":00");
                            //}
                            //if (period.Equals("3"))
                            //{
                            //    DateTime dt2 = DateTime.Parse("2008-08-08 " + wordingDay.beginTime + ":00");
                            //    TimeSpan dt3 = dt2.Subtract(wsBeginTime);
                            //    int minute = dt3.Hours * 60 + dt3.Minutes;
                            //    if (minute <= 0)
                            //    {

                            //    }
                            //}
                        }
                        workingDayList.Add(wordingDay);
                    }
                }
            }
            String scheduSets = Newtonsoft.Json.JsonConvert.SerializeObject(workingDayList);

            String param = "deptId=" + mcDept.itemName + "&doctorId=" + mcDoctor.itemName
                + "&hospitalId=" + mcDept.itemTag + "&workDate=" + workDate
                + "&status=" + lueIsUse.EditValue + "&remarks=" + teRemarks.Text
                + "&scheduSets=" + scheduSets;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/saveToOne?";
            String data = HttpClass.httpPost(url, param);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                MessageBoxUtils.Hint("保存成功!");
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }
    }
}
