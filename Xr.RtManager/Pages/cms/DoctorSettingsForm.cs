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
using System.IO;
using System.Net;
using Xr.Common;
using Xr.Common.Controls;
using DevExpress.XtraEditors;

namespace Xr.RtManager.Pages.cms
{
    public partial class DoctorSettingsForm : UserControl
    {
        public DoctorSettingsForm()
        {
            InitializeComponent();
        }

        public DoctorInfoEntity doctorInfo { get; set; }
        public DefaultVisitEntity defaultVisit { get; set; }
        /// <summary>
        /// 出诊信息模板
        /// </summary>
        public DefaultVisitEntity defaultVisitTemplate { get; set; }

        private String hospitalId { get; set; }
        private String deptId { get; set; }

        private void DeptSettingsForm_Load(object sender, EventArgs e)
        {
            //把这行删了，再显示分页控件，就是分页了，不过宽度不够显示分页控件
            pageControl1.PageSize = 10000;//一页一万条，不显示分页；
            dcDoctorInfo.DataType = typeof(DoctorInfoEntity);
            dcDefaultVisit.DataType = typeof(DefaultVisitEntity);
            //清除排班数据
            dcDefaultVisit.ClearValue();
            panScheduling.Controls.Clear();

            List<Item> itemList = new List<Item>();
            foreach (DeptEntity dept in AppContext.Session.deptList)
            {
                Item item = new Item();
                item.name = dept.name;
                item.value = dept.id;
                item.tag = dept.hospitalId;
                itemList.Add(item);
            }
            menuControl2.setDataSource(itemList);

            //查询医院下拉框数据
            String url = AppContext.AppConfig.serverUrl + "cms/hospital/findAll";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueHospital.Properties.DataSource = objT["result"].ToObject<List<HospitalInfoEntity>>();
                lueHospital.Properties.DisplayMember = "name";
                lueHospital.Properties.ValueMember = "id";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
            
            //查询状态下拉框数据
            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=is_use";
            data = HttpClass.httpPost(url);
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueIsUse.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueIsUse.Properties.DisplayMember = "label";
                lueIsUse.Properties.ValueMember = "value";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }

            //查询挂号类型下拉框数据
            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=register_type";
            data = HttpClass.httpPost(url);
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueRegisterType.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueRegisterType.Properties.DisplayMember = "label";
                lueRegisterType.Properties.ValueMember = "value";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }

