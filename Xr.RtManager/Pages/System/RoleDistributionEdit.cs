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
    public partial class RoleDistributionEdit : Form
    {
        public RoleDistributionEdit()
        {
            InitializeComponent();
        }

        public String roleId;
        public String msg;
        private List<OfficeEntity> officeList = null;
        private List<UserEntity> userList = new List<UserEntity>();

        private void RoleDistributionEdit_Load(object sender, EventArgs e)
        {
            treeList1.OptionsBehavior.Editable = false;   //treelist不可编辑
            treeList2.OptionsBehavior.Editable = false;   //treelist不可编辑
            treeList3.OptionsBehavior.Editable = false;   //treelist不可编辑

            treeList2.KeyFieldName = "id";//设置ID  
            treeList3.KeyFieldName = "id";//设置ID 

            String url = AppContext.AppConfig.serverUrl + "sys/sysOffice/findAllJson";
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                officeList = objT["result"].ToObject<List<OfficeEntity>>();
                treeList1.DataSource = officeList;
                treeList1.KeyFieldName = "id";//设置ID  
                treeList1.ParentFieldName = "parentId";//设置PreID   

                url = AppContext.AppConfig.serverUrl + "sys/sysRole/assign?roleId=" + roleId;
                data = HttpClass.httpPost(url);
                objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    userList = objT["result"].ToObject<List<UserEntity>>();
                    treeList3.DataSource = userList;
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

        private void treeList1_Click(object sender, EventArgs e)
        {
            if (treeList1.Nodes.Count != 0)
            {
                var str = treeList1.FocusedNode.GetValue("id");
                if (str == null) return;
                foreach (OfficeEntity office in officeList)
                {
                    if (office.id.Equals(str))
                        treeList2.DataSource = office.userList;
                }
            }
        }

        private void treeList2_Click(object sender, EventArgs e)
        {
            UserEntity user = treeList2.GetDataRecordByNode(treeList2.FocusedNode) as UserEntity;
            bool flag = true;
            foreach (UserEntity entity in userList)
            {
                if (entity.id.Equals(user.id))
                {
                    flag = false;
                    return;
                }
            }
            if (flag)
            {
                userList.Add(user);
                treeList3.DataSource = userList;
                treeList3.RefreshDataSource();
            }
        }

        private void treeList3_Click(object sender, EventArgs e)
        {
            UserEntity user = treeList3.GetDataRecordByNode(treeList3.FocusedNode) as UserEntity;
            if (user == null) return;
            userList.Remove(user);
            treeList3.DataSource = userList;
            treeList3.RefreshDataSource();
            //treeList3.DeleteNode(treeList3.FocusedNode);
        }

        private void btnScavenging_Click(object sender, EventArgs e)
        {
            userList.Clear();
            treeList3.DataSource = userList;
            treeList3.RefreshDataSource();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String ids = "";
            foreach(UserEntity user in userList){
                ids = ids + user.id + ",";
            }
            ids.Substring(0, ids.Length-1);
            String url = AppContext.AppConfig.serverUrl + "sys/sysRole/assignrole?roleId=" + roleId + "&&ids=" + ids;
            String data = HttpClass.httpPost(url);
            JObject objT = JObject.Parse(data);
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                msg = objT["message"].ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(objT["message"].ToString());
            }
        }
    }
}
