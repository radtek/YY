namespace Xr.RtManager
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmHeartbeat = new System.Windows.Forms.Timer(this.components);
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panelEnhanced1 = new Xr.Common.Controls.PanelEnhanced();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panMenuBar = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx1 = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx3 = new Xr.Common.Controls.PanelEx(this.components);
            this.panelEx2 = new Xr.Common.Controls.PanelEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SysTime = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEnhanced1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panMenuBar.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmHeartbeat
            // 
            this.tmHeartbeat.Interval = 30000;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "navBarGroup1";
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "navBarGroup2";
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "navBarGroup3";
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "navBarItem1";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "navBarItem2";
            this.navBarItem2.Name = "navBarItem2";
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(1, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(843, 699);
            this.panel7.TabIndex = 0;
            // 
            // panelEnhanced1
            // 
            this.panelEnhanced1.BackgroundImage = global::Xr.RtManager.Properties.Resources.bg1;
            this.panelEnhanced1.Controls.Add(this.xtraTabControl1);
            this.panelEnhanced1.Controls.Add(this.panel4);
            this.panelEnhanced1.Controls.Add(this.panel5);
            this.panelEnhanced1.Controls.Add(this.panel1);
            this.panelEnhanced1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEnhanced1.Location = new System.Drawing.Point(3, 0);
            this.panelEnhanced1.Name = "panelEnhanced1";
            this.panelEnhanced1.Size = new System.Drawing.Size(1061, 729);
            this.panelEnhanced1.TabIndex = 0;
            this.panelEnhanced1.SizeChanged += new System.EventHandler(this.panelEnhanced1_SizeChanged);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabControl1.Appearance.Options.UseBackColor = true;
            this.xtraTabControl1.Appearance.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(166)))), ((int)(((byte)(137)))));
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseForeColor = true;
            this.xtraTabControl1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(156, 0);
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new System.Drawing.Size(905, 705);
            this.xtraTabControl1.TabIndex = 20;
            this.xtraTabControl1.CloseButtonClick += new System.EventHandler(this.xtraTabControl1_CloseButtonClick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(151, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(5, 705);
            this.panel4.TabIndex = 19;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.panMenuBar);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(151, 705);
            this.panel5.TabIndex = 17;
            // 
            // panMenuBar
            // 
            this.panMenuBar.BackColor = System.Drawing.Color.Transparent;
            this.panMenuBar.BorderColor = System.Drawing.Color.Black;
            this.panMenuBar.BorderSize = 1;
            this.panMenuBar.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panMenuBar.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panMenuBar.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panMenuBar.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panMenuBar.Controls.Add(this.panelEx1);
            this.panMenuBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMenuBar.Location = new System.Drawing.Point(0, 0);
            this.panMenuBar.Name = "panMenuBar";
            this.panMenuBar.Padding = new System.Windows.Forms.Padding(1);
            this.panMenuBar.Size = new System.Drawing.Size(151, 701);
            this.panMenuBar.TabIndex = 5;
            // 
            // panelEx1
            // 
            this.panelEx1.BorderColor = System.Drawing.Color.Black;
            this.panelEx1.BorderSize = 1;
            this.panelEx1.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(1, 1);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(149, 164);
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Visible = false;
            // 
            // panelEx3
            // 
            this.panelEx3.BorderColor = System.Drawing.Color.Black;
            this.panelEx3.BorderSize = 1;
            this.panelEx3.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx3.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx3.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx3.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx3.Location = new System.Drawing.Point(0, 36);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(155, 109);
            this.panelEx3.TabIndex = 1;
            // 
            // panelEx2
            // 
            this.panelEx2.BorderColor = System.Drawing.Color.Black;
            this.panelEx2.BorderSize = 1;
            this.panelEx2.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx2.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx2.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx2.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx2.Controls.Add(this.label3);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(149, 30);
            this.panelEx2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 701);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(151, 4);
            this.panel6.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 705);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1061, 24);
            this.panel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SysTime);
            this.panel3.Controls.Add(this.userName);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(665, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.panel3.Size = new System.Drawing.Size(396, 24);
            this.panel3.TabIndex = 1;
            // 
            // SysTime
            // 
            this.SysTime.AutoSize = true;
            this.SysTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.SysTime.Location = new System.Drawing.Point(321, 5);
            this.SysTime.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.SysTime.Name = "SysTime";
            this.SysTime.Size = new System.Drawing.Size(65, 12);
            this.SysTime.TabIndex = 3;
            this.SysTime.Text = "2018-10-20";
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Dock = System.Windows.Forms.DockStyle.Left;
            this.userName.Location = new System.Drawing.Point(69, 5);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(41, 12);
            this.userName.TabIndex = 2;
            this.userName.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(10, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "当前用户:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.panel2.Size = new System.Drawing.Size(200, 24);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎使用预约分诊系统";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 729);
            this.Controls.Add(this.panelEnhanced1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelEnhanced1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panMenuBar.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tmHeartbeat;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private System.Windows.Forms.Panel panel7;
        private Xr.Common.Controls.PanelEnhanced panelEnhanced1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label SysTime;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private Xr.Common.Controls.PanelEx panMenuBar;
        private Xr.Common.Controls.PanelEx panelEx1;
        private Xr.Common.Controls.PanelEx panelEx3;
        private Xr.Common.Controls.PanelEx panelEx2;
        private System.Windows.Forms.Label label3;

    }
}