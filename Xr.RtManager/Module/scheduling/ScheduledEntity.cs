using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtManager
{
    /// <summary>
    /// 已排班表
    /// </summary>
    public class ScheduledEntity
    {
        /// <summary>
        /// 序号
        /// </summary>
        public String num { get; set; }

        /// <summary>
        /// 科室id
        /// </summary>
        public String deptId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public String deptName { get; set; }

        /// <summary>
        /// 医生id
        /// </summary>
        public String doctorId { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public String doctorName { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public String workDate { get; set; }

        /// <summary>
        /// 上午
        /// </summary>
        public String am { get; set; }

        /// <summary>
        /// 下午
        /// </summary>
        public String pm { get; set; }

        /// <summary>
        /// 晚上
        /// </summary>
        public String night { get; set; }

        /// <summary>
        /// 全天
        /// </summary>
        public String allday { get; set; }

        /// <summary>
        /// 预约数量
        /// </summary>
        public String registerNum { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public String status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String remarks { get; set; }
    }
}
