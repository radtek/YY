namespace Xr.RtManager
{
    partial class UserEdit
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
            this.dcUser = new Xr.Common.Controls.DataController(this.components);
            this.treeCompany = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.teLoginName = new DevExpress.XtraEditors.TextEdit();
            this.tePassword = new DevExpress.XtraEditors.TextEdit();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.textEdit6 = new DevExpress.XtraEditors.TextEdit();
            this.treeOffice = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tePassword2 = new DevExpress.XtraEditors.TextEdit();
            this.teEmail = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.textEdit7 = new DevExpress.XtraEditors.TextEdit();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnUpload = new Xr.Common.Controls.ButtonControl();
            this.btnSelect = new Xr.Common.Controls.ButtonControl();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSave = new Xr.Common.Controls.ButtonControl();
            this.btnCancel = new Xr.Common.Controls.ButtonControl();
            this.panelEx1 = new Xr.Common.Controls.PanelEx(this.components);
            this.richEditor1 = new Xr.Common.Controls.RichEditor();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.treeCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOffice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeCompany
            // 
            this.dcUser.SetDataMember(this.treeCompany, "companyId");
            this.treeCompany.EditValue = "";
            this.treeCompany.Location = new System.Drawing.Point(129, 71);
            this.treeCompany.Name = "treeCompany";
            this.treeCompany.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCompany.Properties.Appearance.Options.UseFont = true;
            this.treeCompany.Properties.AutoHeight = false;
            this.treeCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeCompany.Properties.PopupFormSize = new System.Drawing.Size(232, 250);
            this.treeCompany.Properties.TreeList = this.treeListLookUpEdit1TreeList;
            this.treeCompany.Size = new System.Drawing.Size(232, 29);
            this.treeCompany.TabIndex = 73;
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeListLookUpEdit1TreeList.Location = new System.Drawing.Point(217, 199);
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = true;
            this.treeListLookUpEdit1TreeList.OptionsView.AllowHtmlDrawHeaders = true;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeListLookUpEdit1TreeList.RowHeight = 30;
            this.treeListLookUpEdit1TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEdit1TreeList.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.treeListColumn1.AppearanceCell.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.Caption = "公司";
            this.treeListColumn1.FieldName = "name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // teLoginName
            // 
            this.dcUser.SetDataMember(this.teLoginName, "loginName");
            this.teLoginName.Location = new System.Drawing.Point(489, 117);
            this.teLoginName.Name = "teLoginName";
            this.teLoginName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teLoginName.Properties.Appearance.Options.UseFont = true;
            this.teLoginName.Properties.AutoHeight = false;
            this.teLoginName.Size = new System.Drawing.Size(232, 29);
            this.teLoginName.TabIndex = 74;
            // 
            // tePassword
            // 
            this.dcUser.SetDataMember(this.tePassword, "password");
            this.tePassword.Location = new System.Drawing.Point(129, 162);
            this.tePassword.Name = "tePassword";
            this.tePassword.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePassword.Properties.Appearance.Options.UseFont = true;
            this.tePassword.Properties.AutoHeight = false;
            this.tePassword.Properties.PasswordChar = '*';
            this.tePassword.Size = new System.Drawing.Size(232, 29);
            this.tePassword.TabIndex = 75;
            // 
            // teName
            // 
            this.dcUser.SetDataMember(this.teName, "name");
            this.teName.Location = new System.Drawing.Point(129, 207);
            this.teName.Name = "teName";
            this.teName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teName.Properties.Appearance.Options.UseFont = true;
            this.teName.Properties.AutoHeight = false;
            this.teName.Size = new System.Drawing.Size(232, 29);
            this.teName.TabIndex = 76;
            // 
            // textEdit6
            // 
            this.dcUser.SetDataMember(this.textEdit6, "phone");
            this.textEdit6.Location = new System.Drawing.Point(129, 252);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit6.Properties.Appearance.Options.UseFont = true;
            this.textEdit6.Properties.AutoHeight = false;
            this.textEdit6.Size = new System.Drawing.Size(232, 29);
            this.textEdit6.TabIndex = 77;
            // 
            // treeOffice
            // 
            this.dcUser.SetDataMember(this.treeOffice, "officeId");
            this.treeOffice.EditValue = "";
            this.treeOffice.Location = new System.Drawing.Point(489, 72);
            this.treeOffice.Name = "treeOffice";
            this.treeOffice.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeOffice.Properties.Appearance.Options.UseFont = true;
            this.treeOffice.Properties.AutoHeight = false;
            this.treeOffice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeOffice.Properties.PopupFormSize = new System.Drawing.Size(232, 250);
            this.treeOffice.Properties.TreeList = this.treeList1;
            this.treeOffice.Size = new System.Drawing.Size(233, 29);
            this.treeOffice.TabIndex = 82;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeList1.Location = new System.Drawing.Point(190, 199);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.EnableFiltering = true;
            this.treeList1.OptionsView.AllowHtmlDrawHeaders = true;
            this.treeList1.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList1.RowHeight = 30;
            this.treeList1.Size = new System.Drawing.Size(400, 200);
            this.treeList1.TabIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.treeListColumn2.AppearanceCell.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.Caption = "机构";
            this.treeListColumn2.FieldName = "name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // tePassword2
            // 
            this.dcUser.SetDataMember(this.tePassword2, "newPassword");
            this.tePassword2.Location = new System.Drawing.Point(489, 162);
            this.tePassword2.Name = "tePassword2";
            this.tePassword2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePassword2.Properties.Appearance.Options.UseFont = true;
            this.tePassword2.Properties.AutoHeight = false;
            this.tePassword2.Properties.PasswordChar = '*';
            this.tePassword2.Size = new System.Drawing.Size(233, 29);
            this.tePassword2.TabIndex = 83;
            // 
            // teEmail
            // 
            this.dcUser.SetDataMember(this.teEmail, "email");
            this.teEmail.Location = new System.Drawing.Point(489, 207);
            this.teEmail.Name = "teEmail";
            this.teEmail.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teEmail.Properties.Appearance.Options.UseFont = true;
            this.teEmail.Properties.AutoHeight = false;
            this.teEmail.Size = new System.Drawing.Size(233, 29);
            this.teEmail.TabIndex = 84;
            // 
            // radioGroup1
            // 
            this.dcUser.SetDataMember(this.radioGroup1, "userType");
            this.radioGroup1.EditValue = "0";
            this.radioGroup1.Location = new System.Drawing.Point(127, 117);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "普通用户"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "管理员")});
            this.radioGroup1.Size = new System.Drawing.Size(234, 30);
            this.radioGroup1.TabIndex = 93;
            // 
            // radioGroup2
            // 
            this.dcUser.SetDataMember(this.radioGroup2, "isResetPwd");
            this.radioGroup2.EditValue = "0";
            this.radioGroup2.Location = new System.Drawing.Point(130, 295);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup2.Properties.Appearance.Options.UseFont = true;
            this.radioGroup2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup2.Properties.Columns = 2;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "否")});
            this.radioGroup2.Size = new System.Drawing.Size(234, 30);
            this.radioGroup2.TabIndex = 95;
            // 
            // textEdit7
            // 
            this.textEdit7.Location = new System.Drawing.Point(489, 252);
            this.textEdit7.Name = "textEdit7";
            this.textEdit7.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit7.Properties.Appearance.Options.UseFont = true;
            this.textEdit7.Properties.AutoHeight = false;
            this.textEdit7.Size = new System.Drawing.Size(234, 29);
            this.textEdit7.TabIndex = 85;
            this.textEdit7.Visible = false;
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxControl1.Appearance.Options.UseFont = true;
            this.checkedListBoxControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.checkedListBoxControl1.CheckOnClick = true;
            this.checkedListBoxControl1.Location = new System.Drawing.Point(127, 341);
            this.checkedListBoxControl1.MultiColumn = true;
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(593, 36);
            this.checkedListBoxControl1.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(65, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 39;
            this.label3.Text = "头像：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 40;
            this.label1.Text = "归属公司：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(414, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 41;
            this.label4.Text = "登录名：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(65, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 42;
            this.label5.Text = "密码：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(65, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 43;
            this.label6.Text = "姓名：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(65, 255);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 20);
            this.label9.TabIndex = 45;
            this.label9.Text = "固话：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(65, 390);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 20);
            this.label11.TabIndex = 47;
            this.label11.Text = "备注：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(37, 341);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 20);
            this.label12.TabIndex = 48;
            this.label12.Text = "用户角色：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(400, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 79;
            this.label2.Text = "归属机构：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(400, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 80;
            this.label7.Text = "确认密码：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(427, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 20);
            this.label8.TabIndex = 81;
            this.label8.Text = "邮箱：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(427, 255);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 20);
            this.label10.TabIndex = 46;
            this.label10.Text = "手机：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(129, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 89;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnUpload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnUpload.HoverBackColor = System.Drawing.Color.Empty;
            this.btnUpload.Location = new System.Drawing.Point(335, 30);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 30);
            this.btnUpload.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.btnUpload.TabIndex = 90;
            this.btnUpload.Text = "上传";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnSelect.HoverBackColor = System.Drawing.Color.Empty;
            this.btnSelect.Location = new System.Drawing.Point(238, 30);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 30);
            this.btnSelect.Style = Xr.Common.Controls.ButtonStyle.Query;
            this.btnSelect.TabIndex = 91;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(39, 120);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 20);
            this.label13.TabIndex = 92;
            this.label13.Text = "用户类型：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(12, 300);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 20);
            this.label14.TabIndex = 94;
            this.label14.Text = "更换周期限制：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnSave.HoverBackColor = System.Drawing.Color.Empty;
            this.btnSave.Location = new System.Drawing.Point(538, 557);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.Style = Xr.Common.Controls.ButtonStyle.Save;
            this.btnSave.TabIndex = 98;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
            this.btnCancel.HoverBackColor = System.Drawing.Color.Empty;
            this.btnCancel.Location = new System.Drawing.Point(647, 557);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.Style = Xr.Common.Controls.ButtonStyle.Return;
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(196)))), ((int)(((byte)(203)))));
            this.panelEx1.BorderSize = 1;
            this.panelEx1.BorderStyleBottom = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.BorderStyleLeft = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.BorderStyleRight = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.BorderStyleTop = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.panelEx1.Controls.Add(this.richEditor1);
            this.panelEx1.Location = new System.Drawing.Point(127, 390);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Padding = new System.Windows.Forms.Padding(1);
            this.panelEx1.Size = new System.Drawing.Size(593, 146);
            this.panelEx1.TabIndex = 101;
            // 
            // richEditor1
            // 
            this.richEditor1.BorderColor = System.Drawing.Color.Black;
            this.richEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditor1.HeightLightBolorColor = System.Drawing.Color.Black;
            this.richEditor1.ImagUploadUrl = null;
            this.richEditor1.Location = new System.Drawing.Point(1, 1);
            this.richEditor1.Name = "richEditor1";
            this.richEditor1.Size = new System.Drawing.Size(591, 144);
            this.richEditor1.TabIndex = 101;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(121, 163);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(7, 14);
            this.labelControl1.TabIndex = 102;
            this.labelControl1.Text = "*";
            // 
            // UserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(781, 608);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.radioGroup2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkedListBoxControl1);
            this.Controls.Add(this.textEdit7);
            this.Controls.Add(this.teEmail);
            this.Controls.Add(this.tePassword2);
            this.Controls.Add(this.treeOffice);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textEdit6);
            this.Controls.Add(this.teName);
            this.Controls.Add(this.tePassword);
            this.Controls.Add(this.teLoginName);
            this.Controls.Add(this.treeCompany);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户添加";
            this.Load += new System.EventHandler(this.UserEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOffice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Xr.Common.Controls.DataController dcUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeCompany;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.TextEdit teLoginName;
        private DevExpress.XtraEditors.TextEdit tePassword;
        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraEditors.TextEdit textEdit6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeOffice;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.TextEdit tePassword2;
        private DevExpress.XtraEditors.TextEdit teEmail;
        private DevExpress.XtraEditors.TextEdit textEdit7;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Xr.Common.Controls.ButtonControl btnUpload;
        private Xr.Common.Controls.ButtonControl btnSelect;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private System.Windows.Forms.Label label14;
        private Xr.Common.Controls.ButtonControl btnSave;
        private Xr.Common.Controls.ButtonControl btnCancel;
        private Xr.Common.Controls.PanelEx panelEx1;
        private Xr.Common.Controls.RichEditor richEditor1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
