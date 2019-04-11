using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtScreen.Models
{
    /// <summary>
    /// 大屏帮助类
    /// </summary>
   public class ScreenClass
    {
       public String nextPatient { get; set; }//下一位患者
       public String name { get; set; }//诊室名称
       public String waitPatient { get; set; }//等候患者
      // public String signInNum { get; set; }
       public String bespeakNum { get; set; }//预约人数
       public String visitPatient { get; set; }//在诊患者
       public String isStop { get; set; }//诊室是否正常开诊
       public String waitNum { get; set; }//候诊人数
    }
    /// <summary>
    /// 科室小屏
    /// </summary>
    public class SmallScreenClass
    {
        public String waitNum { get; set; }//候诊人数
        public String nextPatient { get; set; }//下一位患者
        public String waitingDesc { get; set; }//候诊说明
        public String clinicName { get; set; }//诊室名称
        public String doctorName { get; set; }//医生姓名
        public String waitPatient { get; set; }//等候患者
        public String visitPatient { get; set; }//就诊患者
    }
    /// <summary>
    /// 医生小屏
    /// </summary>
    public class DoctorSmallScreenClass
    {
        public String doctorExcellence { get; set; }
        public String doctorIntro { get; set; }
        public String nextPatient { get; set; }
        public String clinicName { get; set; }
        public String doctorName { get; set; }
        public String waitPatient { get; set; }
        public String doctorSpecialty { get; set; }
        public String doctorJob { get; set; }
        public String visitPatient { get; set; }
        public String doctorHeader { get; set; }
    }

    public class StardIsFrom
    {
        public String hospitalId { get; set; }
        public String deptId { get; set; }
        public String clinicId { get; set; }
    }
}
