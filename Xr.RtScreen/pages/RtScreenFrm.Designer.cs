namespace Xr.RtScreen.pages
{
    partial class RtScreenFrm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer();
            this.scrollingTexts1 = new Xr.RtScreen.RtUserContronl.ScrollingTexts();
            this.scrollingText1 = new Xr.RtScreen.RtUserContronl.ScrollingText();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.scrollingText1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1024, 56);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.tableLayoutPanel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 56);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1024, 431);
            this.panelControl2.TabIndex = 1;
            this.panelControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl2_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 431);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tableLayoutPanel1_CellPaint);
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.panel1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 487);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1024, 68);
            this.panelControl3.TabIndex = 2;
            this.panelControl3.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl3_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.scrollingTexts1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 68);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // scrollingTexts1
            // 
            this.scrollingTexts1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollingTexts1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.scrollingTexts1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.scrollingTexts1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scrollingTexts1.ForeColor = System.Drawing.Color.Yellow;
            this.scrollingTexts1.ForegroundBrush = null;
            this.scrollingTexts1.Location = new System.Drawing.Point(1, 4);
            this.scrollingTexts1.Margin = new System.Windows.Forms.Padding(1);
            this.scrollingTexts1.Name = "scrollingTexts1";
            this.scrollingTexts1.ScrollText = "";
            this.scrollingTexts1.ShowBorder = true;
            this.scrollingTexts1.Size = new System.Drawing.Size(1022, 63);
            this.scrollingTexts1.TabIndex = 0;
            this.scrollingTexts1.Text = "scrollingTexts1";
            this.scrollingTexts1.TextScrollDistance = 2;
            this.scrollingTexts1.TextScrollSpeed = 10;
            this.scrollingTexts1.Paint += new System.Windows.Forms.PaintEventHandler(this.scrollingTexts1_Paint);
            // 
            // scrollingText1
            // 
            this.scrollingText1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.scrollingText1.BorderColor = System.Drawing.Color.Black;
            this.scrollingText1.Cursor = System.Windows.Forms.Cursors.Default;
            this.scrollingText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollingText1.Enabled = false;
            this.scrollingText1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scrollingText1.ForeColor = System.Drawing.Color.Yellow;
            this.scrollingText1.ForegroundBrush = null;
            this.scrollingText1.Location = new System.Drawing.Point(0, 0);
            this.scrollingText1.Margin = new System.Windows.Forms.Padding(1);
            this.scrollingText1.Name = "scrollingText1";
            this.scrollingText1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.scrollingText1.ScrollDirection = Xr.RtScreen.RtUserContronl.ScrollDirection.RightToLeft;
            this.scrollingText1.ScrollText = "";
            this.scrollingText1.ShowBorder = false;
            this.scrollingText1.Size = new System.Drawing.Size(1024, 56);
            this.scrollingText1.StopScrollOnMouseOver = false;
            this.scrollingText1.TabIndex = 0;
            this.scrollingText1.Text = "scrollingText1";
            this.scrollingText1.TextScrollDistance = 2;
            this.scrollingText1.TextScrollSpeed = 10;
            this.scrollingText1.VerticleTextPosition = Xr.RtScreen.RtUserContronl.VerticleTextPosition.Center;
            this.scrollingText1.Paint += new System.Windows.Forms.PaintEventHandler(this.scrollingText1_Paint);
            // 
            // RtScreenFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "RtScreenFrm";
            this.Size = new System.Drawing.Size(1024, 555);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private RtUserContronl.ScrollingTexts scrollingTexts1;
        private System.Windows.Forms.Panel panel1;
        //private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        //private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        //private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        //private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        //private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        //private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private RtUserContronl.ScrollingText scrollingText1;//跑马列
        //private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        //private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        //private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}
