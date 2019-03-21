using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xr.Common.Validation;

namespace Xr.RtManager
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class AppConfigEntity
    {
        /// <summary>
        /// 服务器测试库ip+端口
        /// </summary>
        public String serverUrl { get; set; }

        /// <summary>
        /// 医院编码
        /// </summary>
        [Required]
        public String hospitalCode { get; set; }

        /// <summary>
        /// 小票打印机名字
        /// </summary>
        public String PrinterName { get; set; }

        /// <summary>
        /// 第一次启动 1：是 0：否
        /// </summary>
        public String firstStart { get; set; }
    }
}
