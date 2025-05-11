namespace F21Party.Views
{
    partial class frm_CreatePermission
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
            this.label5 = new System.Windows.Forms.Label();
            this.cboPermissionType = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.cboAccessLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboAccessValue = new System.Windows.Forms.ComboBox();
            this.cboPageName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 208);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Permission Type:";
            // 
            // cboPermissionType
            // 
            this.cboPermissionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPermissionType.FormattingEnabled = true;
            this.cboPermissionType.Location = new System.Drawing.Point(230, 205);
            this.cboPermissionType.Margin = new System.Windows.Forms.Padding(2);
            this.cboPermissionType.Name = "cboPermissionType";
            this.cboPermissionType.Size = new System.Drawing.Size(221, 21);
            this.cboPermissionType.TabIndex = 38;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(382, 343);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(69, 23);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(230, 343);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(69, 23);
            this.btnCreate.TabIndex = 36;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // cboAccessLevel
            // 
            this.cboAccessLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccessLevel.FormattingEnabled = true;
            this.cboAccessLevel.Location = new System.Drawing.Point(230, 64);
            this.cboAccessLevel.Margin = new System.Windows.Forms.Padding(2);
            this.cboAccessLevel.Name = "cboAccessLevel";
            this.cboAccessLevel.Size = new System.Drawing.Size(221, 21);
            this.cboAccessLevel.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 274);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Access Value:";
            // 
            // cboAccessValue
            // 
            this.cboAccessValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccessValue.FormattingEnabled = true;
            this.cboAccessValue.Location = new System.Drawing.Point(230, 271);
            this.cboAccessValue.Margin = new System.Windows.Forms.Padding(2);
            this.cboAccessValue.Name = "cboAccessValue";
            this.cboAccessValue.Size = new System.Drawing.Size(221, 21);
            this.cboAccessValue.TabIndex = 41;
            // 
            // cboPageName
            // 
            this.cboPageName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPageName.FormattingEnabled = true;
            this.cboPageName.Location = new System.Drawing.Point(230, 138);
            this.cboPageName.Margin = new System.Windows.Forms.Padding(2);
            this.cboPageName.Name = "cboPageName";
            this.cboPageName.Size = new System.Drawing.Size(221, 21);
            this.cboPageName.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 141);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Page Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Access Level:";
            // 
            // frm_CreatePermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 434);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboPageName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboAccessValue);
            this.Controls.Add(this.cboAccessLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboPermissionType);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreate);
            this.Name = "frm_CreatePermission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Permission";
            this.Load += new System.EventHandler(this.frm_CreatePermission_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cboPermissionType;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Button btnCreate;
        public System.Windows.Forms.ComboBox cboAccessLevel;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cboAccessValue;
        public System.Windows.Forms.ComboBox cboPageName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}