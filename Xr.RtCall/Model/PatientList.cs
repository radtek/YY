using System;
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
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public string triageTime { get; set; }
        /// <summary>
        /// 就诊时间
        /// </summary>
        public string visitTime { get; set; }
        /// <summary>
        /// 队列号
        /// </summary>
        public string queueNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 预约就诊时段，beginTime-endTime
        /// </summary>
        public string regVisitTime { get; set; }
        /// <summary>
        /// 预约途径
        /// </summary>
        public string registerWay { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string cradType { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string cradNo { get; set; }
        public string regTime { get; set; }
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
}
