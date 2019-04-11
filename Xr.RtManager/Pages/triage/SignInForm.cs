﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Common;
using Xr.Http;
using Xr.Common.Controls;
using Xr.RtManager.Pages.booking;
using Xr.RtManager.Module.triage;
using Xr.RtManager.Utils;
using DevExpress.XtraEditors;

namespace Xr.RtManager.Pages.triage
{
    public partial class SignInForm : UserControl
    {
        private Form MainForm; //主窗体
        Xr.Common.Controls.OpaqueCommand cmd;

        private MainForm _parentWin;//主页面
        //private WaitingForm _waitForm;

        //private AsynchronousWorks WorkType = new AsynchronousWorks();
        //private int tem = 0;
        public int inIf = -1;//0 成功可登陆  1未激活  2未能读卡
        StringBuilder patientName = new StringBuilder(31);
        StringBuilder Gender = new StringBuilder(3);
        StringBuilder Folk = new StringBuilder(10);
        StringBuilder BirthDay = new StringBuilder(9);
        StringBuilder Code = new StringBuilder(19);
        StringBuilder Address = new StringBuilder(255);
        StringBuilder Agency = new StringBuilder(31);
        StringBuilder ExpireStart = new StringBuilder(9);
        StringBuilder ExpireEnd = new StringBuilder(9);
        bool NeedCloseWaitingFrm = true;
        /// <summary>
        /// 查询卡号
        /// </summary>
        private string CardID = String.Empty;
        /// <summary>
        /// 医生ID
        /// </summary>
        private string Doctorid = String.Empty;
        /// <summary>
        /// 选中医生ID
        /// </summary>
        private string SelectDoctorid = String.Empty;
        /// <summary>
        /// 选中医生所属科室ID
        /// </summary>
        private string SelectDoctorDepid = String.Empty;
        /// <summary>
        /// 选中医生排班
        /// </summary>
        private string SelectDoctorSchema = String.Empty;
        /// <summary>
        /// 患者主键
        /// </summary>
        private string Patientid = String.Empty;
        /// <summary>
        /// 预约状态
        /// </summary>
        String BookingStatus = String.Empty;
        /// <summary>
        /// 预约主键
        /// </summary>
        String RegisterId = String.Empty;
        /// <summary>
        /// 分诊签到记录主键 分诊后再查询时不为空
        /// </summary>
        String TriageId = String.Empty;
         /// <summary>
        /// 分诊签到记录主键 (患者列表用)
        /// </summary>
        String TriageIdInList = String.Empty;
        /// <summary>
        /// 候诊列表用午别
        /// </summary>
        String  PeriodWaiting = String.Empty;

        public MainForm ParentWindow
        {
            get { return _parentWin; }
            set { _parentWin = value; }
        }
        //public int openT6 = -1;//0 社保卡是否正常打开  0正常 非0不正常++++++++++
        public SignInForm()
        {
            InitializeComponent();
            
            //cmd.ShowOpaqueLayer(225, true);

            //初始化T6 有线程延迟
            /*openT6 = HardwareInitialClass.OpenDevice();
            if (openT6 != 0)
            {
                LogClass.WriteLog("社保读卡器初始化失败:");
            }
            else
            {
                LogClass.WriteLog("社保读卡器初始化成功");
            }
             */
        }

        private JObject obj { get; set; }

