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
using System.Threading;
using Xr.Common;

namespace Xr.RtManager
{
    public partial class RoleEdit : Form
    {
        public RoleEdit()
        {
            InitializeComponent();
        }

        Xr.Common.Controls.OpaqueCommand cmd;

        public RoleEntity role { get; set; }
        private String oldName { get; set; }

        private void RoleEdit_Load(object sender, EventArgs e)
        {
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            cmd.ShowOpaqueLayer(0f);

            dcRole.DataType = typeof(RoleEntity);

            //获取下拉框数据
            String url = AppContext.AppConfig.serverUrl + "sys/sysRole/getDropDownData";
            this.DoWorkAsync( 500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    treeParent.Properties.DataSource = objT["result"]["officeList"].ToObject<List<OfficeEntity>>();
                    treeParent.Properties.TreeList.KeyFieldName = "id";//设置ID  
                    treeParent.Properties.TreeList.ParentFieldName = "parentId";//设置PreID   
                    treeParent.Properties.DisplayMember = "name";  //要在树里展示的
                    treeParent.Properties.ValueMember = "id";    //对应的value
                    treeParent.EditValue = "2";

                    lueDataScope.Properties.DataSource = objT["result"]["dataScopeDictList"].ToObject<List<OfficeEntity>>();
                    lueDataScope.Properties.DisplayMember = "name";
                    lueDataScope.Properties.ValueMember = "code";
                    if (objT["result"]["dataScopeDictList"].Count() > 0)
                        lueDataScope.EditValue = objT["result"]["dataScopeDictList"][0]["code"].ToString();

                    treeList1.DataSource = objT["result"]["menuList"].ToObject<List<MenuEntity>>();
                    treeList1.KeyFieldName = "id";//设置ID  
                    treeList1.ParentFieldName = "parentId";//设置PreID   
                    treeList1.OptionsView.ShowCheckBoxes = true;  //显示多选框
                    treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true; //选中父节点，子节点也会全部选中
                    treeList1.OptionsBehavior.Editable = false; //数据不可编辑
                    treeList1.ExpandAll();//展开所有节点

                    if (role != null)
                    {
                        url = AppContext.AppConfig.serverUrl + "sys/sysRole/getRole?roleId=" + role.id;
                        this.DoWorkAsync(500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                        {
                            data = HttpClass.httpPost(url);
                            return data;

                        }, null, (data2) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                        {
                            objT = JObject.Parse(data2.ToString());
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
                                cmd.HideOpaqueLayer();
                            }
                            else
                            {
                                cmd.HideOpaqueLayer();
                                MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                        });
                    }
                    else
                    {
                        role = new RoleEntity();
                        cmd.HideOpaqueLayer();
                    }
                }
                else
                {
                    cmd.HideOpaqueLayer();
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            });
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
            String url = AppContext.AppConfig.serverUrl + "sys/sysRole/save" + param;
            cmd.ShowOpaqueLayer();
            this.DoWorkAsync(500, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                cmd.HideOpaqueLayer();
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 多线程异步后台处理某些耗时的数据，不会卡死界面
        /// </summary>
        /// <param name="time">线程延迟多少</param>
        /// <param name="workFunc">Func委托，包装耗时处理（不含UI界面处理），示例：(o)=>{ 具体耗时逻辑; return 处理的结果数据 }</param>
        /// <param name="funcArg">Func委托参数，用于跨线程传递给耗时处理逻辑所需要的对象，示例：String对象、JObject对象或DataTable等任何一个值</param>
        /// <param name="workCompleted">Action委托，包装耗时处理完成后，下步操作（一般是更新界面的数据或UI控件），示列：(r)=>{ datagirdview1.DataSource=r; }</param>
        protected void DoWorkAsync(int time, Func<object, object> workFunc, object funcArg = null, Action<object> workCompleted = null)
        {
            var bgWorkder = new BackgroundWorker();


            //Form loadingForm = null;
            //System.Windows.Forms.Control loadingPan = null;
            bgWorkder.WorkerReportsProgress = true;
            bgWorkder.ProgressChanged += (s, arg) =>
            {
                if (arg.ProgressPercentage > 1) return;

            };

            bgWorkder.RunWorkerCompleted += (s, arg) =>
            {

                try
                {
                    bgWorkder.Dispose();

                    if (workCompleted != null)
                    {
                        workCompleted(arg.Result);
                    }
                }
                catch (Exception ex)
                {
                    cmd.HideOpaqueLayer();
                    LogClass.WriteLog(ex.Message);
                    MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            };

            bgWorkder.DoWork += (s, arg) =>
            {
                bgWorkder.ReportProgress(1);
                var result = workFunc(arg.Argument);
                arg.Result = result;
                bgWorkder.ReportProgress(100);
                Thread.Sleep(time);
            };

            bgWorkder.RunWorkerAsync(funcArg);
        }
        
    }
}
