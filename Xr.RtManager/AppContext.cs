using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

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

        public static void Load()
        {
            //AppContext.Client = new ClientInfo();
            AppContext.Session = new SessionInfo();
            Session.cookie = new CookieContainer();
            //AppContext.CommandLineArgs = new AppCommandLineArgs();
            //AppContext.Directories = new AppDirectories();
            //AppContext.Configuration = new ConfigurationManager();
            //AppContext.Configuration.Load();

            //AppContext.Parameter = new SysParameter();
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
