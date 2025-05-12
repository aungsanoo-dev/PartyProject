namespace F21Party.Views
{
    partial class frm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.profileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMasterData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPermissionType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPosition = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccounts = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDonation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuMasterData,
            this.processToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogIn,
            this.mnuRegister,
            this.profileToolStripMenuItem1,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(54, 29);
            this.mnuFile.Text = "File";
            // 
            // mnuLogIn
            // 
            this.mnuLogIn.Name = "mnuLogIn";
            this.mnuLogIn.Size = new System.Drawing.Size(177, 34);
            this.mnuLogIn.Text = "Login";
            this.mnuLogIn.Click += new System.EventHandler(this.mnuLogin_Click);
            // 
            // mnuRegister
            // 
            this.mnuRegister.Name = "mnuRegister";
            this.mnuRegister.Size = new System.Drawing.Size(177, 34);
            this.mnuRegister.Text = "Register";
            this.mnuRegister.Click += new System.EventHandler(this.mnuRegister_Click);
            // 
            // profileToolStripMenuItem1
            // 
            this.profileToolStripMenuItem1.Name = "profileToolStripMenuItem1";
            this.profileToolStripMenuItem1.Size = new System.Drawing.Size(177, 34);
            this.profileToolStripMenuItem1.Text = "Profile";
            this.profileToolStripMenuItem1.Click += new System.EventHandler(this.profileToolStripMenuItem1_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(177, 34);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuMasterData
            // 
            this.mnuMasterData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAccess,
            this.mnuPermission,
            this.mnuPermissionType,
            this.mnuPosition,
            this.mnuPage,
            this.mnuUser,
            this.mnuAccounts});
            this.mnuMasterData.Name = "mnuMasterData";
            this.mnuMasterData.Size = new System.Drawing.Size(119, 29);
            this.mnuMasterData.Text = "MasterData";
            // 
            // mnuAccess
            // 
            this.mnuAccess.Name = "mnuAccess";
            this.mnuAccess.Size = new System.Drawing.Size(270, 34);
            this.mnuAccess.Text = "Access";
            this.mnuAccess.Click += new System.EventHandler(this.mnuAccess_Click);
            // 
            // mnuPermission
            // 
            this.mnuPermission.Name = "mnuPermission";
            this.mnuPermission.Size = new System.Drawing.Size(270, 34);
            this.mnuPermission.Text = "Permission";
            this.mnuPermission.Click += new System.EventHandler(this.mnuPermission_Click);
            // 
            // mnuPermissionType
            // 
            this.mnuPermissionType.Name = "mnuPermissionType";
            this.mnuPermissionType.Size = new System.Drawing.Size(270, 34);
            this.mnuPermissionType.Text = "PermissionType";
            // 
            // mnuPosition
            // 
            this.mnuPosition.Name = "mnuPosition";
            this.mnuPosition.Size = new System.Drawing.Size(270, 34);
            this.mnuPosition.Text = "Position";
            // 
            // mnuPage
            // 
            this.mnuPage.Name = "mnuPage";
            this.mnuPage.Size = new System.Drawing.Size(270, 34);
            this.mnuPage.Text = "Page";
            this.mnuPage.Click += new System.EventHandler(this.mnuPage_Click);
            // 
            // mnuUser
            // 
            this.mnuUser.Name = "mnuUser";
            this.mnuUser.Size = new System.Drawing.Size(270, 34);
            this.mnuUser.Text = "User";
            this.mnuUser.Click += new System.EventHandler(this.mnuEmployee_Click);
            // 
            // mnuAccounts
            // 
            this.mnuAccounts.Name = "mnuAccounts";
            this.mnuAccounts.Size = new System.Drawing.Size(270, 34);
            this.mnuAccounts.Text = "Accounts";
            this.mnuAccounts.Click += new System.EventHandler(this.mnuAccounts_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDonation,
            this.mnuItemRequest});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(88, 29);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // mnuDonation
            // 
            this.mnuDonation.Name = "mnuDonation";
            this.mnuDonation.Size = new System.Drawing.Size(213, 34);
            this.mnuDonation.Text = "Donation";
            // 
            // mnuItemRequest
            // 
            this.mnuItemRequest.Name = "mnuItemRequest";
            this.mnuItemRequest.Size = new System.Drawing.Size(213, 34);
            this.mnuItemRequest.Text = "ItemRequest";
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 449);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frm_Main";
            this.Text = "Project F21Party";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuMasterData;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuAccess;
        private System.Windows.Forms.ToolStripMenuItem mnuPermission;
        private System.Windows.Forms.ToolStripMenuItem mnuUser;
        private System.Windows.Forms.ToolStripMenuItem mnuAccounts;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDonation;
        private System.Windows.Forms.ToolStripMenuItem mnuItemRequest;
        public System.Windows.Forms.ToolStripMenuItem mnuLogIn;
        public System.Windows.Forms.ToolStripMenuItem mnuRegister;
        private System.Windows.Forms.ToolStripMenuItem mnuPosition;
        private System.Windows.Forms.ToolStripMenuItem mnuPermissionType;
        private System.Windows.Forms.ToolStripMenuItem mnuPage;
        private System.Windows.Forms.ToolStripMenuItem profileToolStripMenuItem1;
    }
}