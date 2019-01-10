using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Configuration;

namespace Xr.RtManager
{
    /// <summary>
    /// 应用程序运行上下文信息
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        /// 获取应用程序会话信息
        /// </summary>
        public static SessionInfo Session { get; private set; }
        /// <summary>
        /// 本地配置信息
        /// </summary>
        public static AppConfigEntity AppConfig { get; private set; }

        public static void Load()
        {
            //AppContext.Client = new ClientInfo();
            Session = new SessionInfo();
            loadAppConfig();
            //AppContext.CommandLineArgs = new AppCommandLineArgs();
            //AppContext.Directories = new AppDirectories();
            //AppContext.Configuration = new ConfigurationManager();
            //AppContext.Configuration.Load();

            //AppContext.Parameter = new SysParameter();
        }

        /// <summary>
        /// 加载本地配置信息
        /// </summary>
        private static void loadAppConfig()
        {
            AppConfig = new AppConfigEntity();
            AppConfig.serverUrl = ConfigurationManager.AppSettings["serverUrl"].ToString();
            AppConfig.hospitalCode = ConfigurationManager.AppSettings["hospitalCode"].ToString();
            AppConfig.deptCode = ConfigurationManager.AppSettings["deptCode"].ToString();
        }

        /// <summary>
        /// 卸载应用程序上下文信息
        /// </summary>
        public static void Unload()
        {
            //AppContext.Configuration.Save();
        }
    }
}
