﻿namespace Xr.RtCall
{
    partial class SettingFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonControl1 = new Xr.Common.Controls.ButtonControl();
            this.buttonControl2 = new Xr.Common.Controls.ButtonControl();
            this.treeKeshi = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColunm1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.treeHostile = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeClinc = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeKeshi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeHostile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeClinc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(40, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "科室名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(40, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "诊室名称：";
            // 
            // buttonControl1
            // 
            this.buttonControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.buttonControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.buttonControl1.HoverBackColor = System.Drawing.Color.Empty;
            this.buttonControl1.Location = new System.Drawing.Point(119, 178);
            this.buttonControl1.Name = "buttonControl1";
            this.buttonControl1.Size = new System.Drawing.Size(75, 30);
            this.buttonControl1.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.buttonControl1.TabIndex = 2;
            this.buttonControl1.Text = "确定";
            this.buttonControl1.Click += new System.EventHandler(this.buttonControl1_Click);
            // 
            // buttonControl2
            // 
            this.buttonControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.buttonControl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.buttonControl2.HoverBackColor = System.Drawing.Color.Empty;
            this.buttonControl2.Location = new System.Drawing.Point(213, 178);
            this.buttonControl2.Name = "buttonControl2";
            this.buttonControl2.Size = new System.Drawing.Size(75, 30);
            this.buttonControl2.Style = Xr.Common.Controls.ButtonStyle.Return;
            this.buttonControl2.TabIndex = 3;
            this.buttonControl2.Text = "取消";
            this.buttonControl2.Click += new System.EventHandler(this.buttonControl2_Click);
            // 
            // treeKeshi
            // 
            this.treeKeshi.EditValue = "";
            this.treeKeshi.Location = new System.Drawing.Point(131, 75);
            this.treeKeshi.Name = "treeKeshi";
            this.treeKeshi.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeKeshi.Properties.Appearance.Options.UseFont = true;
            this.treeKeshi.Properties.AutoHeight = false;
            this.treeKeshi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeKeshi.Properties.NullText = "";
            this.treeKeshi.Properties.PopupFormMinSize = new System.Drawing.Size(30, 0);
            this.treeKeshi.Properties.PopupFormSize = new System.Drawing.Size(160, 0);
            this.treeKeshi.Properties.TreeList = this.treeListLookUpEdit1TreeList;
            this.treeKeshi.Size = new System.Drawing.Size(157, 27);
            this.treeKeshi.TabIndex = 125;
            this.treeKeshi.EditValueChanged += new System.EventHandler(this.treeKeshi_EditValueChanged);
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColunm1});
            this.treeListLookUpEdit1TreeList.Location = new System.Drawing.Point(-28, 31);
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = true;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeListLookUpEdit1TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEdit1TreeList.TabIndex = 0;
            // 
            // treeListColunm1
            // 
            this.treeListColunm1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColunm1.AppearanceCell.Options.UseFont = true;
            this.treeListColunm1.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColunm1.AppearanceHeader.Options.UseFont = true;
            this.treeListColunm1.Caption = "科室列表";
            this.treeListColunm1.FieldName = "name";
            this.treeListColunm1.MinWidth = 30;
            this.treeListColunm1.Name = "treeListColunm1";
            this.treeListColunm1.Visible = true;
            this.treeListColunm1.VisibleIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 246);
            this.panel1.TabIndex = 126;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.treeHostile);
            this.panel2.Controls.Add(this.buttonControl1);
            this.panel2.Controls.Add(this.buttonControl2);
            this.panel2.Controls.Add(this.treeClinc);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.treeKeshi);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 246);
            this.panel2.TabIndex = 129;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(40, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "医院名称：";
            // 
            // treeHostile
            // 
            this.treeHostile.EditValue = "";
            this.treeHostile.Location = new System.Drawing.Point(131, 25);
            this.treeHostile.Name = "treeHostile";
            this.treeHostile.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeHostile.Properties.Appearance.Options.UseFont = true;
            this.treeHostile.Properties.AutoHeight = false;
            this.treeHostile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeHostile.Properties.NullText = "";
            this.treeHostile.Properties.PopupFormMinSize = new System.Drawing.Size(30, 0);
            this.treeHostile.Properties.PopupFormSize = new System.Drawing.Size(160, 0);
            this.treeHostile.Properties.TreeList = this.treeList2;
            this.treeHostile.Size = new System.Drawing.Size(157, 27);
            this.treeHostile.TabIndex = 127;
            this.treeHostile.EditValueChanged += new System.EventHandler(this.treeHostile_EditValueChanged);
            // 
            // treeList2
            // 
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeList2.Location = new System.Drawing.Point(-43, 25);
            this.treeList2.Name = "treeList2";
            this.treeList2.OptionsBehavior.EnableFiltering = true;
            this.treeList2.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList2.Size = new System.Drawing.Size(410, 200);
            this.treeList2.TabIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceCell.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.Caption = "医院列表";
            this.treeListColumn2.FieldName = "name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 20;
            // 
            // treeClinc
            // 
            this.treeClinc.EditValue = "";
            this.treeClinc.Location = new System.Drawing.Point(131, 124);
            this.treeClinc.Name = "treeClinc";
            this.treeClinc.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeClinc.Properties.Appearance.Options.UseFont = true;
            this.treeClinc.Properties.AutoHeight = false;
            this.treeClinc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeClinc.Properties.NullText = "";
            this.treeClinc.Properties.PopupFormMinSize = new System.Drawing.Size(30, 0);
            this.treeClinc.Properties.PopupFormSize = new System.Drawing.Size(160, 0);
            this.treeClinc.Properties.TreeList = this.treeList1;
            this.treeClinc.Size = new System.Drawing.Size(157, 27);
            this.treeClinc.TabIndex = 126;
            this.treeClinc.EditValueChanged += new System.EventHandler(this.treeClinc_EditValueChanged);
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.Location = new System.Drawing.Point(-28, 31);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.EnableFiltering = true;
            this.treeList1.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList1.Size = new System.Drawing.Size(410, 200);
            this.treeList1.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceCell.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.Caption = "诊室列表";
            this.treeListColumn1.FieldName = "name";
            this.treeListColumn1.MinWidth = 30;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // SettingFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(340, 246);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "初始设置";
            ((System.ComponentModel.ISupportInitialize)(this.treeKeshi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeHostile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeClinc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Common.Controls.ButtonControl buttonControl1;
        private Common.Controls.ButtonControl buttonControl2;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeKeshi;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColunm1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeClinc;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeHostile;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
    }
}