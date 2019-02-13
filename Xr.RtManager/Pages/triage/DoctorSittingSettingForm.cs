using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.Http;
using Newtonsoft.Json.Linq;
using Xr.RtManager.Module.cms;
using Xr.RtManager.Module.triage;
using Xr.Common;

namespace Xr.RtManager.Pages.triage
{
    public partial class DoctorSittingSettingForm : UserControl
    {
        public DoctorSittingSettingForm()
        {
            InitializeComponent();
            #region
            GetDoctorAndDepartment(AppContext.AppConfig.deptCode);
            Doc = 0;
            SelectDoctor(AppContext.Session.deptId);
            DoctorSittingSelect(1, pageControl1.PageSize,DateTime.Now.ToString("yyy-MM-dd"),DateTime.Now.ToString("yyyy-MM-dd"));
            GetClinicList(AppContext.Session.hospitalId,AppContext.Session.deptId);
            this.beginDate.Text = DateTime.Now.ToString("yyy-MM-dd");
            this.endDate.Text = DateTime.Now.ToString("yyy-MM-dd");
            //DoctorSittingSelect(1, pageControl1.PageSize, "2019-02-01", "2019-02-01");
            #endregion 
        }
        #region 医生坐诊分页查询
        /// <summary>
        /// 医生坐诊分页查询
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        public void DoctorSittingSelect(int pageNo, int pageSize, string beginDate, string endDate)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "sch/doctorSitting/list?pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospitalId=" + AppContext.Session.hospitalId + "&deptId=" + string.Join(",", from p in listoffice where p.name == treeListLookUpEdit1.Text.Trim() select p.id) + "&doctorId=" + string.Join(",", from d in doctorInfoEntity where d.name == luDoctords.Text.Trim() select d.id) + "&beginDate=" + beginDate + "&endDate=" + endDate;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    this.gc_Atrlices.DataSource = objT["result"]["list"].ToObject<List<DoctorSittingInfoEntity>>();
                    pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                    int.Parse(objT["result"]["pageSize"].ToString()),
                    int.Parse(objT["result"]["pageNo"].ToString()));
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("医生坐诊分页查询列表错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 获取科室信息
        List<TreeList> listoffice;
        List<HospitalInfoEntity> doctorInfoEntity;
        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <param name="code"></param>
        public void GetDoctorAndDepartment(string code)
        {
            try
            {
                listoffice = new List<TreeList>();
                //查询科室下拉框数据
                String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?hospital.code=" + AppContext.AppConfig.hospitalCode + "&code=" + code;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    listoffice = objT["result"].ToObject<List<TreeList>>();
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
                listoffice.Add(new TreeList { id = "", parentId = "", name = "全部科室" });
                treeListLookUpEdit1.Properties.DataSource = listoffice;
                treeListLookUpEdit1.Properties.TreeList.KeyFieldName = "id";
                treeListLookUpEdit1.Properties.TreeList.ParentFieldName = "parentId";
                treeListLookUpEdit1.Properties.DisplayMember = "name";
                treeListLookUpEdit1.Properties.ValueMember = "id";
                treeListLookUpEdit2.Properties.DataSource = listoffice;
                treeListLookUpEdit2.Properties.TreeList.KeyFieldName = "id";
                treeListLookUpEdit2.Properties.TreeList.ParentFieldName = "parentId";
                treeListLookUpEdit2.Properties.DisplayMember = "name";
                treeListLookUpEdit2.Properties.ValueMember = "id";
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 
        #region  获取科室下面的医生
        /// <summary>
        /// 获取当前科室医生
        /// </summary>
        /// <param name="dept"></param>
        public void SelectDoctor(string dept)
        {
            try
            {
                doctorInfoEntity = new List<HospitalInfoEntity>();
                // 查询医生下拉框数据
                String url = AppContext.AppConfig.serverUrl + "cms/doctor/findAll?hospital.id=" + AppContext.Session.hospitalId + "&dept.id=" + dept;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    doctorInfoEntity = objT["result"].ToObject<List<HospitalInfoEntity>>();
                    doctorInfoEntity.Insert(0, new HospitalInfoEntity { id = "", name = "选择" });
                    switch (Doc)
                    {
                        case 0:
                            luDoctords.Properties.DataSource = doctorInfoEntity;
                            luDoctords.Properties.DisplayMember = "name";
                            luDoctords.Properties.ValueMember = "id";
                            lookUpEdit1.Properties.DataSource = doctorInfoEntity;
                            lookUpEdit1.Properties.DisplayMember = "name";
                            lookUpEdit1.Properties.ValueMember = "id";
                            break;
                        case 1:
                            luDoctords.Properties.DataSource = doctorInfoEntity;
                            luDoctords.Properties.DisplayMember = "name";
                            luDoctords.Properties.ValueMember = "id";
                            break;
                        case 2:
                            lookUpEdit1.Properties.DataSource = doctorInfoEntity;
                            lookUpEdit1.Properties.DisplayMember = "name";
                            lookUpEdit1.Properties.ValueMember = "id";
                            break;
                    }
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
       
        int Doc = 0;
        private void treeListLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Doc = 1;
            SelectDoctor(treeListLookUpEdit1.EditValue.ToString());
        }

        private void treeListLookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Doc = 2;
            SelectDoctor(treeListLookUpEdit2.EditValue.ToString());
          //  this.gridView1.SetRowCellValue(0, gridView1.Columns["deptName"], treeListLookUpEdit2.Text.Trim());
        }
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
           // this.gridView1.SetRowCellValue(0, gridView1.Columns["doctorName"], lookUpEdit1.Text.Trim());
        }
        #endregion
        #region 设置显示格式
        /// <summary>
        /// 设置显示格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Atrlices_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName=="period")
            {
                switch (e.Value.ToString())//0 上午，1下午，2晚上，3全天
                {
                    case "0":
                        e.DisplayText = "上午";
                        break;
                    case "1":
                        e.DisplayText = "下午";
                        break;
                    case "2":
                        e.DisplayText = "晚上";
                        break;
                    case "3":
                        e.DisplayText = "全天";
                        break;
                }
            }
            if (e.Column.FieldName=="isStop")
            {
                 switch (e.Value.ToString())
                {
                    case "0":
                        e.DisplayText = "是";
                        break;
                    case "1":
                        e.DisplayText = "否";
                        break;
                }
            }
        }
        #endregion 
        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl1_Click(object sender, EventArgs e)
        {
            DoctorSittingSelect(1, pageControl1.PageSize, this.beginDate.Text.Trim(), this.endDate.Text.Trim());
        }
        #endregion 
        #region  获取诊室列表
        #region 
        public class Test
        {
            public String Department { get; set; }
            public String Doctor { get; set; }
            public String morning { get; set; }
            public String mornings { get; set; }
            public String afternoon { get; set; }
            public String afternoons { get; set; }
            public String DarkNight { get; set; }
            public String DarkNights { get; set; }
            public String AllDay { get; set; }
            public String AllDays { get; set; }
        }
        #endregion 
        List<ClinicInfoEntity> clinicInfo;
        public void GetClinicList(string hospitalId, string deptId)
        {
            try
            {
                clinicInfo = new List<ClinicInfoEntity>();
                String url = AppContext.AppConfig.serverUrl + "cms/clinic/list?hospital.id=" + hospitalId + "&dept.id=" + deptId;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    clinicInfo = new List<ClinicInfoEntity>();
                    clinicInfo = objT["result"]["list"].ToObject<List<ClinicInfoEntity>>();
                    ClinicInfoEntity dept = new ClinicInfoEntity();
                    dept.id = "";
                    dept.name = "选择诊室";
                    clinicInfo.Insert(0, dept);
                    repositoryItemLookUpEdit3.Properties.DataSource = clinicInfo;
                    repositoryItemLookUpEdit3.Properties.DisplayMember = "name";
                    repositoryItemLookUpEdit3.Properties.ValueMember = "id";
                    repositoryItemLookUpEdit3.ShowHeader = false;
                    repositoryItemLookUpEdit3.ShowFooter = false;
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("获取诊室列表错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 检查数据
        private void repositoryItemLookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            string id = ((DevExpress.XtraEditors.LookUpEdit)sender).EditValue.ToString();//诊室ID
            int selectRow = gridView1.GetSelectedRows()[0];
            string period = this.gridView1.GetRowCellValue(selectRow, "period").ToString();//时段
            string workDate = this.gridView1.GetRowCellValue(selectRow, "workDate").ToString();//时间
            string doctorId = lookUpEdit1.EditValue.ToString();//医生ID
            string deptId = treeListLookUpEdit2.EditValue.ToString();//科室ID
            if (!CheckInfo(AppContext.Session.hospitalId, doctorId, deptId, id, workDate, period))
            {
                ((DevExpress.XtraEditors.LookUpEdit)sender).Text = "";
            }
        }
        #region 
        //private void repositoryItemComboBox1_EditValueChanged(object sender, EventArgs e)
        //{
        //    var i = ((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedItem;
        //    string id = string.Join(",", from p in clinicInfo where p.name == i select p.id);
        //    if (dateEdit3.Text.Trim() == "")
        //    {
        //        MessageBox.Show("请选择坐诊日期");
        //        return;
        //    }
        //    #region
        //    string period = "";
        //    //switch (this.gv_VitalSignsInfo.FocusedColumn.Caption.ToString())//0 上午，1下午，2晚上，3全天
        //    //{
        //    //    case "上午":
        //    //        period = "0";
        //    //        break;
        //    //    case "下午":
        //    //        period = "1";
        //    //        break;
        //    //    case "晚上":
        //    //        period = "2";
        //    //        break;
        //    //    case "全天":
        //    //        period = "3";
        //    //        break;
        //    //}
        //    if (treeListLookUpEdit2.Text.Trim() == "")
        //    {
        //        MessageBox.Show("请选择坐诊科室");
        //        return;
        //    }
        //    #endregion
        //    string doctorId = lookUpEdit1.EditValue.ToString();
        //    string deptId = string.Join(",", from s in listoffice where s.name == treeListLookUpEdit2.Text.Trim() select s.id);
        //    if (!CheckInfo(AppContext.Session.hospitalId, doctorId, deptId, id, dateEdit3.Text.Trim(), period))
        //    {
        //        ((DevExpress.XtraEditors.ComboBoxEdit)sender).Text = "";
        //    }
        //}
        #endregion 
        #endregion
        #region 医生坐诊保存设置
        /// <summary>
        /// 医生坐诊保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl2_Click(object sender, EventArgs e)
        {
            string deptId = treeListLookUpEdit2.EditValue.ToString();//科室ID
            string doctorId = lookUpEdit1.EditValue.ToString();//医生ID
            /*
             * [{"workDate":"2019-02-01", "values":[{"period":"0", "clinicId":"1"}, {"period":"1", "clinicId":"1"}, {"period":"2", "clinicId":"1"}]},{"workDate":"2019-02-02", "values":[{"period":"0", "clinicId":"1"}, {"period":"1", "clinicId":"1"}]},{"workDate":"2019-02-03", "values":[{"period":"0", "clinicId":"1"}]}]
             */
            SaveDoctorSetting(AppContext.Session.hospitalId, deptId, doctorId,"");
            #region 
            //string deptId = string.Join(",", from s in listoffice where s.name == treeListLookUpEdit2.Text.Trim() select s.id);
            //string doctorId = string.Join(",", from sw in doctorInfoEntity where sw.name == lookUpEdit1.Text.Trim() select sw.id);
            //string begiontime = this.dateEdit3.Text.Trim();
            //string endtime = this.dateEdit4.Text.Trim();
            //List<info> info = new List<info>();
            //List<info> infos = new List<info>();
            //List<OveradeJson> stra = new List<OveradeJson>();
            //string LookTime = "";
            //string clinicIds = "";
            //for (int i = 2; i < gv_VitalSignsInfo.Columns.Count; i++)
            //{
            //    #region 时段
            //    switch (i)//0 上午，1下午，2晚上，3全天
            //    {
            //        case 2:
            //            LookTime = "0";
            //            clinicIds = gv_VitalSignsInfo.GetRowCellValue(0, "morning").ToString();
            //            break;
            //        case 3:
            //            LookTime = "1";
            //            clinicIds = gv_VitalSignsInfo.GetRowCellValue(0, "afternoon").ToString();
            //            break;
            //        case 4:
            //            LookTime = "2";
            //            clinicIds = gv_VitalSignsInfo.GetRowCellValue(0, "DarkNight").ToString();
            //            break;
            //        case 5:
            //            LookTime = "3";
            //            clinicIds = gv_VitalSignsInfo.GetRowCellValue(0, "AllDay").ToString();
            //            break;
            //        case 6:
            //            LookTime = "0";
            //            clinicIds = gv_VitalSignsInfo.GetRowCellValue(0, "mornings").ToString();
            //            break;
            //        case 7:
            //            LookTime = "1";
            //            clinicIds = gv_VitalSignsInfo.GetRowCellValue(0, "afternoons").ToString();
            //            break;
            //        case 8:
            //            LookTime = "2";
            //            clinicIds = gv_VitalSignsInfo.GetRowCellValue(0, "DarkNights").ToString();
            //            break;
            //    }
            //    #endregion 
            //    #region
            //    //var str = ((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedItem;
            //    //string id = string.Join(",", from p in clinicInfo where p.name == str select p.id);
            //    //string period = "";
            //    //switch (this.gv_VitalSignsInfo.FocusedColumn.Caption.ToString())//0 上午，1下午，2晚上，3全天
            //    //{
            //    //    case "上午":
            //    //        period = "0";
            //    //        break;
            //    //    case "下午":
            //    //        period = "1";
            //    //        break;
            //    //    case "晚上":
            //    //        period = "2";
            //    //        break;
            //    //    case "全天":
            //    //        period = "3";
            //    //        break;
            //    //}
            //    #endregion 
            //    string a =string.Join(",", from g in clinicInfo where g.name == clinicIds select g.id);
            //    //if (i >= 5)
            //    //{
            //    //    info.Add(new info() { period = LookTime, clinicId = a });
            //    //}
            //    //else
            //    //{
            //    //    infos.Add(new info() { period = LookTime, clinicId = a });
            //    //}
            //    //if (i == 5)
            //    //{
            //    //    stra.Add(new OveradeJson() { workDate = begiontime, values = info });
            //    //}
            //    //else if(i==8)
            //    //{
            //    //    stra.Add(new OveradeJson() { workDate = endtime, values = infos });
            //    //}
          //  }
            //var str = ToJSON(stra);
            //SaveDoctorSetting(AppContext.Session.hospitalId, deptId, doctorId,str);
            #region
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("0", "0");
            //dic.Add("1", "1");

            //JObject[] jo = (from p in dic select new JObject { new JProperty("period", p.Key), new JProperty("clinicId", p.Value) }).ToArray<JObject>();
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(jo);
            // var str = new
            // {
            //     workDate = "1",
            //     values = new {
            //         period="1",
            //         clinicId="1",
            //     }
            // };
            //var a= SerializeObject(str);
            #endregion
            #endregion 
        }
        #region 保存医生坐诊
        /// <summary>
        /// 保存医生坐诊设置
        /// </summary>
        /// <param name="hospitalId">医院主键</param>
        /// <param name="deptId">科室主键</param>
        /// <param name="doctorId">医生ID</param>
        /// <param name="sittingArray">日期+时段+坐诊诊室的json数组</param>
        public void SaveDoctorSetting(string hospitalId, string deptId, string doctorId,dynamic sittingArray)
        {
            try
            {
                String url = AppContext.AppConfig.serverUrl + "sch/doctorSitting/saveSitting?hospitalId=" + hospitalId + "&deptId=" + deptId + "&doctorId=" + doctorId + "&sittingArray=" + sittingArray;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint(objT["message"].ToString());
                    DoctorSittingSelect(1, pageControl1.PageSize, DateTime.Now.ToString("yyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                    //List<Test> li = new List<Test>();
                    //li.Add(new Test { Department = "", Doctor = "", morning = "", mornings = "", DarkNight = "", DarkNights = "", afternoon = "", afternoons = "", AllDay = "" });
                    // this.gc_VitalSignsInfo.DataSource = li;
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("保存医生坐诊设置错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 将对象序列化成JSON格式字符串
        /// <summary>
        /// 将对象序列化成JSON格式字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ToJSON(object obj)
        {
            //JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            //{
            //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            //};
            //return Newtonsoft.Json.JsonConvert.SerializeObject(obj,microsoftDateFormatSettings);
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeFormat = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, timeFormat);
        }
        #endregion 
        #endregion
        #region 检查当前科室+日期+诊室是否已经存在
        /// <summary>
        /// 检查当前科室+日期+诊室是否已经存在
        /// </summary>
        /// <param name="hospitalId">医院主键</param>
        /// <param name="deptId">科室主键</param>
        /// <param name="clinicId">诊间ID</param>
        /// <param name="workDate">坐诊日期</param>
        /// <param name="period">时间段代码(0 上午，1下午，2晚上，3全天)</param>
        public bool CheckInfo(string hospitalId,string doctorID, string deptId, string clinicId, string workDate, string period)
        {
            bool Check = false;
            try
            {
                if (clinicId != "")
                {
                    String url = AppContext.AppConfig.serverUrl + "sch/doctorSitting/checkIsExist?hospitalId=" + hospitalId + "&doctorId=" + doctorID + "&deptId=" + deptId + "&clinicId=" + clinicId + "&workDate=" + workDate + "&period=" + period;
                    String data = HttpClass.httpPost(url);
                    JObject objT = JObject.Parse(data);
                    if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                    {
                        Check = true;
                    }
                    else
                    {
                        MessageBox.Show(objT["message"].ToString());
                        Check = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("检查当前科室+日期+诊室是否已经存在错误信息:" + ex.Message);
            }
            return Check;
        }
        #endregion 
        #region 验证开始日期是否小于当前日期
        /// <summary>
        /// 验证开始日期是否小于当前日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEdit3.Text != "")
            {
                if (CompanyDate(dateEdit3.Text.Trim(), DateTime.Now.ToString("yyy-MM-dd")))
                {
                    //gridBand3.Caption = "日期" + "(" + dateEdit3.Text.Trim() + ")";
                }
                else
                {
                    MessageBox.Show("选择的开始日期不能小于当前日期");
                    dateEdit3.Text = "";//DateTime.Now.ToString("yyy-MM-dd");
                }
            }
        }
        #region 比较两个日期大小
        /// <summary>
        /// 比较两个日期大小
        /// </summary>
        /// <param name="dateStr1">日期1</param>
        /// <param name="dateStr2">日期2</param>
        /// <param name="msg">返回信息</param>
        public bool CompanyDate(string dateStr1, string dateStr2)
        {
            //将日期字符串转换为日期对象
            DateTime t1 = Convert.ToDateTime(dateStr1);
            DateTime t2 = Convert.ToDateTime(dateStr2);
            //通过DateTIme.Compare()进行比较（）
            int compNum = DateTime.Compare(t1, t2);

            //t1> t2
            if (compNum < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion 

        /// <summary>
        /// 计算开始日期和结束日期隔几天并且验证结束日期是否小于开始日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEdit4_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEdit4.Text != "")
            {
                if (CompanyDate(dateEdit4.Text.Trim(), DateTime.Now.ToString("yyy-MM-dd")))
                {
                   // gridBand4.Caption = "日期" + "(" + dateEdit4.Text.Trim() + ")";
                    DateTime d1 = Convert.ToDateTime(dateEdit3.Text.Trim());
                    DateTime d2 = Convert.ToDateTime(dateEdit4.Text.Trim());
                    DateTime d3 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d1.Year, d1.Month, d1.Day));
                    DateTime d4 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d2.Year, d2.Month, d2.Day));
                    int days = (d4 - d3).Days;//相隔的天数
                    GetDoctorSittingClinic();
                }
                else
                {
                    MessageBox.Show("选择的结束日期不能小于当前日期");
                    dateEdit4.Text = "";// DateTime.Now.ToString("yyy-MM-dd");
                }
            }
        }
        #endregion 
        #region 获取指定医院、科室、医生、日期范围内医生每个日期的排班时段及坐诊诊室
        public void GetDoctorSittingClinic()
        {
            try
            {
                String Url = AppContext.AppConfig.serverUrl + "sch/doctorScheduPlan/findPlanPeriod?hospitalId="+AppContext.Session.hospitalId+"&deptId="+treeListLookUpEdit2.EditValue+"&doctorId="+lookUpEdit1.EditValue+"&beginDate="+this.dateEdit3.Text.Trim()+"&endDate="+this.dateEdit4.Text.Trim();
                String data = HttpClass.httpPost(Url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<DoctorSrtting> scheduledList = objT["result"].ToObject<List<DoctorSrtting>>();
                    List<DoctorSrtting> dataSource = new List<DoctorSrtting>();
                    for (int i = 0; i < scheduledList.Count; i++)
                    {
                        DoctorSrtting scheduled = scheduledList[i];
                        scheduled.deptName = treeListLookUpEdit2.Text.Trim();
                        scheduled.doctorName = lookUpEdit1.Text.Trim();
                        dataSource.Add(scheduled);
                    }
                    gcScheduled.DataSource = dataSource;
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    this.gcScheduled.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteLog(" 获取指定医院、科室、医生、日期范围内医生每个日期的排班时段及坐诊诊室错误信息："+ex.Message);
            }
        }
        #endregion
        #region 合并列
        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            int rowHandle1 = e.RowHandle1;
            int rowHandle2 = e.RowHandle2;
            string deptName1 = gridView1.GetRowCellValue(rowHandle1, gridView1.Columns["deptName"]).ToString(); //获取科室列值
            string deptName2 = gridView1.GetRowCellValue(rowHandle2, gridView1.Columns["deptName"]).ToString();
            string doctorName1 = gridView1.GetRowCellValue(rowHandle1, gridView1.Columns["doctorName"]).ToString(); //获取医生列值
            string doctorName2 = gridView1.GetRowCellValue(rowHandle2, gridView1.Columns["doctorName"]).ToString();
            string workDate1 = gridView1.GetRowCellValue(rowHandle1, gridView1.Columns["workDate"]).ToString(); //获取日期列值
            string workDate2 = gridView1.GetRowCellValue(rowHandle2, gridView1.Columns["workDate"]).ToString();
            if (e.Column.FieldName == "deptName")
            {
                if (deptName1 != deptName2)
                {
                    e.Merge = false; //值相同的2个单元格是否要合并在一起
                    e.Handled = true; //合并单元格是否已经处理过，无需再次进行省缺处理
                }
            }
            else if (e.Column.FieldName == "workDate")
            {
                if (!(deptName1 == deptName2 && workDate1 == workDate2))
                {
                    e.Merge = false;
                    e.Handled = true;
                }
            }
            else if (e.Column.FieldName == "doctorName")
            {
                if (doctorName1 != doctorName2)
                {
                    e.Merge = false;
                    e.Handled = true;
                }
            }
            else
            {
                e.Merge = false;
                e.Handled = true;
            }
        }
        #endregion 
        #region 设置列显示字段
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "period")
            {
                switch (e.Value.ToString())//0 上午，1下午，2晚上，3全天
                {
                    case "0":
                        e.DisplayText = "上午";
                        break;
                    case "1":
                        e.DisplayText = "下午";
                        break;
                    case "2":
                        e.DisplayText = "晚上";
                        break;
                    case "3":
                        e.DisplayText = "全天";
                        break;
                }
            }
        }
        #endregion 
        #region 医生坐诊时段查询用
        /// <summary>
        /// 医生坐诊时段查询用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl3_Click(object sender, EventArgs e)
        {
            GetDoctorSittingClinic();
        }
        #endregion 
    }
}
