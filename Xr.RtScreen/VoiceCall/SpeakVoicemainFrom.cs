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

namespace Xr.RtScreen.VoiceCall
{
    public partial class SpeakVoicemainFrom : Form
    {
        private SpeechVoiceSpeakFlags _spFlags;
        private SpVoice _voice;
        public SpeakVoicemainFrom()
        {
            InitializeComponent();
            this.MaximizeBox = false;///禁用最大化
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            timer1.Interval = Int32.Parse(ConfigurationManager.AppSettings["CallNextSpan"]);
            this.WindowState = FormWindowState.Minimized;
        }
        #region 数据更新
        public static Func<List<CallPrint>> GetDataUpdate = new Func<List<CallPrint>>(delegate()
        {
            return GetData();
        });
        int succeedCount = 0;
        static int failedCount = 0;
        public void UpdateForeach(List<CallPrint> list)
        {
            foreach (var callpatient in list)
            {
                _voice.Speak(callpatient.CallVoiceString(), _spFlags);
                _voice.Speak(callpatient.CallVoiceString(), _spFlags);//播放两次
              //  LogPrints(callpatient.LogString());
                succeedCount++;
            }
            lab_succeedCount.Text = "正常次数：" + succeedCount;
            lab_failedCount.Text = "失败次数：" + failedCount;
            lab_lasttime.Text = "最后时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private static List<CallPrint> GetData()
        {
            List<CallPrint> cpList = new List<CallPrint>();
            string serverUrl = ConfigurationManager.AppSettings["YuYing"];
            string unitSn = ConfigurationManager.AppSettings["unitSn"];
            Dictionary<string, string> pa = new Dictionary<string, string>();
            pa.Add("unitSn", unitSn);
            string result = HttpHelper.CallRemote(serverUrl, pa, HttpMethod.Post);
            try
            {
                var objT = Newtonsoft.Json.Linq.JObject.Parse(result);
                if (objT["common_return"].ToString().ToLower() != "true")
                {
                    return new List<CallPrint>();
                }
                else
                {
                    JArray jars = JArray.Parse(objT["return_info"].ToString());
                    foreach (var jar in jars)//遍历数组
                    {
                        string bespeakClass = jar.Value<string>("showNumber") == null ? "" : jar.Value<string>("showNumber").Trim();
                        string eName = String.Empty;
                        eName = jar.Value<string>("showNumber") == null ? "" : jar.Value<string>("showNumber").Trim();
                        if (jar.Value<string>("showNumber") != null && jar.Value<string>("showNumber") != String.Empty)
                        {
                            eName = "" + eName;
                        }
                        CallPrint cp2 = new CallPrint(jar.Value<string>("showNumber"), eName + jar.Value<string>("patientName"), jar.Value<string>("siteName"), jar.Value<string>("typeName"));
                        cpList.Add(cp2);
                    }
                }
            }
            catch (Exception rx)
            {
               // Logs("错误信息：" + rx.Message);
            }
           // Logs("请求地址：" + serverUrl + "?unitSn=" + unitSn);
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
    }
}
