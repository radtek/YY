using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtCall.Model
{
    /// <summary>
    /// 患者列表
    /// </summary>
    public class PatientList
    {
        public int id { get; set; }
        public int name { get; set; }
        public int sex { get; set; }
        public int Cardno { get; set; }
        public int time { get; set; }
        public int times { get; set; }
    }
    public class CardType
    {
        public string value { get; set; }
        public string label { get; set; }
    }
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