            //查询性别下拉框数据
            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=sex";
            data = HttpClass.httpPost(url);
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueSex.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueSex.Properties.DisplayMember = "label";
                lueSex.Properties.ValueMember = "value";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }

            //查询是否显示下拉框数据
            url = AppContext.AppConfig.serverUrl + "sys/sysDict/findByType?type=show_hide";
            data = HttpClass.httpPost(url);
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                lueIsShow.Properties.DataSource = objT["result"].ToObject<List<DictEntity>>();
                lueIsShow.Properties.DisplayMember = "label";
                lueIsShow.Properties.ValueMember = "value";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }

            //获取默认出诊时间字典配置
            url = AppContext.AppConfig.serverUrl + "cms/doctor/findDoctorVisitingDict";
            data = HttpClass.httpPost(url);
            objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                defaultVisitTemplate = objT["result"].ToObject<DefaultVisitEntity>();
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        public void SearchData(int pageNo, int pageSize)
        {
            //缩小后宽度不够分页控件显示，所以这里不显示分页了，当前页传10000条，以后要分页的话，把这个10000条去掉就行了
            String param = "pageNo=" + pageNo + "&pageSize=" + pageSize + "&hospital.id=" + hospitalId + "&dept.id=" + deptId;
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/list?"+param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DoctorInfoEntity> doctorList = objT["result"]["list"].ToObject<List<DoctorInfoEntity>>();
                List<DictEntity> useDictList = lueIsUse.Properties.DataSource as List<DictEntity>;
                foreach (DoctorInfoEntity doctor in doctorList)
                {
                    if (doctor.isShow.Equals("1"))
                        doctor.isShow = "√";
                    else
                        doctor.isShow = "";
                    if (doctor.ignoreHoliday.Equals("1"))
                        doctor.ignoreHoliday = "√";
                    else
                        doctor.ignoreHoliday = "";
                    foreach(DictEntity dict in useDictList){
                        if(doctor.isUse.Equals(dict.value))
                            doctor.isUse = dict.label;
                    }
                }

                gcDoctor.DataSource = doctorList;
                pageControl1.setData(int.Parse(objT["result"]["count"].ToString()),
                int.Parse(objT["result"]["pageSize"].ToString()),
                int.Parse(objT["result"]["pageNo"].ToString()));
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        private void pageControl1_Query(int CurrentPage, int PageSize)
        {
            SearchData(CurrentPage, PageSize);
        }



        #region 选择与上传图片
        String pictureFilePath = "";
        String pictureServiceFilePath = "";
        
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "请选择文件";
                fileDialog.Filter = "所有文件(*txt*)|*.*"; //设置要选择的文件的类型
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureFilePath = fileDialog.FileName;//返回文件的完整路径   
                    pbPicture.ImageLocation = pictureFilePath; //显示本地图片
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (pictureFilePath != "")
            {
                string url = AppContext.AppConfig.serverUrl + "common/uploadFile";
                List<FormItemModel> lstPara = new List<FormItemModel>();
                FormItemModel model = new FormItemModel();
                // 文件
                model.Key = "multipartFile";
                int l = pictureFilePath.Length;
                int i = pictureFilePath.LastIndexOf("\\") + 2;
                model.FileName = pictureFilePath.Substring(i, l - i);
                model.FileContent = new FileStream(pictureFilePath, FileMode.Open);
                lstPara.Add(model);

                String data = HttpClass.PostForm(url, lstPara);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    WebClient web = new WebClient();
                    var bytes = web.DownloadData(objT["result"][0].ToString());
                    this.pbPicture.Image = Bitmap.FromStream(new MemoryStream(bytes));
                    pictureServiceFilePath = objT["result"][0].ToString();
                    MessageBoxUtils.Hint("上传图片成功");
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择要上传的文件");
            }
        }
        #endregion

        /// <summary>
        /// 设置出诊时间
        /// </summary>
        private void setDefaultVisit()
        {
            defaultVisit = new DefaultVisitEntity();
            String[] hyArr = defaultVisitTemplate.defaultSourceNumber.Split(new char[] { '-' });

            defaultVisit.mStart = defaultVisitTemplate.defaultVisitTimeAm.Substring(0, 5);
            defaultVisit.mEnd = defaultVisitTemplate.defaultVisitTimeAm.Substring(6, 5);
            defaultVisit.mSubsection = defaultVisitTemplate.segmentalDuration;
            defaultVisit.mScene = hyArr[0];
            defaultVisit.mOpen = hyArr[1];
            defaultVisit.mRoom = hyArr[2];
            defaultVisit.mEmergency = hyArr[3];

            defaultVisit.aStart = defaultVisitTemplate.defaultVisitTimePm.Substring(0, 5);
            defaultVisit.aEnd = defaultVisitTemplate.defaultVisitTimePm.Substring(6, 5);
            defaultVisit.aSubsection = defaultVisitTemplate.segmentalDuration;
            defaultVisit.aScene = hyArr[0];
            defaultVisit.aOpen = hyArr[1];
            defaultVisit.aRoom = hyArr[2];
            defaultVisit.aEmergency = hyArr[3];

            defaultVisit.nStart = defaultVisitTemplate.defaultVisitTimeNight.Substring(0, 5);
            defaultVisit.nEnd = defaultVisitTemplate.defaultVisitTimeNight.Substring(6, 5);
            defaultVisit.nSubsection = defaultVisitTemplate.segmentalDuration;
            defaultVisit.nScene = hyArr[0];
            defaultVisit.nOpen = hyArr[1];
            defaultVisit.nRoom = hyArr[2];
            defaultVisit.nEmergency = hyArr[3];
            dcDefaultVisit.SetValue(defaultVisit);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {   
            //本来保存成功后清除一次就行了，不过为了预防新增或修改事件触发的过程中出现异常
            //导致面板有数据却无法编辑,导致新增的时候有默认数据，所以这里需要再清一次
            //清除医生数据
            dcDoctorInfo.ClearValue();
            pbPicture.Image = null;
            pbPicture.Refresh();
            pictureServiceFilePath = null;
            //清除排班数据
            dcDefaultVisit.ClearValue();
            tableLayoutPanel4.Enabled = false;
            panScheduling.Controls.Clear();

            setDefaultVisit();
            tableLayoutPanel4.Enabled = true;
            groupBox1.Enabled = true;
            doctorInfo = new DoctorInfoEntity();
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            //本来保存成功后清除一次就行了，不过为了预防新增或修改事件触发的过程中出现异常
            //导致面板有数据却无法编辑,导致新增的时候有默认数据，所以这里需要再清一次
            //清除医生数据
            dcDoctorInfo.ClearValue();
            pbPicture.Image = null;
            pbPicture.Refresh();
            pictureServiceFilePath = null;
            //清除排班数据
            dcDefaultVisit.ClearValue();
            tableLayoutPanel4.Enabled = false;
            panScheduling.Controls.Clear();

            doctorInfo = new DoctorInfoEntity();
            var selectedRow = gridView1.GetFocusedRow() as DoctorInfoEntity;
            if (selectedRow == null)
                return;
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/findById?id=" + selectedRow.id;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                doctorInfo = objT["result"].ToObject<DoctorInfoEntity>();
                doctorInfo.hospitalId = doctorInfo.dept.hospital.id;
                doctorInfo.deptId = doctorInfo.dept.id;
                dcDoctorInfo.SetValue(doctorInfo);
                //显示图片
                pictureServiceFilePath = doctorInfo.pictureUrl;
                WebClient web;
                if (pictureServiceFilePath != null && pictureServiceFilePath.Length > 0)
                {
                    web = new WebClient();
                    var bytes = web.DownloadData(pictureServiceFilePath);
                    this.pbPicture.Image = Bitmap.FromStream(new MemoryStream(bytes));
                }
                groupBox1.Enabled = true;
                setDefaultVisit();
                tableLayoutPanel4.Enabled = true;

                //获取已排班信息
                url = AppContext.AppConfig.serverUrl + "cms/doctor/findDoctorVisitingList?deptId=" + doctorInfo.dept.id + "&doctorId=" + selectedRow.id;
                data = HttpClass.httpPost(url);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    List<WorkingDayEntity> workingDayList = objT["result"].ToObject<List<WorkingDayEntity>>();
                    if (workingDayList.Count > 0)
                    {
                        setWorkingDay(workingDayList);
                    }
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //检验并取值
            if (!dcDoctorInfo.Validate())
            {
                return;
            }
            dcDoctorInfo.GetValue(doctorInfo);

            if (cbIgnoreHoliday.CheckState == CheckState.Checked)
                doctorInfo.ignoreHoliday = "0";
            else
                doctorInfo.ignoreHoliday = "1";

            if (cbIgnoreHoliday.CheckState == CheckState.Checked)
                doctorInfo.ignoreHoliday = "1";
            else doctorInfo.ignoreHoliday = "0";

            if (pictureServiceFilePath == null || pictureServiceFilePath.Length == 0)
            {
                dcDoctorInfo.ShowError(pbPicture, "请先上传文件");
                return;
            }
            doctorInfo.pictureUrl = pictureServiceFilePath;

            List<WorkingDayEntity> workingDayList = new List<WorkingDayEntity>();
            //获取排班信息
            int days = panScheduling.Controls.Count; //排班天数
            if (days > 0)
            {
                for (int i = 0; i < days; i++)
                {
                    GroupBox groupBoy = (GroupBox)panScheduling.Controls[i];//周几的面板
                    for (int period = 0; period < 3; period++)//循环上午、下午、晚上
                    {
                        TableLayoutPanel tlp = (TableLayoutPanel)groupBoy.Controls[period];
                        if (tlp.Enabled)
                        {
                            Panel pCb = (Panel)tlp.GetControlFromPosition(0, 1);
                            CheckBox cbIsUse = (CheckBox)pCb.Controls[0];
                            Panel pAuto = (Panel)tlp.GetControlFromPosition(0, 2);
                            CheckBox cbAuto = (CheckBox)pAuto.Controls[0];
                            for (int r = 1; r < tlp.RowCount; r++)//行
                            {
                                WorkingDayEntity wordingDay = new WorkingDayEntity();
                                wordingDay.week = i + 1 + ""; //周几
                                wordingDay.period = period.ToString(); //0：上午，1：下午，2：晚上
                                if (cbIsUse.CheckState == CheckState.Checked)
                                    wordingDay.isUse = "0";
                                else
                                    wordingDay.isUse = "1";
                                if (cbAuto.CheckState == CheckState.Checked)
                                    wordingDay.autoSchedule = "0";
                                else
                                    wordingDay.autoSchedule = "1";

                                for (int c = 1; c < tlp.ColumnCount; c++)//列
                                {
                                    Panel panel = (Panel)tlp.GetControlFromPosition(c, r);
                                    TextEdit te = (TextEdit)panel.Controls[0];
                                    if (c == 1)
                                        wordingDay.beginTime = te.Text;
                                    else if (c == 2)
                                        wordingDay.endTime = te.Text;
                                    else if (c == 3)
                                        wordingDay.numSource = te.Text;
                                    else if (c == 4)
                                        wordingDay.numOpen = te.Text;
                                    else if (c == 5)
                                        wordingDay.numClinic = te.Text;
                                    else if (c == 6)
                                        wordingDay.numYj = te.Text;
                                }
                                workingDayList.Add(wordingDay);
                            }
                        }
                    }
                }
            }
            String workStr = Newtonsoft.Json.JsonConvert.SerializeObject(workingDayList);

            String param =  PackReflectionEntity<DoctorInfoEntity>.GetEntityToRequestParameters(doctorInfo, true);
            param += "&workStr=" + workStr;
            //请求接口
            String url = AppContext.AppConfig.serverUrl + "cms/doctor/save?";
            String data = HttpClass.httpPost(url, param);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                MessageBoxUtils.Hint("保存成功！");
                pageControl1_Query(pageControl1.CurrentPage, pageControl1.PageSize);
                groupBox1.Enabled = false;
                //清除医生数据
                dcDoctorInfo.ClearValue();
                pbPicture.Image = null;
                pbPicture.Refresh();
                pictureServiceFilePath = null;
                //清除排班数据
                dcDefaultVisit.ClearValue();
                tableLayoutPanel4.Enabled = false;
                panScheduling.Controls.Clear();
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow() as DoctorInfoEntity;
            if (selectedRow == null)
                return;
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除吗?", "删除医生信息", messButton);

            if (dr == DialogResult.OK)
            {
                String param = "?id=" + selectedRow.id;
                String url = AppContext.AppConfig.serverUrl + "cms/doctor/delete" + param;
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    MessageBoxUtils.Hint("删除成功!");
                    SearchData(pageControl1.CurrentPage, pageControl1.PageSize);
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var edit = new RichEditorForm();
            edit.text = doctorInfo.synopsis;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                doctorInfo.synopsis = edit.text;
            }
        }

        private void lueHospital_EditValueChanged(object sender, EventArgs e)
        {
            if (lueHospital.EditValue == null || lueHospital.EditValue.ToString().Length == 0)
            {
                lueDept.Properties.DataSource = null;
                return;
            }
            HospitalInfoEntity hospitalInfo = lueHospital.GetSelectedDataRow() as HospitalInfoEntity;
            //查询科室下拉框数据
            String url = AppContext.AppConfig.serverUrl + "cms/dept/findAll?hospital.code=" + hospitalInfo.code;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                List<DeptEntity> deptLsit = objT["result"].ToObject<List<DeptEntity>>();
                DeptEntity dept = new DeptEntity();
                dept.id = "0";
                dept.name = "无";
                deptLsit.Insert(0, dept);
                lueDept.Properties.DataSource = deptLsit;
                lueDept.Properties.DisplayMember = "name";
                lueDept.Properties.ValueMember = "id";
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
                return;
            }
        }

        private void menuControl2_MenuItemClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            hospitalId = label.Tag.ToString();
            deptId = label.Name;
            SearchData(1, pageControl1.PageSize);
        }

        private void DoctorSettingsForm_Paint(object sender, PaintEventArgs e)
        {
            float tlpWidth = this.Width * 30 / 100;
            if (tlpWidth < 460)
                tableLayoutPanel1.ColumnStyles[1].Width = tlpWidth;
            else
                tableLayoutPanel1.ColumnStyles[1].Width = 460;
        }

        private void buttonControl2_Click(object sender, EventArgs e)
        {
            //数据验证
            //if (!dcDefaultVisit.Validate())
            //{
            //    return;
            //}
            //清除排班数据
            panScheduling.Controls.Clear();
            CheckState morning = cbMorning.CheckState;
            CheckState afternoon = cbAfternoon.CheckState;
            CheckState night = cbNight.CheckState;
            if (morning != CheckState.Checked && afternoon != CheckState.Checked
                && night != CheckState.Checked)
            {
                return;
            }
            //获取默认排班数据
            defaultVisit = new DefaultVisitEntity();
            dcDefaultVisit.GetValue(defaultVisit);

            //分段数量
            int rowMorningNum = 0;
            int rowAfternoonNum = 0;
            int rowNightNum = 0;

            #region 计算分段数量
            //计算早上的分段数量
            if (morning == CheckState.Checked)
            {
                if(defaultVisit.mStart.Trim().Length==0||defaultVisit.mEnd.Trim().Length==0
                    || defaultVisit.mSubsection.Trim().Length == 0)
                {
                    MessageBoxUtils.Hint("上午的设置不能为空");
                    return;
                }
                String[] startArr = defaultVisit.mStart.Split(new char[] { ':', '：' });
                String[] endArr = defaultVisit.mEnd.Split(new char[] { ':', '：' });
                if (startArr.Length != 2)
                {
                    MessageBoxUtils.Hint("上午的开始时间设置有误");
                    return;
                }
                if (endArr.Length != 2)
                {
                    MessageBoxUtils.Hint("上午的结束时间设置有误");
                    return;
                }
                DateTime d1 = new DateTime(2004, 1, 1, int.Parse(startArr[0]), int.Parse(startArr[1]), 00);
                DateTime d2 = new DateTime(2004, 1, 1, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                TimeSpan d3 = d2.Subtract(d1);
                int minute = d3.Hours * 60 + d3.Minutes;
                if (minute <= 0)
                {
                    MessageBoxUtils.Hint("上午结束时间不能小于或等于开始时间");
                    return;
                }
                if (minute < int.Parse(defaultVisit.mSubsection))
                {
                    MessageBoxUtils.Hint("上午分段时间大于总时间");
                    return;
                }
                rowMorningNum = minute / int.Parse(defaultVisit.mSubsection);
            }

            //计算下午的分段数量
            if (afternoon == CheckState.Checked)
            {
                if (defaultVisit.aStart.Trim().Length == 0 || defaultVisit.aEnd.Trim().Length == 0
                    || defaultVisit.aSubsection.Trim().Length == 0)
                {
                    MessageBoxUtils.Hint("下午的设置不能为空");
                    return;
                }
                String[] startArr = defaultVisit.aStart.Split(new char[] { ':', '：' });
                String[] endArr = defaultVisit.aEnd.Split(new char[] { ':', '：' });
                if (startArr.Length != 2)
                {
                    MessageBoxUtils.Hint("下午的开始时间设置有误");
                    return;
                }
                if (endArr.Length != 2)
                {
                    MessageBoxUtils.Hint("下午的结束时间设置有误");
                    return;
                }
                DateTime d1 = new DateTime(2004, 1, 1, int.Parse(startArr[0]), int.Parse(startArr[1]), 00);
                DateTime d2 = new DateTime(2004, 1, 1, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                TimeSpan d3 = d2.Subtract(d1);
                int minute = d3.Hours * 60 + d3.Minutes;
                if (minute <= 0)
                {
                    MessageBoxUtils.Hint("下午结束时间不能小于或等于开始时间");
                    return;
                }
                if (minute < int.Parse(defaultVisit.aSubsection))
                {
                    MessageBoxUtils.Hint("下午分段时间大于总时间");
                    return;
                }
                rowAfternoonNum = minute / int.Parse(defaultVisit.aSubsection);
            }

            //计算晚上的分段数量
            if (night == CheckState.Checked)
            {
                if (defaultVisit.nStart.Trim().Length == 0 || defaultVisit.nEnd.Trim().Length == 0
                    || defaultVisit.nSubsection.Trim().Length == 0)
                {
                    MessageBoxUtils.Hint("晚上的设置不能为空");
                    return;
                }
                String[] startArr = defaultVisit.nStart.Split(new char[] { ':', '：' });
                String[] endArr = defaultVisit.nEnd.Split(new char[] { ':', '：' });
                if (startArr.Length != 2)
                {
                    MessageBoxUtils.Hint("晚上的开始时间设置有误");
                    return;
                }
                if (endArr.Length != 2)
                {
                    MessageBoxUtils.Hint("晚上的结束时间设置有误");
                    return;
                }
                DateTime d1 = new DateTime(2004, 1, 1, int.Parse(startArr[0]), int.Parse(startArr[1]), 00);
                DateTime d2 = new DateTime();
                if (endArr[0].Equals("24"))
                    d2 = new DateTime(2004, 1, 2, 00, int.Parse(endArr[1]), 00);
                else
                    d2 = new DateTime(2004, 1, 1, int.Parse(endArr[0]), int.Parse(endArr[1]), 00);
                TimeSpan d3 = d2.Subtract(d1);
                int minute = d3.Hours * 60 + d3.Minutes;
                if (minute <= 0)
                {
                    MessageBoxUtils.Hint("晚上结束时间不能小于或等于开始时间");
                    return;
                }
                if (minute < int.Parse(defaultVisit.nSubsection))
                {
                    MessageBoxUtils.Hint("晚上分段时间大于总时间");
                    return;
                }
                rowNightNum = minute / int.Parse(defaultVisit.nSubsection);
            }
            #endregion

            int gbY = 0; //groupBox的Y轴位置

            #region 生成周一到周日的排班
            //生成周一到周日的排班
            for (int i = 1; i <= 7; i++)
            {
                if (i != 1) gbY += 40;
                int blpY = 20; //TableLayoutPanel的y轴位置
                GroupBox groupBox = new GroupBox();
                groupBox.Width = 415;
                groupBox.Location = new System.Drawing.Point(0, gbY);
                groupBox.AutoSize = true;
                switch (i)
                {
                    case 1:
                        groupBox.Text = "周一";
                        break;
                    case 2:
                        groupBox.Text = "周二";
                        break;
                    case 3:
                        groupBox.Text = "周三";
                        break;
                    case 4:
                        groupBox.Text = "周四";
                        break;
                    case 5:
                        groupBox.Text = "周五";
                        break;
                    case 6:
                        groupBox.Text = "周六";
                        break;
                    case 7:
                        groupBox.Text = "周日";
                        break;
                    default: break;
                }

                for (int j = 0; j < 3; j++)
                {
                    //j=0:上午 j=1:下午 j=2:晚上
                    TableLayoutPanel tlpMorning = new TableLayoutPanel();
                    int row = 0;//行数(包括标题)
                    DateTime dt1 = new DateTime();//开始时间
                    DateTime dt2 = new DateTime();//结束时间
                    String timeInterval = ""; //
                    if (j == 0)
                    {
                        if (rowMorningNum > 3) row = rowMorningNum + 1;
                        else row = 4;
                        dt1 = DateTime.Parse("2008-08-08 " + defaultVisit.mStart + ":00");
                        dt2 = dt1.AddMinutes(int.Parse(defaultVisit.mSubsection));
                        timeInterval = "上午";
                        if(morning != CheckState.Checked){
                            tlpMorning.Enabled = false;
                        }
                    }
                    else if (j == 1)
                    {
                        if (rowAfternoonNum > 3) row = rowAfternoonNum + 1;
                        else row = 4;
                        dt1 = DateTime.Parse("2008-08-08 " + defaultVisit.aStart + ":00");
                        dt2 = dt1.AddMinutes(int.Parse(defaultVisit.aSubsection));
                        timeInterval = "下午";
                        if (afternoon != CheckState.Checked)
                        {
                            tlpMorning.Enabled = false;
                        }
                    }
                    else if (j == 2)
                    {
                        if (rowNightNum > 3) row = rowNightNum + 1;
                        else row = 4;
                        dt1 = DateTime.Parse("2008-08-08 " + defaultVisit.nStart + ":00");
                        dt2 = dt1.AddMinutes(int.Parse(defaultVisit.nSubsection));
                        timeInterval = "晚上";
                        if (night != CheckState.Checked)
                        {
                            tlpMorning.Enabled = false;
                        }
                    }
                    
                    tlpMorning.ColumnCount = 7;
                    tlpMorning.RowCount = row;

                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));

                    for (int n = 0; n < row; n++)
                    {
                        tlpMorning.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
                    }
                    tlpMorning.Size = new System.Drawing.Size(342, row * 30);
                    //标题栏
                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    //tlpMorning.Controls.Add(panel);
                    Label label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "开始";
                    tlpMorning.Controls.Add(label, 1, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "结束";
                    tlpMorning.Controls.Add(label, 2, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "现场";
                    tlpMorning.Controls.Add(label, 3, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "公开";
                    tlpMorning.Controls.Add(label, 4, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "诊间";
                    tlpMorning.Controls.Add(label, 5, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "应急";
                    tlpMorning.Controls.Add(label, 6, 0);

                    bool teEnabled = true;//当行数小于3的时候，空白文本框需要设为不可选
                    String start = "";
                    String end = "";
                    String scene = "";
                    String open = "";
                    String room = "";
                    String emergency = "";
                    CheckState checkState = CheckState.Unchecked;
                    if (j == 0 && morning == CheckState.Checked)
                    {
                        scene = defaultVisit.mScene;
                        open = defaultVisit.mOpen;
                        room = defaultVisit.mRoom;
                        emergency = defaultVisit.mEmergency;
                        checkState = CheckState.Checked;
                    }
                    if(j==1 && afternoon == CheckState.Checked){
                        scene = defaultVisit.aScene;
                        open = defaultVisit.aOpen;
                        room = defaultVisit.aRoom;
                        emergency = defaultVisit.aEmergency;
                        checkState = CheckState.Checked;
                    }
                    if(j==2 && night == CheckState.Checked){
                        scene = defaultVisit.nScene;
                        open = defaultVisit.nOpen;
                        room = defaultVisit.nRoom;
                        emergency = defaultVisit.nEmergency;
                        checkState = CheckState.Checked;
                    }
                    for (int r = 1; r < row; r++)
                    {
                        if (j == 0 && morning == CheckState.Checked)
                        {
                            start = dt1.ToString().Substring(11, 5);
                            if (dt2.ToString().Substring(11, 2).Equals("00"))
                                end = "24" + dt2.ToString().Substring(13, 3);
                            else
                                end = dt2.ToString().Substring(11, 5);
                            dt1 = dt2;
                            dt2 = dt1.AddMinutes(int.Parse(defaultVisit.mSubsection));
                            if (r > rowMorningNum)
                            {
                                start = "";
                                end = "";
                                scene = "";
                                open = "";
                                room = "";
                                emergency = "";
                                teEnabled = false;
                            }
                        }
                        if (j == 1 && afternoon == CheckState.Checked)
                        {
                            start = dt1.ToString().Substring(11, 5);
                            if (dt2.ToString().Substring(11, 2).Equals("00"))
                                end = "24" + dt2.ToString().Substring(13, 3);
                            else
                                end = dt2.ToString().Substring(11, 5);
                            dt1 = dt2;
                            dt2 = dt1.AddMinutes(int.Parse(defaultVisit.aSubsection));
                            if (r > rowAfternoonNum)
                            {
                                start = "";
                                end = "";
                                scene = "";
                                open = "";
                                room = "";
                                emergency = "";
                                teEnabled = false;
                            }
                        }
                        if (j == 2 && night == CheckState.Checked)
                        {
                            start = dt1.ToString().Substring(11, 5);
                            if (dt2.ToString().Substring(11, 2).Equals("00"))
                                end = "24" + dt2.ToString().Substring(13, 3);
                            else
                                end = dt2.ToString().Substring(11, 5);
                            dt1 = dt2;
                            dt2 = dt1.AddMinutes(int.Parse(defaultVisit.nSubsection));
                            if (r > rowNightNum)
                            {
                                start = "";
                                end = "";
                                scene = "";
                                open = "";
                                room = "";
                                emergency = "";
                                teEnabled = false;
                            }
                        }
                        for (int c = 0; c < 7; c++)
                        {
                            if (r == 1 && c == 0)
                            {
                                //第一行第一列
                                panel = new Panel();
                                panel.Dock = DockStyle.Fill;
                                CheckBox checkBox = new CheckBox();
                                checkBox.Dock = DockStyle.Fill;
                                checkBox.Font = new Font("微软雅黑", 10);
                                checkBox.Text = timeInterval;
                                checkBox.CheckState = checkState;
                                panel.Controls.Add(checkBox);
                                tlpMorning.Controls.Add(panel, 0, 1);
                            }
                            else if (r == 2 && c == 0)
                            {
                                //第二行第一列
                                //需跨1行
                                panel = new Panel();
                                panel.Dock = DockStyle.Fill;
                                CheckBox checkBox = new CheckBox();
                                checkBox.Dock = DockStyle.Fill;
                                checkBox.Font = new Font("微软雅黑", 10);
                                checkBox.ForeColor = Color.FromArgb(255, 153, 102);
                                checkBox.Text = "自动排班";
                                panel.Controls.Add(checkBox);
                                tlpMorning.SetRowSpan(panel, 2);
                                tlpMorning.Controls.Add(panel, c, r);
                            }
                            else if (c == 0)
                            {
                                //不做处理
                            }
                            else
                            {
                                if (c == 1)
                                {
                                    //第二列
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = start;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 2)
                                {
                                    //第三列
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = end;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 3)
                                {
                                    //第四列 现场
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = scene;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 4)
                                {
                                    //第五列 公开
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = open;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 5)
                                {
                                    //第六列 诊间
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = room;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 6)
                                {
                                    //第七列 应急
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = emergency;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                            }
                        }
                    }
                    tlpMorning.Location = new System.Drawing.Point(0, blpY);
                    blpY += tlpMorning.Height;
                    gbY += tlpMorning.Height;
                    groupBox.Controls.Add(tlpMorning);
                }
                panScheduling.Controls.Add(groupBox);
            }
            #endregion
        }

        private void setWorkingDay(List<WorkingDayEntity> workingDayList)
        {
            //清除排班数据
            panScheduling.Controls.Clear();


            int gbY = 0; //groupBox的Y轴位置

            #region 生成周一到周日的排班
            //生成周一到周日的排班
            for (int i = 1; i <= 7; i++)
            {
                if (i != 1) gbY += 40;
                int blpY = 20; //TableLayoutPanel的y轴位置
                GroupBox groupBox = new GroupBox();
                groupBox.Width = 415;
                groupBox.Location = new System.Drawing.Point(0, gbY);
                groupBox.AutoSize = true;
                switch (i)
                {
                    case 1:
                        groupBox.Text = "周一";
                        break;
                    case 2:
                        groupBox.Text = "周二";
                        break;
                    case 3:
                        groupBox.Text = "周三";
                        break;
                    case 4:
                        groupBox.Text = "周四";
                        break;
                    case 5:
                        groupBox.Text = "周五";
                        break;
                    case 6:
                        groupBox.Text = "周六";
                        break;
                    case 7:
                        groupBox.Text = "周日";
                        break;
                    default: break;
                }

                for (int j = 0; j < 3; j++)
                {
                    //当前TableLayoutPanel的数量
                    List<WorkingDayEntity> wdwpList = getWorkingDayData(workingDayList, i.ToString(), j.ToString());
                    //多少行数据
                    int rowNum = wdwpList.Count;
                    //j=0:上午 j=1:下午 j=2:晚上
                    TableLayoutPanel tlpMorning = new TableLayoutPanel();
                    if (rowNum == 0) tlpMorning.Enabled = false;
                    int row = 0;//行数(包括标题)
                    String timeInterval = ""; //
                    if (rowNum > 3) row = rowNum + 1;
                    else row = 4;
                    CheckState checkState = CheckState.Unchecked;
                    CheckState checkAuto = CheckState.Unchecked;
                    if (rowNum==0)
                        checkState = CheckState.Unchecked;
                    else if(wdwpList[0].isUse.Equals("0"))
                        checkState = CheckState.Checked;
                    else
                        checkState = CheckState.Unchecked;
                    if (rowNum == 0)
                        checkAuto = CheckState.Unchecked;
                    else if(wdwpList[0].autoSchedule.Equals("0"))
                        checkAuto = CheckState.Checked;
                    else
                        checkAuto = CheckState.Unchecked;
                    if (j == 0)
                    {
                        timeInterval = "上午";
                    }
                    else if (j == 1)
                    {
                        timeInterval = "下午";
                    }
                    else if (j == 2)
                    {
                        timeInterval = "晚上";
                    }

                    tlpMorning.ColumnCount = 7;
                    tlpMorning.RowCount = row;

                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
                    tlpMorning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));

                    for (int n = 0; n < row; n++)
                    {
                        tlpMorning.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
                    }
                    tlpMorning.Size = new System.Drawing.Size(342, row * 30);
                    //标题栏
                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    //tlpMorning.Controls.Add(panel);
                    Label label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "开始";
                    tlpMorning.Controls.Add(label, 1, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "结束";
                    tlpMorning.Controls.Add(label, 2, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "现场";
                    tlpMorning.Controls.Add(label, 3, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "公开";
                    tlpMorning.Controls.Add(label, 4, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "诊间";
                    tlpMorning.Controls.Add(label, 5, 0);
                    label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.BottomCenter;
                    label.Font = new Font("微软雅黑", 10);
                    label.Text = "应急";
                    tlpMorning.Controls.Add(label, 6, 0);

                    bool teEnabled = true;//当行数小于3的时候，空白文本框需要设为不可选
                    String start = "";
                    String end = "";
                    String scene = "";
                    String open = "";
                    String room = "";
                    String emergency = "";
                    
                    for (int r = 1; r < row; r++)
                    {
                        if (r > rowNum)
                        {
                            start = "";
                            end = "";
                            scene = "";
                            open = "";
                            room = "";
                            emergency = "";
                            teEnabled = false;
                        }
                        else
                        {
                            start = wdwpList[r - 1].beginTime;
                            end = wdwpList[r - 1].endTime;
                            scene = wdwpList[r - 1].numSource;
                            open = wdwpList[r - 1].numOpen;
                            room = wdwpList[r - 1].numClinic;
                            emergency = wdwpList[r - 1].numYj;
                            teEnabled = true;
                        }
                        for (int c = 0; c < 7; c++)
                        {
                            if (r == 1 && c == 0)
                            {
                                //第一行第一列
                                panel = new Panel();
                                panel.Dock = DockStyle.Fill;
                                CheckBox checkBox = new CheckBox();
                                checkBox.Dock = DockStyle.Fill;
                                checkBox.Font = new Font("微软雅黑", 10);
                                checkBox.Text = timeInterval;
                                checkBox.CheckState = checkState;
                                panel.Controls.Add(checkBox);
                                tlpMorning.Controls.Add(panel, 0, 1);
                            }
                            else if (r == 2 && c == 0)
                            {
                                //第二行第一列
                                //需跨1行
                                panel = new Panel();
                                panel.Dock = DockStyle.Fill;
                                CheckBox checkBox = new CheckBox();
                                checkBox.Dock = DockStyle.Fill;
                                checkBox.Font = new Font("微软雅黑", 10);
                                checkBox.ForeColor = Color.FromArgb(255, 153, 102);
                                checkBox.Text = "自动排班";
                                checkBox.CheckState = checkAuto;
                                panel.Controls.Add(checkBox);
                                tlpMorning.SetRowSpan(panel, 2);
                                tlpMorning.Controls.Add(panel, c, r);
                            }
                            else if (c == 0)
                            {
                                //不做处理
                            }
                            else
                            {
                                if (c == 1)
                                {
                                    //第二列
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = start;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 2)
                                {
                                    //第三列
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = end;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 3)
                                {
                                    //第四列 现场
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = scene;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 4)
                                {
                                    //第五列 公开
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = open;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 5)
                                {
                                    //第六列 诊间
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = room;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                                else if (c == 6)
                                {
                                    //第七列 应急
                                    panel = new Panel();
                                    panel.Dock = DockStyle.Fill;
                                    TextEdit textEdit = new TextEdit();
                                    textEdit.Properties.AutoHeight = false;
                                    textEdit.Dock = DockStyle.Fill;
                                    textEdit.Font = new Font("微软雅黑", 10);
                                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    textEdit.Text = emergency;
                                    textEdit.Enabled = teEnabled;
                                    panel.Controls.Add(textEdit);
                                    tlpMorning.Controls.Add(panel, c, r);
                                }
                            }
                        }
                    }
                    tlpMorning.Location = new System.Drawing.Point(0, blpY);
                    blpY += tlpMorning.Height;
                    gbY += tlpMorning.Height;
                    groupBox.Controls.Add(tlpMorning);
                }
                panScheduling.Controls.Add(groupBox);
            }
            #endregion
        }

        /// <summary>
        /// 获取周几上午||下午||晚上的排班数据
        /// </summary>
        /// <param name="workingDayList">排班数据</param>
        /// <param name="week">周几</param>
        /// <param name="period">0：上午，1：下午，2：晚上</param>
        /// <returns></returns>
        private List<WorkingDayEntity> getWorkingDayData(List<WorkingDayEntity> workingDayList, String week, String period)
        {
            List<WorkingDayEntity> workingDayByWeekList = new List<WorkingDayEntity>();
            foreach (WorkingDayEntity workingDay in workingDayList)
            {
                if (workingDay.week.Equals(week)&&workingDay.period.Equals(period))
                    workingDayByWeekList.Add(workingDay);
            }
            return workingDayByWeekList;
        }
    }
}
