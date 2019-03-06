﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtManager
{
    /// <summary>
    /// 已排班表
    /// </summary>
    public class SourceDataEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public String id { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public String beginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public String endTime { get; set; }

        /// <summary>
        /// 现场挂号数
        /// </summary>
        public String numSite { get; set; }

        /// <summary>
        /// 公开数量
        /// </summary>
        public String numOpen { get; set; }

        /// <summary>
        /// 诊间数量
        /// </summary>
        public String numClinic { get; set; }

        /// <summary>
        /// 应急数量
        /// </summary>
        public String numYj { get; set; }
    }
}
