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
using Xr.RtManager.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace Xr.RtManager.Pages.booking
{
    public partial class AppointmentQueryForm : UserControl
    {
        Xr.Common.Controls.OpaqueCommand cmd;
        public AppointmentQueryForm()
        {
            InitializeComponent();
            //cmd = new Xr.Common.Controls.OpaqueCommand(this);
            //cmd.ShowOpaqueLayer(225, true);
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
            //下拉框数据
            getLuesInfo();
            //配置时间格式
            setDateFomartDefult();
            性别.Caption = "状\r\n态";
            状态.Caption = "状\r\n态";
            就诊类别.Caption = "就诊\r\n类别";
            术后复诊.Caption = "术后\r\n复诊";
            出院复诊.Caption = "出院\r\n复诊";
            外院转诊.Caption = "外院\r\n转诊";
            登记时间.Caption = "登记\r\n时间";
        }
        /// <summary>
        /// 下拉框数据
        /// </summary>
        void getLuesInfo()
        {
            //查询科室下拉框数据
            String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + AppContext.AppConfig.deptCode;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DeptEntity> deptList = objT["result"].ToObject<List<DeptEntity>>();
                /*DeptEntity dept = new DeptEntity();
                dept.id = "0";
                dept.name = "无";
                deptList.Insert(0, dept);
                 */
                treeDeptId.Properties.DataSource = deptList;
                treeDeptId.Properties.TreeList.KeyFieldName = "id";
                treeDeptId.Properties.TreeList.ParentFieldName = "parentId";
                treeDeptId.Properties.DisplayMember = "name";
                treeDeptId.Properties.ValueMember = "id";
            }
            else
            {
                MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            //默认选中第一个
            treeDeptId.EditValue = AppContext.Session.deptList[0].id;

            //预约状态下拉框数据
            String param = "type={0}";
            param = String.Format(param, "register_status_type");

            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?" + param;
             objT = new JObject();
            objT = JObject.Parse(HttpClass.httpPost(url));
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                //List<Dic> list = objT["result"].ToObject<List<Dic>>();
                List<Dic> list = new List<Dic>();
                list.Add(new Dic { label = "全部", value = "" });
                list.AddRange(objT["result"].ToObject<List<Dic>>());
                lueState.Properties.DataSource = list;
                lueState.Properties.DisplayMember = "label";
                lueState.Properties.ValueMember = "value";
                lueState.ItemIndex = 0;
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
                List<Dic> list = new List<Dic>();
                list.Add(new Dic { label = "全部", value = "" });
                list.AddRange(objT["result"].ToObject<List<Dic>>());
                lueRegisterWay.Properties.DataSource = list;
                lueRegisterWay.Properties.DisplayMember = "label";
                lueRegisterWay.Properties.ValueMember = "value";
                lueRegisterWay.ItemIndex = 0;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }
        private void buttonControl1_Click(object sender, EventArgs e)
        {
            cmd = new OpaqueCommand(this);
            if (VerifyInfo())
            {
                QueryInfo();
            }
        }
        AppointmentQueryParam CurrentParam = new AppointmentQueryParam();
        private bool VerifyInfo()
        { //deStart.Text,
            //deEnd.Text

            if (deStart.EditValue == null)
            {
                MessageBox.Show("请选择开始日期");
                return false;
            }
            if (deEnd.EditValue == null)
            {
                MessageBox.Show("请选择结束日期");
                return false;
            }

            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
                dtStart = DateTime.ParseExact(deStart.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                dtEnd = DateTime.ParseExact(deEnd.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            
            if (dtEnd < dtStart)
            {
                MessageBox.Show("结束日期需大于开始日期");
                return false;
            }

            CurrentParam.deptId = treeDeptId.EditValue.ToString();
            CurrentParam.patientName = txt_nameQuery.Text;
            CurrentParam.registerWay = lueRegisterWay.EditValue.ToString();
            CurrentParam.status = lueState.EditValue.ToString();
            CurrentParam.startDate = deStart.Text;
            CurrentParam.endDate = deEnd.Text;

            return true;
        }
        private void QueryInfo()
        {
            // 弹出加载提示框
            //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(WaitingForm));
            cmd.ShowOpaqueLayer(225, true);

            // 开始异步
            BackgroundWorkerUtil.start_run(bw_DoWork, bw_RunWorkerCompleted, null, false);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<String> Results = new List<String>();//lueDept.EditValue
                //String param = "deptId=2&registerWay=0&status=1&patientName=李鹏真&startDate=2019-01-05&endDate=2019-01-11";
                String param = "";//deptId={0}&registerWay={1}&status={2}&patientName={3}&startDate={4}&endDate={5}&pageSize=10000";
                /*param = String.Format(param,
                    CurrentParam.deptId,
                    CurrentParam.registerWay,
                    CurrentParam.status,
                    CurrentParam.patientName,
                    CurrentParam.startDate,
                    CurrentParam.endDate);
                 */

                //获取预约信息
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("deptId", CurrentParam.deptId);
                if (CurrentParam.registerWay != String.Empty)
                    prament.Add("registerWay", CurrentParam.registerWay);
                if (CurrentParam.status != String.Empty)
                    prament.Add("status", CurrentParam.status);
                if (CurrentParam.patientName != String.Empty)
                    prament.Add("patientName", CurrentParam.patientName);
                prament.Add("startDate", CurrentParam.startDate);
                prament.Add("endDate", CurrentParam.endDate);
                prament.Add("pageSize", "10000");

                String url = String.Empty;
                if (prament.Count != 0)
                {
                    param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
                }
                url = AppContext.AppConfig.serverUrl + "sch/doctorScheduRegister/list?" + param;
                Results.Add(HttpClass.httpPost(url));


                e.Result = Results;
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                List<String> datas = e.Result as List<String>;
                if (datas.Count == 0)
                {
                    return;
                }
                JObject objT = new JObject();
                objT = JObject.Parse(datas[0]);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<AppointmentEntity> list = objT["result"]["list"].ToObject<List<AppointmentEntity>>();
                    this.gcAppointmentInfo.DataSource = list;
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // 关闭加载提示框
                //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                cmd.HideOpaqueLayer();
            }
        }

        private void gv_AppointmentInfo_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;

            AppointmentEntity CurrentrowItem = gv.GetRow(e.RowHandle) as AppointmentEntity;
            lab_deptName.Text = CurrentrowItem.deptName;
            lab_patientName.Text = CurrentrowItem.patientName;
            lab_visitType.Text = CurrentrowItem.visitType;
            lab_doctorName.Text = CurrentrowItem.doctorName;
            lab_sex.Text = CurrentrowItem.sex;
            lab_cardType.Text = CurrentrowItem.cardType;
            lab_beginTime.Text = CurrentrowItem.beginTime;
            lab_age.Text = CurrentrowItem.age;
            lab_cardNo.Text = CurrentrowItem.cardNo;
            lab_statusTxt.Text = CurrentrowItem.statusTxt;
            lab_tempPhone.Text = CurrentrowItem.tempPhone;
            lab_registerTime.Text = CurrentrowItem.registerTime;
            lab_address.Text = CurrentrowItem.address;
            lab_note.Text = CurrentrowItem.note;

        }
        public void setDateFomartDefult()
        {
            this.deStart.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deStart.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deStart.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.deStart.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;

            this.deEnd.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deEnd.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deEnd.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.deEnd.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
            this.deEnd.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));

        }
    }
    /// <summary>
    /// 下拉框数据
    /// </summary>
    public class Dic
    {
        public string value { get; set; }
        public string label { get; set; }
    }
    /// <summary>
    /// 查询参数实体
    /// </summary>
    public class AppointmentQueryParam
    {
        /*deptId=2&registerWay=0&status=1&patientName=李鹏真&startDate=2019-01-05&endDate=2019-01-11
         */
        /// <summary>
        /// 科室ID
        /// </summary>
        public String deptId { get; set; }
        /// <summary>
        /// 预约途径
        /// </summary>
        public String registerWay { get; set; }
        /// <summary>
        /// 预约状态
        /// </summary>
        public String status { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public String patientName { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public String startDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public String endDate { get; set; }
    }
    /// <summary>
    ///  预约信息实体
    /// </summary>
    public class AppointmentEntity
    {    /// <summary>
        /// 患者姓名
        /// </summary>
        public String patientName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public String sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public String age { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public String tempPhone { get; set; }
        /// <summary>
        /// 预约卡类型 
        /// </summary>
        public String cardType { get; set; }
        /// <summary>
        /// 预约卡号 
        /// </summary>
        public String cardNo { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public String workDate { get; set; }
        /// <summary>
        /// 预约周
        /// </summary>
        public String week { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public String beginTime { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public String deptName { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public String doctorName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public String statusTxt { get; set; }
        /// <summary>
        /// 途径 
        /// </summary>
        public String registerWay { get; set; }
        /// <summary>
        /// 就诊类别 
        /// </summary>
        public String visitType { get; set; }
        /// <summary>
        /// 术后复诊
        /// </summary>
        public String isShfz { get; set; }
        /// <summary>
        /// 出院复诊
        /// </summary>
        public String isCyfz { get; set; }

        /// <summary>
        /// 外院转诊
        /// </summary>
        public String isYwzz { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        public String registerTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String note { get; set; }
        /// <summary>
        /// 地址 #
        /// </summary>
        public String address { get; set; }
        
    }
}
