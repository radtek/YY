namespace Xr.RtManager.Pages.triage
{
    partial class TemporaryStopFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonControl1 = new Xr.Common.Controls.ButtonControl();
            this.buttonControl2 = new Xr.Common.Controls.ButtonControl();
            this.treeKeshi = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeDoctor = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeClinc = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList3 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeKeshi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDoctor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeClinc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(81, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "科室：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(81, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "诊室：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(81, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "医生：";
            // 
            // buttonControl1
            // 
            this.buttonControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.buttonControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.buttonControl1.HoverBackColor = System.Drawing.Color.Empty;
            this.buttonControl1.Location = new System.Drawing.Point(128, 219);
            this.buttonControl1.Name = "buttonControl1";
            this.buttonControl1.Size = new System.Drawing.Size(75, 30);
            this.buttonControl1.Style = Xr.Common.Controls.ButtonStyle.Query;
            this.buttonControl1.TabIndex = 3;
            this.buttonControl1.Text = "保存";
            this.buttonControl1.Click += new System.EventHandler(this.buttonControl1_Click);
            // 
            // buttonControl2
            // 
            this.buttonControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.buttonControl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.buttonControl2.HoverBackColor = System.Drawing.Color.Empty;
            this.buttonControl2.Location = new System.Drawing.Point(222, 219);
            this.buttonControl2.Name = "buttonControl2";
            this.buttonControl2.Size = new System.Drawing.Size(75, 30);
            this.buttonControl2.Style = Xr.Common.Controls.ButtonStyle.Return;
            this.buttonControl2.TabIndex = 4;
            this.buttonControl2.Text = "返回";
            this.buttonControl2.Click += new System.EventHandler(this.buttonControl2_Click);
            // 
            // treeKeshi
            // 
            this.treeKeshi.EditValue = "";
            this.treeKeshi.Location = new System.Drawing.Point(132, 41);
            this.treeKeshi.Name = "treeKeshi";
            this.treeKeshi.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeKeshi.Properties.Appearance.Options.UseFont = true;
            this.treeKeshi.Properties.AutoHeight = false;
            this.treeKeshi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeKeshi.Properties.NullText = "";
            this.treeKeshi.Properties.TreeList = this.treeList1;
            this.treeKeshi.Size = new System.Drawing.Size(169, 31);
            this.treeKeshi.TabIndex = 127;
            this.treeKeshi.EditValueChanged += new System.EventHandler(this.treeKeshi_EditValueChanged);
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.Location = new System.Drawing.Point(0, 63);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.EnableFiltering = true;
            this.treeList1.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList1.Size = new System.Drawing.Size(400, 200);
            this.treeList1.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceCell.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.Caption = "科室列表";
            this.treeListColumn1.FieldName = "name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeDoctor
            // 
            this.treeDoctor.EditValue = "";
            this.treeDoctor.Location = new System.Drawing.Point(132, 103);
            this.treeDoctor.Name = "treeDoctor";
            this.treeDoctor.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeDoctor.Properties.Appearance.Options.UseFont = true;
            this.treeDoctor.Properties.AutoHeight = false;
            this.treeDoctor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeDoctor.Properties.NullText = "";
            this.treeDoctor.Properties.TreeList = this.treeList2;
            this.treeDoctor.Size = new System.Drawing.Size(169, 31);
            this.treeDoctor.TabIndex = 128;
            this.treeDoctor.EditValueChanged += new System.EventHandler(this.treeDoctor_EditValueChanged);
            // 
            // treeList2
            // 
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeList2.Location = new System.Drawing.Point(0, 63);
            this.treeList2.Name = "treeList2";
            this.treeList2.OptionsBehavior.EnableFiltering = true;
            this.treeList2.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList2.Size = new System.Drawing.Size(400, 200);
            this.treeList2.TabIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceCell.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.Caption = "科室列表";
            this.treeListColumn2.FieldName = "name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // treeClinc
            // 
            this.treeClinc.EditValue = "";
            this.treeClinc.Location = new System.Drawing.Point(132, 163);
            this.treeClinc.Name = "treeClinc";
            this.treeClinc.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeClinc.Properties.Appearance.Options.UseFont = true;
            this.treeClinc.Properties.AutoHeight = false;
            this.treeClinc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeClinc.Properties.NullText = "";
            this.treeClinc.Properties.TreeList = this.treeList3;
            this.treeClinc.Size = new System.Drawing.Size(169, 31);
            this.treeClinc.TabIndex = 129;
            this.treeClinc.EditValueChanged += new System.EventHandler(this.treeClinc_EditValueChanged);
            // 
            // treeList3
            // 
            this.treeList3.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn3});
            this.treeList3.Location = new System.Drawing.Point(0, 63);
            this.treeList3.Name = "treeList3";
            this.treeList3.OptionsBehavior.EnableFiltering = true;
            this.treeList3.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList3.Size = new System.Drawing.Size(479, 200);
            this.treeList3.TabIndex = 0;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn3.AppearanceCell.Options.UseFont = true;
            this.treeListColumn3.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn3.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn3.Caption = "科室列表";
            this.treeListColumn3.FieldName = "name";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            // 
            // TemporaryStopFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 299);
            this.Controls.Add(this.treeClinc);
            this.Controls.Add(this.treeDoctor);
            this.Controls.Add(this.treeKeshi);
            this.Controls.Add(this.buttonControl2);
            this.Controls.Add(this.buttonControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemporaryStopFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "临时坐诊设置";
            ((System.ComponentModel.ISupportInitialize)(this.treeKeshi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDoctor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeClinc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Xr.Common.Controls.ButtonControl buttonControl1;
        private Xr.Common.Controls.ButtonControl buttonControl2;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeKeshi;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeDoctor;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeClinc;
        private DevExpress.XtraTreeList.TreeList treeList3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
    }
}