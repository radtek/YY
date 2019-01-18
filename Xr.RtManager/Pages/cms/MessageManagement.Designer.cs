namespace Xr.RtManager.Pages.cms
{
    partial class MessageManagement
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
            this.pageControl1 = new Xr.Common.Controls.PageControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.meUpdateDesc = new DevExpress.XtraEditors.MemoEdit();
            this.teStardTime = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butClose = new Xr.Common.Controls.ButtonControl();
            this.butContronl = new Xr.Common.Controls.ButtonControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.gc_Message = new DevExpress.XtraGrid.GridControl();
            this.gv_Message = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.borderPanel1 = new Xr.Common.Controls.BorderPanel();
            this.btnDel = new Xr.Common.Controls.ButtonControl();
            this.btnSave = new Xr.Common.Controls.ButtonControl();
            this.btnAdd = new Xr.Common.Controls.ButtonControl();
            this.dcMessage = new Xr.Common.Controls.DataController(this.components);
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meUpdateDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStardTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Message)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Message)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.borderPanel1)).BeginInit();
            this.borderPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.CurrentPage = 1;
            this.pageControl1.Location = new System.Drawing.Point(12, 219);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageSize = 20;
            this.pageControl1.Record = 0;
            this.pageControl1.Size = new System.Drawing.Size(890, 39);
            this.pageControl1.TabIndex = 50;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.pageControl1);
            this.panel4.Controls.Add(this.gc_Message);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 70);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(15, 20, 15, 20);
            this.panel4.Size = new System.Drawing.Size(920, 444);
            this.panel4.TabIndex = 67;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.meUpdateDesc);
            this.groupBox1.Controls.Add(this.teStardTime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.butClose);
            this.groupBox1.Controls.Add(this.butContronl);
            this.groupBox1.Controls.Add(this.textEdit1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(15, 256);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(890, 168);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息编辑区";
            // 
            // meUpdateDesc
            // 
            this.dcMessage.SetDataMember(this.meUpdateDesc, "content");
            this.meUpdateDesc.Location = new System.Drawing.Point(87, 66);
            this.meUpdateDesc.Name = "meUpdateDesc";
            this.meUpdateDesc.Size = new System.Drawing.Size(300, 77);
            this.meUpdateDesc.TabIndex = 119;
            this.meUpdateDesc.UseOptimizedRendering = true;
            // 
            // teStardTime
            // 
            this.dcMessage.SetDataMember(this.teStardTime, "type");
            this.teStardTime.Location = new System.Drawing.Point(87, 23);
            this.teStardTime.Name = "teStardTime";
            this.teStardTime.Properties.AutoHeight = false;
            this.teStardTime.Size = new System.Drawing.Size(300, 28);
            this.teStardTime.TabIndex = 118;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(16, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 121;
            this.label6.Text = "模板内容：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(16, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 120;
            this.label3.Text = "模板类型：";
            // 
            // butClose
            // 
            this.butClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.butClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.butClose.HoverBackColor = System.Drawing.Color.Empty;
            this.butClose.Location = new System.Drawing.Point(524, 113);
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
            this.butContronl.Location = new System.Drawing.Point(415, 113);
            this.butContronl.Name = "butContronl";
            this.butContronl.Size = new System.Drawing.Size(75, 30);
            this.butContronl.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.butContronl.TabIndex = 116;
            this.butContronl.Text = "保存";
            this.butContronl.Click += new System.EventHandler(this.butContronl_Click);
            // 
            // textEdit1
            // 
            this.dcMessage.SetDataMember(this.textEdit1, "id");
            this.textEdit1.Location = new System.Drawing.Point(809, 26);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AutoHeight = false;
            this.textEdit1.Size = new System.Drawing.Size(14, 28);
            this.textEdit1.TabIndex = 111;
            this.textEdit1.Visible = false;
            // 
            // gc_Message
            // 
            this.gc_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_Message.Location = new System.Drawing.Point(15, 20);
            this.gc_Message.MainView = this.gv_Message;
            this.gc_Message.Name = "gc_Message";
            this.gc_Message.Size = new System.Drawing.Size(890, 193);
            this.gc_Message.TabIndex = 49;
            this.gc_Message.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Message});
            // 
            // gv_Message
            // 
            this.gv_Message.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gv_Message.Appearance.EvenRow.Options.UseBackColor = true;
            this.gv_Message.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gv_Message.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_Message.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_Message.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_Message.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gv_Message.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gv_Message.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gv_Message.Appearance.OddRow.Options.UseBackColor = true;
            this.gv_Message.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.gv_Message.Appearance.Row.Options.UseFont = true;
            this.gv_Message.Appearance.Row.Options.UseTextOptions = true;
            this.gv_Message.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_Message.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gv_Message.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gv_Message.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gv_Message.GridControl = this.gc_Message;
            this.gv_Message.Name = "gv_Message";
            this.gv_Message.OptionsBehavior.ReadOnly = true;
            this.gv_Message.OptionsCustomization.AllowFilter = false;
            this.gv_Message.OptionsCustomization.AllowSort = false;
            this.gv_Message.OptionsMenu.EnableColumnMenu = false;
            this.gv_Message.OptionsView.AllowHtmlDrawHeaders = true;
            this.gv_Message.OptionsView.EnableAppearanceEvenRow = true;
            this.gv_Message.OptionsView.RowAutoHeight = true;
            this.gv_Message.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "主键";
            this.gridColumn5.FieldName = "id";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "所属医院";
            this.gridColumn2.FieldName = "Hostile";
            this.gridColumn2.MinWidth = 25;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 154;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "模本类型";
            this.gridColumn3.FieldName = "type";
            this.gridColumn3.MinWidth = 25;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 158;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "模版内容";
            this.gridColumn4.FieldName = "content";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 485;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.borderPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(15, 20, 15, 0);
            this.panel1.Size = new System.Drawing.Size(920, 70);
            this.panel1.TabIndex = 66;
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
            // MessageManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "MessageManagement";
            this.Size = new System.Drawing.Size(920, 514);
            this.Load += new System.EventHandler(this.MessageManagement_Load);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meUpdateDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStardTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Message)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Message)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.borderPanel1)).EndInit();
            this.borderPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Xr.Common.Controls.PageControl pageControl1;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraGrid.GridControl gc_Message;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Message;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Panel panel1;
        private Xr.Common.Controls.BorderPanel borderPanel1;
        private Xr.Common.Controls.ButtonControl btnDel;
        private Xr.Common.Controls.ButtonControl btnSave;
        private Xr.Common.Controls.ButtonControl btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private Xr.Common.Controls.ButtonControl butClose;
        private Xr.Common.Controls.ButtonControl butContronl;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.MemoEdit meUpdateDesc;
        private DevExpress.XtraEditors.TextEdit teStardTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private Xr.Common.Controls.DataController dcMessage;
    }
}
