using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.Http;
using Xr.RtScreen.Models;

namespace Xr.RtScreen
{
    public partial class SettingFrm : Form
    {
        public SettingFrm()
        {
            InitializeComponent();
            treeClinc.EditValue = "";
            treeKeshi.EditValue = "";
            treeHostile.EditValue = "";
            GetHostile();
            //GetInforSetting();
        }
        #region 取消按钮
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion 
        #region 确定按钮
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl1_Click(object sender, EventArgs e)
        {
            if (this.treeHostile.EditValue == "")
            {
                Xr.Common.MessageBoxUtils.Show("请选择医院", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                return;
            }
            if (treeKeshi.EditValue == "")
            {
                Xr.Common.MessageBoxUtils.Show("请选择科室", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                return;
            }
            if (treeClinc.EditValue == "")
            {
                Xr.Common.MessageBoxUtils.Show("请选择诊室", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                return;
            }
            SaveConfigSeting(treeHostile.EditValue.ToString(), treeClinc.EditValue.ToString(), treeKeshi.EditValue.ToString(), "1");
            Application.ExitThread();
            Application.Exit();
            Application.Restart();
            System.Diagnostics.Process.GetCurrentProcess().Kill(); 
        }
        #endregion 
        #region 保存信息到本地配置文件中
        /// <summary>
        /// 保存信息到本地配置文件中
        /// </summary>
        private void SaveConfigSeting(string hostalCode,string ClincCode, string deptCode, string Setting)
        {
            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = Environment.CurrentDirectory +
                        @"\Xr.RtScreen.exe.config"
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                config.AppSettings.Settings["hospitalCode"].Value = hostalCode;
                config.AppSettings.Settings["deptCode"].Value = deptCode;
                config.AppSettings.Settings["clinicCode"].Value = ClincCode;
                config.AppSettings.Settings["Setting"].Value = Setting;
                config.Save(ConfigurationSaveMode.Full);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件
                Log4net.LogHelper.Info("保存配置文件内容成功：" + "医院编码：" + hostalCode + "科室编码：" + deptCode + "诊室编码" + ClincCode + "并且修改Setting值为1标识为不是第一次启动了");
            }
            catch (Exception ex)
            {
                Xr.Common.MessageBoxUtils.Show("保存配置时出错" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, null);
                Log4net.LogHelper.Error("保存配置文件错误信息：" + ex.Message);
            }
        }
        #endregion
        public List<dynamic> HostalList;
        public List<dynamic> DeptList;
        #region 获取医院信息
        public void GetHostile()
        {
            try
            {
                String urls = AppContext.AppConfig.serverUrl + InterfaceAddress.hostalInfo;
                String datas = HttpClass.httpPost(urls);
                JObject objTs = JObject.Parse(datas);
                if (string.Compare(objTs["state"].ToString(), "true", true) == 0)
                {
                    List<dynamic> list = objTs["result"].ToObject<List<dynamic>>();
                    HostalList = new List<dynamic>();
                    HostalList = list;
                    this.treeHostile.Properties.DataSource = list;
                    treeHostile.Properties.DisplayMember = "name";
                    treeHostile.Properties.ValueMember = "code";
                    treeHostile.EditValue = "";
                }
                else
                {
                    Xr.Common.MessageBoxUtils.Show(objTs["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, null);
                    Log4net.LogHelper.Error("修改配置文件时错误信息：" + objTs["message"].ToString());
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("获取医院信息错误：" + ex.Message);
            }
        }
        #endregion 
        #region 获取科室列表
        /// <summary>
        /// 科室列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeHostile_EditValueChanged(object sender, EventArgs e)
        {
            if (treeHostile.EditValue != "")
            {
                GetInforSetting(treeHostile.EditValue.ToString());
            }
        }
        /// <summary>
        /// 获取科室列表
        /// </summary>
        public void GetInforSetting(string hostalcode)
        {
            try
            {
                // 查询科室数据
                String urls = AppContext.AppConfig.serverUrl + InterfaceAddress.dept + "?hospital.code=" + hostalcode;
                String datas = HttpClass.httpPost(urls);
                JObject objTs = JObject.Parse(datas);
                if (string.Compare(objTs["state"].ToString(), "true", true) == 0)
                {
                    List<dynamic> list = objTs["result"].ToObject<List<dynamic>>();
                    DeptList = new List<dynamic>();
                    DeptList = list;
                    treeKeshi.Properties.DataSource = list;
                    treeKeshi.Properties.TreeList.KeyFieldName = "id";
                    treeKeshi.Properties.TreeList.ParentFieldName = "parentId";
                    treeKeshi.Properties.DisplayMember = "name";
                    treeKeshi.Properties.ValueMember = "code";
                    treeKeshi.EditValue = "";
                }
                //查询诊室数据
                //String urlss = AppContext.AppConfig.serverUrl + InterfaceAddress.clinc + "?code=" + AppContext.AppConfig.ClincCode;
                //String datass = HttpClass.httpPost(urlss);
                //JObject objTss = JObject.Parse(datass);
                //if (string.Compare(objTss["state"].ToString(), "true", true) == 0)
                //{
                //    HelperClass.clinicId = objTss["result"]["clinicId"].ToString();
                //}
            }
            catch (Exception ex)
            {
                Log4net.LogHelper.Error("第一次启动查询科室和诊室错误信息：" + ex.Message);
            }
        }
        #endregion 
        #region 
        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion 
        #region 诊室列表
        /// <summary>
        /// 诊室列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeKeshi_EditValueChanged(object sender, EventArgs e)
        {
            if (treeKeshi.EditValue != "")
            {
                string hospitalid = string.Join(",", from a in HostalList where a.code == treeHostile.EditValue select a.id);
                string deptid = string.Join(",", from s in DeptList where s.code == treeKeshi.EditValue select s.id);
                String urls = AppContext.AppConfig.serverUrl + InterfaceAddress.ClincInfo + "?hospital.id=" + hospitalid + "&dept.id=" + deptid;
                String datas = HttpClass.httpPost(urls);
                JObject objTs = JObject.Parse(datas);
                if (string.Compare(objTs["state"].ToString(), "true", true) == 0)
                {
                    List<dynamic> list = objTs["result"].ToObject<List<dynamic>>();
                    this.treeClinc.Properties.DataSource = list;
                    treeClinc.Properties.DisplayMember = "name";
                    treeClinc.Properties.ValueMember = "code";
                    treeClinc.EditValue = "";
                }
            }
        }
        #endregion 
    }
}
