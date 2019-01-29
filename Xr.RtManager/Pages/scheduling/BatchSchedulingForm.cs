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

namespace Xr.RtManager.Pages.scheduling
{
    public partial class BatchSchedulingForm : UserControl
    {
        public BatchSchedulingForm()
        {
            InitializeComponent();
        }

        private void BatchSchedulingForm_Load(object sender, EventArgs e)
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

            //查询日期下拉框数据
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findWeeks";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DictEntity> dictList = new List<DictEntity>();
                for (int i = 0; i < objT["result"].Count(); i++)
                {
                    DictEntity dict = new DictEntity();
                    dict.value = i+"";
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
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        private void btnTswk_Click(object sender, EventArgs e)
        {
            lueDate.EditValue = "0";
        }

        private void btnNxvWk_Click(object sender, EventArgs e)
        {
            lueDate.EditValue = "1";
        }

        private void lueDate_EditValueChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void mcDept_MenuItemClick(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            if (mcDept.itemName != null && lueDate.Text.Length > 0)
            {
                List<String> holidayList = new List<String>();
                DateTime dtEnd = DateTime.Parse(lueDate.Text + " 00:00:00").AddDays(6);
                String param = "hospitalId=" + mcDept.itemTag + "&beginDate=" + lueDate.Text + "&endDate=" + dtEnd.ToString("u").Substring(0, 10);
                String url = AppContext.AppConfig.serverUrl + "cms/holiday/checkHolidayForDate?" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    holidayList = objT["result"].ToObject<List<String>>();
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }

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
                param = "hospitalId=" + mcDept.itemTag + "&deptId=" + mcDept.itemName + "&date=" + lueDate.Text;
                url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findByDeptAndDate?" + param;
                data = HttpClass.httpPost(url);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    pbList = objT["result"].ToObject<List<SchedulingEntity>>();
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }

                //获取医生列表
                param = "pageNo=1&pageSize=10000&hospital.id=" + mcDept.itemTag + "&dept.id=" + mcDept.itemName;
                url = AppContext.AppConfig.serverUrl + "cms/doctor/list?" + param;
                data = HttpClass.httpPost(url);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DoctorInfoEntity> doctorList = objT["result"]["list"].ToObject<List<DoctorInfoEntity>>();
                    if (doctorList.Count == 0) return;
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
                                else if(i == 0 && j == 1) DVS.mondayAfternoon = value;
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
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
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
            if (DVSList.Count == 0) return;

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
            String param = "hospitalId=" + mcDept.itemTag + "&deptId=" + mcDept.itemName + "&scheduSets=" + scheduSets;
            String url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/saveToMany?";
            String data = HttpClass.httpPost(url, param, 10);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                MessageBoxUtils.Hint("保存成功!");
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }
    }
}