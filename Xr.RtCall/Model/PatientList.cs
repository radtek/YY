﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtCall.Model
{
    /// <summary>
    /// 患者列表
    /// </summary>
    public class Patient 
    {
        /// <summary>
        /// 患者记录主键
        /// </summary>
        public string triageId { get; set; }
        /// <summary>
        /// 排队号
        /// </summary>
        public string queueNum { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string patientId { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }
        /// <summary>
        /// 时段
        /// </summary>
        public string workTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string triageTime { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string visitWinTime { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string statusTxt { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string cardType { get; set; }
        /// <summary>
        /// 卡号类型
        /// </summary>
        public string cardTypeTxt { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string telPhone { get;set;}
    }
    public class PatientList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string Cardno { get; set; }
        public string time { get; set; }
        public string times { get; set; }
    }
    /// <summary>
    /// 卡类型
    /// </summary>
    public class CardType
    {
        public string value { get; set; }
        public string label { get; set; }
    }
    /// <summary>
    /// 时间段
    /// </summary>
    public class TimeNum
    {
        public string id { get; set; }
        public string period { get; set; }
        public string periodName { get; set; }
        public string beginTime { get; set; }
        public string endTime { get; set; }
        public string  num { get; set; }
    }
    public class CallNext
    {
        public static String triageId { get; set; }
        public static String smallCellShow { get; set; }
    }
    public class DoctorScheduling
    {
        public String id { get; set; }
        public String name { get; set; }
    }
}
