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
using System.Threading;
using System.Text.RegularExpressions;

namespace Xr.RtManager.Pages.scheduling
{
    public partial class SingleSchedulingForm : UserControl
    {
        public SingleSchedulingForm()
        {
            InitializeComponent();
        }

        Xr.Common.Controls.OpaqueCommand cmd;
        /// <summary>
        /// 出诊信息模板
        /// </summary>
        public DefaultVisitEntity defaultVisitTemplate { get; set; }

        private void SingleSchedulingForm_Load(object sender, EventArgs e)
        {
            dateEdit1.Properties.MinValue = DateTime.Now;
            dateEdit1.Properties.MaxValue = DateTime.Now.AddDays(90);

            cmd = new Xr.Common.Controls.OpaqueCommand(AppContext.Session.waitControl);
            cmd.ShowOpaqueLayer(0f);
            //设置科室列表
            String param = "hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
            String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?" + param;
            this.DoWorkAsync(250, (o) => 
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => 
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DeptEntity> deptList = objT["result"].ToObject<List<DeptEntity>>();
                    List<Item> itemList = new List<Item>();
                    foreach (DeptEntity dept in deptList)
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
                    url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=is_use";
                    this.DoWorkAsync(250, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                    {
                        data = HttpClass.httpPost(url);
                        return data;

                    }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                    {
                        objT = JObject.Parse(data2.ToString());
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            List<DictEntity> dictList = objT["result"].ToObject<List<DictEntity>>();
                            lueIsUse.Properties.DataSource = dictList;
                            lueIsUse.Properties.DisplayMember = "label";
                            lueIsUse.Properties.ValueMember = "value";
                            lueIsUse.EditValue = dictList[0].value;
                            cmd.HideOpaqueLayer();
                        }
                        else
                        {
                            cmd.HideOpaqueLayer();
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                    });
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            });
        }

