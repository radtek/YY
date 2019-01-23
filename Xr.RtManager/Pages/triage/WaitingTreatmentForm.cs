﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Xr.Common;
using Xr.Http;
using Xr.Common.Controls;

namespace Xr.RtManager
{
    public partial class WaitingTreatmentForm : UserControl
    {
        Xr.Common.Controls.OpaqueCommand cmd;
        public WaitingTreatmentForm()
        {
            InitializeComponent();
            //cmd = new Xr.Common.Controls.OpaqueCommand(this);
            //cmd.ShowOpaqueLayer(225, true);
        }

        private JObject obj { get; set; }

        private void UserForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 243, 243);
            SearchData(true, 1);

        }

        public void SearchData(bool flag, int pageNo)
        {
          
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            SearchData(false, 1);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            var edit = new ClientVersionEdit();
            if (edit.ShowDialog() == DialogResult.OK)
            {
                MessageBoxUtils.Hint("保存成功!");
                SearchData(true, 1);
            }
        }
        private void gv_patients_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标右键点击
            System.Threading.Thread.Sleep(10);
            if (e.Button == MouseButtons.Right)
            {

                //GridHitInfo gridHitInfo = gridView.CalcHitInfo(e.X, e.Y);
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo gridHitInfo = gv_Patients.CalcHitInfo(e.X, e.Y);

                //在列标题栏内且列标题name是"colName"
                if (gridHitInfo.Column != null)
                {
                    if (!gridHitInfo.InColumnPanel)//判断是否在单元格内
                    {
                        contextMenuStrip1.Show(gc_Patients, e.Location);
                        int i = gridHitInfo.RowHandle;
                    }
                }
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {


            //BorderPanel m_OpaqueLayer = new BorderPanel();
            //m_OpaqueLayer.Size = this.Size;
            //m_OpaqueLayer.BringToFront();
            //this.Controls.Add(m_OpaqueLayer);

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
           
        }

    }
}
