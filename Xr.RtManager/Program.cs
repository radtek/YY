using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Xr;

namespace Xr.RtManager
{
    static class Program
    {
        //static string RunFormFullName
        //{
        //    get
        //    {
        //        string setRunFormFullName = CIPACE.Sys.Configuration.RunFormFullName;
        //        if (setRunFormFullName == null)
        //            setRunFormFullName = DigiForm.SETRUNFORMFULLNAME;

        //        return setRunFormFullName;
        //    }
        //}
        /// <summary>
        ///   应用程序的主入口点。
        /// </summary>
        public static ApplicationContext context;


        [STAThread]
        private static void Main()
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
                    context = new ApplicationContext();
                    Application.Idle += Application_Idle; //注册程序运行空闲去执行主程序窗体相应初始化代码
                    Application.Run(context);

                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    ////Application.Run(new MainForm());
                    //LoginForm login = new LoginForm();
                    //login.ShowDialog();
                    //if (login.DialogResult == DialogResult.OK)
                    //{
                    //    Application.Run(new MainForm());
                    //}
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("Main:" + ex);
                MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")) + ",请重启程序。");

                //DigiForm digiForm = new DigiForm();
                //digiForm.UpdateAppSettings(DigiForm.RUNFORMFULLNAME, DigiForm.LOGINFORMFULLNAME);
            }
        }

        private static void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle -= Application_Idle;
            if (context.MainForm == null)
            {
                AppContext.Load();
                AppContext.AppConfig.serverUrl = ConfigurationManager.AppSettings["serverUrl"].ToString();
                LoginForm login = new LoginForm();
                login.ShowDialog();
                if (login.DialogResult == DialogResult.OK)
                {
                    MainForm form = new MainForm();
                    context.MainForm = form;
                    form.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            if (ex != null)
            {
                LogClass.WriteLog("Application_ThreadException:" + ex);
            }

            MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                LogClass.WriteLog("CurrentDomain_UnhandledException:" + ex);
            }

            MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")));
        }

        ///// <summary>
        ///// 应用程序的主入口点。
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    //Application.Run(new MainForm());
        //    LoginForm login = new LoginForm();
        //    login.ShowDialog();
        //    if (login.DialogResult == DialogResult.OK)
        //    {
        //        Application.Run(new MainForm());
        //    }
        //}
    }
}
