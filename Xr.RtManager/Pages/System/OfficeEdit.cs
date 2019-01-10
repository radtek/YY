using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Http;

namespace Xr.RtManager
{
    public partial class OfficeEdit : Form
    {
        public OfficeEdit()
        {
            InitializeComponent();
        }

        public OfficeEntity office { get; set; }

        private void OfficeEdit_Load(object sender, EventArgs e)
        {
            dcOffice.DataType = typeof(OfficeEntity);

            //获取下拉框数据
            String url = AppContext.AppConfig.serverUrl + "sys/sysOffice/getDropDownData";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                treeParent.Properties.DataSource = objT["result"]["officeList"].ToObject<List<OfficeEntity>>();  
                treeParent.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeParent.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeParent.Properties.DisplayMember = "name";  //要在树里展示的
                treeParent.Properties.ValueMember = "id";    //对应的value

                treeArea.Properties.DataSource = objT["result"]["areaList"].ToObject<List<OfficeEntity>>();
                treeArea.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeArea.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeArea.Properties.DisplayMember = "name";  //要在树里展示的
                treeArea.Properties.ValueMember = "id";    //对应的value

                lueType.Properties.DataSource = objT["result"]["typeDictList"].ToObject<List<OfficeEntity>>();
                lueType.Properties.DisplayMember = "name";
                lueType.Properties.ValueMember = "code";

                lueGrade.Properties.DataSource = objT["result"]["gradeDictList"].ToObject<List<OfficeEntity>>();
                lueGrade.Properties.DisplayMember = "name";
                lueGrade.Properties.ValueMember = "code";

                if (office != null)
                {
                    if (office.id != null)
                    {
                        url = AppContext.AppConfig.serverUrl + "sys/sysOffice/getOffice?officeId=" + office.id;
                        data = HttpClass.httpPost(url);
                        objT = JObject.Parse(data);
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            office = objT["result"].ToObject<OfficeEntity>();
                        }
                        else
                        {
                            MessageBox.Show(objT["message"].ToString());
                        }
                    }
                    dcOffice.SetValue(office);
                    teName.Focus();
                }
                else
                {
                    office = new OfficeEntity();
                }
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcOffice.Validate())
            {
                return;
            }
            dcOffice.GetValue(office);
            String param = "?" + PackReflectionEntity<OfficeEntity>.GetEntityToRequestParameters(office, true);
            String url = AppContext.AppConfig.serverUrl + "sys/sysOffice/save" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
