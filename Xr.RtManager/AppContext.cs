using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Configuration;
using System.Text.RegularExpressions;

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
            //updateAppConfig();
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
        /// 修改配置文件
        /// </summary>
        private static void updateAppConfig()
        {
            //获取自动更新的配置文件，用于读取版本号
            string file = System.Windows.Forms.Application.StartupPath + "\\Xr.AutoUpdate.exe";
            WebConfigHelper config = new WebConfigHelper(file, ConfigType.ExeConfig);
            String version = config.GetValueByKey("version");
            string[] sArray = version.Split('.');
            //X.Y正式(1.0, 1.1)；X.YZ修改(1.0.1, 1.0.2)
            int X = int.Parse(sArray[0]);
            int Y = int.Parse(sArray[1]);
            int Z = 0;
            if (sArray.Length > 2)
                Z = int.Parse(sArray[2]);

            //获取本应用程序的配置文件并根据版本号进行修改
            file = System.Windows.Forms.Application.StartupPath + "\\Xr.RtManager.exe";
            WebConfigHelper appConfig = new WebConfigHelper(file, ConfigType.ExeConfig);
            //if (X >= 1 && Y >= 1 && Z >=1)
            //{
            //    //添加应用程序配置节点，如果已经存在此节点，则不做操作
            //    appConfig.AddAppSetting("test", "测试");
            //    //添加应用程序配置节点，如果已经存在此节点，则会修改该节点的值  
            //    appConfig.AddOrModifyAppSetting("test1", "测试");
            //    //修改应用程序配置节点，如果不存在此节点，则会添加此节点及对应的值  
            //    appConfig.ModifyAppSetting("test2", "测试");
            //    //保存所作的修改  
            //    appConfig.Save();
            //}
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
