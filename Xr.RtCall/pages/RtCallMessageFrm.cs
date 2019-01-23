﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Xr.Http.RestSharp;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Xr.RtCall.pages
{
    public partial class RtCallMessageFrm : UserControl
    {
        public SynchronizationContext _context;
        public RtCallMessageFrm()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }
        #region 完成下一位/过号下一位
        /// <summary>
        /// 完成下一位/过号下一位
        /// </summary>
        /// <param name="triageId">分诊记录主键，第一次可空</param>
        /// <param name="type">操作类型：0完成、1过号</param>
        public void PatientOkAndNext(string triageId,string type)
        {
            try
            {
                Dictionary<string, string> prament = new Dictionary<string, string>();
                prament.Add("triageId", triageId);
                prament.Add("Type", type);
                Xr.RtCall.Model.RestSharpHelper.ReturnResult<List<string>>("api/sch/clinicCall/inPlace", prament, Method.POST,
                result =>
                {
                    LogClass.WriteLog("请求结果：" + string.Join(",", result.Data.ToArray()));
                    switch (result.ResponseStatus)
                    {
                        case ResponseStatus.Completed:
                            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                JObject objT = JObject.Parse(string.Join(",", result.Data.ToArray()));
                                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                                {
                                }
                                else
                                {
                                    MessageBox.Show(objT["message"].ToString());
                                }
                            }
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("获取完成下一位/过号下一位错误信息：" + ex.Message);
            }
        }
        #endregion
        #region 过号下一位
        /// <summary>
        /// 过号下一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinbutNew_Click(object sender, EventArgs e)
        {
            PatientOkAndNext("","1");
        }
        #endregion 
        #region 患者到诊
        /// <summary>
        /// 患者到诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButton1_Click(object sender, EventArgs e)
        {
            PatientOkAndNext("", "0");
        }
        #endregion 
    }
}
