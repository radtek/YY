using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xr.RtManager.Pages.cms
{
    public partial class HolidaySettingForm : UserControl
    {
        public HolidaySettingForm()
        {
            InitializeComponent();
            Data = new List<Holiday>
            {
               new Holiday {code="2019", name="春节",StardTime="2019-02-03",EndTime="2019-02-10"},
                 new Holiday {code="2019", name="元宵节",StardTime="2019-02-18",EndTime="2019-02-19"},
            };
            this.gc_Holiday.DataSource = Data;
        }
        public List<Holiday> Data = new List<Holiday>();
        public class Holiday
        {
            public string code { get; set; }
            public string name { get; set; }
            public string StardTime { get; set; }
            public string EndTime { get; set; }
        }
        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = this.gv_Holiday.GetRowCellValue(Data.Count - 1, "name").ToString();
                gv_Holiday.OptionsBehavior.ReadOnly = false;
                if (name != "")
                {
                    Data.Add(new Holiday { code = "", name = "", StardTime = "", EndTime = "" });
                }
                this.gc_Holiday.DataSource = Data;
                gc_Holiday.RefreshDataSource();
            }
            catch (Exception)
            {

            }
        }
        #endregion 
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }
        #endregion 
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRow = gv_Holiday.GetSelectedRows()[0];
                string name = this.gv_Holiday.GetRowCellValue(selectRow, "name").ToString();
                if (DialogResult.Yes == Xr.Common.MessageBoxUtils.Show("是否删除" + name + "节假日",MessageBoxButtons.YesNo,MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
                {
                    Xr.Common.MessageBoxUtils.Hint("删除成功");
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion 
        #region 控制编辑
        /// <summary>
        /// 控制编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Holiday_ShowingEditor(object sender, CancelEventArgs e)
        {
            int selectRow = gv_Holiday.GetSelectedRows()[0];
            string name = this.gv_Holiday.GetRowCellValue(selectRow, "name").ToString();
            if (name != "")
            {
                //if (obj["code"].ToString() != "")
                //{
                e.Cancel = true;
                //  }
            }
        }
        #endregion 
    }
}
