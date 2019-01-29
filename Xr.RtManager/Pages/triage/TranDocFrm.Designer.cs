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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dcOffice = new Xr.Common.Controls.DataController(this.components);
            this.lueGrade = new DevExpress.XtraEditors.LookUpEdit();
            this.lueType = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSave = new Xr.Common.Controls.ButtonControl();
            this.Gc_patients = new DevExpress.XtraGrid.GridControl();
            this.gv_patients = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.select = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGrade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_patients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_patients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 40;
            this.label5.Text = "停诊医生：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(253, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "转入医生：";
            // 
            // lueGrade
            // 
            this.dcOffice.SetDataMember(this.lueGrade, "grade");
            this.lueGrade.Location = new System.Drawing.Point(338, 9);
            this.lueGrade.Name = "lueGrade";
            this.lueGrade.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueGrade.Properties.Appearance.Options.UseFont = true;
            this.lueGrade.Properties.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueGrade.Properties.AppearanceDisabled.Options.UseFont = true;
            this.lueGrade.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueGrade.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueGrade.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueGrade.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueGrade.Properties.AppearanceFocused.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lueGrade.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueGrade.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueGrade.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.lueGrade.Properties.AutoHeight = false;
            this.lueGrade.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGrade.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "键值", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "级别")});
            this.lueGrade.Properties.NullText = "";
            this.lueGrade.Size = new System.Drawing.Size(150, 20);
            this.lueGrade.TabIndex = 6;
            // 
            // lueType
            // 
            this.dcOffice.SetDataMember(this.lueType, "type");
            this.lueType.Location = new System.Drawing.Point(97, 9);
            this.lueType.Name = "lueType";
            this.lueType.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.Appearance.Options.UseFont = true;
            this.lueType.Properties.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.AppearanceDisabled.Options.UseFont = true;
            this.lueType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueType.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueType.Properties.AppearanceFocused.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueType.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.lueType.Properties.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            serializableAppearanceObject1.Options.UseFont = true;
            this.lueType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.lueType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "键值", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "类型")});
            this.lueType.Properties.NullText = "";
            this.lueType.Size = new System.Drawing.Size(150, 20);
            this.lueType.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnSave.HoverBackColor = System.Drawing.Color.Empty;
            this.btnSave.Location = new System.Drawing.Point(502, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 20);
            this.btnSave.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "确认转入";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Gc_patients
            // 
            this.Gc_patients.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Gc_patients.Location = new System.Drawing.Point(12, 32);
            this.Gc_patients.MainView = this.gv_patients;
            this.Gc_patients.Name = "Gc_patients";
            this.Gc_patients.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemPopupContainerEdit1});
            this.Gc_patients.Size = new System.Drawing.Size(560, 417);
            this.Gc_patients.TabIndex = 43;
            this.Gc_patients.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_patients});
            // 
            // gv_patients
            // 
            this.gv_patients.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_patients.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_patients.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gv_patients.Appearance.OddRow.Options.UseBackColor = true;
            this.gv_patients.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.select,
            this.gridColumn1,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn11});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.select;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "1";
            this.gv_patients.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
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
            this.select.Width = 44;
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
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "患者ID";
            this.gridColumn1.FieldName = "name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 165;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "患者姓名";
            this.gridColumn7.FieldName = "spe";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 200;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "年龄";
            this.gridColumn8.FieldName = "num";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 78;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "性别";
            this.gridColumn11.FieldName = "patientName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 71;
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            // 
            // TranDocFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.Gc_patients);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lueType);
            this.Controls.Add(this.lueGrade);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Name = "TranDocFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机构添加";
            this.Load += new System.EventHandler(this.OfficeEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueGrade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_patients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_patients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private Xr.Common.Controls.DataController dcOffice;
        private DevExpress.XtraEditors.LookUpEdit lueGrade;
        private DevExpress.XtraEditors.LookUpEdit lueType;
        private Xr.Common.Controls.ButtonControl btnSave;
        private DevExpress.XtraGrid.GridControl Gc_patients;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_patients;
        private DevExpress.XtraGrid.Columns.GridColumn select;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
    }
}
