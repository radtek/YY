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
       public String nextPatient { get; set; }
       public String name { get; set; }
       public String waitPatient { get; set; }
       public String signInNum { get; set; }
       public String bespeakNum { get; set; }
       public String visitPatient { get; set; }
    }
    public class SmallScreenClass
    {
        public String waitDesc { get; set; }
        public String nextPatient { get; set; }
        public String clinicName { get; set; }
        public String doctorName { get; set; }
        public String waitPatient { get; set; }
        public String signInNum { get; set; }
        public String visitPatient { get; set; }
    }
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
}
