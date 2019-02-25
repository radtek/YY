using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Xr.RtCall.Model
{
   public static class AppContext
    {
        /// <summary>
        /// 本地配置信息
        /// </summary>
        public static AppConfigEntity AppConfig { get; private set; }
        public static void Load()
        {
            loadAppConfig();
        }
        /// <summary>
        /// 加载本地配置信息
        /// </summary>
        private static void loadAppConfig()
        {
            AppConfig = new AppConfigEntity();
            AppConfig.serverUrl = ConfigurationManager.AppSettings["ServerUrl"].ToString();
            AppConfig.hospitalCode = ConfigurationManager.AppSettings["hospitalCode"].ToString();
            AppConfig.deptCode = ConfigurationManager.AppSettings["deptCode"].ToString();
            AppConfig.StartUpSocket = ConfigurationManager.AppSettings["StartUpSocket"].ToString();
            AppConfig.OutPutLocationX = ConfigurationManager.AppSettings["OutPutLocationX"].ToString();
            AppConfig.OutPutLocationY = ConfigurationManager.AppSettings["OutPutLocationY"].ToString();
            AppConfig.sleepOutPutTime = ConfigurationManager.AppSettings["sleepOutPutTime"].ToString();
            AppConfig.WhetherToAssign = ConfigurationManager.AppSettings["WhetherToAssign"].ToString();
            AppConfig.ClincCode = ConfigurationManager.AppSettings["ClincCode"].ToString();
        }
    }
}
