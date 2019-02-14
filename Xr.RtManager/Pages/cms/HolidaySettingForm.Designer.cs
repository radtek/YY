namespace Xr.RtManager.Pages.cms
{
    partial class HolidaySettingForm
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.butClose = new Xr.Common.Controls.ButtonControl();
            this.butContronl = new Xr.Common.Controls.ButtonControl();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.label5 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tename = new DevExpress.XtraEditors.TextEdit();
            this.tecode = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pageControl1 = new Xr.Common.Controls.PageControl();
            this.gc_Holiday = new DevExpress.XtraGrid.GridControl();
            this.gv_Holiday = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.borderPanel1 = new Xr.Common.Controls.BorderPanel();
            this.btnDel = new Xr.Common.Controls.ButtonControl();
            this.btnSave = new Xr.Common.Controls.ButtonControl();
            this.btnAdd = new Xr.Common.Controls.ButtonControl();
            this.dcHodily = new Xr.Common.Controls.DataController(this.components);
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tename.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tecode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Holiday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Holiday)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.borderPanel1)).BeginInit();
            this.borderPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.pageControl1);
            this.panel4.Controls.Add(this.gc_Holiday);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 60);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(920, 454);
            this.panel4.TabIndex = 65;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbYear);
            this.groupBox1.Controls.Add(this.dateEdit2);
            this.groupBox1.Controls.Add(this.dateEdit1);
            this.groupBox1.Controls.Add(this.butClose);
            this.groupBox1.Controls.Add(this.butContronl);
            this.groupBox1.Controls.Add(this.radioGroup2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textEdit1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tename);
            this.groupBox1.Controls.Add(this.tecode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(10, 268);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(900, 176);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息编辑区";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(693, 119);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(191, 27);
            this.cmbYear.TabIndex = 118;
            this.cmbYear.Visible = false;
            // 
            // dateEdit2
            // 
            this.dcHodily.SetDataMember(this.dateEdit2, "endDate");
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(503, 69);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEdit2.Properties.Appearance.Options.UseFont = true;
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateEdit2.Size = new System.Drawing.Size(300, 26);
            this.dateEdit2.TabIndex = 109;
            // 
            // dateEdit1
            // 
            this.dcHodily.SetDataMember(this.dateEdit1, "beginDate");
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(503, 28);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateEdit1.Properties.Appearance.Options.UseFont = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateEdit1.Size = new System.Drawing.Size(300, 26);
            this.dateEdit1.TabIndex = 108;
            // 
            // butClose
            // 
            this.butClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.butClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.butClose.HoverBackColor = System.Drawing.Color.Empty;
            this.butClose.Location = new System.Drawing.Point(612, 116);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 30);
            this.butClose.Style = Xr.Common.Controls.ButtonStyle.Return;
            this.butClose.TabIndex = 117;
            this.butClose.Text = "关闭";
            this.butClose.Visible = false;
            // 
            // butContronl
            // 
            this.butContronl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.butContronl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.butContronl.HoverBackColor = System.Drawing.Color.Empty;
            this.butContronl.Location = new System.Drawing.Point(503, 116);
            this.butContronl.Name = "butContronl";
            this.butContronl.Size = new System.Drawing.Size(75, 30);
            this.butContronl.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.butContronl.TabIndex = 112;
            this.butContronl.Text = "保存";
            this.butContronl.Click += new System.EventHandler(this.butContronl_Click);
            // 
            // radioGroup2
            // 
            this.dcHodily.SetDataMember(this.radioGroup2, "isUse");
            this.radioGroup2.EditValue = "1";
            this.radioGroup2.Location = new System.Drawing.Point(112, 112);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup2.Properties.Appearance.Options.UseFont = true;
            this.radioGroup2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup2.Properties.Columns = 2;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "启用"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "禁用")});
            this.radioGroup2.Size = new System.Drawing.Size(299, 30);
            this.radioGroup2.TabIndex = 110;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(40, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 114;
            this.label5.Text = "是否启用：";
            // 
            // textEdit1
            // 
            this.dcHodily.SetDataMember(this.textEdit1, "id");
            this.textEdit1.Location = new System.Drawing.Point(853, 29);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AutoHeight = false;
            this.textEdit1.Size = new System.Drawing.Size(14, 28);
            this.textEdit1.TabIndex = 111;
            this.textEdit1.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(432, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 113;
            this.label6.Text = "结束日期：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(432, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 112;
            this.label3.Text = "开始日期：";
            // 
            // tename
            // 
            this.dcHodily.SetDataMember(this.tename, "name");
            this.tename.Location = new System.Drawing.Point(111, 26);
            this.tename.Name = "tename";
            this.tename.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tename.Properties.Appearance.Options.UseFont = true;
            this.tename.Properties.AutoHeight = false;
            this.tename.Size = new System.Drawing.Size(300, 28);
            this.tename.TabIndex = 106;
            // 
            // tecode
            // 
            this.dcHodily.SetDataMember(this.tecode, "year");
            this.tecode.Location = new System.Drawing.Point(111, 69);
            this.tecode.Name = "tecode";
            this.tecode.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tecode.Properties.Appearance.Options.UseFont = true;
            this.tecode.Properties.AutoHeight = false;
            this.tecode.Size = new System.Drawing.Size(300, 28);
            this.tecode.TabIndex = 107;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(40, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 109;
            this.label2.Text = "所属年份：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 108;
            this.label1.Text = "节假日名称：";
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.CurrentPage = 1;
            this.pageControl1.Location = new System.Drawing.Point(10, 223);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageSize = 20;
            this.pageControl1.Record = 0;
            this.pageControl1.Size = new System.Drawing.Size(900, 39);
            this.pageControl1.TabIndex = 50;
            this.pageControl1.Query += new Xr.Common.Controls.PageControl.QueryDelegate(this.pageControl1_Query);
            // 
            // gc_Holiday
            // 
            this.gc_Holiday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_Holiday.Location = new System.Drawing.Point(10, 10);
            this.gc_Holiday.MainView = this.gv_Holiday;
            this.gc_Holiday.Name = "gc_Holiday";
            this.gc_Holiday.Size = new System.Drawing.Size(900, 213);
            this.gc_Holiday.TabIndex = 49;
            this.gc_Holiday.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Holiday});
            // 
            // gv_Holiday
            // 
            this.gv_Holiday.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gv_Holiday.Appearance.EvenRow.Options.UseBackColor = true;
            this.gv_Holiday.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_Holiday.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_Holiday.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_Holiday.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_Holiday.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gv_Holiday.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gv_Holiday.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gv_Holiday.Appearance.OddRow.Options.UseBackColor = true;
            this.gv_Holiday.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gv_Holiday.Appearance.Row.Options.UseFont = true;
            this.gv_Holiday.Appearance.Row.Options.UseTextOptions = true;
            this.gv_Holiday.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_Holiday.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gv_Holiday.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gv_Holiday.ColumnPanelRowHeight = 30;
            this.gv_Holiday.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6});
            this.gv_Holiday.GridControl = this.gc_Holiday;
            this.gv_Holiday.Name = "gv_Holiday";
            this.gv_Holiday.OptionsBehavior.ReadOnly = true;
            this.gv_Holiday.OptionsCustomization.AllowFilter = false;
            this.gv_Holiday.OptionsCustomization.AllowSort = false;
            this.gv_Holiday.OptionsMenu.EnableColumnMenu = false;
            this.gv_Holiday.OptionsView.AllowHtmlDrawHeaders = true;
            this.gv_Holiday.OptionsView.EnableAppearanceEvenRow = true;
            this.gv_Holiday.OptionsView.RowAutoHeight = true;
            this.gv_Holiday.OptionsView.ShowGroupPanel = false;
            this.gv_Holiday.RowHeight = 30;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "主键";
            this.gridColumn5.FieldName = "id";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.Caption = "名称";
            this.gridColumn1.FieldName = "name";
            this.gridColumn1.MinWidth = 25;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "所属年份";
            this.gridColumn2.FieldName = "year";
            this.gridColumn2.MinWidth = 25;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "开始日期";
            this.gridColumn3.FieldName = "beginDate";
            this.gridColumn3.MinWidth = 25;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "结束日期";
            this.gridColumn4.FieldName = "endDate";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "gridColumn6";
            this.gridColumn6.FieldName = "isUse";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.borderPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.panel1.Size = new System.Drawing.Size(920, 60);
            this.panel1.TabIndex = 64;
            // 
            // borderPanel1
            // 
            this.borderPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.borderPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.borderPanel1.Controls.Add(this.btnDel);
            this.borderPanel1.Controls.Add(this.btnSave);
            this.borderPanel1.Controls.Add(this.btnAdd);
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
            this.borderPanel1.Size = new System.Drawing.Size(900, 50);
            this.borderPanel1.TabIndex = 1;
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnDel.HoverBackColor = System.Drawing.Color.Empty;
            this.btnDel.Location = new System.Drawing.Point(210, 10);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 30);
            this.btnDel.Style = Xr.Common.Controls.ButtonStyle.Del;
            this.btnDel.TabIndex = 76;
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnSave.HoverBackColor = System.Drawing.Color.Empty;
            this.btnSave.Location = new System.Drawing.Point(120, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.Style = Xr.Common.Controls.ButtonStyle.Query;
            this.btnSave.TabIndex = 75;
            this.btnSave.Text = "修改";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnAdd.HoverBackColor = System.Drawing.Color.Empty;
            this.btnAdd.Location = new System.Drawing.Point(30, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.Style = Xr.Common.Controls.ButtonStyle.Query;
            this.btnAdd.TabIndex = 74;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // HolidaySettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "HolidaySettingForm";
            this.Size = new System.Drawing.Size(920, 514);
            this.Load += new System.EventHandler(this.HolidaySettingForm_Load);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tename.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tecode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Holiday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Holiday)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.borderPanel1)).EndInit();
            this.borderPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraGrid.GridControl gc_Holiday;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Holiday;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Panel panel1;
        private Xr.Common.Controls.BorderPanel borderPanel1;
        private Xr.Common.Controls.ButtonControl btnDel;
        private Xr.Common.Controls.ButtonControl btnSave;
        private Xr.Common.Controls.ButtonControl btnAdd;
        private Xr.Common.Controls.PageControl pageControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit tename;
        private DevExpress.XtraEditors.TextEdit tecode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private System.Windows.Forms.Label label5;
        private Xr.Common.Controls.ButtonControl butClose;
        private Xr.Common.Controls.ButtonControl butContronl;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private Xr.Common.Controls.DataController dcHodily;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private System.Windows.Forms.ComboBox cmbYear;
    }
}
