﻿using System;
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

namespace Xr.RtManager.Pages.cms
{
    public partial class VisitingTimeSettingsForm : UserControl
    {
        public VisitingTimeSettingsForm()
        {
            InitializeComponent();
        }

        public DoctorInfoEntity doctorInfo { get; set; }
        public DefaultVisitEntity defaultVisit { get; set; }
        /// <summary>
        /// 出诊信息模板
        /// </summary>
        public DefaultVisitEntity defaultVisitTemplate { get; set; }

        /// <summary>
        /// 选中医生列表
        /// </summary>
        private List<DoctorVSEntity> selectDoctorList { get; set; }

        private String hospitalId { get; set; }
        private String deptId { get; set; }
        private String deptName { get; set; }

        private void DeptSettingsForm_Load(object sender, EventArgs e)
        {
            dcDoctorInfo.DataType = typeof(DoctorInfoEntity);
            dcDefaultVisit.DataType = typeof(DefaultVisitEntity);
            //清除排班数据
            dcDefaultVisit.ClearValue();
            panScheduling.Controls.Clear();

            //设置科室列表
            List<Item> itemList = new List<Item>();
            foreach (DeptEntity dept in AppContext.Session.deptList)
            {
                Item item = new Item();
                item.name = dept.name;
                item.value = dept.id;
                item.tag = dept.hospitalId;
                itemList.Add(item);
            }
            menuControl2.setDataSource(itemList);

            //获取默认出诊时间字典配置
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/findDoctorVisitingDict";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                defaultVisitTemplate = objT["result"].ToObject<DefaultVisitEntity>();
                setDefaultVisit();
                buttonControl2_Click(null, null);
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        /// <summary>
        /// 设置默认的出诊时间
        /// </summary>
        private void setDefaultVisit()
        {
            defaultVisit = new DefaultVisitEntity();
            String[] hyArr = defaultVisitTemplate.defaultSourceNumber.Split(new char[] { '-' });

            defaultVisit.mStart = defaultVisitTemplate.defaultVisitTimeAm.Substring(0, 5);
            defaultVisit.mEnd = defaultVisitTemplate.defaultVisitTimeAm.Substring(6, 5);
            defaultVisit.mSubsection = defaultVisitTemplate.segmentalDuration;
            defaultVisit.mScene = hyArr[0];
            defaultVisit.mOpen = hyArr[1];
            defaultVisit.mRoom = hyArr[2];
            defaultVisit.mEmergency = hyArr[3];

            defaultVisit.aStart = defaultVisitTemplate.defaultVisitTimePm.Substring(0, 5);
            defaultVisit.aEnd = defaultVisitTemplate.defaultVisitTimePm.Substring(6, 5);
            defaultVisit.aSubsection = defaultVisitTemplate.segmentalDuration;
            defaultVisit.aScene = hyArr[0];
            defaultVisit.aOpen = hyArr[1];
            defaultVisit.aRoom = hyArr[2];
            defaultVisit.aEmergency = hyArr[3];

            defaultVisit.nStart = defaultVisitTemplate.defaultVisitTimeNight.Substring(0, 5);
            defaultVisit.nEnd = defaultVisitTemplate.defaultVisitTimeNight.Substring(6, 5);
            defaultVisit.nSubsection = defaultVisitTemplate.segmentalDuration;
            defaultVisit.nScene = hyArr[0];
            defaultVisit.nOpen = hyArr[1];
            defaultVisit.nRoom = hyArr[2];
            defaultVisit.nEmergency = hyArr[3];

            defaultVisit.allStart = defaultVisitTemplate.defaultVisitTimeAllDay.Substring(0, 5);
            defaultVisit.allEnd = defaultVisitTemplate.defaultVisitTimeAllDay.Substring(6, 5);
            defaultVisit.allSubsection = defaultVisitTemplate.segmentalDuration;
            defaultVisit.allScene = hyArr[0];
            defaultVisit.allOpen = hyArr[1];
            defaultVisit.allRoom = hyArr[2];
            defaultVisit.allEmergency = hyArr[3];

            dcDefaultVisit.SetValue(defaultVisit);
        }

        /// <summary>
        /// 更新按钮事件，生成周一到周日的排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl2_Click(object sender, EventArgs e)
        {
            //数据验证
            //if (!dcDefaultVisit.Validate())
            //{
            //    return;
            //}
            //清除排班数据
            panScheduling.Controls.Clear();
            CheckState morning = cbMorning.CheckState;
            CheckState afternoon = cbAfternoon.CheckState;
            CheckState night = cbNight.CheckState;
            CheckState allDay = cbAllDay.CheckState;
            if (morning != CheckState.Checked && afternoon != CheckState.Checked
                && night != CheckState.Checked && allDay != CheckState.Checked)
            {
                return;
            }
            //获取默认排班数据
            defaultVisit = new DefaultVisitEntity();
            dcDefaultVisit.GetValue(defaultVisit);

            //分段数量
            int rowMorningNum = 0;
            int rowAfternoonNum = 0;
            int rowNightNum = 0;
            int rowAllDayNum = 0;

            #region 计算分段数量
            //计算早上的分段数量
            if (morning == CheckState.Checked)
            {
                if (defaultVisit.mStart.Trim().Length == 0 || defaultVisit.mEnd.Trim().Length == 0
                    || defaultVisit.mSubsection.Trim().Length == 0)
                {
                    MessageBoxUtils.Hint("上午的设置不能为空");
                    return;
                }
                String[] startArr = defaultVisit.mStart.Split(new char[] { ':', '：' });
                String[] endArr = defaultVisit.mEnd.Split(new char[] { ':', '：' });
                if (startArr.Length != 2)
                {
                    MessageBoxUtils.Hint("上午的开始时间设置有误");
                    return;
                }
                if (endArr.Length != 2)
                {
                    MessageBoxUtils.Hint("上午的结束时间设置有误");
                    return;
                }
                DateTime d1 = new DateTime(2004, 1, 1, int.Parse(startArr[0]), int.Parse(startArr[1]), 00);
                DateTime d2 = new DateTime(2004, 1, 1, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                TimeSpan d3 = d2.Subtract(d1);
                int minute = d3.Hours * 60 + d3.Minutes;
                if (minute <= 0)
                {
                    MessageBoxUtils.Hint("上午结束时间不能小于或等于开始时间");
                    return;
                }
                if (minute < int.Parse(defaultVisit.mSubsection))
                {
                    MessageBoxUtils.Hint("上午分段时间大于总时间");
                    return;
                }
                rowMorningNum = minute / int.Parse(defaultVisit.mSubsection);
            }

            //计算下午的分段数量
            if (afternoon == CheckState.Checked)
            {
                if (defaultVisit.aStart.Trim().Length == 0 || defaultVisit.aEnd.Trim().Length == 0
                    || defaultVisit.aSubsection.Trim().Length == 0)
                {
                    MessageBoxUtils.Hint("下午的设置不能为空");
                    return;
                }
                String[] startArr = defaultVisit.aStart.Split(new char[] { ':', '：' });
                String[] endArr = defaultVisit.aEnd.Split(new char[] { ':', '：' });
                if (startArr.Length != 2)
                {
                    MessageBoxUtils.Hint("下午的开始时间设置有误");
                    return;
                }
                if (endArr.Length != 2)
                {
                    MessageBoxUtils.Hint("下午的结束时间设置有误");
                    return;
                }
                DateTime d1 = new DateTime(2004, 1, 1, int.Parse(startArr[0]), int.Parse(startArr[1]), 00);
                DateTime d2 = new DateTime(2004, 1, 1, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                TimeSpan d3 = d2.Subtract(d1);
                int minute = d3.Hours * 60 + d3.Minutes;
                if (minute <= 0)
                {
                    MessageBoxUtils.Hint("下午结束时间不能小于或等于开始时间");
                    return;
                }
                if (minute < int.Parse(defaultVisit.aSubsection))
                {
                    MessageBoxUtils.Hint("下午分段时间大于总时间");
                    return;
                }
                rowAfternoonNum = minute / int.Parse(defaultVisit.aSubsection);
            }

            //计算晚上的分段数量
            if (night == CheckState.Checked)
            {
                if (defaultVisit.nStart.Trim().Length == 0 || defaultVisit.nEnd.Trim().Length == 0
                    || defaultVisit.nSubsection.Trim().Length == 0)
                {
                    MessageBoxUtils.Hint("晚上的设置不能为空");
                    return;
                }
                String[] startArr = defaultVisit.nStart.Split(new char[] { ':', '：' });
                String[] endArr = defaultVisit.nEnd.Split(new char[] { ':', '：' });
                if (startArr.Length != 2)
                {
                    MessageBoxUtils.Hint("晚上的开始时间设置有误");
                    return;
                }
                if (endArr.Length != 2)
                {
                    MessageBoxUtils.Hint("晚上的结束时间设置有误");
                    return;
                }
                DateTime d1 = new DateTime(2004, 1, 1, int.Parse(startArr[0]), int.Parse(startArr[1]), 00);
                DateTime d2 = new DateTime();
                if (endArr[0].Equals("24"))
                    d2 = new DateTime(2004, 1, 2, 00, int.Parse(endArr[1]), 00);
                if (int.Parse(endArr[0]) < int.Parse(startArr[0]))
                    d2 = new DateTime(2004, 1, 2, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                else
                    d2 = new DateTime(2004, 1, 1, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                TimeSpan d3 = d2.Subtract(d1);
                int minute = d3.Hours * 60 + d3.Minutes;
                if (minute <= 0)
                {
                    MessageBoxUtils.Hint("晚上结束时间不能小于或等于开始时间");
                    return;
                }
                if (minute < int.Parse(defaultVisit.nSubsection))
                {
                    MessageBoxUtils.Hint("晚上分段时间大于总时间");
                    return;
                }
                rowNightNum = minute / int.Parse(defaultVisit.nSubsection);
            }

            //计算全天的分段数量
            if (allDay == CheckState.Checked)
            {
                if (defaultVisit.allStart.Trim().Length == 0 || defaultVisit.allEnd.Trim().Length == 0
                    || defaultVisit.allSubsection.Trim().Length == 0)
                {
                    MessageBoxUtils.Hint("全天的设置不能为空");
                    return;
                }
                String[] startArr = defaultVisit.allStart.Split(new char[] { ':', '：' });
                String[] endArr = defaultVisit.allEnd.Split(new char[] { ':', '：' });
                if (startArr.Length != 2)
                {
                    MessageBoxUtils.Hint("全天的开始时间设置有误");
                    return;
                }
                if (endArr.Length != 2)
                {
                    MessageBoxUtils.Hint("全天的结束时间设置有误");
                    return;
                }
                DateTime d1 = new DateTime(2004, 1, 1, int.Parse(startArr[0]), int.Parse(startArr[1]), 00);
                DateTime d2 = new DateTime();
                if (endArr[0].Equals("24"))
                    d2 = new DateTime(2004, 1, 2, 00, int.Parse(endArr[1]), 00);
                else
                    d2 = new DateTime(2004, 1, 1, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                TimeSpan d3 = d2.Subtract(d1);
                int minute = d3.Hours * 60 + d3.Minutes;
                if (minute <= 0)
                {
                    MessageBoxUtils.Hint("全天结束时间不能小于或等于开始时间");
                    return;
                }
                if (minute < int.Parse(defaultVisit.nSubsection))
                {
                    MessageBoxUtils.Hint("全天分段时间大于总时间");
                    return;
                }
                rowAllDayNum = minute / int.Parse(defaultVisit.allSubsection);
            }
            #endregion

            int gbY = 0; //groupBox的Y轴位置

            #region 生成周一到周日的排班
            //生成周一到周日的排班
            for (int i = 1; i <= 7; i++)
            {
                if (i != 1) gbY += 40;
                int blpY = 20; //TableLayoutPanel的y轴位置
                GroupBox groupBox = new GroupBox();
                groupBox.Width = 415;
                groupBox.Location = new System.Drawing.Point(0, gbY);
                groupBox.AutoSize = true;
                switch (i)
                {
                    case 1:
                        groupBox.Text = "周一";
                        break;
                    case 2:
                        groupBox.Text = "周二";
                        break;
                    case 3:
                        groupBox.Text = "周三";
                        break;
                    case 4:
                        groupBox.Text = "周四";
                        break;
                    case 5:
                        groupBox.Text = "周五";
                        break;
                    case 6:
                        groupBox.Text = "周六";
                        break;
                    case 7:
                        groupBox.Text = "周日";
                        break;
                    default: break;
                }

                for (int j = 0; j < 4; j++)
                {
                    //j=0:上午 j=1:下午 j=2:晚上 j=3:全天
                    TableLayoutPanel tlpMorning = new TableLayoutPanel();
                    int row = 0;//行数(包括标题)
                    DateTime dt1 = new DateTime();//开始时间
                    DateTime dt2 = new DateTime();//结束时间
                    String timeInterval = ""; //
                    if (j == 0)
                    {
                        if (rowMorningNum > 3) row = rowMorningNum + 1;
                        else row = 4;
                        dt1 = DateTime.Parse("2008-08-08 " + defaultVisit.mStart + ":00");
                        dt2 = dt1.AddMinutes(int.Parse(defaultVisit.mSubsection));
                        timeInterval = "上午";
                        if (morning != CheckState.Checked)
                        {
                            tlpMorning.Enabled = false;
                        }
                    }
                    else if (j == 1)
                    {
                        if (rowAfternoonNum > 3) row = rowAfternoonNum + 1;
                        else row = 4;
                        dt1 = DateTime.Parse("2008-08-08 " + defaultVisit.aStart + ":00");
                        dt2 = dt1.AddMinutes(int.Parse(defaultVisit.aSubsection));
                        timeInterval = "下午";
                        if (afternoon != CheckState.Checked)
                        {
                            tlpMorning.Enabled = false;
                        }
                    }
                    else if (j == 2)
                    {
                        if (rowNightNum > 3) row = rowNightNum + 1;
                        else row = 4;
                        dt1 = DateTime.Parse("2008-08-08 " + defaultVisit.nStart + ":00");
                        dt2 = dt1.AddMinutes(int.Parse(defaultVisit.nSubsection));
                        timeInterval = "晚上";
                        if (night != CheckState.Checked)
                        {
                            tlpMorning.Enabled = false;
                        }
                    }
                    else if (j == 3)
                    {
                        if (rowAllDayNum > 3) row = rowAllDayNum + 1;
                        else row = 4;
                        dt1 = DateTime.Parse("2008-08-08 " + defaultVisit.allStart + ":00");
                        dt2 = dt1.AddMinutes(int.Parse(defaultVisit.allSubsection));
                        timeInterval = "全天";
                        if (allDay != CheckState.Checked)
                        {
                            tlpMorning.Enabled = false;
                        }
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
                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Fill;
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
                    CheckState checkState = CheckState.Unchecked;
                    if (j == 0 && morning == CheckState.Checked)
                    {
                        scene = defaultVisit.mScene;
                        open = defaultVisit.mOpen;
                        room = defaultVisit.mRoom;
                        emergency = defaultVisit.mEmergency;
                        checkState = CheckState.Checked;
                    }
                    if (j == 1 && afternoon == CheckState.Checked)
                    {
                        scene = defaultVisit.aScene;
                        open = defaultVisit.aOpen;
                        room = defaultVisit.aRoom;
                        emergency = defaultVisit.aEmergency;
                        checkState = CheckState.Checked;
                    }
                    if (j == 2 && night == CheckState.Checked)
                    {
                        scene = defaultVisit.nScene;
                        open = defaultVisit.nOpen;
                        room = defaultVisit.nRoom;
                        emergency = defaultVisit.nEmergency;
                        checkState = CheckState.Checked;
                    }
                    if (j == 3 && allDay == CheckState.Checked)
                    {
                        scene = defaultVisit.allScene;
                        open = defaultVisit.allOpen;
                        room = defaultVisit.allRoom;
                        emergency = defaultVisit.allEmergency;
                        checkState = CheckState.Checked;
                    }
                    for (int r = 1; r < row; r++)
                    {
                        if (j == 0 && morning == CheckState.Checked)
                        {
                            start = dt1.ToString().Substring(11, 5);
                            end = dt2.ToString().Substring(11, 5);
                            dt1 = dt2;
                            dt2 = dt1.AddMinutes(int.Parse(defaultVisit.mSubsection));
                            if (r > rowMorningNum)
                            {
                                start = "";
                                end = "";
                                scene = "";
                                open = "";
                                room = "";
                                emergency = "";
                                teEnabled = false;
                            }
                        }
                        if (j == 1 && afternoon == CheckState.Checked)
                        {
                            start = dt1.ToString().Substring(11, 5);
                            end = dt2.ToString().Substring(11, 5);
                            dt1 = dt2;
                            dt2 = dt1.AddMinutes(int.Parse(defaultVisit.aSubsection));
                            if (r > rowAfternoonNum)
                            {
                                start = "";
                                end = "";
                                scene = "";
                                open = "";
                                room = "";
                                emergency = "";
                                teEnabled = false;
                            }
                        }
                        if (j == 2 && night == CheckState.Checked)
                        {
                            start = dt1.ToString().Substring(11, 5);
                            end = dt2.ToString().Substring(11, 5);
                            dt1 = dt2;
                            dt2 = dt1.AddMinutes(int.Parse(defaultVisit.nSubsection));
                            if (r > rowNightNum)
                            {
                                start = "";
                                end = "";
                                scene = "";
                                open = "";
                                room = "";
                                emergency = "";
                                teEnabled = false;
                            }
                        }
                        if (j == 3 && allDay == CheckState.Checked)
                        {
                            start = dt1.ToString().Substring(11, 5);
                            end = dt2.ToString().Substring(11, 5);
                            dt1 = dt2;
                            dt2 = dt1.AddMinutes(int.Parse(defaultVisit.allSubsection));
                            if (r > rowAllDayNum)
                            {
                                start = "";
                                end = "";
                                scene = "";
                                open = "";
                                room = "";
                                emergency = "";
                                teEnabled = false;
                            }
                        }
                        for (int c = 0; c < 7; c++)
                        {
                            if (r == 1 && c == 0)
                            {
                                //第一行第一列
                                panel = new Panel();
                                panel.Dock = DockStyle.Fill;
                                CheckBox checkBox = new CheckBox();
                                checkBox.Dock = DockStyle.Fill;
                                checkBox.Font = new Font("微软雅黑", 10);
                                checkBox.Text = timeInterval;
                                checkBox.CheckState = checkState;
                                checkBox.CheckStateChanged += new EventHandler(this.checkBox_CheckStateChanged);
                                panel.Controls.Add(checkBox);
                                tlpMorning.Controls.Add(panel, 0, 1);
                            }
                            else if (r == 2 && c == 0)
                            {
                                //第二行第一列
                                //需跨1行
                                panel = new Panel();
                                panel.Dock = DockStyle.Fill;
                                CheckBox checkBox = new CheckBox();
                                checkBox.Dock = DockStyle.Fill;
                                checkBox.Font = new Font("微软雅黑", 10);
                                checkBox.ForeColor = Color.FromArgb(255, 153, 102);
                                checkBox.Text = "自动排班";
                                panel.Controls.Add(checkBox);
                                tlpMorning.SetRowSpan(panel, 2);
                                tlpMorning.Controls.Add(panel, c, r);
                            }
                            else if (c == 0)
                            {
                                //不做处理
                            }
                            else
                            {
                                if (c == 1)
                                {
                                    //第二列
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = start;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 2)
                                {
                                    //第三列
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = end;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 3)
                                {
                                    //第四列 现场
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = scene;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 4)
                                {
                                    //第五列 公开
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = open;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 5)
                                {
                                    //第六列 诊间
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = room;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 6)
                                {
                                    //第七列 应急
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = emergency;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                            }
                        }
                    }
                    tlpMorning.Location = new System.Drawing.Point(0, blpY);
                    blpY += tlpMorning.Height;
                    gbY += tlpMorning.Height;
                    groupBox.Controls.Add(tlpMorning);
                }
                panScheduling.Controls.Add(groupBox);
            }
            #endregion
        }

        private void checkBox_CheckStateChanged(object sender, EventArgs e)
        {
            List<DoctorVSEntity> DVS = gcDoctor.DataSource as List<DoctorVSEntity>;
            if (DVS == null || DVS.Count == 0) return;
            CheckBox checkBox = (CheckBox)sender;
            Panel panel = (Panel)checkBox.Parent;
            TableLayoutPanel tlp = (TableLayoutPanel)panel.Parent;
            GroupBox groupBox = (GroupBox)tlp.Parent;



            foreach (DoctorVSEntity doctor in DVS)
            {
                if (groupBox.Text.Equals("周一"))
                {
                    if (checkBox.Text.Equals("上午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.mondayMorning = "√";
                        else
                            doctor.mondayMorning = "";
                    }
                    else if (checkBox.Text.Equals("下午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.mondayAfternoon = "√";
                        else
                            doctor.mondayAfternoon = "";
                    }
                    else if (checkBox.Text.Equals("晚上"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.mondayNight = "√";
                        else
                            doctor.mondayNight = "";
                    }
                    else if (checkBox.Text.Equals("全天"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.mondayAllAay = "√";
                        else
                            doctor.mondayAllAay = "";
                    }
                }
                else if (groupBox.Text.Equals("周二"))
                {
                    if (checkBox.Text.Equals("上午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.tuesdayMorning = "√";
                        else
                            doctor.tuesdayMorning = "";
                    }
                    else if (checkBox.Text.Equals("下午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.tuesdayAfternoon = "√";
                        else
                            doctor.tuesdayAfternoon = "";
                    }
                    else if (checkBox.Text.Equals("晚上"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.tuesdayNight = "√";
                        else
                            doctor.tuesdayNight = "";
                    }
                    else if (checkBox.Text.Equals("全天"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.tuesdayAllAay = "√";
                        else
                            doctor.tuesdayAllAay = "";
                    }
                }
                else if (groupBox.Text.Equals("周三"))
                {
                    if (checkBox.Text.Equals("上午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.wednesdayMorning = "√";
                        else
                            doctor.wednesdayMorning = "";
                    }
                    else if (checkBox.Text.Equals("下午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.wednesdayAfternoon = "√";
                        else
                            doctor.wednesdayAfternoon = "";
                    }
                    else if (checkBox.Text.Equals("晚上"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.wednesdayNight = "√";
                        else
                            doctor.wednesdayNight = "";
                    }
                    else if (checkBox.Text.Equals("全天"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.wednesdayAllAay = "√";
                        else
                            doctor.wednesdayAllAay = "";
                    }
                }
                else if (groupBox.Text.Equals("周四"))
                {
                    if (checkBox.Text.Equals("上午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.thursdayMorning = "√";
                        else
                            doctor.thursdayMorning = "";
                    }
                    else if (checkBox.Text.Equals("下午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.thursdayAfternoon = "√";
                        else
                            doctor.thursdayAfternoon = "";
                    }
                    else if (checkBox.Text.Equals("晚上"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.thursdayNight = "√";
                        else
                            doctor.thursdayNight = "";
                    }
                    else if (checkBox.Text.Equals("全天"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.thursdayAllAay = "√";
                        else
                            doctor.thursdayAllAay = "";
                    }
                }
                else if (groupBox.Text.Equals("周五"))
                {
                    if (checkBox.Text.Equals("上午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.fridayMorning = "√";
                        else
                            doctor.fridayMorning = "";
                    }
                    else if (checkBox.Text.Equals("下午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.fridayAfternoon = "√";
                        else
                            doctor.fridayAfternoon = "";
                    }
                    else if (checkBox.Text.Equals("晚上"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.fridayNight = "√";
                        else
                            doctor.fridayNight = "";
                    }
                    else if (checkBox.Text.Equals("全天"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.fridayAllAay = "√";
                        else
                            doctor.fridayAllAay = "";
                    }
                }
                else if (groupBox.Text.Equals("周六"))
                {
                    if (checkBox.Text.Equals("上午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.saturdayMorning = "√";
                        else
                            doctor.saturdayMorning = "";
                    }
                    else if (checkBox.Text.Equals("下午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.saturdayAfternoon = "√";
                        else
                            doctor.saturdayAfternoon = "";
                    }
                    else if (checkBox.Text.Equals("晚上"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.saturdayNight = "√";
                        else
                            doctor.saturdayNight = "";
                    }
                    else if (checkBox.Text.Equals("全天"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.saturdayAllAay = "√";
                        else
                            doctor.saturdayAllAay = "";
                    }
                }
                else if (groupBox.Text.Equals("周日"))
                {
                    if (checkBox.Text.Equals("上午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.sundayMorning = "√";
                        else
                            doctor.sundayMorning = "";
                    }
                    else if (checkBox.Text.Equals("下午"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.sundayAfternoon = "√";
                        else
                            doctor.sundayAfternoon = "";
                    }
                    else if (checkBox.Text.Equals("晚上"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.sundayNight = "√";
                        else
                            doctor.sundayNight = "";
                    }
                    else if (checkBox.Text.Equals("全天"))
                    {
                        if (checkBox.CheckState == CheckState.Checked)
                            doctor.sundayAllAay = "√";
                        else
                            doctor.sundayAllAay = "";
                    }
                }
            }
            gcDoctor.DataSource = DVS;
            gcDoctor.RefreshDataSource();
        }

        /// <summary>
        /// 科室列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuControl2_MenuItemClick(object sender, EventArgs e)
        {
            if (selectDoctorList!=null && selectDoctorList.Count > 0)
            {
                MessageBoxUtils.Hint("请先保存当前科室设置或者清空已选医生再切换科室!", HintMessageBoxIcon.Error);
                return;
            }
            Label label = (Label)sender;
            hospitalId = label.Tag.ToString();
            deptId = label.Name;
            deptName = label.Text;
            SearchData(1, 10000);
        }

        /// <summary>
        /// 感觉科室查询医生并根据排班生成数据
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        public void SearchData(int pageNo, int pageSize)
        {
            String param = "pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospital.id=" + hospitalId + "&dept.id=" + deptId;
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/list?"+param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DoctorInfoEntity> doctorList = objT["result"]["list"].ToObject<List<DoctorInfoEntity>>();
                List<DoctorVSEntity> doctorVSList = new List<DoctorVSEntity>();
                for (int i = 0; i < doctorList.Count(); i++)
                {
                    DoctorVSEntity doctor = new DoctorVSEntity();
                    doctor.deptId = deptId;
                    doctor.deptName = deptName;
                    doctor.doctorId = doctorList[i].id;
                    doctor.doctorName = doctorList[i].name;
                    if (panScheduling.Controls.Count == 0)
                    {
                        doctorVSList.Add(doctor);
                        break;
                    }
                    for (int j = 0; j < panScheduling.Controls.Count; j++)
                    {
                        for (int s = 0; s < 4; s++)
                        {
                            TableLayoutPanel tlp = (TableLayoutPanel)panScheduling.Controls[j].Controls[s];
                            Panel pCb = (Panel)tlp.GetControlFromPosition(0, 1);
                            CheckBox cbIsUse = (CheckBox)pCb.Controls[0];
                            if (cbIsUse.CheckState == CheckState.Checked)
                            {
                                if (j == 0 && s == 0) doctor.mondayMorning = "√";
                                else if (j == 0 && s == 1) doctor.mondayAfternoon = "√";
                                else if (j == 0 && s == 2) doctor.mondayNight = "√";
                                else if (j == 0 && s == 3) doctor.mondayAllAay = "√";
                                else if (j == 1 && s == 0) doctor.tuesdayMorning = "√";
                                else if (j == 1 && s == 1) doctor.tuesdayAfternoon = "√";
                                else if (j == 1 && s == 2) doctor.tuesdayNight = "√";
                                else if (j == 1 && s == 3) doctor.tuesdayAllAay = "√";
                                else if (j == 2 && s == 0) doctor.wednesdayMorning = "√";
                                else if (j == 2 && s == 1) doctor.wednesdayAfternoon = "√";
                                else if (j == 2 && s == 2) doctor.wednesdayNight = "√";
                                else if (j == 2 && s == 3) doctor.wednesdayAllAay = "√";
                                else if (j == 3 && s == 0) doctor.thursdayMorning = "√";
                                else if (j == 3 && s == 1) doctor.thursdayAfternoon = "√";
                                else if (j == 3 && s == 2) doctor.thursdayNight = "√";
                                else if (j == 3 && s == 3) doctor.thursdayAllAay = "√";
                                else if (j == 4 && s == 0) doctor.fridayMorning = "√";
                                else if (j == 4 && s == 1) doctor.fridayAfternoon = "√";
                                else if (j == 4 && s == 2) doctor.fridayNight = "√";
                                else if (j == 4 && s == 3) doctor.fridayAllAay = "√";
                                else if (j == 5 && s == 0) doctor.saturdayMorning = "√";
                                else if (j == 5 && s == 1) doctor.saturdayAfternoon = "√";
                                else if (j == 5 && s == 2) doctor.saturdayNight = "√";
                                else if (j == 5 && s == 3) doctor.saturdayAllAay = "√";
                                else if (j == 6 && s == 0) doctor.sundayMorning = "√";
                                else if (j == 6 && s == 1) doctor.sundayAfternoon = "√";
                                else if (j == 6 && s == 2) doctor.sundayNight = "√";
                                else if (j == 6 && s == 3) doctor.thursdayAllAay = "√";
                            }
                        }
                    }
                    doctorVSList.Add(doctor);
                }
                gcDoctor.DataSource = doctorVSList;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        /// <summary>
        /// 单元格点击事件  √：使用排班 空字符串：不使用排班 null：没有排班
        /// null不允许编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandedGridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue == null) return;
            if (e.CellValue.Equals("√"))
            {
                bandedGridView1.SetRowCellValue(e.RowHandle, e.Column.FieldName, "");
            }
            else if (e.CellValue.Equals(""))
            {
                bandedGridView1.SetRowCellValue(e.RowHandle, e.Column.FieldName, "√");
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedRow = bandedGridView1.GetFocusedRow() as DoctorVSEntity;
            if (selectedRow == null)
                return;
            selectDoctorList = gridControl1.DataSource as List<DoctorVSEntity>;
            if (selectDoctorList == null) selectDoctorList = new List<DoctorVSEntity>();
            for (int i = 0; i < selectDoctorList.Count; i++)
            {
                if (selectDoctorList[i].doctorId.Equals(selectedRow.doctorId))
                {
                    return;
                }
            }
            selectDoctorList.Add(selectedRow);
            gridControl1.DataSource = selectDoctorList;
            gridControl1.RefreshDataSource();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as DoctorVSEntity;
            if (selectedRow == null)
                return;
            selectDoctorList = gridControl1.DataSource as List<DoctorVSEntity>;
            foreach (DoctorVSEntity doctor in selectDoctorList)
            {
                if (doctor.doctorId.Equals(selectedRow.doctorId))
                {
                    selectDoctorList.Remove(doctor);
                    gridControl1.DataSource = selectDoctorList;
                    gridControl1.RefreshDataSource();
                    return;
                }
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            selectDoctorList.Clear();
            gridControl1.DataSource = selectDoctorList;
            gridControl1.RefreshDataSource();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectDoctorList == null || selectDoctorList.Count == 0)
                return;
            List<WorkingDayEntity> workingDayList = new List<WorkingDayEntity>();
            #region 获取排班数据
            int days = panScheduling.Controls.Count; //排班天数
            if (days == 0) return;
            foreach (DoctorVSEntity doctor in selectDoctorList)
            {
                for (int i = 0; i < days; i++){
                    String week = "";
                    if (i == 0)
                        week = "一";
                    else if (i == 1)
                        week = "二";
                    else if (i == 2)
                        week = "三";
                    else if (i == 3)
                        week = "四";
                    else if (i == 4)
                        week = "五";
                    else if (i == 5)
                        week = "六";
                    else if (i == 6)
                        week = "日";
                   GroupBox groupBoy = (GroupBox)panScheduling.Controls[i];//周一的面板
                   for (int period = 0; period < 4; period++)//循环上午、下午、晚上、全天
                   {
                       TableLayoutPanel tlp = (TableLayoutPanel)groupBoy.Controls[period];//排班
                       Panel pAuto = (Panel)tlp.GetControlFromPosition(0, 2);
                       CheckBox cbAuto = (CheckBox)pAuto.Controls[0];
                       for (int r = 1; r < tlp.RowCount; r++)//行
                       {
                           WorkingDayEntity wordingDay = new WorkingDayEntity();
                           wordingDay.deptId = doctor.deptId;
                           wordingDay.doctorId = doctor.doctorId;
                           wordingDay.week = week; //周几
                           wordingDay.period = period.ToString(); //0：上午，1：下午，2：晚上 3:晚上
                           if (i == 0 && period == 0)
                           {
                               if (doctor.mondayMorning != null)
                               {
                                   if (doctor.mondayMorning.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 0 && period == 1)
                           {
                               if (doctor.mondayAfternoon != null)
                               {
                                   if (doctor.mondayAfternoon.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 0 && period == 2)
                           {
                               if (doctor.mondayNight!=null)
                               {
                                   if (doctor.mondayNight.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 0 && period == 3)
                           {
                               if (doctor.saturdayAllAay != null)
                               {
                                   if (doctor.saturdayAllAay.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 1 && period == 0)
                           {
                               if (doctor.tuesdayMorning != null)
                               {
                                   if (doctor.tuesdayMorning.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 1 && period == 1)
                           {
                               if (doctor.tuesdayAfternoon != null)
                               {
                                   if (doctor.tuesdayAfternoon.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 1 && period == 2)
                           {
                               if (doctor.tuesdayNight != null)
                               {
                                   if (doctor.tuesdayNight.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 1 && period == 3)
                           {
                               if (doctor.tuesdayAllAay != null)
                               {
                                   if (doctor.tuesdayAllAay.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 2 && period == 0)
                           {
                               if (doctor.wednesdayMorning != null)
                               {
                                   if (doctor.wednesdayMorning.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 2 && period == 1)
                           {
                               if (doctor.wednesdayAfternoon != null)
                               {
                                   if (doctor.wednesdayAfternoon.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 2 && period == 2)
                           {
                               if (doctor.wednesdayNight != null)
                               {
                                   if (doctor.wednesdayNight.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 2 && period == 3)
                           {
                               if (doctor.wednesdayAllAay != null)
                               {
                                   if (doctor.wednesdayAllAay.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 3 && period == 0)
                           {
                               if (doctor.thursdayMorning != null)
                               {
                                   if (doctor.thursdayMorning.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 3 && period == 1)
                           {
                               if (doctor.thursdayAfternoon!=null)
                               {
                                   if (doctor.thursdayAfternoon.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 3 && period == 2)
                           {
                               if (doctor.sundayNight != null)
                               {
                                   if (doctor.sundayNight.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 3 && period == 3)
                           {
                               if (doctor.thursdayAllAay != null)
                               {
                                   if (doctor.thursdayAllAay.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 4 && period == 0)
                           {
                               if (doctor.fridayMorning != null)
                               {
                                   if (doctor.fridayMorning.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 4 && period == 1)
                           {
                               if (doctor.fridayAfternoon != null)
                               {
                                   if (doctor.fridayAfternoon.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 4 && period == 2)
                           {
                               if (doctor.fridayNight != null)
                               {
                                   if (doctor.fridayNight.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 4 && period == 3)
                           {
                               if (doctor.fridayAllAay != null)
                               {
                                   if (doctor.fridayAllAay.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 5 && period == 0)
                           {
                               if (doctor.saturdayMorning != null)
                               {
                                   if (doctor.saturdayMorning.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 5 && period == 1)
                           {
                               if (doctor.saturdayAfternoon != null)
                               {
                                   if (doctor.saturdayAfternoon.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 5 && period == 2)
                           {
                               if (doctor.saturdayNight != null)
                               {
                                   if (doctor.saturdayNight.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 5 && period == 3)
                           {
                               if (doctor.saturdayAllAay != null)
                               {
                                   if (doctor.saturdayAllAay.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 6 && period == 0)
                           {
                               if (doctor.sundayMorning != null)
                               {
                                   if (doctor.sundayMorning.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 6 && period == 1)
                           {
                               if (doctor.sundayAfternoon != null)
                               {
                                   if (doctor.sundayAfternoon.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 6 && period == 2)
                           {
                               if (doctor.sundayNight != null)
                               {
                                   if (doctor.sundayNight.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           }
                           if (i == 6 && period == 3)
                           {
                               if (doctor.sundayAllAay != null)
                               {
                                   if (doctor.sundayAllAay.Equals("√"))
                                       wordingDay.isUse = "0";
                                   else
                                       wordingDay.isUse = "1";
                               }
                               else continue;
                           } 
                           if (cbAuto.CheckState == CheckState.Checked)
                               wordingDay.autoSchedule = "0";
                           else
                               wordingDay.autoSchedule = "1";

                           for (int c = 1; c < tlp.ColumnCount; c++)//列
                           {
                               Panel panel = (Panel)tlp.GetControlFromPosition(c, r);
                               TextEdit te = (TextEdit)panel.Controls[0];
                               if (c == 1)
                                   wordingDay.beginTime = te.Text;
                               else if (c == 2)
                                   wordingDay.endTime = te.Text;
                               else if (c == 3)
                                   wordingDay.numSource = te.Text;
                               else if (c == 4)
                                   wordingDay.numOpen = te.Text;
                               else if (c == 5)
                                   wordingDay.numClinic = te.Text;
                               else if (c == 6)
                                   wordingDay.numYj = te.Text;
                           }
                           workingDayList.Add(wordingDay);
                       }
                   }
                }
            }
            #endregion
            String workStr = Newtonsoft.Json.JsonConvert.SerializeObject(workingDayList);

            String docotrIds = "";
            foreach (DoctorVSEntity doctor in selectDoctorList)
            {
                docotrIds += doctor.doctorId + ",";
            }
            docotrIds = docotrIds.Substring(0, docotrIds.Length - 1);
            String param = "docotrIds=" + docotrIds + "&doctorVisitSets=" + workStr + "&deptId=" + deptId;
            //请求接口
            String url = AppContext.AppConfig.serverUrl + "cms/doctorVisitingTime/save?";
            String data = HttpClass.httpPost(url, param);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                MessageBoxUtils.Hint("保存成功！");
                //清除已选医生
                selectDoctorList.Clear();
                gridControl1.DataSource = selectDoctorList;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void VisitingTimeSettingsForm_Paint(object sender, PaintEventArgs e)
        {
            gcDoctor.Height = panel5.Height - 50;
        }
    }
}