using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace Xr.AutoUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //用子线程工作
            new System.Threading.Thread(new System.Threading.ThreadStart(StartUpdate)).Start();
        }

        //开始更新
        public void StartUpdate()
        {
            Downloader downloader = new Downloader();
            downloader.onDownLoadProgress += new Downloader.dDownloadProgress(downloader_onDownLoadProgress);
            downloader.Start();
        }
        //同步更新UI
        void downloader_onDownLoadProgress(long total, long current, String msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Downloader.dDownloadProgress(downloader_onDownLoadProgress), new object[] { total, current, msg });
            }
            else
            {
                this.progressBar1.Maximum = (int)total;
                this.progressBar1.Value = (int)current;
                label1.Text = msg;
                if (current == 100)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 下载类（您的复杂处理类）
        /// </summary>
        public class Downloader
        {
            //委托
            public delegate void dDownloadProgress(long total, long current, String msg);
            //事件
            public event dDownloadProgress onDownLoadProgress;
            //开始模拟工作
            public void Start()
            {
                int speedOfProgress = 0; //进度
                onDownLoadProgress(100, speedOfProgress, "开始检查版本更新");

                //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径
                string localPath = System.Environment.CurrentDirectory;

                string serverUrl = ConfigurationManager.AppSettings["serverUrl"].ToString();
                string version = ConfigurationManager.AppSettings["version"].ToString();
                string clientVersionType = ConfigurationManager.AppSettings["clientVersionType"].ToString();
                String url = serverUrl + "sys/clientVersion/findUpdateVersion?type=" + clientVersionType + "&version=" + version;
                String data = httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<ClientVersionEntity> cvList = objT["result"].ToObject<List<ClientVersionEntity>>();
                    onDownLoadProgress(100, speedOfProgress, "开始下载，共需下载" + cvList.Count + "个文件");
                    //下载解压
                    if (cvList.Count > 0)
                    {
                        int documentProgress = 90 / cvList.Count; //每个文件所拥有的进度
                        for (int i = 0; i < cvList.Count; i++)
                        {
                            String[] strArr = cvList[i].updateFilePath.Split(new char[] { '/' });
                            String fileName = strArr[strArr.Length - 1];
                            //下载
                            onDownLoadProgress(100, speedOfProgress, "开始下载，共需下载" + cvList.Count + "个文件，正在下载第1个文件:" + fileName);
                            downfile(cvList[i].updateFilePath, fileName, localPath);
                            speedOfProgress += documentProgress / 2;
                            //解压
                            onDownLoadProgress(100, speedOfProgress, "开始下载，共需下载" + cvList.Count + "个文件，第1个文件:" + fileName + "下载成功，正在解压");
                            ZipHelper.UnpackFileRarOrZip(localPath + "/" + fileName, localPath);
                            speedOfProgress += documentProgress / 2;
                        }
                        //修改本地配置文件中的版本号
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings["version"].Value = cvList[cvList.Count - 1].version;
                        configuration.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                    //启动程序
                    System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
                    //设置外部程序名  
                    Info.FileName = ConfigurationManager.AppSettings["StartUpEXE"].ToString();
                    //设置外部程序工作目录为   C:\  
                    Info.WorkingDirectory = ConfigurationManager.AppSettings[@"StartUpEXE"].ToString();
                    //最小化方式启动
                    Info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    //声明一个程序类  
                    System.Diagnostics.Process Proc;
                    try
                    {
                        Proc = System.Diagnostics.Process.Start(Info);
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine();
                    }
                    catch (System.ComponentModel.Win32Exception x)
                    {
                        MessageBox.Show(x.ToString());
                    }
                    onDownLoadProgress(100, 100, "检查更新完成");
                }
                else
                {
                    onDownLoadProgress(100, 100, objT["message"].ToString());
                }
            }

            //下载文件1
            public static void downfile(string downloadUrl, string filename, string filepath)
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(downloadUrl);
                hwr.Timeout = 15000;
                //hwr.CookieContainer = new CookieContainer();
                HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
                Stream ss = hwp.GetResponseStream();
                byte[] buffer = new byte[10240];
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                FileStream fs = new FileStream(
                    string.Format(filepath + @"\" + filename),
                    FileMode.Create);
                try
                {
                    int i;
                    while ((i = ss.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, i);
                    }
                    fs.Close();
                    ss.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// post模拟表单提交
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static String httpPost(String url)
        {
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                System.GC.Collect();    // 请求之前 做一次垃圾回收
                System.Net.ServicePointManager.DefaultConnectionLimit = 20;

                // 打印 请求地址及参数
                WriteLog("请求数据：" + url);

                request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "*/*";
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0";
                //request.CookieContainer = CookieEntity.cookie;
                request.KeepAlive = false;          // 保持短链接
                request.Timeout = 1 * 60 * 1000;    // 1分钟，以防网络超时

                response = (HttpWebResponse)request.GetResponse();
                //CookieEntity.cookie = request.CookieContainer;

                stream = response.GetResponseStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                result = reader.ReadToEnd().Trim();

                // 打印响应结果
                WriteLog("响应结果：" + result);
            }
            catch (Exception e)
            {
                result = e.Message;
                WriteExceptionLog(e);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (response != null) response.Close();
                if (request != null) request.Abort();
            }
            return result;
        }

        public static string path = Environment.CurrentDirectory + "\\sysLog";
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="logMsg"></param>
        public static void WriteLog(string logMsg)
        {
            string fileName = "\\sys" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            try
            {
                if (!Directory.Exists(path))
                {
                    //不存在则创建           
                    Directory.CreateDirectory(path);
                }
                string logFileName = path + fileName;//生成日志文件全名称  
                if (!File.Exists(logFileName))
                {
                    //不存在则创建
                    File.Create(logFileName).Close();
                }
                /*文件超过10MB则重命名,同时创建新的文件*/
                FileInfo finfo = new FileInfo(logFileName);
                if (finfo.Length > 1024 * 1024 * 10)
                {
                    string newFileName = path + "\\sys" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".log";
                    File.Move(logFileName, newFileName);
                    File.Create(logFileName).Close();//创建文件  
                }

                StreamWriter writer = File.AppendText(logFileName);//文件中添加文件流  
                writer.WriteLine("");
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + logMsg);
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                string path = Path.Combine("./sysLog");
                if (!Directory.Exists(path))
                {
                    //不存在则创建           
                    Directory.CreateDirectory(path);
                }
                string logFileName = path + fileName;//生成日志文件全名称 
                if (!File.Exists(logFileName))
                {
                    //不存在则创建
                    File.Create(logFileName).Close();
                }
                StreamWriter writer = File.AppendText(logFileName);
                writer.WriteLine("");
                writer.WriteLine(DateTime.Now.ToString("日志记录错误HH:mm:ss") + " " + e.Message + " " + logMsg);
                writer.Flush();
                writer.Close();
            }
        }
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteExceptionLog(Exception ex)
        {
            StringBuilder exMsg = new StringBuilder();
            exMsg.Append("程序异常信息：")
                 .Append("\r\n   " + ex.StackTrace.Trim().Replace("位置", "\r\n   在 触发文件："))
                 .Append("\r\n   在 触发方法：" + ex.TargetSite)
                 .Append("\r\n   在 异常信息：" + ex.Message);
            WriteLog(exMsg.ToString());
        }
    }
}
