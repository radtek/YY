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
using Xr.Common;

namespace Xr.RtManager.Pages.triage
{
    public partial class TranDocFrm : Form
    {
        String Dept = String.Empty;
        public TranDocFrm(String deptID)
        {
            InitializeComponent();
            Dept = deptID;
        }

        private void OfficeEdit_Load(object sender, EventArgs e)
        {
            //获取获取科室可挂号医生信息
            Dictionary<string, string> prament = new Dictionary<string, string>();
            String param = "";
            prament.Add("hospital.code", AppContext.Session.hospitalId);
            prament.Add("dept.id", Dept);

            String url = String.Empty;
            if (prament.Count != 0)
            {
                param = string.Join("&", prament.Select(x => x.Key + "=" + x.Value).ToArray());
            }
            url = AppContext.AppConfig.serverUrl + "cms/doctor/list?" + param;
            JObject objT = new JObject();
            objT = JObject.Parse(HttpClass.httpPost(url));
            if (string.Compare(objT["state"].ToString(), "true", true) == 0)
            {
                //List<Dic> list = objT["result"].ToObject<List<Dic>>();
                List<DoctorInfoEntity> list = objT["result"]["list"].ToObject<List<DoctorInfoEntity>>();
                lueStopDoctor.Properties.DataSource = list;
                lueStopDoctor.Properties.DisplayMember = "name";
                lueStopDoctor.Properties.ValueMember = "code";
                lueStopDoctor.ItemIndex = 0;
            }
            else
            {
                MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this);
                return;
            } 
    
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        private void gv_patients_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标左键点击
            System.Threading.Thread.Sleep(10);
            if (e.Button == MouseButtons.Left)
            {

                //GridHitInfo gridHitInfo = gridView.CalcHitInfo(e.X, e.Y);
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo gridHitInfo = gv_patients.CalcHitInfo(e.X, e.Y);

                //在列标题栏内且列标题name是"colName"
                if (gridHitInfo.Column != null)
                {
                    if (gridHitInfo.InColumnPanel && gridHitInfo.Column.Name == "select")
                    {

                        //获取该列右边线的x坐标

                        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo gridViewInfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)this.gv_patients.GetViewInfo();

                        int x = gridViewInfo.GetColumnLeftCoord(gridHitInfo.Column) + gridHitInfo.Column.Width;

                        //右边线向左移动3个像素位置不弹出对话框（实验证明3个像素是正好的）

                        if (e.X < x - 3)
                        {

                            //XtraMessageBox.Show("点击select列标题！");

                            for (int i = 0; i < gv_patients.RowCount; i++)
                            {
                                //鼠标的那个按钮按下 
                                string dr = gv_patients.GetRowCellValue(i, "check").ToString();
                                if (dr == "1")
                                    gv_patients.SetRowCellValue(i, gv_patients.Columns["check"], "0");
                                else//(dr == "0")
                                    gv_patients.SetRowCellValue(i, gv_patients.Columns["check"], "1");
                            }
                        }

                    }
                    else if (!gridHitInfo.InColumnPanel)
                    {

                        int i = gridHitInfo.RowHandle;
                        string dr = gv_patients.GetRowCellValue(i, "check").ToString();
                        if (dr == "1")
                            gv_patients.SetRowCellValue(i, gv_patients.Columns["check"], "0");
                        else//(dr == "0")
                            gv_patients.SetRowCellValue(i, gv_patients.Columns["check"], "1");

                    }
                }

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            /*string value = "";
            string strSelected = "";
            List<ReceiveMedicine> itemList = new List<ReceiveMedicine>();
            for (int i = 0; i < gv_patients.RowCount; i++)
            {   //   获取选中行的check的值   
                string dr = gv_patients.GetRowCellValue(i, "check").ToString();
                if (dr != String.Empty)
                {
                    if (dr == "1")
                    {
                        ReceiveMedicine rowItem = gv_patients.GetRow(i) as ReceiveMedicine;
                        itemList.Add(rowItem);
                        //bandedGvList.Columns["euDrugtype"].FilterInfo = new ColumnFilterInfo("[euDrugtype] LIKE '0'");

                    }
                }
            }
            if (itemList.Count > 0)
            {
            }
            else
            {
            }
             */
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
