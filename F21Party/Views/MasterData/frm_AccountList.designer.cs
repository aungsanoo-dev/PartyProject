
namespace F21Party.Views
{
    partial class frm_AccountList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AccountList));
            this.dgvAccountSetting = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslLabel = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmUserName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAccessLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.tstSearchWith = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountSetting)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAccountSetting
            // 
            this.dgvAccountSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccountSetting.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAccountSetting.Location = new System.Drawing.Point(0, 33);
            this.dgvAccountSetting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvAccountSetting.MultiSelect = false;
            this.dgvAccountSetting.Name = "dgvAccountSetting";
            this.dgvAccountSetting.RowHeadersWidth = 62;
            this.dgvAccountSetting.RowTemplate.Height = 28;
            this.dgvAccountSetting.Size = new System.Drawing.Size(947, 326);
            this.dgvAccountSetting.TabIndex = 3;
            this.dgvAccountSetting.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAccountSetting_CellFormatting);
            this.dgvAccountSetting.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvAccountSetting_DataBindingComplete);
            this.dgvAccountSetting.DoubleClick += new System.EventHandler(this.dgvUserSetting_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.toolStripSeparator1,
            this.tsbEdit,
            this.toolStripSeparator2,
            this.tsbDelete,
            this.toolStripSeparator3,
            this.tslLabel,
            this.tstSearchWith,
            this.toolStripSeparator4,
            this.tsbUser,
            this.toolStripSeparator5,
            this.tsbExit,
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(947, 33);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.AutoSize = false;
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(60, 30);
            this.tsbNew.Text = "New";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbEdit
            // 
            this.tsbEdit.AutoSize = false;
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(120, 30);
            this.tsbEdit.Text = "Edit AccessLevel";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbDelete
            // 
            this.tsbDelete.AutoSize = false;
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(60, 30);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // tslLabel
            // 
            this.tslLabel.AutoSize = false;
            this.tslLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUserName,
            this.tsmAccessLevel});
            this.tslLabel.Image = ((System.Drawing.Image)(resources.GetObject("tslLabel.Image")));
            this.tslLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tslLabel.Name = "tslLabel";
            this.tslLabel.Size = new System.Drawing.Size(130, 30);
            this.tslLabel.Text = "UserName";
            this.tslLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsmUserName
            // 
            this.tsmUserName.Name = "tsmUserName";
            this.tsmUserName.Size = new System.Drawing.Size(224, 26);
            this.tsmUserName.Text = "UserName";
            this.tsmUserName.Click += new System.EventHandler(this.tsmUserName_Click);
            // 
            // tsmAccessLevel
            // 
            this.tsmAccessLevel.Name = "tsmAccessLevel";
            this.tsmAccessLevel.Size = new System.Drawing.Size(224, 26);
            this.tsmAccessLevel.Text = "AccessLevel";
            this.tsmAccessLevel.Click += new System.EventHandler(this.tsmAccessLevel_Click);
            // 
            // tstSearchWith
            // 
            this.tstSearchWith.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tstSearchWith.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tstSearchWith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstSearchWith.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstSearchWith.Name = "tstSearchWith";
            this.tstSearchWith.Size = new System.Drawing.Size(178, 33);
            this.tstSearchWith.TextChanged += new System.EventHandler(this.tstSearchWith_TextChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbUser
            // 
            this.tsbUser.AutoSize = false;
            this.tsbUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbUser.Image")));
            this.tsbUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUser.Name = "tsbUser";
            this.tsbUser.Size = new System.Drawing.Size(60, 30);
            this.tsbUser.Text = "Users";
            this.tsbUser.Click += new System.EventHandler(this.tsbUser_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbExit
            // 
            this.tsbExit.AutoSize = false;
            this.tsbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(60, 30);
            this.tsbExit.Text = "Exit";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 33);
            // 
            // frm_AccountList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 359);
            this.Controls.Add(this.dgvAccountSetting);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_AccountList";
            this.Text = "Account List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_AccountList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountSetting)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.DataGridView dgvAccountSetting;
        public System.Windows.Forms.ToolStripTextBox tstSearchWith;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        public System.Windows.Forms.ToolStripButton tsbUser;
        public System.Windows.Forms.ToolStripButton tsbNew;
        public System.Windows.Forms.ToolStripButton tsbEdit;
        public System.Windows.Forms.ToolStripButton tsbDelete;
        public System.Windows.Forms.ToolStripSplitButton tslLabel;
        private System.Windows.Forms.ToolStripMenuItem tsmUserName;
        private System.Windows.Forms.ToolStripMenuItem tsmAccessLevel;
    }
}