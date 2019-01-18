using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtCall.Model
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
        public String hospitalCode { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public String deptCode { get; set; }

        /// <summary>
        /// 是否启动Socket
        /// </summary>
        public String StartUpSocket { get; set; }
    }
}
