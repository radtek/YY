﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xr.RtCall
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //处理未捕获的异常   
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常   
                Application.ThreadException += Application_ThreadException;
                //处理非UI线程异常   
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                var aProcessName = Process.GetCurrentProcess().ProcessName;
                if ((Process.GetProcessesByName(aProcessName)).GetUpperBound(0) > 0)
                {
                    MessageBox.Show(@"系统已经在运行中，如果要重新启动，请从进程中关闭...", @"系统警告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Xr.RtCall.Model.AppContext.Load();
                    Xr.Log4net.LogHelper.InitLog();
                    if (Xr.RtCall.Model.AppContext.AppConfig.Setting == "1")
                    {
                        Application.Run(new Form1());
                    }
                    else
                    {
                        Application.Run(new SettingFrm());
                    }
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("Main:" + ex);
                // MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")) + ",请重启程序。");
            }
        }
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            if (ex != null)
            {
                Log4net.LogHelper.Error("Application_ThreadException:" + ex);
            }

            MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                Log4net.LogHelper.Error("CurrentDomain_UnhandledException:" + ex);
            }

            MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")));
        }
    }
}