        private void UserForm_Load(object sender, EventArgs e)
        {
            MainForm = (Form)this.Parent;
            xtraTabControl1.SelectedTabPage.ResetBackColor();
            xtraTabControl1.SelectedTabPage.BackColor = Color.Transparent;
            cmd = new Xr.Common.Controls.OpaqueCommand(AppContext.Session.waitControl);
            GetLuesInfo();
            SetNoonCode();
            //WorkType = AsynchronousWorks.RoomListQuery;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });

            ReservationInfo_Panel.Visible = false;
            //获取患者列表状态
            GetPatientListStatus();

        }
        /// <summary>
        /// 下拉框数据
        /// </summary>
        private void GetLuesInfo()
        {
            //科室下拉框数据
            lueDept.Properties.DataSource = AppContext.Session.deptList;
            lueDept.Properties.DisplayMember = "name";
            lueDept.Properties.ValueMember = "id";

            lueDept_1.Properties.DataSource = AppContext.Session.deptList;
            lueDept_1.Properties.DisplayMember = "name";
            lueDept_1.Properties.ValueMember = "id";
            
            //默认选中第一个
            lueDept.EditValue = AppContext.Session.deptList[0].id;
            lueDept_1.EditValue = AppContext.Session.deptList[0].id;

            //卡类型下拉框数据
            String param = "type={0}";
            param = String.Format(param, "card_type");

            String url = String.Empty;
            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?" + param;
            JObject objT = new JObject();
            objT = JObject.Parse(HttpClass.httpPost(url));
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                //List<Dic> list = objT["result"].ToObject<List<Dic>>();
                List<DictEntity> list = new List<DictEntity>();
                list.Add(new DictEntity { label = "全部", value = "" });
                list.AddRange(objT["result"].ToObject<List<DictEntity>>());

                lueCardType.Properties.DataSource = list;
                lueCardType.Properties.DisplayMember = "label";
                lueCardType.Properties.ValueMember = "value";
                lueCardType.ItemIndex = 0;

                lueCardTypeQuery.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueCardTypeQuery.Properties.DisplayMember = "label";
                lueCardTypeQuery.Properties.ValueMember = "value";
                lueCardTypeQuery.ItemIndex = 1;

                

            }
            else
            {
                MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                return;
            }
            //预约途径下拉框数据
            param = "type={0}";
            param = String.Format(param, "register_way_type");

            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?" + param;
            objT = JObject.Parse(HttpClass.httpPost(url));
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DictEntity> list = new List<DictEntity>();
                list.Add(new DictEntity { label = "全部", value = "" });
                list.AddRange(objT["result"].ToObject<List<DictEntity>>());
                lueRegisterWay.Properties.DataSource = list;
                lueRegisterWay.Properties.DisplayMember = "label";
                lueRegisterWay.Properties.ValueMember = "value";
                lueRegisterWay.ItemIndex = 0;
            }
            else
            {
                MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
            }
            //日期选框配置
            this.de_date.EditValue = System.DateTime.Now;
            this.de_date.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.de_date.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.de_date.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.de_date.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.de_date.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.de_date.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
            this.de_date.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));
        }
        #region 输入文本框限制
        private void txt_cardNoQuery_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar == 'X'))//这是允许输入"X"  
                {

                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }
        private void txt_cardNoQuery_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }
        private void txt_cardNoQuery_KeyUp(object sender, KeyEventArgs e)
        {
            //允许Ctrl+v粘贴数字
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                if (Clipboard.ContainsText())
                {
                    try
                    {
                        Convert.ToInt64(Clipboard.GetText());  //检查是否数字
                        //((TextBox)sender).SelectedText = Clipboard.GetText().Trim(); //Ctrl+V 粘贴  
                        ((TextBox)sender).Text = Clipboard.GetText().Trim();

                    }
                    catch (Exception)
                    {
                        e.Handled = true;
                        //throw;
                    }
                }
            }

            if (txt_cardNoQuery.Text != String.Empty)
            {
                if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
                {
                    CardID = txt_cardNoQuery.Text;
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReadzlCard, Argument = new String[] { CardID, lueCardTypeQuery.EditValue.ToString() } });
                    txt_cardNoQuery.Text = String.Empty;
                    flowLayoutPanel1.Focus();
                }
            }
        }
        #endregion
        public String ReadType { get; set; }
        private void btn_zlk_Click(object sender, EventArgs e)
        {
            //ClearUIInfo();
            //WorkType = AsynchronousWorks.ReadzlCard;
            //cmd.IsShowCancelBtn = false;
            //cmd.ShowOpaqueLayer();
            /*if (txt_cardNoQuery.Text != String.Empty)
            {
                    CardID = txt_cardNoQuery.Text;
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReadzlCard, Argument = new String[] { CardID } });
                    txt_cardNoQuery.Text = String.Empty;
            }
             */
            lueCardTypeQuery.EditValue = "2";
            if (txt_cardNoQuery.Text != String.Empty)
            {
                CardID = txt_cardNoQuery.Text;
                ReadType = "1";
                //CardID = "000675493100";
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReadzlCard, Argument = new String[] { CardID, "2" } });
            }
        }
        private void btn_readSocialcard_Click(object sender, EventArgs e)
        {
            
            //WorkType = AsynchronousWorks.ReadSocialcard;
            timer1.Start();
            ReadType = "2";
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReadSocialcard});
        }

        private void btn_readIdcard_Click(object sender, EventArgs e)
        {
            //WorkType = AsynchronousWorks.ReadIdCard;
            timer1.Start();
            ReadType = "3";
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReadIdCard });
        }
        public bool CancelFlag = false;
        /// <summary>
        /// 午别，0 上午，1下午，2晚上，3全天
        /// </summary>
        private String Period = "0";
        /// <summary>
        /// 患者列表状态：0预约、1候诊中、2已就诊、3全部
        /// </summary>
        private String PatientListStatus = "1";//默认状态
        private void Asynchronous(AsyncEntity ars)
        {
            //异步操作
            if (!this.backgroundWorker1.IsBusy)
            {
                backgroundWorker1 = new BackgroundWorker();
                var bw = backgroundWorker1;
                bw.WorkerReportsProgress = true;
                //需要异步的操作
                bw.DoWork += new DoWorkEventHandler(DoWork);
                //异步操作时报告前台状态变更
                //bw.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
                //异步操作完成后操作
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.WorkerSupportsCancellation = true;
                //异步调用结束后释放操作
                bw.Disposed += new EventHandler(Disposed);
                //开始异步操作
                bw.RunWorkerAsync(ars);

                //打开等待框
                if (ars.WorkType == AsynchronousWorks.RoomListQuery)
                {
                    cmd.IsShowCancelBtn = false;
                    cmd.ShowOpaqueLayer(0.56f, "请稍后...");
                    flowLayoutPanel1.Controls.Clear();
                    lab_countNum.Text = "0";
                }
                else if (ars.WorkType == AsynchronousWorks.ReadzlCard)
                {
                    cmd.IsShowCancelBtn = false;
                    cmd.ShowOpaqueLayer();
                    ClearUIInfo();
                }

                else if (ars.WorkType == AsynchronousWorks.ReadIdCard || ars.WorkType == AsynchronousWorks.ReadSocialcard)
                {
                    cmd.IsShowCancelBtn = true;
                    cmd.ShowOpaqueLayer(0.56f, "正在读取...");
                    ClearUIInfo();
                }
                    /*
                else if (WorkType == AsynchronousWorks.SingInOrRegister)
                {
                    //cmd.IsShowCancelBtn = false;
                    //cmd.ShowOpaqueLayer();
                }
                     */
               else
                {
                     if (NeedCloseWaitingFrm)
                    {
                        cmd.IsShowCancelBtn = false;
                        cmd.ShowOpaqueLayer(0.56f, "请稍后...");
                    }
                }
            }
        }
        void DoWork(object sender, DoWorkEventArgs e)
        {
            SycResult result = new SycResult();
            String[] Pras=new String[]{};
            AsyncEntity Arg = e.Argument as AsyncEntity;
            AsynchronousWorks workType = Arg.WorkType;
            result.WorkType = Arg.WorkType;
            if (Arg.Argument != null)
            {
                Pras = Arg.Argument;
            }
            // 异步操作1
            Thread.Sleep(100);
            try
            {
                #region 医生坐诊列表
                if (workType == AsynchronousWorks.RoomListQuery)
                {
                    //报告前台状态变更
                    backgroundWorker1.ReportProgress(50);
                    // 异步操作2
                    //Thread.Sleep(300);
                    String param = "";
                    //获取医生坐诊信息
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    //hospitalId=12&deptId=2&period=3
                    prament.Add("hospitalId", AppContext.Session.hospitalId);
                    prament.Add("deptIds", AppContext.Session.deptIds);
                    prament.Add("period", Period);
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/doctorSitting/findDoctorSitting?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    //jsonStr=@"{""code"":200,""message"":""操作成功"",""result"":[{""doctorId"":1,""doctorName"":""张医生"",""period"":""下午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""晚上"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""下午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""晚上"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""下午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""下午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""晚上"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""下午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""下午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""晚上"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""下午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":1,""doctorName"":""张医生"",""period"":""上午"",""clinicPrefix"":""A"",""clinicName"":""01诊室"",""waitingNum"":0,""siteSyNum"":80,""isStop"":""0""},{""doctorId"":15,""doctorName"":""杰大哥"",""period"":""全天"",""clinicPrefix"":""B"",""clinicName"":""02诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":15,""doctorName"":""杰大哥"",""period"":""上午"",""clinicPrefix"":""B"",""clinicName"":""02诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":15,""doctorName"":""杰大哥"",""period"":""下午"",""clinicPrefix"":""B"",""clinicName"":""02诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":15,""doctorName"":""杰大哥"",""period"":""晚上"",""clinicPrefix"":""B"",""clinicName"":""02诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":15,""doctorName"":""杰大哥"",""period"":""上午"",""clinicPrefix"":""B"",""clinicName"":""02诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":15,""doctorName"":""杰大哥"",""period"":""下午"",""clinicPrefix"":""B"",""clinicName"":""02诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""},{""doctorId"":15,""doctorName"":""杰大哥"",""period"":""晚上"",""clinicPrefix"":""B"",""clinicName"":""02诊室"",""waitingNum"":0,""siteSyNum"":0,""isStop"":""0""}],""state"":true}";

                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        e.Result = result;
                    }

                }
                #endregion
                #region 查询患者信息
                else if (workType == AsynchronousWorks.QueryID || workType == AsynchronousWorks.ReadzlCard)
                {

                    //报告前台状态变更
                    backgroundWorker1.ReportProgress(50);
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作
                    if (CardID != String.Empty)
                    {
                        //String serverUrl = ConfigurationManager.AppSettings["serverUrl"].ToString();
                        //String jsonStr = HttpClass.HRequest(serverUrl + "bedChargeSettle/queryHosRecords?numType=2&&num=" + SocialCardID + "&&queryType=1");

                        String param = "";
                        //获取患者信息
                        Dictionary<string, string> prament = new Dictionary<string, string>();
                        JObject objT = new JObject();
                        String url = String.Empty;
                        String jsonStr = String.Empty;
                        /*if (workType == AsynchronousWorks.QueryID)
                        {
                            //prament.Add("cardNo", CardID);
                            prament.Add("cardNo", Pras[0]);

                            //prament.Add("pageSize", "10000");


                            if (prament.Count != 0)
                            {
                                param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                            }
                            url = AppContext.AppConfig.serverUrl + "patmi/findPatMiByCardNo?" + param;
                            jsonStr = HttpClass.httpPost(url);
                            objT = JObject.Parse(jsonStr);
                        }
                         */
                        //手动输入 AsynchronousWorks.ReadzlCard
                        //else
                        //{
                            prament.Add("cardNo", Pras[0]);
                            prament.Add("cardType", Pras[1]);
                            if (prament.Count != 0)
                            {
                                param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                            }
                            url = AppContext.AppConfig.serverUrl + "patmi/findPatMiByTyptAndCardNo?" + param;
                            jsonStr = HttpClass.httpPost(url);
                            objT = JObject.Parse(jsonStr);
                       // }
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;

                        CardID = String.Empty;
                    }
                    else //输入为空时不查询
                    {
                        result.obj = null;
                        result.result = true;
                        result.msg = "成功";
                        e.Result = result;
                    }
                    NeedCloseWaitingFrm = true;
                }
                #endregion
                #region 查询患者预约信息
                else if (workType == AsynchronousWorks.QueryPatientReservation)
                {
                    String param = "";
                    //获取患者信息
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    JObject objT = new JObject();
                    String url = String.Empty;
                    String jsonStr = String.Empty;

                    prament.Add("patientId", Patientid);
                    //prament.Add("patientId", CardID);
                    //prament.Add("pageSize", "10000");

                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "itf/patient/queryArrears?" + param;
                    jsonStr = HttpClass.httpPost(url);//{"code":405,"message":"有未交费的处方，请到窗口或自助机交费后再签到。\r\n \r\n儿科急诊/全院/2016-11-24/一次性消毒药袋(纸塑)/0.09元。\r\n急诊科/付东明/2016-03-21/消毒药袋(电子)(纸塑)/0.18元。\r\n急诊科/洪海斌★/2015-11-26/消毒药袋(电子)(纸塑)/0.09元。\r\n急诊科/洪海斌★/2016-03-29/消毒药袋(电子)(纸塑)/0.18元。\r\n急诊科/洪海斌★/2016-07-17/消毒药袋(电子)(纸塑)/0.09元。\r\n急诊科/黄日华/2016-06-29/消毒药袋(电子)(纸塑)/0.18元。\r\n急诊科/李海忠●/2015-12-04/消毒药袋(电子)(纸塑)/0.09元。\r\n急诊科/庄炯宇/2015-11-16/消毒药袋(电子)(纸塑)/0.09元。\r\n","result":"","state":false}
                    objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        prament = new Dictionary<string, string>();
                        //hospitalId=12&deptId=2&patientId=000675493100
                        prament.Add("hospitalId", AppContext.Session.hospitalId);
                        prament.Add("deptIds", AppContext.Session.deptIds);
                        prament.Add("patientId", Patientid);
                        //prament.Add("patientId", CardID);
                        //prament.Add("pageSize", "10000");

                        if (prament.Count != 0)
                        {
                            param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                        }
                        url = AppContext.AppConfig.serverUrl + "sch/doctorScheduRegister/patCurrentRegsiter?" + param;
                        //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                        jsonStr = HttpClass.httpPost(url);//{"code":204,"message":"没有可以签到的预约记录","result":"","state":false}
                        objT = JObject.Parse(jsonStr);
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.obj = objT;
                        result.result = false;
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }

                }
                #endregion
                #region 读取身份证
                else if (workType == AsynchronousWorks.ReadIdCard)
                {
                    //try
                    //{
                    //    JLIdCardInfoClass idCardInfo = JLIdCardInfoClass.getCardInfo();
                    //    if (idCardInfo != null)
                    //    {
                    //        IDCardID = idCardInfo.Code.ToString();
                    //    }
                    //    if (IDCardID != String.Empty)
                    //    {
                    //        //patientId = carMes.user_id;
                    //        LogClass.WriteLog("读取身份证成功，身份证号：" + IDCardID);
                    //    }
                    //    result.obj = null;
                    //    result.result = true;
                    //    result.msg = "成功";
                    //    e.Result = result;
                    //}
                    //catch (Exception ee)
                    //{
                    //    result.obj = null;
                    //    result.result = false;
                    //    result.msg = "读取身份证失败:" + ee.Message;
                    //    e.Result = result;
                    //}

                    try
                    {
                        JLIdCardInfoClass.CancelFlag = false;
                        BackgroundWorker bgworker = sender as BackgroundWorker;
                        //绑定委托要执行的方法 
                        ReadCardDelegate work = new ReadCardDelegate(ReturnReadCardData);

                        //开始异步执行(ReturnDataTable)方法 
                        IAsyncResult ret = work.BeginInvoke("IdCard", null, null);

                        //(异步编程模式好久就是在执行一个很耗时的方法(ReturnDataTable)时,还能向下继续运行代码) 

                        //接着运行下面的while循环, 
                        //判断异步操作是否完成 
                        while (!ret.IsCompleted)
                        {
                            //没完成 
                            //判断是否取消了backgroundworker异步操作 
                            if (bgworker.CancellationPending)
                            {
                                //如何是  马上取消backgroundwork操作(这个地方才是真正取消) 
                                JLIdCardInfoClass.CancelFlag = true;
                                e.Cancel = true;
                                return;
                            }
                        }
                        e.Result = work.EndInvoke(ret); //返回查询结果 赋值给e.Result 
                    }
                    catch (Exception ex)
                    {
                        e.Result = ex.Message;
                    }

                }
                #endregion
                #region 读取社保卡
                else if (workType == AsynchronousWorks.ReadSocialcard)
                {
                    CancelFlag = false;
                    try
                    {
                        //while (!CancelFlag)
                        //{

                        //    SocialCard carMes = new SocialCard();
                        //    carMes.readCard();
                        //    if (carMes.message_type == "1")
                        //    {
                        //        CancelFlag = true;
                        //        //patientId = carMes.user_id;
                        //        LogClass.WriteLog("读取社保卡成功，卡号：" + carMes.user_id);
                        //        SocialCardID = carMes.user_id;
                        //    }

                        //}
                        //result.obj = null;
                        //result.result = true;
                        //result.msg = "成功";
                        //e.Result = result;

                        BackgroundWorker bgworker = sender as BackgroundWorker;
                        //绑定委托要执行的方法 
                        ReadCardDelegate work = new ReadCardDelegate(ReturnReadCardData);

                        //开始异步执行(ReturnDataTable)方法 
                        IAsyncResult ret = work.BeginInvoke("", null, null);

                        //(异步编程模式好久就是在执行一个很耗时的方法(ReturnDataTable)时,还能向下继续运行代码) 

                        //接着运行下面的while循环, 
                        //判断异步操作是否完成 
                        while (!ret.IsCompleted)
                        {
                            //没完成 
                            //判断是否取消了backgroundworker异步操作 
                            if (bgworker.CancellationPending)
                            {
                                //如何是  马上取消backgroundwork操作(这个地方才是真正取消) 
                                SocialCard cardMes = new SocialCard();
                                cardMes.cancelReadCard();
                                CancelFlag = true;
                                e.Cancel = true;
                                return;
                            }
                        }
                        e.Result = work.EndInvoke(ret); //返回查询结果 赋值给e.Result 
                    }
                    catch (Exception ee)
                    {
                        result.obj = null;
                        result.result = false;
                        result.msg = "读取社保卡失败:" + ee.Message;
                        CancelFlag = true;
                        e.Result = result;
                    }
                }
                #endregion
                #region 签到（查询患者后若有未签到自动调用 或 预约医生停诊后 签到其他医生调用）
                else if (workType == AsynchronousWorks.SingIn)
                {
                    //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作

                    String param = "";
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    //签到
                    prament.Add("registerId", RegisterId);
                    if (Doctorid != String.Empty)
                    {
                        prament.Add("doctorId", Doctorid);
                    }
                    Doctorid = String.Empty;//请求后置空
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/signIn?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;

                        e.Result = result;
                    }

                }
                #endregion
                #region 现场挂号
                else if (workType == AsynchronousWorks.Register)
                {
                    //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作

                    String param = "";
                    //请求现场挂号
                    Dictionary<string, string> prament = new Dictionary<string, string>();

                    //预约挂号
                    prament.Add("hospitalId", Pras[0]);
                    prament.Add("deptId", Pras[1]);
                    prament.Add("period", Pras[2]);
                    prament.Add("doctorId", Doctorid);
                    prament.Add("patientId", Patientid);
                    prament.Add("patientName", lab_name.Text);
                    if (cb_urgent.Checked)//是否加急：0是、1否
                    {
                        prament.Add("urgent", "0");
                    }
                    else
                    {
                        prament.Add("urgent", "1");
                    }
                    //prament.Add("pageSize", "10000");

                    Doctorid = String.Empty;//请求后置空
                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/onSite?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;

                        e.Result = result;
                    }
                }
                #endregion
                #region 加急
                //加急
                else if (workType == AsynchronousWorks.Urgent)
                {
                    //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作

                    String param = "";
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    prament.Add("triageId", Pras[0]);
                    //prament.Add("triageId", TriageId);
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/urgent?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        e.Result = result;
                    }

                }
                #endregion
                #region 过号重排
                //过号重排
                else if (workType == AsynchronousWorks.PassNum)
                {
                    //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作

                    String param = "";
                    //请求过号重排
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    prament.Add("triageId", Pras[0]);
                    //prament.Add("triageId", TriageId);
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/passNum?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        e.Result = result;
                    }

                }
                #endregion
                #region 复诊签到
                //复诊签到
                else if (workType == AsynchronousWorks.ReviewSignIn)
                {
                    //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作

                    String param = "";
                    //请求复诊签到
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    if (Pras.Length == 1)
                    {
                        prament.Add("triageId", Pras[0]);
                        //prament.Add("triageId", TriageId);
                        //prament.Add("pageSize", "10000");
                    }
                    else //复诊医生停诊转诊
                    {
                        prament.Add("triageId", Pras[0]);
                        prament.Add("doctorId", Pras[1]);
                    }

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/reviewSignIn?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        e.Result = result;
                    }

                }
                #endregion
                #region 取消候诊
                //取消候诊
                else if (workType == AsynchronousWorks.CancelWaiting)
                {
                    //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作

                    String param = "";
                    //请求取消候诊
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    prament.Add("triageId", Pras[0]);
                    //prament.Add("triageId", TriageId);
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/cancelWaiting?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        e.Result = result;
                    }

                }
                #endregion
                #region 候诊指引单
                //候诊指引单
                else if (workType == AsynchronousWorks.WaitingList)
                {
                    //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                    // 异步操作2
                    //Thread.Sleep(300);
                    //提交异步操作结果供结束时操作

                    String param = "";
                    //获取候诊指引单
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    prament.Add("triageId", Pras[0]);
                    //prament.Add("triageId", TriageId);
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/waitingList?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        e.Result = result;
                    }

                }
                #endregion
                #region 候诊患者列表
                else if (workType == AsynchronousWorks.WaitingPatientList)
                {
                    //报告前台状态变更
                    backgroundWorker1.ReportProgress(50);
                    // 异步操作2
                    //Thread.Sleep(300);
                    String param = "";
                    //获取候诊患者列表
                    Dictionary<string, string> prament = new Dictionary<string, string>();
                    //hospitalId=12&deptId=2&doctorId=1&workDate=2019-01-10&period=3&status=3
                    prament.Add("hospitalId", AppContext.Session.hospitalId);
                    prament.Add("deptId", SelectDoctorDepid);
                    prament.Add("doctorId", SelectDoctorid);
                    prament.Add("workDate", de_date.Text);
                    prament.Add("period", PeriodWaiting);
                    prament.Add("status", PatientListStatus);
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "sch/registerTriage/findPatientListByDoctor?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    //jsonStr = @"{""code"":200,""message"":""操作成功"",""result"":[{""patientName"":""李鹏真"",""registerWay"":""2"",""cradType"":""1 "",""cradNo"":"""",""regVisitTime"":""08:00 - 08:30"",""regTime"":""2019-02-15 16:52:57"",""status"":""待签到""},{""patientName"":""李鹏真"",""registerWay"":""2"",""cradType"":""1 "",""cradNo"":""02096999"",""regVisitTime"":""15:00 - 15:30"",""regTime"":""2019-02-15 17:30:22"",""status"":""待签到""}],""state"":true}";

                    JObject objT = JObject.Parse(jsonStr);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        result.obj = objT;
                        result.result = true;
                        result.args = Pras;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        e.Result = result;
                    }

                }
                #endregion
            }
            catch(Exception ex)
            {
                result.result = false;
                result.msg =ex.Message;// PatientSearchInfoRef.Msg;
                e.Result = result;
            }
        }
        private delegate SycResult ReadCardDelegate(string parm);  //创建一个委托 
        /// <summary> 
        /// 异步委托的方法 
        /// </summary> 
        /// <param name="sql"></param> 
        /// <returns></returns> 
        private SycResult ReturnReadCardData(string parm)
        {

            SycResult Result = new SycResult() { result = true };
            NeedCloseWaitingFrm = false;
            //System.Threading.Thread.Sleep(10000);
            if (parm == "IdCard")
            {
                //System.Threading.Thread.Sleep(3000);
                //if (backgroundWorker1.IsBusy)
                //{
                //    CardID = "45032219871222151X";
                //    Result.WorkType = AsynchronousWorks.ReadIdCard;
                //    Result.obj = CardID;
                //}

                JLIdCardInfoClass idCardInfo = JLIdCardInfoClass.getCardInfo();
                if (idCardInfo != null)
                {
                 if(backgroundWorker1.IsBusy)
                 {
                    CardID = idCardInfo.Code.ToString();
                    Result.WorkType = AsynchronousWorks.ReadIdCard;
                 }
                }
                if (CardID != String.Empty)
                {
                    //patientId = carMes.user_id;
                    Result.obj = CardID;
                    LogClass.WriteLog("读取身份证成功，身份证号：" + CardID);
                }
                 
                //result.obj = null;
                //result.result = true;
                //result.msg = "成功
            }
            else
            {
                /*System.Threading.Thread.Sleep(3000);
                if (backgroundWorker1.IsBusy)
                {
                    CardID = "000675493100";
                    Result.WorkType = AsynchronousWorks.ReadSocialcard;
                    Result.obj = CardID;
                }
                 */
                
                while (!CancelFlag)
                {
                    SocialCard carMes = new SocialCard();
                    carMes.readCard();
                    if (carMes.message_type == "1")
                    {
                        CancelFlag = true;
                        //patientId = carMes.user_id;
                        LogClass.WriteLog("读取社保卡成功，卡号：" + carMes.user_id);
                        if (backgroundWorker1.IsBusy)
                        {
                            CardID = carMes.user_id;
                            Result.WorkType = AsynchronousWorks.ReadSocialcard;
                            Result.obj = CardID;
                        }
                        break;
                    }
                }
                 
                //result.obj = null;
                //result.result = true;
                //result.msg = "成功";
                //e.Result = result;
            }


            return Result;
        } 


        
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //停止异步调用
            this.backgroundWorker1.CancelAsync();

            try
            {
                //Thread.Sleep(500);
                //通过异步操作完成结果判断后续提示
                if (e.Result == null)
                {
                    //MessageBoxUtils.Hint("操作失败，请稍候尝试。");
                    MessageBoxUtils.Show("操作失败，请稍候尝试。", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                    return;
                }
                var result = (SycResult)e.Result;
                if (result.result)
                {
                    AsynchronousWorks workType = result.WorkType;
                    #region 科室坐诊列表
                    if (workType == AsynchronousWorks.RoomListQuery)
                    {
                        if (result.obj == null)
                        {
                            //_waitForm.DialogResult = DialogResult.OK;
                            //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                            //_waitForm.Close();
                            cmd.HideOpaqueLayer();
                            return;
                        }

                        JObject objT = result.obj as JObject;
                        List<RoomInfoEntity> list = objT["result"].ToObject<List<RoomInfoEntity>>();
                        flowLayoutPanel1.Controls.Clear();
                        flowLayoutPanel2.Controls.Clear();
                        lab_countNum_1.Text = lab_countNum.Text = "0";
                        foreach (var item in list)
                        {
                            Control.RoomPanelButton btn = new Control.RoomPanelButton();
                            if (item.isStop == "1")
                                btn.IsStop = true;
                            btn.noonName = item.periodTxt;
                            btn.BorderColor = System.Drawing.Color.Gray;
                            btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            btn.BtnFont = new System.Drawing.Font("微软雅黑", 18F);
                            btn.BtnText = item.doctorName;
                            btn.EnableCheck = false;
                            btn.FillColor1 = System.Drawing.Color.White;
                            btn.FillColor2 = System.Drawing.Color.White;
                            btn.RoomFont = new System.Drawing.Font("微软雅黑", 16F);
                            btn.RoomText = item.waitingNum + "  " + item.clinicName;//item.clinicPrefix + "  " + 
                            btn.SelctedColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
                            btn.Size = new System.Drawing.Size(195, 110);
                            btn.TipFont = new System.Drawing.Font("微软雅黑", 12F);
                            btn.Tag = new List<String>() { item.periodTxt, item.doctorId, item.periodTxt + item.doctorId, item.periodTxt, item.hospitalId, item.deptId,item.period };// item.period + item.doctorId;
                            btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.roomPanelButton3_MouseClick);
                            this.flowLayoutPanel1.Controls.Add(btn);
                        }
                        foreach (var item in list)
                        {
                            Control.RoomPanelButton btn = new Control.RoomPanelButton();
                            if (item.isStop == "1")
                                btn.IsStop = true;
                            btn.noonName = item.periodTxt;
                            btn.BorderColor = System.Drawing.Color.Gray;
                            btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            btn.BtnFont = new System.Drawing.Font("微软雅黑", 18F);
                            btn.BtnText = item.doctorName;
                            btn.EnableCheck = true;
                            btn.FillColor1 = System.Drawing.Color.White;
                            btn.FillColor2 = System.Drawing.Color.White;
                            btn.RoomFont = new System.Drawing.Font("微软雅黑", 16F);
                            btn.RoomText = item.waitingNum + "  " + item.clinicName;//item.clinicPrefix + "  " +  
                            btn.SelctedColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
                            btn.Size = new System.Drawing.Size(162, 110);
                            btn.TipFont = new System.Drawing.Font("微软雅黑", 12F);
                            btn.Tag = new List<String>() { item.periodTxt, item.doctorId, item.periodTxt + item.doctorId, item.periodTxt, item.hospitalId, item.deptId, item.period };// item.period + item.doctorId;
                            btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.roomPanelButtonWaiting_MouseClick);
                            List<String> prams = btn.Tag as List<String>;
                            if (SelectDoctorSchema != String.Empty && SelectDoctorSchema == prams[2])
                            {
                                btn.IsCheck = true;
                            }
                            this.flowLayoutPanel2.Controls.Add(btn);
                        }

                        lab_countNum.Text = lab_countNum_1.Text = list.Count().ToString();
                        cmd.HideOpaqueLayer();
                        //cmd = new Xr.Common.Controls.OpaqueCommand(this);
                        return;
                    }
                    #endregion
                    #region 患者信息
                    else if (workType == AsynchronousWorks.QueryID || workType == AsynchronousWorks.ReadzlCard)
                    {
                        if (result.obj == null)
                        {
                            //_waitForm.DialogResult = DialogResult.OK;
                            //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                            //_waitForm.Close();
                            cmd.HideOpaqueLayer();
                            return;
                        }
                        JObject objT = result.obj as JObject;//{"code":200,"message":"操作成功","result":{"age":"32","birthday":"1987-12-22","jkt":"003000005010","patientId":"000675493100","patientName":"李鹏真","phone":"17666476268","sbk":"11111111","sex":"男","sfz":"45032219871222151X","zlk":"02071196"},"state":true}
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {

                            lab_name.Text = objT["result"]["patientName"].ToString();
                            //lab_name.Text = objT["result"]["age"].ToString();
                            lab_birthday.Text = objT["result"]["birthday"].ToString();
                            lab_tel.Text = objT["result"]["phone"].ToString();
                            lab_sex.Text = objT["result"]["sex"].ToString();
                            switch (ReadType)
                            {
                                case "1":
                                    lab_Card.Text = objT["result"]["zlk"].ToString();
                                    break;
                                case "2":
                                    lab_Card.Text = objT["result"]["sbk"].ToString();
                                    break;
                                case "3":
                                    lab_Card.Text = objT["result"]["sfz"].ToString();
                                    break;
                                default:
                                    lab_Card.Text = objT["result"]["patientId"].ToString();
#region 
                                    //switch (lueCardType.EditValue.ToString())//卡类型：1患者ID、2诊疗卡、3社保卡、4身份证、5健康卡、6健康虚拟卡
                                    //{
                                    //    case "1":
                                    //        lab_Card.Text = objT["result"]["patientId"].ToString();
                                    //        break;
                                    //    case "2":
                                    //        lab_Card.Text = objT["result"]["zlk"].ToString();
                                    //        break;
                                    //    case "3":
                                    //        lab_Card.Text = objT["result"]["sbk"].ToString();
                                    //        break;
                                    //    case "4":
                                    //        lab_Card.Text = objT["result"]["sfz"].ToString();
                                    //        break;
                                    //    case "5":
                                    //        lab_Card.Text = objT["result"]["jkt"].ToString();
                                    //        break;
                                    //}
#endregion
                                    break;
                            }
                            //lab_sfz.Text = objT["result"]["sfz"].ToString();
                            //lab_zlk.Text = objT["result"]["zlk"].ToString();
                            //lab_jkt.Text = objT["result"]["jkt"].ToString();
                            //lab_sbk.Text = objT["result"]["sbk"].ToString();

                            Patientid = objT["result"]["patientId"].ToString();
                            //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                            //{"code":204,"message":"没有可以签到的预约记录","result":"","state":false}
                            //JObject objT1 = JObject.Parse(@"{""code"":200,""message"":""操作成功"",""result"":{""registerId"":9,""registerWay"":""0"",""cardType"":""1 "",""cardNo"":""02102337"",""status"":""0"",""statusTxt"":""待签到"",""triageId"":""""},""state"":true}");
                            NeedCloseWaitingFrm = false;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryPatientReservation });
                        }
                        else if (objT["message"].ToString() == "未匹配到患者信息")
                        {
                            cmd.HideOpaqueLayer();
                            
                            //NeedCloseWaitingFrm = false;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                            MessageBoxUtils.Hint("没有查询到基本信息，请去办卡", HintMessageBoxIcon.Error, this);
                        }
                        else
                        {
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                        }

                    }
                    #endregion
                    #region 患者预约信息
                    else if (workType == AsynchronousWorks.QueryPatientReservation)
                    {
                        if (result.obj == null)
                        {
                            //_waitForm.DialogResult = DialogResult.OK;
                            //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                            //_waitForm.Close();
                            cmd.HideOpaqueLayer();
                            return;
                        }
                        JObject objT = result.obj as JObject;

                        //JObject objT = JObject.Parse(@"{""code"":200,""message"":""操作成功"",""result"":{""registerId"":9,""registerWay"":""0"",""cardType"":""1 "",""cardNo"":""02102337"",""status"":""0"",""statusTxt"":""待签到"",""triageId"":""""},""state"":true}");
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            List<PatientReservationInfoEntity> list = objT["result"].ToObject<List<PatientReservationInfoEntity>>();
                            if (list.Count == 1)
                            {
                                /*lueRegisterWay.EditValue = objT["result"]["registerWay"] == null ? "" : objT["result"]["registerWay"].ToString();
                                lueCardType.EditValue = objT["result"]["cardType"] == null ? "" : objT["result"]["cardType"].ToString();
                                lab_cardNo.Text = objT["result"]["cardNo"] == null ? "" : objT["result"]["cardNo"].ToString();
                                lab_state.Text = objT["result"]["statusTxt"] == null ? "" : objT["result"]["statusTxt"].ToString();


                                BookingStatus = objT["result"]["status"] == null ? "" : objT["result"]["status"].ToString();
                                RegisterId = objT["result"]["registerId"] == null ? "" : objT["result"]["registerId"].ToString();
                                TriageId = objT["result"]["triageId"] == null ? "" : objT["result"]["triageId"].ToString();
                                 */
                                ReservationInfo_Panel.Visible = false;
                                panel6.Height = 130;

                                lueRegisterWay.EditValue = list[0].registerWay;
                                lueCardType.EditValue = list[0].cardType;
                                lab_cardNo.Text = list[0].cardNo;
                                lab_state.Text = list[0].statusTxt;


                                BookingStatus = list[0].status;
                                RegisterId = list[0].registerId;
                                TriageId = list[0].triageId;

                                cb_urgent.Enabled = false;

                                if (BookingStatus == "0") //未分诊，调用签到接口
                                {
                                    //workType = AsynchronousWorks.SingIn;
                                    NeedCloseWaitingFrm = false;
                                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.SingIn });
                                }
                                else if (BookingStatus == "2") //已在诊
                                {
                                    //清空界面信息
                                    ClearUIInfo();

                                    cmd.HideOpaqueLayer();
                                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                                    MessageBoxUtils.Hint("该患者已在诊", HintMessageBoxIcon.Error, this);
                                }
                                else if (BookingStatus == "3") //调用复诊签到接口
                                {
                                    //workType = AsynchronousWorks.ReviewSignIn;
                                    NeedCloseWaitingFrm = false;
                                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReviewSignIn, Argument = new String[] { TriageId } });
                                }
                                else if (BookingStatus == "1") //提示已签到，是否需要补打候诊单
                                {
                                    btn_more.Enabled = true;
                                    btn_print.Enabled = true;
                                    //MessageBoxUtils.Hint("该患者已签到");
                                }
                                else if (BookingStatus == "6") //该患者预约的医生已停诊，请选择其他医生签到
                                {
                                    btn_more.Enabled = false;
                                    btn_print.Enabled = false;
                                    //MessageBoxUtils.Hint("该患者预约的医生已停诊，请选择其他医生签到");

                                    cmd.HideOpaqueLayer();
                                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                                    MessageBoxUtils.Hint(objT["result"]["statusTxt"].ToString(), HintMessageBoxIcon.Error, MainForm);
                                }
                                else if (BookingStatus == "7") //该复诊患者的医生已停诊，请选择其他医生签到
                                {
                                    btn_more.Enabled = false;
                                    btn_print.Enabled = false;
                                    //MessageBoxUtils.Hint("该患者预约的医生已停诊，请选择其他医生签到");

                                    cmd.HideOpaqueLayer();
                                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                                    MessageBoxUtils.Hint(objT["result"]["statusTxt"].ToString(), HintMessageBoxIcon.Error, MainForm);
                                }
                                else //默认已就诊状态
                                {
                                    btn_more.Enabled = false;
                                    btn_print.Enabled = false;
                                    BookingStatus = "Finshed";

                                    cmd.HideOpaqueLayer();
                                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                                    MessageBoxUtils.Hint("该患者已就诊", MainForm);
                                }
                            }
                            else if (list.Count>1)//多条记录
                            {
                                RegisterRecordForm rr = new RegisterRecordForm();
                                rr.list = list;
                                rr.ShowDialog();
                                if (rr.DialogResult == DialogResult.Cancel)
                                {
                                    //刷新界面
                                    //清空界面信息
                                    ClearUIInfo();
                                    //更新诊室状态
                                    NeedCloseWaitingFrm = false;
                                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                                }
                                else if (rr.DialogResult == DialogResult.No)
                                {
                                    //停诊
                                    this.BookingStatus = "6";
                                    this.RegisterId = rr.registerId;
                                }
                                else if (rr.DialogResult == DialogResult.Retry)
                                {
                                    //复诊患者的医生已停诊，转诊
                                    this.BookingStatus = "7";
                                    this.RegisterId = rr.registerId;
                                }
                                
                                return;
                            }
                        }
                        else if (objT["message"].ToString() == "没有可以签到的记录")
                        {
                            lueRegisterWay.Enabled = true;
                            lueCardType.Enabled = true;
                            BookingStatus = "RegisterNow";
                            cb_urgent.Enabled = true;

                            cmd.HideOpaqueLayer();
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                            MessageBoxUtils.Hint("请选择医生为该患者现场挂号", MainForm);
                        }
                        else//发生错误
                        {
                            btn_more.Enabled = false;
                            btn_print.Enabled = false;
                            BookingStatus = "-1";
                            ClearUIInfo();
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                        }

                    }
                    #endregion
                    #region 签到/现场挂号/复诊签到（需要打印小票）
                    if (workType == AsynchronousWorks.SingIn || workType == AsynchronousWorks.Register || workType == AsynchronousWorks.ReviewSignIn)
                    {
                        if (result.obj == null)
                        {
                            //_waitForm.DialogResult = DialogResult.OK;
                            //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                            //_waitForm.Close();
                            return;
                        }

                        JObject objT = result.obj as JObject;
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            /*
                            lab_name.Text = objT["result"]["hospitalName"].ToString();
                            //lab_name.Text = objT["result"]["age"].ToString();
                            lab_birthday.Text = objT["result"]["deptName"].ToString();
                            lab_tel.Text = objT["result"]["clinicName"].ToString();
                            lab_sex.Text = objT["result"]["queueNum"].ToString();

                            lab_sfz.Text = objT["result"]["waitingNum"].ToString();
                            lab_zlk.Text = objT["result"]["currentTime"].ToString();
                            lab_jkt.Text = objT["result"]["tipMsg"].ToString();
                             */
                            //重新查询更新状态
                            //workType = AsynchronousWorks.QueryID;
                            //CardID = lab_cardNo.Text;
                            //NeedWaitingFrm = false;
                            //Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryID, Argument = new String[] { lab_cardNo.Text } });

 
                            //清空界面信息
                            ClearUIInfo();
                            //更新诊室状态
                            NeedCloseWaitingFrm = false;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                            String tipMsg = "";
                            if (objT["result"]["tipMsg"] != null && objT["result"]["tipMsg"].ToString().Length != 0)
                            {
                                tipMsg = objT["result"]["tipMsg"].ToString() + ",";
                            }
                            //打印小票
                            PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), objT["result"]["patientName"].ToString(), objT["result"]["waitingNum"].ToString(), objT["result"]["currentTime"].ToString(), objT["result"]["tipMsg"].ToString(), objT["result"]["doctorTip"].ToString(), objT["result"]["regVisitTime"].ToString());
                            string message = "";
                            if (!print.Print(ref message))
                            {
                                MessageBoxUtils.Show(tipMsg + "打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                            }
                            else
                            {
                                MessageBoxUtils.Hint(tipMsg + "打印小票完成", MainForm);
                            }
                            //MessageBoxUtils.Hint(result.msg, MainForm);
                        }
                        else
                        {
                            //MessageBoxUtils.Hint(objT["message"].ToString());
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                        }
                    }
                    #endregion
                    #region 读取身份证
                    else if (workType == AsynchronousWorks.ReadIdCard)
                    {
                        /*
                        _waitForm.DialogResult = DialogResult.OK;
                        //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                        _waitForm.Close();
                         */
                        //workType = AsynchronousWorks.QueryID;
                        if (!JLIdCardInfoClass.CancelFlag)
                        {
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryID, Argument = new String[] { result.obj.ToString(),"4" } });
                        }
                        else 
                        { 
                            cmd.HideOpaqueLayer(); 
                        }
                        //Asynchronous();
                    }
                    #endregion
                    #region 读取社保卡
                    else if (workType == AsynchronousWorks.ReadSocialcard)
                    {
                        //workType = AsynchronousWorks.QueryID;
                        Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryID, Argument = new String[] { result.obj.ToString(), "3" } });
                        //Asynchronous();
                    }
                    #endregion
                    #region 加急（需要打印小票）
                    if (workType == AsynchronousWorks.Urgent)
                    {
                        if (result.obj == null)
                        {
                            //_waitForm.DialogResult = DialogResult.OK;
                            //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                            //_waitForm.Close();
                            return;
                        }

                        JObject objT = result.obj as JObject;
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            //打印小票
                            String tipMsg = "";
                            if (objT["result"]["tipMsg"] != null && objT["result"]["tipMsg"].ToString().Length != 0)
                            {
                                tipMsg = objT["result"]["tipMsg"].ToString() + ",";
                            }
                            PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), objT["result"]["patientName"].ToString(), objT["result"]["waitingNum"].ToString(), objT["result"]["currentTime"].ToString(), objT["result"]["tipMsg"].ToString(), objT["result"]["doctorTip"].ToString(), objT["result"]["regVisitTime"].ToString());
                            string message = "";
                            if (!print.Print(ref message))
                            {
                                MessageBoxUtils.Show(tipMsg + "打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                            }
                            else
                            {
                                MessageBoxUtils.Hint(tipMsg + "打印小票完成", MainForm);
                            }
                            //刷新候诊患者列表
                            if (SelectDoctorid != String.Empty)
                            {
                                //WorkType = AsynchronousWorks.WaitingPatientList;
                                NeedCloseWaitingFrm = false;
                                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
                            }
                            //MessageBoxUtils.Hint(result.msg, MainForm);
                        }
                        else
                        {
                            //MessageBoxUtils.Hint(objT["message"].ToString());
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                        }
                    }
                    #endregion
                    #region 过号重排/取消候诊（不需要打印小票）
                    else if (workType == AsynchronousWorks.PassNum || workType == AsynchronousWorks.CancelWaiting)
                    {
                        if (result.obj == null)
                        {
                            cmd.HideOpaqueLayer();
                            return;
                        }
                        JObject objT = result.obj as JObject;
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            //MessageBoxUtils.Hint(result.msg, MainForm);
                            //刷新候诊患者列表
                            if (SelectDoctorid != String.Empty)
                            {
                                //WorkType = AsynchronousWorks.WaitingPatientList;
                                NeedCloseWaitingFrm = false;
                                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList, Argument = new String[] { "Refresh" } });
                            }
                        }
                        else
                        {
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                        }
                    }
                    #endregion
                    #region 候诊指引单
                    else if (workType == AsynchronousWorks.WaitingList)
                    {
                        if (result.obj == null)
                        {
                            //_waitForm.DialogResult = DialogResult.OK;
                            //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                            //_waitForm.Close();
                            cmd.HideOpaqueLayer();
                            return;
                        }

                        JObject objT = result.obj as JObject;
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            //lab_name.Text = objT["result"]["hospitalName"].ToString();
                            ////lab_name.Text = objT["result"]["age"].ToString();
                            //lab_birthday.Text = objT["result"]["deptName"].ToString();
                            //lab_tel.Text = objT["result"]["clinicName"].ToString();
                            //lab_sex.Text = objT["result"]["queueNum"].ToString();

                            //lab_sfz.Text = objT["result"]["waitingNum"].ToString();
                            //lab_zlk.Text = objT["result"]["currentTime"].ToString();
                            //lab_jkt.Text = objT["result"]["tipMsg"].ToString();
                            String tipMsg = "";
                            if (objT["result"]["tipMsg"] != null && objT["result"]["tipMsg"].ToString().Length != 0)
                            {
                                tipMsg = objT["result"]["tipMsg"].ToString() + ",";
                            }
                            //打印小票
                            PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), objT["result"]["patientName"].ToString(), objT["result"]["waitingNum"].ToString(), objT["result"]["currentTime"].ToString(), objT["result"]["tipMsg"].ToString(), objT["result"]["doctorTip"].ToString(), objT["result"]["regVisitTime"].ToString());
                            string message = "";
                            if (!print.Print(ref message))
                            {
                                MessageBoxUtils.Show(tipMsg + "打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                            }
                            else
                            {
                                MessageBoxUtils.Hint(tipMsg + "打印小票完成", MainForm);
                            }
                        }
                        else
                        {
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                        }
                        NeedCloseWaitingFrm = true;
                        //MessageBoxUtils.Hint("打印小票完成", MainForm);
                    }
                    #endregion
                    #region 候诊患者列表
                    if (workType == AsynchronousWorks.WaitingPatientList)
                    {
                        if (result.obj == null)
                        {
                            //_waitForm.DialogResult = DialogResult.OK;
                            //_waitForm.ChangeNoticeComplete(result.msg, Dialog.HintMessageBoxIcon.Error);
                            //_waitForm.Close();
                            cmd.HideOpaqueLayer();
                            return;
                        }

                        JObject objT = result.obj as JObject;
                        String[] args = result.args as String[];
                        List<PatientQueueEntity> list = objT["result"].ToObject<List<PatientQueueEntity>>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].isShow!="true")
                            {
                                list[i].regVisitTime = "";
                            }
                        }
                        gc_Patients.DataSource = list;
                        if (args.Length > 0)
                        {
                            NeedCloseWaitingFrm = false;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
                        }
                        else
                        {
                            cmd.HideOpaqueLayer();
                        }
                        //跳转候诊列表
                        xtraTabControl1.SelectedTabPage = xtraTabPage2;
                        //cmd = new Xr.Common.Controls.OpaqueCommand(this);
                        return;
                    }
                    #endregion
                }
                else
                {
                    if (result.msg == "没有查询到基本信息，请去办卡")
                    {
                        MessageBoxUtils.Hint(result.msg,HintMessageBoxIcon.Error, this);
                    }
                    else
                    {
                        MessageBoxUtils.Show(result.msg, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                    }
                    cmd.HideOpaqueLayer();
                    //MessageBoxUtils.Hint(result.msg);
                }
            }
            catch (Exception ex)
            {
                cmd.HideOpaqueLayer();
                if (ex.Message == "操作被取消。")
                    MessageBoxUtils.Hint(ex.Message, HintMessageBoxIcon.Error, MainForm);
                else
                    MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MainForm);
                cmd.HideOpaqueLayer();
            }
            finally
            {
                // 关闭加载提示框
                //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                //cmd.HideOpaqueLayer();
                //关闭等待框
                if (NeedCloseWaitingFrm)
                {
                    cmd.HideOpaqueLayer();
                }
                else
                {
                    NeedCloseWaitingFrm = true;
                }
            }
        }

        new void Disposed(object sender, object e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!cmd.waitingBox.Visible)
            {
                //开始取消 
                if (backgroundWorker1.IsBusy) //是否在运行异步操作 
                {
                    backgroundWorker1.CancelAsync(); //(是)提交取消命令 
                    if (timer1.Enabled)
                    {
                        timer1.Stop();
                    }
                }
            }
        }
        void ClearUIInfo()
        {
            lab_name.Text = String.Empty;
            //lab_name.Text = objT["result"]["age"].ToString();
            lab_birthday.Text = String.Empty;
            lab_tel.Text = String.Empty;
            lab_sex.Text = String.Empty;

            lab_Card.Text = String.Empty;
            //lab_zlk.Text = String.Empty;
            //lab_jkt.Text = String.Empty;
            //lab_sbk.Text = String.Empty;

            lueRegisterWay.ItemIndex = 0;
            lueRegisterWay.Enabled = false;
            lueCardType.ItemIndex = 0;
            lueCardType.Enabled = false;
            lab_cardNo.Text = String.Empty;
            lab_state.Text = String.Empty;

            BookingStatus = String.Empty;
            RegisterId = String.Empty;
            TriageId = String.Empty;
            Doctorid = String.Empty;
            Patientid = String.Empty;

            btn_more.Enabled = false;
            btn_print.Enabled = false;
            cb_urgent.Checked = false;
            cb_urgent.Enabled = false;
        }

        private void rBtn_Click(object sender, EventArgs e)
        {
            if (rBtn_noon.IsCheck )
            {
                rBtn_noon.IsCheck = true;
                rBtn_noon_1.IsCheck = true;
                Period = "0";
            }
            else if (rBtn_afternoon.IsCheck)
            {
                rBtn_afternoon.IsCheck = true;
                rBtn_afternoon_1.IsCheck = true;
                Period = "1"; 
            }
            else if (rBtn_night.IsCheck)
            {
                rBtn_night.IsCheck = true;
                rBtn_night_1.IsCheck = true;
                Period = "2"; 
            }
            else
            {
                rBtn_allDay.IsCheck = true;
                rBtn_allDay_1.IsCheck = true;
                Period = "3";
            }
            SelectDoctorSchema = String.Empty;
            SelectDoctorid = String.Empty;
            SelectDoctorDepid = String.Empty;
            PeriodWaiting = String.Empty;
            gc_Patients.DataSource = null;
            //WorkType = AsynchronousWorks.RoomListQuery;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
        }
        private void rBtn_1_Click(object sender, EventArgs e)
        {
            if ( rBtn_noon_1.IsCheck)
            {
                rBtn_noon.IsCheck = true;
                rBtn_noon_1.IsCheck = true;
                Period = "0";
            }
            else if (rBtn_afternoon_1.IsCheck)
            {
                rBtn_afternoon.IsCheck = true;
                rBtn_afternoon_1.IsCheck = true;
                Period = "1";
            }
            else if (rBtn_night_1.IsCheck)
            {
                rBtn_night.IsCheck = true;
                rBtn_night_1.IsCheck = true;
                Period = "2";
            }
            else
            {
                rBtn_allDay.IsCheck = true;
                rBtn_allDay_1.IsCheck = true;
                Period = "3";
            }
            SelectDoctorSchema = String.Empty;
            SelectDoctorid = String.Empty;
            SelectDoctorDepid = String.Empty;
            PeriodWaiting = String.Empty;
            gc_Patients.DataSource = null;
            //WorkType = AsynchronousWorks.RoomListQuery;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
        }
        private void rBtn_PatientListStatus_Click(object sender, EventArgs e)
        {
            GetPatientListStatus();
            if (SelectDoctorid != String.Empty)
            {
                //WorkType = AsynchronousWorks.WaitingPatientList;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
            }
        }
        private void GetPatientListStatus()
        {
            if (rBtn_allPatient.IsCheck)
            {
                PatientListStatus = "3";
            }
            else if (rBtn_reservationPatient.IsCheck)
            {
                PatientListStatus = "0";
            }
            else if (rBtn_waittingPatient.IsCheck)
            {
                PatientListStatus = "1";
            }
            else
            {
                PatientListStatus = "2";
            }
        }
        private void lueDept_EditValueChanged(object sender, EventArgs e)
        {
            //WorkType = AsynchronousWorks.RoomListQuery;
            //Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            //WorkType = AsynchronousWorks.RoomListQuery;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
        }

        private void btn_more_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btn_more,10,10);
        }

        private void roomPanelButton3_MouseClick(object sender, MouseEventArgs e)
        {
            Control.RoomPanelButton btn = sender as Control.RoomPanelButton;
            List<String> prams = btn.Tag as List<String>;
            if (e.Button == MouseButtons.Left)
            {
                if (btn.IsStop) 
                {
                    MessageBoxUtils.Hint("该医生已停诊，请选择其他医生", HintMessageBoxIcon.Error, MainForm);
                }
                else
                {
                    Doctorid = prams[1];
                    SelectDoctorid = prams[1];
                    SelectDoctorSchema = prams[2];
                    SelectDoctorDepid = prams[5];
                    //现场挂号或选择该医生候诊
                    //contextMenuStrip1.Show(btn,e.Location.X,e.Location.Y);
                    if (BookingStatus == "6")//预约医生停诊后签到其他医生
                    {
                        //WorkType = AsynchronousWorks.SingIn;
                        Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.SingIn });
                    }
                    else if (BookingStatus == "7")//复诊医生停诊换医生
                    {
                        if (MessageBoxUtils.Show("确定为该复诊患者签到" + btn.BtnText + "的号吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
                        {
                            //WorkType = AsynchronousWorks.Register;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReviewSignIn, Argument = new String[] { TriageId, prams[1] } });
                        }
                    }
                    else if (BookingStatus == "RegisterNow")//选择医生为该患者现场挂号
                    {
                        if (MessageBoxUtils.Show("确定为该患者现场挂" + btn.BtnText + "的号吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
                        {
                            //WorkType = AsynchronousWorks.Register;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.Register, Argument = new String[] { prams[4], prams[5], prams[6] } });
                        }
                    }

                    else if (BookingStatus == "Finshed")
                    {
                        MessageBoxUtils.Hint("该患者已完成分诊，无须现场挂号", HintMessageBoxIcon.Error, MainForm);
                    }
                    else if (BookingStatus == "1")
                    {
                        MessageBoxUtils.Hint("该患者已在分诊队列中", HintMessageBoxIcon.Error, MainForm);
                    }
                    else if (BookingStatus == "-1")
                    {
                        MessageBoxUtils.Hint("请重新读取患者信息后操作", HintMessageBoxIcon.Error, MainForm);
                    }
                    else
                    {
                        MessageBoxUtils.Hint("请读取患者信息后操作", HintMessageBoxIcon.Error, MainForm);
                    }
                }
            }
            if (e.Button == MouseButtons.Right)//跳转候诊列表
            {
                Doctorid = prams[1];
                SelectDoctorid = prams[1];
                SelectDoctorSchema = prams[2];
                PeriodWaiting = GetNoonCode(prams[3]);
                SelectDoctorDepid = prams[5];
                /*foreach (var ctl in flowLayoutPanel2.Controls)
                {
                    Control.RoomPanelButton btn1 = ctl as Control.RoomPanelButton;
                    List<String> prams1 = btn1.Tag as List<String>;
                    if (prams1[2] == prams[2])
                    {
                        btn1.IsCheck = true;
                        break;
                    }
                }
                 */
                //更新候诊列表并跳转
                //WorkType = AsynchronousWorks.WaitingPatientList;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
                //xtraTabControl1.SelectedTabPage = xtraTabPage2;
            }
        }
        private void roomPanelButtonWaiting_MouseClick(object sender, MouseEventArgs e)
        {
            Control.RoomPanelButton btn = sender as Control.RoomPanelButton;
            List<String> prams = btn.Tag as List<String>;
            SelectDoctorid = prams[1];
            SelectDoctorSchema = prams[2];
            PeriodWaiting = GetNoonCode(prams[3]);
            SelectDoctorDepid = prams[5];
            //if (e.Button == MouseButtons.Left)
            //{
                //更新候诊列表
                //WorkType = AsynchronousWorks.WaitingPatientList;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
            //}
        }
        //患者列表右键点击事件
        private void gv_patients_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标右键点击
            System.Threading.Thread.Sleep(10);
            if (e.Button == MouseButtons.Right)
            {

                //GridHitInfo gridHitInfo = gridView.CalcHitInfo(e.X, e.Y);
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo gridHitInfo = gv_Patients.CalcHitInfo(e.X, e.Y);

                if (gridHitInfo.Column != null)
                {
                    if (!gridHitInfo.InColumnPanel)//判断是否在单元格内
                    {
                        
                        int i = gridHitInfo.RowHandle;
                        PatientQueueEntity obj=gv_Patients.GetRow(i) as PatientQueueEntity;
                        TriageIdInList = obj.triageId;
                        contextMenuStrip_waitingList.Show(gc_Patients, e.Location);
                    }
                }
            }
        }

        private void SignInForm_Resize(object sender, EventArgs e)
        {
            //cmd = new Xr.Common.Controls.OpaqueCommand(this);
            cmd.rectDisplay = this.DisplayRectangle;
        }

        private void 加急ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者加急吗?", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.Urgent;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.Urgent, Argument = new String[] { TriageId } });
            }
        }

        private void 排号作废ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者作废排号吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.CancelWaiting;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.CancelWaiting, Argument = new String[] { TriageId } });
            }
        }

        private void 过号重排ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者过号重排吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.PassNum;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.PassNum, Argument = new String[] { TriageId } });
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

            if (MessageBoxUtils.Show("确定为该患者重打指引单吗?", MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.WaitingList;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingList, Argument = new String[] { TriageId } });
            }
        }

        private void btn_refreshPatient_Click(object sender, EventArgs e)
        {
            if (SelectDoctorid != String.Empty)
            {
                //WorkType = AsynchronousWorks.WaitingPatientList;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
            }
        }
        private void 加急ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者加急吗?", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.Urgent;
                if (TriageIdInList != null && TriageIdInList != String.Empty)
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.Urgent, Argument = new String[] { TriageIdInList } });
                else
                    MessageBoxUtils.Hint("该患者尚未分诊", HintMessageBoxIcon.Error, MainForm);
            }
        }

        private void 排号作废ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者作废排号吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.CancelWaiting;
                if (TriageIdInList != null && TriageIdInList != String.Empty)
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.CancelWaiting, Argument = new String[] { TriageIdInList } });
                else
                    MessageBoxUtils.Hint("该患者尚未分诊", HintMessageBoxIcon.Error, MainForm);
            }
        }

        private void 过号重排ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者过号重排吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MainForm) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.PassNum;
                if (TriageIdInList != null && TriageIdInList != String.Empty)
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.PassNum, Argument = new String[] { TriageIdInList } });
                else
                    MessageBoxUtils.Hint("该患者尚未分诊", HintMessageBoxIcon.Error, MainForm);
            }
        }
        /// <summary>
        /// 候诊列表日期变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void de_date_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_tranDoc_Click(object sender, EventArgs e)
        {
            TranDocFrm frm = new TranDocFrm(lueDept.EditValue.ToString());
            frm.ShowDialog();
        }
        /// <summary>
        /// 切换Tab更新诊室状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //更新诊室状态
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
        }

        /*
        private void btn_query_Click(object sender, EventArgs e)
        {
            //numType 1：身份证 2：社保卡 3：诊疗卡 4:住院号 5：床位号
            if (cbID.Text != String.Empty)
            {
                WorkType = AsynchronousWorks.QueryID;
                Asynchronous();
            }
        }
         */
        #region 根据系统时间设置当前午别
        /// <summary>
        /// 根据系统时间设置当前午别
        /// </summary>
        private void SetNoonCode()
        {
            string url = AppContext.AppConfig.serverUrl + "sch/registerTriage/getCurrentPeriod";
            //{"code": 200,"message": "操作成功","result": {"period": "2"},"state": true}
            string jsonStr = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(jsonStr);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                Period = objT["result"]["period"].ToString();
                if (Period =="0")
                {
                    rBtn_noon.IsCheck = true;
                    rBtn_noon_1.IsCheck = true;
                }
                else if (Period == "1")
                {
                    rBtn_afternoon.IsCheck = true;
                    rBtn_afternoon_1.IsCheck = true;
                }
                else if (Period == "2")
                {
                    rBtn_night.IsCheck = true;
                    rBtn_night_1.IsCheck = true;
                }
            }
            else
            {
                int hour = System.DateTime.Now.Hour;
                if (hour <= 7)
                {
                    rBtn_night.IsCheck = true;
                    rBtn_night_1.IsCheck = true;
                    Period = "2";
                }
                else if (hour > 7 && hour <= 13)
                {
                    rBtn_noon.IsCheck = true;
                    rBtn_noon_1.IsCheck = true;
                    Period = "0";
                }
                else if (hour > 13 && hour <= 18)
                {
                    rBtn_afternoon.IsCheck = true;
                    rBtn_afternoon_1.IsCheck = true;
                    Period = "1";
                }
                else
                {
                    rBtn_night.IsCheck = true;
                    rBtn_night_1.IsCheck = true;
                    Period = "2";
                }
            }
        }
        /// <summary>
        /// 根据午别设置午别代号
        /// </summary>
        /// <returns>0上午 1下午 2晚上 3全天</returns>
        private String GetNoonCode(String noon)
        {
            if (noon == "上午")
            {
                return "0";
            }
            else if (noon == "下午")
            {
                return "1";
            }
            else if (noon == "晚上")
            {
                return "2";
            }
            else//全天
            {
                return "3";
            }
        }
        #endregion


        Point downPoint;
        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReservationInfo_Panel.Location = new Point(ReservationInfo_Panel.Location.X + e.X - downPoint.X,
                    ReservationInfo_Panel.Location.Y + e.Y - downPoint.Y);
            }
        }
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }
        /// <summary>
        /// 多条预约记录时调整尺寸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel6_Resize(object sender, EventArgs e)
        {
            ReservationInfo_Panel.Size = groupBorderPanel2.Size;
        }


    }
    /// <summary>
    ///  预约信息实体
    /// </summary>
    public class RoomInfoEntity
    {
        /// <summary>
        /// 所属医院ID
        /// </summary>
        public String hospitalId { get; set; }
        /// <summary>
        /// 所属科室ID
        /// </summary>
        public String deptId { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public String doctorId { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public String doctorName { get; set; }
        /// <summary>
        /// 午别
        /// </summary>
        public String periodTxt { get; set; }
        /// <summary>
        /// 午别代码
        /// </summary>
        public String period { get; set; }
        /// <summary>
        /// 诊室代码
        /// </summary>
        public String clinicPrefix { get; set; }
        /// <summary>
        /// 诊室名称
        /// </summary>
        public String clinicName { get; set; }
        /// <summary>
        /// 候诊人数 
        /// </summary>
        public String waitingNum { get; set; }
        /// <summary>
        /// 签到人数 
        /// </summary>
        public String siteSyNum { get; set; }
        /// <summary>
        /// 停诊标志 （0:开诊1:停诊）
        /// </summary>
        public String isStop { get; set; }
       
    }

     /// <summary>
    ///  患者预约列表实体
    /// </summary>
    public class PatientReservationInfoEntity
    {
        /// <summary>
        /// 预约主键
        /// </summary>
        public String registerId { get; set; }

        /// <summary>
        /// 预约方式
        /// </summary>
        public String registerWay { get; set; }
        /// <summary>
        /// 卡类别
        /// </summary>
        public String cardType { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public String cardNo { get; set; }

        /// <summary>
        /// 预约状态代码
        /// </summary>
        public String status { get; set; }
        /// <summary>
        /// 预约状态
        /// </summary>
        public String statusTxt { get; set; }
        /// <summary>
        /// 分诊ID
        /// </summary>
        public String triageId { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public String deptName { get; set; }
        /// <summary>
        /// 医生id
        /// </summary>
        public String doctorId { get; set; }
        /// <summary>
        /// 医生名称
        /// </summary>
        public String doctorName { get; set; }
        /// <summary>
        /// 患者名称
        /// </summary>
        public String patientName { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public String registerTime { get; set; }
    }

    /// <summary>
    ///  患者列表实体
    /// </summary>
    public class PatientQueueEntity
    {
        /// <summary>
        /// 队列号：分诊表的triage_type+seq,如Y001、X001
        /// </summary>
        public String queueNum { get; set; }

        /// <summary>
        /// 分诊记录主键
        /// </summary>
        public String triageId { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public String patientName { get; set; }
                /// <summary>
        /// 患者性别
        /// </summary>
        public String sex { get; set; }
        
        /// <summary>
        /// 卡类型
        /// </summary>
        public String cardType { get; set; }
        /// <summary>
        /// 卡类型TxT
        /// </summary>
        public String cardTypeTxt { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public String cardNo { get; set; }
        /// <summary>
        /// 预约途径
        /// </summary>
        public String registerWay { get; set; }
        /// <summary>
        /// 预约途径TxT
        /// </summary>
        public String registerWayTxt { get; set; }
        /// <summary>
        /// 状态 
        /// </summary>
        public String status { get; set; }
        /// <summary>
        /// 预约就诊时段，beginTime-endTime
        /// </summary>
        public String regVisitTime { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public String triageTime { get; set; }
        /// <summary>
        /// 就诊时间
        /// </summary>
        public String visitTime { get; set; }
        public String isShow { get; set; }
    }
}
