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
        public string patientName { get; set; }
        public string triageTime { get; set; }
        public string visitTime { get; set; }
        public string queueNum { get; set; }
        public string status { get; set; }
        public string regVisitTime { get; set; }
        public string registerWay { get; set; }
        public string cradType { get; set; }
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
