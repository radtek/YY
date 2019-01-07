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
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json;
using Xr.Http;

namespace Xr.RtManager
{
    public partial class RoleEdit : Form
    {
        public RoleEdit()
        {
            InitializeComponent();
        }

        public RoleEntity role { get; set; }
        private String oldName { get; set; }

        private void RoleEdit_Load(object sender, EventArgs e)
        {
            dcRole.DataType = typeof(RoleEntity);

            //获取下拉框数据
            String url = AppContext.Session.serverUrl + "sys/sysRole/getDropDownData";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                treeParent.Properties.DataSource = objT["result"]["officeList"].ToObject<List<OfficeEntity>>();  
                treeParent.Properties.TreeList.KeyFieldName = "id";//设置ID  
                treeParent.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                treeParent.Properties.DisplayMember = "name";  //要在树里展示的
                treeParent.Properties.ValueMember = "id";    //对应的value

                lueDataScope.Properties.DataSource = objT["result"]["dataScopeDictList"].ToObject<List<OfficeEntity>>();
                lueDataScope.Properties.DisplayMember = "name";
                lueDataScope.Properties.ValueMember = "code";

                treeList1.DataSource = objT["result"]["menuList"].ToObject<List<MenuEntity>>();
                treeList1.KeyFieldName = "id";//设置ID  
                treeList1.ParentFieldName = "parentId";//设置PreID   
                treeList1.OptionsView.ShowCheckBoxes = true;  //显示多选框
                treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true; //选中父节点，子节点也会全部选中
                treeList1.OptionsBehavior.Editable = false; //数据不可编辑
                treeList1.ExpandAll();//展开所有节点
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }

            if (role != null)
            {
                url = AppContext.Session.serverUrl + "sys/sysRole/getRole?roleId=" + role.id;
                data = HttpClass.httpPost(url);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    role = objT["result"].ToObject<RoleEntity>();
                    dcRole.SetValue(role);
                    String[] roleArray = role.menuIds.Split(',');
                    for (int i = 0; i < roleArray.Count(); i++)
                    {
                        SetNodeChecked(roleArray[i], treeList1.Nodes);
                    }
                    oldName = role.name;
                }
                else
                {
                    MessageBox.Show(objT["message"].ToString());
                }
            }
            else
            {
                role = new RoleEntity();
            }
        }

        private List<String> lstCheckedOfficeID = new List<String>();//选择局ID集合
        /// <summary>
        /// 获取选择状态的数据主键ID集合
        /// </summary>
        /// <param name="parentNode">父级节点</param>
        private void GetCheckedOfficeID(TreeListNode parentNode)
        {
            if (parentNode.Nodes.Count == 0)
            {
                return;//递归终止
            }

            foreach (TreeListNode node in parentNode.Nodes)
            {
                if (node.CheckState == CheckState.Checked || node.CheckState == CheckState.Indeterminate)
                {
                    MenuEntity menu = treeList1.GetDataRecordByNode(node) as MenuEntity;
                    if (menu != null)
                    {
                        lstCheckedOfficeID.Add(menu.id);
                    }


                }
                GetCheckedOfficeID(node);
            }
        }

        /// <summary>
        /// 设置CheckBox选中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        private void SetNodeChecked(String key, TreeListNodes node)
        {
            foreach (TreeListNode childeNode in node)
            {
                MenuEntity data = treeList1.GetDataRecordByNode(childeNode) as MenuEntity;
                if (data.id == key)
                {
                    childeNode.Checked = true;
                }
                if (childeNode.HasChildren)
                {
                    SetNodeChecked(key, childeNode.Nodes);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dcRole.Validate())
            {
                return;
            }
            dcRole.GetValue(role);
            //获取选中的权限
            this.lstCheckedOfficeID.Clear();
            if (treeList1.Nodes.Count > 0)
            {
                foreach (TreeListNode root in treeList1.Nodes)
                {
                    if (root.CheckState == CheckState.Checked || root.CheckState == CheckState.Indeterminate)
                    {
                        MenuEntity menu = treeList1.GetDataRecordByNode(root) as MenuEntity;
                        if (menu != null)
                        {
                            lstCheckedOfficeID.Add(menu.id);
                        }
                    }
                    GetCheckedOfficeID(root);
                }
            }

            string idStr = string.Empty;
            foreach (String id in lstCheckedOfficeID)
            {
                idStr += id + ",";
            }
            if(idStr.Length!=0)
                idStr = idStr.Substring(0, idStr.Length-1);
            role.menuIds = idStr;
            //string strJson = JsonConvert.SerializeObject(test);
            String param = "?" + PackReflectionEntity<RoleEntity>.GetEntityToRequestParameters(role, true) + "&&oldName=" + oldName;
            String url = AppContext.Session.serverUrl + "sys/sysRole/save" + param;
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
