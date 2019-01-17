namespace Xr.RtManager.Pages.cms
{
    partial class ClinicSettingEdit
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
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.teKeShi = new DevExpress.XtraEditors.TextEdit();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.teID = new DevExpress.XtraEditors.TextEdit();
            this.teType = new DevExpress.XtraEditors.TextEdit();
            this.teTime = new DevExpress.XtraEditors.TextEdit();
            this.teUpdate = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.butClose = new Xr.Common.Controls.ButtonControl();
            this.butAdd = new Xr.Common.Controls.ButtonControl();
            this.dcClinic = new Xr.Common.Controls.DataController();
            ((System.ComponentModel.ISupportInitialize)(this.teKeShi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(56, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 103;
            this.label6.Text = "分诊标识：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(70, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 100;
            this.label3.Text = "诊室号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(56, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 102;
            this.label5.Text = "创建时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(56, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 99;
            this.label2.Text = "诊室名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(56, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "所属科室：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(80, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 101;
            this.label4.Text = "状态 ：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(56, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 102;
            this.label7.Text = "修改时间：";
            // 
            // teKeShi
            // 
            this.dcClinic.SetDataMember(this.teKeShi, "name");
            this.teKeShi.Location = new System.Drawing.Point(126, 23);
            this.teKeShi.Name = "teKeShi";
            this.teKeShi.Properties.AutoHeight = false;
            this.teKeShi.Size = new System.Drawing.Size(300, 28);
            this.teKeShi.TabIndex = 1;
            // 
            // teName
            // 
            this.dcClinic.SetDataMember(this.teName, "code");
            this.teName.Location = new System.Drawing.Point(126, 66);
            this.teName.Name = "teName";
            this.teName.Properties.AutoHeight = false;
            this.teName.Size = new System.Drawing.Size(300, 28);
            this.teName.TabIndex = 2;
            // 
            // teID
            // 
            this.dcClinic.SetDataMember(this.teID, "parent");
            this.teID.Location = new System.Drawing.Point(126, 109);
            this.teID.Name = "teID";
            this.teID.Properties.AutoHeight = false;
            this.teID.Size = new System.Drawing.Size(300, 28);
            this.teID.TabIndex = 3;
            // 
            // teType
            // 
            this.dcClinic.SetDataMember(this.teType, "showSort");
            this.teType.Location = new System.Drawing.Point(126, 150);
            this.teType.Name = "teType";
            this.teType.Properties.AutoHeight = false;
            this.teType.Size = new System.Drawing.Size(300, 28);
            this.teType.TabIndex = 4;
            // 
            // teTime
            // 
            this.dcClinic.SetDataMember(this.teTime, "createDate");
            this.teTime.Location = new System.Drawing.Point(126, 229);
            this.teTime.Name = "teTime";
            this.teTime.Properties.AutoHeight = false;
            this.teTime.Size = new System.Drawing.Size(300, 28);
            this.teTime.TabIndex = 6;
            // 
            // teUpdate
            // 
            this.dcClinic.SetDataMember(this.teUpdate, "updateDate");
            this.teUpdate.Location = new System.Drawing.Point(126, 276);
            this.teUpdate.Name = "teUpdate";
            this.teUpdate.Properties.AutoHeight = false;
            this.teUpdate.Size = new System.Drawing.Size(300, 28);
            this.teUpdate.TabIndex = 7;
            // 
            // radioGroup2
            // 
            this.dcClinic.SetDataMember(this.radioGroup2, "printSort");
            this.radioGroup2.EditValue = "1";
            this.radioGroup2.Location = new System.Drawing.Point(126, 189);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup2.Properties.Appearance.Options.UseFont = true;
            this.radioGroup2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup2.Properties.Columns = 2;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "正常"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "停用")});
            this.radioGroup2.Size = new System.Drawing.Size(234, 30);
            this.radioGroup2.TabIndex = 5;
            // 
            // butClose
            // 
            this.butClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.butClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.butClose.HoverBackColor = System.Drawing.Color.Empty;
            this.butClose.Location = new System.Drawing.Point(277, 356);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 30);
            this.butClose.Style = Xr.Common.Controls.ButtonStyle.Return;
            this.butClose.TabIndex = 9;
            this.butClose.Text = "关闭";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butAdd
            // 
            this.butAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.butAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.butAdd.HoverBackColor = System.Drawing.Color.Empty;
            this.butAdd.Location = new System.Drawing.Point(168, 356);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(75, 30);
            this.butAdd.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.butAdd.TabIndex = 8;
            this.butAdd.Text = "保存";
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // ClinicSettingEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(540, 431);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butAdd);
            this.Controls.Add(this.radioGroup2);
            this.Controls.Add(this.teUpdate);
            this.Controls.Add(this.teTime);
            this.Controls.Add(this.teType);
            this.Controls.Add(this.teID);
            this.Controls.Add(this.teName);
            this.Controls.Add(this.teKeShi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClinicSettingEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加诊室";
            this.Load += new System.EventHandler(this.ClinicSettingEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.teKeShi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit teKeShi;
        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraEditors.TextEdit teID;
        private DevExpress.XtraEditors.TextEdit teType;
        private DevExpress.XtraEditors.TextEdit teTime;
        private DevExpress.XtraEditors.TextEdit teUpdate;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private Xr.Common.Controls.ButtonControl butClose;
        private Xr.Common.Controls.ButtonControl butAdd;
        private Xr.Common.Controls.DataController dcClinic;
    }
}