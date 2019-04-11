using Newtonsoft.Json.Linq;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Xr.Http;
using Xr.RtScreen.Models;

namespace Xr.RtScreen.VoiceCall
{
    public partial class SpeakVoicemainFrom : Form
    {
        // 第一步：声明一个委托。（根据自己的需求）
        public delegate void setTextValue(string textValue);
        //第二步：声明一个委托类型的事件
        public event setTextValue setFormTextValue;
        private SpeechVoiceSpeakFlags _spFlags;
        private SpVoice _voice;
        public SpeakVoicemainFrom()
        {
            InitializeComponent();
            this.MaximizeBox = false;///禁用最大化
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            timer1.Interval = Int32.Parse(ConfigurationManager.AppSettings["CallNextSpan"]);
            this.WindowState = FormWindowState.Minimized;
            this.Size = new Size(739, 150);
            time();
        }
        #region 数据更新
        public static Func<List<CallPrint>> GetDataUpdate = new Func<List<CallPrint>>(delegate()
        {
            return GetData();
        });
        int succeedCount = 0;
        static int failedCount = 0;
        int a = 0;
        public void UpdateForeach(List<CallPrint> list)
        {
            FuZhi = 0;
            foreach (var callpatient in list)
            {
                try
                {
                    _voice.Speak(callpatient.CallVoiceString(), _spFlags);
                    _voice.Speak(callpatient.CallVoiceString(), _spFlags);//播放两次
                }
                catch 
                {
                }
                setFormTextValue(callpatient.LogString());
                LogPrint(callpatient.LogString());
                succeedCount++;
                do
                {
                    FuZhi++;
                } while (FuZhi != 3000);
            }
            lab_succeedCount.Text = "正常次数：" + succeedCount;
            lab_failedCount.Text = "失败次数：" + failedCount;
            lab_lasttime.Text = "最后时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        static List<CallPrint> listPrint;
        private static List<CallPrint> GetData()
        {
            List<CallPrint> cpList = new List<CallPrint>();
            listPrint = new List<CallPrint>();
            string Url = "";
            if (Form1.ScreenType == "1")
            {
                Url = AppContext.AppConfig.serverUrl + InterfaceAddress.findCallList + "?hospitalId=" + HelperClass.hospitalId + "&deptId=" + HelperClass.deptId + "&clinicId="+"";
            }
            else
            {
                Url = AppContext.AppConfig.serverUrl + InterfaceAddress.findCallList + "?hospitalId=" + HelperClass.hospitalId + "&deptId=" + HelperClass.deptId + "&clinicId=" + HelperClass.clincId;
            }
            string result = HttpClass.httpPost(Url);
            Log4net.LogHelper.Info("呼号请求地址：" + Url);
            try
            {
                var objT = Newtonsoft.Json.Linq.JObject.Parse(result);
                Log4net.LogHelper.Info("呼号请求返回结果：" + objT);
                if (objT["state"].ToString().ToLower() != "true")
                {
                    return new List<CallPrint>();
                }
                else
                {
                    JArray jars = JArray.Parse(objT["result"].ToString());
                    foreach (var jar in jars)//遍历数组
                    {
                        string eName = String.Empty;
                        eName = jar.Value<string>("cellText") == null ? "" : jar.Value<string>("cellText").Trim();
                        if (jar.Value<string>("cellText") != null && jar.Value<string>("cellText") != String.Empty)
                        {
                            eName = "" + eName;
                        }
                        CallPrint cp2 = new CallPrint(jar.Value<string>("cellText"));
                        cpList.Add(cp2);
                    }
                }
            }
            catch (Exception rx)
            {
                Log4net.LogHelper.Error("查询呼号错误信息：" + rx.Message);
                failedCount++;
            }
            listPrint = cpList;
            return cpList;
        }
        #endregion
        #region 语音播放
        public void PlayVoice()
        {
            try
            {
                GetDataUpdate.BeginInvoke(ar =>
                {
                    //ar.AsyncWaitHandle.WaitOne(); 
                    //  使用EndInvoke获取到任务结果（5）
                    List<CallPrint> objT = GetDataUpdate.EndInvoke(ar);

                    //  使用Control.Invoke方法将5显示到一个label上，如果没有Invoke，
                    //  直接写lblStatus.Text="5"，将会抛出跨线程访问UI控件的异常
                    Invoke(new Action(() => UpdateForeach(objT)));
                }, null);
            }
            catch
            {
            }
        }
        #endregion
        #region 播放按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开始播放（暂停中）")
            {
                button1.Text = "暂停播放（播放中）";
                lab_startime.Text = "开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                PlayVoice();
                timer1.Start();
                _voice.Resume();
            }
            else
            {
                button1.Text = "开始播放（暂停中）";
                timer1.Stop();
                _voice.Pause();
            }
        }
        #endregion
        #region 播放设置
        private void SpeakVoicemainFrom_Load(object sender, EventArgs e)
        {
            _voice = new SpVoice();
            _spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            _voice.Rate = Int16.Parse(ConfigurationManager.AppSettings["VoiceRate"]);// -2;//播音速度
            _voice.Volume = 100;//音量大小
            try
            {
                _voice.Voice = _voice.GetVoices(string.Empty, string.Empty).Item(Int16.Parse(ConfigurationManager.AppSettings["VoicePackage"]));//选取指定语音包
            }
            catch
            {
                _voice.Voice = _voice.GetVoices(string.Empty, string.Empty).Item(0);//取不到指定语音包选取系统默认语音包
            }
            finally
            {
                this.button1.PerformClick();
            }
        }
        #endregion
        #region 播放
        private void timer1_Tick(object sender, EventArgs e)
        {
            PlayVoice();
        }
        #endregion
        #region 把数据写入到界面
        int FuZhi = 0;
        private delegate void LogPrintDelegate(string log);
        public void LogPrint(string log)
        {
            FuZhi = 0;
            if (txt_log.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以
                txt_log.Invoke(new LogPrintDelegate(LogPrint), log);
                //setFormTextValue(log);
            }
            else
            {
                //txt_log.AppendText(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "==>" + log + "\n");
                string newLine = String.Format("{0:yyyy-MM-dd HH:mm:ss}==>{1}\n{2}", DateTime.Now, log, Environment.NewLine);
                // 将textBox1的内容插入到第一行
                // 索引0是 richText1 第一行位置
                txt_log.Text = txt_log.Text.Insert(0, newLine);
                //setFormTextValue(log);
            }
        }
        private void SpeakVoicemainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion
        #region 清除滚动文字信息
        public void time()
        {
            if (!timer2.Enabled)
            {
                timer2.Interval = 1 * 60 * 1000;
                timer2.Start();
            }
            else
            {
                timer2.Stop();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (listPrint.Count == 0)
            {
                setFormTextValue("请耐心等候叫号");
            }
        }
        #endregion
    }
}
