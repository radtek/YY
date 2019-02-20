using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtCall.Model
{
    /// <summary>
    ///  可约日期实体
    /// </summary>
    public class AvaDateEntity
    {    /// <summary>
        /// 可约日期
        /// </summary>
        public String workDate { get; set; }

        /// <summary>
        ///  年
        /// </summary>
        public String year
        {
            get
            {
                return workDate.Substring(0, 4);
            }
        }
        /// <summary>
        /// 月 
        /// </summary>
        public String month
        {
            get
            {
                return workDate.Substring(5, 2);
            }
        }
        /// <summary>
        /// 日
        /// </summary>
        public String day
        {
            get
            {
                return workDate.Substring(8, 2);
            }
        }
    }
}
