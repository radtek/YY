using System;
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
        bool NeedWaitingFrm = true;
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
            xtraTabControl1.SelectedTabPage.ResetBackColor();
            xtraTabControl1.SelectedTabPage.BackColor = Color.Transparent;
            cmd = new Xr.Common.Controls.OpaqueCommand(AppContext.Session.waitControl);
            getLuesInfo();
            //WorkType = AsynchronousWorks.RoomListQuery;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
        }
        /// <summary>
        /// 下拉框数据
        /// </summary>
        void getLuesInfo()
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
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
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
                MessageBox.Show(objT["message"].ToString());
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
        private void btn_zlk_Click(object sender, EventArgs e)
        {
            //ClearUIInfo();
            //WorkType = AsynchronousWorks.ReadzlCard;
            //cmd.IsShowCancelBtn = false;
            //cmd.ShowOpaqueLayer();
            CardID = "000675493100";
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReadzlCard, Argument = new String[] { CardID } });
        }
        private void btn_readSocialcard_Click(object sender, EventArgs e)
        {
            
            //WorkType = AsynchronousWorks.ReadSocialcard;
            timer1.Start();
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReadSocialcard});
        }

        private void btn_readIdcard_Click(object sender, EventArgs e)
        {
            //WorkType = AsynchronousWorks.ReadIdCard;
            timer1.Start();
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
        private String PatientListStatus = "3";
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
                    cmd.ShowOpaqueLayer();
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
                     if (NeedWaitingFrm)
                    {
                        cmd.IsShowCancelBtn = false;
                        cmd.ShowOpaqueLayer();
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
                    prament.Add("deptId", lueDept.EditValue.ToString());
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
                    //prament.Add("cardNo", CardID);
                    prament.Add("cardNo", Pras[0]);
                    
                    //prament.Add("pageSize", "10000");

                    String url = String.Empty;
                    if (prament.Count != 0)
                    {
                        param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    url = AppContext.AppConfig.serverUrl + "patmi/findPatMiByCradNo?" + param;
                    String jsonStr = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(jsonStr);
                    List<JObject> objTs = new List<JObject>();
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        objTs.Add(objT);
                        Patientid = objT["result"]["patientId"].ToString();

                        //hospitalId=12&deptId=2&patientId=000675493100
                        prament.Add("hospitalId", AppContext.Session.hospitalId);
                        prament.Add("deptId", lueDept.EditValue.ToString());
                        prament.Add("patientId", Pras[0]);
                        //prament.Add("patientId", CardID);
                        //prament.Add("pageSize", "10000");

                        if (prament.Count != 0)
                        {
                            param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                        }
                        url = AppContext.AppConfig.serverUrl + "sch/doctorScheduRegister/patCurrentRegsiter?" + param;
                        //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                        jsonStr = HttpClass.httpPost(url);//{"code":204,"message":"没有可以签到的预约记录","result":"","state":false}
                        JObject objT2 = JObject.Parse(jsonStr);
                        objTs.Add(objT2);

                        result.obj = objTs;
                        result.result = true;
                        //result.msg = "成功";
                        result.msg = objT["message"].ToString();
                        e.Result = result;
                    }
                    else
                    {
                        result.result = false;
                        if (objT["message"].ToString() == "未匹配到患者信息")
                        {
                            result.msg = "没有查询到基本信息，请去办卡";
                        }
                        else
                        {
                            result.msg = objT["message"].ToString();// PatientSearchInfoRef.Msg;
                        }
                        e.Result = result;
                    }
                    CardID = String.Empty;
                }
                else //输入为空时不查询
                {
                    result.obj = null;
                    result.result = true;
                    result.msg = "成功";
                    e.Result = result;
                }
                NeedWaitingFrm = true;
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
                prament.Add("hospitalId", AppContext.Session.hospitalId);
                prament.Add("deptId", lueDept.EditValue.ToString());
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
                prament.Add("triageId", Pras[0]);
                //prament.Add("triageId", TriageId);
                //prament.Add("pageSize", "10000");

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
                prament.Add("deptId", lueDept.EditValue.ToString());
                prament.Add("doctorId", SelectDoctorid);
                prament.Add("workDate", de_date.Text);
                prament.Add("period", Period);
                prament.Add("status",PatientListStatus);
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
        private delegate SycResult ReadCardDelegate(string parm);  //创建一个委托 
        /// <summary> 
        /// 异步委托的方法 
        /// </summary> 
        /// <param name="sql"></param> 
        /// <returns></returns> 
        private SycResult ReturnReadCardData(string parm)
        {

            SycResult Result = new SycResult() { result = true };
            NeedWaitingFrm = false;
            //System.Threading.Thread.Sleep(10000);
            if (parm == "IdCard")
            {
                System.Threading.Thread.Sleep(3000);
                if (backgroundWorker1.IsBusy)
                {
                    CardID = "000675493100";
                    Result.WorkType = AsynchronousWorks.ReadIdCard;
                    Result.obj = CardID;
                }

                /*JLIdCardInfoClass idCardInfo = JLIdCardInfoClass.getCardInfo();
                if (idCardInfo != null)
                {
                 if(backgroundWorker1.IsBusy)
                    CardID = idCardInfo.Code.ToString();
                }
                if (CardID != String.Empty)
                {
                    //patientId = carMes.user_id;
                    Result.obj = CardID;
                    LogClass.WriteLog("读取身份证成功，身份证号：" + IDCardID);
                }
                 */
                //result.obj = null;
                //result.result = true;
                //result.msg = "成功
            }
            else
            {
                System.Threading.Thread.Sleep(3000);
                if (backgroundWorker1.IsBusy)
                {
                    CardID = "000675493100";
                    Result.WorkType = AsynchronousWorks.ReadSocialcard;
                    Result.obj = CardID;
                }
                /*
                while (!CancelFlag)
                {
                    SocialCard carMes = new SocialCard();
                    carMes.readCard();
                    if (carMes.message_type == "1")
                    {
                        CancelFlag = true;
                        //patientId = carMes.user_id;
                        LogClass.WriteLog("读取社保卡成功，卡号：" + carMes.user_id);
                        if(backgroundWorker1.IsBusy)
                          {
                            CardID = carMes.user_id;
                            Result.obj = CardID;
                          }
                    }
                }
                 */
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
                    MessageBoxUtils.Show("操作失败，请稍候尝试。", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                        lab_countNum_1.Text=lab_countNum.Text = "0";
                        foreach (var item in list)
                        {
                            Control.RoomPanelButton btn = new Control.RoomPanelButton();
                            if (item.isStop == "1")
                                btn.Enabled = false;
                            btn.noonName = item.period;
                            btn.BorderColor = System.Drawing.Color.Aqua;
                            btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            btn.BtnFont = new System.Drawing.Font("微软雅黑", 18F);
                            btn.BtnText = item.doctorName;
                            btn.EnableCheck = false;
                            btn.FillColor1 = System.Drawing.Color.White;
                            btn.FillColor2 = System.Drawing.Color.White;
                            btn.RoomFont = new System.Drawing.Font("微软雅黑", 16F);
                            btn.RoomText = item.clinicPrefix + "  " + item.waitingNum + "  " + item.clinicName;
                            btn.SelctedColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
                            btn.Size = new System.Drawing.Size(260, 128);
                            btn.TipFont = new System.Drawing.Font("微软雅黑", 12F);
                            btn.Tag = new List<String>() { item.period, item.doctorId, item.period + item.doctorId };// item.period + item.doctorId;
                            btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.roomPanelButton3_MouseClick);
                            this.flowLayoutPanel1.Controls.Add(btn);
                        }
                        foreach (var item in list)
                        {
                            Control.RoomPanelButton btn = new Control.RoomPanelButton();
                            if (item.isStop == "1")
                                btn.Enabled = false;
                            btn.noonName = item.period;
                            btn.BorderColor = System.Drawing.Color.Aqua;
                            btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            btn.BtnFont = new System.Drawing.Font("微软雅黑", 18F);
                            btn.BtnText = item.doctorName;
                            btn.EnableCheck = true;
                            btn.FillColor1 = System.Drawing.Color.White;
                            btn.FillColor2 = System.Drawing.Color.White;
                            btn.RoomFont = new System.Drawing.Font("微软雅黑", 16F);
                            btn.RoomText = item.clinicPrefix + "  " + item.waitingNum + "  " + item.clinicName;
                            btn.SelctedColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
                            btn.Size = new System.Drawing.Size(162, 110);
                            btn.TipFont = new System.Drawing.Font("微软雅黑", 12F);
                            btn.Tag = new List<String>() { item.period, item.doctorId, item.period + item.doctorId };// item.period + item.doctorId;
                            btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.roomPanelButtonWaiting_MouseClick);
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
                        List<JObject> objTs = result.obj as List<JObject>;//{"code":200,"message":"操作成功","result":{"age":"32","birthday":"1987-12-22","jkt":"003000005010","patientId":"000675493100","patientName":"李鹏真","phone":"17666476268","sbk":"11111111","sex":"男","sfz":"45032219871222151X","zlk":"02071196"},"state":true}
                        JObject objT = objTs[0];
                        lab_name.Text = objT["result"]["patientName"].ToString();
                        //lab_name.Text = objT["result"]["age"].ToString();
                        lab_birthday.Text = objT["result"]["birthday"].ToString();
                        lab_tel.Text = objT["result"]["phone"].ToString();
                        lab_sex.Text = objT["result"]["sex"].ToString();

                        lab_sfz.Text = objT["result"]["sfz"].ToString();
                        lab_zlk.Text = objT["result"]["zlk"].ToString();
                        lab_jkt.Text = objT["result"]["jkt"].ToString();
                        lab_sbk.Text = objT["result"]["sbk"].ToString();
                        //{"code":200,"message":"操作成功","result":{"registerId":9,"registerWay":"0","cardType":"1 ","cardNo":"02102337","status":"0","statusTxt":"待签到","triageId":""},"state":true}
                        //{"code":204,"message":"没有可以签到的预约记录","result":"","state":false}
                        JObject objT1 = objTs[1];
                        //JObject objT1 = JObject.Parse(@"{""code"":200,""message"":""操作成功"",""result"":{""registerId"":9,""registerWay"":""0"",""cardType"":""1 "",""cardNo"":""02102337"",""status"":""0"",""statusTxt"":""待签到"",""triageId"":""""},""state"":true}");
                        if (string.Compare(objT1["state"].ToString(), "true", true) == 0)
                        {
                            lueRegisterWay.EditValue = objT1["result"]["registerWay"].ToString();
                            lueCardType.EditValue = objT1["result"]["cardType"].ToString();
                            lab_cardNo.Text = objT1["result"]["cardNo"].ToString();
                            lab_state.Text = objT1["result"]["statusTxt"].ToString();
                            BookingStatus = objT1["result"]["status"].ToString();
                            RegisterId = objT1["result"]["registerId"].ToString();
                            TriageId = objT1["result"]["triageId"].ToString();
                            cb_urgent.Enabled = false;

                            if (BookingStatus == "0") //未分诊，调用签到接口
                            {
                                //workType = AsynchronousWorks.SingIn;
                                NeedWaitingFrm = false;
                                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.SingIn });
                            }
                            if (BookingStatus == "3") //调用复诊签到接口
                            {
                                //workType = AsynchronousWorks.ReviewSignIn;
                                NeedWaitingFrm = false;
                                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.ReviewSignIn });
                            }
                            if (BookingStatus == "1") //提示已签到，是否需要补打候诊单
                            {
                                btn_more.Enabled = true;
                                btn_print.Enabled = true;
                                //MessageBoxUtils.Hint("该患者已签到");
                            }
                            if (BookingStatus == "6") //该患者预约的医生已停诊，请选择其他医生签到
                            {
                                btn_more.Enabled = false;
                                btn_print.Enabled = false;
                                //MessageBoxUtils.Hint("该患者预约的医生已停诊，请选择其他医生签到");
                                MessageBoxUtils.Hint(objT1["result"]["statusTxt"].ToString());
                            }

                            //NeedWaitingFrm = false;
                        }
                        else
                        {
                            lueRegisterWay.Enabled = true;
                            lueCardType.Enabled = true;
                            BookingStatus = "RegisterNow";
                            cb_urgent.Enabled = true;
                            MessageBoxUtils.Hint("请选择医生为该患者现场挂号");
                        }

                    }
                    #endregion
                    #region 签到/现场挂号/加急/复诊签到（需要打印小票）
                    if (workType == AsynchronousWorks.SingIn || workType == AsynchronousWorks.Register || workType == AsynchronousWorks.Urgent || workType == AsynchronousWorks.ReviewSignIn)
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
                            NeedWaitingFrm = false;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryID, Argument = new String[] { lab_cardNo.Text } });
                            //打印小票
                            /*PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), objT["result"]["waitingNum"].ToString(), lab_zlk.Text = objT["result"]["currentTime"].ToString());
                            string message = "";
                            if (!print.Print(ref message))
                            {
                                MessageBoxUtils.Show("打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                MessageBoxUtils.Hint("打印小票完成");
                            }
                             */
                            MessageBoxUtils.Hint(result.msg);
                        }
                        else
                        {
                            //MessageBoxUtils.Hint(objT["message"].ToString());
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                        Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryID, Argument = new String[] { result.obj.ToString() }});
                        //Asynchronous();
                    }
                    #endregion
                    #region 读取社保卡
                    else if (workType == AsynchronousWorks.ReadSocialcard)
                    {
                        //workType = AsynchronousWorks.QueryID;
                        Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryID, Argument = new String[] { result.obj.ToString() }});
                        //Asynchronous();
                    }
                    #endregion
                    #region 过号重排/取消候诊（不需要打印小票）
                    else if (workType == AsynchronousWorks.PassNum || workType == AsynchronousWorks.CancelWaiting)
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
                            /*
                            lab_name.Text = objT["result"]["patientName"].ToString();
                            //lab_name.Text = objT["result"]["age"].ToString();
                            lab_birthday.Text = objT["result"]["birthday"].ToString();
                            lab_tel.Text = objT["result"]["phone"].ToString();
                            lab_sex.Text = objT["result"]["sex"].ToString();

                            lab_sfz.Text = objT["result"]["sfz"].ToString();
                            lab_zlk.Text = objT["result"]["zlk"].ToString();
                            lab_jkt.Text = objT["result"]["jkt"].ToString();
                            lab_sbk.Text = objT["result"]["sbk"].ToString();
                             */
                            //重新查询更新状态
                            //workType = AsynchronousWorks.QueryID;
                            //CardID = lab_cardNo.Text;
                            NeedWaitingFrm = false;
                            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.QueryID, Argument = new String[] { lab_cardNo.Text } });
                            //Asynchronous();
                            MessageBoxUtils.Hint(result.msg);
                        }
                        else
                        {
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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

                            //打印小票
                            PrintNote print = new PrintNote(objT["result"]["hospitalName"].ToString(), objT["result"]["deptName"].ToString(), objT["result"]["clinicName"].ToString(), objT["result"]["queueNum"].ToString(), objT["result"]["waitingNum"].ToString(), lab_zlk.Text = objT["result"]["currentTime"].ToString());
                            string message = "";
                            if (!print.Print(ref message))
                            {
                                MessageBoxUtils.Show("打印小票失败：" + message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                MessageBoxUtils.Hint("打印小票完成");
                            }
                        }
                        else
                        {
                            MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                        NeedWaitingFrm = true;
                        MessageBoxUtils.Hint("打印小票完成");
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
                        List<PatientQueueEntity> list = objT["result"].ToObject<List<PatientQueueEntity>>();
                        gc_Patients.DataSource = list;
                        cmd.HideOpaqueLayer();
                        //跳转候诊列表
                        xtraTabControl1.SelectedTabPage = xtraTabPage2;
                        //cmd = new Xr.Common.Controls.OpaqueCommand(this);
                        return;
                    }
                    #endregion
                }
                else
                    MessageBoxUtils.Show(result.msg, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                //MessageBoxUtils.Hint(result.msg);

            }
            catch (Exception ex)
            {
                cmd.HideOpaqueLayer();
                if (ex.Message == "操作被取消。")
                    MessageBoxUtils.Hint(ex.Message);
                else
                    MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                // 关闭加载提示框
                //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                //cmd.HideOpaqueLayer();
                //关闭等待框
                if (NeedWaitingFrm)
                {
                    cmd.HideOpaqueLayer();
                }
                else
                {
                    NeedWaitingFrm = true;
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

            lab_sfz.Text = String.Empty;
            lab_zlk.Text = String.Empty;
            lab_jkt.Text = String.Empty;
            lab_sbk.Text = String.Empty;

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
            //WorkType = AsynchronousWorks.RoomListQuery;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
        }
        private void rBtn_PatientListStatus_Click(object sender, EventArgs e)
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
            //WorkType = AsynchronousWorks.WaitingPatientList;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
        }
        private void lueDept_EditValueChanged(object sender, EventArgs e)
        {
            //WorkType = AsynchronousWorks.RoomListQuery;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.RoomListQuery });
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
                Doctorid = prams[1];
                SelectDoctorid = prams[1];
                //现场挂号或选择该医生候诊
                //contextMenuStrip1.Show(btn,e.Location.X,e.Location.Y);
                if (BookingStatus == "6")//预约医生停诊后签到其他医生
                {
                    //WorkType = AsynchronousWorks.SingIn;
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.SingIn });
                }
                else if (BookingStatus == "RegisterNow")//选择医生为该患者现场挂号
                {
                    //WorkType = AsynchronousWorks.Register;
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.Register });
                }
                else
                {
                    MessageBoxUtils.Hint("请读取患者信息后操作");
                }
            }
            if (e.Button == MouseButtons.Right)//跳转候诊列表
            {
                Doctorid = prams[1];
                SelectDoctorid = prams[1];
                foreach (var ctl in flowLayoutPanel2.Controls)
                {
                    Control.RoomPanelButton btn1 = ctl as Control.RoomPanelButton;
                    List<String> prams1 = btn1.Tag as List<String>;
                    if (prams1[2] == prams[2])
                    {
                        btn1.IsCheck = true;
                        break;
                    }
                }
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
            if (e.Button == MouseButtons.Left)
            {
                //更新候诊列表
                //WorkType = AsynchronousWorks.WaitingPatientList;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
            }
        }
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
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.Urgent;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.Urgent, Argument = new String[] { TriageId } });
            }
        }

        private void 排号作废ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者作废排号吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.CancelWaiting;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.CancelWaiting, Argument = new String[] { TriageId } });
            }
        }

        private void 过号重排ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者过号重排吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.PassNum;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.PassNum, Argument = new String[] { TriageId } });
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

            if (MessageBoxUtils.Show("确定为该患者重打指引单吗?", MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.WaitingList;
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingList, Argument = new String[] { TriageId } });
            }
        }

        private void btn_refreshPatient_Click(object sender, EventArgs e)
        {
            //WorkType = AsynchronousWorks.WaitingPatientList;
            Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.WaitingPatientList });
        }
        private void 加急ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者加急吗?", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.Urgent;
                if (TriageIdInList != null && TriageIdInList != String.Empty)
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.Urgent, Argument = new String[] { TriageIdInList } });
                else
                    MessageBoxUtils.Hint("该患者尚未分诊");
            }
        }

        private void 排号作废ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者作废排号吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.CancelWaiting;
                if (TriageIdInList != null && TriageIdInList != String.Empty)
                    Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.CancelWaiting, Argument = new String[] { TriageIdInList } });
                else
                    MessageBoxUtils.Hint("该患者尚未分诊");
            }
        }

        private void 过号重排ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.Show("确定为该患者过号重排吗?", MessageBoxButtons.OKCancel,
     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //WorkType = AsynchronousWorks.PassNum;
                if (TriageIdInList != null && TriageIdInList != String.Empty)
                Asynchronous(new AsyncEntity() { WorkType = AsynchronousWorks.PassNum, Argument = new String[] { TriageIdInList } });
                else
                    MessageBoxUtils.Hint("该患者尚未分诊");
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




    }
    /// <summary>
    ///  预约信息实体
    /// </summary>
    public class RoomInfoEntity
    {    /// <summary>
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
        public String triage_time { get; set; }
        /// <summary>
        /// 就诊时间
        /// </summary>
        public String visit_time { get; set; }
    }
}
