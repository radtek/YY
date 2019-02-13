namespace Xr.RtManager.Pages.booking
{
    partial class BlackListAdminForm
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.select = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.gc_BlackList = new DevExpress.XtraGrid.GridControl();
            this.gv_BlackList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBorderPanel6 = new Xr.Common.Controls.GroupBorderPanel();
            this.gc_PatientList = new DevExpress.XtraGrid.GridControl();
            this.gv_PatientList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemPopupContainerEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.borderPanel1 = new Xr.Common.Controls.BorderPanel();
            this.cb_AutoRefresh = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonControl2 = new Xr.Common.Controls.ButtonControl();
            this.buttonControl3 = new Xr.Common.Controls.ButtonControl();
            this.panelEx12 = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx15 = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx16 = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx17 = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx13 = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx14 = new Xr.Common.Controls.PanelEx(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_BlackList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BlackList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBorderPanel6)).BeginInit();
            this.groupBorderPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_PatientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PatientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.borderPanel1)).BeginInit();
            this.borderPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.select.Width = 57;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1049, 718);
            this.panel3.TabIndex = 62;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 60);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(1049, 658);
            this.panel4.TabIndex = 63;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.gc_BlackList);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(10, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1029, 375);
            this.panel5.TabIndex = 45;
            // 
            // gc_BlackList
            // 
            this.gc_BlackList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_BlackList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gc_BlackList.Location = new System.Drawing.Point(0, 0);
            this.gc_BlackList.MainView = this.gv_BlackList;
            this.gc_BlackList.Name = "gc_BlackList";
            this.gc_BlackList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemPopupContainerEdit1});
            this.gc_BlackList.Size = new System.Drawing.Size(1029, 375);
            this.gc_BlackList.TabIndex = 44;
            this.gc_BlackList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_BlackList});
            // 
            // gv_BlackList
            // 
            this.gv_BlackList.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_BlackList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_BlackList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_BlackList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_BlackList.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gv_BlackList.Appearance.OddRow.Options.UseBackColor = true;
            this.gv_BlackList.Appearance.Row.Options.UseTextOptions = true;
            this.gv_BlackList.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_BlackList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.select,
            this.gridColumn9,
            this.gridColumn1,
            this.gridColumn7,
            this.gridColumn11,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn12});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.select;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "1";
            this.gv_BlackList.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gv_BlackList.GridControl = this.gc_BlackList;
            this.gv_BlackList.Name = "gv_BlackList";
            this.gv_BlackList.OptionsCustomization.AllowColumnMoving = false;
            this.gv_BlackList.OptionsCustomization.AllowFilter = false;
            this.gv_BlackList.OptionsCustomization.AllowGroup = false;
            this.gv_BlackList.OptionsCustomization.AllowQuickHideColumns = false;
            this.gv_BlackList.OptionsCustomization.AllowSort = false;
            this.gv_BlackList.OptionsFilter.AllowFilterEditor = false;
            this.gv_BlackList.OptionsMenu.EnableColumnMenu = false;
            this.gv_BlackList.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gv_BlackList.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gv_BlackList.OptionsSelection.InvertSelection = true;
            this.gv_BlackList.OptionsSelection.MultiSelect = true;
            this.gv_BlackList.OptionsView.EnableAppearanceOddRow = true;
            this.gv_BlackList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gv_BlackList.OptionsView.ShowGroupPanel = false;
            this.gv_BlackList.OptionsView.ShowIndicator = false;
            this.gv_BlackList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gv_deptInfo_RowCellClick);
            this.gv_BlackList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gv_BlackList_MouseDown);
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "序号";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 50;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "患者ID";
            this.gridColumn1.FieldName = "patientId";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "姓名";
            this.gridColumn7.FieldName = "patientName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 200;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "性别";
            this.gridColumn11.FieldName = "sex";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 60;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "电话";
            this.gridColumn8.FieldName = "phone";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 129;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "爽约次数";
            this.gridColumn10.FieldName = "breakTimes";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 149;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "最后爽约时间";
            this.gridColumn12.FieldName = "updateDate";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 200;
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBorderPanel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 385);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel2.Size = new System.Drawing.Size(1029, 263);
            this.panel2.TabIndex = 0;
            // 
            // groupBorderPanel6
            // 
            this.groupBorderPanel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.groupBorderPanel6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupBorderPanel6.Controls.Add(this.gc_PatientList);
            this.groupBorderPanel6.CornerRadius.All = 5;
            this.groupBorderPanel6.CornerRadius.BottomLeft = 5;
            this.groupBorderPanel6.CornerRadius.BottomRight = 5;
            this.groupBorderPanel6.CornerRadius.TopLeft = 5;
            this.groupBorderPanel6.CornerRadius.TopRight = 5;
            this.groupBorderPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBorderPanel6.FillColor1 = System.Drawing.Color.Transparent;
            this.groupBorderPanel6.FillColor2 = System.Drawing.Color.Transparent;
            this.groupBorderPanel6.GroupText = "历次爽约明细";
            this.groupBorderPanel6.GroupTextFont = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.groupBorderPanel6.Location = new System.Drawing.Point(0, 10);
            this.groupBorderPanel6.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.groupBorderPanel6.Name = "groupBorderPanel6";
            this.groupBorderPanel6.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.groupBorderPanel6.Size = new System.Drawing.Size(1029, 253);
            this.groupBorderPanel6.TabIndex = 186;
            // 
            // gc_PatientList
            // 
            this.gc_PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_PatientList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gc_PatientList.Location = new System.Drawing.Point(10, 20);
            this.gc_PatientList.MainView = this.gv_PatientList;
            this.gc_PatientList.Name = "gc_PatientList";
            this.gc_PatientList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2,
            this.repositoryItemPopupContainerEdit2});
            this.gc_PatientList.Size = new System.Drawing.Size(1009, 223);
            this.gc_PatientList.TabIndex = 45;
            this.gc_PatientList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_PatientList});
            // 
            // gv_PatientList
            // 
            this.gv_PatientList.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_PatientList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_PatientList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_PatientList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_PatientList.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gv_PatientList.Appearance.OddRow.Options.UseBackColor = true;
            this.gv_PatientList.Appearance.Row.Options.UseTextOptions = true;
            this.gv_PatientList.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_PatientList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Value1 = "1";
            this.gv_PatientList.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition2});
            this.gv_PatientList.GridControl = this.gc_PatientList;
            this.gv_PatientList.Name = "gv_PatientList";
            this.gv_PatientList.OptionsCustomization.AllowColumnMoving = false;
            this.gv_PatientList.OptionsCustomization.AllowFilter = false;
            this.gv_PatientList.OptionsCustomization.AllowGroup = false;
            this.gv_PatientList.OptionsCustomization.AllowQuickHideColumns = false;
            this.gv_PatientList.OptionsCustomization.AllowSort = false;
            this.gv_PatientList.OptionsFilter.AllowFilterEditor = false;
            this.gv_PatientList.OptionsMenu.EnableColumnMenu = false;
            this.gv_PatientList.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gv_PatientList.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gv_PatientList.OptionsSelection.InvertSelection = true;
            this.gv_PatientList.OptionsSelection.MultiSelect = true;
            this.gv_PatientList.OptionsView.EnableAppearanceOddRow = true;
            this.gv_PatientList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gv_PatientList.OptionsView.ShowGroupPanel = false;
            this.gv_PatientList.OptionsView.ShowIndicator = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "序号";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 50;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "患者ID";
            this.gridColumn4.FieldName = "patientId";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 201;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "患者姓名";
            this.gridColumn5.FieldName = "patientName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 201;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "预约科室";
            this.gridColumn6.FieldName = "deptName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 114;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "预约医生";
            this.gridColumn13.FieldName = "doctorName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 150;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "预约就诊时间";
            this.gridColumn14.FieldName = "regVisitTime";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 4;
            this.gridColumn14.Width = 145;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "记录爽约时间";
            this.gridColumn15.FieldName = "createDate";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 136;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Caption = "Check";
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit2.ValueChecked = "1";
            this.repositoryItemCheckEdit2.ValueUnchecked = "0";
            // 
            // repositoryItemPopupContainerEdit2
            // 
            this.repositoryItemPopupContainerEdit2.AutoHeight = false;
            this.repositoryItemPopupContainerEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit2.Name = "repositoryItemPopupContainerEdit2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.borderPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.panel1.Size = new System.Drawing.Size(1049, 60);
            this.panel1.TabIndex = 8;
            // 
            // borderPanel1
            // 
            this.borderPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.borderPanel1.Appearance.Options.UseBackColor = true;
            this.borderPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.borderPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.borderPanel1.Controls.Add(this.cb_AutoRefresh);
            this.borderPanel1.Controls.Add(this.tableLayoutPanel2);
            this.borderPanel1.CornerRadius.All = 4;
            this.borderPanel1.CornerRadius.BottomLeft = 4;
            this.borderPanel1.CornerRadius.BottomRight = 4;
            this.borderPanel1.CornerRadius.TopLeft = 4;
            this.borderPanel1.CornerRadius.TopRight = 4;
            this.borderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel1.FillColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.borderPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.borderPanel1.Location = new System.Drawing.Point(10, 10);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.borderPanel1.Size = new System.Drawing.Size(1029, 50);
            this.borderPanel1.TabIndex = 0;
            // 
            // cb_AutoRefresh
            // 
            this.cb_AutoRefresh.AutoSize = true;
            this.cb_AutoRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.cb_AutoRefresh.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cb_AutoRefresh.Location = new System.Drawing.Point(925, 10);
            this.cb_AutoRefresh.Name = "cb_AutoRefresh";
            this.cb_AutoRefresh.Size = new System.Drawing.Size(84, 30);
            this.cb_AutoRefresh.TabIndex = 180;
            this.cb_AutoRefresh.Text = "自动刷新";
            this.cb_AutoRefresh.UseVisualStyleBackColor = true;
            this.cb_AutoRefresh.CheckedChanged += new System.EventHandler(this.cb_AutoRefresh_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel2.Controls.Add(this.buttonControl2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonControl3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 10);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(292, 30);
            this.tableLayoutPanel2.TabIndex = 175;
            // 
            // buttonControl2
            // 
            this.buttonControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.buttonControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonControl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.buttonControl2.HoverBackColor = System.Drawing.Color.Empty;
            this.buttonControl2.Location = new System.Drawing.Point(85, 0);
            this.buttonControl2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.buttonControl2.Name = "buttonControl2";
            this.buttonControl2.Size = new System.Drawing.Size(70, 30);
            this.buttonControl2.Style = Xr.Common.Controls.ButtonStyle.Del;
            this.buttonControl2.TabIndex = 91;
            this.buttonControl2.Text = "解冻";
            this.buttonControl2.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // buttonControl3
            // 
            this.buttonControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.buttonControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonControl3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.buttonControl3.HoverBackColor = System.Drawing.Color.Empty;
            this.buttonControl3.Location = new System.Drawing.Point(5, 0);
            this.buttonControl3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.buttonControl3.Name = "buttonControl3";
            this.buttonControl3.Size = new System.Drawing.Size(70, 30);
            this.buttonControl3.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.buttonControl3.TabIndex = 89;
            this.buttonControl3.Text = "新增";
            this.buttonControl3.Click += new System.EventHandler(this.buttonControl3_Click);
            // 
            // panelEx12
            // 
            this.panelEx12.BorderColor = System.Drawing.Color.Aqua;
            this.panelEx12.BorderSize = 1;
            this.panelEx12.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx12.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx12.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx12.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx12.Location = new System.Drawing.Point(0, 0);
            this.panelEx12.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx12.Name = "panelEx12";
            this.panelEx12.Padding = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.panelEx12.Size = new System.Drawing.Size(250, 150);
            this.panelEx12.TabIndex = 0;
            // 
            // panelEx15
            // 
            this.panelEx15.BorderColor = System.Drawing.Color.Aqua;
            this.panelEx15.BorderSize = 1;
            this.panelEx15.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx15.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx15.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx15.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx15.Location = new System.Drawing.Point(0, 0);
            this.panelEx15.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx15.Name = "panelEx15";
            this.panelEx15.Padding = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.panelEx15.Size = new System.Drawing.Size(250, 150);
            this.panelEx15.TabIndex = 0;
            // 
            // panelEx16
            // 
            this.panelEx16.BorderColor = System.Drawing.Color.Aqua;
            this.panelEx16.BorderSize = 1;
            this.panelEx16.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx16.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx16.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx16.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx16.Location = new System.Drawing.Point(0, 0);
            this.panelEx16.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx16.Name = "panelEx16";
            this.panelEx16.Padding = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.panelEx16.Size = new System.Drawing.Size(250, 150);
            this.panelEx16.TabIndex = 0;
            // 
            // panelEx17
            // 
            this.panelEx17.BorderColor = System.Drawing.Color.Aqua;
            this.panelEx17.BorderSize = 1;
            this.panelEx17.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx17.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx17.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx17.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx17.Location = new System.Drawing.Point(0, 0);
            this.panelEx17.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx17.Name = "panelEx17";
            this.panelEx17.Padding = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.panelEx17.Size = new System.Drawing.Size(250, 150);
            this.panelEx17.TabIndex = 0;
            // 
            // panelEx13
            // 
            this.panelEx13.BorderColor = System.Drawing.Color.Aqua;
            this.panelEx13.BorderSize = 1;
            this.panelEx13.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx13.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx13.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx13.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx13.Location = new System.Drawing.Point(0, 0);
            this.panelEx13.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx13.Name = "panelEx13";
            this.panelEx13.Padding = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.panelEx13.Size = new System.Drawing.Size(250, 150);
            this.panelEx13.TabIndex = 0;
            // 
            // panelEx14
            // 
            this.panelEx14.BorderColor = System.Drawing.Color.Aqua;
            this.panelEx14.BorderSize = 1;
            this.panelEx14.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx14.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx14.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx14.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx14.Location = new System.Drawing.Point(0, 0);
            this.panelEx14.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx14.Name = "panelEx14";
            this.panelEx14.Padding = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.panelEx14.Size = new System.Drawing.Size(250, 150);
            this.panelEx14.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BlackListAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel3);
            this.Name = "BlackListAdminForm";
            this.Size = new System.Drawing.Size(1049, 718);
            this.Load += new System.EventHandler(this.UserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_BlackList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BlackList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBorderPanel6)).EndInit();
            this.groupBorderPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_PatientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PatientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.borderPanel1)).EndInit();
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private Xr.Common.Controls.BorderPanel borderPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Xr.Common.Controls.PanelEx panelEx12;
        private Xr.Common.Controls.PanelEx panelEx15;
        private Xr.Common.Controls.PanelEx panelEx16;
        private Xr.Common.Controls.PanelEx panelEx17;
        private Xr.Common.Controls.PanelEx panelEx13;
        private Xr.Common.Controls.PanelEx panelEx14;
        private System.Windows.Forms.CheckBox cb_AutoRefresh;
        private Xr.Common.Controls.ButtonControl buttonControl3;
        private System.Windows.Forms.Panel panel2;
        private Xr.Common.Controls.GroupBorderPanel groupBorderPanel6;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraGrid.GridControl gc_BlackList;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_BlackList;
        private DevExpress.XtraGrid.Columns.GridColumn select;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.GridControl gc_PatientList;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_PatientList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit2;
        private Xr.Common.Controls.ButtonControl buttonControl2;
        private System.Windows.Forms.Timer timer1;
        //private Xr.Common.Controls.TXButton txButton3;
        //private Xr.Common.Controls.TXButton txButton2;
        //private Xr.Common.Controls.TXButton txButton1;

    }
}
