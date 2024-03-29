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

namespace Xr.RtManager.Pages.scheduling
{
    public partial class BatchSchedulingForm : UserControl
    {
        private Form MainForm; //主窗体
        Xr.Common.Controls.OpaqueCommand cmd;

        public BatchSchedulingForm()
        {
            InitializeComponent();
        }

        private void BatchSchedulingForm_Load(object sender, EventArgs e)
        {
            MainForm = (Form)this.Parent;
            cmd = new Xr.Common.Controls.OpaqueCommand(AppContext.Session.waitControl);
            cmd.ShowOpaqueLayer(0f);
            //设置科室列表
            //String param = "hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
            //String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?" + param;
            //this.DoWorkAsync( 0, (o) => 
            //{
            //    String data = HttpClass.httpPost(url);
            //    return data;

            //}, null, (data) => 
            //{
            //    JObject objT = JObject.Parse(data.ToString());
            //    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            //    {
                    //List<DeptEntity> deptList = objT["result"].ToObject<List<DeptEntity>>();
                    List<DeptEntity> deptList = AppContext.Session.deptList;
                    treeMenuControl1.KeyFieldName = "id";
                    treeMenuControl1.ParentFieldName = "parentId";
                    treeMenuControl1.DisplayMember = "name";
                    treeMenuControl1.ValueMember = "id";
                    treeMenuControl1.DataSource = deptList;

                    //查询日期下拉框数据
                    String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findWeeks";
                    this.DoWorkAsync( 0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                    {
                        String data = HttpClass.httpPost(url);
                        return data;

                    }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                    {
                        JObject objT = JObject.Parse(data2.ToString());
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            List<DictEntity> dictList = new List<DictEntity>();
                            for (int i = 0; i < objT["result"].Count(); i++)
                            {
                                DictEntity dict = new DictEntity();
                                dict.value = i + "";
                                dict.label = objT["result"][i].ToString();
                                dictList.Add(dict);
                            }
                            lueDate.Properties.DataSource = dictList;
                            lueDate.Properties.DisplayMember = "label";
                            lueDate.Properties.ValueMember = "value";
                            lueDate.EditValue = "0";

                            DateTime dt = DateTime.Parse(lueDate.Text + " 00:00:00");
                            monday.Caption = dt.ToString("u").Substring(5, 5) + "(一)";
                            dt = dt.AddDays(1);
                            tuesday.Caption = dt.ToString("u").Substring(5, 5) + "(二)";
                            dt = dt.AddDays(1);
                            wednesday.Caption = dt.ToString("u").Substring(5, 5) + "(三)";
                            dt = dt.AddDays(1);
                            thursday.Caption = dt.ToString("u").Substring(5, 5) + "(四)";
                            dt = dt.AddDays(1);
                            friday.Caption = dt.ToString("u").Substring(5, 5) + "(五)";
                            dt = dt.AddDays(1);
                            saturday.Caption = dt.ToString("u").Substring(5, 5) + "(六)";
                            dt = dt.AddDays(1);
                            sunday.Caption = dt.ToString("u").Substring(5, 5) + "(日)";
                            cmd.HideOpaqueLayer();
                        }
                        else
                        {
                            cmd.HideOpaqueLayer();
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                            return;
                        }
                    });
            //    }
            //    else
            //    {
            //        cmd.HideOpaqueLayer();
            //        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
            //        return;
            //    }
            //});
        }

        private void btnTswk_Click(object sender, EventArgs e)
        {
            int i = int.Parse(lueDate.EditValue.ToString());
            List<DictEntity> dictList = lueDate.Properties.DataSource as List<DictEntity>;
            if (i > 0 && i <= dictList.Count()-1)
            {
                i--;
                lueDate.EditValue = i.ToString();
            }
        }

        private void btnNxvWk_Click(object sender, EventArgs e)
        {
            int i = int.Parse(lueDate.EditValue.ToString());
            List<DictEntity> dictList = lueDate.Properties.DataSource as List<DictEntity>;
            if (i >= 0 && i < dictList.Count() - 1)
            {
                i++;
                lueDate.EditValue = i.ToString();
            }
        }

        private void lueDate_EditValueChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void treeMenuControl1_MenuItemClick(object sender, EventArgs e, object selectItem)
        {
            SearchData();
        }

