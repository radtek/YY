using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtScreen.Models
{
    public class HelperClass
    {
        /// <summary>
        /// 医院主键
        /// </summary>
        public static String hospitalId
        {
            get
            {
                return string.Join(",", from w in list where w.code == AppContext.AppConfig.deptCode select w.hospitalId);
            }
        }
        /// <summary>
        /// 科室主键
        /// </summary>
        public static String deptId
        { 
            get
            {
                return string.Join(",", from d in list where d.code == AppContext.AppConfig.deptCode select d.id);
            }
        }
        /// <summary>
        /// 科室列表集合
        /// </summary>
        public static List<HelperClassDoctor> list { get; set; }
    }
    /// <summary>
    /// 科室列表
    /// </summary>
    public class HelperClassDoctor
    {
        public String id { get; set; }
        public String parentId { get; set; }
        public String name { get; set; }
        public String hospitalId { get; set; }
        public String code { get; set; }
    }
}
