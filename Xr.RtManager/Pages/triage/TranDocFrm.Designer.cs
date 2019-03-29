namespace Xr.RtManager.Pages.triage
{
    partial class TranDocFrm
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.select = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lueIntoDoctor = new DevExpress.XtraEditors.LookUpEdit();
            this.lueStopDoctor = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSave = new Xr.Common.Controls.ButtonControl();
            this.Gc_patients = new DevExpress.XtraGrid.GridControl();
            this.gv_patients = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.treeDeptId = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueIntoDoctor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueStopDoctor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_patients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_patients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDeptId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.SuspendLayout();
            // 
            // select
            // 
            this.select.AppearanceCell.Options.UseTextOptions = true;
            this.select.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.select.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.select.AppearanceHeader.Options.UseFont = true;
            this.select.AppearanceHeader.Options.UseTextOptions = true;
            this.select.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.select.Caption = "☑";
            this.select.ColumnEdit = this.repositoryItemCheckEdit1;
            this.select.FieldName = "check";
            this.select.Name = "select";
            this.select.Visible = true;
            this.select.VisibleIndex = 0;
            this.select.Width = 20;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(197, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 30);
            this.label5.TabIndex = 40;
            this.label5.Text = "停诊医生：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(438, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 30);
            this.label7.TabIndex = 42;
            this.label7.Text = "转入医生：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lueIntoDoctor
            // 
            this.lueIntoDoctor.Location = new System.Drawing.Point(523, 9);
            this.lueIntoDoctor.Name = "lueIntoDoctor";
            this.lueIntoDoctor.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueIntoDoctor.Properties.Appearance.Options.UseFont = true;
            this.lueIntoDoctor.Properties.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueIntoDoctor.Properties.AppearanceDisabled.Options.UseFont = true;
            this.lueIntoDoctor.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueIntoDoctor.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueIntoDoctor.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueIntoDoctor.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueIntoDoctor.Properties.AppearanceFocused.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueIntoDoctor.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueIntoDoctor.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueIntoDoctor.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.lueIntoDoctor.Properties.AutoHeight = false;
            this.lueIntoDoctor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueIntoDoctor.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("doctorId", "键值", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("doctorName", "级别")});
            this.lueIntoDoctor.Properties.NullText = "";
            this.lueIntoDoctor.Size = new System.Drawing.Size(150, 30);
            this.lueIntoDoctor.TabIndex = 6;
            this.lueIntoDoctor.EditValueChanged += new System.EventHandler(this.lueIntoDoctor_EditValueChanged);
            // 
            // lueStopDoctor
            // 
            this.lueStopDoctor.Location = new System.Drawing.Point(282, 9);
            this.lueStopDoctor.Name = "lueStopDoctor";
            this.lueStopDoctor.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueStopDoctor.Properties.Appearance.Options.UseFont = true;
            this.lueStopDoctor.Properties.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueStopDoctor.Properties.AppearanceDisabled.Options.UseFont = true;
            this.lueStopDoctor.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueStopDoctor.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueStopDoctor.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueStopDoctor.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueStopDoctor.Properties.AppearanceFocused.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueStopDoctor.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueStopDoctor.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueStopDoctor.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.lueStopDoctor.Properties.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            serializableAppearanceObject1.Options.UseFont = true;
            this.lueStopDoctor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.lueStopDoctor.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("doctorId", "键值", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("doctorName", "类型")});
            this.lueStopDoctor.Properties.NullText = "";
            this.lueStopDoctor.Size = new System.Drawing.Size(150, 30);
            this.lueStopDoctor.TabIndex = 5;
            this.lueStopDoctor.EditValueChanged += new System.EventHandler(this.lueStopDoctor_EditValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnSave.HoverBackColor = System.Drawing.Color.Empty;
            this.btnSave.Location = new System.Drawing.Point(685, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "确认转入";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Gc_patients
            // 
            this.Gc_patients.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Gc_patients.Location = new System.Drawing.Point(12, 45);
            this.Gc_patients.MainView = this.gv_patients;
            this.Gc_patients.Name = "Gc_patients";
            this.Gc_patients.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemPopupContainerEdit1});
            this.Gc_patients.Size = new System.Drawing.Size(745, 410);
            this.Gc_patients.TabIndex = 43;
            this.Gc_patients.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_patients});
            // 
            // gv_patients
            // 
            this.gv_patients.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.gv_patients.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_patients.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_patients.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_patients.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gv_patients.Appearance.OddRow.Options.UseBackColor = true;
            this.gv_patients.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.gv_patients.Appearance.Row.Options.UseFont = true;
            this.gv_patients.Appearance.Row.Options.UseTextOptions = true;
            this.gv_patients.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_patients.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.select,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn7,
            this.gridColumn4,
            this.gridColumn5});
            this.gv_patients.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gv_patients.GridControl = this.Gc_patients;
            this.gv_patients.Name = "gv_patients";
            this.gv_patients.OptionsCustomization.AllowColumnMoving = false;
            this.gv_patients.OptionsCustomization.AllowFilter = false;
            this.gv_patients.OptionsCustomization.AllowGroup = false;
            this.gv_patients.OptionsCustomization.AllowQuickHideColumns = false;
            this.gv_patients.OptionsCustomization.AllowSort = false;
            this.gv_patients.OptionsFilter.AllowFilterEditor = false;
            this.gv_patients.OptionsMenu.EnableColumnMenu = false;
            this.gv_patients.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gv_patients.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gv_patients.OptionsSelection.InvertSelection = true;
            this.gv_patients.OptionsSelection.MultiSelect = true;
            this.gv_patients.OptionsView.EnableAppearanceOddRow = true;
            this.gv_patients.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gv_patients.OptionsView.ShowGroupPanel = false;
            this.gv_patients.OptionsView.ShowIndicator = false;
            this.gv_patients.RowHeight = 30;
            this.gv_patients.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gv_patients_MouseDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "预约ID";
            this.gridColumn1.FieldName = "triageId";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Width = 165;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "医生姓名";
            this.gridColumn2.FieldName = "doctorName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "候诊号";
            this.gridColumn3.FieldName = "queueNum";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "患者姓名";
            this.gridColumn7.FieldName = "patientName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 200;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "就诊日期";
            this.gridColumn4.FieldName = "workDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "就诊时段";
            this.gridColumn5.FieldName = "workTime";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            // 
            // treeDeptId
            // 
            this.treeDeptId.EditValue = "";
            this.treeDeptId.Location = new System.Drawing.Point(66, 9);
            this.treeDeptId.Name = "treeDeptId";
            this.treeDeptId.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeDeptId.Properties.Appearance.Options.UseFont = true;
            this.treeDeptId.Properties.AutoHeight = false;
            this.treeDeptId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeDeptId.Properties.NullText = "";
            this.treeDeptId.Properties.PopupFormSize = new System.Drawing.Size(232, 0);
            this.treeDeptId.Properties.TreeList = this.treeList2;
            this.treeDeptId.Size = new System.Drawing.Size(125, 30);
            this.treeDeptId.TabIndex = 190;
            this.treeDeptId.EditValueChanged += new System.EventHandler(this.treeDeptId_EditValueChanged);
            // 
            // treeList2
            // 
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn9,
            this.treeListColumn10});
            this.treeList2.Location = new System.Drawing.Point(345, -37);
            this.treeList2.Name = "treeList2";
            this.treeList2.OptionsBehavior.EnableFiltering = true;
            this.treeList2.OptionsView.AllowHtmlDrawHeaders = true;
            this.treeList2.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList2.RowHeight = 30;
            this.treeList2.Size = new System.Drawing.Size(400, 150);
            this.treeList2.TabIndex = 0;
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.treeListColumn9.AppearanceCell.Options.UseFont = true;
            this.treeListColumn9.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.treeListColumn9.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn9.Caption = "科室";
            this.treeListColumn9.FieldName = "name";
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.Visible = true;
            this.treeListColumn9.VisibleIndex = 0;
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.Caption = "id";
            this.treeListColumn10.FieldName = "id";
            this.treeListColumn10.Name = "treeListColumn10";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(-14, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 30);
            this.label3.TabIndex = 189;
            this.label3.Text = "科室：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TranDocFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(768, 465);
            this.Controls.Add(this.treeDeptId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Gc_patients);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lueStopDoctor);
            this.Controls.Add(this.lueIntoDoctor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TranDocFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "患者转诊";
            this.Load += new System.EventHandler(this.OfficeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueIntoDoctor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueStopDoctor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_patients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_patients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDeptId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.LookUpEdit lueIntoDoctor;
        private DevExpress.XtraEditors.LookUpEdit lueStopDoctor;
        private Xr.Common.Controls.ButtonControl btnSave;
        private DevExpress.XtraGrid.GridControl Gc_patients;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_patients;
        private DevExpress.XtraGrid.Columns.GridColumn select;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeDeptId;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}
