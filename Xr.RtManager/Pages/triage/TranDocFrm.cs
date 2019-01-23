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
    public partial class TranDocFrm : Form
    {
        public TranDocFrm()
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
