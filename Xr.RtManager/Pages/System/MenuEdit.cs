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
    public partial class MenuEdit : Form
    {
        public MenuEdit()
        {
            InitializeComponent();
        }

        public MenuEntity menu { get; set; }

        private void MenuEdit_Load(object sender, EventArgs e)
        {
            dcMenu.DataType = typeof(MenuEntity);
            String extId = "";
            if(menu!=null && menu.id !=null)
                extId = menu.id;
            //获取下拉框数据
            String param = "?extId=" + extId;
            String url = AppContext.AppConfig.serverUrl + "sys/sysMenu/treeData" + param;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                treeParent.Properties.DataSource = objT["result"].ToObject<List<MenuEntity>>();  
                treeParent.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeParent.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeParent.Properties.DisplayMember = "name";  //要在树里展示的
                treeParent.Properties.ValueMember = "id";    //对应的value
                treeParent.EditValue = "1";

                if (menu != null)
                {
                    if(menu.id != null)
                    {
                        data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysMenu/getMenu?menuId=" + menu.id);
                        objT = JObject.Parse(data);
                        if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                        {
                            menu = objT["result"].ToObject<MenuEntity>();
                            dcMenu.SetValue(menu);
                        }
                        else
                        {
                            MessageBox.Show(objT["message"].ToString());
                        }
                    }
                    else
                    {
                        menu.isShow = "1";
                        dcMenu.SetValue(menu);
                    }
                }
                else
                {
                    menu = new MenuEntity();
                }
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcMenu.Validate())
            {
                return;
            }
            dcMenu.GetValue(menu);
            var index = treeParent.Properties.GetIndexByKeyValue(menu.parentId);
            List<MenuEntity> entityList = treeParent.Properties.TreeList.DataSource as List<MenuEntity>;
            menu.parentIds = entityList[index].parentIds + menu.parentId + ",";
            String param = PackReflectionEntity<MenuEntity>.GetEntityToRequestParameters(menu, true);
            String data = HttpClass.httpPost(AppContext.AppConfig.serverUrl + "sys/sysMenu/save?" + param);
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