        private void menuControl2_MenuItemClick(object sender, EventArgs e)
        {
            Label label = null;
            if (typeof(Label).IsInstanceOfType(sender))
            {
                label = (Label)sender;
            }
            else
            {
                PanelEx panelEx = (PanelEx)sender;
                label = (Label)panelEx.Controls[0];
            }
            String hospitalId = label.Tag.ToString();
            String deptId = label.Name;
            String deptName = label.Text;

            String param = "pageNo=1&pageSize=10000&hospital.id=" + hospitalId + "&dept.id=" + deptId;
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/list?" + param;
            cmd.ShowOpaqueLayer();
            this.DoWorkAsync(500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
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
                    cmd.HideOpaqueLayer();
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            });
        }

        private String workDate;

        private void btnQuery_Click(object sender, EventArgs e)
        {
            setScheduling();
        }

        private void setScheduling()
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
            if (periodList.Count == 0)
            {
                dataController1.ShowError(cbAllAay, "至少选一个");
                return;
            }
            String param = null;
            String url = null;
            String data = null;
            JObject objT = null;
            cmd.ShowOpaqueLayer();
            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
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
                        if (string.Compare(objT["result"].ToString(), "true", true) != 0)
                        {
                            String sd = "";
                            if (periodList[i] == "0") sd = "上午";
                            else if (periodList[i] == "1") sd = "下午";
                            else if (periodList[i] == "2") sd = "晚午";
                            else if (periodList[i] == "3") sd = "全天";
                            //labMsg.Text = "该日期" + sd + "已有排班，请先在【排班列表中停诊或者删除】";
                            return "1|该日期" + sd + "已有排班，请先在【排班列表中停诊或者删除】";
                        }
                    }
                    else
                    {
                        return "2|" + objT["message"].ToString();
                    }
                }
                return "0|0";

            }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                string[] sArray = data2.ToString().Split('|');
                if (sArray[0] == "1")
                {
                    labMsg.Text = sArray[1];
                    cmd.HideOpaqueLayer();
                    return;
                }
                else if (sArray[0] == "2")
                {
                    labMsg.Text = "";
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(sArray[1], MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {
                    labMsg.Text = "";
                }
                List<List<WorkingDayEntity>> sList = new List<List<WorkingDayEntity>>();
                this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                {
                    //for (int i = periodList.Count - 1; i >= 0; i--)
                    for (int i = 0; i < periodList.Count; i++)
                    {
                        param = "deptId=" + mcDept.itemName + "&doctorId=" + mcDoctor.itemName
                            + "&workDate=" + workDate + "&period=" + periodList[i];
                        url = AppContext.AppConfig.serverUrl + "cms/doctorVisitingTime/findByPropertys?" + param;
                        data = HttpClass.httpPost(url);
                        objT = JObject.Parse(data);
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            List<WorkingDayEntity> workingDayList = objT["result"].ToObject<List<WorkingDayEntity>>();
                            sList.Add(workingDayList);
                            //setWorkingDay(workingDayList, periodList[i]);
                        }
                        else
                        {
                            //MessageBox.Show(objT["message"].ToString());
                            return "2|" + objT["message"].ToString();
                        }
                    }
                    return "0|0";

                }, null, (data3) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                {
                    string[] sArray2 = data3.ToString().Split('|');
                    if (sArray2[0] == "2")
                    {
                        cmd.HideOpaqueLayer();
                        MessageBoxUtils.Show(sArray[1], MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    for (int i = sList.Count()-1; i >= 0; i--)
                    {
                        setWorkingDay(sList[i], periodList[i]);
                    }
                    for (int i = 0; i < panel9.Controls.Count; i++)
                    {
                        Panel panel = (Panel)panel9.Controls[i];
                        GroupBox gb = (GroupBox)panel.Controls[0];
                        if (gb.Height < panel.Height) gb.Height = panel.Height;
                    }
                    cmd.HideOpaqueLayer();
                });
            });
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

            Label labelTips = new Label(); //没有默认出诊时间的提示内容
            //当前TableLayoutPanel的数量
            List<WorkingDayEntity> wdwpList = getWorkingDayData(workingDayList, period);
            //多少行数据
            int rowNum = wdwpList.Count;
            //period=0:上午 period=1:下午 period=2:晚上 period=3:全天
            TableLayoutPanel tlpMorning = new TableLayoutPanel();
            if (rowNum == 0) tlpMorning.Enabled = false;
            else labelTips.Visible = false;
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
                    scene = wdwpList[r - 1].numSite;
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
            
            labelTips.Location = new System.Drawing.Point(10, 10);
            labelTips.Text = "没有设置默认出诊时间的，请到医生设置里面设置";
            labelTips.ForeColor = Color.Red;
            labelTips.AutoSize = true;
            groupBox.Controls.Add(labelTips);
            tlpMorning.Location = new System.Drawing.Point(5, 20);
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
                TableLayoutPanel tlp = (TableLayoutPanel)groupBoy.Controls[1];
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
            cmd.ShowOpaqueLayer();
            String scheduSets = Newtonsoft.Json.JsonConvert.SerializeObject(workingDayList);

            String param = "deptId=" + mcDept.itemName + "&doctorId=" + mcDoctor.itemName
                + "&hospitalId=" + mcDept.itemTag + "&workDate=" + workDate
                + "&status=" + lueIsUse.EditValue + "&remarks=" + teRemarks.Text
                + "&scheduSets=" + scheduSets;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/saveToOne?";
            this.DoWorkAsync(500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url, param);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                cmd.HideOpaqueLayer();
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    cbMorning.CheckState = CheckState.Unchecked;
                    cbAfternoon.CheckState = CheckState.Unchecked;
                    cbNight.CheckState = CheckState.Unchecked;
                    cbAllAay.CheckState = CheckState.Unchecked;
                    MessageBoxUtils.Hint("保存成功!");
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            });
        }

        private void cbMorning_CheckStateChanged(object sender, EventArgs e)
        {
            setScheduling();
            //CheckBox cb = (CheckBox)sender;
            //if (cb.CheckState == CheckState.Checked)
            //{
            //    setScheduling();
            //}
        }

        private void cbAfternoon_CheckStateChanged(object sender, EventArgs e)
        {
            setScheduling();
            //CheckBox cb = (CheckBox)sender;
            //if (cb.CheckState == CheckState.Checked)
            //{
            //    setScheduling();
            //}
        }

        private void cbNight_CheckStateChanged(object sender, EventArgs e)
        {
            setScheduling();
            //CheckBox cb = (CheckBox)sender;
            //if (cb.CheckState == CheckState.Checked)
            //{
            //    setScheduling();
            //}
        }

        private void cbAllAay_CheckStateChanged(object sender, EventArgs e)
        {
            setScheduling();
            CheckBox cb = (CheckBox)sender;
            //if (cb.CheckState == CheckState.Checked)
            //{
            //    setScheduling();
            //}
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            setScheduling();
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
    }
}
