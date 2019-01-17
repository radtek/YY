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
            this.panel4 = new System.Windows.Forms.Panel();
            this.pageControl1 = new Xr.Common.Controls.PageControl();
            this.gc_Holiday = new DevExpress.XtraGrid.GridControl();
            this.gv_Holiday = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.borderPanel1 = new Xr.Common.Controls.BorderPanel();
            this.btnDel = new Xr.Common.Controls.ButtonControl();
            this.btnSave = new Xr.Common.Controls.ButtonControl();
            this.btnAdd = new Xr.Common.Controls.ButtonControl();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Holiday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Holiday)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.borderPanel1)).BeginInit();
            this.borderPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pageControl1);
            this.panel4.Controls.Add(this.gc_Holiday);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 70);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(15, 20, 15, 20);
            this.panel4.Size = new System.Drawing.Size(920, 444);
            this.panel4.TabIndex = 65;
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 1;
            this.pageControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageControl1.Location = new System.Drawing.Point(15, 385);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageSize = 20;
            this.pageControl1.Record = 0;
            this.pageControl1.Size = new System.Drawing.Size(890, 39);
            this.pageControl1.TabIndex = 50;
            // 
            // gc_Holiday
            // 
            this.gc_Holiday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_Holiday.Location = new System.Drawing.Point(15, 20);
            this.gc_Holiday.MainView = this.gv_Holiday;
            this.gc_Holiday.Name = "gc_Holiday";
            this.gc_Holiday.Size = new System.Drawing.Size(890, 359);
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
            this.gv_Holiday.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
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
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.borderPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(15, 20, 15, 0);
            this.panel1.Size = new System.Drawing.Size(920, 70);
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
            this.borderPanel1.Location = new System.Drawing.Point(15, 20);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(890, 50);
            this.borderPanel1.TabIndex = 1;
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnDel.HoverBackColor = System.Drawing.Color.Empty;
            this.btnDel.Location = new System.Drawing.Point(205, 10);
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
            this.btnSave.Location = new System.Drawing.Point(115, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.Style = Xr.Common.Controls.ButtonStyle.Save;
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
            this.panel4.ResumeLayout(false);
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
    }
}
