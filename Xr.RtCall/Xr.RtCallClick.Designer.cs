namespace Xr.RtCall
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button3 = new System.Windows.Forms.Button();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panel_MainFrm = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.skinbutLook = new CCWin.SkinControl.SkinButton();
            this.skinbutBig = new CCWin.SkinControl.SkinButton();
            this.skinButNext = new CCWin.SkinControl.SkinButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel_MainFrm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(488, 1);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 25);
            this.button3.TabIndex = 0;
            this.button3.TabStop = false;
            this.button3.Text = "×";
            this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panel_MainFrm);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(515, 528);
            this.panelControl1.TabIndex = 10;
            // 
            // panel_MainFrm
            // 
            this.panel_MainFrm.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel_MainFrm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MainFrm.Location = new System.Drawing.Point(0, 28);
            this.panel_MainFrm.Name = "panel_MainFrm";
            this.panel_MainFrm.Size = new System.Drawing.Size(515, 500);
            this.panel_MainFrm.TabIndex = 3;
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(103)))));
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.panel1);
            this.panelControl3.Controls.Add(this.button3);
            this.panelControl3.Controls.Add(this.skinbutLook);
            this.panelControl3.Controls.Add(this.skinButton1);
            this.panelControl3.Controls.Add(this.skinbutBig);
            this.panelControl3.Controls.Add(this.skinButNext);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(515, 28);
            this.panelControl3.TabIndex = 2;
            this.panelControl3.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl3_Paint);
            this.panelControl3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GreenFrmPanel_MouseDown);
            this.panelControl3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GreenFrmPanel_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(84, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 27);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "等待呼叫病人 [请稍候...]";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GreenFrmPanel_MouseDown);
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GreenFrmPanel_MouseMove);
            // 
            // skinbutLook
            // 
            this.skinbutLook.BackColor = System.Drawing.Color.Transparent;
            this.skinbutLook.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.skinbutLook.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.skinbutLook.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinbutLook.DownBack = null;
            this.skinbutLook.Font = new System.Drawing.Font("微软雅黑", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.skinbutLook.ForeColor = System.Drawing.Color.White;
            this.skinbutLook.IsDrawBorder = false;
            this.skinbutLook.IsDrawGlass = false;
            this.skinbutLook.IsEnabledDraw = false;
            this.skinbutLook.Location = new System.Drawing.Point(358, 2);
            this.skinbutLook.MouseBack = null;
            this.skinbutLook.Name = "skinbutLook";
            this.skinbutLook.NormlBack = null;
            this.skinbutLook.Radius = 4;
            this.skinbutLook.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinbutLook.Size = new System.Drawing.Size(69, 24);
            this.skinbutLook.TabIndex = 0;
            this.skinbutLook.Text = "临时停诊";
            this.skinbutLook.UseVisualStyleBackColor = false;
            this.skinbutLook.Click += new System.EventHandler(this.skinbutLook_Click);
            // 
            // skinbutBig
            // 
            this.skinbutBig.BackColor = System.Drawing.Color.Transparent;
            this.skinbutBig.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.skinbutBig.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.skinbutBig.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinbutBig.DownBack = null;
            this.skinbutBig.Font = new System.Drawing.Font("微软雅黑", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.skinbutBig.ForeColor = System.Drawing.Color.White;
            this.skinbutBig.IsDrawBorder = false;
            this.skinbutBig.IsDrawGlass = false;
            this.skinbutBig.IsEnabledDraw = false;
            this.skinbutBig.Location = new System.Drawing.Point(433, 2);
            this.skinbutBig.MouseBack = null;
            this.skinbutBig.Name = "skinbutBig";
            this.skinbutBig.NormlBack = null;
            this.skinbutBig.Radius = 4;
            this.skinbutBig.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinbutBig.Size = new System.Drawing.Size(49, 24);
            this.skinbutBig.TabIndex = 0;
            this.skinbutBig.Text = "展开";
            this.skinbutBig.UseVisualStyleBackColor = false;
            this.skinbutBig.Click += new System.EventHandler(this.skinbutBig_Click);
            // 
            // skinButNext
            // 
            this.skinButNext.BackColor = System.Drawing.Color.Transparent;
            this.skinButNext.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.skinButNext.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.skinButNext.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButNext.DownBack = null;
            this.skinButNext.Font = new System.Drawing.Font("微软雅黑", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.skinButNext.ForeColor = System.Drawing.Color.White;
            this.skinButNext.IsDrawBorder = false;
            this.skinButNext.IsDrawGlass = false;
            this.skinButNext.IsEnabledDraw = false;
            this.skinButNext.Location = new System.Drawing.Point(2, 2);
            this.skinButNext.MouseBack = null;
            this.skinButNext.Name = "skinButNext";
            this.skinButNext.NormlBack = null;
            this.skinButNext.Radius = 4;
            this.skinButNext.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButNext.Size = new System.Drawing.Size(75, 24);
            this.skinButNext.TabIndex = 0;
            this.skinButNext.Text = "下";
            this.skinButNext.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.skinButNext.UseVisualStyleBackColor = false;
            this.skinButNext.Click += new System.EventHandler(this.skinButNext_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(224)))), ((int)(((byte)(158)))));
            this.skinButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(224)))), ((int)(((byte)(158)))));
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Font = new System.Drawing.Font("微软雅黑", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.skinButton1.ForeColor = System.Drawing.Color.White;
            this.skinButton1.IsDrawBorder = false;
            this.skinButton1.IsDrawGlass = false;
            this.skinButton1.IsEnabledDraw = false;
            this.skinButton1.Location = new System.Drawing.Point(314, 2);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Radius = 4;
            this.skinButton1.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton1.Size = new System.Drawing.Size(40, 24);
            this.skinButton1.TabIndex = 0;
            this.skinButton1.Text = "完";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 528);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "叫号";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel_MainFrm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        public DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private CCWin.SkinControl.SkinButton skinbutBig;
        private CCWin.SkinControl.SkinButton skinButNext;
        private CCWin.SkinControl.SkinButton skinbutLook;
        public DevExpress.XtraEditors.PanelControl panel_MainFrm;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer2;
        private CCWin.SkinControl.SkinButton skinButton1;

    }
}

