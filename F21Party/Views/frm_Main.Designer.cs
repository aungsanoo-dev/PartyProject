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
            this.munProfile = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuItemRequests = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPartyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.mnuTeamManagment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuMasterData,
            this.processToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(812, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogIn,
            this.mnuRegister,
            this.munProfile,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(49, 27);
            this.mnuFile.Text = "File";
            // 
            // mnuLogIn
            // 
            this.mnuLogIn.Name = "mnuLogIn";
            this.mnuLogIn.Size = new System.Drawing.Size(155, 28);
            this.mnuLogIn.Text = "Login";
            this.mnuLogIn.Click += new System.EventHandler(this.mnuLogin_Click);
            // 
            // mnuRegister
            // 
            this.mnuRegister.Name = "mnuRegister";
            this.mnuRegister.Size = new System.Drawing.Size(155, 28);
            this.mnuRegister.Text = "Register";
            this.mnuRegister.Click += new System.EventHandler(this.mnuRegister_Click);
            // 
            // munProfile
            // 
            this.munProfile.Name = "munProfile";
            this.munProfile.Size = new System.Drawing.Size(155, 28);
            this.munProfile.Text = "Profile";
            this.munProfile.Click += new System.EventHandler(this.munProfile_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(155, 28);
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
            this.mnuMasterData.Size = new System.Drawing.Size(112, 27);
            this.mnuMasterData.Text = "MasterData";
            // 
            // mnuAccess
            // 
            this.mnuAccess.Name = "mnuAccess";
            this.mnuAccess.Size = new System.Drawing.Size(210, 28);
            this.mnuAccess.Text = "Access";
            this.mnuAccess.Click += new System.EventHandler(this.mnuAccess_Click);
            // 
            // mnuPermission
            // 
            this.mnuPermission.Name = "mnuPermission";
            this.mnuPermission.Size = new System.Drawing.Size(210, 28);
            this.mnuPermission.Text = "Permission";
            this.mnuPermission.Click += new System.EventHandler(this.mnuPermission_Click);
            // 
            // mnuPermissionType
            // 
            this.mnuPermissionType.Name = "mnuPermissionType";
            this.mnuPermissionType.Size = new System.Drawing.Size(210, 28);
            this.mnuPermissionType.Text = "PermissionType";
            this.mnuPermissionType.Click += new System.EventHandler(this.mnuPermissionType_Click);
            // 
            // mnuPosition
            // 
            this.mnuPosition.Name = "mnuPosition";
            this.mnuPosition.Size = new System.Drawing.Size(210, 28);
            this.mnuPosition.Text = "Position";
            this.mnuPosition.Click += new System.EventHandler(this.mnuPosition_Click);
            // 
            // mnuPage
            // 
            this.mnuPage.Name = "mnuPage";
            this.mnuPage.Size = new System.Drawing.Size(210, 28);
            this.mnuPage.Text = "Page";
            this.mnuPage.Click += new System.EventHandler(this.mnuPage_Click);
            // 
            // mnuUser
            // 
            this.mnuUser.Name = "mnuUser";
            this.mnuUser.Size = new System.Drawing.Size(210, 28);
            this.mnuUser.Text = "User";
            this.mnuUser.Click += new System.EventHandler(this.mnuEmployee_Click);
            // 
            // mnuAccounts
            // 
            this.mnuAccounts.Name = "mnuAccounts";
            this.mnuAccounts.Size = new System.Drawing.Size(210, 28);
            this.mnuAccounts.Text = "Accounts";
            this.mnuAccounts.Click += new System.EventHandler(this.mnuAccounts_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDonation,
            this.mnuItemRequests,
            this.mnuPartyItem,
            this.mnuTeam,
            this.mnuTeamManagment});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(62, 27);
            this.processToolStripMenuItem.Text = "Party";
            // 
            // mnuDonation
            // 
            this.mnuDonation.Name = "mnuDonation";
            this.mnuDonation.Size = new System.Drawing.Size(227, 28);
            this.mnuDonation.Text = "Donation";
            this.mnuDonation.Click += new System.EventHandler(this.mnuDonation_Click);
            // 
            // mnuItemRequests
            // 
            this.mnuItemRequests.Name = "mnuItemRequests";
            this.mnuItemRequests.Size = new System.Drawing.Size(227, 28);
            this.mnuItemRequests.Text = "ItemRequests";
            this.mnuItemRequests.Click += new System.EventHandler(this.mnuItemRequests_Click);
            // 
            // mnuPartyItem
            // 
            this.mnuPartyItem.Name = "mnuPartyItem";
            this.mnuPartyItem.Size = new System.Drawing.Size(227, 28);
            this.mnuPartyItem.Text = "PartyItem";
            this.mnuPartyItem.Click += new System.EventHandler(this.mnuPartyItem_Click);
            // 
            // mnuTeam
            // 
            this.mnuTeam.Name = "mnuTeam";
            this.mnuTeam.Size = new System.Drawing.Size(227, 28);
            this.mnuTeam.Text = "Team";
            this.mnuTeam.Click += new System.EventHandler(this.mnuTeam_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(571, 58);
            this.label1.TabIndex = 2;
            this.label1.Text = "Welcome To The Party!";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.btnRegister);
            this.panel1.Controls.Add(this.btnLogIn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(29, 31);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 357);
            this.panel1.TabIndex = 3;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Blue;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(530, 228);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(116, 44);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.TabStop = false;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.White;
            this.btnLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.Color.Black;
            this.btnLogIn.Location = new System.Drawing.Point(126, 228);
            this.btnLogIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(116, 44);
            this.btnLogIn.TabIndex = 3;
            this.btnLogIn.TabStop = false;
            this.btnLogIn.Text = "LogIn";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // mnuTeamManagment
            // 
            this.mnuTeamManagment.Name = "mnuTeamManagment";
            this.mnuTeamManagment.Size = new System.Drawing.Size(227, 28);
            this.mnuTeamManagment.Text = "TeamManagment";
            this.mnuTeamManagment.Click += new System.EventHandler(this.mnuTeamManagment_Click);
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 388);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_Main";
            this.Text = "Project F21Party";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem mnuItemRequests;
        public System.Windows.Forms.ToolStripMenuItem mnuLogIn;
        public System.Windows.Forms.ToolStripMenuItem mnuRegister;
        private System.Windows.Forms.ToolStripMenuItem mnuPosition;
        private System.Windows.Forms.ToolStripMenuItem mnuPermissionType;
        private System.Windows.Forms.ToolStripMenuItem mnuPage;
        private System.Windows.Forms.ToolStripMenuItem munProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnRegister;
        public System.Windows.Forms.Button btnLogIn;
        public System.Windows.Forms.ToolStripMenuItem mnuPartyItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTeam;
        private System.Windows.Forms.ToolStripMenuItem mnuTeamManagment;
    }
}