        public void SearchData()
        {
            if (treeMenuControl1.EditValue != null && lueDate.Text.Length > 0)
            {
                //检查是否是节假日
                List<String> holidayList = new List<String>();
                DateTime dtEnd = DateTime.Parse(lueDate.Text + " 00:00:00").AddDays(6);
                String param = "hospitalId=" + AppContext.Session.hospitalId + "&beginDate=" + lueDate.Text + "&endDate=" + dtEnd.ToString("u").Substring(0, 10);
                String url = AppContext.AppConfig.serverUrl + "cms/holiday/checkHolidayForDate?" + param;
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
                        holidayList = objT["result"].ToObject<List<String>>();

                        //修改表格背景色
                        DateTime dt = DateTime.Parse(lueDate.Text + " 00:00:00");
                        monday.Caption = dt.ToString("u").Substring(5, 5) + "(一)";
                        if (ifListContainStr(holidayList, dt.ToString("u").Substring(0, 10)))
                        {
                            monday.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            mondayMorning.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            mondayAfternoon.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            mondayNight.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            mondayAllDay.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                        }
                        else
                        {
                            monday.AppearanceHeader.BackColor = SystemColors.Control;
                            mondayMorning.AppearanceHeader.BackColor = SystemColors.Control;
                            mondayAfternoon.AppearanceHeader.BackColor = SystemColors.Control;
                            mondayNight.AppearanceHeader.BackColor = SystemColors.Control;
                            mondayAllDay.AppearanceHeader.BackColor = SystemColors.Control;
                        }
                        dt = dt.AddDays(1);
                        tuesday.Caption = dt.ToString("u").Substring(5, 5) + "(二)";
                        if (ifListContainStr(holidayList, dt.ToString("u").Substring(0, 10)))
                        {
                            tuesday.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            tuesdayMorning.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            tuesdayAfternoon.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            tuesdayNight.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            tuesdayAllDay.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                        }
                        else
                        {
                            tuesday.AppearanceHeader.BackColor = SystemColors.Control;
                            tuesdayMorning.AppearanceHeader.BackColor = SystemColors.Control;
                            tuesdayAfternoon.AppearanceHeader.BackColor = SystemColors.Control;
                            tuesdayNight.AppearanceHeader.BackColor = SystemColors.Control;
                            tuesdayAllDay.AppearanceHeader.BackColor = SystemColors.Control;
                        }
                        dt = dt.AddDays(1);
                        wednesday.Caption = dt.ToString("u").Substring(5, 5) + "(三)";
                        if (ifListContainStr(holidayList, dt.ToString("u").Substring(0, 10)))
                        {
                            wednesday.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            wednesdayMorning.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            wednesdayAfternoon.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            wednesdayNight.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            wednesdayAllDay.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                        }
                        else
                        {
                            wednesday.AppearanceHeader.BackColor = SystemColors.Control;
                            wednesdayMorning.AppearanceHeader.BackColor = SystemColors.Control;
                            wednesdayAfternoon.AppearanceHeader.BackColor = SystemColors.Control;
                            wednesdayNight.AppearanceHeader.BackColor = SystemColors.Control;
                            wednesdayAllDay.AppearanceHeader.BackColor = SystemColors.Control;
                        }
                        dt = dt.AddDays(1);
                        thursday.Caption = dt.ToString("u").Substring(5, 5) + "(四)";
                        if (ifListContainStr(holidayList, dt.ToString("u").Substring(0, 10)))
                        {
                            thursday.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            thursdayMorning.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            thursdayAfternoon.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            thursdayNight.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            thursdayAllDay.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                        }
                        else
                        {
                            thursday.AppearanceHeader.BackColor = SystemColors.Control;
                            thursdayMorning.AppearanceHeader.BackColor = SystemColors.Control;
                            thursdayAfternoon.AppearanceHeader.BackColor = SystemColors.Control;
                            thursdayNight.AppearanceHeader.BackColor = SystemColors.Control;
                            thursdayAllDay.AppearanceHeader.BackColor = SystemColors.Control;
                        }
                        dt = dt.AddDays(1);
                        friday.Caption = dt.ToString("u").Substring(5, 5) + "(五)";
                        if (ifListContainStr(holidayList, dt.ToString("u").Substring(0, 10)))
                        {
                            friday.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            fridayMorning.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            fridayAfternoon.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            fridayNight.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            fridayAllDay.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                        }
                        else
                        {
                            friday.AppearanceHeader.BackColor = SystemColors.Control;
                            fridayMorning.AppearanceHeader.BackColor = SystemColors.Control;
                            fridayAfternoon.AppearanceHeader.BackColor = SystemColors.Control;
                            fridayNight.AppearanceHeader.BackColor = SystemColors.Control;
                            fridayAllDay.AppearanceHeader.BackColor = SystemColors.Control;
                        }
                        dt = dt.AddDays(1);
                        saturday.Caption = dt.ToString("u").Substring(5, 5) + "(六)";
                        if (ifListContainStr(holidayList, dt.ToString("u").Substring(0, 10)))
                        {
                            saturday.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            saturdayMorning.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            saturdayAfternoon.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            saturdayNight.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            saturdayAllDay.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                        }
                        else
                        {
                            saturday.AppearanceHeader.BackColor = SystemColors.Control;
                            saturdayMorning.AppearanceHeader.BackColor = SystemColors.Control;
                            saturdayAfternoon.AppearanceHeader.BackColor = SystemColors.Control;
                            saturdayNight.AppearanceHeader.BackColor = SystemColors.Control;
                            saturdayAllDay.AppearanceHeader.BackColor = SystemColors.Control;
                        }
                        dt = dt.AddDays(1);
                        sunday.Caption = dt.ToString("u").Substring(5, 5) + "(日)";
                        if (ifListContainStr(holidayList, dt.ToString("u").Substring(0, 10)))
                        {
                            sunday.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            sundayMorning.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            sundayAfternoon.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            sundayNight.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                            sundayAllDay.AppearanceHeader.BackColor = Color.FromArgb(255, 204, 255);
                        }
                        else
                        {
                            sunday.AppearanceHeader.BackColor = SystemColors.Control;
                            sundayMorning.AppearanceHeader.BackColor = SystemColors.Control;
                            sundayAfternoon.AppearanceHeader.BackColor = SystemColors.Control;
                            sundayNight.AppearanceHeader.BackColor = SystemColors.Control;
                            sundayAllDay.AppearanceHeader.BackColor = SystemColors.Control;
                        }

                        List<DoctorVSEntity> DVSList = new List<DoctorVSEntity>();//表格数据
                        List<SchedulingEntity> pbList = null;//后台返回的排班数据
                        //获取排班数据
                        param = "hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + treeMenuControl1.EditValue + "&date=" + lueDate.Text;
                        url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findByDeptAndDate?" + param;
                        this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                        {
                            data = HttpClass.httpPost(url);
                            return data;

                        }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                        {
                            objT = JObject.Parse(data2.ToString());
                            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                            {
                                pbList = objT["result"].ToObject<List<SchedulingEntity>>();

                                //获取医生列表
                                param = "pageNo=1&pageSize=10000&hospital.id=" + AppContext.Session.hospitalId + "&dept.id=" + treeMenuControl1.EditValue;
                                url = AppContext.AppConfig.serverUrl + "cms/doctor/list?" + param;
                                this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                                {
                                    data = HttpClass.httpPost(url);
                                    return data;

                                }, null, (data3) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                                {
                                    objT = JObject.Parse(data3.ToString());
                                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                    {
                                        List<DoctorInfoEntity> doctorList = objT["result"]["list"].ToObject<List<DoctorInfoEntity>>();
                                        if (doctorList.Count == 0) DVSList.Clear();
                                        foreach (DoctorInfoEntity doctor in doctorList)
                                        {
                                            DoctorVSEntity DVS = new DoctorVSEntity();
                                            DVS.doctorId = doctor.id;
                                            DVS.doctorName = doctor.name;
                                            dt = DateTime.Parse(lueDate.Text + " 00:00:00");
                                            for (int i = 0; i < 7; i++)
                                            {
                                                String date = dt.ToString("u").Substring(0, 10);
                                                for (int j = 0; j < 4; j++)
                                                {
                                                    SchedulingEntity scheduling = getSchedulingData(pbList, date, doctor.id, j.ToString());
                                                    String value = "口";
                                                    if (scheduling != null) value = "√";
                                                    if (i == 0 && j == 0) DVS.mondayMorning = value;
                                                    else if (i == 0 && j == 1) DVS.mondayAfternoon = value;
                                                    else if (i == 0 && j == 2) DVS.mondayNight = value;
                                                    else if (i == 0 && j == 3) DVS.mondayAllAay = value;
                                                    else if (i == 1 && j == 0) DVS.tuesdayMorning = value;
                                                    else if (i == 1 && j == 1) DVS.tuesdayAfternoon = value;
                                                    else if (i == 1 && j == 2) DVS.tuesdayNight = value;
                                                    else if (i == 1 && j == 3) DVS.tuesdayAllAay = value;
                                                    else if (i == 2 && j == 0) DVS.wednesdayMorning = value;
                                                    else if (i == 2 && j == 1) DVS.wednesdayAfternoon = value;
                                                    else if (i == 2 && j == 2) DVS.wednesdayNight = value;
                                                    else if (i == 2 && j == 3) DVS.wednesdayAllAay = value;
                                                    else if (i == 3 && j == 0) DVS.thursdayMorning = value;
                                                    else if (i == 3 && j == 1) DVS.thursdayAfternoon = value;
                                                    else if (i == 3 && j == 2) DVS.thursdayNight = value;
                                                    else if (i == 3 && j == 3) DVS.thursdayAllAay = value;
                                                    else if (i == 4 && j == 0) DVS.fridayMorning = value;
                                                    else if (i == 4 && j == 1) DVS.fridayAfternoon = value;
                                                    else if (i == 4 && j == 2) DVS.fridayNight = value;
                                                    else if (i == 4 && j == 3) DVS.fridayAllAay = value;
                                                    else if (i == 5 && j == 0) DVS.saturdayMorning = value;
                                                    else if (i == 5 && j == 1) DVS.saturdayAfternoon = value;
                                                    else if (i == 5 && j == 2) DVS.saturdayNight = value;
                                                    else if (i == 5 && j == 3) DVS.saturdayAllAay = value;
                                                    else if (i == 6 && j == 0) DVS.sundayMorning = value;
                                                    else if (i == 6 && j == 1) DVS.sundayAfternoon = value;
                                                    else if (i == 6 && j == 2) DVS.sundayNight = value;
                                                    else if (i == 6 && j == 3) DVS.sundayAllAay = value;
                                                }
                                                dt = dt.AddDays(1);
                                            }
                                            DVSList.Add(DVS);
                                        }
                                        gcDoctor.DataSource = DVSList;
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
                            else
                            {
                                cmd.HideOpaqueLayer();
                                MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                                return;
                            }
                        });
                    }
                    else
                    {
                        cmd.HideOpaqueLayer();
                        MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                        return;
                    }
                });
            }
        }

        /// <summary>
        /// 判断List<String>是否含有某个字符串
        /// </summary>
        /// <param name="holidayList"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private bool ifListContainStr(List<String> holidayList, String dt)
        {
            foreach (String holiday in holidayList)
            {
                if(holiday.Equals(dt))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据医生id、日期、时段查询是否与该排班
        /// </summary>
        /// <param name="schedulingList"></param>
        /// <param name="date"></param>
        /// <param name="doctorId"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        private SchedulingEntity getSchedulingData(List<SchedulingEntity> schedulingList, String date, 
            String doctorId, String period)
        {
            foreach (SchedulingEntity scheduling in schedulingList)
            {
                if (scheduling.doctorId.Equals(doctorId) && scheduling.date.Equals(date) 
                    && scheduling.period.Equals(period))
                {
                    return scheduling;
                }
            }
            return null;
        }

        private void bandedGridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue == null) return;
            DateTime dt = DateTime.ParseExact(lueDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            String zj = e.Column.Caption.Substring(0,2);//周几
            if (zj.Equals("名称"))
            {
                return;
            }
            else if (zj.Equals("周一"))
            {
            }
            else if (zj.Equals("周二"))
            {
                dt = dt.AddDays(1);
            }
            else if (zj.Equals("周三"))
            {
                dt = dt.AddDays(2);
            }
            else if (zj.Equals("周四"))
            {
                dt = dt.AddDays(3);
            }
            else if (zj.Equals("周五"))
            {
                dt = dt.AddDays(4);
            }
            else if (zj.Equals("周六"))
            {
                dt = dt.AddDays(5);
            }
            else if (zj.Equals("周日"))
            {
                dt = dt.AddDays(6);
            }
            if (DateTime.Compare(dt, DateTime.Today) < 0) //判断日期大小
            {
                MessageBoxUtils.Hint("过去的日期不能排班", HintMessageBoxIcon.Error, MainForm);
                return;
            }

            String ts = "";
            if(e.Column.Caption.Equals("周一上午")||e.Column.Caption.Equals("周一下午")||e.Column.Caption.Equals("周一晚上")){
                string strName = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "mondayAllAay");
                if(strName.Equals("√")){
                    ts = "选中了全天就不能选择上午、下午、晚上";
                }
            }else if(e.Column.Caption.Equals("周二上午")||e.Column.Caption.Equals("周二下午")||e.Column.Caption.Equals("周二晚上")){
                string strName = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "tuesdayAllAay");
                if(strName.Equals("√")){
                    ts = "选中了全天就不能选择上午、下午、晚上";
                }
            }else if(e.Column.Caption.Equals("周三上午")||e.Column.Caption.Equals("周三下午")||e.Column.Caption.Equals("周三晚上")){
                string strName = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "wednesdayAllAay");
                if(strName.Equals("√")){
                    ts = "选中了全天就不能选择上午、下午、晚上";
                }
            }else if(e.Column.Caption.Equals("周四上午")||e.Column.Caption.Equals("周四下午")||e.Column.Caption.Equals("周四晚上")){
                string strName = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "thursdayAllAay");
                if(strName.Equals("√")){
                    ts = "选中了全天就不能选择上午、下午、晚上";
                }
            }else if(e.Column.Caption.Equals("周五上午")||e.Column.Caption.Equals("周五下午")||e.Column.Caption.Equals("周五晚上")){
                string strName = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "fridayAllAay");
                if(strName.Equals("√")){
                    ts = "选中了全天就不能选择上午、下午、晚上";
                }
            }else if(e.Column.Caption.Equals("周六上午")||e.Column.Caption.Equals("周六下午")||e.Column.Caption.Equals("周六晚上")){
                string strName = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "saturdayAllAay");
                if(strName.Equals("√")){
                    ts = "选中了全天就不能选择上午、下午、晚上";
                }
            }else if(e.Column.Caption.Equals("周日上午")||e.Column.Caption.Equals("周日下午")||e.Column.Caption.Equals("周日晚上")){
                string strName = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "sundayAllAay");
                if(strName.Equals("√")){
                    ts = "选中了全天就不能选择上午、下午、晚上";
                }
            }else if(e.Column.Caption.Equals("周一全天")){
                string morning = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "mondayMorning");
                string afternoon = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "mondayAfternoon");
                string night = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "mondayNight");
                if(morning.Equals("√")||afternoon.Equals("√")||afternoon.Equals("√")){
                    ts = "选中了上午、下午、晚上就不能选择全天";
                }
            }else if(e.Column.Caption.Equals("周二全天")){
                string morning = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "tuesdayMorning");
                string afternoon = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "tuesdayAfternoon");
                string night = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "tuesdayNight");
                if(morning.Equals("√")||afternoon.Equals("√")||afternoon.Equals("√")){
                    ts = "选中了上午、下午、晚上就不能选择全天";
                }
            }else if(e.Column.Caption.Equals("周三全天")){
                string morning = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "wednesdayMorning");
                string afternoon = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "wednesdayAfternoon");
                string night = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "wednesdayNigh");
                if(morning.Equals("√")||afternoon.Equals("√")||afternoon.Equals("√")){
                    ts = "选中了上午、下午、晚上就不能选择全天";
                }
            }else if(e.Column.Caption.Equals("周四全天")){
                string morning = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "thursdayMorning");
                string afternoon = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "thursdayAfternoon");
                string night = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "thursdayNight");
                if(morning.Equals("√")||afternoon.Equals("√")||afternoon.Equals("√")){
                    ts = "选中了上午、下午、晚上就不能选择全天";
                }
            }else if(e.Column.Caption.Equals("周五全天")){
                string morning = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "fridayMorning");
                string afternoon = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "fridayAfternoon");
                string night = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "fridayNight");
                if(morning.Equals("√")||afternoon.Equals("√")||afternoon.Equals("√")){
                    ts = "选中了上午、下午、晚上就不能选择全天";
                }
            }else if(e.Column.Caption.Equals("周六全天")){
                string morning = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "saturdayMorning");
                string afternoon = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "saturdayAfternoon ");
                string night = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "saturdayNight");
                if(morning.Equals("√")||afternoon.Equals("√")||afternoon.Equals("√")){
                    ts = "选中了上午、下午、晚上就不能选择全天";
                }
            }else if(e.Column.Caption.Equals("周日全天")){
                string morning = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "sundayMorning");
                string afternoon = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "sundayAfternoon");
                string night = bandedGridView1.GetRowCellDisplayText(e.RowHandle, "sundayNight");
                if(morning.Equals("√")||afternoon.Equals("√")||afternoon.Equals("√")){
                    ts = "选中了上午、下午、晚上就不能选择全天";
                }
            }
            if(ts.Length>0){
                MessageBoxUtils.Hint(ts, HintMessageBoxIcon.Error, MainForm);
                return;
            }
            
            if (e.CellValue.Equals("√"))
            {
                bandedGridView1.SetRowCellValue(e.RowHandle, e.Column.FieldName, "口");
            }
            else if (e.CellValue.Equals("口"))
            {
                bandedGridView1.SetRowCellValue(e.RowHandle, e.Column.FieldName, "√");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<SchedulingSubEntity> schedulingSubList = new List<SchedulingSubEntity>();
            List<DoctorVSEntity> DVSList = gcDoctor.DataSource as List<DoctorVSEntity>;
            if (DVSList == null || DVSList.Count == 0) return;

            #region 获取排班数据
            for (int i = 0; i < DVSList.Count; i++)
            {
                DoctorVSEntity doctor = DVSList[i];
                DateTime dt = DateTime.Parse(lueDate.Text + " 00:00:00");
                for (int c = 0; c < 28; c++ )
                {
                    SchedulingSubEntity schedulingSub = new SchedulingSubEntity();
                    schedulingSub.doctorId = doctor.doctorId;
                    if (c == 0)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "一";
                        schedulingSub.period = "0";
                        if (doctor.mondayMorning.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 1)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "一";
                        schedulingSub.period = "1";
                        if (doctor.mondayAfternoon.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 2)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "一";
                        schedulingSub.period = "2";
                        if (doctor.mondayNight.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 3)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "一";
                        schedulingSub.period = "3";
                        if (doctor.mondayAllAay.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 4)
                    {
                        dt = dt.AddDays(1);
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "二";
                        schedulingSub.period = "0";
                        if (doctor.tuesdayMorning.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 5)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "二";
                        schedulingSub.period = "1";
                        if (doctor.tuesdayAfternoon.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 6)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "二";
                        schedulingSub.period = "2";
                        if (doctor.tuesdayNight.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 7)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "二";
                        schedulingSub.period = "3";
                        if (doctor.tuesdayAllAay.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 8)
                    {
                        dt = dt.AddDays(1);
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "三";
                        schedulingSub.period = "0";
                        if (doctor.wednesdayMorning.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 9)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "三";
                        schedulingSub.period = "1";
                        if (doctor.wednesdayAfternoon.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 10)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "三";
                        schedulingSub.period = "2";
                        if (doctor.wednesdayNight.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 11)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "三";
                        schedulingSub.period = "3";
                        if (doctor.wednesdayAllAay.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 12)
                    {
                        dt = dt.AddDays(1);
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "四";
                        schedulingSub.period = "0";
                        if (doctor.thursdayMorning.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 13)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "四";
                        schedulingSub.period = "1";
                        if (doctor.thursdayAfternoon.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 14)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "四";
                        schedulingSub.period = "2";
                        if (doctor.thursdayNight.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 15)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "四";
                        schedulingSub.period = "3";
                        if (doctor.thursdayAllAay.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 16)
                    {
                        dt = dt.AddDays(1);
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "五";
                        schedulingSub.period = "0";
                        if (doctor.fridayMorning.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 17)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "五";
                        schedulingSub.period = "1";
                        if (doctor.fridayAfternoon.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 18)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "五";
                        schedulingSub.period = "2";
                        if (doctor.fridayNight.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 19)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "五";
                        schedulingSub.period = "3";
                        if (doctor.fridayAllAay.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 20)
                    {
                        dt = dt.AddDays(1);
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "六";
                        schedulingSub.period = "0";
                        if (doctor.saturdayMorning.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 21)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "六";
                        schedulingSub.period = "1";
                        if (doctor.saturdayAfternoon.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 22)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "六";
                        schedulingSub.period = "2";
                        if (doctor.saturdayNight.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 23)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "六";
                        schedulingSub.period = "3";
                        if (doctor.saturdayAllAay.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 24)
                    {
                        dt = dt.AddDays(1);
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "日";
                        schedulingSub.period = "0";
                        if (doctor.sundayMorning.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 25)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "日";
                        schedulingSub.period = "1";
                        if (doctor.sundayAfternoon.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 26)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "日";
                        schedulingSub.period = "2";
                        if (doctor.sundayNight.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    else if (c == 27)
                    {
                        schedulingSub.workDate = dt.ToString("u").Substring(0, 10);
                        schedulingSub.week = "日";
                        schedulingSub.period = "3";
                        if (doctor.sundayAllAay.Equals("√")) schedulingSub.isPlan = true;
                        else schedulingSub.isPlan = false;
                    }
                    schedulingSubList.Add(schedulingSub);
                }
            }
            #endregion
            String scheduSets = Newtonsoft.Json.JsonConvert.SerializeObject(schedulingSubList);
            String param = "hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + treeMenuControl1.EditValue + "&scheduSets=" + scheduSets;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/saveToMany?";

            cmd.ShowOpaqueLayer(0.56f, "正在提交数据中");
            this.DoWorkAsync( 500 ,(o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url, param, 10);
                return data;

            }, null, (r) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(r.ToString());
                cmd.HideOpaqueLayer();
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("保存成功!", MainForm);
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                }
            });

            //String data = HttpClass.httpPost(url, param, 10);
            //JObject objT = JObject.Parse(data);
            //if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            //{
            //    MessageBoxUtils.Hint("保存成功!");
            //}
            //else
            //{
            //    MessageBox.Show(objT["message"].ToString());
            //}
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

        private void BatchSchedulingForm_Resize(object sender, EventArgs e)
        {
            cmd.rectDisplay = this.DisplayRectangle;
            if (gcDoctor.Width < 1080)
            {
                mondayMorning.Caption = "上";
                mondayAfternoon.Caption = "下";
                mondayNight.Caption = "晚";
                mondayAllDay.Caption = "全";

                tuesdayMorning.Caption = "上";
                tuesdayAfternoon.Caption = "下";
                tuesdayNight.Caption = "晚";
                tuesdayAllDay.Caption = "全";

                wednesdayMorning.Caption = "上";
                wednesdayAfternoon.Caption = "下";
                wednesdayNight.Caption = "晚";
                wednesdayAllDay.Caption = "全";

                thursdayMorning.Caption = "上";
                thursdayAfternoon.Caption = "下";
                thursdayNight.Caption = "晚";
                thursdayAllDay.Caption = "全";

                fridayMorning.Caption = "上";
                fridayAfternoon.Caption = "下";
                fridayNight.Caption = "晚";
                fridayAllDay.Caption = "全";

                saturdayMorning.Caption = "上";
                saturdayAfternoon.Caption = "下";
                saturdayNight.Caption = "晚";
                saturdayAllDay.Caption = "全";

                sundayMorning.Caption = "上";
                sundayAfternoon.Caption = "下";
                sundayNight.Caption = "晚";
                sundayAllDay.Caption = "全";
            }
            else
            {
                mondayMorning.Caption = "上午";
                mondayAfternoon.Caption = "下午";
                mondayNight.Caption = "晚上";
                mondayAllDay.Caption = "全天";

                tuesdayMorning.Caption = "上午";
                tuesdayAfternoon.Caption = "下午";
                tuesdayNight.Caption = "晚上";
                tuesdayAllDay.Caption = "全天";

                wednesdayMorning.Caption = "上午";
                wednesdayAfternoon.Caption = "下午";
                wednesdayNight.Caption = "晚上";
                wednesdayAllDay.Caption = "全天";

                thursdayMorning.Caption = "上午";
                thursdayAfternoon.Caption = "下午";
                thursdayNight.Caption = "晚上";
                thursdayAllDay.Caption = "全天";

                fridayMorning.Caption = "上午";
                fridayAfternoon.Caption = "下午";
                fridayNight.Caption = "晚上";
                fridayAllDay.Caption = "全天";

                saturdayMorning.Caption = "上午";
                saturdayAfternoon.Caption = "下午";
                saturdayNight.Caption = "晚上";
                saturdayAllDay.Caption = "全天";

                sundayMorning.Caption = "上午";
                sundayAfternoon.Caption = "下午";
                sundayNight.Caption = "晚上";
                sundayAllDay.Caption = "全天";
            }
        }
    }
}
